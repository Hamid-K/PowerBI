using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000030 RID: 48
	internal sealed class JoinSymbol : Symbol
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00011637 File Offset: 0x0000F837
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00011652 File Offset: 0x0000F852
		internal List<Symbol> ColumnList
		{
			get
			{
				if (this.columnList == null)
				{
					this.columnList = new List<Symbol>();
				}
				return this.columnList;
			}
			set
			{
				this.columnList = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001165B File Offset: 0x0000F85B
		internal List<Symbol> ExtentList
		{
			get
			{
				return this.extentList;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00011663 File Offset: 0x0000F863
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0001167E File Offset: 0x0000F87E
		internal List<Symbol> FlattenedExtentList
		{
			get
			{
				if (this.flattenedExtentList == null)
				{
					this.flattenedExtentList = new List<Symbol>();
				}
				return this.flattenedExtentList;
			}
			set
			{
				this.flattenedExtentList = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00011687 File Offset: 0x0000F887
		internal Dictionary<string, Symbol> NameToExtent
		{
			get
			{
				return this.nameToExtent;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001168F File Offset: 0x0000F88F
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00011697 File Offset: 0x0000F897
		internal bool IsNestedJoin { get; set; }

		// Token: 0x0600045E RID: 1118 RVA: 0x000116A0 File Offset: 0x0000F8A0
		public JoinSymbol(string name, TypeUsage type, List<Symbol> extents)
			: base(name, type)
		{
			this.extentList = new List<Symbol>(extents.Count);
			this.nameToExtent = new Dictionary<string, Symbol>(extents.Count, StringComparer.OrdinalIgnoreCase);
			foreach (Symbol symbol in extents)
			{
				this.nameToExtent[symbol.Name] = symbol;
				this.ExtentList.Add(symbol);
			}
		}

		// Token: 0x040000DC RID: 220
		private List<Symbol> columnList;

		// Token: 0x040000DD RID: 221
		private readonly List<Symbol> extentList;

		// Token: 0x040000DE RID: 222
		private List<Symbol> flattenedExtentList;

		// Token: 0x040000DF RID: 223
		private readonly Dictionary<string, Symbol> nameToExtent;
	}
}
