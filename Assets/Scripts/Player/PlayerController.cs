using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnHitByChair;

    public GameObject defaultSprite;
    public GameObject tieSprite;
    public GameObject socksSprite;
    public GameObject tieAndSocksSprite;

    public Animator defaultAnimator;
    public Animator tieAnimator;
    public Animator socksAnimator;
    public Animator tieAndSocksAnimator;

    [HideInInspector]
    public Animator currentAnimator;

    public float skillReactivationTime = 2.0f;
    public float collisionImpactThreshold = 10.0f;

    private SpriteRenderer _currentSR;
    private DataTracker _dataTracker;
    private PlayerState _currentState;
    private PlayerInput _playerInput;
    private PlayerSneeze _playerSneeze;
    private PlayerWave _playerWave;
    private PlayerDash _playerDash;

    // Properties to track collected items
    public bool HasTie { get; private set; }
    public bool HasSocks { get; private set; }
    public bool HasResume { get; private set; }

    public void CollectTie()
    {
        HasTie = true;
        InitStateChange();

        // Update DataTracker
        _dataTracker.HasTie = true;
    }

    public void CollectSocks()
    {
        HasSocks = true;
        InitStateChange();

        // Update DataTracker
        _dataTracker.HasSocks = true;
    }

    public void CollectResume()
    {
        HasResume = true;

        // Update DataTracker
        _dataTracker.HasResume = true;
    }

    public void SetAppearance(GameObject sprite, Animator animator)
    {
        // Activate the desired sprite
        sprite.SetActive(true);

        //Set the current, active SpriteRenderer
        _currentSR = sprite.GetComponent<SpriteRenderer>();

        //Set the current, active Animator
        currentAnimator = animator;
    }

    public void SwitchToPlayerActionMap()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void SwitchToUIActionMap()
    {
        _playerInput.SwitchCurrentActionMap("UI");
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerSneeze = GetComponent<PlayerSneeze>();
        _playerWave = GetComponent<PlayerWave>();
        _playerDash = GetComponent<PlayerDash>();
    }

    private void Start()
    {
        _dataTracker = DataTracker.GetInstance();

        // Set initial PlayerState to default
        ChangeState(new PlayerDefaultState(this));

        // Initialize player inventory
        HasTie = _dataTracker.HasTie;
        HasSocks = _dataTracker.HasSocks;
        HasResume = _dataTracker.HasResume;

        // Reset initial PlayerState
        InitStateChange();
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OfficeChair"))
        {
            // Get the relative velocity magnitude
            float impactForce = collision.relativeVelocity.magnitude;

            // Check if the impact force exceeds the threshold
            if (impactForce >= collisionImpactThreshold)
            {
                //Change color of player sprite
                _currentSR.color = Color.red;

                OnHitByChair?.Invoke();

                // Temporarily disable special skills
                StartCoroutine(TemporarilyDisableSpecialSkills(skillReactivationTime));
            }
        }
    }

    private void InitStateChange()
    {
        if (HasTie && !HasSocks)
        {
            ChangeState(new PlayerTieState(this));
        }
        else if (HasSocks && !HasTie)
        {
            ChangeState(new PlayerSocksState(this));
        }
        else if (HasTie && HasSocks)
        {
            ChangeState(new PlayerTieAndSocksState(this));
        }
        else
        {
            ChangeState(new PlayerDefaultState(this));
        }
    }

    private void ChangeState(PlayerState newState)
    {
        _currentState?.OnStateExit();
        _currentState = newState;
        _currentState.OnStateEnter();
    }

    private IEnumerator TemporarilyDisableSpecialSkills(float timeToDisable)
    {
        // Deactivate special actions that are available in this level
        _playerSneeze.DeactivateSneeze();
        _playerWave.DeactivateWave();
        _playerDash.DeactivateDash();

        yield return new WaitForSeconds(timeToDisable);

        // Activate special actions that are available in this level
        _playerSneeze.ActivateSneeze();
        _playerWave.ActivateWave();
        _playerDash.ActivateDash();

        //Change player sprite color back to the original
        _currentSR.color = Color.white;
    }
}
