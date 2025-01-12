using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerField : MonoBehaviour, IDropHandler
{
    public Transform content;
    //دالة اسقاط البطاقة 
    public void OnDrop(PointerEventData eventData)
    {
        HandCard card = eventData.pointerDrag.transform.GetComponent<HandCard>();
        Player player = Player.localPlayer;
        int manaCost = card.cost.text.ToInt();

        //
        if (player.IsOurTurn() && player.deck.CanPlayCard(manaCost))
        {
            int index = card.handIndex;
            CardInfo cardInfo = player.deck.hand[index];
            //Debug.LogError(index + " / " + cardInfo.name);
            //
            Player.gameManager.isSpawning = true;
            Player.gameManager.isHovering = false; //جعل التحويم فولص من اجل لا يمكن للعب ترحريك البطاقة من مكان الملعب
            Player.gameManager.CmdOnCardHover(0, index);
            player.deck.CmdPlayCard(cardInfo, index); // Summon card onto the board
            player.combat.CmdChangeMana(-manaCost); // Reduce player's mana  //تقليل صحة المانة من خلال السارفر
        }
    }

    public void UpdateFieldCards()
    {
        int cardCount = content.childCount;
        for (int i = 0; i < cardCount; ++i)
        {
            FieldCard card = content.GetChild(i).GetComponent<FieldCard>();
            card.CmdUpdateWaitTurn();
        }
    }
}
