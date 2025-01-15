using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000120 RID: 288
	[DebuggerDisplay("MediaType [{ToText()}]")]
	public sealed class ODataMediaType
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x000275F0 File Offset: 0x000257F0
		public ODataMediaType(string type, string subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000275FB File Offset: 0x000257FB
		public ODataMediaType(string type, string subType, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			this.type = type;
			this.subType = subType;
			this.parameters = parameters;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00027618 File Offset: 0x00025818
		internal ODataMediaType(string type, string subType, KeyValuePair<string, string> parameter)
			: this(type, subType, new KeyValuePair<string, string>[] { parameter })
		{
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00027642 File Offset: 0x00025842
		public string SubType
		{
			get
			{
				return this.subType;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002764A File Offset: 0x0002584A
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00027652 File Offset: 0x00025852
		public IEnumerable<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002765A File Offset: 0x0002585A
		internal string FullTypeName
		{
			get
			{
				return this.type + "/" + this.subType;
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000276A0 File Offset: 0x000258A0
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

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000277BC File Offset: 0x000259BC
		internal string ToText()
		{
			return this.ToText(null);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000277C8 File Offset: 0x000259C8
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

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000278F4 File Offset: 0x00025AF4
		private static Encoding EncodingFromName(string name)
		{
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(name);
			if (encodingFromCharsetName == null)
			{
				throw new ODataException(Strings.MediaType_EncodingNotSupported(name));
			}
			return encodingFromCharsetName;
		}

		// Token: 0x04000455 RID: 1109
		private readonly IEnumerable<KeyValuePair<string, string>> parameters;

		// Token: 0x04000456 RID: 1110
		private readonly string subType;

		// Token: 0x04000457 RID: 1111
		private readonly string type;
	}
}
