using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    [Range(0.5f, 50.0f)]
    public float CharacterMaxSpeed;

    public Transform player;
    public Transform body;
    public Transform head;    
    public Camera mainCamera;
    public Animator animator;

    private Vector2 vMove, vRight;

	// Use this for initialization
	void Start () {
	    
	}

    void LookToMouse()
    {
        Vector3 dir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - head.position;
        dir.z = 0;
        dir.Normalize();
        Vector3 headDir = head.up;
        Vector3 bodyDir = body.up;
        if (Vector3.Dot(dir, bodyDir) > 0.0f)
            head.up = Vector3.Lerp(headDir, dir, 10 * Time.deltaTime);
    }
    
	
	// Update is called once per frame
	void Update () 
    {
        LookToMouse();
        vMove.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        vMove.Normalize();

        animator.SetFloat("CharacterSpeed", vMove.sqrMagnitude);   

        if (vMove.magnitude > 0.5f)
        {
            vRight.Set(body.up.x, body.up.y);
            body.up = Vector2.Lerp(vRight, vMove, 10 * Time.deltaTime);
        }

        vMove *= CharacterMaxSpeed * Time.deltaTime;

        player.position += new Vector3(vMove.x, vMove.y, 0.0f);                        
	}
}
