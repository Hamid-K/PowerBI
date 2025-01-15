using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics
{
	// Token: 0x02001A67 RID: 6759
	public static class Semantics
	{
		// Token: 0x0600DEAB RID: 57003 RVA: 0x002F4DE5 File Offset: 0x002F2FE5
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject Object(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty property)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject(property);
		}

		// Token: 0x0600DEAC RID: 57004 RVA: 0x002F4DED File Offset: 0x002F2FED
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject Append(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty property, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject = new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject(obj);
			jobject.AddFirst(property);
			return jobject;
		}

		// Token: 0x0600DEAD RID: 57005 RVA: 0x002F4DFC File Offset: 0x002F2FFC
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject FlattenObject(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			if (!(x is Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject))
			{
				return null;
			}
			return Semantics.FlattenObject(x.SelectFirstToken(path) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject);
		}

		// Token: 0x0600DEAE RID: 57006 RVA: 0x002F4E19 File Offset: 0x002F3019
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken Array(IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> elements)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray(elements);
		}

		// Token: 0x0600DEAF RID: 57007 RVA: 0x002F4E21 File Offset: 0x002F3021
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken ToArray(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			return Semantics.ToArray(x.SelectFirstToken(path) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject);
		}

		// Token: 0x0600DEB0 RID: 57008 RVA: 0x002F4E34 File Offset: 0x002F3034
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty Property(string key, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken value)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty(key, value);
		}

		// Token: 0x0600DEB1 RID: 57009 RVA: 0x002F4E3D File Offset: 0x002F303D
		public static string Key(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken selectValue)
		{
			if (!(selectValue is Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue))
			{
				return null;
			}
			return selectValue.ToString();
		}

		// Token: 0x0600DEB2 RID: 57010 RVA: 0x00004FAE File Offset: 0x000031AE
		public static string ConstKey(string str)
		{
			return str;
		}

		// Token: 0x0600DEB3 RID: 57011 RVA: 0x002F4E4F File Offset: 0x002F304F
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken Value(string selectKey)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(selectKey);
		}

		// Token: 0x0600DEB4 RID: 57012 RVA: 0x002F4E57 File Offset: 0x002F3057
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken TransformValue(ValueSubstring selectKey)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(selectKey.Value);
		}

		// Token: 0x0600DEB5 RID: 57013 RVA: 0x002F4E4F File Offset: 0x002F304F
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken ConstValue(string str)
		{
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(str);
		}

		// Token: 0x0600DEB6 RID: 57014 RVA: 0x002F4E64 File Offset: 0x002F3064
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject SelectObject(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			return x.SelectFirstToken(path) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject;
		}

		// Token: 0x0600DEB7 RID: 57015 RVA: 0x002F4E72 File Offset: 0x002F3072
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty SelectProperty(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			return x.SelectFirstProperty(path);
		}

		// Token: 0x0600DEB8 RID: 57016 RVA: 0x002F4E7B File Offset: 0x002F307B
		public static string SelectKey(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty jproperty = x.SelectFirstProperty(path);
			if (jproperty == null)
			{
				return null;
			}
			return jproperty.Name;
		}

		// Token: 0x0600DEB9 RID: 57017 RVA: 0x002F4E8F File Offset: 0x002F308F
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken SelectValue(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			return x.SelectFirstToken(path);
		}

		// Token: 0x0600DEBA RID: 57018 RVA: 0x002F4E98 File Offset: 0x002F3098
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken ValueToString(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x.SelectFirstToken(path);
			if (jtoken != null)
			{
				return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(jtoken.ToString());
			}
			return null;
		}

		// Token: 0x0600DEBB RID: 57019 RVA: 0x002F4EC0 File Offset: 0x002F30C0
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken ConvertValueTo(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JTokenType t, JPath path)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x.SelectFirstToken(path);
			if (jtoken == null)
			{
				return null;
			}
			string text = jtoken.ToString();
			switch (t)
			{
			case JTokenType.Integer:
			{
				int num;
				if (int.TryParse(text, out num))
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(num);
				}
				return null;
			}
			case JTokenType.Float:
			{
				double num2;
				if (double.TryParse(text, out num2))
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(num2);
				}
				return null;
			}
			case JTokenType.Boolean:
			{
				bool flag;
				if (bool.TryParse(text, out flag))
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(flag);
				}
				return null;
			}
			}
			return null;
		}

		// Token: 0x0600DEBC RID: 57020 RVA: 0x002F4F36 File Offset: 0x002F3136
		public static IRow SelectStringValues(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x)
		{
			return new JPathMapperRow(x.Wrapped, null);
		}

		// Token: 0x0600DEBD RID: 57021 RVA: 0x002F4F44 File Offset: 0x002F3144
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken SelectArray(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x, JPath path)
		{
			return x.SelectFirstToken(path) as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray;
		}

		// Token: 0x0600DEBE RID: 57022 RVA: 0x002F4F54 File Offset: 0x002F3154
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject FlattenObject(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject = new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject();
			Semantics.FlattenObject(obj, jobject, "");
			return jobject;
		}

		// Token: 0x0600DEBF RID: 57023 RVA: 0x002F4F7C File Offset: 0x002F317C
		private static void FlattenObject(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken cur, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject result, string prop)
		{
			if (cur is Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue)
			{
				result.Add(new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty(prop, cur));
				return;
			}
			Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray jarray = cur as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray;
			if (jarray != null)
			{
				if (jarray.Count == 0)
				{
					result[prop] = new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray();
					return;
				}
				for (int i = 0; i < jarray.Count; i++)
				{
					Semantics.FlattenObject(jarray[i], result, prop + "[" + i.ToString() + "]");
				}
				return;
			}
			else
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject = cur as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject;
				if (jobject == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting JObject instead of {0}", new object[] { cur.Type })));
				}
				if (jobject.Count == 0 && prop != string.Empty)
				{
					result.Add(new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty(prop, null));
					return;
				}
				foreach (KeyValuePair<string, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> keyValuePair in jobject.PropertiesEnumerable)
				{
					string text = ((prop == string.Empty) ? keyValuePair.Key : (prop + "." + keyValuePair.Key));
					Semantics.FlattenObject(keyValuePair.Value, result, text);
				}
				return;
			}
		}

		// Token: 0x0600DEC0 RID: 57024 RVA: 0x002F50C0 File Offset: 0x002F32C0
		public static Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray ToArray(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			return new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray(from c in obj.Children()
				select new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject(c));
		}
	}
}
