using System;
using System.Diagnostics;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000110 RID: 272
	[DebuggerDisplay("Length={Count}")]
	[Serializable]
	internal struct TransformationMatchSegment
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0003209B File Offset: 0x0003029B
		public int Count
		{
			get
			{
				return this.Segment.Count;
			}
		}

		// Token: 0x1700022B RID: 555
		public TransformationMatch this[int i]
		{
			get
			{
				return this.Segment.Array[this.Segment.Offset + i];
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000320C7 File Offset: 0x000302C7
		public TransformationMatchSegment(BinaryReader r, ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Segment = default(ArraySegment<TransformationMatch>);
			this.Read(r, tranMatchAllocator, intAllocator, byteAllocator);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000320E0 File Offset: 0x000302E0
		public TransformationMatchSegment Clone(ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			ArraySegment<TransformationMatch> arraySegment = tranMatchAllocator.New(this.Segment.Count);
			for (int i = 0; i < this.Count; i++)
			{
				arraySegment.Array[arraySegment.Offset + i] = this.Segment.Array[this.Segment.Offset + i].Clone(intAllocator, byteAllocator);
			}
			return new TransformationMatchSegment
			{
				Segment = arraySegment
			};
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0003215A File Offset: 0x0003035A
		public void Clear()
		{
			this.Segment = default(ArraySegment<TransformationMatch>);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00032168 File Offset: 0x00030368
		public void Write(BinaryWriter w)
		{
			w.Write(this.Segment.Count);
			for (int i = 0; i < this.Segment.Count; i++)
			{
				this.Segment.Array[this.Segment.Offset + i].Write(w);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000321C0 File Offset: 0x000303C0
		public void Read(BinaryReader r, ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			int num = r.ReadInt32();
			this.Segment = tranMatchAllocator.New(num);
			for (int i = 0; i < num; i++)
			{
				this.Segment.Array[this.Segment.Offset + i] = new TransformationMatch(r, allocator, byteAllocator);
			}
		}

		// Token: 0x04000455 RID: 1109
		private ArraySegment<TransformationMatch> Segment;
	}
}
