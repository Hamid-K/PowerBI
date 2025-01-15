using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x02000121 RID: 289
	internal sealed class HttpHeaderValueElement
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x000195F7 File Offset: 0x000177F7
		public HttpHeaderValueElement(string name, string value, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<KeyValuePair<string, string>>>(parameters, "parameters");
			this.Name = name;
			this.Value = value;
			this.Parameters = parameters;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001962A File Offset: 0x0001782A
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00019632 File Offset: 0x00017832
		public string Name { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001963B File Offset: 0x0001783B
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00019643 File Offset: 0x00017843
		public string Value { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001964C File Offset: 0x0001784C
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00019654 File Offset: 0x00017854
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; private set; }

		// Token: 0x060007A0 RID: 1952 RVA: 0x00019660 File Offset: 0x00017860
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

		// Token: 0x060007A1 RID: 1953 RVA: 0x000196E4 File Offset: 0x000178E4
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
