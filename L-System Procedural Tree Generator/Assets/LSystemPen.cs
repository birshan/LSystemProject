using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemPen : MonoBehaviour
{
    public string axiom;
    public int iterations;
    public float length = 10f;
    public float angle;
    // public bool doBranchWidth = false;
    public float initialThickness = 5f;
    public float contractionRatio = 0.707f; // Leonardo da Vinci's rule - 'Algorithmic Beauty of Plants'
    //public bool is3d = true;
    private Color brown;
    private Color darkestBrown;
    private Color darkBrown;
    private Color darkestGreen;
    public int randomAngle = 20;
    public int randomAngleChange = 5;
    private struct TransformData
    {
        public Vector3 position;
        // public Vector3 direction;
        public Quaternion rotation;
        public float thickness;

        public TransformData(Vector3 position, Quaternion rotation, float thickness)
        {
            this.position = position;
            this.rotation = rotation;
            this.thickness = thickness;
        }
    }
    private Stack<TransformData> _stack = new Stack<TransformData>(); // to go back to a point and draw from that point
    // private LineRenderer _lineRenderer;
    // private List<Vector3> _linePositions = new List<Vector3>();
    private List<LineRenderer> _lineRenderers = new List<LineRenderer>();
    public void DrawLSystem(string LString)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        // change rotation by 180 on y axis
        // transform.Rotate(Vector3.up, 180);
        if(_lineRenderers.Count > 0)
        {
            foreach(LineRenderer lineRenderer in _lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }
            _lineRenderers.Clear();
        }
        iterations= UIConfig.Iterations;
        axiom = UIConfig.Axiom;
        angle = UIConfig.Angle;

        float currentThickness = initialThickness;
        Color currentBrown = darkestBrown;

        int currentDepth = 0;
        int maxDepth = 0;
        Dictionary<int, List<LineRenderer>> depthofLines = new Dictionary<int, List<LineRenderer>>();
        foreach(char c in LString)
        {
            switch(c)
            {
                case 'F':
                    // DrawForward();
                    Vector3 startPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    Vector3 endPosition = transform.position;

                    // Create a new line renderer for this segment
                    // Debug.DrawLine(startPosition, transform.position, Color.green, 10000f);
                    LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, startPosition);
                    lineRenderer.SetPosition(1, endPosition);
                    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                    
                    // reduce color based on thickness
                    lineRenderer.startColor = currentBrown;
                    lineRenderer.endColor = currentBrown;
                    // _lineRenderers.Add(lineRenderer);
                    // // Line renderer - Using one line renderer didnt work since the tree needs to branch and theres a lot of end points
                    // _linePositions.Add(startPosition);
                    // _linePositions.Add(endPosition);
                    // _lineRenderer.positionCount = _linePositions.Count;
                    // _lineRenderer.SetPositions(_linePositions.ToArray());
                    // Set the width based on the current thickness
                    lineRenderer.startWidth = currentThickness;
                    lineRenderer.endWidth = currentThickness;// Keeping consistent width for each line segment

                    //use a softer shade of brown every time the thickness is reduced
                    

                    // Find out max depth to color leaves
                    if(!depthofLines.ContainsKey(currentDepth))
                    {
                        depthofLines[currentDepth] = new List<LineRenderer>();
                    }
                    depthofLines[currentDepth].Add(lineRenderer);
                    // Find out max depth to color leaves


                    _lineRenderers.Add(lineRenderer);
                    break;
                case '+':
                    // TurnRight();
                    transform.Rotate(Vector3.forward, angle);
                    
                    //transform.Rotate(Vector3.up, 30);
                    //randomly choose between -30 and30
                    // transform.Rotate(Vector3.up, Random.Range(-randomAngle, randomAngle));
                    if(UIConfig.Is3D)
                    {
                        if(UIConfig.DoSmoothBranching)
                        {
                            transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                        }
                        if(UIConfig.DoTwistedHorrorBranching)
                        {
                            transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                        }
                        if(UIConfig.DoChaoticBranching)
                        {
                            transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                        }
                        // transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                        // transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                        // transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                    }
              
                    break;
                case '-':
                    // TurnLeft();
                    transform.Rotate(Vector3.forward, -angle);
                    // transform.Rotate(Vector3.up, Random.Range(-randomAngle, randomAngle));
                    if(UIConfig.Is3D)
                    {
                        if(UIConfig.DoSmoothBranching)
                        {
                            transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                        }
                        if(UIConfig.DoTwistedHorrorBranching)
                        {
                            transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                        }
                        if(UIConfig.DoChaoticBranching)
                        {
                            transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                            transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                        }
                        // transform.Rotate(Vector3.right, Random.Range(-1, 1)*(randomAngle));
                        // transform.Rotate(Vector3.left, Random.Range(-1, 1)*(randomAngle));
                        // transform.Rotate(Vector3.up, Random.Range(-1, 1)*(randomAngle));
                    }
                    
                    
                    break;
                case '[':
                    // Push();
                    _stack.Push(new TransformData(transform.position, transform.rotation, currentThickness));
                    //reduce the thickness for the daughter segment using leonardo factor
                    currentThickness *= contractionRatio;

                    //leaves depth
                    currentDepth++;
                    maxDepth = Mathf.Max(maxDepth, currentDepth);

                    //lighten brown color
                    float intensity = currentThickness / initialThickness;
                    // currentBrown = Color.Lerp(brown, Color.white, 1-intensity);
                    currentBrown = Color.Lerp(darkestBrown, Color.white, 1-intensity);
                    // currentBrown = Color.Lerp(Color.black, Color.white, 1-intensity);

                    //increase random angle value every time a branch is created
                    randomAngle += randomAngleChange;
                    break;
                case ']':
                    // Pop();
                    TransformData data = _stack.Pop();
                    transform.position = data.position;
                    transform.rotation = data.rotation;
                    currentThickness = data.thickness;
                    currentDepth--;// depth tracking for leaves

                    //restore thickness of color
                    float restoreIntensity = currentThickness / initialThickness;
                    // currentBrown = Color.Lerp(brown, Color.white, 1-restoreIntensity);
                    currentBrown = Color.Lerp(darkestBrown, Color.white, 1-restoreIntensity);
                    // currentBrown = Color.Lerp(Color.black, Color.white, 1-restoreIntensity);
                    currentBrown = darkestBrown;

                    //reset random angle value every time back at the trunk
                    randomAngle -= randomAngleChange;
                    break;
            }
        }

        // Color leaves
        // Color autumnLeafColor = new Color(0.8f, 0.3f, 0.1f);
// 
        if(depthofLines.ContainsKey(maxDepth))
        {
            foreach(LineRenderer lineRenderer in depthofLines[maxDepth])
            {
                // lineRenderer.startColor = Color.green;
                // lineRenderer.endColor = Color.green;

                // Color DarkGreen = new Color(0.0f, 0.2f, 0.0f);
                // lineRenderer.startColor = DarkGreen;
                // lineRenderer.endColor = DarkGreen;

                Color autumnLeafRandomizedColor = new Color(
                    Random.Range(0.7f, 1.0f), //reddish
                    Random.Range(0.3f, 0.6f),//yellowish-orange
                    Random.Range(0.0f, 0.2f)//slight green
                );
                lineRenderer.startColor = autumnLeafRandomizedColor;
                lineRenderer.endColor = autumnLeafRandomizedColor;

                
            }
        }
    }
    
    private void Awake() {
        // _lineRenderer = GetComponent<LineRenderer>();
        brown = new Color(0.59f, 0.29f, 0.0f);
        darkestBrown = new Color(0.29f, 0.14f, 0.0f);
        darkBrown = new Color(0.39f, 0.19f, 0.0f);
        darkestGreen = new Color(0.0f, 0.2f, 0.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
