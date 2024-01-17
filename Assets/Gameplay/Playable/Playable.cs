using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Playable : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler
{
	private bool m_IsPointerDown;
	private List<Cell> m_Targets;
	private GridLayoutGroup m_Grid;
	private void Awake()
	{
		m_Targets = new List<Cell>();
		m_Grid = GetComponent<GridLayoutGroup>();
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		m_IsPointerDown = true;
		OnPointerMove(eventData);
	}
	public void OnPointerMove(PointerEventData eventData)
	{
		if (m_IsPointerDown)
		{
			if
			(
				eventData.pointerCurrentRaycast.gameObject &&
				eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Cell cell) && !m_Targets.Contains(cell)/* &&
				m_Targets.Count > 0*/
			)
			{
				if (m_Targets.Count == 0)
				{
					m_Targets.Add(cell);
					cell.Visit();
				}
				else if
				(
					Vector3.Distance(m_Targets[m_Targets.Count - 1].transform.position, cell.transform.position) < Mathf.Sqrt(2) * (cell.rectTransform.sizeDelta.magnitude) &&
					CheckMerge(m_Targets, cell)

				)
				{
					m_Targets.Add(cell);
					cell.Visit();
				}
			}
		}
	}

	protected bool CheckMerge(List<Cell> targets, Cell newTarget)
	{
		int value = 0;
		for (int i = 0; i < targets.Count; i++)
			value += targets[i].Id.Calculate();

		return Enumerable.Range(targets[0].Id, value.Decalculate()).Contains(newTarget.Id) && targets[targets.Count -1].Id <= newTarget.Id;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (m_IsPointerDown)
		{
			Merge();
			for (int i = 0; i < m_Targets.Count; i++)
				m_Targets[i].Leave();
			m_Targets.Clear();
			m_IsPointerDown = false;
		}
	}

	public void Merge()
	{
		int value = 0;
		for (int i = 0; i < m_Targets.Count; i++)
		{
			value += m_Targets[i].Id.Calculate();
			if(m_Targets.Count-1 != i)
				m_Targets[i].ClearAndNew(0.1f+i *0.1f);
		}
		m_Targets[m_Targets.Count - 1].Id = value.Decalculate();
	}
}
