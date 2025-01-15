using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000066 RID: 102
	public static class JsonProcessingHelper
	{
		// Token: 0x060002CA RID: 714 RVA: 0x000080E4 File Offset: 0x000062E4
		public static JsonOperatorResult NormalizeJsonWithOperators(JObject jsonObject, IList<JsonProcessingHelper.JsonOperator> operators)
		{
			int num = 0;
			foreach (JsonProcessingHelper.JsonOperator jsonOperator in operators)
			{
				foreach (JToken jtoken in jsonObject.SelectTokens(jsonOperator.Selector).ToList<JToken>())
				{
					JToken jtoken2 = jsonOperator.OperandAccessor(jtoken);
					if (jtoken2 == null || jtoken2.GetType() != jsonOperator.ExpectedOperandType)
					{
						if (jtoken2 != null)
						{
							string name = jtoken2.GetType().Name;
						}
						num++;
					}
					else
					{
						jsonOperator.Operator(jtoken2);
					}
				}
			}
			return new JsonOperatorResult(jsonObject, num);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000081C8 File Offset: 0x000063C8
		public static JObject RemoveUnnecessaryElements(JObject json)
		{
			foreach (JToken jtoken in json.SelectTokens("$..ordinal").ToList<JToken>())
			{
				if ((int)jtoken == 0)
				{
					jtoken.Parent.Remove();
				}
			}
			foreach (JToken jtoken2 in json.SelectTokens("$..tabOrder").ToList<JToken>())
			{
				jtoken2.Parent.Remove();
			}
			return json;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008280 File Offset: 0x00006480
		public static JObject SetFloatingPrecisionForKeys(JObject json, string[] keys, int precision, string prefixForFloatKeyStringValues)
		{
			string text = "F" + precision.ToString(CultureInfo.InvariantCulture);
			foreach (string text2 in keys)
			{
				foreach (JToken jtoken in json.SelectTokens("$.." + text2).ToList<JToken>())
				{
					JValue jvalue = new JValue(prefixForFloatKeyStringValues + Math.Round((double)(float)jtoken, precision, MidpointRounding.AwayFromZero).ToString(text, CultureInfo.InvariantCulture));
					jtoken.Replace(jvalue);
				}
			}
			return json;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000833C File Offset: 0x0000653C
		public static bool ValidateAndGetUniqueType(JArray array, string key, out JTokenType? uniqueType)
		{
			uniqueType = null;
			bool flag;
			try
			{
				foreach (JToken jtoken in array)
				{
					if (!jtoken.HasValues)
					{
						return false;
					}
					JToken jtoken2 = jtoken[key];
					JTokenType? jtokenType = ((jtoken2 != null) ? new JTokenType?(jtoken2.Type) : null);
					if (uniqueType == null)
					{
						if (jtokenType == null)
						{
							return false;
						}
						uniqueType = jtokenType;
					}
					else
					{
						JTokenType? jtokenType2 = jtokenType;
						JTokenType value = uniqueType.Value;
						if (!((jtokenType2.GetValueOrDefault() == value) & (jtokenType2 != null)))
						{
							return false;
						}
					}
				}
				flag = true;
			}
			catch (Exception ex) when (PBIProjectUtils.IsSafeException(ex))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x020000D2 RID: 210
		public class JsonOperator
		{
			// Token: 0x060004EC RID: 1260 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
			private JsonOperator(string selector, Func<JToken, JToken> operandAccessor, string operandAccessorDescriptor, Type expectedOperandType, Action<JToken> currentOperator)
			{
				this.Selector = selector;
				this.OperandAccessor = operandAccessor;
				this.OperandAccessorDescriptor = operandAccessorDescriptor;
				this.ExpectedOperandType = expectedOperandType;
				this.Operator = currentOperator;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000DF15 File Offset: 0x0000C115
			public string Selector { get; }

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000DF1D File Offset: 0x0000C11D
			public Func<JToken, JToken> OperandAccessor { get; }

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000DF25 File Offset: 0x0000C125
			public string OperandAccessorDescriptor { get; }

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000DF2D File Offset: 0x0000C12D
			public Type ExpectedOperandType { get; }

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000DF35 File Offset: 0x0000C135
			public Action<JToken> Operator { get; }

			// Token: 0x060004F2 RID: 1266 RVA: 0x0000DF3D File Offset: 0x0000C13D
			public static JsonProcessingHelper.JsonOperator ForParentProperty(string selector, Action<JToken> currentOperator)
			{
				return new JsonProcessingHelper.JsonOperator(selector, (JToken item) => item.Parent, "Parent", typeof(JProperty), currentOperator);
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x0000DF74 File Offset: 0x0000C174
			public static JsonProcessingHelper.JsonOperator ForSelfObject(string selector, Action<JToken> currentOperator)
			{
				return new JsonProcessingHelper.JsonOperator(selector, (JToken item) => item, "Self", typeof(JObject), currentOperator);
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x0000DFAB File Offset: 0x0000C1AB
			public static JsonProcessingHelper.JsonOperator ForArray(string selector, Action<JToken> currentOperator)
			{
				return new JsonProcessingHelper.JsonOperator(selector, (JToken item) => item, "Self", typeof(JArray), currentOperator);
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x0000DFE2 File Offset: 0x0000C1E2
			public static void RemoveToken(JToken selectedItem)
			{
				selectedItem.Remove();
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x0000DFEC File Offset: 0x0000C1EC
			public static JObject SortJsonObjectTree(JObject json, string putPropertyFirst_Name = null, IList<KeyAndType> sortOrder = null, string[] sortedArrayKeys = null, bool sortArrays = true)
			{
				JProperty jproperty = new JProperty("temp", json);
				foreach (JObject jobject in (from jobj in jproperty.Descendants().OfType<JObject>()
					where jobj.Children().FirstOrDefault<JToken>() is JProperty
					select jobj).Reverse<JObject>())
				{
					JsonProcessingHelper.JsonOperator.SortObject(jobject, null, putPropertyFirst_Name);
				}
				json = (JObject)jproperty.First<JToken>();
				if (sortArrays)
				{
					IEnumerable<JArray> enumerable;
					if (sortedArrayKeys != null && sortedArrayKeys.Length != 0)
					{
						enumerable = (from p in json.Descendants().OfType<JProperty>()
							where sortedArrayKeys != null && sortedArrayKeys.Contains(p.Name)
							select p.Value).OfType<JArray>().Reverse<JArray>();
					}
					else
					{
						enumerable = json.Descendants().OfType<JArray>().Reverse<JArray>();
					}
					foreach (JArray jarray in enumerable)
					{
						try
						{
							string sortingKeyOrNull = JsonProcessingHelper.JsonOperator.GetSortingKeyOrNull(jarray, sortOrder);
							JsonProcessingHelper.JsonOperator.SortArray(jarray, sortingKeyOrNull, false);
						}
						catch (Exception ex) when (PBIProjectUtils.IsSafeException(ex))
						{
						}
					}
				}
				return json;
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x0000E170 File Offset: 0x0000C370
			public static void SortArray(JArray jsonArray, string sortBy = null, bool sortByNeedValidation = true)
			{
				JTokenType? jtokenType = null;
				if (sortBy != null && (!sortByNeedValidation || JsonProcessingHelper.ValidateAndGetUniqueType(jsonArray, sortBy, out jtokenType)))
				{
					try
					{
						jsonArray.Replace(new JArray(jsonArray.OrderBy((JToken e) => e[sortBy])));
						return;
					}
					catch (Exception ex) when (PBIProjectUtils.IsSafeException(ex))
					{
					}
				}
				jsonArray.Replace(new JArray(jsonArray.OrderBy((JToken e) => e.ToString())));
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x0000E228 File Offset: 0x0000C428
			public static void SortObject(JToken selectedItem, string sortBy = null, string putPropertyFirst_Name = null)
			{
				List<JToken> list = selectedItem.OrderBy(delegate(JToken e)
				{
					if (sortBy != null)
					{
						return e[sortBy];
					}
					return ((JProperty)e).Name;
				}).ToList<JToken>();
				JToken jtoken = list.SingleOrDefault((JToken e) => ((JProperty)e).Name == putPropertyFirst_Name);
				if (jtoken != null)
				{
					list.Remove(jtoken);
					list.Insert(0, jtoken);
				}
				selectedItem.Replace(new JObject(list));
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x0000E294 File Offset: 0x0000C494
			private static string GetSortingKeyOrNull(JArray array, IList<KeyAndType> sortOrder)
			{
				if (array != null && array.Count > 0 && sortOrder != null && sortOrder.Count > 0)
				{
					foreach (KeyAndType keyAndType in sortOrder)
					{
						JTokenType? jtokenType = null;
						if (JsonProcessingHelper.ValidateAndGetUniqueType(array, keyAndType.Key, out jtokenType))
						{
							JTokenType? jtokenType2 = jtokenType;
							JTokenType keyType = keyAndType.KeyType;
							if ((jtokenType2.GetValueOrDefault() == keyType) & (jtokenType2 != null))
							{
								return keyAndType.Key;
							}
						}
					}
				}
				return null;
			}
		}
	}
}
