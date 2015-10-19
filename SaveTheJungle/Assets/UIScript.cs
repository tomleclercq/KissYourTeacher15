using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject Menu;
    public GameObject Book;
    public GameObject UI;
    public GameObject Credit;
    Image creditBackground;

    public float scrollSpeed = 2.0f;
    public RectTransform CreditScroll;
    public float creditEndPosition = 800;
    public Image closingScreen;



    public void SetMenuUI()
    {
        if (Menu != null) Menu.SetActive(true);
        if (Book != null) Book.SetActive(false);
        if (UI != null) UI.SetActive(false);
        if (Credit != null) Credit.SetActive(false);
    }

    public void SetGameUI()
    {
        if( Menu != null )Menu.SetActive(false);
        if (Book != null)
        {
            Book.SetActive(false);
            Book.GetComponent<BookScript>().Init();        
        }
        if (UI != null) UI.SetActive(true);
        if (Credit != null) Credit.SetActive(false);

    }

    public IEnumerator DarkenScreen()
    {
        float lerpValue = 0;
        Color color = closingScreen.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1.0f, lerpValue);
            closingScreen.color = color;
            yield return null;
        }
    }

    public IEnumerator LightenScreen()
    {
        float lerpValue = 0;
        Color color = creditBackground.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0.0f, lerpValue);
            creditBackground.color = color;
            yield return null;
        }
    }

    public IEnumerator LightenCreditScreen()
    {
        float lerpValue = 0;
        Color color = closingScreen.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0.0f, lerpValue);
            closingScreen.color = color;
            yield return null;
        }
    }

    public IEnumerator StartCredit()
    {
        Credit.SetActive(true);

        Vector3 startPosition = CreditScroll.anchoredPosition3D;
        startPosition.y = -930;
        CreditScroll.anchoredPosition3D = startPosition;

        float lerpValue = 0;
        Image creditBackground = Credit.GetComponent<Image>();
        Color color = creditBackground.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1.0f, lerpValue);
            creditBackground.color = color;
            yield return null;
        }

        lerpValue = 0;
        startPosition = CreditScroll.anchoredPosition3D;
        Vector3 endPosition =startPosition ;
        Vector3 position = CreditScroll.anchoredPosition3D;

        endPosition.y = 0;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * 1/scrollSpeed;
            position = Vector3.Lerp(startPosition, endPosition, lerpValue);
            CreditScroll.anchoredPosition3D = position;
            yield return  null;
        }
        yield return new WaitForSeconds(1.5f);

        lerpValue = 0;
        startPosition = CreditScroll.anchoredPosition3D;
        endPosition.y = creditEndPosition;

        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * 1/scrollSpeed;
            position = Vector3.Lerp( startPosition , endPosition, lerpValue);
            CreditScroll.anchoredPosition3D = position;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
    }

}
