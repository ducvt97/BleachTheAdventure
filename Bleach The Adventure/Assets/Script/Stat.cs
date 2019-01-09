using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
   [SerializeField]
   private BarScript bar;
   [SerializeField]
   private float maxVal;
   [SerializeField]
   private float curVal;
   public float CurrentVal
   {
   		get
   		{
   			return curVal;
   		}
   		set 
   		{
   			this.curVal = Mathf.Clamp(value, 0, MaxVal);
   			bar.Value = curVal;
   		}
   }

   public float MaxVal
   {
   		get
   		{
   			return maxVal;
   		}
   		set 
   		{
   			this.maxVal = value;
   			bar.MaxValue = maxVal;
   		}
   }

   public void Init()
   {
   		this.MaxVal = maxVal;
   		this.CurrentVal = curVal;
   }

}
