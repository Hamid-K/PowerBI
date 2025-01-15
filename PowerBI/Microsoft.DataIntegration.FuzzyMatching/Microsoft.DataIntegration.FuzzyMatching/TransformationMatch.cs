using System;
using System.Diagnostics;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010F RID: 271
	[DebuggerDisplay("Pos={Position} TransformationMatchId={TransformationMatchId} {Transformation.ToString()}")]
	[Serializable]
	public struct TransformationMatch : IMemoryUsage, ITransformationMatch
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00031F87 File Offset: 0x00030187
		int ITransformationMatch.Position
		{
			get
			{
				return this.Position;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00031F8F File Offset: 0x0003018F
		Transformation ITransformationMatch.Transformation
		{
			get
			{
				return this.Transformation;
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00031F97 File Offset: 0x00030197
		public TransformationMatch(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Position = r.ReadInt32();
			this.Transformation = new Transformation(r, allocator, byteAllocator);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00031FB3 File Offset: 0x000301B3
		public TransformationMatch(TransformationMatch source)
		{
			this.Position = source.Position;
			this.Transformation = source.Transformation;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00031FD0 File Offset: 0x000301D0
		public TransformationMatch Clone(ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			return new TransformationMatch
			{
				Position = this.Position,
				Transformation = this.Transformation.Clone(intAllocator, byteAllocator)
			};
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00032007 File Offset: 0x00030207
		public void Clear()
		{
			this.Transformation = default(Transformation);
			this.Position = -1;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003201C File Offset: 0x0003021C
		public void Write(BinaryWriter w)
		{
			w.Write(this.Position);
			this.Transformation.Write(w);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00032036 File Offset: 0x00030236
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Position = r.ReadInt32();
			this.Transformation.Read(r, allocator, byteAllocator);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00032052 File Offset: 0x00030252
		public bool IsNull
		{
			get
			{
				return int.MinValue == this.Position;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00032061 File Offset: 0x00030261
		public override string ToString()
		{
			return this.Transformation.ToString();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00032074 File Offset: 0x00030274
		public void Reset()
		{
			this.Position = -1;
			this.Transformation = Transformation.Null;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00032088 File Offset: 0x00030288
		public long MemoryUsage
		{
			get
			{
				return 16L + this.Transformation.MemoryUsage;
			}
		}

		// Token: 0x04000452 RID: 1106
		public static readonly TransformationMatch Empty;

		// Token: 0x04000453 RID: 1107
		public int Position;

		// Token: 0x04000454 RID: 1108
		public Transformation Transformation;
	}
}
