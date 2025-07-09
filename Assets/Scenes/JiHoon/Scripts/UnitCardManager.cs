using System.Collections.Generic;
using UnityEngine;

namespace JiHoon
{
    public class UnitCardManager : MonoBehaviour
    {
        [Header("UnitSpawner (unitPresets 담긴 컴포넌트)")]
        public UnitSpawner spawner;

        [Header("카드 UI 프리팹 (UnitCardUI 붙어 있어야 함)")]
        public GameObject cardUIPrefab;

        [Header("유닛 프리셋 리스트 (순서대로 icon 포함)")]
        public List<UnitPreset> unitPresets;

        [Header("카드 덱을 표시할 부모 오브젝트")]
        public Transform deckContainer;

        [Header("배치 매니저")]
        public UnitPlacementManager placementMgr;

        [Header("하스스톤 스타일 카드덱")]
        public SimpleCardDeck hearthstoneDeck;

        [Header("카드덱 모드 선택")]
        public bool useHearthstoneStyle = true;

        [Header("카드 개수 제한")]
        public int maxCardCount = 10; // 최대 카드 개수

        // 현재 카드 목록 (관리용)
        private List<GameObject> currentCards = new List<GameObject>();

        void Start()
        {
            // placementMgr 자동 찾기
            if (placementMgr == null)
                placementMgr = FindFirstObjectByType<UnitPlacementManager>();

            // hearthstoneDeck 자동 찾기
            if (hearthstoneDeck == null && useHearthstoneStyle)
            {
                hearthstoneDeck = deckContainer.GetComponent<SimpleCardDeck>();
                if (hearthstoneDeck == null)
                    useHearthstoneStyle = false;
            }

            // 하스스톤 스타일 사용시 기존 레이아웃 비활성화
            if (useHearthstoneStyle && hearthstoneDeck != null)
            {
                var layoutGroup = deckContainer.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();
                if (layoutGroup) layoutGroup.enabled = false;
            }
        }

        public void AddRandomCards(int count)
        {
            var presets = spawner.unitPresets;
            int total = presets.Length;

            for (int i = 0; i < count; i++)
            {
                // 최대 개수 확인
                if (currentCards.Count >= maxCardCount)
                {
                    Debug.Log($"카드덱이 가득참! 최대 {maxCardCount}장");
                    break;
                }

                int idx = Random.Range(0, total);

                if (useHearthstoneStyle && hearthstoneDeck != null)
                {
                    // 하스스톤 스타일
                    var tempGo = CreateTempCardUI(idx, presets[idx].icon, false, null);
                    var tempUI = tempGo.GetComponent<UnitCardUI>();

                    if (tempUI.placementMgr == null)
                        tempUI.placementMgr = placementMgr;

                    hearthstoneDeck.AddCard(tempUI);
                    tempGo.SetActive(false);
                    currentCards.Add(tempGo);
                }
                else
                {
                    // 기존 방식
                    var go = Instantiate(cardUIPrefab, deckContainer);
                    var ui = go.GetComponent<UnitCardUI>();
                    ui.Init(idx, presets[idx].icon, placementMgr);
                    currentCards.Add(go);
                }
            }

            Debug.Log($"현재 카드 개수: {currentCards.Count}/{maxCardCount}");
        }

        public void AddCardFromShopItem(ItemData item)
        {
            Debug.Log($"=== AddCardFromShopItem 시작 ===");
            Debug.Log($"아이템: {item?.itemName}");

            if (item == null || item.unitPrefab == null)
            {
                Debug.LogWarning("유효하지 않은 아이템입니다.");
                return;
            }

            // 강제로 null 카드 정리
            CleanupNullCards();

            Debug.Log($"정리 후 현재 카드 개수: {currentCards.Count}");
            Debug.Log($"최대 카드 개수: {maxCardCount}");

            // ★★★ 올바른 최대 개수 확인 ★★★
            if (currentCards.Count >= maxCardCount)
            {
                Debug.Log($"카드덱이 가득참! 최대 {maxCardCount}장. 상점 아이템을 추가할 수 없습니다.");
                ShowDeckFullWarning();
                return;
            }

            if (useHearthstoneStyle && hearthstoneDeck != null)
            {
                Debug.Log(" 카드 추가 시작");

                // 하스스톤 스타일
                var tempGo = CreateTempCardUI(-1, item.icon, true, item);
                var tempUI = tempGo.GetComponent<UnitCardUI>();

                if (tempUI.placementMgr == null)
                    tempUI.placementMgr = placementMgr;

                hearthstoneDeck.AddCard(tempUI);
                tempGo.SetActive(false);
                currentCards.Add(tempGo);

                Debug.Log($"카드 추가 완료. tempGo: {tempGo.name}");
            }
            else
            {
                Debug.Log("기존 방식 카드 추가 시작");

                // 기존 방식
                var go = Instantiate(cardUIPrefab, deckContainer);
                var ui = go.GetComponent<UnitCardUI>();
                ui.InitFromShopItem(item, placementMgr);
                currentCards.Add(go);

                Debug.Log($"기존 방식 카드 추가 완료. go: {go.name}");
            }

            Debug.Log($"상점 아이템 추가 완료. 현재 카드 개수: {currentCards.Count}/{maxCardCount}");
            Debug.Log($"=== AddCardFromShopItem 완료 ===");
        }

