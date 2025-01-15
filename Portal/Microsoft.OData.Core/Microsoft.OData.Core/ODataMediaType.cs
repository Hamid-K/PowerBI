using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000049 RID: 73
	[DebuggerDisplay("MediaType [{ToText()}]")]
	public sealed class ODataMediaType
	{
		// Token: 0x06000243 RID: 579 RVA: 0x000065C4 File Offset: 0x000047C4
		public ODataMediaType(string type, string subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000065CF File Offset: 0x000047CF
		public ODataMediaType(string type, string subType, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			this.type = type;
			this.subType = subType;
			this.parameters = parameters;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000065EC File Offset: 0x000047EC
		internal ODataMediaType(string type, string subType, KeyValuePair<string, string> parameter)
			: this(type, subType, new KeyValuePair<string, string>[] { parameter })
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006604 File Offset: 0x00004804
		public string SubType
		{
			get
			{
				return this.subType;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000660C File Offset: 0x0000480C
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00006614 File Offset: 0x00004814
		public IEnumerable<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000661C File Offset: 0x0000481C
		internal string FullTypeName
		{
			get
			{
				return this.type + "/" + this.subType;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006634 File Offset: 0x00004834
		internal Encoding SelectEncoding()
		{
			if (this.parameters != null)
			{
				using (IEnumerator<string> enumerator = (from parameter in this.parameters
					where HttpUtils.CompareMediaTypeParameterNames("charset", parameter.Key)
					select parameter.Value.Trim() into encodingName
					where encodingName.Length > 0
					select encodingName).GetEnumerator())
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

		// Token: 0x0600024B RID: 587 RVA: 0x00006754 File Offset: 0x00004954
		internal string ToText()
		{
			return this.ToText(null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006760 File Offset: 0x00004960
		internal string ToText(Encoding encoding)
		{
			if (this.parameters == null || !this.parameters.Any<KeyValuePair<string, string>>())
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

		// Token: 0x0600024D RID: 589 RVA: 0x00006888 File Offset: 0x00004A88
		private static Encoding EncodingFromName(string name)
		{
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(name);
			if (encodingFromCharsetName == null)
			{
				throw new ODataException(Strings.MediaType_EncodingNotSupported(name));
			}
			return encodingFromCharsetName;
		}

		// Token: 0x040000F6 RID: 246
		private readonly IEnumerable<KeyValuePair<string, string>> parameters;

		// Token: 0x040000F7 RID: 247
		private readonly string subType;

		// Token: 0x040000F8 RID: 248
		private readonly string type;
	}
}
