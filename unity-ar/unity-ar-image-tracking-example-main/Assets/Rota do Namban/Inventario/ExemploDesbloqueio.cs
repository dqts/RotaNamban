using UnityEngine;

public class ExemploDesbloqueio : MonoBehaviour
{
    public InventarioManager inventarioManager;

    public void DesbloquearCasaco()
    {
        inventarioManager.DesbloquearItem("Casaco");
    }
}

