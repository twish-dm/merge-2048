using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

static public class Utils
{
	static public T Pop<T>(this List<T> list)
	{
		bool isHave = list.Count > 0;
		T item = default(T);
		if (isHave)
		{
			item = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}
		return item;
	}
	static public bool Peek<T>(this List<T> list, out T item)
	{
		item = default;
		bool isHave = list.Count > 0;
		if (isHave)
			item = list[list.Count - 1];
		return isHave;
	}

	public static bool IsPointerOverUI
	{
		get
		{
			var eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;
			var results = new List<RaycastResult>();
			if(EventSystem.current)
				EventSystem.current.RaycastAll(eventData, results);
			return results.Count > 0;
		}
	}

	public static string ToMoneyFormat<T>(this T Value)
	{
		return ShrinkWorker(Convert.ToInt64(Value));
	}
	static public void UpdateLayout(this RectTransform rectTransform)
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		LayoutGroup[] layoutControllers = rectTransform.GetComponentsInChildren<LayoutGroup>();
		for (int i = 0; i < layoutControllers.Length; i++)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutControllers[i].transform);
		}
	}
	public static string ShrinkWorker(long Value)
	{
		if (Value >= 10000000000000000000D)
		{
			return (Value / 1000000000000000000D).ToString("0.#Qi");
		}
		if (Value >= 1000000000000000000D)
		{
			return (Value / 1000000000000000000D).ToString("0.##Qi");
		}
		if (Value >= 10000000000000000)
		{
			return (Value / 1000000000000000D).ToString("0.#Qa");
		}
		if (Value >= 1000000000000000)
		{
			return (Value / 1000000000000000D).ToString("0.##Qa");
		}
		if (Value >= 10000000000000)
		{
			return (Value / 1000000000000D).ToString("0.#T");
		}
		if (Value >= 1000000000000)
		{
			return (Value / 1000000000000D).ToString("0.##T");
		}
		if (Value >= 10000000000)
		{
			return (Value / 1000000000D).ToString("0.#B");
		}
		if (Value >= 1000000000)
		{
			return (Value / 1000000000D).ToString("0.##B");
		}

		if (Value >= 100000000)
		{
			return (Value / 1000000D).ToString("0.#M");
		}
		if (Value >= 1000000)
		{
			return (Value / 1000000D).ToString("0.##M");
		}
		if (Value >= 100000)
		{
			return (Value / 1000D).ToString("0.#K");
		}
		if (Value >= 1000)
		{
			return (Value / 1000D).ToString("0.##K");
		}

		return Value.ToString("#,0");
	}


	public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera = null, bool useNormalizeViewPort = false)
	{
		if (camera == null)
		{
			camera = Camera.main;
		}

		var viewportPosition = camera.WorldToViewportPoint(worldPosition);

		if (useNormalizeViewPort)
		{
			Rect normalizedViewPort = camera.rect;
			viewportPosition.x = viewportPosition.x * normalizedViewPort.width + normalizedViewPort.x;
			viewportPosition.y = viewportPosition.y * normalizedViewPort.height + normalizedViewPort.y;
		}

		return canvas.ViewportToCanvasPosition(viewportPosition);
	}

	public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
	{
		var viewportPosition = new Vector3(screenPosition.x / Screen.width,
						   screenPosition.y / Screen.height,
						   0);
		return canvas.ViewportToCanvasPosition(viewportPosition);
	}

	public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
	{
		var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
		var canvasRect = canvas.GetComponent<RectTransform>();
		var scale = canvasRect.sizeDelta;
		return Vector3.Scale(centerBasedViewPortPosition, scale);
	}
}
