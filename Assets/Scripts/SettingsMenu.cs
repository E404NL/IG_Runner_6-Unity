using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;       //AudioMixer

    public Dropdown ResolutionDropdown; //liste des resolution
    Resolution[] resolutions;           //resolution

    private void Start()
    {
        //Aweee aled on evite la duplication de resolution dans la liste
        resolutions = Screen.resolutions.Select(resolution=> new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;                     //Resolution actuelle, initialisee à 0
        for (int i = 0; i < resolutions.Length; i++)        //boucle d'ajout des resolutions possibles dans les options
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);             //listes de resolutions dans le dropdown
        ResolutionDropdown.value = currentResolutionIndex;  //Attribution de la resolution
        ResolutionDropdown.RefreshShownValue();             //rafraichie la liste des elements selectionnes pour definir si il est bien pri

        Screen.fullScreen = true;
    }
    public void SetVolume(float volume)     //reglage du volume
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)      //Application du changement de resolution
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
