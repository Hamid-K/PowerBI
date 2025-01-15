using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006AF RID: 1711
	public sealed class DbDeleteCommandTree : DbModificationCommandTree
	{
		// Token: 0x06005015 RID: 20501 RVA: 0x00121876 File Offset: 0x0011FA76
		internal DbDeleteCommandTree()
		{
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0012187E File Offset: 0x0011FA7E
		public DbDeleteCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate)
			: base(metadata, dataSpace, target)
		{
			this._predicate = predicate;
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x00121891 File Offset: 0x0011FA91
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x00121899 File Offset: 0x0011FA99
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Delete;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x0012189C File Offset: 0x0011FA9C
		internal override bool HasReader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0012189F File Offset: 0x0011FA9F
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			if (this.Predicate != null)
			{
				dumper.Dump(this.Predicate, "Predicate");
			}
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x001218C1 File Offset: 0x0011FAC1
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04001D49 RID: 7497
		private readonly DbExpression _predicate;
	}
}
