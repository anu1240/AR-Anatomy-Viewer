using UnityEngine;
using UnityEngine.UI;

// ─────────────────────────────────────────────────────────────
//  RegionInfo.cs  — FINAL v4
//  AR Anatomy Viewer — Complete Anatomy Information System
//  Brain | Heart | Skull
//  Developed by: Anubhav Khetan (RA2311003011525)
//               Avinash Aaryan  (RA2311003011523)
//  SRMIST — Dept. of Computer Science & Engineering
// ─────────────────────────────────────────────────────────────

public class RegionInfo : MonoBehaviour
{
    // ── UI References (assign in Unity Inspector) ─────────────
    public GameObject infoPanel;
    public Text       regionNameText;   // Large title
    public Text       regionLatinText;  // Subtitle / category
    public Text       regionDescText;   // Full description
    public Button     closeButton;

    private bool isPanelVisible = false;

    void Start()
    {
        if (infoPanel   != null) infoPanel.SetActive(false);
        if (closeButton != null) closeButton.onClick.AddListener(HidePanel);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            DetectTouch(Input.GetTouch(0).position);

        if (Input.GetMouseButtonDown(0))
            DetectTouch(Input.mousePosition);
    }

    void DetectTouch(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
            ShowInfoForPart(hit.collider.gameObject.name);
        else
            if (isPanelVisible) HidePanel();
    }

