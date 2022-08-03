using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IVLab.MinVR3;

public class DomeRain : MonoBehaviour
{
    public SphericalDome dome;

    public string text = "Gimiwan! It is raining in the dome.";
    public GameObject letterPrefab;

    [Range(0, 10000)]
    public int numRaindrops = 1000;

    [Range(0.0f, 10.0f)]
    public float initialSpeed = 1.0f;

    [Range(0.0f, 10.0f)]
    public float acceleration = 1.0f;


    private List<GameObject> raindropObjs;
    private List<float> speeds;
    private List<bool> hasHitDome;
    private System.Random rand;


    // Start is called before the first frame update
    void Start()
    {
        raindropObjs = new List<GameObject>();
        speeds = new List<float>();
        hasHitDome = new List<bool>();

        rand = new System.Random();

        for (int i = 0; i < numRaindrops; i++) {

            int randomLetterIndex = rand.Next() % text.Length;
            string letter = text.Substring(randomLetterIndex, 1);

            GameObject letterObj = Instantiate(letterPrefab, transform, false);
            letterObj.name = "Raindrop " + letter;

            TextMeshPro tmPro = letterObj.GetComponent<TextMeshPro>();
            tmPro.SetText(letter);
            float bluishHue = 0.58f + 0.25f * (float)rand.NextDouble();
            float saturation = 0.3f + 0.5f * (float)rand.NextDouble();
            float value = 0.2f + 0.5f * (float)rand.NextDouble();
            tmPro.color = Color.HSVToRGB(bluishHue, saturation, value);

            raindropObjs.Add(letterObj);
            speeds.Add(0.0f);
            hasHitDome.Add(false);

            InitializeWithRandomPosition(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < raindropObjs.Count; i++) {
            GameObject obj = raindropObjs[i];

            Vector3 downInRoomSpace = -Vector3.up;
            Vector3 downInDomeSpace = dome.RoomDirectionToDomeSpace(downInRoomSpace);

            Vector3 pos = obj.transform.localPosition;

            speeds[i] += acceleration * Time.deltaTime;

            Vector3 vel = speeds[i] * downInDomeSpace;

            pos = pos + Time.deltaTime * vel;

            if (dome.IsPointInsideDome(pos)) {
                pos = dome.ClosestPointOnDome(pos);
                obj.transform.localRotation = dome.InwardFacingRotation(pos);

                if (!hasHitDome[i]) {
                    // just landed on the dome, reset speed to zero
                    speeds[i] = 0.0f;
                }
                hasHitDome[i] = true;
            }

            obj.transform.localPosition = pos;

            // if this new position would mean it slides off the bottom of the dome, recycle
            // the rain by re-initializing with random values
            SphericalCoordinate s = dome.RectangularPointToSpherical(pos);
            if (s.polarAngleInDeg > dome.maxPolarAngleInView + 10.0f) {                
                InitializeWithRandomPosition(i);
            }
        }
    }

    void InitializeWithRandomPosition(int index)
    {
        Vector3 posInDomeSpace = dome.RandomPointOnDome();

        Vector3 upInRoomSpace = Vector3.up;
        Vector3 upInDomeSpace = dome.RoomDirectionToDomeSpace(upInRoomSpace);

        posInDomeSpace += 10.0f * (float)rand.NextDouble() * upInDomeSpace;

        raindropObjs[index].transform.localPosition = posInDomeSpace;
        Vector3 raindropForward = Vector3.up;
        Vector3 raindropUp = new Vector3(-posInDomeSpace.x, 0, -posInDomeSpace.z).normalized;
        raindropObjs[index].transform.localRotation = Quaternion.LookRotation(raindropForward, raindropUp);

        speeds[index] = initialSpeed;
        hasHitDome[index] = false;
    }
    
}
