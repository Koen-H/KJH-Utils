using KJH.Utils.GUINS.MenuNS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KJH.Utils.Samples.GUI.MenuNS
{
    public class SampleMenu : Menu<SampleMenuCell, SampleMenuCellData>
    {
        [Header("Sample Menu Settings")]
        [SerializeField] private int sampleCellCount = 5;
        [SerializeField] private bool autoSelectFirstItem = false;

        private int populationIndex = 0;

        protected void Start()
        {
            PopulateMenu();

            if (autoSelectFirstItem)
            {
                MenuCell<SampleMenuCellData> firstCell = GetMenuCellAtIndex(0);
                EventSystem.current.SetSelectedGameObject(firstCell.gameObject);
            }

            OnMenuCellSelected += Menu_OnMenuCellSelected;
            OnMenuCellClicked += Menu_OnMenuCellClicked;
            OnMenuCellBeginDrag += Menu_OnMenuCellBeginDrag;
            OnMenuCellDragged += Menu_OnMenuCellDrag;
            OnMenuCellEndDrag += Menu_OnMenuCellEndDrag;
        }

        private void Menu_OnMenuCellSelected(int menuCellIndex)
        {
            SampleMenuCell sampleMenuCell = GetMenuCellAtIndex(menuCellIndex);
            SampleMenuCellData cellData = GetMenuCellDataAtIndex(menuCellIndex);

            Debug.Log($"Menu cell selected: {menuCellIndex}, {cellData.Title} ", sampleMenuCell);

            SampleMenuCellData newCellData = new SampleMenuCellData($"{populationIndex++}");
            SetDataAtIndex(newCellData, menuCellIndex);

        }

        private void Menu_OnMenuCellClicked(int menuCellIndex)
        {
            SampleMenuCell sampleMenuCell = GetMenuCellAtIndex(menuCellIndex);
            SampleMenuCellData cellData = GetMenuCellDataAtIndex(menuCellIndex);
            Debug.Log($"Menu cell clicked: {menuCellIndex}, {cellData.Title} ", sampleMenuCell);

            SampleMenuCellData newCellData = new SampleMenuCellData($"{populationIndex++}");
            SetDataAtIndex(newCellData, menuCellIndex);
        }

        private void Menu_OnMenuCellBeginDrag(int menuCellIndex)
        {
            SampleMenuCell sampleMenuCell = GetMenuCellAtIndex(menuCellIndex);
            SampleMenuCellData cellData = GetMenuCellDataAtIndex(menuCellIndex);
            Debug.Log($"Menu cell begin drag: {menuCellIndex}, {cellData.Title} ", sampleMenuCell);

            SampleMenuCellData newCellData = new SampleMenuCellData($"{populationIndex++}");
            SetDataAtIndex(newCellData, menuCellIndex);
        }

        private void Menu_OnMenuCellDrag(int menuCellIndex)
        {
            SampleMenuCell sampleMenuCell = GetMenuCellAtIndex(menuCellIndex);
            SampleMenuCellData cellData = GetMenuCellDataAtIndex(menuCellIndex);
            Debug.Log($"Menu cell drag: {menuCellIndex}, {cellData.Title} ", sampleMenuCell);

            SampleMenuCellData newCellData = new SampleMenuCellData($"{populationIndex++}");
            SetDataAtIndex(newCellData, menuCellIndex);
        }

        private void Menu_OnMenuCellEndDrag(int menuCellIndex)
        {
            SampleMenuCell sampleMenuCell = GetMenuCellAtIndex(menuCellIndex);
            SampleMenuCellData cellData = GetMenuCellDataAtIndex(menuCellIndex);
            Debug.Log($"Menu cell end drag: {menuCellIndex}, {cellData.Title} ", sampleMenuCell);

            SampleMenuCellData newCellData = new SampleMenuCellData($"{populationIndex++}");
            SetDataAtIndex(newCellData, menuCellIndex);
        }

        private void PopulateMenu()
        {
            List<SampleMenuCellData> cellDataList = new List<SampleMenuCellData>();

            for (int i = 0; i < sampleCellCount; i++)
            {
                string title = $"{i}";
                SampleMenuCellData cellData = new SampleMenuCellData(title);
                cellDataList.Add(cellData);
            }
            populationIndex = sampleCellCount;
            SetMenuData(cellDataList);
        }
    }
}
