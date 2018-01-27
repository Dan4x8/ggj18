using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Emitter : MonoBehaviour {

    private float _range;

    public EmitterState State = EmitterState.Inactive;
	
	void Start () {
        _range = GetComponent<AudioSource>().maxDistance;
	}

    public void ChangeState(EmitterState s)
    {
        State = s;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().RegisterEmitter(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().UnregisterEmitter(this);
    }
}

public enum EmitterState { Inactive = 0, Push = -1, Pull = 1 };

