using UnityEngine;
using System.Collections;

public class PlayerController : Controller
{
    private PlayerHUDController playerHUDController;

    [Header("Stamina")]
    public float currentStamina;
    public static readonly float maxStamina = 100;
    private bool isUseStamina;
    public float staminaHealTime = 0;
    public float staminaHealValue = 0;

    [Header("Move")]
    public float rotSpeed;
    public float sprintSpeed;
    public float jumpPower;
    public float slidePower;

    [Header("Weapon")]
    public Transform gunPos;

    protected override void Awake()
    {
        base.Awake();
        playerHUDController = GetComponent<PlayerHUDController>();
    }

    protected override void Start()
    {
        base.Start();
        currentStamina = maxStamina;
        sprintSpeed = moveSpeed * 2;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveMent();
        Rotate();
    }

    protected override void Update()
    {
        base.Update();

        UpdateStamina();
        UpdateInput();
        UpdateHUD();
    }

    #region Stamina
    private void UpdateStamina()
    {
        if (currentStamina >= maxStamina) return;

        if (isUseStamina)
        {
            if (staminaHealTime >= 1)
            {
                isUseStamina = false;
                staminaHealTime = 0;
            }
            else
            {
                staminaHealTime += Time.deltaTime;
            }
        }
        else
        {
            currentStamina += Time.deltaTime * staminaHealValue;
        }
    }

    private void RemoveStamina(float value)
    {
        if (currentStamina - value <= 0)
        {
            //대충 UI 코드
            return;
        }

        currentStamina -= value;
    }
    #endregion

    private void UpdateInput()
    {
        Interect();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentStamina > 10)
        {
            isUseStamina = true;
            Jump();
            RemoveStamina(10);
        }

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 5)
        {
            isUseStamina = true;
            Sprint();
            RemoveStamina(5 * Time.deltaTime);
        }
        else
        {
            currentMoveSpeed = moveSpeed;
            gunPos.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && currentStamina > 30)
        {
            isUseStamina = true;
            Slide();
            RemoveStamina(30);
        }

        currentWeapon.currentAttackDelay += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void Interect()
    {
        Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 3, 1 << 10 | 1 << 11))
        {
            Debug.Log("Detect");
            if (hitInfo.collider.TryGetComponent(out InterectionObject interactionObject))
            {
                playerHUDController.InterectionHUD(true, interactionObject.infoName, interactionObject.transform.position + new Vector3(0, 0.5f, 0));
            }
        }
        else
        {
            Debug.Log("NotDetect");
            playerHUDController.InterectionHUD(false);
        }
    }

    private void UpdateHUD()
    {
        playerHUDController.UpdateHpHUD(health.maxHp, health.currentHp);
        playerHUDController.UpdateStaminaHealHUD(1, staminaHealTime);
        playerHUDController.UpdateStaminaHUD(maxStamina, currentStamina);

        int bulletCount = currentWeapon.GetCurrentBullet();
        int bulletMaxCount = currentWeapon.GetMaxBullet();
        int magazineCount = weaponMagazineInventory.GetMagazineCount(currentWeapon.GetWeaponType());
        int magazineMaxCount = weaponMagazineInventory.GetMagazineMaxCount(currentWeapon.GetWeaponType());

        playerHUDController.UpdateWeaponHUD(bulletCount, bulletMaxCount, magazineCount, magazineMaxCount);
    }

    #region MoveMent
    private void MoveMent()
    {
        Vector3 moveVec = new(InputManager.GetInputHorizontal(), 0, InputManager.GetInputVertical());
        moveVec.Normalize();

        transform.Translate(currentMoveSpeed * Time.deltaTime * moveVec);
    }

    private void Sprint()
    {
        currentMoveSpeed = sprintSpeed;
        CameraMoveMent.Instance.SprintCamera(70);
        gunPos.rotation = Quaternion.Euler(-65, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void Slide()
    {
        Vector3 moveVec = transform.forward * slidePower;

        rigid.AddForce(moveVec, ForceMode.Impulse);

        CameraMoveMent.Instance.SlideCamera();
    }

    float rotY = 0;
    private void Rotate()
    {
        rotY += InputManager.GetInputMouse().x * rotSpeed;
        transform.eulerAngles = new Vector3(0, rotY);
    }
    #endregion

    protected override void Attack()
    {
        if (IsCanAttack())
        {
            if (currentWeapon.IsBulletZero())
            {
                Reload();
                return;
            }
            currentWeapon.Shoot();
            currentWeapon.currentAttackDelay = 0;
        }
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
    }
}