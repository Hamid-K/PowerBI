using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000445 RID: 1093
	[Serializable]
	internal abstract class ServerAuditStatement : TSqlStatement
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x0016F888 File Offset: 0x0016DA88
		// (set) Token: 0x060031BE RID: 12734 RVA: 0x0016F890 File Offset: 0x0016DA90
		public Identifier AuditName
		{
			get
			{
				return this._auditName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._auditName = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x0016F8A0 File Offset: 0x0016DAA0
		// (set) Token: 0x060031C0 RID: 12736 RVA: 0x0016F8A8 File Offset: 0x0016DAA8
		public AuditTarget AuditTarget
		{
			get
			{
				return this._auditTarget;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._auditTarget = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x0016F8B8 File Offset: 0x0016DAB8
		public IList<AuditOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x0016F8C0 File Offset: 0x0016DAC0
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x0016F8C8 File Offset: 0x0016DAC8
		public BooleanExpression PredicateExpression
		{
			get
			{
				return this._predicateExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._predicateExpression = value;
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x0016F8D8 File Offset: 0x0016DAD8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.AuditName != null)
			{
				this.AuditName.Accept(visitor);
			}
			if (this.AuditTarget != null)
			{
				this.AuditTarget.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			if (this.PredicateExpression != null)
			{
				this.PredicateExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E7F RID: 7807
		private Identifier _auditName;

		// Token: 0x04001E80 RID: 7808
		private AuditTarget _auditTarget;

		// Token: 0x04001E81 RID: 7809
		private List<AuditOption> _options = new List<AuditOption>();

		// Token: 0x04001E82 RID: 7810
		private BooleanExpression _predicateExpression;
	}
}
