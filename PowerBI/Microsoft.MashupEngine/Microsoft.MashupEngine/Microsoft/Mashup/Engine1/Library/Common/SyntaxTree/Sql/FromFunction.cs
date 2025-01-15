using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D1 RID: 4561
	internal sealed class FromFunction : FromItem
	{
		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x0600786F RID: 30831 RVA: 0x001A13D4 File Offset: 0x0019F5D4
		// (set) Token: 0x06007870 RID: 30832 RVA: 0x001A13DC File Offset: 0x0019F5DC
		public StoredFunctionReference Function { get; set; }

		// Token: 0x06007871 RID: 30833 RVA: 0x001A13E5 File Offset: 0x0019F5E5
		public override FromItem ShallowCopy()
		{
			return new FromFunction
			{
				Alias = base.Alias,
				Function = this.Function,
				RotationClause = base.RotationClause
			};
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x001A1410 File Offset: 0x0019F610
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.Function.WriteCreateScript(writer);
			base.WriteRotationClause(writer);
			if (base.Alias != null)
			{
				writer.WriteFromAlias(base.Alias);
				writer.WriteSpace();
			}
		}
	}
}
