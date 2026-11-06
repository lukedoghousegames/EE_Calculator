
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Calculator : MonoBehaviour
{
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button0;
    public Button button00;
    public Button button_decimal;

    public Button op_plus;
    public Button op_minus;
    public Button op_divide;
    public Button op_mult;
    public Button op_equal;
    public Button op_ac;

    public TMP_Text text_bottom;
    public TMP_Text text_top;

    public enum operation
    {
        None,
        Add,
        Sub,
        Mult,
        Div,
        Equals,
        AC,
    }

    class Value
    {
        public operation op;
        public string value;

        public override string ToString()
        {
            switch (op)
            {
                case operation.None:
                    return value;
                    break;
                case operation.Add:
                    return "+";
                    break;
                case operation.Sub:
                    return "-";
                    break;
                case operation.Mult:
                    return "*";
                    break;  
                case operation.Div:
                    return "/";
                    break;
                default:
                    Debug.LogError("Invalid operation");
                    return "";
                    break;
            }
        }
        
        public bool IsNumber()
        {
            return op == operation.None;
        }

        public bool IsOperation()
        {
            return op != operation.None;
        }
    }

    // LOGIC STUFF
    List<Value> values = new List<Value>();
    Value currentValue = new Value();

    bool HasValues()
    {
        return values.Count > 0;
    }
    
    Value LastValue()
    {
        if (HasValues()) return null;
        return values[values.Count - 1];
    }
    
    void Start()
    {
        button0.onClick.AddListener(() => ClickedNumber("0"));
        button00.onClick.AddListener(() => ClickedNumber("00"));
        button1.onClick.AddListener(() => ClickedNumber("1"));
        button2.onClick.AddListener(() => ClickedNumber("2"));
        button3.onClick.AddListener(() => ClickedNumber("3"));
        button4.onClick.AddListener(() => ClickedNumber("4"));
        button5.onClick.AddListener(() => ClickedNumber("5"));
        button6.onClick.AddListener(() => ClickedNumber("6"));
        button7.onClick.AddListener(() => ClickedNumber("7"));
        button8.onClick.AddListener(() => ClickedNumber("8"));
        button9.onClick.AddListener(() => ClickedNumber("9"));
        button_decimal.onClick.AddListener(() => ClickedNumber("."));

        op_plus.onClick.AddListener(() => ClickedOperation(operation.Add));
        op_mult.onClick.AddListener(() => ClickedOperation(operation.Mult));
        op_minus.onClick.AddListener(() => ClickedOperation(operation.Sub));
        op_divide.onClick.AddListener(() => ClickedOperation(operation.Div));

        op_ac.onClick.AddListener(() => ClickedOperation(operation.AC));
        op_equal.onClick.AddListener(() => ClickedOperation(operation.Equals));
    }

    private void ClickedNumber(string v)
    {
        currentValue.value += v;
    }

    private void ClickedOperation(operation v)
    {
        // TODO **********************************
        // handle all the different operation types 
        // Tip: add in the current Value to values, then add in a new Value with the op set, and value to the text representation 
        // then set currentValue = new Value();
        // Can also perform other logic checks in here like:
            // swapping the last entered operation ( if you pressed * last and wanted - it will swap )
            // checking if the currentValue is empty or null and returning .
            // etc. 

        // LOGIC CHECKS HERE
        // SWITCH STATEMENT HERE

        switch (v)
        {
            case operation.AC:
                ClearAll();
                break;

            case operation.Equals:
                PushCurrentValue();
                ClearCurrent();
                PerformCalculation();
                break;
            
            case operation.None:
                Debug.LogError("Shouldn't happen");
                break;
            
            default:
                if (HasValues() && LastValue().IsOperation())
                {
                    RemovePreviousOperator();
                    AddOperation(v);
                    break;
                }
                
                PushCurrentValue();
                AddOperation(v);
                ClearCurrent();
                break;
        }

        void AddOperation(operation op)
        {
            values.Add(new Value(){op = op});
        }
    }

    void RemovePreviousOperator()
    {
        if (values.Count <= 0) return;
        if (LastValue().IsNumber()) return;

        values.RemoveAt(values.Count - 1);
    }

    void ClearAll()
    {
        values.Clear();
        ClearCurrent();
    }

    void ClearCurrent()
    {
        currentValue.value = "";
        currentValue.op = operation.None;
    }

    void PushCurrentValue()
    {
        if (currentValue.IsNumber())
        {
            Value newValue = new Value();
            newValue.op = operation.None;
            newValue.value = currentValue.value;
            values.Add(newValue);
        }
    }

    void PerformCalculation()
    {
        // TODO ************************************
        // Loop current values 
        // Perform the operations on the result as they come in.
        float result = 0;

        // LOOP HERE

        currentValue = new Value() { value = result.ToString() };
    }

    string GetCurrentTopString()
    {
        string ret = "";
        for (int i = 0; i < values.Count; i++)
        {
            Value val = values[i];
            if(val.op != operation.None)
            {
                ret += val.ToString();
            }
            else
            {
                ret += " " + val + " ";
            }
        }
        return ret;
    }

    string GetBottomText()
    {
        return currentValue.ToString();
    }

    private void Update()
    {
        text_top.text = GetCurrentTopString();
        text_bottom.text = GetBottomText();
    }
}
