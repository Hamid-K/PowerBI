using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D0 RID: 4560
	internal abstract class FromItem : IScriptable
	{
		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06007867 RID: 30823 RVA: 0x001A139C File Offset: 0x0019F59C
		// (set) Token: 0x06007868 RID: 30824 RVA: 0x001A13A4 File Offset: 0x0019F5A4
		public Alias Alias { get; set; }

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06007869 RID: 30825 RVA: 0x001A13AD File Offset: 0x0019F5AD
		// (set) Token: 0x0600786A RID: 30826 RVA: 0x001A13B5 File Offset: 0x0019F5B5
		public RotationClause RotationClause { get; set; }

		// Token: 0x0600786B RID: 30827 RVA: 0x001A13BE File Offset: 0x0019F5BE
		protected void WriteRotationClause(ScriptWriter writer)
		{
			if (this.RotationClause != null)
			{
				this.RotationClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x0600786C RID: 30828
		public abstract FromItem ShallowCopy();

		// Token: 0x0600786D RID: 30829
		public abstract void WriteCreateScript(ScriptWriter writer);
	}
}
