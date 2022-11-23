using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainNode : Node{
    [NonSerialized] private readonly CardNode[] Nodes = new CardNode[4];
    [SerializeField] private GameObject[] Buttons = new GameObject[4];

    private readonly string[] _order ={"Top", "Right", "Bottom", "Left"};

    public void AddCard(int index){
        if (Nodes[index] != null)
            throw new Exception("Error, card already exists at the " + _order[index] + " border of the node.");

        GameObject cardPrefab = index % 2 == 0 ? CardPrefabHorizontal : CardPrefabVertical;
        
        GameObject newCard = Instantiate(cardPrefab, transform);
        Nodes[index] = newCard.GetComponent<CardNode>();
        Nodes[index].Master = this;
        Buttons[index].SetActive(false);

        RectTransform newCardTransform = newCard.GetComponent<RectTransform>();
        switch (index){
            case 0:
                newCardTransform.pivot = new Vector2(0.5f, 1);
                newCardTransform.anchorMin = new Vector2(0, 1);
                newCardTransform.anchorMax = new Vector2(1, 1);
                newCardTransform.sizeDelta = new Vector2(0, 50);
                newCardTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                Nodes[index].RotateText();
                break;
            case 1:
                newCardTransform.pivot = new Vector2(0, 0.5f);
                newCardTransform.anchorMin = new Vector2(1, 0);
                newCardTransform.anchorMax = new Vector2(1, 1);
                newCardTransform.sizeDelta = new Vector2(50, 0);
                break;
            case 2:
                newCardTransform.pivot = new Vector2(0.5f, 1);
                newCardTransform.anchorMin = new Vector2(0, 0);
                newCardTransform.anchorMax = new Vector2(1, 0);
                newCardTransform.sizeDelta = new Vector2(0, 50);
                break;
            case 3:
                newCardTransform.pivot = new Vector2(0, 0.5f);
                newCardTransform.anchorMin = new Vector2(0, 0);
                newCardTransform.anchorMax = new Vector2(0, 1);
                newCardTransform.sizeDelta = new Vector2(50, 0);
                newCardTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                Nodes[index].RotateText();
                break;
        }

        Nodes[index].Rotation = index;
    }
    
    private void OnEnable(){
        GameVariablesManager.OnScaleChange += ChangeScale;
        CardNode.OnCardDestroyed += UpdateNodes;
    }

    private void OnDisable(){
        GameVariablesManager.OnScaleChange -= ChangeScale;
        CardNode.OnCardDestroyed += UpdateNodes;
    }

    private void ChangeScale(){
        transform.localScale = Vector3.one * GameVariablesManager.CardScale;
    }

    private void UpdateNodes(CardNode node){
        if (!Nodes.Contains(node)) return;
        
        var index = Array.FindIndex(Nodes, obj => obj = node);
        Nodes[index] = null;
    }
}