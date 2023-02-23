using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    private SceneController sceneController;

    private float speed;
    public float obstacleRange = 5.0f;

    private bool _alive;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.Find("Controller").GetComponent<SceneController>();
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = sceneController.speed;

        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //void OnEnable()
    //{
    //    Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}

    //void OnDisable()
    //{
    //    Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    //}

    //public void OnSpeedChanged(float value)
    //{
    //    speed = baseSpeed * value;
    //}

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
