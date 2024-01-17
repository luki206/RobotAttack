using UnityEditor;
using UnityEngine;

public class GunController : MonoBehaviour
{

    //Gun Stats
    [HideInInspector]public int damage;
    [HideInInspector]public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    [HideInInspector]public int magazineSize, bulletsPerTap;
    [HideInInspector]public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //Camera settings
    [HideInInspector] public bool fpsController = false;
    [HideInInspector] public Transform _camera;
    [HideInInspector]public bool thirdPersonController = false;
    [HideInInspector] public Transform shootingPoint;

    //Bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask whatIsEnemy;
    [Space]
    [Header("Guns")]
    bool machineGun;
    [HideInInspector]public bool shotGun;
    [HideInInspector]public bool burstGun;
    [HideInInspector]public bool customGun;
    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
            bulletsShot = bulletsPerTap;
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        
        if(fpsController)
        {
            Vector3 direction = _camera.transform.forward + new Vector3(x, y, 0);

            if(Physics.Raycast(_camera.transform.position, direction, out hit, range, whatIsEnemy))
            {
                if(hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<HealthManager>().TakeDamage(damage);
                }
            }
        }

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished",reloadTime);   
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

[CustomEditor(typeof(GunController))]
public class GunControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Camera Settings
        var script = (GunController)target;
        if(script.fpsController == false && script.thirdPersonController == false)
        {
            script.fpsController = EditorGUILayout.Toggle("FPS Controller", script.fpsController);
            script.thirdPersonController = EditorGUILayout.Toggle("Third Person Controller", script.thirdPersonController);
        }
        
        if(script.fpsController && script.thirdPersonController == false)
        {
            script.fpsController = EditorGUILayout.Toggle("FPS Controller",script.fpsController);
            script._camera = EditorGUILayout.ObjectField("Camera", script._camera, typeof(Transform), true) as Transform;
        }

        if(script.fpsController == false && script.thirdPersonController)
        {
            script.thirdPersonController = EditorGUILayout.Toggle("Third Person Controller", script.thirdPersonController);
            script.shootingPoint = EditorGUILayout.ObjectField("Shooting Point", script.shootingPoint, typeof(Transform), true) as Transform;
        }

        //Gun's settings
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Guns");
        //script.machineGun = EditorGUILayout.Toggle("Machine gun", script.machineGun);
        script.shotGun = EditorGUILayout.Toggle("Shotgun", script.shotGun);
        script.burstGun = EditorGUILayout.Toggle("Burst gun", script.burstGun);
        script.customGun = EditorGUILayout.Toggle("Custom gun", script.customGun);

    }
}
