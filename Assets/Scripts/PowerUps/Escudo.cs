using UnityEngine;

public class Escudo : PowerUp
{
    protected override void AplicarEfecto(PlayerMovement player)
    {
        player.ActivarEscudo();
    }
}