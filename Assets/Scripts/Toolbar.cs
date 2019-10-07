using System;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { private set; get; }

    public GameObject toolbarCanvas;
    public Outline axe;
    public Outline pickaxe;
    public Color activeColor;

    private float _scrollTimer;

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

        if (!Player.Instance.HasPickaxe() || !Player.Instance.HasAxe())
        {
            return;
        }

        _scrollTimer -= Time.deltaTime;
        if (Math.Abs(Input.GetAxis("Mouse ScrollWheel")) < 0.001f || _scrollTimer > 0)
        {
            return;
        }

        _scrollTimer = .25f;
        _activeTool = _activeTool == Tool.Axe ? Tool.Pickaxe : Tool.Axe;
        SwitchTool();
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
        axe.GetComponent<Image>().color = Color.white;
        pickaxe.GetComponent<Image>().color = Color.white;

        switch (_activeTool)
        {
            case Tool.None:
                break;
            case Tool.Axe:
                Player.Instance.axe.gameObject.SetActive(true);
                axe.effectColor = new Color(0.70f, 0.305f, 0, 1f);
                axe.GetComponent<Image>().color = activeColor;
                break;
            case Tool.Pickaxe:
                Player.Instance.pickaxe.gameObject.SetActive(true);
                pickaxe.effectColor = new Color(0.70f, 0.305f, 0, 1f);
                pickaxe.GetComponent<Image>().color = activeColor;
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