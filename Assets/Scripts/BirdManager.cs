using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    //Notes : 
    //Cohesion = centre point all neighbors will steer toward to, in this case either player or center of the group
    //Alignment = The avg heading of all neighbors when moving in common direction
    //Avoidance = if too close, move away. Opposite of cohesion.
    public BirdObject birdPrefab;
    public List<BirdObject> birdList = new List<BirdObject>();

    public int birdCount = 5;
    float birdDensity = 0.08f;

    public float driveFactor = 10f;
    public float maxSpeed = 5f;

    public float neighborRadius = 15f;
    public float avoidRadiusMult = .5f;

    float sqMaxSpeed;
    float sqNeighborRadius;
    float sqAvoidRadius;
    public float SqAvoidRadius { get { return sqAvoidRadius; } }

    //Behaviour stuff
    Vector2[] behaviourVectors;
    public float[] weights;
    Vector2 currVelocity;
    public float birdSmoothTime = .5f;

    public float radius = 50f;

    public bool trackPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        sqMaxSpeed = maxSpeed * maxSpeed;
        sqNeighborRadius = neighborRadius * neighborRadius;
        sqAvoidRadius = sqNeighborRadius * avoidRadiusMult * avoidRadiusMult;

        for(int i =0; i< birdCount; i++)
        {
            BirdObject birb = Instantiate(birdPrefab, Random.insideUnitCircle * birdCount * birdDensity, Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),transform);
            birb.name = "Bird " + i;
            birdList.Add(birb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(BirdObject bird in birdList)
        {
            List<Transform> context = GetNeighbors(bird);
            GameObject objToTrack;
            //bird.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.green, context.Count / 6f);
            if(trackPlayer)
            {
                objToTrack = PlayerScript.instance.gameObject;
            }
            else
            {
                objToTrack = gameObject;
            }

          
            Vector2 steer = CombinedBehaviour(bird,context,objToTrack);
            steer *= driveFactor;
            if (steer.sqrMagnitude > sqMaxSpeed)
            {
                steer = steer.normalized * maxSpeed * PlayerScript.instance.movespeedMult;
            }
            bird.Move(steer);
        }
    }
    
    List<Transform> GetNeighbors(BirdObject bird)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCol = Physics2D.OverlapCircleAll(bird.transform.position, neighborRadius);
        foreach(Collider2D c in contextCol)
        {
            if(c!= bird.BirdCollider) //don't add yourself
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    public Vector2 CombinedBehaviour(BirdObject bird, List<Transform> context, GameObject gameObj)
    {
        behaviourVectors = new Vector2[4];
        behaviourVectors[0] = CohesionMove(bird, context, gameObj);
        behaviourVectors[1] = AlignmentMove(bird, context, gameObj);
        behaviourVectors[2] = AvoidMove(bird, context, gameObj);
        behaviourVectors[3] = CenterMove(bird, context, gameObj);
        if (weights.Length != behaviourVectors.Length)
        {
            throw new System.Exception("Number of weights do not match with number of behaviours!");
        }
        Vector2 move = Vector2.zero;
        //iterate through behaviours
        for(int i=0; i< behaviourVectors.Length; i++)
        {
            Vector2 partialMove = behaviourVectors[i]*weights[i];
            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > (weights[i]*weights[i]))
                    {
                    partialMove.Normalize();
                    partialMove*= weights[i];
                }
                move += partialMove;
            }
        }
        return move;
    }

    public Vector2 CohesionMove(BirdObject bird, List<Transform> context, GameObject gameObj)
    {
        if(context.Count == 0)  //no neighbors no cohesion
        {
            return Vector2.zero;
        }
        Vector2 cohesionMove = Vector2.zero;
        //foreach (Transform obj in context)
        //{
        //    cohesionMove += (Vector2)obj.position;
        //}
        //cohesionMove /= context.Count;
        //cohesionMove -= (Vector2)bird.transform.position;
        cohesionMove = gameObj.transform.position - bird.transform.position;    //offset the pos
        cohesionMove = Vector2.SmoothDamp(bird.transform.up, cohesionMove, ref currVelocity, birdSmoothTime);
        //return PlayerScript.instance.transform.position;
        return cohesionMove; //cohesion to flock manager?
    }
    public Vector2 CenterMove(BirdObject bird, List<Transform> context, GameObject gameObj)
    {
        Vector2 centerMove = Vector2.zero;
        centerMove = gameObj.transform.position - bird.transform.position;    //offset the pos
        float r = centerMove.magnitude / radius;
        if(r < 0.9f) //if within 90% ignore 0 for centre, 1 for far as fuck boiiii
        {
            return Vector2.zero;
        }
        return centerMove * r * r;
    }

    public Vector2 AlignmentMove(BirdObject bird, List<Transform> context, GameObject gameObj)
    {
        if(context.Count == 0)  //if no neighbor continue facing the same dir
        {
            return bird.transform.up;
        }
        Vector2 alignmentMove = Vector2.zero;
        foreach(Transform obj in context)
        {
            alignmentMove += (Vector2)obj.transform.up;
        }
        alignmentMove /= context.Count;
        //return gameObj.transform.up;
        return alignmentMove;
    }
    public Vector2 AvoidMove(BirdObject bird, List<Transform> context, GameObject gameObj)
    {
        if (context.Count == 0)  //no neighbors no cohesion
        {
            return Vector2.zero;
        }
        Vector2 avoidMove = Vector2.zero;
        int nAvoid = 0; 
        foreach(Transform obj in context)
        {
            if(Vector2.SqrMagnitude(obj.position-bird.transform.position)<sqAvoidRadius)
            {
                nAvoid++;
                avoidMove += (Vector2)(bird.transform.position - obj.position);
            }
        }
        if(nAvoid >0)
        {
            avoidMove /= nAvoid;
        }
        return avoidMove;
    }
    public void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(PlayerScript.instance.transform.position, radius);
        if(PlayerScript.instance != null)
            Gizmos.DrawWireSphere(PlayerScript.instance.transform.position, radius);
    }

}
