using UnityEngine;

public class EvieStateMachine : StateMachine
{
    [field: Header("References")]
    [field: SerializeField] public InputHandler InputHandler { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Transform MyCamera { get; private set; }


    [field: Header("General Config")]
    [field: SerializeField] public float CameraSens { get; private set; }
    [field: SerializeField] public GameObject EvieStaff { get; private set; }


    [field: Header("Movement Variables")]
    [field: SerializeField] public float StandardMovementSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }

    [field: Header("Rotation values")]
    public float XRotation { get; set; }
    public float YRotation { get; set; }

    [field: Header("M1 Config")]
    [field: SerializeField] public GameObject M1Projectile { get; private set; }
    [field: SerializeField] public Transform M1ProjectileSpawnPoint { get; private set; }
    [SerializeField] private float m1Cooldown;
    float m1CurrentCooldown;

    [field: Header("M2 Config")]
    [field: SerializeField] public GameObject IceCube { get; private set; }
    [field: SerializeField] public GameObject IceCubeCamera { get; private set; }
    [field: SerializeField] public float CubeFormDuration { get; private set; }
    [SerializeField] float m2Cooldown;
    float m2CurrentCooldown;

    [field: Header("Blink config")]
    [field: SerializeField] public GameObject BlinkPlaceholder { get; private set; }
    [field: SerializeField] public float QSkillDistance { get; private set; }

    [SerializeField] float qSkillCooldown;
    float qSkillCurrentCooldown;

    [field: Header("Flying config")]
    [field: SerializeField] public GameObject FlyingCamera { get; private set; }
    [field: SerializeField] public float FlyingSpeed { get; private set; }
    [field: SerializeField] public float FlyingDuration { get; private set; }
    [SerializeField] float fSkillCooldown;
    float fSkillCurrentCooldown;

    private void Start()
    {
        XRotation = transform.eulerAngles.x;
        YRotation = transform.eulerAngles.y;

        MyCamera = Camera.main.transform;

        m1CurrentCooldown = m1Cooldown;
        m2CurrentCooldown = m2Cooldown;
        qSkillCurrentCooldown = qSkillCooldown;
        fSkillCurrentCooldown = fSkillCooldown;

        SwitchState(new EvieStandardState(this));
    }

    private void Update()
    {
        base.Update();

        // Cooldowns
        M1CooldownControl();
        M2CooldownControl();
        QCooldownControl();
        FSkillCooldownControl();
    }

    public bool CanUseM1Skill()
    {
        return m1CurrentCooldown <= 0;
    }

    public bool CanUseM2Skill()
    {
        return m2CurrentCooldown <= 0;
    }

    public bool CanUseQSkill()
    {
        return qSkillCurrentCooldown <= 0;
    }

    public bool CanUseFSkill()
    {
        return fSkillCurrentCooldown <= 0;
    }

    public void M2Skill()
    {
        if (!CanUseM2Skill()) return;
        SwitchState(new EvieM2SkillState(this));
    }

    public void UltimateSkill()
    {
    }

    public void QSkill()
    {
        if (!CanUseQSkill()) return;
        SwitchState(new EvieQSkillState(this));
    }

    public void FSkill()
    {
        if (!CanUseFSkill()) return;
        SwitchState(new EvieFSkillState(this));
    }

    void M1CooldownControl()
    {
        if (m1CurrentCooldown > 0)
        {
            m1CurrentCooldown -= Time.deltaTime;
        }
    }

    void M2CooldownControl()
    {
        if (m2CurrentCooldown > 0)
        {
            m2CurrentCooldown -= Time.deltaTime;
        }
    }

    void QCooldownControl()
    {
        if (qSkillCurrentCooldown > 0)
        {
            qSkillCurrentCooldown -= Time.deltaTime;
        }
    }

    void FSkillCooldownControl()
    {
        if (fSkillCurrentCooldown > 0)
        {
            fSkillCurrentCooldown -= Time.deltaTime;
        }
    }

    public void RestartM1Cooldown()
    {
        m1CurrentCooldown = m1Cooldown;
    }

    public void RestartQSkillCooldown()
    {
        qSkillCurrentCooldown = qSkillCooldown;
    }
    
    public void RestartFSkillCooldown()
    {
        fSkillCurrentCooldown = fSkillCooldown;
    }

    public void ToggleStaff(bool _value)
    {
        EvieStaff.SetActive(_value);
    }
}