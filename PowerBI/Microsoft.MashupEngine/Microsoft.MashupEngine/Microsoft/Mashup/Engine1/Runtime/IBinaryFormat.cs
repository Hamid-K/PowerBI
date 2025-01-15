using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001337 RID: 4919
	public interface IBinaryFormat
	{
		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x060081D4 RID: 33236
		BinaryFormatType BinaryFormatType { get; }

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x060081D5 RID: 33237
		bool CanReadUInt64 { get; }

		// Token: 0x060081D6 RID: 33238
		Value ReadValue(BinaryFormatReader reader);

		// Token: 0x060081D7 RID: 33239
		bool TryReadValue(BinaryFormatReader reader, out Value value);

		// Token: 0x060081D8 RID: 33240
		ulong ReadUInt64(BinaryFormatReader reader);

		// Token: 0x060081D9 RID: 33241
		bool TryReadUInt64(BinaryFormatReader reader, out ulong value);

		// Token: 0x060081DA RID: 33242
		Stream ReadStream(BinaryFormatReader reader);

		// Token: 0x060081DB RID: 33243
		IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader);
	}
}
