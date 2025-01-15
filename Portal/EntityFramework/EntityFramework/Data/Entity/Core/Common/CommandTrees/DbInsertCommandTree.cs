using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C3 RID: 1731
	public sealed class DbInsertCommandTree : DbModificationCommandTree
	{
		// Token: 0x060050DE RID: 20702 RVA: 0x00122096 File Offset: 0x00120296
		internal DbInsertCommandTree()
		{
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0012209E File Offset: 0x0012029E
		public DbInsertCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning)
			: base(metadata, dataSpace, target)
		{
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x001220B9 File Offset: 0x001202B9
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x001220C1 File Offset: 0x001202C1
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x001220C9 File Offset: 0x001202C9
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Insert;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060050E3 RID: 20707 RVA: 0x001220CC File Offset: 0x001202CC
		internal override bool HasReader
		{
			get
			{
				return this.Returning != null;
			}
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x001220D8 File Offset: 0x001202D8
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			dumper.Begin("SetClauses");
			foreach (DbModificationClause dbModificationClause in this.SetClauses)
			{
				if (dbModificationClause != null)
				{
					dbModificationClause.DumpStructure(dumper);
				}
			}
			dumper.End("SetClauses");
			if (this.Returning != null)
			{
				dumper.Dump(this.Returning, "Returning");
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00122160 File Offset: 0x00120360
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04001D9F RID: 7583
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;

		// Token: 0x04001DA0 RID: 7584
		private readonly DbExpression _returning;
	}
}
