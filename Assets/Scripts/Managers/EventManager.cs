using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static LevelEvents LevelEvents = new();
    public static GameEvents GameEvents = new ();
    public static UIEvents UIEvents = new();
    
}

public class LevelEvents
{
    public Action CheckPointEvent;
    public Action CollectableCounterEvent;
}
public class GameEvents
{
    public Action StartGameEvent;
    public Action PassedEvent;
    public Action SuccessEvent;
    public Action FailEvent;
}

public class UIEvents
{
    public Action StartButtonEvent;
    public Action SuccessButtonEvent;
    public Action FailButtonEvent;
}
