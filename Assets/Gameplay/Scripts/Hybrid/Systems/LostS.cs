using Unity.Entities;
using UnityEngine.SceneManagement;

class LostS : ComponentSystem
{
    float time;

    protected override void OnUpdate()
    {
        Entities.ForEach((LostC lost) =>
        {
            if (lost.hp.anchorMax.x <= 0 || lost.thirst.anchorMax.x <= 0)
            {
                lost.loserView.SetActive(true);
                time += Time.DeltaTime;

                if (time > 2)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }
        });
    }
}
