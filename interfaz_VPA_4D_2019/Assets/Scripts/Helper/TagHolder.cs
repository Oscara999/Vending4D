using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationTags
{
    public const string ZOOM_IN_ANIM = "ZoomIn";
    public const string ZOOM_OUT_ANIM = "ZoomOut";

    public const string SHOOT_TRIGGER = "Shoot";
    public const string AIM_PARAMETER = "Aim";
    public const string RELOAD_TRIGGER = "Reload";

    public const string WALK_PARAMETER = "Walk";
    public const string RUN_PARAMETER = "Run";
    public const string DAMAGE_PARAMETER = "Damage";
    public const string ATTACK_TRIGGER = "Attack";
    public const string DEAD_TRIGGER = "Dead";
}

public class Tags
{
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string MAINPANEL_TAG = "MainPanel";
    public const string VOLUMENMASTER_TAG= "MasterVolumen";
    public const string ITEM_TAG = "Item";
    public const string LOOK_ROOT = "Look Root";
    public const string ZOOM_CAMERA = "FP Camera";
    public const string CROSSHAIR = "Crosshair";
    public const string ARROW_TAG = "Arrow";
    public const string BARREL_TAG = "BarrelExploted";
    public const string AXE_TAG = "Axe";
    public const string SELECTABLE_TAG = "Selectable";
    public const string SELECTABLEATTACK_TAG = "SelectableAttack";
}

public enum EnemyState
{
    PATROL,
    MOVE,
    GROUND,
    DAMAGE,
    ATTACK,
    LEVELUP,
    QTE,
    DEAD
}

public enum MusicLevel
{
    MAINMENU,
    GAME,
    TEST
}

public enum MusicType
{
    BACKGROUND,
    SFX,
    VOICE
}

































