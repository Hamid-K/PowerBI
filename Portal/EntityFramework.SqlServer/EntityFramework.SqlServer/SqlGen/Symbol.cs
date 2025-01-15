using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003C RID: 60
	internal class Symbol : ISqlFragment
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00019E41 File Offset: 0x00018041
		internal Dictionary<string, Symbol> Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
				}
				return this.columns;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00019E61 File Offset: 0x00018061
		internal Dictionary<string, Symbol> OutputColumns
		{
			get
			{
				if (this.outputColumns == null)
				{
					this.outputColumns = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
				}
				return this.outputColumns;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00019E81 File Offset: 0x00018081
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00019E89 File Offset: 0x00018089
		internal bool NeedsRenaming { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00019E92 File Offset: 0x00018092
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00019E9A File Offset: 0x0001809A
		internal bool OutputColumnsRenamed { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00019EA3 File Offset: 0x000180A3
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00019EAB File Offset: 0x000180AB
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00019EB3 File Offset: 0x000180B3
		public string NewName { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00019EBC File Offset: 0x000180BC
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00019EC4 File Offset: 0x000180C4
		internal TypeUsage Type { get; set; }

		// Token: 0x060005BB RID: 1467 RVA: 0x00019ECD File Offset: 0x000180CD
		public Symbol(string name, TypeUsage type)
		{
			this.name = name;
			this.NewName = name;
			this.Type = type;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00019EEA File Offset: 0x000180EA
		public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> outputColumns, bool outputColumnsRenamed)
		{
			this.name = name;
			this.NewName = name;
			this.Type = type;
			this.outputColumns = outputColumns;
			this.OutputColumnsRenamed = outputColumnsRenamed;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00019F18 File Offset: 0x00018118
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this.NeedsRenaming)
			{
				int num;
				if (sqlGenerator.AllColumnNames.TryGetValue(this.NewName, out num))
				{
					string text;
					do
					{
						num++;
						text = this.NewName + num.ToString(CultureInfo.InvariantCulture);
					}
					while (sqlGenerator.AllColumnNames.ContainsKey(text));
					sqlGenerator.AllColumnNames[this.NewName] = num;
					this.NewName = text;
				}
				sqlGenerator.AllColumnNames[this.NewName] = 0;
				this.NeedsRenaming = false;
			}
			writer.Write(SqlGenerator.QuoteIdentifier(this.NewName));
		}

		// Token: 0x0400011D RID: 285
		private Dictionary<string, Symbol> columns;

		// Token: 0x0400011E RID: 286
		private Dictionary<string, Symbol> outputColumns;

		// Token: 0x04000121 RID: 289
		private readonly string name;
	}
}
