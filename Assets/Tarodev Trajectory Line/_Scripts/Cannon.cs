using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cannon : MonoBehaviour
{
    // [SerializeField] private Projection _projection; // DONT NEED
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public HealthBar healthBar;
    public ForceBar forceBar;
    public AngleSlider _angleSlider;
    public VelocitySlider _velocitySlider;

    public bool isPlayer = false;
    private float _currAngle;
    [SerializeField] private Transform _topCornerCam;
    [SerializeField] private Transform _enemyCannon;

    public WindowGraph _graph;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        if (this.tag == "Player")
        {
            isPlayer = true;
            Debug.Log("Distance player - enemy: " + Vector3.Distance(_barrelPivot.position, _enemyCannon.position));

        }
        if (forceBar != null)
        {
            forceBar.setMaxForce(this.maxVelocity);//
            forceBar.setForce(0);
        }
    }
    // private List<int> lsty;
    // private float time = 0f;
    // public float interpolated = .5f;
    private void Update()
    {
        HandleControls();
        if (isPlayer)
        {
            Debug.DrawRay(_forward.position, _forward.forward * 5, Color.green);
            Debug.DrawRay(_forward.position, _barrelPivot.forward * 5, Color.red);
        }
        // if (spawned != null && isPlayer)
        // {
        //     time += Time.deltaTime;
        //     if (time >= interpolated)
        //     {
        //         time = time - interpolated;
        //         lsty.Add((int)spawned.transform.position.y);
        //     }
        // }
        // if (spawned == null && lsty.Count > 0)
        // {
        //     _graph.ShowGraph(lsty, lsty.Max());
        // }
    }
    private void FixedUpdate()
    {
        if (_topCornerCam != null)
        {
            // Vector3 displacement = _barrelPivot.
            Quaternion rotation = Quaternion.LookRotation(_barrelPivot.right);
            _topCornerCam.rotation = rotation;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.setHealth(currentHealth);
    }

    public void receivemsg(string str)
    {
        Debug.Log("Cannon received a msg: " + str);
    }

    #region Handle Controls

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _velocity = 0;
    // private int forceAcc = 0;
    // private float forceAcc = 0.0f;
    private int maxVelocity = 50; // 50m/s
    [SerializeField] private float timeMultiplier = 10;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private Transform _forward;
    [SerializeField] private float _rotateSpeed = 30;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform _leftWheel, _rightWheel;
    [SerializeField] private ParticleSystem _launchParticles;

    /// <summary>
    /// This is absolute spaghetti and should not be look upon for inspiration. I quickly smashed this together
    /// for the tutorial and didn't look back
    /// </summary>
    [SerializeField] private float _minRot;
    [SerializeField] private float _maxRot;
    private Ball spawned;
    private void HandleControls()
    {
        if (isPlayer)
        {
            // _angleSlider.setAngleVal(angle);
        }
        var rotation = _barrelPivot.rotation.eulerAngles;
        // var rotation = _barrelPivot.localRotation.eulerAngles;
        if (Input.GetKey(KeyCode.S))
        {
            // _barrelPivot.Rotate(Vector3.right * _rotateSpeed * Time.deltaTime);
            rotation.x += _rotateSpeed * Time.deltaTime;
            if (isPlayer)
            {
                // Debug.Log(rotation.x);
                rotation.x = rotation.x > 180 ? rotation.x - 360 : rotation.x;
                rotation.x = Mathf.Clamp(rotation.x, _maxRot, _minRot);
                _barrelPivot.rotation = Quaternion.Euler(rotation);
                var angle = Vector3.Angle(_forward.forward, _barrelPivot.forward);
                _angleSlider.setAngleVal(angle);
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            // _barrelPivot.Rotate(Vector3.left * _rotateSpeed * Time.deltaTime);
            rotation.x -= _rotateSpeed * Time.deltaTime;
            if (isPlayer)
            {
                // Debug.Log(rotation.x);
                rotation.x = rotation.x > 180 ? rotation.x - 360 : rotation.x;
                rotation.x = Mathf.Clamp(rotation.x, _maxRot, _minRot);
                _barrelPivot.rotation = Quaternion.Euler(rotation);
                var angle = Vector3.Angle(_forward.forward, _barrelPivot.forward);
                _angleSlider.setAngleVal(angle);
            }
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * _rotateSpeed * Time.deltaTime);
            _leftWheel.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
            _rightWheel.Rotate(Vector3.back * _rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
            _leftWheel.Rotate(Vector3.back * _rotateSpeed * 1.5f * Time.deltaTime);
            _rightWheel.Rotate(Vector3.forward * _rotateSpeed * 1.5f * Time.deltaTime);
        }

        // float tmpForceAcc = 0.0f;
        // if (Input.GetKey(KeyCode.Space))
        // {

        // _velocity += 2f * Time.deltaTime;
        // _velocity = _velocity > maxVelocity ? maxVelocity : _velocity;
        // if (forceBar != null && _velocitySlider != null)
        // {
        //     forceBar.setForce(_velocity);
        //     _velocitySlider.setVelocityVal(_velocity);
        // }
        // }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
            spawned.fromPlayer = isPlayer;
            if (isPlayer)
            {
                Debug.Log("Local forward: " + _ballSpawn.forward.ToString());
                Debug.Log("Spawned with velocity: " + (_ballSpawn.forward * _velocity).ToString());
            }
            spawned.Init(_ballSpawn.forward * _velocitySlider.GetValue(), false);
            // _ballPrefab.lst;
            // spawned.lst
            // Debug.Log("Force Acc : " + forceAcc);
            _launchParticles.Play();
            _source.PlayOneShot(_clip);
            _velocity = 0;
        }
    }

    #endregion
}