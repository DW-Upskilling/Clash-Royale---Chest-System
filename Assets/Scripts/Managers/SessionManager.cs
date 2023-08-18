using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    GameManager gameManager;

    protected override void Initialize()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager instance not found!");        
    }
}
