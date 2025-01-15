using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D3 RID: 467
	internal interface IChunkedArrayBackedSerializer
	{
		// Token: 0x06000F25 RID: 3877
		bool TrySerialize(object userObject, bool isCompressionEnabled, out byte[][] serialized);

		// Token: 0x06000F26 RID: 3878
		bool TryDeserialize(byte[][] array, out object deserialized);
	}
}
