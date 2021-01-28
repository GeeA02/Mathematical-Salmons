using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPController : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(vignette.active)
            vignette.intensity.value = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 2f) * 0.5f + 0.2f);
    }

    public void ToggleIsAlmostDead(bool isIt)
        => vignette.active = isIt;
}
