using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DioBehavior
{
    public static _app _AppMaster;
    public static UIMaster _UIMaster;
    public static Database _DatabaseMaster;
    //still not sure what this default event system does or how to access
    //public static EventSystem _EventSystemMaster;
    public static SoundMaster _SoundMaster;

    public static Game _GameMaster;
    public static GameEventMaster _GameEventMaster;
    public static Galaxy _Galaxy;

    public static void setAppMaster(Transform app)
    {
        _AppMaster = app.gameObject.GetComponent<_app>();
    }

    public static void setUIMaster(Transform uim)
    {
        _UIMaster = uim.gameObject.GetComponent<UIMaster>();
    }

    public static void setDBMaster(Transform dbm)
    {
        _DatabaseMaster = dbm.gameObject.GetComponent<Database>();
    }

    public static void setSoundMaster(Transform sm)
    {
        _SoundMaster = sm.gameObject.GetComponent<SoundMaster>();
    }

    public static void setGameMaster(Transform gm)
    {
        _GameMaster = gm.gameObject.GetComponent<Game>();
    }

    public static void setGameEventMaster(Transform gem)
    {
        _GameEventMaster = gem.GetComponent<GameEventMaster>();
    }

    public static void setGalaxyMaster(Transform gal)
    {
        _Galaxy = gal.gameObject.GetComponent<Galaxy>();
    }
}
