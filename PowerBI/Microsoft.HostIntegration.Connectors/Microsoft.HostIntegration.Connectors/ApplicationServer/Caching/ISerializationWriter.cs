using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000385 RID: 901
	internal interface ISerializationWriter : IDisposable
	{
		// Token: 0x06001FAE RID: 8110
		void Write(bool value);

		// Token: 0x06001FAF RID: 8111
		void Write(byte value);

		// Token: 0x06001FB0 RID: 8112
		void Write(short value);

		// Token: 0x06001FB1 RID: 8113
		void Write(int value);

		// Token: 0x06001FB2 RID: 8114
		void Write(long value);

		// Token: 0x06001FB3 RID: 8115
		void Write(string value);

		// Token: 0x06001FB4 RID: 8116
		void Write(double value);

		// Token: 0x06001FB5 RID: 8117
		void Write(byte[] buffer, int startIndex, int count);

		// Token: 0x06001FB6 RID: 8118
		void Write(byte[] buffer);

		// Token: 0x06001FB7 RID: 8119
		void Write(byte[][] buffer);

		// Token: 0x06001FB8 RID: 8120
		void Write(ulong value);

		// Token: 0x06001FB9 RID: 8121
		void Write(uint value);

		// Token: 0x06001FBA RID: 8122
		void Write(ushort value);
	}
}
