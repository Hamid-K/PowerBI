using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public sealed class EditTrie
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001C625 File Offset: 0x0001A825
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0001C62D File Offset: 0x0001A82D
		public int Count { get; private set; }

		// Token: 0x0600048C RID: 1164 RVA: 0x0001C638 File Offset: 0x0001A838
		public EditTrie()
		{
			this.m_nodeBlocks = new List<EditTrie.Node[]>();
			this.m_nodeBlocks.Add(new EditTrie.Node[8192]);
			this.m_root = this.NewNode('\0');
			this.m_level0Nodes['\0'] = this.m_root.NodePointer;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001C69A File Offset: 0x0001A89A
		private EditTrie.NodeReference NewNode(char c)
		{
			return this.NewNode(c, EditTrie.Node.Null, EditTrie.Node.Null);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		private EditTrie.NodeReference NewNode(char c, int sibling, int child)
		{
			if (this.m_nextNodeIndex == 8192)
			{
				this.m_nodeBlocks.Add(new EditTrie.Node[8192]);
				this.m_nextNodeIndex = 0;
			}
			int num = this.m_nodeBlocks.Count - 1;
			int nextNodeIndex = this.m_nextNodeIndex;
			this.m_nextNodeIndex = nextNodeIndex + 1;
			int num2 = nextNodeIndex;
			this.m_nodeBlocks[num][num2] = new EditTrie.Node(c, sibling, child);
			return new EditTrie.NodeReference(num * 8192 + num2, this.m_nodeBlocks[num], num2);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001C73B File Offset: 0x0001A93B
		private EditTrie.NodeReference GetNode(int p)
		{
			if (EditTrie.Node.Null == p)
			{
				return EditTrie.NodeReference.Null;
			}
			return new EditTrie.NodeReference(p, this.m_nodeBlocks[p / 8192], p % 8192);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001C76C File Offset: 0x0001A96C
		private EditTrie.NodeReference GetChild(EditTrie.NodeReference parent, char c)
		{
			if (parent.IsNull)
			{
				int @null = EditTrie.Node.Null;
				if (this.m_level0Nodes.TryGetValue(c, ref @null))
				{
					return this.GetNode(@null);
				}
				return EditTrie.NodeReference.Null;
			}
			else
			{
				if (EditTrie.Node.Null == parent.Child)
				{
					return EditTrie.NodeReference.Null;
				}
				EditTrie.NodeReference nodeReference = this.GetNode(parent.Child);
				while (nodeReference.Symbol != c)
				{
					if (nodeReference.Symbol > c || nodeReference.Sibling == EditTrie.Node.Null)
					{
						return EditTrie.NodeReference.Null;
					}
					nodeReference = this.GetNode(nodeReference.Sibling);
				}
				return nodeReference;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001C804 File Offset: 0x0001AA04
		private EditTrie.NodeReference GetOrCreateChild(EditTrie.NodeReference parent, char c)
		{
			if (parent.IsNull)
			{
				int num;
				if (!this.m_level0Nodes.TryGetValue(c, ref num))
				{
					EditTrie.NodeReference nodeReference = this.NewNode(c);
					EditTrie.NodeReference nodeReference2 = this.m_root;
					int num2 = this.m_root.NodePointer;
					while (c > nodeReference2.Symbol && nodeReference2.Sibling != EditTrie.Node.Null)
					{
						num2 = nodeReference2.NodePointer;
						nodeReference2 = this.GetNode(nodeReference2.Sibling);
					}
					if (c > nodeReference2.Symbol)
					{
						nodeReference2.Sibling = nodeReference.NodePointer;
					}
					else
					{
						EditTrie.NodeReference node = this.GetNode(num2);
						nodeReference.Sibling = node.Sibling;
						node.Sibling = nodeReference.NodePointer;
					}
					this.m_level0Nodes.Add(c, nodeReference.NodePointer);
					return nodeReference;
				}
				return this.GetNode(num);
			}
			else
			{
				if (EditTrie.Node.Null == parent.Child)
				{
					EditTrie.NodeReference nodeReference3 = this.NewNode(c);
					parent.Child = nodeReference3.NodePointer;
					return nodeReference3;
				}
				EditTrie.NodeReference nodeReference4 = this.GetNode(parent.Child);
				EditTrie.NodeReference nodeReference5 = parent;
				while (nodeReference4.NodePointer != EditTrie.Node.Null)
				{
					if (c == nodeReference4.Symbol)
					{
						return nodeReference4;
					}
					if (c < nodeReference4.Symbol)
					{
						EditTrie.NodeReference nodeReference6 = this.NewNode(c, nodeReference4.NodePointer, EditTrie.Node.Null);
						if (nodeReference5.NodePointer == parent.NodePointer)
						{
							parent.Child = nodeReference6.NodePointer;
						}
						else
						{
							nodeReference5.Sibling = nodeReference6.NodePointer;
						}
						return nodeReference6;
					}
					nodeReference5 = nodeReference4;
					nodeReference4 = this.GetNode(nodeReference4.Sibling);
				}
				nodeReference4 = this.NewNode(c);
				nodeReference5.Sibling = nodeReference4.NodePointer;
				return nodeReference4;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001C9A5 File Offset: 0x0001ABA5
		public void Add(string s)
		{
			this.Add<StringWrapper>(s);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public void Add<T>(T s) where T : IString
		{
			if (s == null)
			{
				throw new ArgumentNullException();
			}
			EditTrie.NodeReference nodeReference = EditTrie.NodeReference.Null;
			for (int i = 0; i < s.Length; i++)
			{
				nodeReference = this.GetOrCreateChild(nodeReference, s[i]);
			}
			this.GetOrCreateChild(nodeReference, '\0');
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		public bool Contains(string s)
		{
			return this.Contains<StringWrapper>(s);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001CA2C File Offset: 0x0001AC2C
		public bool Contains<T>(T s) where T : IString
		{
			EditTrie.NodeReference nodeReference = EditTrie.NodeReference.Null;
			for (int i = 0; i < s.Length; i++)
			{
				nodeReference = this.GetChild(nodeReference, s[i]);
				if (nodeReference.IsNull)
				{
					return false;
				}
			}
			return !this.GetChild(nodeReference, '\0').IsNull;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001CA8B File Offset: 0x0001AC8B
		public EditTrie.IMatchReader Find<T>(T s) where T : IString
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001CA94 File Offset: 0x0001AC94
		public EditTrie.IMatchReader Find<T>(T s, int maxDistance) where T : IString
		{
			EditTrie.MatchContext matchContext = new EditTrie.MatchContext();
			Matrix<EditTrie.LevCell> matrix = matchContext.Matrix;
			this.InitializeCostMatrix<T>(matrix, s);
			this.Find<T>(matchContext, s, 0, '\0', this.GetNode(this.m_root.Sibling), maxDistance);
			return matchContext;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		private void AddMatch<T>(char[] prefix, int prefixLen, int cost)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001CADA File Offset: 0x0001ACDA
		private void Find<T>(EditTrie.MatchContext mc, T s, int j, char c, EditTrie.NodeReference p, int maxDistance) where T : IString
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001CADC File Offset: 0x0001ACDC
		private short CharCost(char precedingChar, char from, char to)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		private void InitializeCostMatrix<T>(Matrix<EditTrie.LevCell> matrix, T s) where T : IString
		{
			matrix[0][0].Cost = 0;
			for (int i = 0; i < s.Length; i++)
			{
				matrix[i + 1][0].Cost = matrix[i][0].Cost + (int)this.CharCost('\0', s[i], '\0');
				matrix[i + 1][0].From = s[i];
				matrix[i + 1][0].To = '\0';
				matrix[i + 1][0].Span = 0;
			}
		}

		// Token: 0x040000D2 RID: 210
		private const int NodeBlockSize = 8192;

		// Token: 0x040000D3 RID: 211
		private List<EditTrie.Node[]> m_nodeBlocks;

		// Token: 0x040000D4 RID: 212
		private int m_nextNodeIndex;

		// Token: 0x040000D5 RID: 213
		private EditTrie.NodeReference m_root;

		// Token: 0x040000D6 RID: 214
		private Dictionary<char, int> m_level0Nodes = new Dictionary<char, int>();

		// Token: 0x020000F3 RID: 243
		[Serializable]
		private struct Node
		{
			// Token: 0x06000918 RID: 2328 RVA: 0x0002CF08 File Offset: 0x0002B108
			public Node(char symbol, int sibling, int child)
			{
				this.Symbol = symbol;
				this.Sibling = sibling;
				this.Child = child;
			}

			// Token: 0x0400025C RID: 604
			public static readonly int Null = -1;

			// Token: 0x0400025D RID: 605
			public int Sibling;

			// Token: 0x0400025E RID: 606
			public int Child;

			// Token: 0x0400025F RID: 607
			public char Symbol;
		}

		// Token: 0x020000F4 RID: 244
		[Serializable]
		private struct NodeReference
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002CF27 File Offset: 0x0002B127
			public bool IsNull
			{
				get
				{
					return this.NodePointer == EditTrie.Node.Null;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002CF36 File Offset: 0x0002B136
			public bool IsLeaf
			{
				get
				{
					return this.Symbol == '\0' && this.NodePointer != EditTrie.Node.Null;
				}
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x0002CF52 File Offset: 0x0002B152
			public NodeReference(int p, EditTrie.Node[] nodeArray, int nodeIndex)
			{
				this.NodePointer = p;
				this.m_nodeArray = nodeArray;
				this.m_nodeIndex = nodeIndex;
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002CF69 File Offset: 0x0002B169
			// (set) Token: 0x0600091E RID: 2334 RVA: 0x0002CF81 File Offset: 0x0002B181
			public char Symbol
			{
				get
				{
					return this.m_nodeArray[this.m_nodeIndex].Symbol;
				}
				set
				{
					this.m_nodeArray[this.m_nodeIndex].Symbol = value;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600091F RID: 2335 RVA: 0x0002CF9A File Offset: 0x0002B19A
			// (set) Token: 0x06000920 RID: 2336 RVA: 0x0002CFB2 File Offset: 0x0002B1B2
			public int Sibling
			{
				get
				{
					return this.m_nodeArray[this.m_nodeIndex].Sibling;
				}
				set
				{
					this.m_nodeArray[this.m_nodeIndex].Sibling = value;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002CFCB File Offset: 0x0002B1CB
			// (set) Token: 0x06000922 RID: 2338 RVA: 0x0002CFE3 File Offset: 0x0002B1E3
			public int Child
			{
				get
				{
					return this.m_nodeArray[this.m_nodeIndex].Child;
				}
				set
				{
					this.m_nodeArray[this.m_nodeIndex].Child = value;
				}
			}

			// Token: 0x04000260 RID: 608
			public static readonly EditTrie.NodeReference Null = new EditTrie.NodeReference(EditTrie.Node.Null, null, -1);

			// Token: 0x04000261 RID: 609
			private EditTrie.Node[] m_nodeArray;

			// Token: 0x04000262 RID: 610
			private int m_nodeIndex;

			// Token: 0x04000263 RID: 611
			public int NodePointer;
		}

		// Token: 0x020000F5 RID: 245
		public interface IMatchReader
		{
			// Token: 0x06000924 RID: 2340
			bool Read();

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000925 RID: 2341
			StringExtent Current { get; }

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000926 RID: 2342
			int Distance { get; }
		}

		// Token: 0x020000F6 RID: 246
		[DebuggerDisplay("Cost={Cost} From={From} To={To}")]
		[Serializable]
		internal struct LevCell
		{
			// Token: 0x06000927 RID: 2343 RVA: 0x0002D00F File Offset: 0x0002B20F
			public void SetFromToSpan(char from, char to, short span)
			{
				this.From = from;
				this.To = to;
				this.Span = span;
			}

			// Token: 0x04000264 RID: 612
			public int Cost;

			// Token: 0x04000265 RID: 613
			public char From;

			// Token: 0x04000266 RID: 614
			public char To;

			// Token: 0x04000267 RID: 615
			public short Span;
		}

		// Token: 0x020000F7 RID: 247
		[Serializable]
		public class MatchContext : EditTrie.IMatchReader
		{
			// Token: 0x06000928 RID: 2344 RVA: 0x0002D026 File Offset: 0x0002B226
			internal MatchContext()
			{
				this.Matrix = new Matrix<EditTrie.LevCell>();
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x0002D039 File Offset: 0x0002B239
			public bool Read()
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002D040 File Offset: 0x0002B240
			public StringExtent Current
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002D047 File Offset: 0x0002B247
			public int Distance
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x04000268 RID: 616
			internal Matrix<EditTrie.LevCell> Matrix;
		}

		// Token: 0x020000F8 RID: 248
		internal enum OperationType
		{
			// Token: 0x0400026A RID: 618
			Zero,
			// Token: 0x0400026B RID: 619
			Insertion,
			// Token: 0x0400026C RID: 620
			Deletion,
			// Token: 0x0400026D RID: 621
			Substitution = 4,
			// Token: 0x0400026E RID: 622
			Transposition
		}
	}
}
