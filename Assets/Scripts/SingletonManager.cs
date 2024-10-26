using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance { get; private set; }

    // Mantén una referencia a cada objeto que necesita ser un singleton
    public UnlockExtraDoubleJump unlockExtraDoubleJump;

    private void Awake()
    {
        // Verificar si ya existe una instancia
        if (Instance != null && Instance != this)
        {
            // Si ya hay una instancia existente y no es esta, destruir este objeto
            Destroy(gameObject);
            return;
        }

        // Establecer esta instancia como la instancia única
        Instance = this;
    }
}
