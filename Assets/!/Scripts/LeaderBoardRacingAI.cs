using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using System;

namespace LootLocker
{

    public class LeaderboardRacingAI : MonoBehaviour
    {
        public InputField scoreInputField;
        public string infoText;
        public string playerIDText;
        public string leaderboardTop10Text;
        public string leaderboardCenteredText;



        /*
        * leaderboardKey or leaderboardID can be used.
        * leaderboardKey can be the same between stage and live /development mode on/off.
        * So if you use the key instead of the ID, you don't need to change any code when switching development_mode.
        */
        string leaderboardKey = "racingai";
        // int leaderboardID = 4705;

        string memberID;

        // Start is called before the first frame update
        void Start()
        {
           // StartCoroutine(DoLoginAndSetUp());
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                UploadScore("100");
            }
        }

        IEnumerator DoLoginAndSetUp()
        {
            // Wait until end of frame to ensure that the UI has been loaded
            yield return new WaitForEndOfFrame();
            /* 
             * Override settings to use the Example game setup
             */
            LootLockerSettingsOverrider.OverrideSettings();
            StartGuestSession();
        }

        public void StartGuestSession()
        {
            /* Start guest session without an identifier.
             * LootLocker will create an identifier for the user and store it in PlayerPrefs.
             * If you want to create a new player when testing, you can use PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
             */
            PlayerPrefs.DeleteKey("LootLockerGuestPlayerID");
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    infoText = "Guest session started";
                    playerIDText = "Player ID:" + response.player_id.ToString();
                    memberID = response.player_id.ToString();
                    UpdateLeaderboardTop10();
                    UpdateLeaderboardCentered();
                }
                else
                {
                    infoText = "Error" + response.errorData.message;
                }
            });
        }

        public void UploadScore(string score)
        {
            /*
             * Get the players System language and send it as metadata
             */
            string metadata = Application.systemLanguage.ToString();

            /*
             * Since this is a player leaderboard, member_id is not needed, 
             * the logged in user is the one that will upload the score.
             */
            LootLockerSDKManager.SubmitScore("", int.Parse(score), leaderboardKey, metadata, (response) =>
            {
                if (response.success)
                {
                    infoText = "Player score was submitted";
                    /*
                     * Update the leaderboards when the new score was sent so we can see them
                     */
                    UpdateLeaderboardCentered();
                    UpdateLeaderboardTop10();
                }
                else
                {
                    infoText = "Error submitting score:" + response.errorData.message;
                    Debug.Log(infoText);    
                }
            });
        }
        void UpdateLeaderboardCentered()
        {
            LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (memberResponse) =>
            {
                if (memberResponse.success)
                {
                    if (memberResponse.rank == 0)
                    {
                        leaderboardCenteredText = "Upload score to see centered";
                        return;
                    }
                    int playerRank = memberResponse.rank;
                    int count = 10;
                    /*
                     * Set "after" to 5 below and 4 above the rank for the current player.
                     * "after" means where to start fetch the leaderboard entries.
                     */
                    int after = playerRank < 6 ? 0 : playerRank - 5;

                    LootLockerSDKManager.GetScoreList(leaderboardKey, count, after, (scoreResponse) =>
                    {
                        if (scoreResponse.success)
                        {
                            infoText = "Centered scores updated";

                            /*
                             * Format the leaderboard
                             */
                            string leaderboardText = "";
                            for (int i = 0; i < scoreResponse.items.Length; i++)
                            {
                                LootLockerLeaderboardMember currentEntry = scoreResponse.items[i];

                                /*
                                 * Highlight the player with rich text
                                 */
                                if (currentEntry.rank == playerRank)
                                {
                                    leaderboardText += "<color=yellow>";
                                }

                                leaderboardText += currentEntry.rank + ".";
                                leaderboardText += currentEntry.player.id;
                                leaderboardText += " - ";
                                leaderboardText += currentEntry.score;
                                leaderboardText += " - ";
                                leaderboardText += currentEntry.metadata;

                                /*
                                * End highlighting the player
                                */
                                if (currentEntry.rank == playerRank)
                                {
                                    leaderboardText += "</color>";
                                }
                                leaderboardText += "\n";
                            }
                            leaderboardCenteredText = leaderboardText;
                        }
                        else
                        {
                            infoText = "Could not update centered scores:" + scoreResponse.errorData.message;
                        }
                    });
                }
                else
                {
                    infoText = "Could not get member rank:" + memberResponse.errorData.message;
                }
            });
        }

        void UpdateLeaderboardTop10()
        {
            LootLockerSDKManager.GetScoreList(leaderboardKey, 10, (response) =>
            {
                if (response.success)
                {
                    infoText = "Top 10 leaderboard updated";
                    infoText = "Centered scores updated";

                    /*
                     * Format the leaderboard
                     */
                    string leaderboardText = "";
                    for (int i = 0; i < response.items.Length; i++)
                    {
                        LootLockerLeaderboardMember currentEntry = response.items[i];
                        leaderboardText += currentEntry.rank + ".";
                        leaderboardText += currentEntry.player.id;
                        leaderboardText += " - ";
                        leaderboardText += currentEntry.score;
                        leaderboardText += " - ";
                        leaderboardText += currentEntry.metadata;
                        leaderboardText += "\n";
                    }
                    leaderboardTop10Text = leaderboardText;
                }
                else
                {
                    infoText = "Error updating Top 10 leaderboard";
                }
            });
        }
    }
}
