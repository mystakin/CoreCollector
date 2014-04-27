using UnityEngine;
using System.Collections;

public class GatherInput : MonoBehaviour {

	public Vector3[] laserStartPosition;
	public GameObject laser;
	public float horizSpeed;
	public float forwardSpeed;
	public float horizMovement;
	public float forwardMovement;
	public float distanceLimit;
	public float laserCooldown;
	public float laserCooldownMax;
	public GameObject planet;
	public AudioClip laserSFX;
	public float laserVolume;

	GameObject player;
	PauseManager pause;

	void Awake(){
		laserVolume = StaticVariables.defaultVolume / 2;

		player = GameObject.FindGameObjectWithTag(Tags.player);
		planet = GameObject.FindGameObjectWithTag(Tags.planet);

		pause = GetComponent<PauseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		horizMovement = 0;
		forwardMovement = 0;

		if( Input.GetButtonDown( Tags.pause ) ){
			if( Enums.inputMode == Enums.InputMode.PAUSE ){
				pause.Unpause();
			}
			else if( Enums.inputMode == Enums.InputMode.GAMEPLAY ){
				pause.Pause();
			}
		}

		if( Enums.inputMode == Enums.InputMode.GAMEPLAY ){
			horizMovement = Input.GetAxisRaw( Tags.horizontal );
			forwardMovement = Input.GetAxisRaw( Tags.vertical );

			if( Input.GetButton( Tags.shoot ) && laserCooldown <= 0 )
				Shoot();
			else if ( laserCooldown > 0 )
				laserCooldown -= Time.deltaTime;
		}
		else if( Enums.inputMode == Enums.InputMode.GAMEOVER ){
			if( Input.GetButtonDown( Tags.shoot ) ){
				Application.LoadLevel( 0 );
			}
		}

		if( Input.GetKeyDown( KeyCode.Escape ) ){
			Application.Quit ();
		}
	}

	void FixedUpdate(){
		if( Enums.inputMode == Enums.InputMode.GAMEPLAY ){
			Move ( horizMovement );
			ForwardMove( forwardMovement );
		}
	}

	void Move( float moveValue ){
		player.transform.RotateAround( planet.transform.position, Vector3.forward, horizSpeed * moveValue * Time.fixedDeltaTime );
	}

	void ForwardMove( float moveValue ){
		if( moveValue > 0 )
			player.transform.position += player.transform.up * forwardSpeed * forwardMovement * Time.fixedDeltaTime;
		else{
			if( WithinVertLimits( player ) ){
				player.transform.position += player.transform.up * forwardSpeed * forwardMovement * Time.fixedDeltaTime;
			}
		}
	}

	bool WithinVertLimits( GameObject obj ){
		if( Vector2.Distance( obj.transform.position, planet.transform.position ) < distanceLimit )
			return true;
		else
			return false;
	}

	void Shoot(){
		AudioSource.PlayClipAtPoint( laserSFX, transform.position, laserVolume + StaticVariables.VolumeVariance() );
		laserCooldown = laserCooldownMax;

		GameObject laser_01 = (GameObject)Instantiate( laser, player.transform.position, player.transform.rotation );
		GameObject laser_02 = (GameObject)Instantiate( laser, player.transform.position, player.transform.rotation );

		laser_01.transform.localPosition += laserStartPosition[0].y * laser_01.transform.up;
		laser_01.transform.localPosition += laserStartPosition[0].x * laser_01.transform.right;
		laser_02.transform.localPosition += laserStartPosition[1].y * laser_01.transform.up;
		laser_02.transform.localPosition += laserStartPosition[1].x * laser_01.transform.right;

		laser_01.transform.parent = Camera.main.transform;
		laser_02.transform.parent = Camera.main.transform;
	}
}
