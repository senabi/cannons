using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    // [SerializeField] private ParticleSystem _launchParticles;
    [SerializeField] private GameObject _explosion;
    private bool _isGhost;

    private bool _onAir = true;
    public bool fromPlayer = false;
    public bool collisioned = false;
    // private Rigidbody _rb;

    private bool readyToDestroy = false;

    // public WindowGraph windowGraph;

    public void Init(Vector3 velocity, bool isGhost)
    {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
        lsty = new List<float>();
        // if (fromPlayer){

        // }
        // windowGraph = graph;
        // _rb.velocity = velocity; // if velocity >= 31 the object disappears idky
    }

    private float _destroyPeriod = 0f;
    private float _destroyInterval = 0.1f;
    private List<float> lsty;
    private List<float> intervalLst = new List<float>();
    private float period = 0.0f;
    public float interval = 0.25f;
    private bool sent = false;
    // [SerializeField] private Transform player;
    [SerializeField] private WindowGraph graph;
    void Update()
    {
        if (readyToDestroy || _rb.position.y < 0)
        {
            if (_destroyPeriod > _destroyInterval)
            {
                Destroy(this.gameObject);
            }
            _destroyPeriod += Time.deltaTime;
            if (fromPlayer && !sent)
            {
                var graph = GameObject.FindGameObjectsWithTag("Graph")[0].GetComponent<WindowGraph>();
                if (graph != null)
                {
                    graph.ShowGraph(lsty, intervalLst, lsty.Max());
                }
                sent = true;
            }
        }
        if (fromPlayer)
        {
            if (period > interval && !sent)
            {
                Debug.Log("Position Y: " + transform.position.y);
                lsty.Add(transform.position.y);
                if (intervalLst.Count == 0)
                {
                    intervalLst.Add(0.0f);
                }
                else
                {
                    intervalLst.Add(intervalLst.Last() + interval);
                }
                period = 0;
            }
            period += Time.deltaTime;
        }
        // if (readyToDestroy || _rb.position.y < -5)
        // {
        //     Debug.Log(lsty.ToString());
        // }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (_isGhost) return;
        // Debug.Log("Tag: " + col.collider.tag);
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        Instantiate(_explosion, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        // _source.clip = _clips[Random.Range(0, _clips.Length)];
        _onAir = false;
        _source.clip = _clips[0];
        _source.Play();
        if (col.collider.tag == "Island")
        {
            Debug.Log(col.collider.tag);
        }
        if (col.collider.tag == "Cannon")
        {
            var cannon = col.collider.gameObject.GetComponentInParent<Cannon>();
            cannon.TakeDamage(5);
        }
        // if (fromPlayer)
        // {
        // cannon._graph.ShowGraph(lsty, lsty.Max());
        // Debug.Log(lsty.ToString());
        // }
        readyToDestroy = true;
        // Debug.Log("played");
        // Debug.Log("destroyed");
        // Destroy(this.gameObject);
        // _launchParticles.Play();
    }
    // public void PlayDettaching()
    // {
    // var detachedGo = transform.Find("Audio").gameObject;
    // var audioSource = detachedGo.GetComponent<AudioSource>();
    // audioSource.Play();
    // detachedGo.transform.parent = null;
    // Destroy(detachedGo, audioSource.clip.length);
    // }
}