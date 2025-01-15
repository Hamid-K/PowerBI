using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D0 RID: 208
	public interface IJsonWriter
	{
		// Token: 0x06000730 RID: 1840
		void WriteStartArray();

		// Token: 0x06000731 RID: 1841
		void WriteStartObject();

		// Token: 0x06000732 RID: 1842
		void WriteEndArray();

		// Token: 0x06000733 RID: 1843
		void WriteEndObject();

		// Token: 0x06000734 RID: 1844
		void WriteComma();

		// Token: 0x06000735 RID: 1845
		void WriteProperty(string name, string value);

		// Token: 0x06000736 RID: 1846
		void WriteProperty(string name, bool? value);

		// Token: 0x06000737 RID: 1847
		void WriteProperty(string name, int? value);

		// Token: 0x06000738 RID: 1848
		void WriteProperty(string name, double? value);

		// Token: 0x06000739 RID: 1849
		void WriteProperty(string name, TimeSpan? value);

		// Token: 0x0600073A RID: 1850
		void WriteProperty(string name, DateTimeOffset? value);

		// Token: 0x0600073B RID: 1851
		void WriteProperty(string name, IDictionary<string, double> values);

		// Token: 0x0600073C RID: 1852
		void WriteProperty(string name, IDictionary<string, string> values);

		// Token: 0x0600073D RID: 1853
		void WritePropertyName(string name);

		// Token: 0x0600073E RID: 1854
		void WriteRawValue(object value);
	}
}
