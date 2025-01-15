using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000020 RID: 32
	[DebuggerDisplay("MediaType [{ToText()}]")]
	public sealed class ODataMediaType
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003FB7 File Offset: 0x000021B7
		public ODataMediaType(string type, string subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FC2 File Offset: 0x000021C2
		public ODataMediaType(string type, string subType, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			this.type = type;
			this.subType = subType;
			this.parameters = parameters;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003FDF File Offset: 0x000021DF
		internal ODataMediaType(string type, string subType, KeyValuePair<string, string> parameter)
			: this(type, subType, new KeyValuePair<string, string>[] { parameter })
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003FF7 File Offset: 0x000021F7
		public string SubType
		{
			get
			{
				return this.subType;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003FFF File Offset: 0x000021FF
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004007 File Offset: 0x00002207
		public IEnumerable<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000400F File Offset: 0x0000220F
		internal string FullTypeName
		{
			get
			{
				return this.type + "/" + this.subType;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004028 File Offset: 0x00002228
		internal Encoding SelectEncoding()
		{
			if (this.parameters != null)
			{
				using (IEnumerator<string> enumerator = Enumerable.Where<string>(Enumerable.Select<KeyValuePair<string, string>, string>(Enumerable.Where<KeyValuePair<string, string>>(this.parameters, (KeyValuePair<string, string> parameter) => HttpUtils.CompareMediaTypeParameterNames("charset", parameter.Key)), (KeyValuePair<string, string> parameter) => parameter.Value.Trim()), (string encodingName) => encodingName.Length > 0).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						return ODataMediaType.EncodingFromName(text);
					}
				}
			}
			if (HttpUtils.CompareMediaTypeNames("text", this.type))
			{
				if (!HttpUtils.CompareMediaTypeNames("xml", this.subType))
				{
					return MediaTypeUtils.MissingEncoding;
				}
				return null;
			}
			else
			{
				if (HttpUtils.CompareMediaTypeNames("application", this.type) && HttpUtils.CompareMediaTypeNames("json", this.subType))
				{
					return MediaTypeUtils.FallbackEncoding;
				}
				return null;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004148 File Offset: 0x00002348
		internal string ToText()
		{
			return this.ToText(null);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004154 File Offset: 0x00002354
		internal string ToText(Encoding encoding)
		{
			if (this.parameters == null || !Enumerable.Any<KeyValuePair<string, string>>(this.parameters))
			{
				string text = this.FullTypeName;
				if (encoding != null)
				{
					text = string.Concat(new string[] { text, ";", "charset", "=", encoding.WebName });
				}
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(this.FullTypeName);
			foreach (KeyValuePair<string, string> keyValuePair in this.parameters)
			{
				if (!HttpUtils.CompareMediaTypeParameterNames("charset", keyValuePair.Key))
				{
					stringBuilder.Append(";");
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append("=");
					stringBuilder.Append(keyValuePair.Value);
				}
			}
			if (encoding != null)
			{
				stringBuilder.Append(";");
				stringBuilder.Append("charset");
				stringBuilder.Append("=");
				stringBuilder.Append(encoding.WebName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000427C File Offset: 0x0000247C
		private static Encoding EncodingFromName(string name)
		{
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(name);
			if (encodingFromCharsetName == null)
			{
				throw new ODataException(Strings.MediaType_EncodingNotSupported(name));
			}
			return encodingFromCharsetName;
		}

		// Token: 0x04000086 RID: 134
		private readonly IEnumerable<KeyValuePair<string, string>> parameters;

		// Token: 0x04000087 RID: 135
		private readonly string subType;

		// Token: 0x04000088 RID: 136
		private readonly string type;
	}
}
