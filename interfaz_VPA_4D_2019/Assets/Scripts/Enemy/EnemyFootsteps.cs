using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    [SerializeField]
    Sound[] footstepAir_Sound;
    [SerializeField]
    Sound[] footstepGround_Sound;
    [SerializeField]
    Sound[] footstepProvisional_Sound;
    [SerializeField]
    bool state;
    [SerializeField]
    int index;
    float accumulated_Distance;
    public float stepDistance;

    public IEnumerator ChangeSound(bool isGround, float time)
    {
        index = 0;
        accumulated_Distance = 0;
        state = true;

        if (isGround)
        {
            footstepProvisional_Sound = footstepGround_Sound;
        }
        else
        {
            footstepProvisional_Sound = footstepAir_Sound;
        }

        yield return new WaitForSeconds(time);

        state = false;
    }

    public void CheckToPlayFootstepSound()
    {
        if (state)
            return;

        accumulated_Distance += Time.deltaTime;

        if(accumulated_Distance > stepDistance)
        { 
            SoundManager.Instance.PlayNewSound(footstepProvisional_Sound[index].name);
            index = (index + 1) % footstepProvisional_Sound.Length;
            accumulated_Distance = 0f;
        }
    }
} 


































