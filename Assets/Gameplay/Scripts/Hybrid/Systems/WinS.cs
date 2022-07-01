using Unity.Entities;
using UnityEngine.SceneManagement;

public class WinS : ComponentSystem
{
    float time;

    protected override void OnUpdate()
    {
        Entities.ForEach((OrAliveC orAlive, WinC win) =>
        {
            if (!orAlive.value)
            {
                win.value.SetActive(true);
                time += Time.DeltaTime;

                if (time > 2)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
        });
    }
}