        private void ShowDeckFullWarning()
        {
            // UI 경고 표시 (옵션)
            Debug.LogWarning("카드덱이 가득 찼습니다! 카드를 사용한 후 다시 시도해주세요.");

            // 실제 UI 팝업을 띄우고 싶다면 여기에 추가
            // 예: warningPopup.Show("카드덱이 가득 찼습니다!");
        }

        private GameObject CreateTempCardUI(int presetIndex, Sprite icon, bool isFromShop, ItemData shopItem)
        {
            Debug.Log($"CreateTempCardUI 시작 - isFromShop: {isFromShop}, shopItem: {shopItem?.itemName}");

            var go = Instantiate(cardUIPrefab);
            var ui = go.GetComponent<UnitCardUI>();

            if (ui == null)
            {
                Debug.LogError("UnitCardUI 컴포넌트를 찾을 수 없습니다!");
                return go;
            }

            if (isFromShop)
            {
                Debug.Log("상점 아이템으로 초기화");
                ui.InitFromShopItem(shopItem, placementMgr);
            }
            else
            {
                Debug.Log("프리셋으로 초기화");
                ui.Init(presetIndex, icon, placementMgr);
            }

            Debug.Log($"CreateTempCardUI 완료 - 생성된 오브젝트: {go.name}");
            return go;
        }

        public void RemoveCard(UnitCardUI cardUI)
        {
            Debug.Log($"=== RemoveCard 호출 ===");
            Debug.Log($"찾는 카드: {cardUI?.name}");
            Debug.Log($"제거 전 카드 개수: {currentCards.Count}");

            // 현재 카드 목록 출력
            for (int i = 0; i < currentCards.Count; i++)
            {
                if (currentCards[i] != null)
                {
                    var ui = currentCards[i].GetComponent<UnitCardUI>();
                    Debug.Log($"  카드 {i}: {currentCards[i].name} (UnitCardUI: {ui?.name})");
                }
                else
                {
                    Debug.Log($"  카드 {i}: NULL");
                }
            }

            bool found = false;
            for (int i = currentCards.Count - 1; i >= 0; i--)
            {
                if (currentCards[i] != null)
                {
                    var ui = currentCards[i].GetComponent<UnitCardUI>();
                    if (ui == cardUI)
                    {
                        Debug.Log($"카드 찾음! 인덱스: {i}, 제거할 오브젝트: {currentCards[i].name}");
                        Destroy(currentCards[i]);
                        currentCards.RemoveAt(i);
                        found = true;
                        break;
                    }
                }
                else
                {
                    // null 카드 정리
                    Debug.Log($"null 카드 발견 및 제거: 인덱스 {i}");
                    currentCards.RemoveAt(i);
                }
            }

            if (found)
            {
                Debug.Log($"카드 제거 완료! 현재 카드 개수: {currentCards.Count}/{maxCardCount}");
            }
            else
            {
                Debug.LogWarning("제거할 카드를 찾지 못했습니다!");
                Debug.LogWarning($"찾으려던 카드 주소: {cardUI?.GetHashCode()}");
            }

            Debug.Log($"=== RemoveCard 완료 ===");
        }

        // 추가: null 카드들 정리하는 메서드
        public void CleanupNullCards()
        {
            int removed = 0;
            for (int i = currentCards.Count - 1; i >= 0; i--)
            {
                if (currentCards[i] == null)
                {
                    currentCards.RemoveAt(i);
                    removed++;
                }
            }

            if (removed > 0)
            {
                Debug.Log($"{removed}개의 null 카드 정리됨. 현재 카드 개수: {currentCards.Count}");
            }
        }

        // GetCardCount 메서드도 수정
        public int GetCardCount()
        {
            // null 카드 정리
            CleanupNullCards();
            return currentCards.Count;
        }

        // IsCardDeckFull 메서드도 수정
        public bool IsCardDeckFull()
        {
            // null 카드 정리
            CleanupNullCards();
            return currentCards.Count >= maxCardCount;
        }

        public int GetRemainingSlots()
        {
            return maxCardCount - currentCards.Count;
        }

        // 기존 메서드들 유지
        public void SetHearthstoneMode(bool enable)
        {
            useHearthstoneStyle = enable;

            var layoutGroup = deckContainer.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();

            if (enable && hearthstoneDeck != null)
            {
                if (layoutGroup) layoutGroup.enabled = false;
                ConvertToHearthstoneStyle();
            }
            else
            {
                if (layoutGroup) layoutGroup.enabled = true;
                ConvertToClassicStyle();
            }
        }

        private void ConvertToHearthstoneStyle()
        {
            for (int i = 0; i < currentCards.Count; i++)
            {
                if (currentCards[i] != null)
                {
                    var ui = currentCards[i].GetComponent<UnitCardUI>();
                    hearthstoneDeck.AddCard(ui);
                    currentCards[i].SetActive(false);
                }
            }
        }

        private void ConvertToClassicStyle()
        {
            if (hearthstoneDeck != null)
                hearthstoneDeck.ClearAllCards();

            for (int i = 0; i < currentCards.Count; i++)
            {
                if (currentCards[i] != null)
                {
                    currentCards[i].SetActive(true);
                    currentCards[i].transform.SetParent(deckContainer);
                }
            }
        }


    }
}