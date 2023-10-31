//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using LootLocker.Requests;
//using TMPro;

//public class LoginController : MonoBehaviour
//{
//    [SerializeField] private TMP_InputField newUserInput;
//   [SerializeField]  private TMP_InputField newEmailInput;
//    [SerializeField] private TMP_InputField newPasswordInput;


//    [SerializeField] private TMP_InputField existingEmailInput;
//    [SerializeField] private TMP_InputField existingPasswordInput;


//    [Header("Reset Password")] 

//    [SerializeField] public TMP_InputField resetPassword;





//    // Start is called before the first frame update
//    void Start()
//    {
    
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }





//    //when the player presses the submit button
//    void CreateUser() {
//        LootLockerSDKManager.WhiteLabelSignUp(newEmailInput.text, newPasswordInput.text, (response) => {
        
//            if (!response.success)
//            {
//                  UnityEngine.Debug.Log("error while creating user");

//                return;
//            } 
//                LootLockerSDKManager.WhiteLabelLogin(newEmailInput.text, newPasswordInput.text, false, (response) => {
//                    if(!response.success) {
//                        return;
//                    }

//                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
//                    {
//                        if (!response.success)
//                        {
                            
//                            return;
//                        }
//                        // Set nickname to be public UID if nothing was provided
//                        if (newUserInput.text == "")
//                        {
//                            newUserInput.text = response.public_uid;
//                        }
//                        // Set new nickname for player
//                        LootLockerSDKManager.SetPlayerName(newUserInput.text, (response) =>
//                        {
//                            if (!response.success)
//                            {
//                                return;
//                            }

//                        });

                     
//                           // End this session
//                    });


           

//           UnityEngine.Debug.Log("Account Created"); 
            
//         });
                   
//        });

    

//}

//    void Login() {
//        LootLockerSDKManager.WhiteLabelLogin(existingEmailInput.text, existingPasswordInput.text, true, response =>
//        {
//            if (!response.success)
//            {
//                  UnityEngine.Debug.Log("error while logging in");
//                return;
//            }

//            string token = response.SessionToken;
            
//            // Start game session here
//            StartSession();

//            StartGame();
//        });
//    }



//    //call this function after creating or logging in user
//    void StartSession() {
//        LootLockerSDKManager.StartWhiteLabelSession((response) =>
//    {
//        if (!response.success)
//        {
//              UnityEngine.Debug.Log("error starting LootLocker session");

//            return;
//        }
//          UnityEngine.Debug.Log("session started successfully");
//    });
//    }

//    void StartGame() {

//    }


//    public void PasswordReset()
//    {
//        string email = resetPassword.text;
//        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
//        {
//            if (!response.success)
//            {
//                  UnityEngine.Debug.Log("error requesting password reset");
               
//                return;
//            }

//              UnityEngine.Debug.Log("requested password reset successfully");
            
//        });
//    }

//    public void AutoLogin() {
//         //checks if the player has logged in before
//        LootLockerSDKManager.CheckWhiteLabelSession(response =>
//            {
//                if (response == false)
//                {
//                    // Session was not valid, show error animation
//                    // and show back butt

//                    return;
                   
//                }
//                else
//                {
//                    // Session is valid, start game session
//                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
//                    {
//                        if (response.success)
//                        {
//                            // It was successful, log in
//                            StartGame();
                           
//                        }
//                        else
//                        {
//                            // Error

//                              UnityEngine.Debug.Log("error starting LootLocker session");
//                            // set the remember me bool to false here, so that the next time the player press login
//                            // they will get to the login screen
                            

//                            return;
//                        }

//                    });

//                }

//            });
            
//    }
//}
