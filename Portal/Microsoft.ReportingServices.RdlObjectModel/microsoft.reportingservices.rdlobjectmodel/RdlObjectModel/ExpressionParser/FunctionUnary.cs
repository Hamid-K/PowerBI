using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002A1 RID: 673
	[Serializable]
	internal abstract class FunctionUnary : BaseInternalExpression
	{
		// Token: 0x060014F7 RID: 5367 RVA: 0x000311E1 File Offset: 0x0002F3E1
		public FunctionUnary()
		{
			this._rhs = null;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000311F0 File Offset: 0x0002F3F0
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = this.Rhs.WriteSource(nameChanges);
			if (this.Rhs.PriorityCode > this.PriorityCode)
			{
				text = "(" + text + ")";
			}
			if (base.Bracketed)
			{
				return "(" + this.UnaryOperator() + text + ")";
			}
			return this.UnaryOperator() + text;
		}

		// Token: 0x060014F9 RID: 5369
		public abstract string UnaryOperator();

		// Token: 0x060014FA RID: 5370 RVA: 0x00031259 File Offset: 0x0002F459
		public FunctionUnary(IInternalExpression r)
		{
			this._rhs = r;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00031268 File Offset: 0x0002F468
		public override bool IsConstant()
		{
			return this._rhs.IsConstant();
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00031275 File Offset: 0x0002F475
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0003127D File Offset: 0x0002F47D
		public IInternalExpression Rhs
		{
			get
			{
				return this._rhs;
			}
			set
			{
				this._rhs = value;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private IInternalExpression _rhs;
	}
}
