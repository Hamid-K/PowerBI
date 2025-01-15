using System;

namespace System.Buffers
{
	// Token: 0x02000024 RID: 36
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000B4DD File Offset: 0x000096DD
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000B4E5 File Offset: 0x000096E5
		public ReadOnlyMemory<T> Memory { get; protected set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000B4EE File Offset: 0x000096EE
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000B4F6 File Offset: 0x000096F6
		public ReadOnlySequenceSegment<T> Next { get; protected set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000B4FF File Offset: 0x000096FF
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000B507 File Offset: 0x00009707
		public long RunningIndex { get; protected set; }
	}
}
