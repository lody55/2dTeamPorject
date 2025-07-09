using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JiHoon
{
    public class ShopManager : MonoBehaviour
    {
        [Header("카드 덱 매니저")]
        public UnitCardManager cardManager;

        [Header("UI 패널")]
        public GameObject shopPanel;

        [Header("플레이어 자원")]
        public int playerDiscontent = 50;
        public int playerGold = 1000;
        public int playerDominance = 50;
        public int playerChaos = 50;
        public TextMeshProUGUI goldText;

        [Header("데이터")]
        public List<ItemData> allItems;
        public Transform gridParent;
        public GameObject itemButtonPrefab;

        [Header("우측 UI")]
        public Button buyButton;

        [Header("추가 UI")]
        public TextMeshProUGUI effectText;
        public Image npcIllustration;

        private ItemData selectedItem;

        private void Start()
        {
            UpdateGoldUI();
            PopulateGrid();
            ClearDetail();
            buyButton.onClick.RemoveAllListeners();
            
        }

        private void PopulateGrid()
        {
            var shuffled = new List<ItemData>(allItems);
            ShuffleList(shuffled);
            foreach (var item in shuffled)
            {
                var go = Instantiate(itemButtonPrefab, gridParent);
                var ui = go.GetComponent<ItemButtonUI>();
                ui.Initialize(item, this);
            }
        }

        public void SelectItem(ItemData item)
        {
            selectedItem = item;
            buyButton.interactable = true;
            effectText.text = item.description;
            npcIllustration.sprite = item.illustration;
        }

        private void ClearDetail()
        {
            buyButton.interactable = false;
            effectText.text = "";
            npcIllustration.sprite = null;
        }

        public void OnBuyButton()
        {
            if (selectedItem == null) return;

            // 기본 구매 조건 확인
            bool canBuy =
                 playerGold >= selectedItem.price
              && playerDiscontent >= selectedItem.discontent
              && playerDominance >= selectedItem.dominace
              && playerChaos >= selectedItem.chaos;

            if (!canBuy)
            {
                Debug.Log("자원이 부족합니다!");
                return;
            }

            // ★★★ 유닛 아이템인 경우 카드덱 공간 확인 ★★★
            if (selectedItem.itemType == ItemType.Unit && selectedItem.unitPrefab != null)
            {
                // 강제로 null 카드 정리
                cardManager.CleanupNullCards();

                // 실제 카드 개수 확인
                int currentCount = cardManager.GetCardCount();

                Debug.Log($"구매 전 카드덱 상태: {currentCount}/10");

                if (currentCount >= 10)
                {
                    Debug.Log("카드덱이 가득 찼습니다! 카드를 사용한 후 다시 시도해주세요.");
                    return; // ★ 여기서 return하므로 돈이 깎이지 않음
                }
            }

            // ★★★ 모든 조건을 통과한 경우에만 돈 깎기 ★★★
            playerGold -= selectedItem.price;
            playerDiscontent -= selectedItem.discontent;
            playerDominance -= selectedItem.dominace;
            playerChaos -= selectedItem.chaos;
            UpdateGoldUI();

            // 유닛 아이템 추가
            if (selectedItem.itemType == ItemType.Unit && selectedItem.unitPrefab != null)
            {
                cardManager.AddCardFromShopItem(selectedItem);
                Debug.Log($"유닛 카드 구매 완료!");
            }

            // 구매 완료 후 선택 해제
            ClearDetail();
        }



        private void UpdateGoldUI()
        {
            goldText.text = $"Gold : {playerGold}";
        }

        public void OpenShop() => shopPanel.SetActive(true);
        public void CloseShop() { shopPanel.SetActive(false); ClearDetail(); }

        private void ShuffleList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int r = Random.Range(i, list.Count);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }
    }
}
