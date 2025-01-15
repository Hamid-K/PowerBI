using System;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000016 RID: 22
	public interface IStreamingStructureEncodedWriter : IStreamingStructureWriter, IDisposable
	{
		// Token: 0x06000069 RID: 105
		void WriteTypeEncodedProperty(string name, object value);

		// Token: 0x0600006A RID: 106
		void WriteSimpleEncodedProperty(string name, object value);

		// Token: 0x0600006B RID: 107
		void WriteProperty(string name, string value);

		// Token: 0x0600006C RID: 108
		void WriteJsonEncodedProperty(string name, string value);

		// Token: 0x0600006D RID: 109
		void WriteJsonEncodedStringProperty(string name, string value);

		// Token: 0x0600006E RID: 110
		void WriteProperty(string name, bool value);

		// Token: 0x0600006F RID: 111
		void WriteProperty(string name, int value);

		// Token: 0x06000070 RID: 112
		void WriteProperty(string name, DateTimeOffset value);

		// Token: 0x06000071 RID: 113
		void WriteTypeEncodedValue(object value);

		// Token: 0x06000072 RID: 114
		void WriteSimpleEncodedValue(object value);

		// Token: 0x06000073 RID: 115
		void WriteJsonEncodedValue(string value);

		// Token: 0x06000074 RID: 116
		void WriteJsonEncodedString(string value);

		// Token: 0x06000075 RID: 117
		void WriteValue(DateTimeOffset value);
	}
}
