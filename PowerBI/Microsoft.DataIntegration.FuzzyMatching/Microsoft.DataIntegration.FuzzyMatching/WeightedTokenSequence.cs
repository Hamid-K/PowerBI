using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000EE RID: 238
	[DebuggerDisplay("Tokens={ToString()} Weights={ToString()}")]
	[Serializable]
	public struct WeightedTokenSequence : INullable
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0002BC02 File Offset: 0x00029E02
		public WeightedTokenSequence(ArraySegment<int> tokens, ArraySegment<int> weights)
		{
			this.Tokens = tokens;
			this.Weights = weights;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002BC17 File Offset: 0x00029E17
		public WeightedTokenSequence(params int[] tokens)
		{
			this.Tokens = new TokenSequence(tokens);
			this.Weights = default(ArraySegment<int>);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002BC34 File Offset: 0x00029E34
		internal WeightedTokenSequence(bool initWeightsToOne, params int[] tokens)
		{
			this.Tokens = new TokenSequence(tokens);
			if (initWeightsToOne)
			{
				int[] array = new int[tokens.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 1;
				}
				this.Weights = new ArraySegment<int>(array);
				return;
			}
			this.Weights = default(ArraySegment<int>);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002BC84 File Offset: 0x00029E84
		public WeightedTokenSequence(int[] tokens, int[] weights)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("Tokens may not be null.");
			}
			this.Tokens = new TokenSequence(tokens);
			if (weights == null)
			{
				this.Weights = default(ArraySegment<int>);
				return;
			}
			if (tokens.Length != weights.Length && weights.Length != 0)
			{
				throw new ArgumentException("Token array Length must be equal to token weight array Length.");
			}
			this.Weights = new ArraySegment<int>(weights);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002BCE0 File Offset: 0x00029EE0
		public WeightedTokenSequence Clone(ISegmentAllocator<int> intAllocator)
		{
			return new WeightedTokenSequence
			{
				Tokens = this.Tokens.Clone(intAllocator),
				Weights = this.Weights.Clone(intAllocator)
			};
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002BD21 File Offset: 0x00029F21
		public void Clear()
		{
			this.Tokens.Clear();
			this.Weights = default(ArraySegment<int>);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0002BD3A File Offset: 0x00029F3A
		public int Count
		{
			get
			{
				return this.Tokens.Count;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002BD47 File Offset: 0x00029F47
		public bool IsNull
		{
			get
			{
				return this.Tokens.Array == null;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0002BD58 File Offset: 0x00029F58
		public static WeightedTokenSequence Null
		{
			get
			{
				return default(WeightedTokenSequence);
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002BD6E File Offset: 0x00029F6E
		public void Write(BinaryWriter w)
		{
			this.Tokens.Write(w);
			this.Weights.Write(w);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002BD88 File Offset: 0x00029F88
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator)
		{
			this.Tokens.Read(r, allocator);
			this.Weights = this.Weights.Read(r, allocator);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002BDAA File Offset: 0x00029FAA
		public static WeightedTokenSequence Parse(SqlString s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002BDB1 File Offset: 0x00029FB1
		public int TotalWeight()
		{
			return Enumerable.Sum(this.Weights);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002BDC8 File Offset: 0x00029FC8
		public int GetWeight(int index)
		{
			return this.Weights.Array[this.Weights.Offset + index];
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Count * 4);
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" ");
				}
				int num = -1;
				if (this.Weights.Array != null && i < this.Weights.Count)
				{
					num = this.Weights.Array[this.Weights.Offset + i];
				}
				stringBuilder.AppendFormat("{0}({1})", this.Tokens[i], num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002BE80 File Offset: 0x0002A080
		public string ToVerboseString(ITokenIdProvider tokenIdProvider)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Count * 4);
			stringBuilder.Append(this.Tokens.ToString());
			for (int i = 0; i < this.Weights.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" ");
				}
				int num = this.Weights.Array[this.Weights.Offset + i];
				stringBuilder.AppendFormat("{0}({1})", tokenIdProvider.SupportsGetToken ? tokenIdProvider.GetToken(this.Tokens[i]).ToString() : this.Tokens[i].ToString(), num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002BF4D File Offset: 0x0002A14D
		public override bool Equals(object obj)
		{
			return (obj is TokenSequence && this.Equals((TokenSequence)obj)) || (obj is WeightedTokenSequence && this.Equals(((WeightedTokenSequence)obj).Tokens));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002BF82 File Offset: 0x0002A182
		public bool Equals(WeightedTokenSequence t)
		{
			return this.Tokens.Equals(t.Tokens);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002BF98 File Offset: 0x0002A198
		public bool Equals(TokenSequence t)
		{
			if (this.Tokens.Array == t.Array && this.Tokens.Offset == t.Offset && this.Tokens.Count == t.Count)
			{
				return true;
			}
			if (t.Count == this.Tokens.Count)
			{
				for (int i = 0; i < t.Count; i++)
				{
					if (t[i] != this.Tokens.Array[this.Tokens.Offset + i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0002C031 File Offset: 0x0002A231
		public long MemoryUsage
		{
			get
			{
				return this.Tokens.MemoryUsage + 16L + (long)(this.Weights.Count * 4);
			}
		}

		// Token: 0x040003AC RID: 940
		private const int UnsetWeight = -1;

		// Token: 0x040003AD RID: 941
		public TokenSequence Tokens;

		// Token: 0x040003AE RID: 942
		public ArraySegment<int> Weights;
	}
}
