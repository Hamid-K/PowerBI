using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200026F RID: 623
	[DebuggerDisplay("MediaType [{ToText()}]")]
	internal sealed class MediaType
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x00049548 File Offset: 0x00047748
		internal MediaType(string type, string subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00049553 File Offset: 0x00047753
		internal MediaType(string type, string subType, params KeyValuePair<string, string>[] parameters)
			: this(type, subType, (IList<KeyValuePair<string, string>>)parameters)
		{
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00049563 File Offset: 0x00047763
		internal MediaType(string type, string subType, IList<KeyValuePair<string, string>> parameters)
		{
			this.type = type;
			this.subType = subType;
			this.parameters = parameters;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00049580 File Offset: 0x00047780
		internal static Encoding FallbackEncoding
		{
			get
			{
				return MediaTypeUtils.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00049587 File Offset: 0x00047787
		internal static Encoding MissingEncoding
		{
			get
			{
				return Encoding.GetEncoding("ISO-8859-1", new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0004959D File Offset: 0x0004779D
		internal string FullTypeName
		{
			get
			{
				return this.type + "/" + this.subType;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x000495B5 File Offset: 0x000477B5
		internal string SubTypeName
		{
			get
			{
				return this.subType;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000495BD File Offset: 0x000477BD
		internal string TypeName
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000495C5 File Offset: 0x000477C5
		internal IList<KeyValuePair<string, string>> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000495FC File Offset: 0x000477FC
		internal Encoding SelectEncoding()
		{
			if (this.parameters != null)
			{
				using (IEnumerator<string> enumerator = Enumerable.Where<string>(Enumerable.Select<KeyValuePair<string, string>, string>(Enumerable.Where<KeyValuePair<string, string>>(this.parameters, (KeyValuePair<string, string> parameter) => HttpUtils.CompareMediaTypeParameterNames("charset", parameter.Key)), (KeyValuePair<string, string> parameter) => parameter.Value.Trim()), (string encodingName) => encodingName.Length > 0).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						return MediaType.EncodingFromName(text);
					}
				}
			}
			if (HttpUtils.CompareMediaTypeNames("text", this.type))
			{
				if (!HttpUtils.CompareMediaTypeNames("xml", this.subType))
				{
					return MediaType.MissingEncoding;
				}
				return null;
			}
			else
			{
				if (HttpUtils.CompareMediaTypeNames("application", this.type) && HttpUtils.CompareMediaTypeNames("json", this.subType))
				{
					return MediaType.FallbackEncoding;
				}
				return null;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00049718 File Offset: 0x00047918
		internal string ToText()
		{
			return this.ToText(null);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00049724 File Offset: 0x00047924
		internal string ToText(Encoding encoding)
		{
			if (this.parameters == null || this.parameters.Count == 0)
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

		// Token: 0x06001391 RID: 5009 RVA: 0x00049850 File Offset: 0x00047A50
		private static Encoding EncodingFromName(string name)
		{
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(name);
			if (encodingFromCharsetName == null)
			{
				throw new ODataException(Strings.MediaType_EncodingNotSupported(name));
			}
			return encodingFromCharsetName;
		}

		// Token: 0x04000746 RID: 1862
		private readonly IList<KeyValuePair<string, string>> parameters;

		// Token: 0x04000747 RID: 1863
		private readonly string subType;

		// Token: 0x04000748 RID: 1864
		private readonly string type;
	}
}
