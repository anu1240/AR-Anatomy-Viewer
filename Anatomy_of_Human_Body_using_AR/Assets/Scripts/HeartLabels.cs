using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// ─────────────────────────────────────────────────────────────
//  HeartLabels.cs
//  AR Anatomy Viewer — Floating Heart Region Labels
//  Developed by: Anubhav Khetan (RA2311003011525)
//               Avinash Aaryan  (RA2311003011523)
//  SRMIST — Dept. of Computer Science & Engineering
// ─────────────────────────────────────────────────────────────

public class HeartLabels : MonoBehaviour
{
    // ── Settings ──────────────────────────────────────────────
    public bool  showLabelsOnStart = true;
    public float labelFadeSpeed    = 2f;

    // ── Label GameObjects (assign in Inspector) ───────────────
    // Create these as child Canvas/Text objects under the heart
    public GameObject labelLeftVentricle;
    public GameObject labelRightVentricle;
    public GameObject labelLeftAtrium;
    public GameObject labelRightAtrium;
    public GameObject labelAorta;
    public GameObject labelPulmonaryArtery;

    // ── Info Panel (same as RegionInfo) ───────────────────────
    public GameObject infoPanel;
    public Text       regionNameText;
    public Text       regionLatinText;
    public Text       regionDescText;
    public Button     closeButton;

    private bool isPanelVisible = false;
    private bool labelsVisible  = false;

    // ── Heart Region Data ─────────────────────────────────────
    public struct HeartRegion
    {
        public string name;
        public string latin;
        public string desc;
        public HeartRegion(string n, string l, string d) { name=n; latin=l; desc=d; }
    }

    HeartRegion[] heartRegions = new HeartRegion[]
    {
        new HeartRegion(
            "Left Ventricle",
            "Ventriculus Sinister",
            "The most powerful chamber. Pumps oxygenated blood through the aorta to the entire body. Has the thickest muscular walls of all four chambers."
        ),
        new HeartRegion(
            "Right Ventricle",
            "Ventriculus Dexter",
            "Pumps deoxygenated blood through the pulmonary artery to the lungs for oxygenation. Has thinner walls than the left ventricle."
        ),
        new HeartRegion(
            "Left Atrium",
            "Atrium Sinistrum",
            "Receives oxygenated blood returning from the lungs via the pulmonary veins. Passes it down to the left ventricle through the mitral valve."
        ),
        new HeartRegion(
            "Right Atrium",
            "Atrium Dextrum",
            "Receives deoxygenated blood from the body via the superior and inferior vena cava. Passes it to the right ventricle through the tricuspid valve."
        ),
        new HeartRegion(
            "Aorta",
            "Aorta Ascendens",
            "The largest artery in the body. Carries oxygenated blood from the left ventricle and distributes it to all organs and tissues of the body."
        ),
        new HeartRegion(
            "Pulmonary Artery",
            "Arteria Pulmonalis",
            "Carries deoxygenated blood from the right ventricle to the lungs. Unique — it is the only artery in the body that carries deoxygenated blood."
        ),
    };

    void Start()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(HidePanel);

        // Show or hide labels on start
        SetLabelsVisible(showLabelsOnStart);
    }

    void Update()
    {
        // Always face labels toward camera
        FaceLabelsToCamera();

        // Touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            DetectTouch(Input.GetTouch(0).position);

        // Mouse click for PC testing
        if (Input.GetMouseButtonDown(0))
            DetectTouch(Input.mousePosition);
    }

    void FaceLabelsToCamera()
    {
        // Make each label always face the camera (billboard effect)
        GameObject[] labels = {
            labelLeftVentricle, labelRightVentricle,
            labelLeftAtrium,    labelRightAtrium,
            labelAorta,         labelPulmonaryArtery
        };

        foreach (GameObject label in labels)
        {
            if (label != null && Camera.main != null)
            {
                label.transform.LookAt(Camera.main.transform);
                label.transform.Rotate(0, 180, 0);
            }
        }
    }

    void DetectTouch(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            string hitName = hit.collider.gameObject.name.ToLower();

            // Check which label area was tapped
            if (hitName.Contains("leftventricle") || hitName.Contains("left_ventricle"))
                ShowRegion(heartRegions[0]);
            else if (hitName.Contains("rightventricle") || hitName.Contains("right_ventricle"))
                ShowRegion(heartRegions[1]);
            else if (hitName.Contains("leftatrium") || hitName.Contains("left_atrium"))
                ShowRegion(heartRegions[2]);
            else if (hitName.Contains("rightatrium") || hitName.Contains("right_atrium"))
                ShowRegion(heartRegions[3]);
            else if (hitName.Contains("aorta"))
                ShowRegion(heartRegions[4]);
            else if (hitName.Contains("pulmonary"))
                ShowRegion(heartRegions[5]);
            else if (hitName.Contains("heart") || hitName.Contains("circle"))
            {
                // Tapped main heart body — show general info
                ShowPanel("Human Heart", "Cor",
                    "A muscular organ beating ~100,000 times/day. Has 4 chambers: Left & Right Atria (upper) and Left & Right Ventricles (lower). Tap the floating labels to learn about each part.");
            }
        }
        else
        {
            if (isPanelVisible) HidePanel();
        }
    }

    public void ShowRegion(HeartRegion region)
    {
        ShowPanel(region.name, region.latin, region.desc);
    }

    void ShowPanel(string title, string latin, string desc)
    {
        if (infoPanel == null) return;
        if (regionNameText  != null) regionNameText.text  = title;
        if (regionLatinText != null) regionLatinText.text = "(" + latin + ")";
        if (regionDescText  != null) regionDescText.text  = desc;
        infoPanel.SetActive(true);
        isPanelVisible = true;
    }

    public void HidePanel()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
        isPanelVisible = false;
    }

    public void ToggleLabels()
    {
        labelsVisible = !labelsVisible;
        SetLabelsVisible(labelsVisible);
    }

    void SetLabelsVisible(bool visible)
    {
        labelsVisible = visible;
        GameObject[] labels = {
            labelLeftVentricle, labelRightVentricle,
            labelLeftAtrium,    labelRightAtrium,
            labelAorta,         labelPulmonaryArtery
        };
        foreach (GameObject label in labels)
            if (label != null) label.SetActive(visible);
    }
}
