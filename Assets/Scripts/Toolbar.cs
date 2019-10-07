using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { private set; get; }

    public GameObject toolbarCanvas;
    public Outline axe;
    public Outline pickaxe;
    
    private Tool _activeTool = Tool.None;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Player.Instance.axe.IsActive())
        {
            return;
        }
        
        if (Player.Instance.HasAxe() && Input.GetKeyDown(KeyCode.Alpha1))
        {
            _activeTool = Tool.Axe;
            SwitchTool();
            return;
        }
        
        if (Player.Instance.HasPickaxe() && Input.GetKeyDown(KeyCode.Alpha2))
        {
            _activeTool = Tool.Pickaxe;
            SwitchTool();
            return;
        }
    }
    
    public Tool GetActiveTool() => _activeTool;

    public void SetActiveTool(Tool tool)
    {
        _activeTool = tool;
        SwitchTool();
    }

    private void SwitchTool()
    {
        if (Player.Instance.HasAxe() && Player.Instance.HasPickaxe())
        {
            toolbarCanvas.SetActive(true);
        }

        Player.Instance.axe.gameObject.SetActive(false);
        Player.Instance.pickaxe.gameObject.SetActive(false);
        
        axe.effectColor = new Color(0, 0, 0, 0);
        pickaxe.effectColor = new Color(0, 0, 0, 0);
        
        switch (_activeTool)
        {
            case Tool.None:
                break;
            case Tool.Axe:
                Player.Instance.axe.gameObject.SetActive(true);
                axe.effectColor = new Color(0.70f,0.305f, 0, 1f);
                break;
            case Tool.Pickaxe:
                Player.Instance.pickaxe.gameObject.SetActive(true);
                pickaxe.effectColor = new Color(0.70f,0.305f, 0, 1f);
                break;
            default:
                Debug.LogError("Tool not implemented!");
                break;
        }
    }
}

public enum Tool
{
    None,
    Axe,
    Pickaxe
}