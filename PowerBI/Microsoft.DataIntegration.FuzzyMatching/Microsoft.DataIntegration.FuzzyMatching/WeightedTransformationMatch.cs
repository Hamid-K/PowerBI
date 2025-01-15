using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public struct WeightedTransformationMatch : ITransformationMatch
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00032424 File Offset: 0x00030624
		public bool IsUnitRule
		{
			get
			{
				return this.Transformation.IsUnitRule;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00032431 File Offset: 0x00030631
		int ITransformationMatch.Position
		{
			get
			{
				return this.Position;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00032439 File Offset: 0x00030639
		Transformation ITransformationMatch.Transformation
		{
			get
			{
				return this.Transformation.Transformation;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00032448 File Offset: 0x00030648
		public WeightedTransformationMatch Clone(ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			return new WeightedTransformationMatch
			{
				Position = this.Position,
				Transformation = this.Transformation.Clone(intAllocator, byteAllocator)
			};
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0003247F File Offset: 0x0003067F
		public long MemoryUsage
		{
			get
			{
				return 8L + this.Transformation.MemoryUsage;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003248F File Offset: 0x0003068F
		public void Clear()
		{
			this.Transformation = default(WeightedTransformation);
			this.Position = -1;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000324A4 File Offset: 0x000306A4
		public void SetTransformationToWeights(ArraySegment<int> weights)
		{
			this.Transformation.SetToWeights(weights);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000324B2 File Offset: 0x000306B2
		public WeightedTransformationMatch(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Position = r.ReadInt32();
			this.Transformation = new WeightedTransformation(r, allocator, byteAllocator);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000324CE File Offset: 0x000306CE
		public void Write(BinaryWriter w)
		{
			w.Write(this.Position);
			this.Transformation.Write(w);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000324E8 File Offset: 0x000306E8
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Position = r.ReadInt32();
			this.Transformation = new WeightedTransformation(r, allocator, byteAllocator);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00032504 File Offset: 0x00030704
		public static explicit operator WeightedTransformationMatch(TransformationMatch tm)
		{
			return new WeightedTransformationMatch
			{
				Position = tm.Position,
				Transformation = (WeightedTransformation)tm.Transformation
			};
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003253C File Offset: 0x0003073C
		public static explicit operator TransformationMatch(WeightedTransformationMatch wtm)
		{
			return new TransformationMatch
			{
				Position = wtm.Position,
				Transformation = (Transformation)wtm.Transformation
			};
		}

		// Token: 0x04000458 RID: 1112
		public int Position;

		// Token: 0x04000459 RID: 1113
		public WeightedTransformation Transformation;
	}
}
