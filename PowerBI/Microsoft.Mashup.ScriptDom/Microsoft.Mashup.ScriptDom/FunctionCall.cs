using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	internal class FunctionCall : PrimaryExpression
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00161EFA File Offset: 0x001600FA
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x00161F02 File Offset: 0x00160102
		public CallTarget CallTarget
		{
			get
			{
				return this._callTarget;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._callTarget = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x00161F12 File Offset: 0x00160112
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x00161F1A File Offset: 0x0016011A
		public Identifier FunctionName
		{
			get
			{
				return this._functionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._functionName = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00161F2A File Offset: 0x0016012A
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00161F32 File Offset: 0x00160132
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x00161F3A File Offset: 0x0016013A
		public UniqueRowFilter UniqueRowFilter
		{
			get
			{
				return this._uniqueRowFilter;
			}
			set
			{
				this._uniqueRowFilter = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00161F43 File Offset: 0x00160143
		// (set) Token: 0x060024A0 RID: 9376 RVA: 0x00161F4B File Offset: 0x0016014B
		public OverClause OverClause
		{
			get
			{
				return this._overClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._overClause = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00161F5B File Offset: 0x0016015B
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00161F63 File Offset: 0x00160163
		public WithinGroupClause WithinGroupClause
		{
			get
			{
				return this._withinGroupClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._withinGroupClause = value;
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00161F73 File Offset: 0x00160173
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00161F80 File Offset: 0x00160180
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.CallTarget != null)
			{
				this.CallTarget.Accept(visitor);
			}
			if (this.FunctionName != null)
			{
				this.FunctionName.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			if (this.OverClause != null)
			{
				this.OverClause.Accept(visitor);
			}
			if (this.WithinGroupClause != null)
			{
				this.WithinGroupClause.Accept(visitor);
			}
		}

		// Token: 0x04001ACF RID: 6863
		private CallTarget _callTarget;

		// Token: 0x04001AD0 RID: 6864
		private Identifier _functionName;

		// Token: 0x04001AD1 RID: 6865
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();

		// Token: 0x04001AD2 RID: 6866
		private UniqueRowFilter _uniqueRowFilter;

		// Token: 0x04001AD3 RID: 6867
		private OverClause _overClause;

		// Token: 0x04001AD4 RID: 6868
		private WithinGroupClause _withinGroupClause;
	}
}
