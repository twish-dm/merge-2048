using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class MathUtils
{
	static public int Calculate(this int index)
	{
		return Mathf.CeilToInt(Mathf.Pow(2, index));
	}
	static public long Calculate(this long index)
	{
		return Mathf.CeilToInt(Mathf.Pow(2, index));
	}
	static public int Calculate(this float index)
	{
		return Mathf.CeilToInt(index).Calculate();
	}

	static public int Decalculate(this int value)
	{
		return Mathf.CeilToInt(Mathf.Log(value, 2));
	}
	static public long Decalculate(this long value)
	{
		return Mathf.CeilToInt(Mathf.Log(value, 2));
	}
	static public int Decalculate(this float value)
	{
		return Mathf.CeilToInt(value).Decalculate();
	}
}