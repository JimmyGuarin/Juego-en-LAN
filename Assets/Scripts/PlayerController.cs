using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public void Start()
    {
        if (isLocalPlayer)
        {
            Vector3 pos = transform.position;
            pos.y = 1;
            transform.position = pos;
            Camera.main.transform.parent = gameObject.transform;
            Camera.main.transform.localPosition = new Vector3(0, 0.6f, 0.26f);
            
        }

       
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        
        var x2= Input.GetAxis("Horizontal2") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(x2, 0, z);
        

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CmdFire();
        }
       


    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;

        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public void OnCollisionEnter(Collision collision)
    {

    }



}
