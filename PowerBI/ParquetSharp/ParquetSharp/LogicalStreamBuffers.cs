using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200005F RID: 95
	[NullableContext(2)]
	[Nullable(0)]
	internal struct LogicalStreamBuffers<TPhysical>
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public LogicalStreamBuffers([Nullable(1)] TPhysical[] values, short[] defLevels, short[] repLevels)
		{
			this.Values = values;
			this.DefLevels = defLevels;
			this.RepLevels = repLevels;
			this.Length = values.Length;
			if (defLevels != null && defLevels.Length != this.Length)
			{
				throw new Exception(string.Format("Expected definition levels buffer length ({0}) to match values buffer length ({1}", defLevels.Length, values.Length));
			}
			if (repLevels != null && repLevels.Length != this.Length)
			{
				throw new Exception(string.Format("Expected repetition levels buffer length ({0}) to match values buffer length ({1}", repLevels.Length, values.Length));
			}
		}

		// Token: 0x040000BD RID: 189
		[Nullable(1)]
		public readonly TPhysical[] Values;

		// Token: 0x040000BE RID: 190
		public readonly short[] DefLevels;

		// Token: 0x040000BF RID: 191
		public readonly short[] RepLevels;

		// Token: 0x040000C0 RID: 192
		public readonly int Length;
	}
}
