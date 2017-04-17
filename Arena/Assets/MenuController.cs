using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    MenuButton[] buttons;
    MenuSelector selector;
    int selected = 0;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("JustWaitAGoddamnSecond");

        selector = GetComponentInChildren<MenuSelector>();
        buttons = GetComponentsInChildren<MenuButton>();
        buttons[selected].Highlight();
        selector.Target = buttons[selected];
    }

    IEnumerator JustWaitAGoddamnSecond()
    {
        yield return new WaitForSeconds(1.0f);
        InputEvents.MenuSelect.Subscribe(OnMovement);
        InputEvents.MenuConfirm.Subscribe(OnConfirm);
        yield return null;
    }

    private void OnDestroy()
    {
        InputEvents.MenuSelect.Unsubscribe(OnMovement);
        InputEvents.MenuConfirm.Unsubscribe(OnConfirm);
    }

    void OnMovement(InputEventInfo info)
    {
        if (info.singleAxisValue > 0.1f)
            IncrementSelection();
        if (info.singleAxisValue < 0.1f)
            DecrementSelection();
    }

    void OnConfirm(InputEventInfo info)
    {
        selector.Target.Select();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void IncrementSelection()
    {
        if (selected == buttons.Length - 1)
            return;

        ChangeSelection(selected + 1);
    }

    void DecrementSelection()
    {
        if (selected == 0)
            return;

        ChangeSelection(selected - 1);
    }


    void ChangeSelection(int newSelection)
    {
        Debug.Assert(newSelection >= 0 && newSelection < buttons.Length);

        buttons[selected].Unhighlight();
        selected = newSelection;
        buttons[selected].Highlight();
        selector.Target = buttons[selected];
    }
}
