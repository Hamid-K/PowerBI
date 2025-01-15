using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E5 RID: 1765
	public sealed class DbUpdateCommandTree : DbModificationCommandTree
	{
		// Token: 0x06005194 RID: 20884 RVA: 0x00123C2F File Offset: 0x00121E2F
		internal DbUpdateCommandTree()
		{
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x00123C37 File Offset: 0x00121E37
		public DbUpdateCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning)
			: base(metadata, dataSpace, target)
		{
			this._predicate = predicate;
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x00123C5A File Offset: 0x00121E5A
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x00123C62 File Offset: 0x00121E62
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x00123C6A File Offset: 0x00121E6A
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x00123C72 File Offset: 0x00121E72
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Update;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x0600519A RID: 20890 RVA: 0x00123C75 File Offset: 0x00121E75
		internal override bool HasReader
		{
			get
			{
				return this.Returning != null;
			}
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00123C80 File Offset: 0x00121E80
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			if (this.Predicate != null)
			{
				dumper.Dump(this.Predicate, "Predicate");
			}
			dumper.Begin("SetClauses", null);
			foreach (DbModificationClause dbModificationClause in this.SetClauses)
			{
				if (dbModificationClause != null)
				{
					dbModificationClause.DumpStructure(dumper);
				}
			}
			dumper.End("SetClauses");
			dumper.Dump(this.Returning, "Returning");
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x00123D18 File Offset: 0x00121F18
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04001DD1 RID: 7633
		private readonly DbExpression _predicate;

		// Token: 0x04001DD2 RID: 7634
		private readonly DbExpression _returning;

		// Token: 0x04001DD3 RID: 7635
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;
	}
}
