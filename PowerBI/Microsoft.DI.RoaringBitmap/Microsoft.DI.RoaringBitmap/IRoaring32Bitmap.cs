using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.DI.RoaringBitmap
{
	// Token: 0x02000005 RID: 5
	internal interface IRoaring32Bitmap
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10
		ulong Cardinality { get; }

		// Token: 0x0600000B RID: 11
		void Add(IEnumerable<uint> indexList);

		// Token: 0x0600000C RID: 12
		void Add(uint index);

		// Token: 0x0600000D RID: 13
		void Remove(uint index);

		// Token: 0x0600000E RID: 14
		bool IsEmpty();

		// Token: 0x0600000F RID: 15
		bool Contains(uint index);

		// Token: 0x06000010 RID: 16
		IEnumerable<T> Values<T>(uint msbToAdd = 0U);

		// Token: 0x06000011 RID: 17
		void Serialize(Stream stream);

		// Token: 0x06000012 RID: 18
		void Deserialize(Stream stream);

		// Token: 0x06000013 RID: 19
		int SerializedSizeInBytes();
	}
}
