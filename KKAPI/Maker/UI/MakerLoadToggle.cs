﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace KKAPI.Maker.UI
{
    /// <summary>
    /// Adds a toggle to the bottom of the character card load window in character maker.
    /// Use to allow user to not load data related to your mod.
    /// Use with <see cref="AddLoadToggle"/>
    /// </summary>
    public class MakerLoadToggle : BaseEditableGuiEntry<bool>
    {
        private const int TotalWidth = 380 + 292;

        private static readonly List<MakerLoadToggle> Toggles = new List<MakerLoadToggle>();
        private static Transform _baseToggle;
        private static GameObject _root;

        private static int _createdCount;

        public MakerLoadToggle(string text, bool initialValue = true) : base(null, initialValue, null)
        {
            Text = text;
        }

        public string Text { get; }
        public static bool AnyEnabled => Toggles.Any(x => x.Value);
        internal static Button LoadButton { get; private set; }

        internal static void CreateCustomToggles()
        {
            foreach (var toggle in Toggles)
                toggle.CreateControl(_root.transform);
        }

        protected override GameObject OnCreateControl(Transform loadBoxTransform)
        {
            var copy = Object.Instantiate(_baseToggle, _root.transform);
            copy.name = "tglItem" + GuiApiNameAppendix;

            var tgl = copy.GetComponentInChildren<Toggle>();
            tgl.onValueChanged.AddListener(SetNewValue);
            BufferedValueChanged.Subscribe(b => tgl.isOn = b);
            tgl.image.raycastTarget = true;
            tgl.graphic.raycastTarget = true;

            var txt = copy.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = Text;

            var singleItemWidth = TotalWidth / Toggles.Count;

            var rt = copy.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(-380 + singleItemWidth * _createdCount, 26);
            rt.offsetMax = new Vector2(rt.offsetMin.x + singleItemWidth, rt.offsetMin.y + 26);

            copy.gameObject.SetActive(true);
            _createdCount++;

            return copy.gameObject;
        }

        protected internal override void Initialize() { }

        internal static MakerLoadToggle AddLoadToggle(MakerLoadToggle toggle)
        {
            Toggles.Add(toggle);
            return toggle;
        }

        internal static void Reset()
        {
            foreach (var toggle in Toggles)
                toggle.Dispose();
            Toggles.Clear();
            _createdCount = 0;
            LoadButton = null;
        }

        internal static void Setup()
        {
            Reset();

            _root = GetRootObject();
            var baseToggles = _root.transform.Cast<Transform>()
                .Where(x => x.name.StartsWith("tglItem"))
                .Select(x => x.GetComponent<RectTransform>())
                .ToList();

            var singleWidth = TotalWidth / baseToggles.Count;
            for (var index = 0; index < baseToggles.Count; index++)
            {
                var baseToggle = baseToggles[index];
                baseToggle.localPosition = new Vector3(-380 + singleWidth * index, 52, 0);
                baseToggle.offsetMax = new Vector2(baseToggle.offsetMin.x + singleWidth, baseToggle.offsetMin.y + 26);
            }

            _baseToggle = _root.transform.Find("tglItem01");

            var allon = _root.transform.Find("btnAllOn");
            allon.GetComponentInChildren<Button>().onClick.AddListener(OnAllOn);
            var alloff = _root.transform.Find("btnAllOff");
            alloff.GetComponentInChildren<Button>().onClick.AddListener(OnAllOff);

            LoadButton = _root.transform.parent.Find("btnLoad").GetComponent<Button>();
        }

        private static GameObject GetRootObject()
        {
            return GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/06_SystemTop/charaFileControl/charaFileWindow/WinRect/CharaLoad/Select");
        }

        private static void OnAllOff()
        {
            foreach (var toggle in Toggles)
            {
                if (toggle != null)
                    toggle.Value = false;
            }
        }

        private static void OnAllOn()
        {
            foreach (var toggle in Toggles)
            {
                if (toggle != null)
                    toggle.Value = true;
            }
        }
    }
}