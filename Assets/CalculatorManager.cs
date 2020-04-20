using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorManager : MonoBehaviour
{

    public float m_a = 0f; 
    public float m_b = 0f;
    public float m_result = 0f;

    public OperationType m_currentOperation = OperationType.ADD;

    public enum OperationType { ADD, SUB, MULT, DIV };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentOperation == OperationType.ADD) { m_result = Add(m_a, m_b); }
        if (m_currentOperation == OperationType.SUB) { m_result = Substract(m_a, m_b); }
        if (m_currentOperation == OperationType.MULT) { m_result = Multiply(m_a, m_b); }
        if (m_currentOperation == OperationType.DIV) { m_result = Divide(m_a, m_b); }
    }


    float Add(float x, float y) { return x + y; }
    float Substract(float x, float y) { return x - y; }

    float Multiply(float x, float y) { return x * y; }

    float Divide(float x, float y) { return x / y; }
}
