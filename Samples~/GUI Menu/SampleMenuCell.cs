using KJH.Utils.GUINS.MenuNS;
using TMPro;
using UnityEngine;

namespace KJH.Utils.Samples.GUI.MenuNS
{
    public class SampleMenuCell : MenuCell<SampleMenuCellData>
    {
        [SerializeField] private TextMeshProUGUI text = default;

        protected override void Refresh()
        {
            text.text = Data.Title;
        }
    }

    public class SampleMenuCellData : MenuCellData
    {
        public string Title { get; private set; }

        public SampleMenuCellData(string title)
        {
            Title = title;
        }
    }
}
