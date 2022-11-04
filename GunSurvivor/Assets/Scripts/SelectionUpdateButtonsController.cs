using UnityEngine;

public class SelectionUpdateButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    private CharacterController _characterController;

    private SelectionUpdate _selectionUpdate;

    private GameObject _gun;
    private GameObject _firePoint;

    [SerializeField] GameObject _machineGun;
    [SerializeField] GameObject _sniperGun;
    [SerializeField] GameObject _bullet_Fire;
    [SerializeField] GameObject _bullet_Ice;
    [SerializeField] GameObject _bullet_Blast;

    private void Start()
    {
        _gun = _player.transform.Find("Gun").gameObject;
        _firePoint = _player.transform.Find("FirePoint").gameObject;

        _selectionUpdate = GetComponent<SelectionUpdate>();

        _characterController = _player.GetComponent<CharacterController>();
    }

    public void ChangePlayerToDoubleGun()
    {
        _gun.transform.localPosition += new Vector3(-1.2f, 0);
        _firePoint.transform.localPosition += new Vector3(-1.2f, 0);

        GameObject newGun = Instantiate(_gun, _player.transform);
        GameObject newFirePoint = Instantiate(_firePoint, _player.transform);

        newGun.transform.localPosition = new Vector3(-_gun.transform.localPosition.x, _gun.transform.localPosition.y, _gun.transform.localPosition.z);
        newFirePoint.transform.localPosition = new Vector3(-_firePoint.transform.localPosition.x, _firePoint.transform.localPosition.y, _firePoint.transform.localPosition.z);

        _player.GetComponent<PlayerController>().PlayerType = "DoubleGun";
        _player.GetComponent<CharacterController>().FirePoint2 = newFirePoint.transform;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void ChangePlayerToMachineGun()
    {
        Destroy(_gun);
        GameObject machineGun = Instantiate(_machineGun, _player.transform);

        _player.GetComponent<PlayerController>().PlayerType = "MachineGun";
        _characterController.ShootCooldown *= 0.25f;
        _characterController.PlayerDamage *= 0.5f;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void ChangePlayerToSniperGun()
    {
        Destroy(_gun);
        Instantiate(_sniperGun, _player.transform);

        _player.GetComponent<PlayerController>().PlayerType = "SniperGun";
        _characterController.ShootCooldown *= 5f;
        _characterController.PlayerDamage *= 2.5f;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void ChangeBulletToFire()
    {
        _characterController.Bullet = _bullet_Fire;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void ChangeBulletToIce()
    {
        _characterController.Bullet = _bullet_Ice;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void ChangeBulletToBlast()
    {
        _characterController.Bullet = _bullet_Blast;
        _characterController.ShootCooldown *= 3f;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void UpgradePlayerDamage()
    {
        _characterController.PlayerDamage *= 1.5f;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void UpgradePlayerSpeed()
    {
        if (_characterController.Speed * 2 < _characterController.MaxSpeed)
        {
            _characterController.Speed *= 2;
            _selectionUpdate.DeactivateUpdateField();
        }
        else if (_characterController.Speed < _characterController.MaxSpeed)
        {
            _characterController.Speed = _characterController.MaxSpeed;
            _selectionUpdate.DeactivateUpdateField();
        }
    }

    public void UpgradePlayerBulletSpeed()
    {
        if (_characterController.BulletSpeed * 1.5f < _characterController.MaxBulletSpeed)
        {
            _characterController.BulletSpeed *= 1.5f;
            _selectionUpdate.DeactivateUpdateField();
        }
        else if (_characterController.BulletSpeed < _characterController.MaxBulletSpeed)
        {
            _characterController.BulletSpeed = _characterController.MaxBulletSpeed;
            _selectionUpdate.DeactivateUpdateField();
        }
    }

    public void UpgradePlayerShootCooldown()
    {
        _characterController.ShootCooldown /= 1.5f;

        _selectionUpdate.DeactivateUpdateField();
    }

    public void UpgradePlayerCollectDistance()
    {
        _characterController.CollectDistance *= 1.5f;

        _selectionUpdate.DeactivateUpdateField();
    }
}
