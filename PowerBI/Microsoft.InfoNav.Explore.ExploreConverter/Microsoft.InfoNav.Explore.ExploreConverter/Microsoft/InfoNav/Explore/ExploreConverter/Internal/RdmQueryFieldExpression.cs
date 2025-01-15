using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007D RID: 125
	internal sealed class RdmQueryFieldExpression : IRdmQueryExpression
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000C3B2 File Offset: 0x0000A5B2
		internal RdmQueryFieldExpression(string field, IRdmQueryExpression instance)
		{
			this._field = field;
			this._instance = instance;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		internal string Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		internal IRdmQueryExpression Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.PropertyName = this._field;
			context.EdmReferenceKind = new FormulaEdmReferenceKind?(FormulaEdmReferenceKind.FieldProperty);
			if (this._instance != null)
			{
				this._instance.FindFormulaComponents(context);
			}
		}

		// Token: 0x04000195 RID: 405
		private readonly string _field;

		// Token: 0x04000196 RID: 406
		private readonly IRdmQueryExpression _instance;
	}
}
