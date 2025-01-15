using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000387 RID: 903
	internal interface ISerializationReader : IDisposable
	{
		// Token: 0x06001FCC RID: 8140
		bool ReadBoolean();

		// Token: 0x06001FCD RID: 8141
		byte ReadByte();

		// Token: 0x06001FCE RID: 8142
		short ReadInt16();

		// Token: 0x06001FCF RID: 8143
		int ReadInt32();

		// Token: 0x06001FD0 RID: 8144
		long ReadInt64();

		// Token: 0x06001FD1 RID: 8145
		string ReadString();

		// Token: 0x06001FD2 RID: 8146
		int Read(byte[] buffer, int startIndex, int count);

		// Token: 0x06001FD3 RID: 8147
		ushort ReadUInt16();

		// Token: 0x06001FD4 RID: 8148
		uint ReadUInt32();

		// Token: 0x06001FD5 RID: 8149
		ulong ReadUInt64();

		// Token: 0x06001FD6 RID: 8150
		double ReadDouble();

		// Token: 0x06001FD7 RID: 8151
		byte[][] ReadChunkedByteArray();
	}
}
