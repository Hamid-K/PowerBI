using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D8 RID: 216
	internal class JsonParser
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00023F79 File Offset: 0x00022F79
		internal JsonParser(Stream inputStream)
		{
			this.tokenizer = new JsonTokenizer(inputStream);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00023F8D File Offset: 0x00022F8D
		internal JsonObject Parse()
		{
			return this.ParseObject();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00023F98 File Offset: 0x00022F98
		private JsonObject ParseObject()
		{
			JsonObject jsonObject = new JsonObject();
			string text;
			this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.ObjectOpen });
			while (this.tokenizer.Peek() != JsonTokenType.ObjectClose)
			{
				this.ParseKeyValuePair(jsonObject);
				if (this.tokenizer.Peek() != JsonTokenType.Comma)
				{
					break;
				}
				this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.Comma });
			}
			this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.ObjectClose });
			return jsonObject;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00024018 File Offset: 0x00023018
		private void ParseKeyValuePair(JsonObject jsonObject)
		{
			JsonTokenType[] array = new JsonTokenType[1];
			string text;
			this.ReadAndValidateToken(out text, array);
			string text2;
			this.ReadAndValidateToken(out text2, new JsonTokenType[] { JsonTokenType.Colon });
			text = this.UnescapeString(text);
			jsonObject.Add(text, this.ParseValue());
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00024060 File Offset: 0x00023060
		private object ParseValue()
		{
			switch (this.tokenizer.Peek())
			{
			case JsonTokenType.String:
			{
				JsonTokenType[] array = new JsonTokenType[1];
				string text;
				this.ReadAndValidateToken(out text, array);
				return this.UnescapeString(text);
			}
			case JsonTokenType.Number:
			{
				string text;
				this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.Number });
				return this.ParseNumber(text);
			}
			case JsonTokenType.Boolean:
			{
				string text;
				this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.Boolean });
				return bool.Parse(text);
			}
			case JsonTokenType.Null:
			{
				string text;
				this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.Null });
				return null;
			}
			case JsonTokenType.ObjectOpen:
				return this.ParseObject();
			case JsonTokenType.ArrayOpen:
				return this.ParseArray();
			}
			throw new ServiceJsonDeserializationException();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00024130 File Offset: 0x00023130
		private object ParseNumber(string valueToken)
		{
			if (Regex.IsMatch(valueToken, "^-?\\d+$"))
			{
				return long.Parse(valueToken, CultureInfo.InvariantCulture);
			}
			return double.Parse(valueToken, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00024160 File Offset: 0x00023160
		private object[] ParseArray()
		{
			List<object> list = new List<object>();
			string text;
			this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.ArrayOpen });
			while (this.tokenizer.Peek() != JsonTokenType.ArrayClose)
			{
				list.Add(this.ParseValue());
				if (this.tokenizer.Peek() != JsonTokenType.Comma)
				{
					break;
				}
				this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.Comma });
			}
			this.ReadAndValidateToken(out text, new JsonTokenType[] { JsonTokenType.ArrayClose });
			return list.ToArray();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000241E8 File Offset: 0x000231E8
		private string UnescapeString(string value)
		{
			string text = value.Substring(1, value.Length - 2);
			if (Enumerable.Contains<char>(text, '\\'))
			{
				if (text.Contains("\\\\"))
				{
					text = text.Replace("\\\\", "\\");
				}
				if (text.Contains("\\\""))
				{
					text = text.Replace("\\\"", "\"");
				}
				if (text.Contains("\\/"))
				{
					text = text.Replace("\\/", "/");
				}
				if (text.Contains("\\b"))
				{
					text = text.Replace("\\b", "\b");
				}
				if (text.Contains("\\f"))
				{
					text = text.Replace("\\f", "\f");
				}
				if (text.Contains("\\n"))
				{
					text = text.Replace("\\n", "\n");
				}
				if (text.Contains("\\r"))
				{
					text = text.Replace("\\r", "\r");
				}
				if (text.Contains("\\t"))
				{
					text = text.Replace("\\t", "\t");
				}
				if (text.Contains("\\u"))
				{
					MatchCollection matchCollection = Regex.Matches(text, "\\\\u([\\da-fA-F]{4})");
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						if (match.Success)
						{
							int num = int.Parse(match.Value.Substring(2), 515);
							string text2 = char.ConvertFromUtf32(num);
							text = text.Replace(match.Value, text2);
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002439C File Offset: 0x0002339C
		private JsonTokenType ReadAndValidateToken(out string token, params JsonTokenType[] expectedTokenTypes)
		{
			JsonTokenType jsonTokenType = this.tokenizer.NextToken(out token);
			foreach (JsonTokenType jsonTokenType2 in expectedTokenTypes)
			{
				if (jsonTokenType == jsonTokenType2)
				{
					return jsonTokenType;
				}
			}
			throw new ServiceJsonDeserializationException();
		}

		// Token: 0x04000320 RID: 800
		private JsonTokenizer tokenizer;
	}
}
