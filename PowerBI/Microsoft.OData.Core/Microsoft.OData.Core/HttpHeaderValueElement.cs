using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000037 RID: 55
	public sealed class HttpHeaderValueElement
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x000053B8 File Offset: 0x000035B8
		public HttpHeaderValueElement(string name, string value, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyValuePair<string, string>>>(parameters, "parameters");
			this.Name = name;
			this.Value = value;
			this.Parameters = parameters;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000053EC File Offset: 0x000035EC
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000053F4 File Offset: 0x000035F4
		public string Name { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000053FD File Offset: 0x000035FD
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00005405 File Offset: 0x00003605
		public string Value { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000540E File Offset: 0x0000360E
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005416 File Offset: 0x00003616
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; private set; }

		// Token: 0x060001E8 RID: 488 RVA: 0x00005420 File Offset: 0x00003620
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			HttpHeaderValueElement.AppendNameValuePair(stringBuilder, this.Name, this.Value);
			foreach (KeyValuePair<string, string> keyValuePair in this.Parameters)
			{
				stringBuilder.Append(";");
				HttpHeaderValueElement.AppendNameValuePair(stringBuilder, keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000054A4 File Offset: 0x000036A4
		private static void AppendNameValuePair(StringBuilder stringBuilder, string name, string value)
		{
			stringBuilder.Append(name);
			if (value != null)
			{
				stringBuilder.Append("=");
				stringBuilder.Append(value);
			}
		}
	}
}
