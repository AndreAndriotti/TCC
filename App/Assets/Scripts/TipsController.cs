using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    public GameObject dicaPrefab; // O prefab de uma única dica
    public Transform contentPanel; // O painel de conteúdo onde as dicas serão listadas
    public int numDicasIniciais = 11; // O número inicial de dicas a serem exibidas
    public ScrollRect scrollRect; // O componente ScrollRect para permitir a rolagem
    private float dicaSpacing = 200f;

    private void Start()
    {
        for (int i = 0; i < numDicasIniciais; i++)
        {
            AdicionarDica("Dica de exemplo #" + (i + 1));
        }

        scrollRect.verticalNormalizedPosition = 1.0f;
    }

    private void AdicionarDica(string textoDica)
    {
        // Instancia o prefab da dica
        GameObject novaDicaObj = Instantiate(dicaPrefab, contentPanel);

        // Define a posição vertical da dica com base no número de dicas já presentes
        RectTransform novaDicaTransform = novaDicaObj.GetComponent<RectTransform>();
        float yPos = -dicaSpacing * contentPanel.childCount;
        novaDicaTransform.anchoredPosition = new Vector2(novaDicaTransform.anchoredPosition.x, yPos);

        // Define o texto da dica
        Text novaDicaText = novaDicaObj.GetComponentInChildren<Text>();
        novaDicaText.text = textoDica;

        // Atualiza o conteúdo do ScrollRect para acomodar a nova dica
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
