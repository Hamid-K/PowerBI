using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000ED RID: 237
	[DebuggerDisplay("Tokens={ToString()}")]
	[Serializable]
	public struct TokenSequence : IEnumerable<int>, IEnumerable, IMemoryUsage, IArraySegment<int>, IXmlSerializable
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0002B673 File Offset: 0x00029873
		public int[] Array
		{
			get
			{
				return this._Array;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0002B67B File Offset: 0x0002987B
		public int Offset
		{
			get
			{
				return this._Offset;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0002B683 File Offset: 0x00029883
		public int Count
		{
			get
			{
				return this._Count;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002B68B File Offset: 0x0002988B
		int[] IArraySegment<int>.Array
		{
			get
			{
				return this.Array;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0002B693 File Offset: 0x00029893
		int IArraySegment<int>.Offset
		{
			get
			{
				return this.Offset;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002B69B File Offset: 0x0002989B
		int IArraySegment<int>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public static TokenSequence Create(IEnumerable<StringExtent> tokens, int domainId, ITokenIdProvider tokenIdProvider, ArraySegmentBuilder<int> segmentBuilder, ISegmentAllocator<int> intAllocator)
		{
			segmentBuilder.Reset();
			foreach (StringExtent stringExtent in tokens)
			{
				segmentBuilder.Add(tokenIdProvider.GetOrCreateTokenId(stringExtent, domainId));
			}
			return new TokenSequence(segmentBuilder.ToSegment(intAllocator));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002B708 File Offset: 0x00029908
		public static implicit operator TokenSequence(ArraySegment<int> tokens)
		{
			return new TokenSequence(tokens);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002B710 File Offset: 0x00029910
		public static implicit operator TokenSequence(ArraySegmentBuilder<int> tokens)
		{
			return new TokenSequence(tokens);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002B71D File Offset: 0x0002991D
		public TokenSequence(BinaryReader r, ISegmentAllocator<int> allocator)
		{
			this._Array = null;
			this._Offset = 0;
			this._Count = 0;
			this.Read(r, allocator);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002B73C File Offset: 0x0002993C
		public TokenSequence(int[] array, int offset, int count)
		{
			this._Array = array;
			this._Offset = offset;
			this._Count = count;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002B753 File Offset: 0x00029953
		public TokenSequence(ArraySegment<int> segment)
		{
			this._Array = segment.Array;
			this._Offset = segment.Offset;
			this._Count = segment.Count;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002B77C File Offset: 0x0002997C
		public TokenSequence(int[] tokens, int len)
		{
			this._Array = tokens;
			this._Offset = 0;
			this._Count = len;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0002B793 File Offset: 0x00029993
		public TokenSequence(params int[] tokens)
		{
			this._Array = tokens;
			this._Offset = 0;
			this._Count = tokens.Length;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002B7AC File Offset: 0x000299AC
		public TokenSequence(TokenSequence tokenSequence, ISegmentAllocator<int> allocator)
		{
			if (tokenSequence.Count > 0)
			{
				ArraySegment<int> arraySegment = allocator.New(tokenSequence.Count);
				this._Array = arraySegment.Array;
				this._Offset = arraySegment.Offset;
				this._Count = arraySegment.Count;
				global::System.Array.Copy(tokenSequence.Array, tokenSequence.Offset, arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				return;
			}
			this._Array = null;
			this._Offset = 0;
			this._Count = 0;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002B838 File Offset: 0x00029A38
		public int[] ToArray()
		{
			int[] array = new int[this.Count];
			if (this.Count > 0)
			{
				global::System.Array.ConstrainedCopy(this.Array, this.Offset, array, 0, this.Count);
			}
			return array;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002B874 File Offset: 0x00029A74
		public ArraySegment<int> Clone(ISegmentAllocator<int> allocator)
		{
			ArraySegment<int> arraySegment = allocator.New(this.Count);
			if (this.Count > 0)
			{
				global::System.Array.ConstrainedCopy(this.Array, this.Offset, arraySegment.Array, arraySegment.Offset, this.Count);
			}
			return arraySegment;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public void Write(BinaryWriter w)
		{
			w.Write(this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				w.Write(this[i]);
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator)
		{
			int num = r.ReadInt32();
			ArraySegment<int> arraySegment = allocator.New(num);
			for (int i = 0; i < num; i++)
			{
				arraySegment.Array[arraySegment.Offset + i] = r.ReadInt32();
			}
			this._Array = arraySegment.Array;
			this._Offset = arraySegment.Offset;
			this._Count = arraySegment.Count;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002B95E File Offset: 0x00029B5E
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002B961 File Offset: 0x00029B61
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002B968 File Offset: 0x00029B68
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002B970 File Offset: 0x00029B70
		public override int GetHashCode()
		{
			int num = 101;
			for (int i = 0; i < this.Count; i++)
			{
				num = Utilities.GetHashCode(num, this[i]);
			}
			return num;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002B9A0 File Offset: 0x00029BA0
		public override bool Equals(object obj)
		{
			return (obj is TokenSequence && this.Equals((TokenSequence)obj)) || (obj is WeightedTokenSequence && this.Equals(((WeightedTokenSequence)obj).Tokens));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002B9D5 File Offset: 0x00029BD5
		public bool Equals(WeightedTokenSequence t)
		{
			return this.Equals(t.Tokens);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		public bool Equals(TokenSequence t)
		{
			if (this.Count != t.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this.Array[this.Offset + i] != t.Array[t.Offset + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002BA38 File Offset: 0x00029C38
		public IEnumerator<int> GetEnumerator()
		{
			return new ArraySegment32<int>.Enumerator(this);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002BA4A File Offset: 0x00029C4A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ArraySegment32<int>.Enumerator(this);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002BA5C File Offset: 0x00029C5C
		public void Clear()
		{
			this._Array = null;
			this._Offset = 0;
			this._Count = 0;
		}

		// Token: 0x170001CD RID: 461
		public int this[int index]
		{
			get
			{
				return this.Array[this.Offset + index];
			}
			set
			{
				this.Array[this.Offset + index] = value;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002BA98 File Offset: 0x00029C98
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Count * 4);
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(this[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		public string ToVerboseString(ITokenIdProvider tokenIdProvider)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Count * 4);
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" ");
				}
				StringExtent stringExtent;
				if (!tokenIdProvider.TryGetToken(this[i], out stringExtent))
				{
					stringExtent = new StringExtent(string.Format("{0}", this[i]));
				}
				stringBuilder.AppendFormat("{0}", stringExtent.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0002BB79 File Offset: 0x00029D79
		public long MemoryUsage
		{
			get
			{
				return (long)(16 + this.Count * 4);
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002BB88 File Offset: 0x00029D88
		public TokenSequence SubSequence(ITokenIdProvider tokenIdProvider, int domainId)
		{
			for (int i = 0; i < this._Count; i++)
			{
				if (tokenIdProvider.GetDomainId(this._Array[this._Offset + i]) == domainId)
				{
					int num = i + 1;
					while (num < this._Count && tokenIdProvider.GetDomainId(this._Array[this._Offset + i]) == domainId)
					{
						num++;
					}
					return new TokenSequence(this._Array, i, num - i);
				}
			}
			return default(TokenSequence);
		}

		// Token: 0x040003A9 RID: 937
		private int[] _Array;

		// Token: 0x040003AA RID: 938
		private int _Offset;

		// Token: 0x040003AB RID: 939
		private int _Count;
	}
}
