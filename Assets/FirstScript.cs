using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    public float m_chrono = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_chrono = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_chrono = m_chrono + Time.deltaTime;
    }
}
