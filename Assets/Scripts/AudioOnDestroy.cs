using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio -> On Destroy")]

public class AudioOnDestroy : Audio
{
    private void OnDestroy()
    {
        Play();
    }
}
