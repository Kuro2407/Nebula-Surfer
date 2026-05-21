using UnityEngine;

public class Teleport : PowerUp
{
    //[SerializeField] private float distancia = 3f;

    protected override void AplicarEfecto(PlayerMovement player)
    {
        player.GuardarTeleporte();
    }
}