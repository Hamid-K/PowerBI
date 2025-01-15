using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	internal sealed class TranPrefixTree : IMemoryUsage, IRawSerializable, ISerializable
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000314CB File Offset: 0x0002F6CB
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000314D3 File Offset: 0x0002F6D3
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x000314DC File Offset: 0x0002F6DC
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x000314E4 File Offset: 0x0002F6E4
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x06000B18 RID: 2840 RVA: 0x000314F0 File Offset: 0x0002F6F0
		public TranPrefixTree()
		{
			this.m_numTotalRules = 0;
			this.m_goto = new FastIntPairToIntHash();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00031554 File Offset: 0x0002F754
		private TranPrefixTree(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_goto = (FastIntPairToIntHash)info.GetValue("m_goto", typeof(FastIntPairToIntHash));
			this.m_numTotalRules = (int)info.GetValue("m_numTotalRules", typeof(int));
			this.m_nextTransformationNode = (int)info.GetValue("m_nextTransformationNode", typeof(int));
			this.m_nextTrieNode = (int)info.GetValue("m_nextTrieNode", typeof(int));
			this.m_trieNodes = (int[])info.GetValue("m_trieNodes", typeof(int[]));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_transformationNodes = (TranPrefixTree.TransformationNode[])info.GetValue("m_transformationNodes", typeof(TranPrefixTree.TransformationNode[]));
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000316AC File Offset: 0x0002F8AC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_goto", this.m_goto);
			info.AddValue("m_numTotalRules", this.m_numTotalRules);
			info.AddValue("m_nextTransformationNode", this.m_nextTransformationNode);
			info.AddValue("m_nextTrieNode", this.m_nextTrieNode);
			info.AddValue("m_trieNodes", this.m_trieNodes);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_transformationNodes", this.m_transformationNodes);
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003174C File Offset: 0x0002F94C
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_transformationNodes.Length);
			for (int i = 0; i < this.m_transformationNodes.Length; i++)
			{
				this.m_transformationNodes[i].Transformation.Write(new BinaryWriter(s));
				StreamUtilities.WriteInt32(s, this.m_transformationNodes[i].NextTransformationIndex);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000317C8 File Offset: 0x0002F9C8
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.m_transformationNodes = new TranPrefixTree.TransformationNode[StreamUtilities.ReadInt32(s)];
			this.m_tokenIdAllocator = new BlockedSegmentArray<int>();
			this.m_byteAllocator = new BlockedSegmentArray<byte>();
			for (int i = 0; i < this.m_transformationNodes.Length; i++)
			{
				this.m_transformationNodes[i].Transformation = new Transformation(new BinaryReader(s), this.m_tokenIdAllocator, this.m_byteAllocator);
				this.m_transformationNodes[i].NextTransformationIndex = StreamUtilities.ReadInt32(s);
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00031880 File Offset: 0x0002FA80
		public void BeginUpdate()
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00031884 File Offset: 0x0002FA84
		public void Insert(Transformation value)
		{
			int num = 0;
			int num2 = 0;
			TokenSequence from = value.From;
			if (from.Count == 0)
			{
				return;
			}
			this.m_numTotalRules++;
			int num3;
			for (int i = 0; i < from.Count; i++)
			{
				if (!this.m_goto.TryGetValue(num, from[i], out num2))
				{
					while (i < from.Count)
					{
						if (this.m_nextTrieNode == this.m_trieNodes.Length)
						{
							Array.Resize<int>(ref this.m_trieNodes, this.m_trieNodes.Length * 2);
						}
						num3 = this.m_nextTrieNode;
						this.m_nextTrieNode = num3 + 1;
						num2 = num3;
						this.m_goto.Add(num, from[i], num2);
						num = num2;
						i++;
					}
					break;
				}
				num = num2;
			}
			if (this.m_nextTransformationNode == this.m_transformationNodes.Length)
			{
				Array.Resize<TranPrefixTree.TransformationNode>(ref this.m_transformationNodes, this.m_transformationNodes.Length * 2);
			}
			this.m_transformationNodes[this.m_nextTransformationNode] = new TranPrefixTree.TransformationNode
			{
				Transformation = value,
				NextTransformationIndex = this.m_trieNodes[num]
			};
			int[] trieNodes = this.m_trieNodes;
			int num4 = num;
			num3 = this.m_nextTransformationNode;
			this.m_nextTransformationNode = num3 + 1;
			trieNodes[num4] = num3;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000319BC File Offset: 0x0002FBBC
		public void EndUpdate()
		{
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000319C0 File Offset: 0x0002FBC0
		public void Lookup(TokenSequence tokenSequence, ISegmentAllocator<int> allocator, ArraySegmentBuilder<TransformationMatch> tranMatchListBuilder, out ArraySegment<TransformationMatch> matchList)
		{
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				int num = 0;
				int num2 = i;
				while (num2 < tokenSequence.Count && this.m_goto.TryGetValue(num, tokenSequence[num2], out num))
				{
					for (int num3 = this.m_trieNodes[num]; num3 != 0; num3 = this.m_transformationNodes[num3].NextTransformationIndex)
					{
						if (TranPrefixTree.HasContext(this.m_transformationNodes[num3].Transformation, tokenSequence, allocator))
						{
							tranMatchListBuilder.Add(new TransformationMatch
							{
								Position = i,
								Transformation = this.m_transformationNodes[num3].Transformation
							});
						}
					}
					num2++;
				}
			}
			matchList = tranMatchListBuilder;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00031A8C File Offset: 0x0002FC8C
		private static bool HasContext(Transformation t, TokenSequence tokenSequence, ISegmentAllocator<int> allocator)
		{
			TokenSequence context = new StaticTransformationMetadata(t.Metadata, allocator).Context;
			bool flag = true;
			for (int i = 0; i < context.Count; i++)
			{
				bool flag2 = false;
				for (int j = 0; j < tokenSequence.Count; j++)
				{
					if (tokenSequence[j] == context[i])
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00031AF6 File Offset: 0x0002FCF6
		internal IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			int num;
			for (int i = 0; i < this.m_nextTransformationNode; i = num + 1)
			{
				yield return this.m_transformationNodes[i].Transformation;
				num = i;
			}
			yield break;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00031B06 File Offset: 0x0002FD06
		public long MemoryUsage
		{
			get
			{
				return 8L + this.m_goto.MemoryUsage + (long)(this.m_transformationNodes.Length * 20) + (long)(this.m_trieNodes.Length * 4);
			}
		}

		// Token: 0x0400043A RID: 1082
		private BlockedSegmentArray<int> m_tokenIdAllocator = new BlockedSegmentArray<int>();

		// Token: 0x0400043B RID: 1083
		private BlockedSegmentArray<byte> m_byteAllocator = new BlockedSegmentArray<byte>();

		// Token: 0x0400043C RID: 1084
		private TranPrefixTree.TransformationNode[] m_transformationNodes = new TranPrefixTree.TransformationNode[2];

		// Token: 0x0400043D RID: 1085
		private int m_nextTransformationNode = 1;

		// Token: 0x0400043E RID: 1086
		private int[] m_trieNodes = new int[2];

		// Token: 0x0400043F RID: 1087
		private int m_nextTrieNode = 1;

		// Token: 0x04000440 RID: 1088
		private FastIntPairToIntHash m_goto;

		// Token: 0x04000441 RID: 1089
		private int m_numTotalRules;

		// Token: 0x020001AE RID: 430
		[Serializable]
		private struct TransformationNode
		{
			// Token: 0x04000712 RID: 1810
			public Transformation Transformation;

			// Token: 0x04000713 RID: 1811
			public int NextTransformationIndex;
		}
	}
}
