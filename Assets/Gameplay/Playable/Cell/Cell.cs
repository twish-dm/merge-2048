using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	[SerializeField] private Image m_Visited;
	[SerializeField] private Image m_Body;
	[SerializeField] private Text m_ValueField;
	[SerializeField] private Gradient m_Colors;
	private int m_Id;
	public int Id
	{
		get
		{
			return m_Id;
		}
		set
		{
			m_Id = value;
			m_Body.gameObject.SetActive(m_Id > 0);
			m_Body.color = m_Colors.Evaluate((float)m_Id/(float)20);
			m_ValueField.text = Mathf.CeilToInt(m_Id).Calculate().ToMoneyFormat();
		}
	}
	
	public RectTransform rectTransform { get; protected set; }
	private void Awake()
	{
		rectTransform = (RectTransform)transform;
		m_Visited.gameObject.SetActive(false);
		Id = RandomValue();
	}
	protected int RandomValue()
	{
		return Random.Range(1, 3);
	}
	public void ClearAndNew(float delay)
	{
		Id = 0;
		StartCoroutine(WaitAndSetValueCoroutine(delay));
		IEnumerator WaitAndSetValueCoroutine(float delay)
		{
			yield return new WaitForSeconds(delay);
			Id = RandomValue();
		}
	}
	public void Visit()
	{
		m_Visited.gameObject.SetActive(true);
	}
	public void Leave()
	{
		m_Visited.gameObject.SetActive(false);
	}
}
