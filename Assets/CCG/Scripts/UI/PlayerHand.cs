using UnityEngine;

public class PlayerHand : MonoBehaviour  // سركربت يد اللعب 
{
    public GameObject panel;
    public HandCard cardPrefab;
    public Transform handContent;
    public PlayerType playerType;
    private Player player;
    private PlayerInfo enemyInfo;
    private int cardCount = 0; // Amount of cards in hand

    void Update()
    {
        player = Player.localPlayer;
        //تحقق من اذا كان العب والعدو موجودين 
        if (player && player.hasEnemy) enemyInfo = player.enemyInfo;
        //اذا كان اللعب هو اللعب وليس العدو قم باضافة البطاقات 
        if (IsPlayerHand() && Input.GetKeyDown(KeyCode.C))
        {
            player.deck.DrawCard(7); //قم باضافة 7  بطاقات الى اللعب 
        }
        if (IsEnemyHand())
        {
            // instantiate/destroy enough slots
            UIUtils.BalancePrefabs(cardPrefab.gameObject, enemyInfo.handCount, handContent);

            // refresh all members
            for (int i = 0; i < enemyInfo.handCount; ++i)
            {
                //تقوم الفور لوب بالدخول الى الكونتنت الخاصة بيد العدو ولمرور على جميع البطاقات وقلب جميع البطاقات عن تريق زيادة الشفافية كي تظهر خلفية البطاقة المقلوبة 
                HandCard slot = handContent.GetChild(i).GetComponent<HandCard>();

                slot.AddCardBack();
                //عدد لاوراق يساوي عدد لاوراق الموجودة في يد العدو  
                cardCount = enemyInfo.handCount;
            }
        }
    }
    //دالة اضافة البطاقة الى اليد
    public void AddCard(int index)
    {
        //انشاء البطاقة اي لكائن الخاص بي البطاقات
        GameObject cardObj = Instantiate(cardPrefab.gameObject);
        cardObj.transform.SetParent(handContent, false);
        //اعطاء معلومات البطاقة مثل قوة الهجوم وقوة الدفاع الخ
        CardInfo card = player.deck.hand[index];
        HandCard slot = cardObj.GetComponent<HandCard>();

        slot.AddCard(card, index, playerType);
    }

    public void RemoveCard(int index)
    {
        for (int i = index; i < handContent.childCount; ++i)
        {
            HandCard slot = handContent.GetChild(i).GetComponent<HandCard>();
            int count = i;
            if (count == index) slot.RemoveCard();
            else if (slot.handIndex > index) slot.handIndex--;
        }
    }
    //
    bool IsEnemyHand() => player && player.hasEnemy && player.deck.hand.Count == 7 && playerType == PlayerType.ENEMY && enemyInfo.handCount != cardCount;
    //هذه الدالة تتحقق من اذا كان اللعب هو اللعب المحلي وعملية انشاء البطاقات تساوي صحيح و نوع اللعب هو اللعب وليس العدو ترجع قيمة صحيح
    bool IsPlayerHand() => player && player.deck.spawnInitialCards && playerType == PlayerType.PLAYER;
}