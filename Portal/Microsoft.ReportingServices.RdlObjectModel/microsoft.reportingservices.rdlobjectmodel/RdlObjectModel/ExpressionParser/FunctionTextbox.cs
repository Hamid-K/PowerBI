using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000289 RID: 649
	[Serializable]
	internal sealed class FunctionTextbox : BaseInternalExpression
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x0003015E File Offset: 0x0002E35E
		public FunctionTextbox(Textbox tb)
		{
			this.t = tb;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0003016D File Offset: 0x0002E36D
		public FunctionTextbox(IInternalExpression nameExpr)
		{
			this.m_nameExpr = nameExpr;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003017C File Offset: 0x0002E37C
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00030180 File Offset: 0x0002E380
		public override string WriteSource(NameChanges nameChanges)
		{
			this.m_nameExpr = new ConstantString(nameChanges.GetNewName(NameChanges.EntryType.ReportItem, (string)this.m_nameExpr.Evaluate()));
			return "ReportItems!" + (string)this.m_nameExpr.Evaluate() + ".Value";
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x000301CE File Offset: 0x0002E3CE
		internal Textbox Textbox
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000301D6 File Offset: 0x0002E3D6
		public override object Evaluate()
		{
			return null;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000301D9 File Offset: 0x0002E3D9
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			if (this.m_nameExpr != null)
			{
				this.m_nameExpr.Traverse(callback);
			}
		}

		// Token: 0x040006AE RID: 1710
		private IInternalExpression m_nameExpr;

		// Token: 0x040006AF RID: 1711
		private readonly Textbox t;
	}
}
