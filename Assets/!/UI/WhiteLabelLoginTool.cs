using LootLocker.Requests;
using System;
using System.Collections;
using UnityEngine;

using UnityEngine.UIElements;

namespace LootLocker
{
    public class WhiteLabelLoginTool : MonoBehaviour
    {

        public UIDocument _document;
        public StyleSheet _styleSheet;

        [Header("Mainui ")]
        public UIDocument _mainDocument;
        public UIDocument _miniMapDocument;

        [Header("New User")]
        public TextField newUserEmailInputField;
        public TextField newUserPasswordInputField;

        [Header("Existing User")]
        public TextField existingUserEmailInputField;
        public TextField existingUserPasswordInputField;

        public Label loginInformationText, playerIdText;

        public Label infoText;


        string leaderboardKey = "myracingai";

        Toggle rememberMeToggle;
        int rememberMe;

        public VisualElement root;
        public Button joinBtn, loginBtn, updateBtn, guestBtn;
        public Label label;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                root.AddToClassList("dot");
                _mainDocument.rootVisualElement.visible = true;
                _miniMapDocument.rootVisualElement.visible = true;
            }
        }

        public void UploadScore(string score)
        {
           
            string metadata = Application.systemLanguage.ToString();

            Debug.Log("playerID " + playerID);
            //147277  nothingise
            /*
             * Since this is a player leaderboard, member_id is not needed, 
             * the logged in user is the one that will upload the score.
             */
            LootLockerSDKManager.SubmitScore(playerID, int.Parse(score), leaderboardKey, metadata, (response) =>
            {
                if (response.success)
                {
                    //infoText = "Player score was submitted";
                    Debug.Log("Player score was submitted" + score.ToString());
                    return;
                }
                else
                {
                    //infoText = "Error submitting score:" + response.errorData.message;
                    Debug.Log("error Player score was submitted" + response.errorData.message);
                    return;
                }
            });
        }
        private void Start()
        {

            MainUI.SessionClearEvent += MainUI_SessionClearEvent;
            _mainDocument.rootVisualElement.visible = false;
            _miniMapDocument.rootVisualElement.visible = false;

            root = _document.rootVisualElement;
            root.styleSheets.Add(_styleSheet);

            joinBtn = root.Q<Button>("joinBtn");
            loginBtn = root.Q<Button>("loginBtn");
            updateBtn = root.Q<Button>("updateBtn");
            guestBtn = root.Q<Button>("guestBtn");
            infoText = root.Q<Label>("infoText");
            rememberMeToggle = root.Q<Toggle>("rememberMeToggle");


            joinBtn.AddToClassList("button");
            loginBtn.AddToClassList("button");
            updateBtn.AddToClassList("button");
            guestBtn.AddToClassList("button");



            loginInformationText = root.Q<Label>("loginInformationText");
            playerIdText = root.Q<Label>("loginInformationText");

            newUserEmailInputField = root.Q<TextField>("newUserEmailInputField");
            newUserPasswordInputField = root.Q<TextField>("newUserPasswordInputField");
            existingUserEmailInputField = root.Q<TextField>("existingUserEmailInputField");
            existingUserPasswordInputField = root.Q<TextField>("existingUserPasswordInputField");
            joinBtn.clicked += () =>
            {
                CreateAccount();
            };

            loginBtn.clicked += () =>
            {
                Login();
            };

            updateBtn.clicked += () =>
            {
                CreateOrUpdateKeyValue();
            };

            guestBtn.clicked += () =>
            {
                StartCoroutine(DoGuestLogin());


            };


            // See if we should log in the player automatically
            rememberMe = PlayerPrefs.GetInt("rememberMe", 0);
            if (rememberMe == 0)
            {
                rememberMeToggle.value = false;
            }
            else
            {
                rememberMeToggle.value = true;
            }
            rememberMeToggle.RegisterValueChangedCallback(evt =>
            {
                ToggleRememberMe();
            });

        }

        private void MainUI_SessionClearEvent(string ranking)
        {
           // UploadScore(ranking);
        }

        public void ToggleRememberMe()
        {
            bool rememberMeBool = rememberMeToggle.value;
            rememberMe = Convert.ToInt32(rememberMeBool);


            PlayerPrefs.SetInt("rememberMe", rememberMe);
        }
        IEnumerator DoGuestLogin()
        {
            // Wait until end of frame to ensure that the UI has been loaded
            yield return new WaitForEndOfFrame();
            /* 
            * Override settings to use the Example game setup
            */
            LootLockerSettingsOverrider.OverrideSettings();

            /* Start guest session without an identifier.
            * LootLocker will create an identifier for the user and store it in PlayerPrefs.
            * If you want to create a new player when testing, you can use PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
            */
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {

                    loginInformationText.text = "Guest session started";
                    playerIdText.text = "Player ID:" + response.player_id.ToString();
                    playerID = response.player_id.ToString();

                    root.AddToClassList("dot");
                    _mainDocument.rootVisualElement.visible = true;
                    _miniMapDocument.rootVisualElement.visible = true;

                }
                else
                {
                    loginInformationText.text = "Error" + response.errorData.ToString();
                }
            });



        }
        string plyerULID = string.Empty;
        private void Awake()
        {
            LootLockerSettingsOverrider.OverrideSettings();
        }
        //
        private void OnCompleteCallback(LootLockerGetPersistentStorageResponse response)
        {
            if (response.success)
            {
                Console.WriteLine("Key-value pair updated successfully!");
            }
            else
            {
                Console.WriteLine($"Failed to update key-value: {response.statusCode}");
            }
        }

        public void CreateOrUpdateKeyValue()
        {
            LootLockerSDKManager.UpdateOrCreateKeyValue("BESS", "cccc", OnCompleteCallback, plyerULID);

        }
        // Called when pressing "Log in"
        string playerID = string.Empty;
        public void Login()
        {
            string email = existingUserEmailInputField.text;
            string password = existingUserPasswordInputField.text;
            LootLockerSDKManager.WhiteLabelLogin(email, password, rememberMeToggle.value, loginResponse =>
            {
                if (!loginResponse.success)
                {


                    // Error
                    infoText.text = "Error logging in:" + loginResponse.errorData.message;
                    return;
                }
                else
                {
                    playerID = loginResponse.ID.ToString();

                    Debug.Log("loginResponse.ID  " + loginResponse.ID);
                    // leaderboard.UploadScore("800");
                    infoText.text = "Player was logged in succesfully";

                    root.AddToClassList("dot");
                    _mainDocument.rootVisualElement.visible = true;
                    _miniMapDocument.rootVisualElement.visible = true;
                }

                // Is the account verified?
                if (loginResponse.VerifiedAt == null)
                {
                    // Stop here if you want to require your players to verify their email before continuing,
                    // verification must also be enabled on the LootLocker dashboard
                }

                // Player is logged in, now start a game session
                LootLockerSDKManager.StartWhiteLabelSession((startSessionResponse) =>
                {
                    if (startSessionResponse.success)
                    {
                        plyerULID = startSessionResponse.player_ulid;
                        // Session was succesfully started;
                        // After this you can use LootLocker features
                        infoText.text = "Session started successfully";

                        root.visible = false;
                        //SceneManager.LoadScene("SAHARA");
                    }
                    else
                    {
                        // Error
                        infoText.text = "Error starting LootLocker session:" + startSessionResponse.errorData.message;
                    }
                });


            });



        }



        // Called when pressing "Create account"
        public void CreateAccount()
        {
            string email = newUserEmailInputField.text;
            string password = newUserPasswordInputField.text;

            LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
            {
                if (!response.success)
                {
                    infoText.text = "Error signing up:" + response.errorData.message;
                    return;
                }
                else
                {
                    // Succesful response  
                    infoText.text = "Account created";
                    root.visible = false;
                }
            });
        }


        public void ResendVerificationEmail()
        {
            // Player ID can be retrieved when starting a session or creating an account.
            int playerID = 0;
            // Request a verification email to be sent to the registered user, 
            LootLockerSDKManager.WhiteLabelRequestVerification(playerID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Verification email sent!");
                }
                else
                {
                    Debug.Log("Error sending verification email:" + response.errorData.message);
                }

            });
        }

        public void SendResetPassword()
        {
            // Sends a password reset-link to the email
            LootLockerSDKManager.WhiteLabelRequestPassword("email@email-provider.com", (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Password reset link sent!");
                }
                else
                {
                    Debug.Log("Error sending password-reset-link:" + response.errorData.message);
                }
            });
        }
    }
}
