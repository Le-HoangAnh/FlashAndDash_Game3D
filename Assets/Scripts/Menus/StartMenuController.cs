using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadGameButton;
    [SerializeField] EnterUsernameModal enterUsernameModal;
    [SerializeField] SaveSlotsModal saveSlotsModal;

    private void OnEnable()
    {
        newGameButton.onClick.AddListener(HandleNewGameButtonClicked);
        loadGameButton.onClick.AddListener(HandleLoadGameButtonClicked);
        Events.usernameSubmitted += HandleUsernameWasSubmitted;
        saveSlotsModal.saveSlotSelected += HandleSaveSlotWasSelected;
    }

    private void OnDisable()
    {
        newGameButton.onClick.RemoveListener(HandleNewGameButtonClicked);
        loadGameButton.onClick.RemoveListener(HandleLoadGameButtonClicked);
        Events.usernameSubmitted -= HandleUsernameWasSubmitted;
        saveSlotsModal.saveSlotSelected -= HandleSaveSlotWasSelected;
    }

    void HandleNewGameButtonClicked()
    {
        saveSlotsModal.SetIsNewGame(true);
        saveSlotsModal.ShowModal();
    }

    void HandleLoadGameButtonClicked()
    {
        saveSlotsModal.SetIsNewGame(false);
        saveSlotsModal.ShowModal();
    }

    void HandleUsernameWasSubmitted(string username)
    {
        Dictionary<string, string> sceneData = new Dictionary<string, string>();
        sceneData.Add("username", username);
        NavigationManager.LoadScene(Scenes.GARAGE, sceneData);
    }

    void HandleSaveSlotWasSelected(bool isNewGame, string index)
    {
        if (isNewGame)
        {
            enterUsernameModal.ShowModal();
            enterUsernameModal.Init(enterUsernameModal.HideModal);
        }
        else
        {
            Dictionary<string, string> sceneData = new Dictionary<string, string>();
            sceneData.Add("username", index);
            NavigationManager.LoadScene(Scenes.MAIN_MENU, sceneData);
        }
    }
}
