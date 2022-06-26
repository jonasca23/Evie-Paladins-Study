using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evie : PlayableCharacter
{
    [field: SerializeField] public InputHandler InputHandler { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    Transform myCamera;

    [Header("M1 Config")]
    [SerializeField] GameObject m1Projectile;
    [SerializeField] float m1Cooldown;
    float m1CurrentCooldown;
    bool m1usable = true;
    [SerializeField] Transform m1ProjectileSpawnPoint;

    [Header("Blink config")]
    [SerializeField] GameObject blinkPlaceholder;
    [SerializeField] float qSkillDistance;
    [SerializeField] float qSkillCooldown;
    float qSkillCurrentCooldown;
    bool qSkillUsable = true;
    bool isPlacingBlink;

    [Header("Flying config")]
    [SerializeField] GameObject flyingCamera;
    [SerializeField] float flyingSpeed;
    [SerializeField] float flyingDuration;
    [SerializeField] float fSkillCooldown;
    float fSkillCurrentCooldown;
    bool isFlying = false;
    bool fSkillUsable = true;

    private void OnEnable()
    {
        InputHandler.M1PressedEvent += M1Skill;

        InputHandler.M2PressedEvent += CancelBlink;

        InputHandler.QPressedEvent += QSkill;
        InputHandler.QReleasedEvent += Blink;

        InputHandler.FPressedEvent += FSkill;
    }

    private void OnDisable()
    {
        InputHandler.M1PressedEvent -= M1Skill;

        InputHandler.M2PressedEvent -= CancelBlink;

        InputHandler.QPressedEvent -= QSkill;
        InputHandler.QReleasedEvent -= Blink;

        InputHandler.FPressedEvent -= FSkill;
    }

    private void Start()
    {
        myCamera = Camera.main.transform;

        m1CurrentCooldown = m1Cooldown;
        qSkillCurrentCooldown = qSkillCooldown;
        fSkillCurrentCooldown = fSkillCooldown;
    }

    private void Update()
    {
        AimBlink();

        // Cooldowns
        M1CooldownControl();
        QCooldownControl();
        FSkillCooldownControl();

        if (isFlying)
        {
            ControlFlyingBroom();
        }
    }

    public override void M1Skill()
    {
        if (!m1usable) return;

        Evie_M1_Projectile proj = Instantiate(m1Projectile, m1ProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Evie_M1_Projectile>();
        proj.SetDirection(transform.forward);
        m1CurrentCooldown = m1Cooldown;
    }

    void M1CooldownControl()
    {
        if(m1CurrentCooldown >= 0)
        {
            m1CurrentCooldown -= Time.deltaTime;
            m1usable = false;
        } else
        {
            m1usable = true;
        }
    }

    public override void M2Skill()
    {
    }

    public override void QSkill()
    {
        if (!qSkillUsable) return;

        isPlacingBlink = true;
        blinkPlaceholder.SetActive(true);
        qSkillCurrentCooldown = qSkillCooldown;
    }

    void QCooldownControl()
    {
        if (qSkillCurrentCooldown >= 0)
        {
            qSkillCurrentCooldown -= Time.deltaTime;
            qSkillUsable = false;
        }
        else
        {
            qSkillUsable = true;
        }
    }

    public override void FSkill()
    {
        if (isFlying)
        {
            FinishFSKill();
            return;
        }

        if (!fSkillUsable) return;

        GetComponent<CharacterController>().enabled = false;
        flyingCamera.SetActive(true);
        ForceReceiver.ToggleGravity(false);
        isFlying = true;
    }

    void FinishFSKill()
    {
        if (!isFlying) return;

        GetComponent<CharacterController>().enabled = true;
        ForceReceiver.ToggleGravity(true);
        flyingCamera.SetActive(false);
        isFlying = false;
        fSkillCurrentCooldown = fSkillCooldown;
        flyingCamera.SetActive(false);
    }

    void ControlFlyingBroom()
    {
        if (!isFlying) return;

        transform.position = Vector3.Lerp(transform.position, (transform.position + transform.forward * flyingSpeed * Time.deltaTime), .8f);

        float x = InputHandler.MouseDelta.x * 200;
        float y = InputHandler.MouseDelta.y * 200;

        Vector3 rotationDirection = (Vector3.forward * y) + (Vector3.up * x);
        myCamera.Rotate(rotationDirection);
    }

    void FSkillCooldownControl()
    {
        if (fSkillCurrentCooldown >= 0)
        {
            fSkillCurrentCooldown -= Time.deltaTime;
            fSkillUsable = false;
        }
        else
        {
            fSkillUsable = true;
        }
    }

    public override void UltimateSkill()
    {
    }

    void AimBlink()
    {
        if (!isPlacingBlink) return;

        RaycastHit hit;
        if (Physics.Raycast(myCamera.position, myCamera.forward, out hit, qSkillDistance))
        {
            blinkPlaceholder.transform.position = hit.point;
        }
        else
        {
            blinkPlaceholder.transform.position = myCamera.position + (myCamera.forward * qSkillDistance);
        }
    }

    void Blink()
    {
        if (!isPlacingBlink) return;

        GetComponent<CharacterController>().enabled = false;
        transform.position = blinkPlaceholder.transform.position;
        blinkPlaceholder.gameObject.SetActive(false);
        qSkillUsable = false;
        isPlacingBlink = false;
        GetComponent<CharacterController>().enabled = true;
    }

    void CancelBlink()
    {
        if (isPlacingBlink)
        {
            isPlacingBlink = false;
            blinkPlaceholder.gameObject.SetActive(false);
        }
    }
}
