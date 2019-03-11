using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SwitchAnim : MonoBehaviour {

	[Header("SWITCH")]
    public Animator switchAnimator;

    [Header("SETTINGS")]
    [Tooltip("IMPORTANT! EVERY SWITCH MUST HAVE A DIFFERENT ID")]
    public int switchID;
    public bool isOn;
    public bool saveValue;

    public UnityEvent OffEvents;
    public UnityEvent OnEvents;

    private Button offButton;
    private Button onButton;

    private int playerPrefsHelper;
    private string onTransition = "Switch On";
    private string offTransition = "Switch Off";

    void Start ()
	{
        playerPrefsHelper = PlayerPrefs.GetInt(switchID + "Switch");
    }

	public void AnimateSwitch()
    {
        if (isOn == true) 
		{
            OffEvents.Invoke();
            switchAnimator.Play(offTransition);
            isOn = false;
            playerPrefsHelper = 0;
        } 

		else
		{
            OnEvents.Invoke();
            switchAnimator.Play(onTransition);
            isOn = true;
            playerPrefsHelper = 1;
        }

        if (saveValue == true)
        {
            PlayerPrefs.SetInt(switchID + "Switch", playerPrefsHelper);
        }
    }
}