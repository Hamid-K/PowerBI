using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000096 RID: 150
	internal sealed class HttpHeaderValueElement
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00014E3A File Offset: 0x0001303A
		public HttpHeaderValueElement(string name, string value, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyValuePair<string, string>>>(parameters, "parameters");
			this.Name = name;
			this.Value = value;
			this.Parameters = parameters;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00014E6D File Offset: 0x0001306D
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00014E75 File Offset: 0x00013075
		public string Name { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00014E7E File Offset: 0x0001307E
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00014E86 File Offset: 0x00013086
		public string Value { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00014E8F File Offset: 0x0001308F
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00014E97 File Offset: 0x00013097
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; private set; }

		// Token: 0x060005CD RID: 1485 RVA: 0x00014EA0 File Offset: 0x000130A0
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

		// Token: 0x060005CE RID: 1486 RVA: 0x00014F24 File Offset: 0x00013124
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
