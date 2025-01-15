using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000039 RID: 57
	internal static class FormUrlEncodedJson
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00007541 File Offset: 0x00005741
		public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs)
		{
			return FormUrlEncodedJson.ParseInternal(nameValuePairs, int.MaxValue, true);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000754F File Offset: 0x0000574F
		public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth)
		{
			return FormUrlEncodedJson.ParseInternal(nameValuePairs, maxDepth, true);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000755C File Offset: 0x0000575C
		public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, out JObject value)
		{
			JObject jobject;
			value = (jobject = FormUrlEncodedJson.ParseInternal(nameValuePairs, int.MaxValue, false));
			return jobject != null;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007580 File Offset: 0x00005780
		public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, out JObject value)
		{
			JObject jobject;
			value = (jobject = FormUrlEncodedJson.ParseInternal(nameValuePairs, maxDepth, false));
			return jobject != null;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000075A0 File Offset: 0x000057A0
		private static JObject ParseInternal(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, bool throwOnError)
		{
			if (nameValuePairs == null)
			{
				throw Error.ArgumentNull("nameValuePairs");
			}
			if (maxDepth <= 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxDepth", maxDepth, 1);
			}
			JObject jobject = new JObject();
			foreach (KeyValuePair<string, string> keyValuePair in nameValuePairs)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (key == null)
				{
					if (string.IsNullOrEmpty(value))
					{
						if (throwOnError)
						{
							throw Error.Argument("nameValuePairs", Resources.QueryStringNameShouldNotNull, new object[0]);
						}
						return null;
					}
					else
					{
						string[] array = new string[] { value };
						if (!FormUrlEncodedJson.Insert(jobject, array, null, throwOnError))
						{
							return null;
						}
					}
				}
				else
				{
					string[] path = FormUrlEncodedJson.GetPath(key, maxDepth, throwOnError);
					if (path == null || !FormUrlEncodedJson.Insert(jobject, path, value, throwOnError))
					{
						return null;
					}
				}
			}
			FormUrlEncodedJson.FixContiguousArrays(jobject);
			return jobject;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000769C File Offset: 0x0000589C
		private static string[] GetPath(string key, int maxDepth, bool throwOnError)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return FormUrlEncodedJson._emptyPath;
			}
			if (!FormUrlEncodedJson.ValidateQueryString(key, throwOnError))
			{
				return null;
			}
			string[] array = key.Split(new char[] { '[' });
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].EndsWith("]", StringComparison.Ordinal))
				{
					array[i] = array[i].Substring(0, array[i].Length - 1);
				}
			}
			if (array.Length < maxDepth)
			{
				return array;
			}
			if (throwOnError)
			{
				throw Error.Argument(Resources.MaxDepthExceeded, new object[] { maxDepth });
			}
			return null;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007730 File Offset: 0x00005930
		private static bool ValidateQueryString(string key, bool throwOnError)
		{
			bool flag = false;
			for (int i = 0; i < key.Length; i++)
			{
				char c = key[i];
				if (c != '[')
				{
					if (c == ']')
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							if (throwOnError)
							{
								throw Error.Argument(Resources.UnMatchedBracketNotValid, "application/x-www-form-urlencoded", new object[] { i });
							}
							return false;
						}
					}
				}
				else if (!flag)
				{
					flag = true;
				}
				else
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.NestedBracketNotValid, "application/x-www-form-urlencoded", new object[] { i });
					}
					return false;
				}
			}
			if (!flag)
			{
				return true;
			}
			if (throwOnError)
			{
				throw Error.Argument(Resources.NestedBracketNotValid, "application/x-www-form-urlencoded", new object[] { key.LastIndexOf('[') });
			}
			return false;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000077EC File Offset: 0x000059EC
		private static bool Insert(JObject root, string[] path, string value, bool throwOnError)
		{
			JObject jobject = root;
			JObject jobject2 = null;
			int i = 0;
			while (i < path.Length - 1)
			{
				if (string.IsNullOrEmpty(path[i]))
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.InvalidArrayInsert, FormUrlEncodedJson.BuildPathString(path, i), new object[0]);
					}
					return false;
				}
				else
				{
					if (!jobject.ContainsKey(path[i]))
					{
						jobject[path[i]] = new JObject();
					}
					else if (jobject[path[i]] == null || jobject[path[i]] is JValue)
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, i), new object[0]);
						}
						return false;
					}
					jobject2 = jobject;
					jobject = jobject[path[i]] as JObject;
					i++;
				}
			}
			if (string.IsNullOrEmpty(path[path.Length - 1]) && path.Length > 1)
			{
				if (!FormUrlEncodedJson.AddToArray(jobject2, path, value, throwOnError))
				{
					return false;
				}
			}
			else if (jobject == null)
			{
				if (throwOnError)
				{
					throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, path.Length - 1), new object[0]);
				}
				return false;
			}
			else if (!FormUrlEncodedJson.AddToObject(jobject, path, value, throwOnError))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000078F4 File Offset: 0x00005AF4
		private static bool AddToObject(JObject obj, string[] path, string value, bool throwOnError)
		{
			int num = path.Length - 1;
			string text = path[num];
			if (obj.ContainsKey(text))
			{
				if (obj[text] == null || obj[text].Type == 10)
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, num), new object[0]);
					}
					return false;
				}
				else if (path.Length == 1)
				{
					if (obj[text].Type == 8)
					{
						string text2 = obj[text].ToObject<string>();
						JObject jobject = new JObject();
						jobject.Add("0", text2);
						jobject.Add("1", value);
						obj[text] = jobject;
					}
					else if (obj[text] is JObject)
					{
						JObject jobject2 = obj[text] as JObject;
						string index = FormUrlEncodedJson.GetIndex(jobject2, throwOnError);
						if (index == null)
						{
							return false;
						}
						jobject2.Add(index, value);
					}
				}
				else
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.JQuery13CompatModeNotSupportNestedJson, FormUrlEncodedJson.BuildPathString(path, num), new object[0]);
					}
					return false;
				}
			}
			else if (value == null)
			{
				obj[text] = null;
			}
			else
			{
				obj[text] = value;
			}
			return true;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007A1C File Offset: 0x00005C1C
		private static bool AddToArray(JObject parent, string[] path, string value, bool throwOnError)
		{
			string text = path[path.Length - 2];
			JObject jobject = parent[text] as JObject;
			if (jobject == null)
			{
				if (throwOnError)
				{
					throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, path.Length - 1), new object[0]);
				}
				return false;
			}
			else
			{
				string index = FormUrlEncodedJson.GetIndex(jobject, throwOnError);
				if (index == null)
				{
					return false;
				}
				jobject.Add(index, value);
				return true;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007A80 File Offset: 0x00005C80
		private static string GetIndex(JObject jsonObject, bool throwOnError)
		{
			int num = -1;
			if (jsonObject.Count > 0)
			{
				foreach (string text in jsonObject.Keys)
				{
					int num2;
					if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2) && num2 > num)
					{
						num = num2;
					}
					else
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, text, new object[0]);
						}
						return null;
					}
				}
			}
			num++;
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007B18 File Offset: 0x00005D18
		private static void FixContiguousArrays(JToken jv)
		{
			JArray jarray = jv as JArray;
			if (jarray != null)
			{
				for (int i = 0; i < jarray.Count; i++)
				{
					if (jarray[i] != null)
					{
						jarray[i] = FormUrlEncodedJson.FixSingleContiguousArray(jarray[i]);
						FormUrlEncodedJson.FixContiguousArrays(jarray[i]);
					}
				}
				return;
			}
			JObject jobject = jv as JObject;
			if (jobject != null && jobject.Count > 0)
			{
				foreach (string text in new List<string>(jobject.Keys))
				{
					if (jobject[text] != null)
					{
						jobject[text] = FormUrlEncodedJson.FixSingleContiguousArray(jobject[text]);
						FormUrlEncodedJson.FixContiguousArrays(jobject[text]);
					}
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007BF0 File Offset: 0x00005DF0
		private static JToken FixSingleContiguousArray(JToken original)
		{
			JObject jobject = original as JObject;
			List<string> list;
			if (jobject != null && jobject.Count > 0 && FormUrlEncodedJson.CanBecomeArray(new List<string>(jobject.Keys), out list))
			{
				JArray jarray = new JArray();
				foreach (string text in list)
				{
					jarray.Add(jobject[text]);
				}
				return jarray;
			}
			return original;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007C78 File Offset: 0x00005E78
		private static bool CanBecomeArray(List<string> keys, out List<string> sortedKeys)
		{
			List<FormUrlEncodedJson.ArrayCandidate> list = new List<FormUrlEncodedJson.ArrayCandidate>();
			sortedKeys = null;
			bool flag = true;
			foreach (string text in keys)
			{
				int num;
				if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out num))
				{
					flag = false;
					break;
				}
				string text2 = num.ToString(CultureInfo.InvariantCulture);
				if (!text2.Equals(text, StringComparison.Ordinal))
				{
					flag = false;
					break;
				}
				list.Add(new FormUrlEncodedJson.ArrayCandidate(num, text2));
			}
			if (flag)
			{
				list.Sort((FormUrlEncodedJson.ArrayCandidate x, FormUrlEncodedJson.ArrayCandidate y) => x.Key - y.Key);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Key != i)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				sortedKeys = new List<string>(list.Select((FormUrlEncodedJson.ArrayCandidate x) => x.Value));
			}
			return flag;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007D8C File Offset: 0x00005F8C
		private static string BuildPathString(string[] path, int i)
		{
			StringBuilder stringBuilder = new StringBuilder(path[0]);
			for (int j = 1; j <= i; j++)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", new object[] { path[j] });
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000A3 RID: 163
		private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";

		// Token: 0x040000A4 RID: 164
		private const int MinDepth = 0;

		// Token: 0x040000A5 RID: 165
		private static readonly string[] _emptyPath = new string[] { string.Empty };

		// Token: 0x02000079 RID: 121
		private class ArrayCandidate
		{
			// Token: 0x060003CC RID: 972 RVA: 0x0000E730 File Offset: 0x0000C930
			public ArrayCandidate(int key, string value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060003CD RID: 973 RVA: 0x0000E746 File Offset: 0x0000C946
			// (set) Token: 0x060003CE RID: 974 RVA: 0x0000E74E File Offset: 0x0000C94E
			public int Key { get; set; }

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060003CF RID: 975 RVA: 0x0000E757 File Offset: 0x0000C957
			// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000E75F File Offset: 0x0000C95F
			public string Value { get; set; }
		}
	}
}
