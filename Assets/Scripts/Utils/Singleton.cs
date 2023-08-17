using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public T Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = (T)this;
            DontDestroyOnLoad(this.gameObject);

            // Implement below method in derived calss to make use of Awake
            Initialize();
        } else
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void Initialize();
}
