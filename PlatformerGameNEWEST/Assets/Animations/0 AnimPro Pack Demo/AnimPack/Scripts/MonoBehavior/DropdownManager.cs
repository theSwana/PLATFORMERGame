using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DropdownManager : MonoBehaviour {

    [Space()]
    public ImportedAnimations animationData;

    [Space()]
    public Dropdown dropdown;
    public Animator animator;
    public AnimationClip idle;

    [Space()]
    public Text animName;
    public Text animLenght;

    public string animNameText = "Animation Name:";
    public string animLengthText = "Animation Length:";

    [Space()]
    public GameObject[] copAttach;
    public string copPrefix = "CP-";
    public GameObject[] brawAttach;
    public string brawPrefix = "BR-";

    private AnimationClip[] m_animations;
    private AnimatorOverrideController animatorOverride;
    private int currentAnimation;

    private const string animDefault = "anim_default";
    private const string stateDefault = "State1";

    private int animLoop = Animator.StringToHash("Loop");

    // Use this for initialization
    void Start ()
    {

        if (dropdown == null)
        {
            Debug.LogWarning("SET DROPDOWN");

            return;
        }
        
        dropdown.options.Clear();

        animatorOverride = new AnimatorOverrideController();
        animatorOverride.runtimeAnimatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = animatorOverride;

        m_animations = animationData.GetAnimations;

        if(idle)
        {
            ChangeAnimation(animDefault, idle);
            
        }else
        {
            idle = m_animations[0];
            ChangeAnimation(animDefault, idle);
        }
        
        for (int i = 0; i < m_animations.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData(m_animations[i].name));
        }

        for (int j = 0; j < dropdown.options.Count; j++)
        {
            if (dropdown.options[j].text.Equals(idle.name))
            {
                dropdown.value = j;
            }
        }
	}


    void ResetTransform()
    {
        animator.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("right"))
        {
            if (currentAnimation >= m_animations.Length - 1)
            {
                currentAnimation = 0;
            }
            else if (currentAnimation < m_animations.Length - 1)
            {
                currentAnimation++;
            }

            if (currentAnimation < 0 || currentAnimation > m_animations.Length)
            {
                currentAnimation = 0;
            }

            ChangeAnimation(currentAnimation);
            dropdown.value = currentAnimation;
            //Debug.Log(currentAnimation);

        }
        else if (CrossPlatformInputManager.GetButtonDown("left"))
        {
            if (currentAnimation <= 0)
            {
                currentAnimation = m_animations.Length - 1;

            }else if (currentAnimation >= 1)
            {
                currentAnimation--;
            }

            if (currentAnimation < 0 || currentAnimation > m_animations.Length)
            {
                currentAnimation = m_animations.Length;
            }

            ChangeAnimation(currentAnimation);
            dropdown.value = currentAnimation;
            //Debug.Log(currentAnimation);
        }

    }

    public void PlayCurrentAnimation()
    {
        animator.Play(stateDefault, 0,0);
        ResetTransform();
        animator.enabled = true;
    }

    public void StopCurrentAnimation()
    {
        animator.enabled = false;
    }

    public void ChangeAnimation(int index)
    {

        currentAnimation = index;

        animatorOverride[animDefault] = m_animations[index];

        CheckAnimation(m_animations[index].name);

        animator.Play(stateDefault, 0, 0);
        animator.enabled = true;
        ResetTransform();

        if (animLenght && animName != null)
        {
            animName.text = animNameText + " " + m_animations[index].name;
            animLenght.text = animLengthText + " " + m_animations[index].length + " seconds";
        }

    }
    public void ChangeAnimation(string anim, int index)
    {
        currentAnimation = index;
        CheckAnimation(m_animations[index].name);
        animatorOverride[anim] = m_animations[index];
        animator.Play(stateDefault, 0, 0);
        animator.enabled = true;
        animator.Rebind();
        ResetTransform();
    }
    public void ChangeAnimation(string anim, AnimationClip clip)
    {
        animatorOverride[anim] = clip;
        CheckAnimation(clip.name);
        animator.Play(stateDefault, 0, 0);
        animator.enabled = true;
        ResetTransform();
        if (animLenght && animName != null)
        {
            animName.text = animNameText + " " + clip.name;
            animLenght.text = animLengthText + " " + clip.length + " seconds";
        }
        currentAnimation = 0;
    }

    void CheckAnimation(string animName)
    {
        if (animName.Contains(copPrefix))
        {
            for (int i = 0; i < copAttach.Length; i++)
            {
                copAttach[i].SetActive(true);
            }
            for (int i = 0; i < brawAttach.Length; i++)
            {
                brawAttach[i].SetActive(false);
            }
        }
        else if (animName.Contains(brawPrefix))
        {
            for (int i = 0; i < copAttach.Length; i++)
            {
                copAttach[i].SetActive(false);
            }
            for (int i = 0; i < brawAttach.Length; i++)
            {
                brawAttach[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < copAttach.Length; i++)
            {
                if (copAttach[i].activeSelf)
                    copAttach[i].SetActive(false);
            }
            for (int i = 0; i < brawAttach.Length; i++)
            {
                if (brawAttach[i].activeSelf)
                    brawAttach[i].SetActive(false);
            }
        }

    }
}
