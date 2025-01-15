using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public struct WeightedTransformation
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x00032213 File Offset: 0x00030413
		public WeightedTransformation(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Transformation = new Transformation(r, allocator, byteAllocator);
			this.ToWeights = default(ArraySegment<int>);
			this.ToWeights = this.ToWeights.Read(r, allocator);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00032242 File Offset: 0x00030442
		public void Clear()
		{
			this.Transformation = default(Transformation);
			this.ToWeights = default(ArraySegment<int>);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003225C File Offset: 0x0003045C
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Transformation.Read(r, allocator, byteAllocator);
			this.ToWeights = this.ToWeights.Read(r, allocator);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0003227F File Offset: 0x0003047F
		public void Write(BinaryWriter w)
		{
			this.Transformation.Write(w);
			this.ToWeights.Write(w);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00032299 File Offset: 0x00030499
		public int GetToWeight(int tokenIndex)
		{
			return this.ToWeights.Array[this.ToWeights.Offset + tokenIndex];
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000322B4 File Offset: 0x000304B4
		public void SetToWeight(int tokenIndex, int weight)
		{
			this.ToWeights.Array[this.ToWeights.Offset + tokenIndex] = weight;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000322D0 File Offset: 0x000304D0
		public void SetToWeights(ArraySegment<int> weights)
		{
			this.ToWeights = weights;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000322DC File Offset: 0x000304DC
		public WeightedTransformation Clone(ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			return new WeightedTransformation
			{
				Transformation = this.Transformation.Clone(allocator, byteAllocator),
				ToWeights = this.ToWeights.Clone(allocator)
			};
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0003231C File Offset: 0x0003051C
		public static explicit operator WeightedTransformation(Transformation t)
		{
			return new WeightedTransformation
			{
				Transformation = t,
				ToWeights = default(ArraySegment<int>)
			};
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00032347 File Offset: 0x00030547
		public static explicit operator Transformation(WeightedTransformation wt)
		{
			return wt.Transformation;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0003234F File Offset: 0x0003054F
		internal bool IsUnitRule
		{
			get
			{
				return this.Transformation.IsUnitRule;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0003235C File Offset: 0x0003055C
		internal bool IsUnitMulti
		{
			get
			{
				return this.Transformation.IsUnitRule;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00032369 File Offset: 0x00030569
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00032376 File Offset: 0x00030576
		public TransformationType Type
		{
			get
			{
				return this.Transformation.Type;
			}
			set
			{
				this.Transformation.Type = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00032384 File Offset: 0x00030584
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00032391 File Offset: 0x00030591
		public TokenSequence To
		{
			get
			{
				return this.Transformation.To;
			}
			set
			{
				this.Transformation.To = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0003239F File Offset: 0x0003059F
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x000323AC File Offset: 0x000305AC
		public TokenSequence From
		{
			get
			{
				return this.Transformation.From;
			}
			set
			{
				this.Transformation.From = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000323BA File Offset: 0x000305BA
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x000323C7 File Offset: 0x000305C7
		public ArraySegment<byte> Metadata
		{
			get
			{
				return this.Transformation.Metadata;
			}
			set
			{
				this.Transformation.Metadata = value;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000323D5 File Offset: 0x000305D5
		public override string ToString()
		{
			return this.Transformation.ToString();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000323E8 File Offset: 0x000305E8
		public override bool Equals(object obj)
		{
			return this.Transformation.Equals(obj);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000323FC File Offset: 0x000305FC
		public bool Equals(Transformation t)
		{
			return this.Transformation.Equals(t);
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0003240A File Offset: 0x0003060A
		public long MemoryUsage
		{
			get
			{
				return this.Transformation.MemoryUsage + (long)this.ToWeights.Count;
			}
		}

		// Token: 0x04000456 RID: 1110
		public Transformation Transformation;

		// Token: 0x04000457 RID: 1111
		public ArraySegment<int> ToWeights;
	}
}
