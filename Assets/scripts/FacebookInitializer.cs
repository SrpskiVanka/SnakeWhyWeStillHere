using System;
using Facebook.Unity;
using UnityEngine;

public class FacebookInitializer : MonoBehaviour
{
    private void Start()
    {
        FB.Init(OnInit);
    }

    private void OnInit()
    {
        FB.ActivateApp();
    }
}