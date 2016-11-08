using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	//[Range(0.0f, 1.0f)]
	//public int JoystickNumber;
	public Vector2 pos;

	private Image background;
	private Image joystick;


	void Start () {
		//Carrega as imagens de background e do joystick
		background = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image> ();
	}

	public void OnPointerDown(PointerEventData ped)
	{
		//Quando ocorrer um toque dentro da área do objeto chama a função OnDrag passando as informaçoes do toque
		OnDrag (ped);
	}

	public void OnPointerUp(PointerEventData ped)
	{
		pos = Vector2.zero;
		//Desenha o Joystick a partir de uma ancora no meio do usando o vetor pos obtido acima multiplicado pela metade do tamanho  do retangulo do background
		joystick.rectTransform.anchoredPosition = new Vector2 (pos.x * (background.rectTransform.sizeDelta.x/3), pos.y * (background.rectTransform.sizeDelta.y/3));

	}

	public void OnDrag(PointerEventData ped)
	{
		//Ativa a imagem do joystick
		joystick.enabled = true;

		//Transforma o ponto onde ocorreu toque na tela num ponto local do retângulo da imagem background e coloca essa posição no vetor pos
		RectTransformUtility.ScreenPointToLocalPointInRectangle (background.rectTransform, ped.position, ped.pressEventCamera, out pos);

		//Converte o resultado obtido acima para um número de -1 à 1
		pos.x = pos.x / (background.rectTransform.sizeDelta.x/2);
		pos.y = pos.y / (background.rectTransform.sizeDelta.y/2);

		//Normaliza o vetor se necessário para que sua magnitude máxima seja sempre 1 forçando-o a respeitar o formato circular do desenho mesmo quando o toque ocorrer nas extremidades
		if (pos.magnitude > 1) 
		{
			pos.Normalize();
		}

		//Desenha o Joystick a partir de uma ancora no meio do usando o vetor pos obtido acima multiplicado pela metade do tamanho  do retangulo do background
		joystick.rectTransform.anchoredPosition = new Vector2 (pos.x * (background.rectTransform.sizeDelta.x/3), pos.y * (background.rectTransform.sizeDelta.y/3));

		//Debug.Log ("Pos  X = " + pos.x);
		//Debug.Log ("Pos  Y = " + pos.y);
	}
}
