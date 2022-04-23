using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events {
    // Mobs


    // Player Events
    public static readonly EventNative OnSpawn = new EventNative(); // TODO
    public static readonly EventNative OnDeath = new EventNative(); // TODO
    public static readonly EventNative<int, int> OnHealthUpdate = new EventNative<int, int>();

    public static readonly EventNative OnCatchManaCrystal = new EventNative();
    public static readonly EventNative OnCatchHealthCrystal = new EventNative();

    // Input Events
    public static readonly EventNative OnInteract = new EventNative();
    public static readonly EventNative OnUseHealthCrystal = new EventNative();
    public static readonly EventNative OnUseManaCrystal = new EventNative();
    public static readonly EventNative OnJump = new EventNative();
    public static readonly EventNative OnAttack = new EventNative();
    public static readonly EventNative<Vector2> OnLook = new EventNative<Vector2>();
    public static readonly EventNative<Vector2> OnMove = new EventNative<Vector2>();
}