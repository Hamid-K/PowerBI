using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000194 RID: 404
	internal struct CustomJsonPropertyHelper
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x000A40AE File Offset: 0x000A22AE
		public CustomJsonPropertyHelper(string json)
		{
			this.jObject = (string.IsNullOrEmpty(json) ? null : JObject.Parse(json));
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000A40C7 File Offset: 0x000A22C7
		public CustomJsonPropertyHelper(JToken json)
		{
			this.jObject = ((json != null) ? ((JObject)json) : null);
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000A40DB File Offset: 0x000A22DB
		public bool IsNullOrEmpty
		{
			get
			{
				return this.jObject == null || this.jObject.Count == 0;
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000A40F8 File Offset: 0x000A22F8
		public TValue GetValue<TValue>(string key)
		{
			if (this.jObject == null)
			{
				return default(TValue);
			}
			JToken jtoken = this.jObject[key];
			if (jtoken == null)
			{
				return default(TValue);
			}
			object primitiveValue = CustomJsonPropertyHelper.GetPrimitiveValue(key, jtoken);
			if (primitiveValue is TValue)
			{
				return (TValue)((object)primitiveValue);
			}
			return default(TValue);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000A4154 File Offset: 0x000A2354
		public void SetValue<TValue>(string key, TValue value)
		{
			if (value == null)
			{
				if (this.jObject != null)
				{
					this.jObject.Remove(key);
					return;
				}
			}
			else
			{
				if (this.jObject == null)
				{
					this.jObject = new JObject();
				}
				this.jObject[key] = CustomJsonPropertyHelper.PrimitiveValueToJson(key, value);
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000A41AA File Offset: 0x000A23AA
		public JToken ToJson()
		{
			if (this.jObject == null)
			{
				return null;
			}
			return this.jObject.DeepClone();
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000A41C1 File Offset: 0x000A23C1
		public override string ToString()
		{
			if (this.jObject == null || this.jObject.Count == 0)
			{
				return string.Empty;
			}
			return this.jObject.ToString();
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000A41EC File Offset: 0x000A23EC
		internal static bool TryConvertTokenToPrimitiveValue<TValue>(JToken token, out TValue value)
		{
			object obj;
			if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue(token, out obj) && obj is TValue)
			{
				TValue tvalue = (TValue)((object)obj);
				value = tvalue;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000A4228 File Offset: 0x000A2428
		internal static bool TryConvertTokenToPrimitiveValue(JToken token, out object value)
		{
			switch (token.Type)
			{
			case 6:
				value = (long)token;
				return true;
			case 7:
				value = (double)token;
				return true;
			case 8:
				value = (string)token;
				return true;
			case 9:
				value = (bool)token;
				return true;
			default:
				value = null;
				return false;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000A4294 File Offset: 0x000A2494
		internal static bool TryConvertPrimitiveValueToToken(object value, bool excludeComplexTypes, out JToken token)
		{
			if (value == null)
			{
				token = null;
				return true;
			}
			if (value is bool || value is long || value is int || value is double)
			{
				token = JToken.FromObject(value);
				return true;
			}
			string text = value as string;
			if (text != null)
			{
				try
				{
					token = JToken.Parse(text);
					switch (token.Type)
					{
					case 0:
					case 3:
					case 4:
					case 5:
					case 10:
					case 11:
						return false;
					case 1:
					case 2:
						return !excludeComplexTypes;
					}
					return true;
				}
				catch (Exception)
				{
					token = new JValue(text);
					return true;
				}
			}
			token = null;
			return false;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000A4358 File Offset: 0x000A2558
		private static object GetPrimitiveValue(string key, JToken token)
		{
			object obj;
			if (!CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue(token, out obj))
			{
				throw CustomJsonPropertyHelper.GetUnsupportedJsonPrimitiveTypeException(key);
			}
			return obj;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000A4378 File Offset: 0x000A2578
		private static JToken PrimitiveValueToJson(string key, object value)
		{
			JToken jtoken;
			if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value, true, out jtoken))
			{
				throw CustomJsonPropertyHelper.GetUnsupportedJsonPrimitiveTypeException(key);
			}
			return jtoken;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000A4398 File Offset: 0x000A2598
		private static Exception GetUnsupportedJsonPrimitiveTypeException(string propertyName)
		{
			return new NotImplementedException(TomSR.Exception_UnsupportedJsonPrimitiveType(propertyName));
		}

		// Token: 0x040004B1 RID: 1201
		private JObject jObject;
	}
}
