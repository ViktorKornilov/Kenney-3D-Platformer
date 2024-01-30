using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public async void ResetScene()
    {
        var scene = SceneManager.GetActiveScene();

        await Transition.FadeIn();
        await SceneManager.LoadSceneAsync(scene.name);
        await Transition.FadeOut();

    }
}