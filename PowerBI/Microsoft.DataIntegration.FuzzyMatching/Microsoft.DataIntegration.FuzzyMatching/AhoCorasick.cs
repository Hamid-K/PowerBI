using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public class AhoCorasick : IRawSerializable, ISerializable, ISessionable
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002C2D5 File Offset: 0x0002A4D5
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0002C2DD File Offset: 0x0002A4DD
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002C2E6 File Offset: 0x0002A4E6
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0002C2EE File Offset: 0x0002A4EE
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002C2F7 File Offset: 0x0002A4F7
		public AhoCorasick()
		{
			this.m_state = AhoCorasick.State.UNINIT;
			this.m_numNodes = 0;
			this.m_rootBNode = null;
			this.m_lNodeBuf = new AhoCorasick.LNode[0];
			this.BeginUpdate();
			this.EndUpdate();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002C32C File Offset: 0x0002A52C
		private AhoCorasick(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_state = (AhoCorasick.State)info.GetValue("state", typeof(AhoCorasick.State));
			this.m_numNodes = (int)info.GetValue("numNodes", typeof(int));
			this.m_rootBNode = (AhoCorasick.BNode)info.GetValue("rootBNode", typeof(AhoCorasick.BNode));
			this.m_failFn = (int[])info.GetValue("failFn", typeof(int[]));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_lNodeBuf = (AhoCorasick.LNode[])info.GetValue("lNodeBuf", typeof(AhoCorasick.LNode[]));
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002C428 File Offset: 0x0002A628
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("state", this.m_state);
			info.AddValue("numNodes", this.m_numNodes);
			info.AddValue("rootBNode", this.m_rootBNode);
			info.AddValue("failFn", this.m_failFn);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("lNodeBuf", this.m_lNodeBuf);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002C4BC File Offset: 0x0002A6BC
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_lNodeBuf.Length);
			for (int i = 0; i < this.m_lNodeBuf.Length; i++)
			{
				StreamUtilities.WriteChar(s, this.m_lNodeBuf[i].Char);
				StreamUtilities.WriteInt32(s, this.m_lNodeBuf[i].FirstChildIdx);
				StreamUtilities.WriteInt32(s, this.m_lNodeBuf[i].LastChildIdx);
				for (AhoCorasick.EntityListNode entityListNode = this.m_lNodeBuf[i].EntityList; entityListNode != null; entityListNode = entityListNode.Next)
				{
					StreamUtilities.WriteBoolean(s, true);
					StreamUtilities.WriteInt32(s, entityListNode.Id);
					StreamUtilities.WriteInt32(s, entityListNode.Len);
				}
				StreamUtilities.WriteBoolean(s, false);
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002C594 File Offset: 0x0002A794
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			this.m_lNodeBuf = new AhoCorasick.LNode[StreamUtilities.ReadInt32(s)];
			for (int i = 0; i < this.m_lNodeBuf.Length; i++)
			{
				this.m_lNodeBuf[i].Char = StreamUtilities.ReadChar(s);
				this.m_lNodeBuf[i].FirstChildIdx = StreamUtilities.ReadInt32(s);
				this.m_lNodeBuf[i].LastChildIdx = StreamUtilities.ReadInt32(s);
				AhoCorasick.EntityListNode entityListNode = null;
				while (StreamUtilities.ReadBoolean(s))
				{
					if (entityListNode == null)
					{
						entityListNode = (this.m_lNodeBuf[i].EntityList = new AhoCorasick.EntityListNode());
					}
					else
					{
						entityListNode.Next = new AhoCorasick.EntityListNode();
						entityListNode = entityListNode.Next;
					}
					entityListNode.Id = StreamUtilities.ReadInt32(s);
					entityListNode.Len = StreamUtilities.ReadInt32(s);
				}
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002C698 File Offset: 0x0002A898
		public void BeginUpdate()
		{
			this.m_rootBNode = this.GetNewBNode();
			this.m_rootBNode.Char = '\0';
			this.m_rootBNode.FirstChild = null;
			this.m_rootBNode.NextSibling = null;
			this.m_rootBNode.EntityId = int.MaxValue;
			this.m_numNodes = 1;
			this.m_state = AhoCorasick.State.UPDATE;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002C6F3 File Offset: 0x0002A8F3
		public ISession CreateSession()
		{
			return new AhoCorasick.Session(this);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002C6FB File Offset: 0x0002A8FB
		public void Add(string entity, int entityId)
		{
			this.Add<StringWrapper>(entity, entityId);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002C70C File Offset: 0x0002A90C
		public void Add<T>(T entity, int entityId) where T : IString
		{
			if (entityId == 2147483647)
			{
				throw new ArgumentException(string.Format("entityId may not be {0}.", int.MaxValue));
			}
			AhoCorasick.BNode bnode = this.m_rootBNode;
			for (int i = 0; i < entity.Length; i++)
			{
				char c = entity[i];
				if (bnode.FirstChild == null || bnode.FirstChild.Char != c)
				{
					AhoCorasick.BNode newBNode = this.GetNewBNode();
					newBNode.Char = c;
					newBNode.FirstChild = null;
					newBNode.NextSibling = bnode.FirstChild;
					newBNode.EntityId = int.MaxValue;
					bnode.FirstChild = newBNode;
					this.m_numNodes++;
				}
				bnode = bnode.FirstChild;
			}
			bnode.EntityId = entityId;
			bnode.EntityLen = entity.Length;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		public void EndUpdate()
		{
			this.BuildLookupTree();
			this.ComputeFailFunction();
			this.m_rootBNode = null;
			this.m_state = AhoCorasick.State.LOOKUP;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002C800 File Offset: 0x0002AA00
		public void ResetLookup(ISession session, string lookupSequence)
		{
			((AhoCorasick.Session)session).ResetLookup(lookupSequence);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002C80E File Offset: 0x0002AA0E
		public void ResetLookup<T>(ISession session, T lookupSequence) where T : IString
		{
			((AhoCorasick.Session)session).ResetLookup<T>(lookupSequence);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002C81C File Offset: 0x0002AA1C
		public void ResetLookupForSegmentation(ISession session, string lookupSequence)
		{
			((AhoCorasick.Session)session).ResetLookupForSegmentation(lookupSequence);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002C82A File Offset: 0x0002AA2A
		public void ResetLookupForSegmentation<T>(ISession session, T lookupSequence) where T : IString
		{
			((AhoCorasick.Session)session).ResetLookupForSegmentation<T>(lookupSequence);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002C838 File Offset: 0x0002AA38
		public bool GetNextMatch(ISession session)
		{
			return ((AhoCorasick.Session)session).GetNextMatch();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002C845 File Offset: 0x0002AA45
		public int GetCurMatch(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurMatch();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002C852 File Offset: 0x0002AA52
		public int GetCurMatchPos(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurMatchPos();
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002C85F File Offset: 0x0002AA5F
		public int GetCurMatchLen(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurMatchLen();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002C86C File Offset: 0x0002AA6C
		public bool GetNextSegmentation(ISession session)
		{
			return ((AhoCorasick.Session)session).GetNextSegmentation();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002C879 File Offset: 0x0002AA79
		public bool GetNextSegmentation(ISession session, BloomFilter<int> entityPairBF)
		{
			return ((AhoCorasick.Session)session).GetNextSegmentation(entityPairBF);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002C887 File Offset: 0x0002AA87
		public int GetCurSegment(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurSegment();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002C894 File Offset: 0x0002AA94
		public int GetCurSegmentLen(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurSegmentLen();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002C8A1 File Offset: 0x0002AAA1
		public int GetCurSegmentPos(ISession session)
		{
			return ((AhoCorasick.Session)session).GetCurSegmentPos();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002C8AE File Offset: 0x0002AAAE
		public bool GetNextSegment(ISession session)
		{
			return ((AhoCorasick.Session)session).GetNextSegment();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002C8BB File Offset: 0x0002AABB
		public int GetNumSegments(ISession session)
		{
			return ((AhoCorasick.Session)session).GetNumSegments();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
		private AhoCorasick.BNode GetNewBNode()
		{
			return new AhoCorasick.BNode();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002C8D0 File Offset: 0x0002AAD0
		private void BuildLookupTree()
		{
			Stack<AhoCorasick.BNode> stack = new Stack<AhoCorasick.BNode>();
			Stack<int> stack2 = new Stack<int>();
			this.m_lNodeBuf = new AhoCorasick.LNode[this.m_numNodes + 1];
			this.m_lNodeBuf[0].Char = '\0';
			this.m_lNodeBuf[0].EntityList = null;
			int num = 1;
			stack.Push(this.m_rootBNode);
			stack2.Push(0);
			while (stack.Count > 0)
			{
				AhoCorasick.BNode bnode = stack.Pop();
				int num2 = stack2.Pop();
				this.m_lNodeBuf[num2].FirstChildIdx = num;
				for (AhoCorasick.BNode bnode2 = bnode.FirstChild; bnode2 != null; bnode2 = bnode2.NextSibling)
				{
					this.m_lNodeBuf[num].Char = bnode2.Char;
					this.m_lNodeBuf[num].EntityList = ((bnode2.EntityId == int.MaxValue) ? null : new AhoCorasick.EntityListNode(bnode2.EntityId, bnode2.EntityLen, null));
					num++;
				}
				this.m_lNodeBuf[num2].LastChildIdx = num - 1;
				if (bnode.NextSibling != null)
				{
					stack.Push(bnode.NextSibling);
					stack2.Push(num2 + 1);
				}
				if (bnode.FirstChild != null)
				{
					stack.Push(bnode.FirstChild);
					stack2.Push(this.m_lNodeBuf[num2].FirstChildIdx);
				}
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002CA30 File Offset: 0x0002AC30
		private void ComputeFailFunction()
		{
			this.m_failFn = new int[this.m_numNodes];
			Queue<int> queue = new Queue<int>();
			for (int i = 1; i <= this.m_lNodeBuf[0].LastChildIdx; i++)
			{
				this.m_failFn[i] = 0;
				queue.Enqueue(i);
			}
			while (queue.Count > 0)
			{
				int num = queue.Dequeue();
				int firstChildIdx = this.m_lNodeBuf[num].FirstChildIdx;
				int lastChildIdx = this.m_lNodeBuf[num].LastChildIdx;
				for (int j = firstChildIdx; j <= lastChildIdx; j++)
				{
					char @char = this.m_lNodeBuf[j].Char;
					queue.Enqueue(j);
					int num2 = this.m_failFn[num];
					int num3;
					while ((num3 = AhoCorasick.Goto(this.m_lNodeBuf, num2, @char)) == -1 && num2 != 0)
					{
						num2 = this.m_failFn[num2];
					}
					this.m_failFn[j] = ((num3 == -1) ? 0 : num3);
					if (this.m_lNodeBuf[j].EntityList == null)
					{
						this.m_lNodeBuf[j].EntityList = this.m_lNodeBuf[this.m_failFn[j]].EntityList;
					}
					else
					{
						this.m_lNodeBuf[j].EntityList.Next = this.m_lNodeBuf[this.m_failFn[j]].EntityList;
					}
				}
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002CBA4 File Offset: 0x0002ADA4
		private static int Goto(AhoCorasick.LNode[] lNodeBuf, int state, char c)
		{
			int i = lNodeBuf[state].FirstChildIdx;
			int num = lNodeBuf[state].LastChildIdx;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				char @char = lNodeBuf[num2].Char;
				if (c < @char)
				{
					num = num2 - 1;
				}
				else
				{
					if (c <= @char)
					{
						return num2;
					}
					i = num2 + 1;
				}
			}
			if (state != 0)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x040003B5 RID: 949
		private const int EntityIdNull = 2147483647;

		// Token: 0x040003B6 RID: 950
		private AhoCorasick.State m_state;

		// Token: 0x040003B7 RID: 951
		private int m_numNodes;

		// Token: 0x040003B8 RID: 952
		private AhoCorasick.BNode m_rootBNode;

		// Token: 0x040003B9 RID: 953
		private AhoCorasick.LNode[] m_lNodeBuf;

		// Token: 0x040003BA RID: 954
		private int[] m_failFn;

		// Token: 0x02000188 RID: 392
		private class Session : ISession
		{
			// Token: 0x06000D2E RID: 3374 RVA: 0x000385BC File Offset: 0x000367BC
			public Session(AhoCorasick tree)
			{
				this.m_ahoCorasick = tree;
				this.m_matchBuf = new AhoCorasick.Match[0];
				this.m_bMatchEndsInPos = new bool[0];
				this.m_firstMatchEndingInPos = new int[0];
				this.m_curSegns = new int[0];
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x000385FB File Offset: 0x000367FB
			public void Reset()
			{
				this.m_numMatches = 0;
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x00038604 File Offset: 0x00036804
			public void ResetLookup(string lookupSequence)
			{
				this.ResetLookup<StringWrapper>(lookupSequence);
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x00038614 File Offset: 0x00036814
			public void ResetLookup<T>(T lookupSequence) where T : IString
			{
				int num = 0;
				this.m_numMatches = 0;
				AhoCorasick.LNode[] lNodeBuf = this.m_ahoCorasick.m_lNodeBuf;
				int[] failFn = this.m_ahoCorasick.m_failFn;
				for (int i = 0; i < lookupSequence.Length; i++)
				{
					int num2;
					while ((num2 = AhoCorasick.Goto(lNodeBuf, num, lookupSequence[i])) == -1)
					{
						num = failFn[num];
					}
					num = num2;
					for (AhoCorasick.EntityListNode entityListNode = lNodeBuf[num].EntityList; entityListNode != null; entityListNode = entityListNode.Next)
					{
						if (this.m_numMatches == this.m_matchBuf.Length)
						{
							this.GrowMatchBuf();
						}
						this.m_matchBuf[this.m_numMatches].EntityId = entityListNode.Id;
						this.m_matchBuf[this.m_numMatches].Pos = i - entityListNode.Len + 1;
						this.m_matchBuf[this.m_numMatches].Len = entityListNode.Len;
						this.m_numMatches++;
					}
				}
				this.m_curMatchIdx = -1;
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x0003872A File Offset: 0x0003692A
			public void ResetLookupForSegmentation(string lookupSequence)
			{
				this.ResetLookupForSegmentation<StringWrapper>(lookupSequence);
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x00038738 File Offset: 0x00036938
			public void ResetLookupForSegmentation<T>(T lookupSequence) where T : IString
			{
				this.PrepareForSegmentation(lookupSequence.Length);
				this.m_bMatchEndsInPos[0] = true;
				this.m_numMatches = 0;
				int num = 0;
				AhoCorasick.LNode[] lNodeBuf = this.m_ahoCorasick.m_lNodeBuf;
				int[] failFn = this.m_ahoCorasick.m_failFn;
				int i = 0;
				while (i < lookupSequence.Length)
				{
					int num2;
					while ((num2 = AhoCorasick.Goto(lNodeBuf, num, lookupSequence[i])) == -1)
					{
						num = failFn[num];
					}
					if (num == num2)
					{
						this.m_firstMatchEndingInPos[lookupSequence.Length] = this.m_numMatches;
						break;
					}
					num = num2;
					i++;
					this.m_firstMatchEndingInPos[i] = this.m_numMatches;
					for (AhoCorasick.EntityListNode entityListNode = lNodeBuf[num].EntityList; entityListNode != null; entityListNode = entityListNode.Next)
					{
						if (this.m_numMatches == this.m_matchBuf.Length)
						{
							this.GrowMatchBuf();
						}
						if (this.m_bMatchEndsInPos[i - entityListNode.Len])
						{
							this.m_matchBuf[this.m_numMatches].EntityId = entityListNode.Id;
							this.m_matchBuf[this.m_numMatches].Pos = i - entityListNode.Len;
							this.m_matchBuf[this.m_numMatches].Len = entityListNode.Len;
							this.m_numMatches++;
							this.m_bMatchEndsInPos[i] = true;
						}
					}
				}
				if (this.m_numMatches == this.m_matchBuf.Length)
				{
					this.GrowMatchBuf();
				}
				this.m_matchBuf[this.m_numMatches].EntityId = -1;
				this.m_matchBuf[this.m_numMatches].Pos = 0;
				this.m_matchBuf[this.m_numMatches].Len = lookupSequence.Length;
				this.m_curSegnLen = 1;
				this.m_curSegns[0] = this.m_numMatches;
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0003891C File Offset: 0x00036B1C
			public bool GetNextMatch()
			{
				int num = this.m_curMatchIdx + 1;
				this.m_curMatchIdx = num;
				return num < this.m_numMatches;
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x00038942 File Offset: 0x00036B42
			public int GetCurMatch()
			{
				return this.m_matchBuf[this.m_curMatchIdx].EntityId;
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x00038956 File Offset: 0x00036B56
			public int GetCurMatchPos()
			{
				return this.m_matchBuf[this.m_curMatchIdx].Pos;
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x0003896A File Offset: 0x00036B6A
			public int GetCurMatchLen()
			{
				return this.m_matchBuf[this.m_curMatchIdx].Len;
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x00038980 File Offset: 0x00036B80
			public bool GetNextSegmentation()
			{
				if (this.m_curSegnLen == 0)
				{
					return false;
				}
				this.m_curSegnLen--;
				int num = this.m_curSegns[this.m_curSegnLen];
				AhoCorasick.Match match = this.m_matchBuf[num];
				int num2 = match.Pos + match.Len;
				for (int i = num - 1; i >= this.m_firstMatchEndingInPos[num2]; i--)
				{
					int[] curSegns = this.m_curSegns;
					int curSegnLen = this.m_curSegnLen;
					this.m_curSegnLen = curSegnLen + 1;
					curSegns[curSegnLen] = i;
					if (this.GetFirstSegmentation())
					{
						return true;
					}
					this.m_curSegnLen--;
				}
				return this.GetNextSegmentation();
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x00038A1C File Offset: 0x00036C1C
			public bool GetNextSegmentation(BloomFilter<int> entityPairBF)
			{
				if (this.m_curSegnLen == 0)
				{
					return false;
				}
				this.m_curSegnLen--;
				int num = this.m_curSegns[this.m_curSegnLen];
				AhoCorasick.Match match = this.m_matchBuf[num];
				int num2 = match.Pos + match.Len;
				int num3 = ((this.m_curSegnLen > 0) ? this.m_matchBuf[this.m_curSegns[this.m_curSegnLen - 1]].EntityId : (-1));
				int i = num - 1;
				while (i >= this.m_firstMatchEndingInPos[num2])
				{
					if (this.m_curSegnLen <= 0)
					{
						goto IL_0095;
					}
					int hashCode = Utilities.GetHashCode(this.m_matchBuf[i].EntityId, num3);
					if (entityPairBF.Contains(hashCode))
					{
						goto IL_0095;
					}
					IL_00CB:
					i--;
					continue;
					IL_0095:
					int[] curSegns = this.m_curSegns;
					int curSegnLen = this.m_curSegnLen;
					this.m_curSegnLen = curSegnLen + 1;
					curSegns[curSegnLen] = i;
					if (this.GetFirstSegmentation(entityPairBF))
					{
						return true;
					}
					this.m_curSegnLen--;
					goto IL_00CB;
				}
				return this.GetNextSegmentation(entityPairBF);
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x00038B0D File Offset: 0x00036D0D
			public int GetCurSegment()
			{
				return this.m_matchBuf[this.m_curSegns[this.m_curSegnLen - 1 - this.m_curSegIdx]].EntityId;
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x00038B31 File Offset: 0x00036D31
			public int GetCurSegmentLen()
			{
				return this.m_matchBuf[this.m_curSegns[this.m_curSegnLen - 1 - this.m_curSegIdx]].Len;
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x00038B55 File Offset: 0x00036D55
			public int GetCurSegmentPos()
			{
				return this.m_matchBuf[this.m_curSegns[this.m_curSegnLen - 1 - this.m_curSegIdx]].Pos;
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x00038B7C File Offset: 0x00036D7C
			public bool GetNextSegment()
			{
				int num = this.m_curSegIdx + 1;
				this.m_curSegIdx = num;
				return num < this.m_curSegnLen;
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x00038BA2 File Offset: 0x00036DA2
			public int GetNumSegments()
			{
				return this.m_curSegnLen;
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x00038BAC File Offset: 0x00036DAC
			private void GrowMatchBuf()
			{
				int num = this.m_matchBuf.Length;
				int num2 = (int)((double)num * 1.5) + 1;
				Array.Resize<AhoCorasick.Match>(ref this.m_matchBuf, num2);
				for (int i = num; i < num2; i++)
				{
					this.m_matchBuf[i] = new AhoCorasick.Match();
				}
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x00038BF8 File Offset: 0x00036DF8
			private void PrepareForSegmentation(int lookupSequenceLen)
			{
				if (this.m_bMatchEndsInPos.Length <= lookupSequenceLen)
				{
					this.m_bMatchEndsInPos = new bool[lookupSequenceLen + 1];
					this.m_firstMatchEndingInPos = new int[lookupSequenceLen + 1];
					this.m_curSegns = new int[lookupSequenceLen + 1];
					return;
				}
				for (int i = 0; i < lookupSequenceLen; i++)
				{
					this.m_bMatchEndsInPos[i] = false;
				}
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x00038C50 File Offset: 0x00036E50
			private bool GetFirstSegmentation()
			{
				int num = this.m_curSegns[this.m_curSegnLen - 1];
				int pos = this.m_matchBuf[num].Pos;
				if (pos == 0)
				{
					this.m_curSegIdx = -1;
					return true;
				}
				for (int i = this.m_firstMatchEndingInPos[pos + 1] - 1; i >= this.m_firstMatchEndingInPos[pos]; i--)
				{
					int[] curSegns = this.m_curSegns;
					int curSegnLen = this.m_curSegnLen;
					this.m_curSegnLen = curSegnLen + 1;
					curSegns[curSegnLen] = i;
					if (this.GetFirstSegmentation())
					{
						return true;
					}
					this.m_curSegnLen--;
				}
				return false;
			}

			// Token: 0x06000D42 RID: 3394 RVA: 0x00038CD8 File Offset: 0x00036ED8
			private bool GetFirstSegmentation(BloomFilter<int> entityPairBF)
			{
				int num = this.m_curSegns[this.m_curSegnLen - 1];
				int pos = this.m_matchBuf[num].Pos;
				if (pos == 0)
				{
					this.m_curSegIdx = -1;
					return true;
				}
				int entityId = this.m_matchBuf[this.m_curSegns[this.m_curSegnLen - 1]].EntityId;
				for (int i = this.m_firstMatchEndingInPos[pos + 1] - 1; i >= this.m_firstMatchEndingInPos[pos]; i--)
				{
					int hashCode = Utilities.GetHashCode(this.m_matchBuf[i].EntityId, entityId);
					if (entityPairBF.Contains(hashCode))
					{
						int[] curSegns = this.m_curSegns;
						int curSegnLen = this.m_curSegnLen;
						this.m_curSegnLen = curSegnLen + 1;
						curSegns[curSegnLen] = i;
						if (this.GetFirstSegmentation(entityPairBF))
						{
							return true;
						}
						this.m_curSegnLen--;
					}
				}
				return false;
			}

			// Token: 0x04000656 RID: 1622
			private AhoCorasick m_ahoCorasick;

			// Token: 0x04000657 RID: 1623
			private AhoCorasick.Match[] m_matchBuf;

			// Token: 0x04000658 RID: 1624
			private int m_numMatches;

			// Token: 0x04000659 RID: 1625
			private int m_curMatchIdx;

			// Token: 0x0400065A RID: 1626
			private bool[] m_bMatchEndsInPos;

			// Token: 0x0400065B RID: 1627
			private int[] m_firstMatchEndingInPos;

			// Token: 0x0400065C RID: 1628
			private int m_curSegnLen;

			// Token: 0x0400065D RID: 1629
			private int[] m_curSegns;

			// Token: 0x0400065E RID: 1630
			private int m_curSegIdx;
		}

		// Token: 0x02000189 RID: 393
		private enum State
		{
			// Token: 0x04000660 RID: 1632
			UNINIT,
			// Token: 0x04000661 RID: 1633
			UPDATE,
			// Token: 0x04000662 RID: 1634
			LOOKUP,
			// Token: 0x04000663 RID: 1635
			ERROR
		}

		// Token: 0x0200018A RID: 394
		[Serializable]
		private class BNode
		{
			// Token: 0x04000664 RID: 1636
			public char Char;

			// Token: 0x04000665 RID: 1637
			public AhoCorasick.BNode NextSibling;

			// Token: 0x04000666 RID: 1638
			public AhoCorasick.BNode FirstChild;

			// Token: 0x04000667 RID: 1639
			public int EntityId;

			// Token: 0x04000668 RID: 1640
			public int EntityLen;
		}

		// Token: 0x0200018B RID: 395
		[Serializable]
		private struct LNode
		{
			// Token: 0x04000669 RID: 1641
			public char Char;

			// Token: 0x0400066A RID: 1642
			public int FirstChildIdx;

			// Token: 0x0400066B RID: 1643
			public int LastChildIdx;

			// Token: 0x0400066C RID: 1644
			public AhoCorasick.EntityListNode EntityList;
		}

		// Token: 0x0200018C RID: 396
		[Serializable]
		private class EntityListNode
		{
			// Token: 0x06000D44 RID: 3396 RVA: 0x00038DA6 File Offset: 0x00036FA6
			public EntityListNode()
			{
			}

			// Token: 0x06000D45 RID: 3397 RVA: 0x00038DAE File Offset: 0x00036FAE
			public EntityListNode(int i, int l, AhoCorasick.EntityListNode n)
			{
				this.Id = i;
				this.Len = l;
				this.Next = n;
			}

			// Token: 0x0400066D RID: 1645
			public int Id;

			// Token: 0x0400066E RID: 1646
			public int Len;

			// Token: 0x0400066F RID: 1647
			public AhoCorasick.EntityListNode Next;
		}

		// Token: 0x0200018D RID: 397
		private class Match
		{
			// Token: 0x04000670 RID: 1648
			public int EntityId;

			// Token: 0x04000671 RID: 1649
			public int Pos;

			// Token: 0x04000672 RID: 1650
			public int Len;
		}
	}
}
