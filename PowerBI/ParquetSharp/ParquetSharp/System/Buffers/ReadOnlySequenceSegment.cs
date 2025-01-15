using System;

namespace System.Buffers
{
	// Token: 0x020000E2 RID: 226
	internal abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00022CE0 File Offset: 0x00020EE0
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00022CE8 File Offset: 0x00020EE8
		public ReadOnlyMemory<T> Memory { get; protected set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00022CF4 File Offset: 0x00020EF4
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00022CFC File Offset: 0x00020EFC
		public ReadOnlySequenceSegment<T> Next { get; protected set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00022D08 File Offset: 0x00020F08
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00022D10 File Offset: 0x00020F10
		public long RunningIndex { get; protected set; }
	}
}
