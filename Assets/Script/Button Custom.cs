using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCustom : MonoBehaviour
{
    public GameObject menuPanel;      
    public GameObject tutorialPanel;  
    public GameObject aboutPanel;     

    void Start()
    {
        if (menuPanel != null) menuPanel.SetActive(true);
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        if (aboutPanel != null) aboutPanel.SetActive(false);
    }

    public void Tutorial()
    {
        if (menuPanel != null) menuPanel.SetActive(false);  
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            Invoke("HideTutorial", 5f);
        }
    }

    public void About()
    {
        if (menuPanel != null) menuPanel.SetActive(false);  
        if (aboutPanel != null)
        {
            aboutPanel.SetActive(true);
            Invoke("HideAbout", 5f);
        }
    }

    void HideTutorial()
    {
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(true);
    }

    void HideAbout()
    {
        if (aboutPanel != null) aboutPanel.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(true);  // Hiện menu lại
    }
}
