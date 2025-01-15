using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010D6 RID: 4310
	internal class TableNodesMatch
	{
		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06008163 RID: 33123 RVA: 0x001B12ED File Offset: 0x001AF4ED
		public Dictionary<int, Dictionary<int, IDomNode>> ColumnNodes { get; }

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06008164 RID: 33124 RVA: 0x001B12F5 File Offset: 0x001AF4F5
		public Dictionary<int, IDomNode> RowNodes { get; }

		// Token: 0x06008165 RID: 33125 RVA: 0x001B12FD File Offset: 0x001AF4FD
		public TableNodesMatch(Dictionary<int, Dictionary<int, IDomNode>> columnNodes, Dictionary<int, IDomNode> rowNodes)
		{
			this.ColumnNodes = columnNodes;
			this.RowNodes = rowNodes;
		}

		// Token: 0x06008166 RID: 33126 RVA: 0x001B1314 File Offset: 0x001AF514
		public bool Equals(TableNodesMatch m)
		{
			if (m == null)
			{
				return false;
			}
			if (this.RowNodes.Count != m.RowNodes.Count)
			{
				return false;
			}
			foreach (KeyValuePair<int, IDomNode> keyValuePair in this.RowNodes)
			{
				if (!m.RowNodes.ContainsKey(keyValuePair.Key) || m.RowNodes[keyValuePair.Key] != keyValuePair.Value)
				{
					return false;
				}
			}
			if (this.ColumnNodes.Count != m.ColumnNodes.Count)
			{
				return false;
			}
			foreach (KeyValuePair<int, Dictionary<int, IDomNode>> keyValuePair2 in this.ColumnNodes)
			{
				if (!m.ColumnNodes.ContainsKey(keyValuePair2.Key))
				{
					return false;
				}
				Dictionary<int, IDomNode> value = keyValuePair2.Value;
				Dictionary<int, IDomNode> dictionary = m.ColumnNodes[keyValuePair2.Key];
				if (value.Count != dictionary.Count)
				{
					return false;
				}
				foreach (KeyValuePair<int, IDomNode> keyValuePair3 in value)
				{
					if (!dictionary.ContainsKey(keyValuePair3.Key) || dictionary[keyValuePair3.Key] != keyValuePair3.Value)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x001B14C4 File Offset: 0x001AF6C4
		public override bool Equals(object obj)
		{
			TableNodesMatch tableNodesMatch = obj as TableNodesMatch;
			return tableNodesMatch != null && this.Equals(tableNodesMatch);
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x001B14E4 File Offset: 0x001AF6E4
		public override int GetHashCode()
		{
			int num = 17;
			foreach (KeyValuePair<int, IDomNode> keyValuePair in this.RowNodes)
			{
				num = num * 23 + keyValuePair.Key;
				if (keyValuePair.Value != null)
				{
					num = num * 23 + keyValuePair.Value.GetHashCode();
				}
			}
			foreach (KeyValuePair<int, Dictionary<int, IDomNode>> keyValuePair2 in this.ColumnNodes)
			{
				num = num * 23 + keyValuePair2.Key;
				foreach (KeyValuePair<int, IDomNode> keyValuePair3 in keyValuePair2.Value)
				{
					num = num * 23 + keyValuePair3.Key;
					if (keyValuePair3.Value != null)
					{
						num = num * 23 + keyValuePair3.Value.GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06008169 RID: 33129 RVA: 0x001B160C File Offset: 0x001AF80C
		public static bool operator ==(TableNodesMatch a, TableNodesMatch b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x0600816A RID: 33130 RVA: 0x001B1623 File Offset: 0x001AF823
		public static bool operator !=(TableNodesMatch a, TableNodesMatch b)
		{
			return !(a == b);
		}
	}
}
