using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;
using System;

public class PlayfabManager : MonoBehaviour {

    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    private static Action<UpdatePlayerStatisticsResult> onLeaderboardUpdate;

    public TMP_InputField nameInput;
    public GameObject nameWindow;

    string loggedInPlayfabID;

    // REGISTER/LOGIN/RESET_PASSWORD //
    public void RegisterButton() {
        if(passwordInput.text.Length < 6) {
            messageText.text = "Password is too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, onRegisterSuccess, onError);
    }

    void onRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onLoginSuccess, onError);
    }

    private void onLoginSuccess(LoginResult result) {
        loggedInPlayfabID = result.PlayFabId;
        messageText.text = "Logged in!";
        Debug.Log("Successful login");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        if (name == null)
            nameWindow.SetActive(true);
    }

    public void SubmitNameButton() {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, onError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result) {
        Debug.Log("Updated display name!");
    }

    public void ResetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = "7E6E0"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, onError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
        messageText.text = "Password reset mail sent!";
    }

    #region otherStuffs

    void Awake() {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start() {
        Login();
    }
    void Login() {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, onSuccess, onError);
    }

    void onSuccess(LoginResult result) {
        Debug.Log("Successful login/account create!");
    }
    void onError(PlayFabError error) {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }


    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, onError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successfully Updated leaderboard");
    }
    public void GetLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 8
        };
        PlayFabClientAPI.GetLeaderboard(request, onLeaderboardGet, onError);
    }
    void onLeaderboardGet(GetLeaderboardResult result) {
        foreach (Transform item in PlayerControll.Table)
            Destroy(item.gameObject);

        foreach (var item in result.Leaderboard) {
            GameObject newGo = Instantiate(PlayerControll.rowPrefab, PlayerControll.Table);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString() + ".";
            if(item.DisplayName == null)
                texts[1].text = "User" + UnityEngine.Random.Range(1,1000).ToString();
            else
                texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("Place: {0} | ID: {1} | Value: {2}", item.Position, item.PlayFabId, item.StatValue));
        }

    }
    public void GetLeaderboardAroundPlayer() {
        var request = new GetLeaderboardAroundPlayerRequest {
            StatisticName = "Score",
            MaxResultsCount = 8,
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, onError);
    }
    private void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result) {
        foreach (Transform item in PlayerControll.Table)
            Destroy(item.gameObject);

        foreach (var item in result.Leaderboard) {
            GameObject newGo = Instantiate(PlayerControll.rowPrefab, PlayerControll.Table);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString() + ".";
            if (item.DisplayName == null)
                texts[1].text = "User" + UnityEngine.Random.Range(1, 100).ToString();
            else
                texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInPlayfabID) {
                texts[0].color = Color.yellow;
                texts[1].color = Color.yellow;
                texts[2].color = Color.yellow;
            }
            Debug.Log(string.Format("Place: {0} | ID: {1} | Value: {2}", item.Position, item.PlayFabId, item.StatValue));
        }
    }










    #endregion
}
