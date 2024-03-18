using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniSpdController : MonoBehaviour
{
    public AnimatorOverrideController animatorOverrideController;
    public AnimationClip animationClipToChange;
    public float animationSpeed; // 1f is the default speed

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Jika Anda menggunakan AnimatorOverrideController, pastikan untuk mendapatkan animator dari kontroler yang digantikan.
        if (animatorOverrideController != null)
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = animatorOverrideController;
            }
            else
            {
                Debug.LogError("Animator component not found on the GameObject.");
            }
        }
    }

    void Update()
    {
        // Set speed animasi jika animator dan clip yang spesifik diberikan
        if (animator != null && animationClipToChange != null)
        {
            // Mencari animasi yang sesuai dalam daftar animasi di Animator Override Controller
            foreach (AnimationClip clip in animatorOverrideController.animationClips)
            {
                if (clip.name == animationClipToChange.name)
                {
                    animator.SetFloat(animationClipToChange.name + "_speed", animationSpeed);
                    break;
                }
            }
        }
    }
}
