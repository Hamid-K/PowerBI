using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000259 RID: 601
	internal interface IBinarySerializable
	{
		// Token: 0x06001452 RID: 5202
		void ReadStream(ISerializationReader reader);

		// Token: 0x06001453 RID: 5203
		void WriteStream(ISerializationWriter writer);
	}
}
