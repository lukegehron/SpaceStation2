using System.Collections;
using System.Collections.Generic;
using SpeckleCore;
using UnityEngine;
using UnityEngine.Rendering;
using Newtonsoft.Json;

namespace SpeckleUnity
{
	/// <summary>
	/// 
	/// </summary>
	[CreateAssetMenu(menuName = "SpeckleUnity/Rendering Rule: Color By Color")]
	public class ColorByColor : RenderingRule
	{
		/// <summary>
		/// 
		/// </summary>
		public Gradient gradient;

		/// <summary>
		/// 
		/// </summary>
		public string colorName = "_Color";

		/// <summary>
		/// 
		/// </summary>
		public bool receiveShadows = false;

		/// <summary>
		/// 
		/// </summary>
		public ShadowCastingMode shadowCastingMode = ShadowCastingMode.Off;

		/// <summary>
		/// 
		/// </summary>
		public List<ColorKey> colorKey = new List<ColorKey>();

		/// <summary>
		/// 
		/// </summary>
		protected Dictionary<string, Color> colorLookup = new Dictionary<string, Color>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="speckleStream"></param>
		/// <param name="objectIndex"></param>
		/// <param name="block"></param>
		public override void ApplyRuleToObject(Renderer renderer, SpeckleStream speckleStream, int objectIndex, MaterialPropertyBlock block)
		{

			Color colorToApply;
			colorToApply = gradient.Evaluate(Random.Range(0f, 1f));



			SpeckleObject data = speckleStream.Objects[objectIndex];

			if (data.Properties.TryGetValue("parameters", out object propertyValue))
			{

				//Debug.Log(propertyValue.ToString());
				var json = JsonConvert.SerializeObject(propertyValue);
				var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
				if (dictionary.TryGetValue("Comments", out string propertyValue1))
				{
					
					if (ColorUtility.TryParseHtmlString("#" + propertyValue1, out colorToApply))
					{

					}
				}
			}


			if (colorLookup.Count == 0) colorKey.Clear();


			block.SetColor(colorName, colorToApply);
			renderer.SetPropertyBlock(block);

			renderer.receiveShadows = receiveShadows;
			renderer.shadowCastingMode = shadowCastingMode;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	[System.Serializable]
	public class ColorKey
	{
		/// <summary>
		/// 
		/// </summary>
		public string name;

		/// <summary>
		/// 
		/// </summary>
		public Color color;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="n"></param>
		/// <param name="c"></param>
		public ColorKey(string n, Color c)
		{
			name = n;
			color = c;
		}
	}
}