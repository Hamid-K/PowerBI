using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	public sealed class Trie : IRawSerializable, ISerializable
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000242A7 File Offset: 0x000224A7
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x000242AF File Offset: 0x000224AF
		public int Count { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x000242B8 File Offset: 0x000224B8
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x000242C0 File Offset: 0x000224C0
		public bool NormalizeByMaxLength { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000242C9 File Offset: 0x000224C9
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x000242D1 File Offset: 0x000224D1
		protected int NodeCount { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000242DA File Offset: 0x000224DA
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000242E2 File Offset: 0x000224E2
		public double Mass { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000242EB File Offset: 0x000224EB
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x000242F3 File Offset: 0x000224F3
		public bool RemovePunctuation { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000242FC File Offset: 0x000224FC
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00024304 File Offset: 0x00022504
		public bool RemoveWhitespace { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0002430D File Offset: 0x0002250D
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00024315 File Offset: 0x00022515
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0002431E File Offset: 0x0002251E
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00024326 File Offset: 0x00022526
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x060006E1 RID: 1761 RVA: 0x00024330 File Offset: 0x00022530
		public Trie()
		{
			this.m_trieNodes = new Trie.TrieNode[2];
			this.NodeCount = 0;
			this.NewTrieNode('\0');
			this.Root = this.NewTrieNode('\0');
			this.Level0Nodes[0] = this.Root;
			this.RemovePunctuation = false;
			this.RemoveWhitespace = false;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000243D0 File Offset: 0x000225D0
		private Trie(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.RemovePunctuation = (bool)info.GetValue("RemovePunctuation", typeof(bool));
			this.RemoveWhitespace = (bool)info.GetValue("RemoveWhitespace", typeof(bool));
			this.NormalizeByMaxLength = (bool)info.GetValue("NormalizeByMaxLength", typeof(bool));
			this.m_charCosts = (CharCostLookup)info.GetValue("m_charCosts", typeof(CharCostLookup));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_trieNodes = (Trie.TrieNode[])info.GetValue("m_trieNodes", typeof(Trie.TrieNode[]));
				this.Root = (int)info.GetValue("Root", typeof(int));
				this.Level0Nodes = (int[])info.GetValue("Level0Nodes", typeof(int[]));
				this.Count = (int)info.GetValue("Count", typeof(int));
				this.MaxStringLength = (int)info.GetValue("MaxStringLength", typeof(int));
				this.NodeCount = (int)info.GetValue("NodeCount", typeof(int));
				this.Mass = (double)info.GetValue("Mass", typeof(double));
				this.m_minCharCost = (int)info.GetValue("m_minCharCost", typeof(int));
				this.m_maxDelToTransposeCostDelta = (int)info.GetValue("m_maxDelToTransposeCostDelta", typeof(int));
			}
			else
			{
				this.m_trieNodes = new Trie.TrieNode[2];
				this.NodeCount = 0;
				this.NewTrieNode('\0');
				this.Root = this.NewTrieNode('\0');
				this.Level0Nodes[0] = this.Root;
			}
			this.m_matchContext = new Trie.MatchContext(this.MaxStringLength);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00024660 File Offset: 0x00022860
		[SecurityCritical]
		[SecurityPermission(6, Flags = 128)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("RemovePunctuation", this.RemovePunctuation);
			info.AddValue("RemoveWhitespace", this.RemoveWhitespace);
			info.AddValue("NormalizeByMaxLength", this.NormalizeByMaxLength);
			info.AddValue("m_charCosts", this.m_charCosts);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_trieNodes", this.m_trieNodes);
				info.AddValue("Root", this.Root);
				info.AddValue("Level0Nodes", this.Level0Nodes);
				info.AddValue("Count", this.Count);
				info.AddValue("MaxStringLength", this.MaxStringLength);
				info.AddValue("NodeCount", this.NodeCount);
				info.AddValue("Mass", this.Mass);
				info.AddValue("m_minCharCost", this.m_minCharCost);
				info.AddValue("m_maxDelToTransposeCostDelta", this.m_maxDelToTransposeCostDelta);
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00024778 File Offset: 0x00022978
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.Count);
			int num = 0;
			foreach (Trie.MatchResult matchResult in this.Items)
			{
				StreamUtilities.WriteInt32(s, matchResult.Length);
				for (int i = 0; i < matchResult.Length; i++)
				{
					s.WriteByte((byte)((matchResult.Word[i] >> 8) & 'ÿ'));
					s.WriteByte((byte)(matchResult.Word[i] & 'ÿ'));
				}
				num++;
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00024834 File Offset: 0x00022A34
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			char[] array = new char[0];
			int num = StreamUtilities.ReadInt32(s);
			for (int i = 0; i < num; i++)
			{
				int num2 = StreamUtilities.ReadInt32(s);
				if (array.Length < num2)
				{
					Array.Resize<char>(ref array, num2);
				}
				for (int j = 0; j < num2; j++)
				{
					array[j] = (char)((ushort)(s.ReadByte() << 8) | (ushort)s.ReadByte());
				}
				this.Add(new string(array, 0, num2));
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000248D8 File Offset: 0x00022AD8
		public void Clear()
		{
			this.NodeCount = 0;
			this.NewTrieNode('\0');
			this.Root = this.NewTrieNode('\0');
			Array.Clear(this.Level0Nodes, 0, this.Level0Nodes.Length);
			this.Level0Nodes[0] = this.Root;
			this.Count = 0;
			this.Mass = 0.0;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00024939 File Offset: 0x00022B39
		private bool TrieNodeIsNull(int node)
		{
			return node == 0;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0002493F File Offset: 0x00022B3F
		private char TrieNodeKey(int node)
		{
			return this.m_trieNodes[node].Key;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00024952 File Offset: 0x00022B52
		private int TrieNodeValue(int node)
		{
			return this.m_trieNodes[node].Value;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00024965 File Offset: 0x00022B65
		private int TrieNodeSibling(int node)
		{
			return this.m_trieNodes[node].Sibling;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00024978 File Offset: 0x00022B78
		private int TrieNodeChild(int node)
		{
			return this.m_trieNodes[node].Child;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0002498B File Offset: 0x00022B8B
		private string TrieNodeCompressedPath(int node)
		{
			return null;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0002498E File Offset: 0x00022B8E
		private bool TrieNodeIsCompressedPath(int node)
		{
			return this.m_trieNodes[node].IsCompressedPath;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000249A1 File Offset: 0x00022BA1
		private bool TrieNodeIsLeaf(int node)
		{
			return this.m_trieNodes[node].Key == '\0' && this.TrieNodeIsNull(this.m_trieNodes[node].Child);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000249CF File Offset: 0x00022BCF
		private void TrieNodeKey_Set(int node, char value)
		{
			this.m_trieNodes[node].Key = value;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000249E3 File Offset: 0x00022BE3
		private void TrieNodeValue_Set(int node, int value)
		{
			this.m_trieNodes[node].Value = value;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000249F7 File Offset: 0x00022BF7
		private void TrieNodeSibling_Set(int node, int value)
		{
			this.m_trieNodes[node].Sibling = value;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00024A0B File Offset: 0x00022C0B
		private void TrieNodeChild_Set(int node, int value)
		{
			this.m_trieNodes[node].Child = value;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00024A1F File Offset: 0x00022C1F
		private void TrieNodeCompressedPath_Set(int node, string value)
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00024A21 File Offset: 0x00022C21
		private double Denominator(int leftLength, int rightLength)
		{
			return 10.0 * (double)(this.NormalizeByMaxLength ? Math.Max(leftLength, rightLength) : leftLength);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00024A40 File Offset: 0x00022C40
		private int CharCost(char precedingChar, char from, char to)
		{
			if (from != to)
			{
				return this.m_charCosts.GetCost(precedingChar, from, to);
			}
			return 0;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00024A56 File Offset: 0x00022C56
		public void SetCharCost(char from, short cost)
		{
			this.m_minCharCost = Math.Min(this.m_minCharCost, (int)cost);
			this.m_charCosts.SetCharCost(from, cost);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00024A77 File Offset: 0x00022C77
		public void SetCharCost(char from, char to, short cost)
		{
			this.m_minCharCost = Math.Min(this.m_minCharCost, (int)cost);
			this.m_charCosts.SetCharCost(from, to, cost);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00024A99 File Offset: 0x00022C99
		public void SetCharCost(char precedingChar, char from, char to, short cost)
		{
			this.m_minCharCost = Math.Min(this.m_minCharCost, (int)cost);
			this.m_charCosts.SetCharCost(precedingChar, from, to, cost);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00024ABE File Offset: 0x00022CBE
		private int TranspositionCost(char a, char b)
		{
			if (a == b)
			{
				return 0;
			}
			if (!char.IsDigit(a) || !char.IsDigit(b))
			{
				return 9;
			}
			return 15;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00024ADC File Offset: 0x00022CDC
		private int NewTrieNode()
		{
			if (this.m_trieNodes.Length == this.NodeCount)
			{
				Array.Resize<Trie.TrieNode>(ref this.m_trieNodes, this.m_trieNodes.Length * 2);
			}
			int nodeCount = this.NodeCount;
			this.NodeCount = nodeCount + 1;
			int num = nodeCount;
			this.m_trieNodes[num].Clear();
			return num;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00024B34 File Offset: 0x00022D34
		private int NewTrieNode(char c)
		{
			int num = this.NewTrieNode();
			this.TrieNodeKey_Set(num, c);
			return num;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00024B54 File Offset: 0x00022D54
		private int NewTrieNode(char c, int sibling)
		{
			int num = this.NewTrieNode();
			this.TrieNodeKey_Set(num, c);
			this.TrieNodeSibling_Set(num, sibling);
			return num;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00024B7C File Offset: 0x00022D7C
		private int NewTrieNode(char c, int sibling, int child)
		{
			int num = this.NewTrieNode();
			this.TrieNodeKey_Set(num, c);
			this.TrieNodeSibling_Set(num, sibling);
			this.TrieNodeChild_Set(num, child);
			return num;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00024BA9 File Offset: 0x00022DA9
		public bool Upsert(string s)
		{
			return this.TrieNodeValue(this.Add<StringWrapper>(s, null, 1)) > 1;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00024BC2 File Offset: 0x00022DC2
		public bool Upsert(string s, int value)
		{
			return this.TrieNodeValue(this.Add<StringWrapper>(s, null, value)) > value;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00024BDB File Offset: 0x00022DDB
		public bool Upsert<T>(T s) where T : IString
		{
			return this.TrieNodeValue(this.Add<T>(s, null, 1)) > 1;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00024BEF File Offset: 0x00022DEF
		public bool Upsert<T>(T s, int value) where T : IString
		{
			return this.TrieNodeValue(this.Add<T>(s, null, value)) > value;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00024C03 File Offset: 0x00022E03
		public void Add(string s)
		{
			this.Add<StringWrapper>(s, null, 1);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00024C14 File Offset: 0x00022E14
		public void Add(string s, int value)
		{
			this.Add<StringWrapper>(s, null, value);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00024C25 File Offset: 0x00022E25
		public void Add<T>(T s) where T : IString
		{
			this.Add<T>(s, null, 1);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00024C31 File Offset: 0x00022E31
		public void Add<T>(T s, int value) where T : IString
		{
			this.Add<T>(s, null, value);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00024C3D File Offset: 0x00022E3D
		private int FindSibling(int p, char c)
		{
			while (!this.TrieNodeIsNull(p) && this.TrieNodeKey(p) < c)
			{
				p = this.TrieNodeSibling(p);
			}
			return p;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00024C60 File Offset: 0x00022E60
		private int Add<T>(T s, string compressedPath, int value) where T : IString
		{
			this.Mass += (double)value;
			int num = this.Root;
			for (int i = 0; i < s.Length; i++)
			{
				while (!this.TrieNodeIsNull(this.TrieNodeSibling(num)) && this.TrieNodeKey(this.TrieNodeSibling(num)) <= s[i])
				{
					num = this.TrieNodeSibling(num);
				}
				if (this.TrieNodeKey(num) != s[i])
				{
					if (this.TrieNodeKey(num) < s[i])
					{
						this.TrieNodeSibling_Set(num, this.NewTrieNode(s[i], this.TrieNodeSibling(num)));
						num = this.TrieNodeSibling(num);
					}
					else
					{
						this.TrieNodeSibling_Set(num, this.NewTrieNode(this.TrieNodeKey(num), this.TrieNodeSibling(num), this.TrieNodeChild(num)));
						this.TrieNodeKey_Set(num, s[i]);
						this.TrieNodeChild_Set(num, 0);
					}
					if (i == 0)
					{
						this.Level0Nodes[(int)s[0]] = num;
					}
					for (int j = i + 1; j < s.Length; j++)
					{
						this.TrieNodeChild_Set(num, this.NewTrieNode(s[j]));
						num = this.TrieNodeChild(num);
					}
					break;
				}
				if (i == s.Length - 1)
				{
					break;
				}
				if (this.TrieNodeIsLeaf(num))
				{
					for (int k = i; k < s.Length; k++)
					{
						this.TrieNodeChild_Set(num, this.NewTrieNode(s[k]));
						num = this.TrieNodeChild(num);
					}
					break;
				}
				num = this.TrieNodeChild(num);
			}
			if (!this.TrieNodeIsLeaf(num))
			{
				if (!this.TrieNodeIsNull(this.TrieNodeChild(num)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num)))
				{
					int num2 = this.TrieNodeChild(num);
					this.TrieNodeValue_Set(num2, this.TrieNodeValue(num2) + value);
				}
				else
				{
					this.TrieNodeChild_Set(num, this.NewTrieNode('\0', this.TrieNodeChild(num)));
					num = this.TrieNodeChild(num);
					this.TrieNodeValue_Set(num, value);
					this.TrieNodeCompressedPath_Set(num, compressedPath);
					int count = this.Count;
					this.Count = count + 1;
					int num3 = s.Length + ((compressedPath == null) ? 0 : compressedPath.Length);
					if (num3 > this.MaxStringLength)
					{
						this.MaxStringLength = num3 * 2;
						this.m_matchContext = new Trie.MatchContext(this.MaxStringLength);
					}
				}
			}
			else
			{
				this.TrieNodeValue_Set(num, this.TrieNodeValue(num) + value);
			}
			return num;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00024F10 File Offset: 0x00023110
		public bool Contains(string s)
		{
			int num;
			return this.TryGetValue<StringWrapper>(s, out num);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00024F2C File Offset: 0x0002312C
		public bool Contains<T>(T s) where T : IString
		{
			int num;
			return this.TryGetValue<T>(s, out num);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00024F44 File Offset: 0x00023144
		public bool TryGetNodeValue(string s, out int value)
		{
			int num;
			if (!this.TryGetValue<StringWrapper>(s, out num))
			{
				value = 0;
				return false;
			}
			value = this.TrieNodeValue(num);
			return true;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00024F70 File Offset: 0x00023170
		public bool TryGetNodeValue<T>(T s, out int value) where T : IString
		{
			int num;
			if (!this.TryGetValue<T>(s, out num))
			{
				value = 0;
				return false;
			}
			value = this.TrieNodeValue(num);
			return true;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00024F98 File Offset: 0x00023198
		private bool TryGetValue<T>(T s, out int node) where T : IString
		{
			if (s.Length == 0)
			{
				node = 0;
				return false;
			}
			int num = 0;
			int num2 = this.Level0Nodes[(int)s[0]];
			if (!this.TrieNodeIsNull(num2) && s.Length >= 2)
			{
				num2 = this.TrieNodeChild(num2);
				num = 1;
			}
			return this.TryGetValue<T>(num2, s, num, out node);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00025000 File Offset: 0x00023200
		private bool TryGetValue<T>(int p, T s, int i, out int node) where T : IString
		{
			node = 0;
			while (!this.TrieNodeIsNull(p) && i < s.Length)
			{
				if (this.TrieNodeIsCompressedPath(p))
				{
					if (i + this.TrieNodeCompressedPath(p).Length != s.Length || StringWrapper.Compare<T, StringWrapper>(s, i, this.TrieNodeCompressedPath(p), 0, this.TrieNodeCompressedPath(p).Length) != 0)
					{
						return false;
					}
					node = p;
					return true;
				}
				else
				{
					p = this.FindSibling(p, s[i]);
					if (this.TrieNodeIsNull(p) || this.TrieNodeKey(p) != s[i])
					{
						return false;
					}
					i++;
					if (i == s.Length)
					{
						if (!this.TrieNodeIsNull(this.TrieNodeChild(p)) && this.TrieNodeIsLeaf(this.TrieNodeChild(p)))
						{
							node = this.TrieNodeChild(p);
							return true;
						}
						return false;
					}
					else
					{
						p = this.TrieNodeChild(p);
					}
				}
			}
			return false;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00025108 File Offset: 0x00023308
		private int TrieSearchProperPrefix(string s, int len)
		{
			int num = 0;
			int num2 = len;
			int num3 = 0;
			int num4 = this.Level0Nodes[(int)s.get_Chars(0)];
			if (len >= 2 && !this.TrieNodeIsNull(num4))
			{
				num4 = this.TrieNodeChild(num4);
				num = 1;
			}
			while (!this.TrieNodeIsNull(num4) && num < num2)
			{
				if (this.TrieNodeIsCompressedPath(num4))
				{
					string text = this.TrieNodeCompressedPath(num4);
					int length = text.Length;
					if (num + length > num2)
					{
						return num3;
					}
					int num5 = 0;
					while (num5 < length && text.get_Chars(num5) == s.get_Chars(num + num5))
					{
						num5++;
					}
					if (num5 == length && (num + length == num2 || this.IsSpace(s.get_Chars(num + num5)) || this.IsPunctuation(s.get_Chars(num + num5))))
					{
						len = num + num5;
						return num4;
					}
					return num3;
				}
				else
				{
					num4 = this.FindSibling(num4, s.get_Chars(num));
					if (this.TrieNodeIsNull(num4) || this.TrieNodeKey(num4) != s.get_Chars(num))
					{
						return num3;
					}
					num++;
					if (!this.TrieNodeIsNull(this.TrieNodeChild(num4)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num4)) && this.TrieNodeIsNull(this.TrieNodeChild(this.TrieNodeChild(num4))) && (num == num2 || this.IsSpace(s.get_Chars(num)) || this.IsPunctuation(s.get_Chars(num))))
					{
						len = num;
						num3 = this.TrieNodeChild(num4);
					}
					if (num == num2)
					{
						return num3;
					}
					num4 = this.TrieNodeChild(num4);
				}
			}
			return num3;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00025290 File Offset: 0x00023490
		private int TrieSearchPrefix(string s, ref int len)
		{
			int num = 0;
			int num2 = len;
			int num3 = 0;
			int num4 = this.Level0Nodes[(int)s.get_Chars(0)];
			if (len >= 2 && !this.TrieNodeIsNull(num4))
			{
				num4 = this.TrieNodeChild(num4);
				num = 1;
			}
			while (!this.TrieNodeIsNull(num4) && num < num2)
			{
				if (this.TrieNodeIsCompressedPath(num4))
				{
					string text = this.TrieNodeCompressedPath(num4);
					int length = text.Length;
					if (num + length > num2)
					{
						return num3;
					}
					int num5 = 0;
					while (num5 < length && text.get_Chars(num5) == s.get_Chars(num + num5))
					{
						num5++;
					}
					if (num5 == length)
					{
						len = num + num5;
						return num4;
					}
					return num3;
				}
				else
				{
					while (!this.TrieNodeIsNull(num4) && this.TrieNodeKey(num4) < s.get_Chars(num))
					{
						num4 = this.TrieNodeSibling(num4);
					}
					if (this.TrieNodeIsNull(num4) || this.TrieNodeKey(num4) != s.get_Chars(num))
					{
						return num3;
					}
					num++;
					if (!this.TrieNodeIsNull(this.TrieNodeChild(num4)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num4)) && this.TrieNodeIsNull(this.TrieNodeChild(this.TrieNodeChild(num4))))
					{
						len = num;
						num3 = this.TrieNodeChild(num4);
					}
					if (num == num2)
					{
						return num3;
					}
					num4 = this.TrieNodeChild(num4);
				}
			}
			return num3;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000253E0 File Offset: 0x000235E0
		private int SubtrieSearchPrefix(int root, string s, int len)
		{
			int num = 0;
			int num2 = len;
			int num3 = root;
			int num4 = 0;
			if (this.TrieNodeIsNull(num3))
			{
				return 0;
			}
			while (!this.TrieNodeIsNull(num3) && num < num2)
			{
				if (this.TrieNodeIsCompressedPath(num3))
				{
					string text = this.TrieNodeCompressedPath(num3);
					int length = text.Length;
					if (num + length > num2)
					{
						return num4;
					}
					int num5 = 0;
					while (num5 < length && text.get_Chars(num5) == s.get_Chars(num + num5))
					{
						num5++;
					}
					if (num5 == length)
					{
						len = num + num5;
						return num3;
					}
					return num4;
				}
				else
				{
					num3 = this.FindSibling(num3, s.get_Chars(num));
					if (this.TrieNodeIsNull(num3) || this.TrieNodeKey(num3) != s.get_Chars(num))
					{
						return num4;
					}
					num++;
					if (!this.TrieNodeIsNull(this.TrieNodeChild(num3)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num3)) && this.TrieNodeIsNull(this.TrieNodeChild(this.TrieNodeChild(num3))))
					{
						len = num;
						num4 = this.TrieNodeChild(num3);
					}
					if (num == num2)
					{
						return num4;
					}
					num3 = this.TrieNodeChild(num3);
				}
			}
			return num4;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000254F8 File Offset: 0x000236F8
		private string TrieSearchMapping(string s, ref int nrw)
		{
			int num = 0;
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			nrw = 0;
			int num2 = this.Level0Nodes[(int)s.get_Chars(0)];
			if (s.get_Chars(1) != '\0' && !this.TrieNodeIsNull(num2))
			{
				num2 = this.TrieNodeChild(num2);
				num = 1;
			}
			while (!this.TrieNodeIsNull(num2) && s.get_Chars(num) != '\0')
			{
				if (s.get_Chars(num) == ' ')
				{
					nrw++;
				}
				if (this.TrieNodeIsLeaf(num2))
				{
					if (s.get_Chars(num - 1) == ' ')
					{
						return this.TrieNodeCompressedPath(num2);
					}
					return null;
				}
				else
				{
					num2 = this.FindSibling(num2, s.get_Chars(num));
					if (this.TrieNodeIsNull(num2) || this.TrieNodeKey(num2) != s.get_Chars(num))
					{
						return null;
					}
					num++;
					num2 = this.TrieNodeChild(num2);
				}
			}
			if (s.get_Chars(num) == '\0' && this.TrieNodeIsLeaf(num2))
			{
				return this.TrieNodeCompressedPath(num2);
			}
			return null;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000255D8 File Offset: 0x000237D8
		public IEnumerable<Trie.MatchResult> FindSimilarDF(string s, int maxDistance)
		{
			this.m_dfContext.Reset();
			this.m_dfContext.SetMaxErrors(maxDistance / 10);
			this.m_dfContext.SetPattern(s);
			this.DFSearch(this.m_dfContext, this.Root, 1);
			return this.m_dfContext.GetMatchResultsEnumerable();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0002562C File Offset: 0x0002382C
		public void DFSearch(Trie.DFContext dfContext, int aNode, int level)
		{
			int num = aNode;
			if (this.TrieNodeIsLeaf(num))
			{
				if (this.TrieNodeIsCompressedPath(num))
				{
					string text = this.TrieNodeCompressedPath(num);
					for (int i = 0; i < text.Length; i++)
					{
						char c = text.get_Chars(i);
						dfContext.W[level] = c;
						if (dfContext.W[level] == '\0')
						{
							dfContext.AddResult(dfContext.W, level, dfContext.eDistance, this.TrieNodeValue(num));
							return;
						}
						if (dfContext.EditDistance(level) == dfContext.Infinity)
						{
							return;
						}
						level++;
					}
					return;
				}
				if (level > 1)
				{
					dfContext.W[level] = this.TrieNodeKey(num);
					dfContext.AddResult(dfContext.W, level, dfContext.eDistance, this.TrieNodeValue(num));
				}
				num = this.TrieNodeSibling(num);
			}
			int num2 = num;
			for (;;)
			{
				dfContext.W[level] = this.TrieNodeKey(num2);
				if (dfContext.W[level] == '\0')
				{
					if (level > 1)
					{
						dfContext.AddResult(dfContext.W, level, dfContext.eDistance, this.TrieNodeValue(num2));
					}
					num2 = this.TrieNodeSibling(num2);
				}
				else if (dfContext.EditDistance(level) == dfContext.Infinity)
				{
					num2 = this.TrieNodeSibling(num2);
				}
				else
				{
					if (!this.TrieNodeIsLeaf(num2))
					{
						this.DFSearch(dfContext, this.TrieNodeChild(num2), level + 1);
					}
					num2 = this.TrieNodeSibling(num2);
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0002576C File Offset: 0x0002396C
		private void LevenshteinSearch<T>(Trie.MatchContext mc, int p, char c, int j, T s, int minValue) where T : IString
		{
			int length = s.Length;
			Matrix<Trie.LevCell> matrix = mc.Matrix;
			char[] aux_word = mc.aux_word;
			if (this.TrieNodeIsLeaf(p))
			{
				if (this.TrieNodeIsCompressedPath(p))
				{
					string text = this.TrieNodeCompressedPath(p);
					int num = matrix[0][j].Cost;
					for (int i = 1; i <= length; i++)
					{
						if (matrix[i][j].Cost < num)
						{
							num = matrix[i][j].Cost;
						}
					}
					int num2 = 0;
					while (num2 < text.Length && num <= mc.MaxDistance)
					{
						Trie.LevCell levCell = matrix[0][j + num2 + 1];
						levCell.Cost = matrix[0][j + num2].Cost + this.CharCost(c, '\0', text.get_Chars(num2));
						levCell.SetFromToSpan('\0', text.get_Chars(num2), 0);
						num = mc.MaxDistance + 1;
						for (int k = 1; k <= length; k++)
						{
							int num3 = 0;
							Trie.LevCell levCell2 = matrix[k - 1][j + num2 + 1];
							Trie.LevCell levCell3 = matrix[k][j + num2];
							Trie.LevCell levCell4 = matrix[k - 1][j + num2];
							levCell = matrix[k][j + num2 + 1];
							int num4;
							if (levCell2.To == s[k - 1] && levCell2.From == '\0' && k >= 5 && levCell2.Span <= 2)
							{
								num4 = levCell2.Cost;
								num3 = 1;
							}
							else
							{
								num4 = levCell2.Cost + this.CharCost(text.get_Chars(num2), s[k - 1], '\0');
							}
							int num5;
							if (levCell3.From == text.get_Chars(num2) && levCell3.To == '\0' && k >= 5 && levCell3.Span <= 2)
							{
								num5 = levCell3.Cost;
								num3 = 2;
							}
							else
							{
								num5 = levCell3.Cost + this.CharCost(c, '\0', text.get_Chars(num2));
							}
							int num6 = levCell4.Cost + this.CharCost(c, s[k - 1], text.get_Chars(num2));
							if (num6 <= num5 && num6 <= num4)
							{
								levCell.Cost = num6;
								levCell.From = levCell4.From;
								levCell.To = levCell4.To;
								levCell.Span = levCell4.Span + 1;
							}
							else if (num5 < num4)
							{
								levCell.Cost = num5;
								levCell.From = '\0';
								levCell.To = ((num3 == 2) ? '\0' : text.get_Chars(num2));
								levCell.Span = 0;
							}
							else
							{
								levCell.Cost = num4;
								levCell.From = ((num3 == 1) ? '\0' : s[k - 1]);
								levCell.To = '\0';
								levCell.Span = 0;
							}
							if (k > 1 && s[k - 2] == text.get_Chars(num2) && ((num2 != 0 && s[k - 1] == text.get_Chars(num2 - 1)) || (num2 == 0 && s[k - 1] == c)))
							{
								int num7 = matrix[k - 2][j + num2 - 1].Cost + this.TranspositionCost(s[k - 1], s[k - 2]);
								if (num7 < levCell.Cost)
								{
									levCell.Cost = num7;
								}
							}
							if (levCell.Cost < num)
							{
								num = levCell.Cost;
							}
						}
						c = text.get_Chars(num2);
						num2++;
					}
					if (num2 == text.Length && matrix[length][j + num2].Cost <= mc.MaxDistance && matrix[length][j + num2].Cost >= mc.MinDistance)
					{
						mc.AddMatch<StringWrapper>(aux_word, j, text, matrix[length][j + num2].Cost, this.TrieNodeValue(p));
					}
					return;
				}
				if (this.TrieNodeValue(p) > minValue && matrix[length][j].Cost <= mc.MaxDistance && matrix[length][j].Cost >= mc.MinDistance && (double)matrix[length][j].Cost / this.Denominator(length, j) <= 1.0 - mc.Threshold)
				{
					mc.AddMatch(aux_word, j, matrix[length][j].Cost, this.TrieNodeValue(p));
				}
				p = this.TrieNodeSibling(p);
			}
			matrix[0][j + 1].Span = 0;
			matrix[0][j + 1].From = '\0';
			while (!this.TrieNodeIsNull(p))
			{
				if (this.TrieNodeKey(p) == ' ' && (!mc.AllowSpaces || length - j <= 1 || j <= 1))
				{
					p = this.TrieNodeSibling(p);
					if (this.TrieNodeIsNull(p))
					{
						return;
					}
				}
				matrix[0][j + 1].Cost = matrix[0][j].Cost + this.CharCost(c, '\0', this.TrieNodeKey(p));
				matrix[0][j + 1].To = this.TrieNodeKey(p);
				int num8 = matrix[0][j + 1].Cost;
				for (int l = 1; l <= length; l++)
				{
					int num9 = 0;
					int num10;
					if (matrix[l - 1][j + 1].To == s[l - 1] && l >= 5 && matrix[l - 1][j + 1].Span <= 2)
					{
						num10 = matrix[l - 1][j + 1].Cost;
						num9 = 1;
					}
					else
					{
						num10 = matrix[l - 1][j + 1].Cost + this.CharCost(this.TrieNodeKey(p), s[l - 1], '\0');
					}
					int num11;
					if (matrix[l][j].From == this.TrieNodeKey(p) && l >= 5 && matrix[l][j].Span <= 2)
					{
						num11 = matrix[l][j].Cost;
						num9 = 2;
					}
					else
					{
						num11 = matrix[l][j].Cost + this.CharCost(c, '\0', this.TrieNodeKey(p));
					}
					int num12;
					if (this.TrieNodeKey(p) == ' ' && (l <= 2 || length - l <= 2))
					{
						num12 = mc.MaxDistance + 1;
					}
					else
					{
						num12 = matrix[l - 1][j].Cost + this.CharCost(c, s[l - 1], this.TrieNodeKey(p));
					}
					int num13;
					if (num11 <= num10)
					{
						if (num11 <= num12)
						{
							mc.Operations[l] = Trie.OperationType2.Insertion;
							num13 = num11;
							matrix[l][j + 1].From = '\0';
							matrix[l][j + 1].To = ((num9 == 2) ? '\0' : this.TrieNodeKey(p));
							matrix[l][j + 1].Span = 0;
						}
						else
						{
							mc.Operations[l] = ((s[l - 1] != this.TrieNodeKey(p)) ? Trie.OperationType2.Substitution : Trie.OperationType2.Equality);
							num13 = num12;
							matrix[l][j + 1].From = matrix[l - 1][j].From;
							matrix[l][j + 1].To = matrix[l - 1][j].To;
							matrix[l][j + 1].Span = matrix[l - 1][j].Span + 1;
						}
					}
					else if (num10 <= num12)
					{
						mc.Operations[l] = Trie.OperationType2.Deletion;
						num13 = num10;
						matrix[l][j + 1].From = ((num9 == 1) ? '\0' : s[l - 1]);
						matrix[l][j + 1].To = '\0';
						matrix[l][j + 1].Span = 0;
					}
					else
					{
						mc.Operations[l] = ((s[l - 1] != this.TrieNodeKey(p)) ? Trie.OperationType2.Substitution : Trie.OperationType2.Equality);
						num13 = num12;
						matrix[l][j + 1].From = matrix[l - 1][j].From;
						matrix[l][j + 1].To = matrix[l - 1][j].To;
						matrix[l][j + 1].Span = matrix[l - 1][j].Span + 1;
					}
					if (l >= 2 && s[l - 2] == this.TrieNodeKey(p) && s[l - 1] == c)
					{
						int num14 = matrix[l - 2][j - 1].Cost + this.TranspositionCost(s[l - 1], s[l - 2]);
						if (l == 2)
						{
							num14 += 5;
						}
						if (num14 < num13)
						{
							mc.Operations[l] = Trie.OperationType2.Transposition;
							num13 = num14;
						}
					}
					matrix[l][j + 1].Cost = num13;
					if (num13 < num8)
					{
						num8 = num13;
					}
					if (l > j && num13 > mc.MaxDistance + 12)
					{
						while (l < length - 1)
						{
							l++;
							matrix[l][j + 1].Cost = mc.MaxDistance + 12;
						}
					}
				}
				if (num8 <= mc.MaxDistance + this.m_maxDelToTransposeCostDelta)
				{
					aux_word[j] = this.TrieNodeKey(p);
					if (num8 + this.m_minCharCost <= mc.MaxDistance + this.m_maxDelToTransposeCostDelta)
					{
						this.LevenshteinSearch<T>(mc, this.TrieNodeChild(p), this.TrieNodeKey(p), j + 1, s, minValue);
					}
					else
					{
						for (int m = 1; m < length; m++)
						{
							if (mc.Operations[m] == Trie.OperationType2.Substitution && s[m] == this.TrieNodeKey(p) && matrix[m][j + 1].Cost + this.TranspositionCost(s[m - 1], s[m]) - this.CharCost(c, s[m - 1], s[m]) <= mc.MaxDistance)
							{
								Trie.StringWithReplacedChar<T> stringWithReplacedChar = new Trie.StringWithReplacedChar<T>(s, m, 0, s[m - 1]);
								int num15;
								if (this.TryGetValue<Trie.StringWithReplacedChar<T>>(this.TrieNodeChild(p), stringWithReplacedChar, 0, out num15))
								{
									int num16 = matrix[m][j + 1].Cost - this.CharCost(c, stringWithReplacedChar[0], this.TrieNodeKey(p)) + this.TranspositionCost(stringWithReplacedChar[0], (char)mc.Operations[m]);
									if (m == 1)
									{
										num16 += 5;
									}
									if ((double)num16 / this.Denominator(length, j + 1 + stringWithReplacedChar.Length) <= 1.0 - mc.Threshold)
									{
										mc.AddMatch<Trie.StringWithReplacedChar<T>>(aux_word, j + 1, stringWithReplacedChar, num16, this.TrieNodeValue(num15));
									}
								}
							}
						}
						for (int n = 0; n < length; n++)
						{
							int num17;
							if (matrix[n][j + 1].Cost <= mc.MaxDistance && (this.TrieNodeKey(p) != ' ' || (mc.AllowSpaces && j >= 2 && length - n >= 3)) && (double)matrix[n][j + 1].Cost / this.Denominator(length, j + 1 + s.Length - n) <= 1.0 - mc.Threshold && this.TryGetValue<T>(this.TrieNodeChild(p), s, n, out num17))
							{
								mc.AddMatch<T>(aux_word, j + 1, s, n, matrix[n][j + 1].Cost, this.TrieNodeValue(num17));
							}
						}
						if (!this.TrieNodeIsNull(this.TrieNodeChild(p)) && this.TrieNodeIsLeaf(this.TrieNodeChild(p)) && !this.TrieNodeIsCompressedPath(this.TrieNodeChild(p)) && matrix[length][j + 1].Cost <= mc.MaxDistance && matrix[length][j + 1].Cost >= mc.MinDistance && (double)matrix[length][j + 1].Cost / this.Denominator(length, j + 1) <= 1.0 - mc.Threshold && this.TrieNodeValue(this.TrieNodeChild(p)) > minValue)
						{
							mc.AddMatch(aux_word, j + 1, matrix[length][j + 1].Cost, this.TrieNodeValue(this.TrieNodeChild(p)));
						}
					}
				}
				p = this.TrieNodeSibling(p);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000265E0 File Offset: 0x000247E0
		public IEnumerable<Trie.MatchResult> FindSimilar(string s, int maxDistance)
		{
			return this.FindSimilar(s, 0, maxDistance, 0, true);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000265ED File Offset: 0x000247ED
		public IEnumerable<Trie.MatchResult> FindSimilar<T>(T s, int maxDistance) where T : IString
		{
			return this.FindSimilar<T>(s, 0, maxDistance, 0, true);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000265FA File Offset: 0x000247FA
		public IEnumerable<Trie.MatchResult> FindSimilar(string s, int minDistance, int maxDistance, int minValue, bool allowSpaces)
		{
			return this.FindSimilar(this.m_matchContext, s, minDistance, maxDistance, minValue, allowSpaces);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002660F File Offset: 0x0002480F
		public IEnumerable<Trie.MatchResult> FindSimilar<T>(T s, int minDistance, int maxDistance, int minValue, bool allowSpaces) where T : IString
		{
			return this.FindSimilar<T>(this.m_matchContext, s, minDistance, maxDistance, minValue, allowSpaces);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00026624 File Offset: 0x00024824
		public IEnumerable<Trie.MatchResult> FindSimilar(Trie.MatchContext mc, string s, int minDistance, int maxDistance, int minValue, bool allowSpaces)
		{
			return this.FindSimilar<StringWrapper>(mc, s, minDistance, maxDistance, minValue, allowSpaces);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0002663A File Offset: 0x0002483A
		public IEnumerable<Trie.MatchResult> FindSimilar<T>(Trie.MatchContext mc, T s, int maxDistance) where T : IString
		{
			return this.FindSimilar<T>(mc, s, 0, maxDistance, 0, true);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00026648 File Offset: 0x00024848
		public IEnumerable<Trie.MatchResult> FindSimilar<T>(Trie.MatchContext mc, T s, int minDistance, int maxDistance, int minValue, bool allowSpaces) where T : IString
		{
			mc.Reset();
			mc.MinDistance = minDistance;
			mc.MaxDistance = maxDistance;
			mc.AllowSpaces = allowSpaces;
			if (this.Count > 0)
			{
				this.InitializeCostMatrix<T>(mc, s);
				this.LevenshteinSearch<T>(mc, this.TrieNodeSibling(this.Root), '\0', 0, s, minValue);
			}
			return mc;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002669C File Offset: 0x0002489C
		private int TrieLexicNextToken(string s)
		{
			int length = s.Length;
			int num = 0;
			int num2 = this.TrieSearchPrefix(s, ref length);
			if (!this.TrieNodeIsNull(num2))
			{
				if (s.get_Chars(length) <= '\u0003' || this.IsSpace(s.get_Chars(length)) || this.IsPunctuation(s.get_Chars(length)))
				{
					return length;
				}
				num = length - 1;
				while (num >= 0 && !this.IsSpace(s.get_Chars(num)) && !this.IsPunctuation(s.get_Chars(num)))
				{
					num--;
				}
				if (num < 0)
				{
					num = length;
					while (s.get_Chars(num) > '\u0003' && !this.IsSpace(s.get_Chars(num)) && !this.IsPunctuation(s.get_Chars(num)))
					{
						num++;
					}
					return num;
				}
				while (num >= 0 && (this.IsSpace(s.get_Chars(num)) || this.IsPunctuation(s.get_Chars(num))))
				{
					num--;
				}
				if (num < 0)
				{
					return 0;
				}
				return num + 1;
			}
			else
			{
				if (this.IsPunctuation(s.get_Chars(0)) || s.get_Chars(0) <= '\u0003')
				{
					return 0;
				}
				while (s.get_Chars(num) > '\u0003' && !this.IsSpace(s.get_Chars(num)) && !this.IsPunctuation(s.get_Chars(num)))
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000267D0 File Offset: 0x000249D0
		private int TrieSearchSubstring(string s, Trie.Token[] toklist, int start, int wcnt, uint minval, uint maxval, bool allowexactmatch, bool allowprefix)
		{
			int i = start;
			int num = -1;
			while (i < wcnt)
			{
				int num2 = wcnt;
				while (i < wcnt && num2 > i)
				{
					int num3 = toklist[num2 - 1].Position - toklist[i].Position + toklist[num2 - 1].len;
					if (!allowexactmatch && i == start && num3 == toklist[wcnt].Position - toklist[start].Position)
					{
						num3--;
					}
					int num4 = this.TrieSearchPrefix(s + toklist[i].Position, ref num3);
					if (!this.TrieNodeIsNull(num4) && (num3 >= toklist[i].len || allowprefix))
					{
						if ((long)this.TrieNodeValue(num4) >= (long)((ulong)minval) && (long)this.TrieNodeValue(num4) <= (long)((ulong)maxval) && (allowprefix || s.get_Chars(toklist[i].Position + num3) == '\0' || this.IsSpace(s.get_Chars(toklist[i].Position + num3)) || this.IsPunctuation(s.get_Chars(toklist[i].Position + num3))))
						{
							num = i;
							i = wcnt + 1;
						}
						else
						{
							int j = toklist[i].len;
							num2 = i;
							while (j < num3)
							{
								num2++;
								j = toklist[num2].Position - toklist[i].Position + toklist[num2].len;
							}
						}
					}
					else
					{
						num2 = 0;
					}
				}
				i++;
			}
			if (i > wcnt)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00026930 File Offset: 0x00024B30
		private float LevenshteinDistance<T>(Trie.MatchContext mc, T s, T t, short maxDistance) where T : IString
		{
			Matrix<Trie.LevCell> matrix = mc.Matrix;
			this.InitializeCostMatrix<T>(mc, s);
			char c = '\0';
			int num = 0;
			int num2 = 1;
			while (num2 <= t.Length && num <= (int)maxDistance)
			{
				matrix[0][num2].Cost = matrix[0][num2 - 1].Cost + this.CharCost(c, '\0', t[num2 - 1]);
				matrix[0][num2].From = '\0';
				matrix[0][num2].To = t[num2 - 1];
				matrix[0][num2].Span = 0;
				num = matrix[0][num2].Cost;
				for (int i = 1; i <= s.Length; i++)
				{
					int num3 = 0;
					int num4;
					if (matrix[i - 1][num2].Span <= 3 && matrix[i - 1][num2].From == '\0' && matrix[i - 1][num2].To == s[i - 1])
					{
						num4 = matrix[i - 1][num2].Cost;
						num3 = 1;
					}
					else
					{
						num4 = matrix[i - 1][num2].Cost + this.CharCost(c, s[i - 1], '\0');
					}
					int num5;
					if (matrix[i][num2 - 1].Span <= 3 && matrix[i][num2 - 1].From == t[num2 - 1] && matrix[i][num2].To == '\0')
					{
						num5 = matrix[i][num2 - 1].Cost;
						num3 = 2;
					}
					else
					{
						num5 = matrix[i][num2 - 1].Cost + this.CharCost(c, '\0', t[num2 - 1]);
					}
					int num6 = matrix[i - 1][num2 - 1].Cost + this.CharCost(c, s[i - 1], t[num2 - 1]);
					int num7;
					if (num5 < num4)
					{
						if (num5 < num6)
						{
							num7 = num5;
							matrix[i][num2].From = '\0';
							if (num3 == 2)
							{
								matrix[i][num2].To = '\0';
							}
							else
							{
								matrix[i][num2].To = t[num2 - 1];
							}
							matrix[i][num2].Span = 0;
						}
						else
						{
							num7 = num6;
							matrix[i][num2].From = matrix[i - 1][num2 - 1].From;
							matrix[i][num2].To = matrix[i - 1][num2 - 1].To;
							matrix[i][num2].Span = matrix[i - 1][num2 - 1].Span + 1;
						}
					}
					else if (num4 < num6)
					{
						num7 = num4;
						if (num3 == 1)
						{
							matrix[i][num2].From = '\0';
						}
						else
						{
							matrix[i][num2].From = s[i - 1];
						}
						matrix[i][num2].To = '\0';
						matrix[i][num2].Span = 0;
					}
					else
					{
						num7 = num6;
						matrix[i][num2].From = matrix[i - 1][num2 - 1].From;
						matrix[i][num2].To = matrix[i - 1][num2 - 1].To;
						matrix[i][num2].Span = matrix[i - 1][num2 - 1].Span + 1;
					}
					if (i > 1 && num2 > 1 && s[i - 2] == t[num2 - 1] && s[i - 1] == t[num2 - 2])
					{
						int num8 = matrix[i - 2][num2 - 2].Cost + this.TranspositionCost(s[i - 1], s[i - 2]);
						if (num8 < num7)
						{
							num7 = num8;
						}
					}
					matrix[i][num2].Cost = num7;
					if (num7 < num)
					{
						num = num7;
					}
				}
				if (num > (int)maxDistance)
				{
					return (float)(0.1 * (double)(maxDistance + 1));
				}
				c = t[num2 - 1];
				num2++;
			}
			return (float)(0.1 * (double)matrix[s.Length][t.Length].Cost);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00026ED8 File Offset: 0x000250D8
		private void InitializeCostMatrix<T>(Trie.MatchContext mc, T s) where T : IString
		{
			mc.Resize(s.Length, this.MaxStringLength);
			Matrix<Trie.LevCell> matrix = mc.Matrix;
			matrix[0][0].Cost = 0;
			for (int i = 1; i <= s.Length; i++)
			{
				matrix[i][0].Cost = matrix[i - 1][0].Cost + this.CharCost('\0', s[i - 1], '\0');
				matrix[i][0].From = s[i - 1];
				matrix[i][0].To = '\0';
				matrix[i][0].Span = 0;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00026FC0 File Offset: 0x000251C0
		public void LoadEditWeights(string filename)
		{
			this.m_maxDelToTransposeCostDelta = 2;
			for (char c = '\0'; c <= '\uffff'; c += '\u0001')
			{
				for (char c2 = '\0'; c2 <= '\uffff'; c2 += '\u0001')
				{
					for (char c3 = '\0'; c3 <= '\uffff'; c3 += '\u0001')
					{
						if (c2 == c3)
						{
							this.SetCharCost(c, c2, c3, 0);
						}
						else
						{
							this.SetCharCost(c, c2, c3, 10);
						}
					}
				}
			}
			for (char c4 = '\u0001'; c4 <= '\uffff'; c4 += '\u0001')
			{
				for (char c5 = '\0'; c5 <= '\uffff'; c5 += '\u0001')
				{
					this.SetCharCost(c4, '\0', c5, 9);
				}
				this.SetCharCost(c4, '\0', ' ', 10);
				this.SetCharCost(c4, ' ', '\0', 10);
			}
			for (char c6 = '\0'; c6 <= '\uffff'; c6 += '\u0001')
			{
				for (char c7 = '\0'; c7 <= '\uffff'; c7 += '\u0001')
				{
					if (c6 != c7)
					{
						this.SetCharCost('\0', c6, c7, 15);
						this.SetCharCost(' ', c6, c7, 15);
					}
					else
					{
						this.SetCharCost('\0', c6, c7, 0);
						this.SetCharCost(' ', c6, c7, 0);
					}
				}
			}
			for (char c8 = '\0'; c8 <= '9'; c8 += '\u0001')
			{
				for (char c9 = '\0'; c9 <= '\uffff'; c9 += '\u0001')
				{
					for (char c10 = '\0'; c10 <= '\uffff'; c10 += '\u0001')
					{
						if (c8 != c10)
						{
							this.SetCharCost(c9, c8, c10, 20);
							this.SetCharCost(c9, c10, c8, 20);
						}
					}
					this.SetCharCost(c9, '\0', c8, 15);
				}
				this.SetCharCost('\0', '\0', c8, 20);
			}
			this.SetCharCost('\0', '\'', '"', 10);
			this.SetCharCost('\0', '\'', '`', 10);
			if (!string.IsNullOrEmpty(filename))
			{
				using (FileStream fileStream = File.Open(filename, 3))
				{
					using (StreamReader streamReader = new StreamReader(fileStream))
					{
						char[] array = new char[11];
						while (!streamReader.EndOfStream)
						{
							string text = streamReader.ReadLine();
							int num = text.IndexOf('\t');
							short num2 = short.Parse(text.Substring(0, num));
							int i = num + 1;
							int num3 = 0;
							while (i < text.Length)
							{
								array[num3] = text.get_Chars(i);
								i++;
								num3++;
							}
							for (int j = 0; j < 3; j++)
							{
								if (array[j] == '^')
								{
									array[j] = '\0';
								}
							}
							if (array[0] == '~')
							{
								for (char c11 = '\u0001'; c11 <= '\uffff'; c11 += '\u0001')
								{
									if (c11 != ' ')
									{
										this.SetCharCost(c11, array[2], array[1], num2);
									}
								}
							}
							else
							{
								this.SetCharCost(array[0], array[2], array[1], num2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00027290 File Offset: 0x00025490
		private void TrieToHistogram(int q, int wordpos, StreamWriter sw, int minValue)
		{
			char[] array = new char[this.MaxStringLength];
			while (!this.TrieNodeIsNull(q))
			{
				array[wordpos] = this.TrieNodeKey(q);
				if (this.TrieNodeIsLeaf(q))
				{
					if (this.TrieNodeValue(q) >= minValue)
					{
						sw.Write("{0}\t{1}", this.TrieNodeValue(q), array);
						if (this.TrieNodeIsCompressedPath(q))
						{
							sw.Write("{0}", this.TrieNodeCompressedPath(q));
						}
						sw.WriteLine();
					}
				}
				else
				{
					this.TrieToHistogram(this.TrieNodeChild(q), wordpos + 1, sw, minValue);
				}
				q = this.TrieNodeSibling(q);
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002732C File Offset: 0x0002552C
		public void WriteHistogram(string filename, int minValue)
		{
			using (FileStream fileStream = File.Open(filename, 4))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					this.TrieToHistogram(this.TrieNodeSibling(this.Root), 0, streamWriter, minValue);
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00027390 File Offset: 0x00025590
		private void TrieDestroy(int p)
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00027394 File Offset: 0x00025594
		private void SubtrieDestroy(int p)
		{
			int num;
			if (p.Equals(this.Root))
			{
				if (this.TrieNodeIsNull(this.TrieNodeSibling(p)))
				{
					return;
				}
				num = this.TrieNodeSibling(p);
				this.TrieNodeSibling_Set(p, 0);
			}
			else
			{
				num = this.TrieNodeChild(p);
				this.TrieNodeChild_Set(p, 0);
			}
			this.TrieDestroy(num);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000273EC File Offset: 0x000255EC
		private int aux_findcompressedpath(string s)
		{
			int num = 0;
			int length = s.Length;
			int num2 = this.Level0Nodes[(int)s.get_Chars(0)];
			if (length >= 2 && !this.TrieNodeIsNull(num2))
			{
				num2 = this.TrieNodeChild(num2);
				num = 1;
			}
			while (!this.TrieNodeIsNull(num2) && num < length)
			{
				while (!this.TrieNodeIsNull(num2) && this.TrieNodeKey(num2) < s.get_Chars(num))
				{
					num2 = this.TrieNodeSibling(num2);
				}
				if (this.TrieNodeIsNull(num2) || this.TrieNodeKey(num2) != s.get_Chars(num))
				{
					return 0;
				}
				num++;
				if (num == length)
				{
					if (!this.TrieNodeIsNull(this.TrieNodeChild(num2)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num2)) && !this.TrieNodeIsNull(this.TrieNodeChild(this.TrieNodeChild(num2))))
					{
						return this.TrieNodeChild(num2);
					}
					return 0;
				}
				else
				{
					num2 = this.TrieNodeChild(num2);
				}
			}
			return 0;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000274D0 File Offset: 0x000256D0
		private void TrieProcess(int p, Trie.TrieNodeFunction f)
		{
			while (!this.TrieNodeIsNull(p))
			{
				if (this.TrieNodeIsLeaf(p))
				{
					f(p);
				}
				if (!this.TrieNodeIsNull(p))
				{
					if (!this.TrieNodeIsLeaf(p))
					{
						this.TrieProcess(this.TrieNodeChild(p), f);
					}
					p = this.TrieNodeSibling(p);
				}
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00027521 File Offset: 0x00025721
		private void Nullify(int p)
		{
			if (this.TrieNodeIsLeaf(p))
			{
				this.TrieNodeValue_Set(p, 0);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00027534 File Offset: 0x00025734
		private void SubtrieNullify(int p)
		{
			this.TrieProcess(p, new Trie.TrieNodeFunction(this.Nullify));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00027549 File Offset: 0x00025749
		private void TrieNullify()
		{
			this.TrieProcess(this.TrieNodeSibling(this.Root), new Trie.TrieNodeFunction(this.Nullify));
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0002756C File Offset: 0x0002576C
		private uint GetNodeCount(int p)
		{
			uint num = 0U;
			while (!this.TrieNodeIsNull(p))
			{
				if (!this.TrieNodeIsLeaf(p))
				{
					num += this.GetNodeCount(this.TrieNodeChild(p)) + 1U;
				}
				else
				{
					num += 1U;
				}
				p = this.TrieNodeSibling(p);
			}
			return num;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000275B1 File Offset: 0x000257B1
		private uint TrieNodeCount()
		{
			return 1U + this.GetNodeCount(this.TrieNodeSibling(this.Root));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000275C7 File Offset: 0x000257C7
		private uint SubtrieNodeCount(int root)
		{
			if (this.TrieNodeIsNull(root))
			{
				return 0U;
			}
			if (this.TrieNodeIsLeaf(root) || this.TrieNodeIsNull(this.TrieNodeChild(root)))
			{
				return 1U;
			}
			return 1U + this.GetNodeCount(this.TrieNodeChild(root));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00027600 File Offset: 0x00025800
		private int SubtrieMass(int root)
		{
			if (this.TrieNodeIsLeaf(root) || this.TrieNodeIsNull(this.TrieNodeChild(root)))
			{
				return this.TrieNodeValue(root);
			}
			Trie.MassHandler massHandler = new Trie.MassHandler();
			this.TrieProcess(this.TrieNodeChild(root), new Trie.TrieNodeFunction(massHandler.AccumulateMass));
			return massHandler.TotalMass;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00027654 File Offset: 0x00025854
		private int SubtrieMassRedistribute(int root, int newmass, int oldmass)
		{
			if (newmass == 0)
			{
				this.SubtrieNullify(root);
			}
			if (this.TrieNodeIsNull(root) || this.TrieNodeIsLeaf(root) || this.TrieNodeIsNull(this.TrieNodeChild(root)))
			{
				return 0;
			}
			int num;
			if (oldmass == 0)
			{
				num = this.SubtrieMass(root);
			}
			else
			{
				num = oldmass;
			}
			if (num != newmass)
			{
				Trie.ChangeMassHandler changeMassHandler = new Trie.ChangeMassHandler(num, newmass);
				this.TrieProcess(this.TrieNodeChild(root), new Trie.TrieNodeFunction(changeMassHandler.ChangeMass));
			}
			return 1;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000276C8 File Offset: 0x000258C8
		private void TrieProcessWithPath(int p, Trie.TrieNodeFunction f)
		{
			Trie.NodePathHandler nodePathHandler = new Trie.NodePathHandler(f);
			this.TrieProcessWithPath(p, nodePathHandler, 0);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000276E8 File Offset: 0x000258E8
		private void TrieProcessWithPath(int p, Trie.NodePathHandler h, int pos)
		{
			while (!this.TrieNodeIsNull(p))
			{
				if (this.TrieNodeIsLeaf(p))
				{
					h.aux_triepathlength = pos;
					h.Process(p);
				}
				if (!this.TrieNodeIsNull(p))
				{
					if (!this.TrieNodeIsLeaf(p))
					{
						h.aux_triepath[pos] = this.TrieNodeKey(p);
						this.TrieProcessWithPath(this.TrieNodeChild(p), h, pos + 1);
					}
					p = this.TrieNodeSibling(p);
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00027754 File Offset: 0x00025954
		private static void strncpy(char[] dest, int dest_start, char[] src, int src_start, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Length must be >= 0.");
			}
			int num = 0;
			while (num < len && num + dest_start < dest.Length)
			{
				dest[num + dest_start] = ((num + src_start < src.Length) ? src[num + src_start] : '\0');
				num++;
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002779C File Offset: 0x0002599C
		private static void strncpy<T>(char[] dest, int dest_start, T src, int src_start, int len) where T : IString
		{
			if (len < 0)
			{
				throw new ArgumentException("Length must be >= 0.");
			}
			int num = 0;
			while (num < len && num + dest_start < dest.Length)
			{
				dest[num + dest_start] = ((num + src_start < src.Length) ? src[num + src_start] : '\0');
				num++;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000277F8 File Offset: 0x000259F8
		private static void strcpy(char[] dest, char[] src)
		{
			int num = 0;
			while (num < src.Length && num < dest.Length)
			{
				dest[num] = src[num];
				num++;
			}
			if (num < dest.Length)
			{
				dest[num] = '\0';
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00027829 File Offset: 0x00025A29
		private bool IsSpace(char c)
		{
			return this.RemoveWhitespace && char.IsWhiteSpace(c);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002783B File Offset: 0x00025A3B
		private bool IsPunctuation(char c)
		{
			return this.RemovePunctuation && char.IsPunctuation(c);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002784D File Offset: 0x00025A4D
		private static string strchomp(string s)
		{
			return s;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00027850 File Offset: 0x00025A50
		private static int strncmp(string s, int start_s, string t, int start_t, int len)
		{
			int num = start_s;
			int num2 = start_t;
			int i = 0;
			while (i < len)
			{
				if (num < s.Length && num2 < t.Length)
				{
					if (s.get_Chars(num) != t.get_Chars(num2))
					{
						if (s.get_Chars(num) <= t.get_Chars(num2))
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
						num++;
						num2++;
					}
				}
				else
				{
					if (start_s + len < s.Length)
					{
						return 1;
					}
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000278BD File Offset: 0x00025ABD
		public IEnumerable<Trie.MatchResult> Items
		{
			get
			{
				Trie.MatchResult result = new Trie.MatchResult(this.MaxStringLength);
				Stack<int> stack = new Stack<int>();
				int p = this.Root;
				while (!this.TrieNodeIsNull(p))
				{
					result.Word[stack.Count] = this.TrieNodeKey(p);
					if (this.TrieNodeIsLeaf(p))
					{
						result.Length = stack.Count;
						if (this.TrieNodeIsCompressedPath(p))
						{
							for (int i = 0; i < this.TrieNodeCompressedPath(p).Length; i++)
							{
								char[] word = result.Word;
								Trie.MatchResult matchResult = result;
								int length = matchResult.Length;
								matchResult.Length = length + 1;
								word[length] = this.TrieNodeCompressedPath(p).get_Chars(i);
							}
						}
						if (result.Length > 0)
						{
							yield return result;
						}
					}
					if (!this.TrieNodeIsNull(this.TrieNodeChild(p)))
					{
						stack.Push(p);
						p = this.TrieNodeChild(p);
					}
					else if (!this.TrieNodeIsNull(this.TrieNodeSibling(p)))
					{
						p = this.TrieNodeSibling(p);
					}
					else
					{
						if (stack.Count <= 0)
						{
							break;
						}
						do
						{
							p = stack.Pop();
							p = this.TrieNodeSibling(p);
						}
						while (this.TrieNodeIsNull(p) && stack.Count > 0);
					}
				}
				yield break;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000278D0 File Offset: 0x00025AD0
		private int TrieSearchPath(string s, ref int len, int comp)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this.Level0Nodes[(int)s.get_Chars(0)];
			if (len <= 0 || this.TrieNodeIsNull(num3))
			{
				return 0;
			}
			if (len >= 2 && !this.TrieNodeIsNull(num3))
			{
				num3 = this.TrieNodeChild(num3);
				num = 1;
			}
			while (!this.TrieNodeIsNull(num3) && num < len)
			{
				if (this.TrieNodeIsCompressedPath(num3))
				{
					if (comp == 0)
					{
						len = num;
						return num3;
					}
					string text = this.TrieNodeCompressedPath(num3);
					int num4 = num;
					while (num < len && text.get_Chars(num - num4) != '\0' && s.get_Chars(num) == text.get_Chars(num - num4))
					{
						num++;
					}
					len = num;
					return num3;
				}
				else
				{
					while (!this.TrieNodeIsNull(num3) && this.TrieNodeKey(num3) < s.get_Chars(num))
					{
						if (this.TrieNodeIsNull(this.TrieNodeSibling(num3)))
						{
							len = num;
							if (num > 0)
							{
								return num2;
							}
							return 0;
						}
						else
						{
							num3 = this.TrieNodeSibling(num3);
						}
					}
					if (!this.TrieNodeIsNull(num3) && this.TrieNodeKey(num3) == s.get_Chars(num))
					{
						num++;
						if (num == len)
						{
							return num3;
						}
						num2 = num3;
						num3 = this.TrieNodeChild(num3);
					}
					else
					{
						len = num;
						if (num > 0)
						{
							return num2;
						}
						return 0;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000279FC File Offset: 0x00025BFC
		private int SubtrieSearchPath(int root, string s, ref int len, int comp)
		{
			int num = 0;
			int num2 = root;
			int num3 = 0;
			while (!this.TrieNodeIsNull(num2) && num < len)
			{
				if (this.TrieNodeIsCompressedPath(num2))
				{
					if (comp == 0)
					{
						len = num;
						return num2;
					}
					string text = this.TrieNodeCompressedPath(num2);
					int num4 = num;
					while (num < len && text.get_Chars(num - num4) != '\0' && s.get_Chars(num) == text.get_Chars(num - num4))
					{
						num++;
					}
					len = num;
					return num2;
				}
				else
				{
					while (!this.TrieNodeIsNull(num2) && this.TrieNodeKey(num2) < s.get_Chars(num))
					{
						if (this.TrieNodeIsNull(this.TrieNodeSibling(num2)))
						{
							len = num;
							if (num > 0)
							{
								return num3;
							}
							return 0;
						}
						else
						{
							num2 = this.TrieNodeSibling(num2);
						}
					}
					if (!this.TrieNodeIsNull(num2) && this.TrieNodeKey(num2) == s.get_Chars(num))
					{
						num++;
						if (num == len)
						{
							return num2;
						}
						num3 = num2;
						num2 = this.TrieNodeChild(num2);
					}
					else
					{
						len = num;
						if (num > 0)
						{
							return num3;
						}
						return 0;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00027AEC File Offset: 0x00025CEC
		private int triesubdelsearch(int root, string s, int k, int len)
		{
			int num = 0;
			int num2 = root;
			while (!this.TrieNodeIsNull(num2) && num < len)
			{
				if (num == k)
				{
					num++;
				}
				else if (this.TrieNodeIsCompressedPath(num2))
				{
					string text = this.TrieNodeCompressedPath(num2);
					if (num > k)
					{
						if (num + text.Length == len && Trie.strncmp(s, num, text, 0, len - num) != 0)
						{
							return num2;
						}
						return 0;
					}
					else
					{
						if (num + text.Length != len - 1)
						{
							return 0;
						}
						if (num == k)
						{
							if (Trie.strncmp(s, num, text, 1, len - num - 1) != 0)
							{
								return num2;
							}
							return 0;
						}
						else
						{
							int num3 = 0;
							while (num < len && s.get_Chars(num) == text.get_Chars(num3))
							{
								if (num == k - 1)
								{
									num += 2;
								}
								else
								{
									num++;
								}
								num3++;
							}
							if (num >= len)
							{
								return num2;
							}
							return 0;
						}
					}
				}
				else
				{
					while (!this.TrieNodeIsNull(num2) && this.TrieNodeKey(num2) < s.get_Chars(num))
					{
						num2 = this.TrieNodeSibling(num2);
					}
					if (this.TrieNodeIsNull(num2) || this.TrieNodeKey(num2) != s.get_Chars(num))
					{
						return 0;
					}
					num++;
					if (num >= len)
					{
						if (!this.TrieNodeIsNull(this.TrieNodeChild(num2)) && this.TrieNodeIsLeaf(this.TrieNodeChild(num2)) && this.TrieNodeIsNull(this.TrieNodeChild(this.TrieNodeChild(num2))))
						{
							return this.TrieNodeChild(num2);
						}
						return 0;
					}
					else
					{
						num2 = this.TrieNodeChild(num2);
					}
				}
			}
			return 0;
		}

		// Token: 0x04000164 RID: 356
		public const int DefaultMaxStringLength = 1;

		// Token: 0x04000165 RID: 357
		private const int PS_ALPHABETSIZE = 65536;

		// Token: 0x04000166 RID: 358
		private int Root;

		// Token: 0x04000167 RID: 359
		private int[] Level0Nodes = new int[65536];

		// Token: 0x0400016A RID: 362
		private int MaxStringLength = 1;

		// Token: 0x04000171 RID: 369
		private CharCostLookup m_charCosts = new CharCostLookup();

		// Token: 0x04000172 RID: 370
		private int m_minCharCost = 8;

		// Token: 0x04000173 RID: 371
		private int m_maxDelToTransposeCostDelta = 2;

		// Token: 0x04000174 RID: 372
		private Trie.MatchContext m_matchContext = new Trie.MatchContext(1);

		// Token: 0x04000175 RID: 373
		private Trie.TrieNode[] m_trieNodes;

		// Token: 0x04000176 RID: 374
		private Trie.DFContext m_dfContext = new Trie.DFContext();

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x06000A1D RID: 2589
		private delegate void TrieNodeFunction(int n);

		// Token: 0x02000139 RID: 313
		internal enum OperationType2
		{
			// Token: 0x04000324 RID: 804
			Equality,
			// Token: 0x04000325 RID: 805
			Insertion,
			// Token: 0x04000326 RID: 806
			Deletion,
			// Token: 0x04000327 RID: 807
			Transposition = 4,
			// Token: 0x04000328 RID: 808
			Substitution
		}

		// Token: 0x0200013A RID: 314
		[DebuggerDisplay("Cost={Cost} From={From} To={To}")]
		[Serializable]
		internal struct LevCell
		{
			// Token: 0x06000A20 RID: 2592 RVA: 0x0002E9EF File Offset: 0x0002CBEF
			public void SetFromToSpan(char from, char to, short span)
			{
				this.From = from;
				this.To = to;
				this.Span = span;
			}

			// Token: 0x04000329 RID: 809
			public int Cost;

			// Token: 0x0400032A RID: 810
			public char From;

			// Token: 0x0400032B RID: 811
			public char To;

			// Token: 0x0400032C RID: 812
			public short Span;
		}

		// Token: 0x0200013B RID: 315
		[Serializable]
		internal class MatchResultZoneAllocator
		{
			// Token: 0x06000A21 RID: 2593 RVA: 0x0002EA06 File Offset: 0x0002CC06
			public MatchResultZoneAllocator(int maxStringLength)
			{
				this.m_maxStringLength = maxStringLength;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002EA20 File Offset: 0x0002CC20
			// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002EA2D File Offset: 0x0002CC2D
			public int Capacity
			{
				get
				{
					return this.m_items.Count;
				}
				set
				{
					while (this.m_items.Count < value)
					{
						this.m_items.Add(new Trie.MatchResult(this.m_maxStringLength));
					}
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002EA55 File Offset: 0x0002CC55
			public int Count
			{
				get
				{
					return this.m_nextIndex;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002EA5D File Offset: 0x0002CC5D
			public List<Trie.MatchResult> RawItems
			{
				get
				{
					return this.m_items;
				}
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x0002EA68 File Offset: 0x0002CC68
			public Trie.MatchResult New()
			{
				if (this.m_nextIndex == this.m_items.Count)
				{
					this.m_items.Add(new Trie.MatchResult(this.m_maxStringLength));
				}
				else if (this.m_items[this.m_nextIndex] is IReset)
				{
					(this.m_items[this.m_nextIndex] as IReset).Reset();
				}
				List<Trie.MatchResult> items = this.m_items;
				int nextIndex = this.m_nextIndex;
				this.m_nextIndex = nextIndex + 1;
				return items[nextIndex];
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
			public Trie.MatchResult NewNoReset()
			{
				if (this.m_nextIndex == this.m_items.Count)
				{
					this.m_items.Add(new Trie.MatchResult(this.m_maxStringLength));
				}
				List<Trie.MatchResult> items = this.m_items;
				int nextIndex = this.m_nextIndex;
				this.m_nextIndex = nextIndex + 1;
				return items[nextIndex];
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002EB42 File Offset: 0x0002CD42
			public long MemoryUsage
			{
				get
				{
					return (long)(this.m_items.Capacity * 4);
				}
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x0002EB52 File Offset: 0x0002CD52
			public void Reset()
			{
				this.m_nextIndex = 0;
			}

			// Token: 0x0400032D RID: 813
			private int m_maxStringLength;

			// Token: 0x0400032E RID: 814
			private int m_nextIndex;

			// Token: 0x0400032F RID: 815
			private List<Trie.MatchResult> m_items = new List<Trie.MatchResult>();
		}

		// Token: 0x0200013C RID: 316
		[Serializable]
		public class MatchContext : IEnumerable<Trie.MatchResult>, IEnumerable, IEnumerator<Trie.MatchResult>, IDisposable, IEnumerator
		{
			// Token: 0x06000A2A RID: 2602 RVA: 0x0002EB5C File Offset: 0x0002CD5C
			public MatchContext(int maxStringLength)
			{
				maxStringLength += 2;
				this.Matrix.Resize(maxStringLength, maxStringLength);
				this.Operations = new Trie.OperationType2[maxStringLength];
				this.m_matchResults = new Trie.MatchResultZoneAllocator(maxStringLength);
				this.aux_word = new char[maxStringLength];
				this.Threshold = 0.0;
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
			internal void Resize(int inputStringLength, int maxTrieStringLength)
			{
				int num = Math.Max(maxTrieStringLength + 2, this.Matrix.Width);
				int num2 = Math.Max(Math.Max(inputStringLength + 1, this.Matrix.Height), num);
				if (num2 > this.Operations.Length)
				{
					this.Matrix.Resize(num2, num2);
					this.Operations = new Trie.OperationType2[num2];
					this.aux_word = new char[num2];
					this.m_matchResults = new Trie.MatchResultZoneAllocator(num2);
				}
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0002EC38 File Offset: 0x0002CE38
			public void Reset()
			{
				this.m_matchResults.Reset();
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0002EC45 File Offset: 0x0002CE45
			internal void AddMatch(char[] prefix, int prefixLen, int cost, int value)
			{
				Trie.MatchResult matchResult = this.m_matchResults.New();
				Trie.strncpy(matchResult.Word, 0, prefix, 0, prefixLen);
				matchResult.Length = prefixLen;
				matchResult.Distance = cost;
				matchResult.Value = value;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0002EC78 File Offset: 0x0002CE78
			internal void AddMatch<T>(char[] prefix, int prefixLen, T suffix, int cost, int value) where T : IString
			{
				Trie.MatchResult matchResult = this.m_matchResults.New();
				Trie.strncpy(matchResult.Word, 0, prefix, 0, prefixLen);
				Trie.strncpy<T>(matchResult.Word, prefixLen, suffix, 0, suffix.Length);
				matchResult.Length = prefixLen + suffix.Length;
				matchResult.Distance = cost;
				matchResult.Value = value;
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0002ECE0 File Offset: 0x0002CEE0
			internal void AddMatch<T>(char[] prefix, int prefixLen, T suffix, int suffix_start, int cost, int value) where T : IString
			{
				Trie.MatchResult matchResult = this.m_matchResults.New();
				Trie.strncpy(matchResult.Word, 0, prefix, 0, prefixLen);
				Trie.strncpy<T>(matchResult.Word, prefixLen, suffix, suffix_start, suffix.Length - suffix_start);
				matchResult.Length = prefixLen + suffix.Length - suffix_start;
				matchResult.Distance = cost;
				matchResult.Value = value;
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0002ED4D File Offset: 0x0002CF4D
			IEnumerator IEnumerable.GetEnumerator()
			{
				this.Reset();
				return this;
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x0002ED56 File Offset: 0x0002CF56
			public IEnumerator<Trie.MatchResult> GetEnumerator()
			{
				this.Reset();
				return this;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x0002ED5F File Offset: 0x0002CF5F
			bool IEnumerator.MoveNext()
			{
				if (this.m_currentMatchResult + 1 < this.m_matchResults.Count)
				{
					this.m_currentMatchResult++;
					return true;
				}
				return false;
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002ED87 File Offset: 0x0002CF87
			public Trie.MatchResult Current
			{
				get
				{
					return this.m_matchResults.RawItems[this.m_currentMatchResult];
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002ED9F File Offset: 0x0002CF9F
			object IEnumerator.Current
			{
				get
				{
					return this.m_matchResults.RawItems[this.m_currentMatchResult];
				}
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0002EDB7 File Offset: 0x0002CFB7
			void IEnumerator.Reset()
			{
				this.m_currentMatchResult = -1;
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
			void IDisposable.Dispose()
			{
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002EDC2 File Offset: 0x0002CFC2
			// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002EDCA File Offset: 0x0002CFCA
			internal int MinDistance
			{
				get
				{
					return this.m_minDistance;
				}
				set
				{
					this.m_minDistance = value;
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002EDD3 File Offset: 0x0002CFD3
			// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002EDDB File Offset: 0x0002CFDB
			internal int MaxDistance
			{
				get
				{
					return this.m_maxDistance;
				}
				set
				{
					this.m_maxDistance = value;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002EDE4 File Offset: 0x0002CFE4
			// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002EDEC File Offset: 0x0002CFEC
			public double Threshold { get; set; }

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002EDF5 File Offset: 0x0002CFF5
			// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0002EDFD File Offset: 0x0002CFFD
			internal bool AllowSpaces
			{
				get
				{
					return this.m_allowSpaces;
				}
				set
				{
					this.m_allowSpaces = value;
				}
			}

			// Token: 0x04000330 RID: 816
			private int m_minDistance;

			// Token: 0x04000331 RID: 817
			private int m_maxDistance;

			// Token: 0x04000332 RID: 818
			private bool m_allowSpaces;

			// Token: 0x04000333 RID: 819
			internal Matrix<Trie.LevCell> Matrix = new Matrix<Trie.LevCell>();

			// Token: 0x04000334 RID: 820
			internal Trie.OperationType2[] Operations;

			// Token: 0x04000335 RID: 821
			internal char[] aux_word;

			// Token: 0x04000336 RID: 822
			internal Trie.MatchResultZoneAllocator m_matchResults;

			// Token: 0x04000337 RID: 823
			private int m_currentMatchResult;
		}

		// Token: 0x0200013D RID: 317
		[Serializable]
		private struct TrieNode
		{
			// Token: 0x06000A3F RID: 2623 RVA: 0x0002EE06 File Offset: 0x0002D006
			public TrieNode(char key, int sibling)
			{
				this.Key = key;
				this.Value = 0;
				this.Sibling = sibling;
				this.Child = 0;
			}

			// Token: 0x06000A40 RID: 2624 RVA: 0x0002EE24 File Offset: 0x0002D024
			public TrieNode(char key, int sibling, int child)
			{
				this.Key = key;
				this.Value = 0;
				this.Sibling = sibling;
				this.Child = child;
			}

			// Token: 0x06000A41 RID: 2625 RVA: 0x0002EE42 File Offset: 0x0002D042
			public TrieNode(char key)
			{
				this.Key = key;
				this.Value = 0;
				this.Sibling = 0;
				this.Child = 0;
			}

			// Token: 0x06000A42 RID: 2626 RVA: 0x0002EE60 File Offset: 0x0002D060
			public void Clear()
			{
				this.Key = '\0';
				this.Value = 0;
				this.Sibling = 0;
				this.Child = 0;
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002EE7E File Offset: 0x0002D07E
			public bool IsCompressedPath
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000339 RID: 825
			public char Key;

			// Token: 0x0400033A RID: 826
			public int Value;

			// Token: 0x0400033B RID: 827
			public int Sibling;

			// Token: 0x0400033C RID: 828
			public int Child;
		}

		// Token: 0x0200013E RID: 318
		[Serializable]
		public class MatchResult
		{
			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002EE81 File Offset: 0x0002D081
			// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0002EE89 File Offset: 0x0002D089
			public int Value { get; internal set; }

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002EE92 File Offset: 0x0002D092
			// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002EE9A File Offset: 0x0002D09A
			public int Distance { get; internal set; }

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002EEA3 File Offset: 0x0002D0A3
			// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0002EEAB File Offset: 0x0002D0AB
			public char[] Word { get; private set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002EEB4 File Offset: 0x0002D0B4
			// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0002EEBC File Offset: 0x0002D0BC
			public int Length { get; internal set; }

			// Token: 0x06000A4C RID: 2636 RVA: 0x0002EEC5 File Offset: 0x0002D0C5
			public MatchResult(int maxStringLength)
			{
				this.Word = new char[maxStringLength + 1];
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x0002EEDB File Offset: 0x0002D0DB
			public override string ToString()
			{
				return new string(this.Word, 0, this.Length);
			}
		}

		// Token: 0x0200013F RID: 319
		[Serializable]
		private class Token
		{
			// Token: 0x04000341 RID: 833
			public int Position;

			// Token: 0x04000342 RID: 834
			public int len;

			// Token: 0x04000343 RID: 835
			public int lexvalue;

			// Token: 0x04000344 RID: 836
			public int stopvalue;

			// Token: 0x04000345 RID: 837
			public int levstart;

			// Token: 0x04000346 RID: 838
			public int levend;

			// Token: 0x04000347 RID: 839
			public short levdist;
		}

		// Token: 0x02000140 RID: 320
		[Serializable]
		private struct StringWithReplacedChar<T> : IString where T : IString
		{
			// Token: 0x06000A4F RID: 2639 RVA: 0x0002EEF7 File Offset: 0x0002D0F7
			public StringWithReplacedChar(T baseString, int baseStart, int positionToReplace, char charReplacement)
			{
				this.m_str = baseString;
				this.m_baseStart = baseStart;
				this.m_replacementIndex = positionToReplace;
				this.m_charReplacement = charReplacement;
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002EF18 File Offset: 0x0002D118
			public int Length
			{
				get
				{
					T str = this.m_str;
					return str.Length - this.m_baseStart;
				}
			}

			// Token: 0x170001B4 RID: 436
			public char this[int i]
			{
				get
				{
					if (i != this.m_replacementIndex)
					{
						T str = this.m_str;
						return str[this.m_baseStart + i];
					}
					return this.m_charReplacement;
				}
			}

			// Token: 0x04000348 RID: 840
			private readonly T m_str;

			// Token: 0x04000349 RID: 841
			private readonly int m_baseStart;

			// Token: 0x0400034A RID: 842
			private readonly int m_replacementIndex;

			// Token: 0x0400034B RID: 843
			private readonly char m_charReplacement;
		}

		// Token: 0x02000141 RID: 321
		public class DFContext
		{
			// Token: 0x06000A52 RID: 2642 RVA: 0x0002EF79 File Offset: 0x0002D179
			private void SetT(int i, int j, int val)
			{
				this.T[i][j] = val;
			}

			// Token: 0x06000A53 RID: 2643 RVA: 0x0002EF8A File Offset: 0x0002D18A
			private int GetT(int i, int j)
			{
				if (i == -1 || j == -1)
				{
					return this.Infinity;
				}
				return this.T[i][j];
			}

			// Token: 0x06000A54 RID: 2644 RVA: 0x0002EFAC File Offset: 0x0002D1AC
			public DFContext()
			{
				this.T.Resize(Trie.DFContext.DFMaxStringLength + 2, Trie.DFContext.DFMaxStringLength + 2);
				this.W[0] = '\0';
				for (int i = 0; i < Trie.DFContext.DFMaxStringLength + 1; i++)
				{
					this.T[i][0] = (this.T[0][i] = i);
				}
			}

			// Token: 0x06000A55 RID: 2645 RVA: 0x0002F079 File Offset: 0x0002D279
			public void SetMaxErrors(int maxAllowedErrors)
			{
				this.k = maxAllowedErrors;
				this.C[0] = (short)maxAllowedErrors;
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x0002F08C File Offset: 0x0002D28C
			public void SetPattern(string pattern)
			{
				this.P[0] = '\0';
				Array.Copy(pattern.ToCharArray(), 0, this.P, 1, pattern.Length);
				this.pLength = pattern.Length;
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x0002F0BC File Offset: 0x0002D2BC
			public void Reset()
			{
				this.m_matchResults.Reset();
				this.eDistance = 0;
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
			public void AddResult(char[] word, int length, int distance, int value)
			{
				if (distance <= this.k)
				{
					Trie.MatchResult matchResult = this.m_matchResults.New();
					matchResult.Distance = distance * 10;
					matchResult.Length = length - 1;
					matchResult.Value = value;
					Array.Copy(word, 1, matchResult.Word, 0, length - 1);
				}
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x0002F11E File Offset: 0x0002D31E
			public IEnumerable<Trie.MatchResult> GetMatchResultsEnumerable()
			{
				int num;
				for (int i = 0; i < this.m_matchResults.Count; i = num + 1)
				{
					yield return this.m_matchResults.RawItems[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x0002F130 File Offset: 0x0002D330
			public int EditDistance(int j)
			{
				this.C[j] = 0;
				int i = 1;
				Math.Min((int)(this.C[j - 1] + 1), this.pLength);
				while (i <= this.pLength)
				{
					int num = ((this.P[i] == this.W[j]) ? 0 : 1);
					int num2 = ((this.P[i - 1] == this.W[j] && this.P[i] == this.W[j - 1]) ? 1 : this.Infinity);
					int num3 = this.Min(this.GetT(i, j - 1) + 1, this.GetT(i - 1, j) + 1, this.GetT(i - 1, j - 1) + num, this.GetT(i - 2, j - 2) + num2);
					this.SetT(i, j, num3);
					short num4 = ((this.GetT(i, j) <= this.k) ? ((short)i) : this.C[j]);
					this.C[j] = num4;
					i++;
				}
				int t = this.GetT(i - 1, j);
				this.eDistance = ((this.C[j] == 0) ? this.Infinity : t);
				return this.eDistance;
			}

			// Token: 0x06000A5B RID: 2651 RVA: 0x0002F256 File Offset: 0x0002D456
			private int Min(int w, int x, int y, int z)
			{
				return Math.Min(Math.Min(Math.Min(w, x), y), z);
			}

			// Token: 0x0400034C RID: 844
			private static int DFMaxStringLength = 255;

			// Token: 0x0400034D RID: 845
			private char[] P = new char[Trie.DFContext.DFMaxStringLength + 2];

			// Token: 0x0400034E RID: 846
			private Matrix<int> T = new Matrix<int>();

			// Token: 0x0400034F RID: 847
			private short[] C = new short[Trie.DFContext.DFMaxStringLength + 2];

			// Token: 0x04000350 RID: 848
			public char[] W = new char[Trie.DFContext.DFMaxStringLength + 2];

			// Token: 0x04000351 RID: 849
			public int k = 1;

			// Token: 0x04000352 RID: 850
			private int pLength;

			// Token: 0x04000353 RID: 851
			internal Trie.MatchResultZoneAllocator m_matchResults = new Trie.MatchResultZoneAllocator(Trie.DFContext.DFMaxStringLength + 2);

			// Token: 0x04000354 RID: 852
			public int Infinity = 5000;

			// Token: 0x04000355 RID: 853
			public int eDistance;
		}

		// Token: 0x02000142 RID: 322
		[Serializable]
		private class MassHandler
		{
			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002F278 File Offset: 0x0002D478
			// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0002F280 File Offset: 0x0002D480
			public int TotalMass
			{
				get
				{
					return this.m_mass;
				}
				set
				{
					this.m_mass = value;
				}
			}

			// Token: 0x06000A5F RID: 2655 RVA: 0x0002F289 File Offset: 0x0002D489
			public void AccumulateMass(int p)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000356 RID: 854
			private int m_mass;
		}

		// Token: 0x02000143 RID: 323
		[Serializable]
		private class ChangeMassHandler
		{
			// Token: 0x06000A61 RID: 2657 RVA: 0x0002F298 File Offset: 0x0002D498
			public ChangeMassHandler(int minMass, int maxMass)
			{
				this.m_minMass = minMass;
				this.m_maxMass = maxMass;
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x0002F2AE File Offset: 0x0002D4AE
			public void ChangeMass(int p)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000357 RID: 855
			private int m_minMass;

			// Token: 0x04000358 RID: 856
			private int m_maxMass;
		}

		// Token: 0x02000144 RID: 324
		[Serializable]
		private class NodePathHandler
		{
			// Token: 0x06000A63 RID: 2659 RVA: 0x0002F2B5 File Offset: 0x0002D4B5
			public NodePathHandler(Trie.TrieNodeFunction f)
			{
				this.m_f = f;
			}

			// Token: 0x06000A64 RID: 2660 RVA: 0x0002F2D4 File Offset: 0x0002D4D4
			public void Process(int n)
			{
				this.m_f(n);
			}

			// Token: 0x04000359 RID: 857
			internal char[] aux_triepath = new char[1024];

			// Token: 0x0400035A RID: 858
			internal int aux_triepathlength;

			// Token: 0x0400035B RID: 859
			private Trie.TrieNodeFunction m_f;
		}
	}
}
