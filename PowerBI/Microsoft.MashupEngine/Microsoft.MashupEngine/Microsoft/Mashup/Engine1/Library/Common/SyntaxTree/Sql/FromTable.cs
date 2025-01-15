using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D3 RID: 4563
	internal sealed class FromTable : FromItem
	{
		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x001A14A9 File Offset: 0x0019F6A9
		// (set) Token: 0x0600787A RID: 30842 RVA: 0x001A14B1 File Offset: 0x0019F6B1
		public TableReference Table { get; set; }

		// Token: 0x0600787B RID: 30843 RVA: 0x001A14BA File Offset: 0x0019F6BA
		public override FromItem ShallowCopy()
		{
			return new FromTable
			{
				Alias = base.Alias,
				Table = this.Table,
				RotationClause = base.RotationClause
			};
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x001A14E5 File Offset: 0x0019F6E5
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.Table.WriteCreateScript(writer);
			base.WriteRotationClause(writer);
			if (base.Alias != null)
			{
				writer.WriteFromAlias(base.Alias);
			}
		}
	}
}
