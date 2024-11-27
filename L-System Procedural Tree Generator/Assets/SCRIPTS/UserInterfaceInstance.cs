using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceInstance : MonoBehaviour
{
    [SerializeField] private TMP_InputField _axiomInput;
    
    [SerializeField] private TMP_InputField _angleInput;
    [SerializeField] private TMP_InputField _initialThicknessInput;
    [SerializeField] private TMP_Text _dynamicThicknessText;
    [SerializeField] private TMP_InputField _contractionRatioInput;

    [SerializeField] private TMP_InputField _rule1PredeccessorInput;
    [SerializeField] private TMP_InputField _rule1SuccessorInput;
    [SerializeField] private TMP_InputField _rule2PredeccessorInput;
    [SerializeField] private TMP_InputField _rule2SuccessorInput;
    [SerializeField] private Slider _iterationsInput;
    [SerializeField] private TMP_Text _iterationsText;
    
    [SerializeField] private TMP_Text _dynamicAngle3DText;
    [SerializeField] private TMP_Text _dynamicAngle3DChangeText;
    
    [SerializeField] private Toggle _is3DToggle;
    [SerializeField] private Toggle _smoothBranchingToggle;
    [SerializeField] private Toggle _chaoticBranchingToggle;
    [SerializeField] private Toggle _twistedHorrorToggle;

    public GameObject cameraPosition1;
    public GameObject cameraPosition2;
    private int _verticalCameraOffset1 = 300;
    private int _verticalCameraOffset2 = 1250;
    public GameObject LSystemGeneratorGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        PickPresetC();
        RefreshUI();
        CallGenerateLSystem();
    }

    public void RefreshUI(){
        _axiomInput.text = UIConfig.Axiom;
        _angleInput.text = UIConfig.Angle.ToString();
        _initialThicknessInput.text = UIConfig.InitialThickness.ToString();
        _contractionRatioInput.text = UIConfig.ContractionRatio.ToString();

        _iterationsInput.value = UIConfig.Iterations;
        _iterationsText.text = "Iteration " + UIConfig.Iterations.ToString();
        RefreshToggles();
        UIConfig.Rules.Clear();
        if(!string.IsNullOrEmpty(_rule1PredeccessorInput.text) && !string.IsNullOrEmpty(_rule1SuccessorInput.text))
        {
            
            UIConfig.Rules.Add(_rule1PredeccessorInput.text[0], _rule1SuccessorInput.text);
        }
        if(!string.IsNullOrEmpty(_rule2PredeccessorInput.text) && !string.IsNullOrEmpty(_rule2SuccessorInput.text))
        {
            UIConfig.Rules.Add(_rule2PredeccessorInput.text[0], _rule2SuccessorInput.text);
        }
        foreach(var rule in UIConfig.Rules)
        {
            Debug.Log("Key: " + rule.Key + " Value: " + rule.Value);
        }
        
    }
    public void OnSmoothBranchingToggleChanged(bool value)
    {
        if(_is3DToggle.isOn)
        {
            if(_smoothBranchingToggle.isOn)
            {
                _chaoticBranchingToggle.isOn = false;
                _twistedHorrorToggle.isOn = false;
            }
            // UIConfig.DoSmoothBranching = value;
            // RefreshUIConfigToggles();
        }else
        {
            _smoothBranchingToggle.isOn = false;
        }
        RefreshUIConfigToggles();
    }
    public void OnChaoticBranchingToggleChanged(bool value)
    {
        if(_is3DToggle.isOn)
        {
            if(_chaoticBranchingToggle.isOn)
            {
                _smoothBranchingToggle.isOn = false;
                _twistedHorrorToggle.isOn = false;
            }
            // UIConfig.DoChaoticBranching = value;
            // RefreshUIConfigToggles();
        }else
        {
            _chaoticBranchingToggle.isOn = false;
        }
        RefreshUIConfigToggles();
    }

    public void OnTwistedHorrorToggleChanged(bool value)
    {
        if(_is3DToggle.isOn)
        {
            if(_twistedHorrorToggle.isOn)
            {
                _smoothBranchingToggle.isOn = false;
                _chaoticBranchingToggle.isOn = false;
            }
            // UIConfig.DoTwistedHorrorBranching = value;
            // RefreshUIConfigToggles();
        }else
        {
            _twistedHorrorToggle.isOn = false;
        }
        RefreshUIConfigToggles();
        
    }
    public void OnIs3DToggleChanged(bool value)
    {
        // UIConfig.Is3D = value;
        if(!_is3DToggle.isOn)
        {
            _smoothBranchingToggle.isOn = false;
            _chaoticBranchingToggle.isOn = false;
            _twistedHorrorToggle.isOn = false;
        }
        else{
            _smoothBranchingToggle.isOn = true;
            _chaoticBranchingToggle.isOn = false;
            _twistedHorrorToggle.isOn = false;
        }
        RefreshUIConfigToggles();
    }
    private void RefreshToggles(){
        _is3DToggle.isOn = UIConfig.Is3D;
        _smoothBranchingToggle.isOn = UIConfig.DoSmoothBranching;
        _chaoticBranchingToggle.isOn = UIConfig.DoChaoticBranching;
        _twistedHorrorToggle.isOn = UIConfig.DoTwistedHorrorBranching;
    }
    private void RefreshUIConfigToggles(){
        UIConfig.Is3D = _is3DToggle.isOn;
        UIConfig.DoSmoothBranching = _smoothBranchingToggle.isOn;
        UIConfig.DoChaoticBranching = _chaoticBranchingToggle.isOn;
        UIConfig.DoTwistedHorrorBranching = _twistedHorrorToggle.isOn;
    }
    public void OnAxiomChanged(string axiom)
    {
        // UIConfig.Axiom = axiom;
        UIConfig.Axiom = _axiomInput.text;

    }

    

    public void OnAngleChanged(string angle)
    {
        // UIConfig.Angle = float.Parse(angle);
        Debug.Log(_angleInput.text);
        UIConfig.Angle = float.Parse(_angleInput.text);
    }

    public void OnInitialThicknessChanged(string initialThickness)
    {
        UIConfig.InitialThickness = float.Parse(_initialThicknessInput.text);
    }

    public void OnContractionRatioChanged(string contractionRatio)
    {
        UIConfig.ContractionRatio = float.Parse(_contractionRatioInput.text);
    }

    public void OnIterationsChanged(float iterations)
    {
        // UIConfig.Iterations = int.Parse(iterations);
        // UIConfig.Iterations = (int)iterations;
        // _iterationsText.text = "Iteration " + iterations.ToString();
        UIConfig.Iterations = (int)_iterationsInput.value;   
        _iterationsText.text = "Iteration " + UIConfig.Iterations.ToString();
    }

    // public void OnRule1PredeccessorChanged(string predeccessor)
    // {
    //     // Add key to rule dictionary if key('predeccesor') doesnt exist
    //     if(!UIConfig.Rules.ContainsKey(_rule1PredeccessorInput.text[0]))
    //     {
    //         UIConfig.Rules.Add(_rule1PredeccessorInput.text[0], null);
    //     }
    //     // else
    //     // {
    //     //     UIConfig.Rules[_rule1PredeccessorInput.text[0]] = _rule1SuccessorInput.text;
    //     // }
    // }
    // public void OnRule1SuccessorChanged(string successor)
    // {
    //     // Add key to rule dictionary if key('predeccesor') doesnt exist
    //     if(!UIConfig.Rules.ContainsKey(_rule1PredeccessorInput.text[0]))
    //     {
    //         UIConfig.Rules.Add(_rule1PredeccessorInput.text[0], _rule1SuccessorInput.text);
    //     }
    //     else // if key exists, update the value
    //     {
    //         UIConfig.Rules[_rule1PredeccessorInput.text[0]] = _rule1SuccessorInput.text;
    //     }
    // }

    // public void IncrementThicknessAndCallGenerateLSystem()
    // {
    //     if(UIConfig.InitialThickness < 10)
    //     {
    //         UIConfig.InitialThickness++;
    //         _initialThicknessInput.text = UIConfig.InitialThickness.ToString();
    //         _dynamicThicknessText.text = "Thickness = "+ UIConfig.InitialThickness.ToString();
    //         CallGenerateLSystem();
    //     }else
    //     {
    //         UIConfig.InitialThickness = 10;
    //         _initialThicknessInput.text = UIConfig.InitialThickness.ToString();
    //         _dynamicThicknessText.text = "Thickness Maximum = "+ UIConfig.InitialThickness.ToString();
    //         CallGenerateLSystem();
    //     }
    //     // UIConfig.InitialThickness++;
    //     // _initialThicknessInput.text = "Thickness = "+ UIConfig.InitialThickness.ToString();
    //     // CallGenerateLSystem();
    // }
    // public void DecrementThicknessAndCallGenerateLSystem()
    // {
    //     if(UIConfig.InitialThickness > 1)
    //     {
    //         UIConfig.InitialThickness--;
    //         _initialThicknessInput.text = UIConfig.InitialThickness.ToString();
    //         _dynamicThicknessText.text = "Thickness = "+ UIConfig.InitialThickness.ToString();
    //         CallGenerateLSystem();
    //     }else
    //     {
    //         UIConfig.InitialThickness = 1;
    //         _initialThicknessInput.text = UIConfig.InitialThickness.ToString();
    //         _dynamicThicknessText.text = "Thickness Minimum = "+ UIConfig.InitialThickness.ToString();
    //         CallGenerateLSystem();
    //     }
    // }

    public void Increment3DAngleOffsetAndCallGenerateLSystem()
    {
        if(UIConfig.Is3D)
        {
            if(UIConfig.RandomAngle3D < 90)
            {
                UIConfig.RandomAngle3D++;
                _dynamicAngle3DText.text = "Angle = "+ UIConfig.RandomAngle3D.ToString();
                CallGenerateLSystem();
            }
        }else{
            _dynamicAngle3DText.text = "Turn on 3D!";
        }
    }
    public void Decrement3DAngleOffsetAndCallGenerateLSystem()
    {
        if(UIConfig.Is3D)
        {
            if(UIConfig.RandomAngle3D > 0)
            {
                UIConfig.RandomAngle3D--;
                _dynamicAngle3DText.text = "Angle = "+ UIConfig.RandomAngle3D.ToString();
                CallGenerateLSystem();
            }
        }else{
            _dynamicAngle3DText.text = "Turn on 3D!";
        }
    }
    public void Increment3DAngleOffsetChangeAndCallGenerateLSystem()
    {
        if(UIConfig.Is3D)
        {
            if(UIConfig.RandomAngle3DChange < 90)
            {
                UIConfig.RandomAngle3DChange++;
                _dynamicAngle3DChangeText.text = "Angle Change = "+ UIConfig.RandomAngle3DChange.ToString();
                CallGenerateLSystem();
            }
        }else{
            _dynamicAngle3DChangeText.text = "Turn on 3D!";
        }
    }
    public void Decrement3DAngleOffsetChangeAndCallGenerateLSystem()
    {
        if(UIConfig.Is3D)
        {
            if(UIConfig.RandomAngle3DChange > 0)
            {
                UIConfig.RandomAngle3DChange--;
                _dynamicAngle3DChangeText.text = "Angle Change = "+ UIConfig.RandomAngle3DChange.ToString();
                CallGenerateLSystem();
            }
        }else{
            _dynamicAngle3DChangeText.text = "Turn on 3D!";
        }
    }
    
    public void IncrementAngleAndCallGenerateLSystem()
    {
        if(UIConfig.Angle < 90)
        {
            UIConfig.Angle++;
            _angleInput.text = UIConfig.Angle.ToString();
            CallGenerateLSystem();
        }
    }
    public void DecrementAngleAndCallGenerateLSystem()
    {
        if(UIConfig.Angle > 0)
        {
            UIConfig.Angle--;
            _angleInput.text = UIConfig.Angle.ToString();
            CallGenerateLSystem();
        }
    }
    public void IncrementIterationsAndCallGenerateLSystem()
    {
        if(UIConfig.Iterations < _iterationsInput.maxValue)
        {
            UIConfig.Iterations++;
            _iterationsInput.value = UIConfig.Iterations;
            _iterationsText.text = "Iteration " + UIConfig.Iterations.ToString();
        }
        CallGenerateLSystem();
    }
    public void DecrementIterationsAndCallGenerateLSystem()
    {
        if(UIConfig.Iterations > 0)
        {
            UIConfig.Iterations--;
            _iterationsInput.value = UIConfig.Iterations;
            _iterationsText.text = "Iteration " + UIConfig.Iterations.ToString();
        }
        CallGenerateLSystem();
    }
    public void CallGenerateLSystem()
    {
        ProcessRules();
        LSystemGenerator lSystemGenerator = LSystemGeneratorGameObject.GetComponent<LSystemGenerator>();
        lSystemGenerator.GenerateLSystem();
    }
    public void ProcessRules()
    {
        UIConfig.Rules.Clear(); // Clear rules from the UIConfig Dictionary so that we dont accidentally add too many
        // If the UI has a rule, add it to the dictionary
        if(_rule1PredeccessorInput.text.Length > 0 && _rule1SuccessorInput.text.Length > 0)
        {
            UIConfig.Rules.Add(_rule1PredeccessorInput.text[0], _rule1SuccessorInput.text);
        }
        // If the second rule has a predeccessor and successor, add it to the dictionary
        if(_rule2PredeccessorInput.text.Length > 0 && _rule2SuccessorInput.text.Length > 0)
        {
            UIConfig.Rules.Add(_rule2PredeccessorInput.text[0], _rule2SuccessorInput.text);
        }
    }
    public void PickPresetA()
    {
        // UIConfig.TreeConfigs[UIConfig.TreeType.A];
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.A].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.A].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.A].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.A].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.A].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.A].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.A].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.A].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.A].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.A].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        
        UIConfig.cameraPosition = cameraPosition1.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset2;
        RefreshUI();

        
    }
    public void PickPresetB()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.B].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.B].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.B].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.B].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.B].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.B].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.B].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.B].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.B].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.B].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition2.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset1;
        RefreshUI();
        
    }
    public void PickPresetC()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.C].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.C].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.C].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.C].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.C].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.C].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.C].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.C].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.C].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.C].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition2.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset1;
        RefreshUI();
        
    }
    public void PickPresetD()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.D].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.D].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.D].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.D].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.D].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.D].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.D].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.D].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.D].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.D].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition1.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset2;
        RefreshUI();

        
    }
    public void PickPresetE()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.E].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.E].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.E].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.E].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.E].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.E].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.E].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.E].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.E].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.E].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition1.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset2;
        RefreshUI();

        
    }
    public void PickPresetF()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.F].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.F].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.F].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.F].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.F].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.F].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.F].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.F].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.F].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.F].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition2.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset1;
        RefreshUI();

        
    }

    public void PickPresetG()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.G].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.G].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.G].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.G].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.G].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.G].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.G].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.G].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.G].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.G].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition2.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset1;
        RefreshUI();

        
    }
    public void PickPresetH()
    {
        UIConfig.Axiom = UIConfig.TreeConfigs[TreeType.H].axiom;
        UIConfig.Iterations = UIConfig.TreeConfigs[TreeType.H].interations;
        UIConfig.Angle = UIConfig.TreeConfigs[TreeType.H].angle;
        UIConfig.Rules = UIConfig.TreeConfigs[TreeType.H].rules;
        UIConfig.InitialThickness = UIConfig.TreeConfigs[TreeType.H].initialThickness;
        UIConfig.ContractionRatio = UIConfig.TreeConfigs[TreeType.H].contractionRatio;

        UIConfig.Is3D = UIConfig.TreeConfigs[TreeType.H].is3d;
        UIConfig.DoSmoothBranching = UIConfig.TreeConfigs[TreeType.H].doSmoothBranching;
        UIConfig.DoChaoticBranching = UIConfig.TreeConfigs[TreeType.H].doHorrorBranching;
        UIConfig.DoTwistedHorrorBranching = UIConfig.TreeConfigs[TreeType.H].DoTwistedHorrorBranching;
        _iterationsInput.maxValue = UIConfig.Iterations;
        //Add rules to UI
        int i = 0;
        foreach(var rule in UIConfig.Rules)
        {
            if(i == 0)
            {
                _rule1PredeccessorInput.text = rule.Key.ToString();
                _rule1SuccessorInput.text = rule.Value;
            }
            else if(i == 1)
            {
                _rule2PredeccessorInput.text = rule.Key.ToString();
                _rule2SuccessorInput.text = rule.Value;
            }
            i++;
        }
        // if there is no second rule then clear any that was there before
        if(UIConfig.Rules.Count == 1)
        {
            _rule2PredeccessorInput.text = "";
            _rule2SuccessorInput.text = "";
        }
        UIConfig.cameraPosition = cameraPosition1.transform.position;
        UIConfig.cameraVerticalOffset = _verticalCameraOffset2;
        RefreshUI();

        
    }
}
