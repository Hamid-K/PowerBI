using System;
using System.Runtime.InteropServices;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x02000329 RID: 809
	[StructLayout(LayoutKind.Explicit, Size = 256)]
	public struct Header
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x00065204 File Offset: 0x00063404
		internal static string VersionToString(ulong v)
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				(v >> 48) & 65535UL,
				(v >> 32) & 65535UL,
				(v >> 16) & 65535UL,
				v & 65535UL
			});
		}

		// Token: 0x04000A81 RID: 2689
		public const int HeaderSize = 256;

		// Token: 0x04000A82 RID: 2690
		public const ulong SignatureValue = 18672198525668675UL;

		// Token: 0x04000A83 RID: 2691
		public const ulong TailSignatureValue = 4849615937778106880UL;

		// Token: 0x04000A84 RID: 2692
		public const ulong WriterVersion = 281479271743493UL;

		// Token: 0x04000A85 RID: 2693
		public const ulong CanBeReadByVersion = 281479271743493UL;

		// Token: 0x04000A86 RID: 2694
		[FieldOffset(0)]
		public ulong Signature;

		// Token: 0x04000A87 RID: 2695
		[FieldOffset(8)]
		public ulong Version;

		// Token: 0x04000A88 RID: 2696
		[FieldOffset(16)]
		public ulong CompatibleVersion;

		// Token: 0x04000A89 RID: 2697
		[FieldOffset(24)]
		public long TableOfContentsOffset;

		// Token: 0x04000A8A RID: 2698
		[FieldOffset(32)]
		public long TailOffset;

		// Token: 0x04000A8B RID: 2699
		[FieldOffset(40)]
		public long RowCount;

		// Token: 0x04000A8C RID: 2700
		[FieldOffset(48)]
		public int ColumnCount;
	}
}
