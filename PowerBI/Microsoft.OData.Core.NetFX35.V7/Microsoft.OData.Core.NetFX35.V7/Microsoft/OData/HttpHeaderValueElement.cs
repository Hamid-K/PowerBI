using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000010 RID: 16
	public sealed class HttpHeaderValueElement
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002EA0 File Offset: 0x000010A0
		public HttpHeaderValueElement(string name, string value, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyValuePair<string, string>>>(parameters, "parameters");
			this.Name = name;
			this.Value = value;
			this.Parameters = parameters;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002ED4 File Offset: 0x000010D4
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002EDC File Offset: 0x000010DC
		public string Name { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EE5 File Offset: 0x000010E5
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002EED File Offset: 0x000010ED
		public string Value { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EF6 File Offset: 0x000010F6
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002EFE File Offset: 0x000010FE
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002F08 File Offset: 0x00001108
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

		// Token: 0x06000065 RID: 101 RVA: 0x00002F8C File Offset: 0x0000118C
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
