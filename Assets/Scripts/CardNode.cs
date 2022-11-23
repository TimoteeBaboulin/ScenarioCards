using System;
using UnityEngine;

public class CardNode : Node
{
    public static event Action<CardNode> OnCardDestroyed;
    public int Rotation;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _textArea;
    
    [NonSerialized] public Node Master;
    [NonSerialized] public CardNode Slave;

    public void AddCard(){
        GameObject newCard = Instantiate(Rotation%2 == 0? CardPrefabHorizontal: CardPrefabVertical, transform);
        Slave = newCard.GetComponent<CardNode>();
        Slave.Master = this;
        _button.SetActive(false);

        var newCardTransform = newCard.GetComponent<RectTransform>();
        
        switch (Rotation){
            case 0:
                newCardTransform.pivot = new Vector2(0.5f, 1);
                newCardTransform.anchorMin = new Vector2(0, 0);
                newCardTransform.anchorMax = new Vector2(1, 0);
                newCardTransform.sizeDelta = new Vector2(0, 50);
                newCardTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
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
                newCardTransform.anchorMin = new Vector2(1, 0);
                newCardTransform.anchorMax = new Vector2(1, 1);
                newCardTransform.sizeDelta = new Vector2(50, 0);
                newCardTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
        }
    }

    public void RotateText(){

        Vector3 rotation = _textArea.transform.rotation.eulerAngles;
        rotation.z += 180;
        _textArea.transform.rotation = Quaternion.Euler(rotation);
        
    }
    
    private void OnDestroy(){
        OnCardDestroyed?.Invoke(this);
    }
}