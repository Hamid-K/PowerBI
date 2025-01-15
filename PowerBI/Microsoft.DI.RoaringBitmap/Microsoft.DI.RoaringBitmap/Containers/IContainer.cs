using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.DI.RoaringBitmap.Containers
{
	// Token: 0x0200000F RID: 15
	internal interface IContainer
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007A RID: 122
		int ArraySizeInBytes { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007B RID: 123
		int Cardinality { get; }

		// Token: 0x0600007C RID: 124
		IContainer Add(char index);

		// Token: 0x0600007D RID: 125
		bool IsEmpty();

		// Token: 0x0600007E RID: 126
		bool Contains(char index);

		// Token: 0x0600007F RID: 127
		IContainer Remove(char index);

		// Token: 0x06000080 RID: 128
		IContainer RunOptimize();

		// Token: 0x06000081 RID: 129
		int NumberOfRuns();

		// Token: 0x06000082 RID: 130
		IEnumerable<uint> Values();

		// Token: 0x06000083 RID: 131
		void Serialize(BinaryWriter binaryWriter);

		// Token: 0x06000084 RID: 132
		void Deserialize(BinaryReader binaryReader, int cardinality);
	}
}
