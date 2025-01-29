using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KJH.Utils.GUINS.MenuNS
{
    public abstract class Menu<T1, T2> : MonoBehaviour where T1 : MenuCell<T2> where T2 : MenuCellData
    {
        private class MenuCellButton : IDisposable
        {
            private Button button;
            private int cellIndex;

            public event Action<int> OnCellButtonClicked;
            //public event Action<int> OnCellButtonHovered;
            //public event Action<int> OnCellButtonUnhovered;

            public MenuCellButton(Button button, int cellIndex)
            {
                this.cellIndex = cellIndex;

                button.onClick.AddListener(Button_OnClicked);
            }

            private void Button_OnClicked()
            {
                OnCellButtonClicked?.Invoke(cellIndex);
            }

            public void Dispose()
            {
                if (button == null)
                    return;

                button.onClick.RemoveListener(Button_OnClicked);
            }
        }

        private class MenuCellDrag : IDisposable
        {
            private DragController dragController;
            private int cellIndex;

            public event Action<int> OnCellBeginDrag;
            public event Action<int> OnCellDragged;
            public event Action<int> OnCellEndDrag;

            public MenuCellDrag(DragController dragController, int cellIndex)
            {
                this.cellIndex = cellIndex;

                dragController.OnBeginDragAction += DragController_OnBeginDragAction;
                dragController.OnDragAction += DragController_OnDrag;
                dragController.OnEndDragAction += DragController_OnEndDragAction;
            }

            private void DragController_OnBeginDragAction()
            {
                OnCellBeginDrag?.Invoke(cellIndex);
            }

            private void DragController_OnDrag()
            {
                OnCellDragged?.Invoke(cellIndex);
            }

            private void DragController_OnEndDragAction()
            {
                OnCellEndDrag?.Invoke(cellIndex);
            }

            public void Dispose()
            {
                if (dragController == null)
                    return;

                dragController.OnBeginDragAction -= DragController_OnBeginDragAction;
                dragController.OnDragAction -= DragController_OnDrag;
                dragController.OnEndDragAction -= DragController_OnEndDragAction;
            }
        }

        private class MenuCellNavigation : IDisposable
        {
            private NavigationItem navigationItem;
            private int cellIndex;

            public event Action<int> OnCellSelected;

            public MenuCellNavigation(NavigationItem navigationItem, int cellIndex)
            {
                this.cellIndex = cellIndex;
                this.navigationItem = navigationItem;

                navigationItem.OnCellNavigationItemSelected += NavigationItem_OnNavigationItemSelected;
            }

            private void NavigationItem_OnNavigationItemSelected()
            {
                OnCellSelected?.Invoke(cellIndex);
            }

            public void Dispose()
            {
                if (navigationItem == null)
                    return;

                navigationItem.OnCellNavigationItemSelected -= NavigationItem_OnNavigationItemSelected;
            }
        }

        public event Action<int> OnMenuCellSelected;

        public event Action<int> OnMenuCellClicked;

        public event Action<int> OnMenuCellBeginDrag;
        public event Action<int> OnMenuCellDragged;
        public event Action<int> OnMenuCellEndDrag;

        [Header("Menu Settings")]
        [SerializeField] private bool buttonSupport = false;
        [SerializeField] private bool navigationSupport = false;
        [SerializeField] private bool dragSupport = false;

        private List<T1> activeMenuCells = new List<T1>();
        private List<T1> menuCells = new List<T1>();
        private List<T2> menuData = new List<T2>();

        private List<MenuCellNavigation> menuCellNavigations = new List<MenuCellNavigation>();
        private List<MenuCellButton> menuCellButtons = new List<MenuCellButton>();
        private List<MenuCellDrag> menuCellDrags = new List<MenuCellDrag>();

        [Header("Menu Components")]
        [SerializeField] private Transform menuCellContainer = default;
        [SerializeField] private T1 menuCellPrefab = default;

        private void OnDestroy()
        {
            foreach (MenuCellNavigation menuCellNavigation in menuCellNavigations)
            {
                RemoveNavigationSubscriptions(menuCellNavigation);
                menuCellNavigation.Dispose();
            }

            foreach (MenuCellButton menuCellButton in menuCellButtons)
            {
                RemoveButtonSubscriptions(menuCellButton);
                menuCellButton.Dispose();
            }

            foreach (MenuCellDrag menuCellDrag in menuCellDrags)
            {
                RemoveDragSubscriptions(menuCellDrag);
                menuCellDrag.Dispose();
            }
        }

        private void MenuCellNavigation_OnCellSelected(int cellIndex)
        {
            OnMenuCellSelected?.Invoke(cellIndex);
        }

        private void MenuCellButton_OnClicked(int clickedCellIndex)
        {
            OnMenuCellClicked?.Invoke(clickedCellIndex);
        }

        private void MenuCellDrag_OnMenuCellBeginDrag(int cellIndex)
        {
            OnMenuCellBeginDrag?.Invoke(cellIndex);
        }

        private void MenuCellDrag_OnMenuCellDragged(int cellIndex)
        {
            OnMenuCellDragged?.Invoke(cellIndex);
        }

        private void MenuCellDrag_OnMenuCellEndDrag(int cellIndex)
        {
            OnMenuCellEndDrag?.Invoke(cellIndex);
        }

        public void SetMenuData(List<T2> newData)
        {
            menuData = newData;

            RefreshMenu();
        }

        private void RefreshMenu()
        {
            for (int i = 0; i < menuData.Count; i++)
            {
                if (i < menuCells.Count)
                {
                    if (menuCells[i].Data != menuData[i])
                        menuCells[i].SetData(menuData[i]);

                    if (!menuCells[i].gameObject.activeSelf)
                    {
                        menuCells[i].gameObject.SetActive(true);
                        activeMenuCells.Add(menuCells[i]);
                    }
                }
                else
                {
                    CreateMenuCell(menuData[i], i);
                }
            }

            for (int i = menuData.Count; i < menuCells.Count; i++)
            {
                menuCells[i].gameObject.SetActive(false);
                activeMenuCells.Remove(menuCells[i]);
            }
        }

        public void SetDataAtIndex(T2 newData, int index)
        {
            menuData[index] = newData;
            menuCells[index].SetData(newData);
        }

        public void AddData(params T2[] newData)
        {
            int newItems = newData.Length;
            int oldLastIndex = menuData.Count - 1;

            menuData.AddRange(newData);

            int newLastIndex = menuData.Count - 1;

            for (int i = 0; i < newItems; i++)
            {
                int menuDataIndex = oldLastIndex + i + 1;
                if (menuCells.Count < menuData.Count)
                    CreateMenuCell(newData[i], menuDataIndex);
                else
                    SetDataAtIndex(newData[i], menuDataIndex);
            }

            bool needsMenuRefresh = activeMenuCells.Count < menuData.Count;
            if (needsMenuRefresh)
                RefreshMenu();
        }

        public void RemoveData(T2 dataToRemove)
        {
            menuData.Remove(dataToRemove);
            RefreshMenu();
        }

        public void RemoveDataAtIndex(int index)
        {
            menuData.RemoveAt(index);
            RefreshMenu();
        }

        private void CreateMenuCell(T2 menuCellData, int index)
        {
            T1 newCell = Instantiate(menuCellPrefab, menuCellContainer);
            newCell.SetData(menuCellData);

            if (buttonSupport)
            {
                CreateMenuCellButton(newCell, index);
            }

            if (dragSupport)
                CreateMenuCellDrag(newCell, index);

            if (navigationSupport)
            {
                CreateMenuCellNavigation(newCell, index);
            }

            menuCells.Add(newCell);
            activeMenuCells.Add(newCell);
        }

        private void CreateMenuCellNavigation(T1 newCell, int index)
        {
            NavigationItem navigationItem = newCell.GetComponent<NavigationItem>();
            if (navigationItem == null)
                throw new Exception($"NavigationItem component not found on menu cell prefab with name {menuCellPrefab.name}!");

            MenuCellNavigation newNavigation = new MenuCellNavigation(navigationItem, index);
            newNavigation.OnCellSelected += MenuCellNavigation_OnCellSelected;

            menuCellNavigations.Add(newNavigation);
        }

        private void CreateMenuCellButton(T1 newCell, int index)
        {
            Button button = newCell.GetComponent<Button>();
            if (button == null)
                throw new Exception($"Button component not found on menu cell prefab with name {menuCellPrefab.name}!");

            MenuCellButton newButton = new MenuCellButton(button, index);
            newButton.OnCellButtonClicked += MenuCellButton_OnClicked;

            menuCellButtons.Add(newButton);
        }

        private void CreateMenuCellDrag(T1 newCell, int index)
        {
            DragController dragController = newCell.GetComponent<DragController>();
            if (dragController == null)
                throw new Exception($"DragController component not found on menu cell prefab with name {menuCellPrefab.name}!");

            MenuCellDrag newDrag = new MenuCellDrag(dragController, index);
            newDrag.OnCellBeginDrag += MenuCellDrag_OnMenuCellBeginDrag;
            newDrag.OnCellDragged += MenuCellDrag_OnMenuCellDragged;
            newDrag.OnCellEndDrag += MenuCellDrag_OnMenuCellEndDrag;

            menuCellDrags.Add(newDrag);
        }

        private void RemoveNavigationSubscriptions(MenuCellNavigation menuCellNavigation)
        {
            menuCellNavigation.OnCellSelected -= MenuCellNavigation_OnCellSelected;
        }

        private void RemoveButtonSubscriptions(MenuCellButton menuCellButton)
        {
            menuCellButton.OnCellButtonClicked -= MenuCellButton_OnClicked;
        }

        private void RemoveDragSubscriptions(MenuCellDrag menuCellDrag)
        {
            menuCellDrag.OnCellBeginDrag -= MenuCellDrag_OnMenuCellBeginDrag;
            menuCellDrag.OnCellDragged -= MenuCellDrag_OnMenuCellDragged;
            menuCellDrag.OnCellEndDrag -= MenuCellDrag_OnMenuCellEndDrag;
        }

        public T1 GetMenuCellAtIndex(int index)
        {
            return menuCells[index];
        }

        public T2 GetMenuCellDataAtIndex(int index)
        {
            return menuData[index];
        }

        public IEnumerable<T1> GetActiveMenuCells()
        {
            return activeMenuCells;
        }

        public IEnumerable<T2> GetMenuData()
        {
            return menuData;
        }
    }
}