    void ShowInfoForPart(string partName)
    {
        string title     = "";
        string organType = "";
        string desc      = "";

        switch (partName)
        {
            // ─────────────────────────────────────────────────
            //  BRAIN — FINAL confirmed mapping (front view)
            //
            //  brain 1       → root/parent of whole brain model
            //  Brain_Part_02 → small round bulb at back-bottom = Cerebellum
            //  Brain_Part_04 → large main right hemisphere     = Right Cerebral Hemisphere
            //  Brain_Part_05 → elongated piece hanging below   = Brain Stem
            //  Brain_Part_06 → top/left piece sitting above    = Left Cerebral Hemisphere
            // ─────────────────────────────────────────────────

            case "brain 1":
                title     = "Human Brain";
                organType = "Central Nervous System (Encephalon)";
                desc      = "The most complex organ in the human body, weighing approximately 1.4 kg. Divided into left and right hemispheres, each controlling the opposite side of the body. Consumes 20% of the body's total energy despite being only 2% of body weight. Contains approximately 86 billion neurons.";
                break;

            case "Brain_Part_02":
                title     = "Cerebellum";
                organType = "Hindbrain — Little Brain (Cerebellum)";
                desc      = "Located at the lower back of the brain as a small rounded lobe. Coordinates voluntary movements, maintains balance and posture, and fine-tunes motor skills.\n\nKey facts:\n- Contains over 69 billion neurons — more than the rest of the brain combined\n- Only 10% of brain volume but handles precision movement\n- Damage causes loss of coordination and balance (ataxia)";
                break;

            case "Brain_Part_04":
                title     = "Right Cerebral Hemisphere";
                organType = "Forebrain — Right Half (Hemisphaerium Dextrum)";
                desc      = "The right half of the cerebrum — the largest part of the brain. Controls the LEFT side of the body due to neural crossover.\n\nSpecialises in:\n- Creativity and artistic thinking\n- Spatial reasoning and navigation\n- Music and facial recognition\n- Holistic and intuitive thinking\n\nContains the right frontal, parietal, temporal, and occipital lobes.";
                break;

            case "Brain_Part_05":
                title     = "Brain Stem";
                organType = "Hindbrain — Vital Control Centre (Truncus Cerebri)";
                desc      = "The elongated stalk connecting the brain to the spinal cord. Controls all vital involuntary functions that keep you alive — you cannot consciously control these.\n\nThree sections:\n- Midbrain: eye movement, auditory reflexes\n- Pons: breathing rhythm, facial movement\n- Medulla Oblongata: heart rate, blood pressure, swallowing\n\nWithout the brain stem, survival is impossible.";
                break;

            case "Brain_Part_06":
                title     = "Left Cerebral Hemisphere";
                organType = "Forebrain — Left Half (Hemisphaerium Sinistrum)";
                desc      = "The left half of the cerebrum. Controls the RIGHT side of the body. In most people this is the dominant hemisphere.\n\nSpecialises in:\n- Language and speech (Broca's Area)\n- Language comprehension (Wernicke's Area)\n- Logic, mathematics, and analysis\n- Sequential and detail-oriented thinking\n\nDamage to the left hemisphere often results in speech or language difficulties (aphasia).";
                break;

            // ─────────────────────────────────────────────────
            //  HEART
            // ─────────────────────────────────────────────────

            case "heart":
                title     = "Human Heart";
                organType = "Cardiovascular System (Cor)";
                desc      = "A muscular organ about the size of a fist, located slightly left of centre in the chest. Pumps approximately 5 litres of blood per minute, beating around 100,000 times per day.\n\nFour chambers:\n- Right Atrium: receives deoxygenated blood from the body\n- Right Ventricle: pumps it to the lungs\n- Left Atrium: receives oxygenated blood from the lungs\n- Left Ventricle: pumps oxygenated blood to the entire body (strongest chamber)";
                break;

            // ─────────────────────────────────────────────────
            //  SKULL
            // ─────────────────────────────────────────────────

            case "12140_Skull_v3":
                title     = "Cranium";
                organType = "Skull — Braincase (Calvaria)";
                desc      = "The upper dome-shaped part of the skull that encloses and protects the brain. Made up of eight fused bones: frontal, two parietal, two temporal, occipital, sphenoid, and ethmoid.\n\nThe visible lines on the surface are called sutures — the joints where skull bones fused together during childhood development.";
                break;

            case "12140_Skull_v3_jaw":
                title     = "Mandible (Lower Jaw)";
                organType = "Skull — Only Movable Bone (Mandibula)";
                desc      = "The lower jaw bone — the only movable bone in the entire skull. Holds the lower set of 16 teeth and plays a critical role in chewing (mastication), speech production, and swallowing.\n\nConnects to the skull at the temporomandibular joint (TMJ), just in front of each ear. TMJ disorders are a common cause of jaw pain and headaches.";
                break;

            case "12140_Skull_v3_teeth":
                title     = "Upper Teeth (Maxillary)";
                organType = "Skull — Fixed in Upper Jaw (Dentes Maxillares)";
                desc      = "The upper set of 16 adult teeth anchored in the maxilla (upper jaw bone).\n\nTypes:\n- 4 Incisors: cutting food\n- 2 Canines: tearing food\n- 4 Premolars: crushing food\n- 4 Molars: grinding food\n\nThe upper teeth stay fixed while the lower jaw moves up to meet them during biting and chewing.";
                break;

            case "12140_Skull_v3_teeth_lower":
                title     = "Lower Teeth (Mandibular)";
                organType = "Skull — Fixed in Lower Jaw (Dentes Mandibulares)";
                desc      = "The lower set of 16 adult teeth anchored in the mandible (lower jaw). Mirrors the upper set in tooth arrangement.\n\nWisdom teeth (third molars) are the last to erupt, typically between ages 17 and 25. They are often removed when there is insufficient space in the jaw, as they can cause crowding or infection.";
                break;

            // ─────────────────────────────────────────────────
            //  DEFAULT
            // ─────────────────────────────────────────────────
            default:
                title     = "Anatomy Structure";
                organType = "AR Anatomy Viewer — SRMIST 2026";
                desc      = "Part of the human anatomical model. This AR Anatomy Viewer uses marker-based augmented reality with Unity 2019 and Vuforia Engine.\n\nDeveloped by:\nAnubhav Khetan (RA2311003011525)\nAvinash Aaryan (RA2311003011523)\nSRMIST — Dept. of CSE";
                break;
        }

        ShowPanel(title, organType, desc);
    }

    void ShowPanel(string title, string organType, string desc)
    {
        if (infoPanel == null) return;
        if (regionNameText  != null) regionNameText.text  = title;
        if (regionLatinText != null) regionLatinText.text = organType;
        if (regionDescText  != null) regionDescText.text  = desc;
        infoPanel.SetActive(true);
        isPanelVisible = true;
    }

    public void HidePanel()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
        isPanelVisible = false;
    }
}
