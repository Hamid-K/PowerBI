using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000379 RID: 889
	internal interface IBinarySerializable2 : IBinarySerializable
	{
		// Token: 0x06001F7B RID: 8059
		int GetSerializedSize();

		// Token: 0x06001F7C RID: 8060
		byte[][] WritePayloadDetails(ISerializationWriter writer, out int payloadLength);
	}
}
