using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum Difficulty
    {
        None,
        Poor,
        Rich,
    }

    public enum WeaponType
    {
        Unarmed = 0,
        Knife = 1,
        Bat = 2,
        Axe = 3,
        None,
    }

    public enum ItemType
    {
        Weapon,
        Used,
        ETC,
    }

    public enum ItemPart
    {
        None,
        Hunger,
        Thirst,
    }

    public enum Layer
    {
        Player = 6,
        Floor = 7,
        Wall = 8,
        Enemy = 9,
        ItemSpawner = 10,
    }

    public enum State
    {
        Die,
        Idle,
        Moving,
        Attack,
    }

    public enum KeyAction
    {
        E,
        M,
        I,
        Escape,
    }

    public enum MouseAction
    {
        LeftClick,
        RightClick,
        LeftPress,
        RightPress,
        LeftButtonUp,
        RightButtonUp
    }

    public enum CameraMode
    {
        Default,
        House,
    }
}
