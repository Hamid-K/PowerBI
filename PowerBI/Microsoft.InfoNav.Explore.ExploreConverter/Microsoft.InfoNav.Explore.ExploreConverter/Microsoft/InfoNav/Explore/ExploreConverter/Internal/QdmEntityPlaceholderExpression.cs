using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000079 RID: 121
	internal sealed class QdmEntityPlaceholderExpression : IRdmQueryExpression
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000BD76 File Offset: 0x00009F76
		internal QdmEntityPlaceholderExpression(string target)
		{
			this._target = target;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000BD85 File Offset: 0x00009F85
		internal string Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BD8D File Offset: 0x00009F8D
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.EntityName = this._target;
		}

		// Token: 0x04000193 RID: 403
		private readonly string _target;
	}
}
