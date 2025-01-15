using System;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000017 RID: 23
	public interface IStreamingStructureWriter : IDisposable
	{
		// Token: 0x06000076 RID: 118
		void BeginObject();

		// Token: 0x06000077 RID: 119
		void EndObject();

		// Token: 0x06000078 RID: 120
		void BeginArray();

		// Token: 0x06000079 RID: 121
		void EndArray();

		// Token: 0x0600007A RID: 122
		void BeginProperty(string name);

		// Token: 0x0600007B RID: 123
		void WriteValue(string value);

		// Token: 0x0600007C RID: 124
		void WriteValue(bool value);

		// Token: 0x0600007D RID: 125
		void WriteValue(int value);

		// Token: 0x0600007E RID: 126
		void WriteValue(long value);

		// Token: 0x0600007F RID: 127
		void WriteValue(double value);

		// Token: 0x06000080 RID: 128
		void WriteValue(decimal value);

		// Token: 0x06000081 RID: 129
		void WriteValue(DateTime value);

		// Token: 0x06000082 RID: 130
		void Flush();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000083 RID: 131
		long BytesWritten { get; }
	}
}
