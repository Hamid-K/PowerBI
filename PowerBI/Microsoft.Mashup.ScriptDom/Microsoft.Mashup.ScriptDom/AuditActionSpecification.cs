using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200043C RID: 1084
	[Serializable]
	internal class AuditActionSpecification : AuditSpecificationDetail
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x0016F6B0 File Offset: 0x0016D8B0
		public IList<DatabaseAuditAction> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x0016F6B8 File Offset: 0x0016D8B8
		public IList<SecurityPrincipal> Principals
		{
			get
			{
				return this._principals;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x0016F6C0 File Offset: 0x0016D8C0
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x0016F6C8 File Offset: 0x0016D8C8
		public SecurityTargetObject TargetObject
		{
			get
			{
				return this._targetObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._targetObject = value;
			}
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x0016F6D8 File Offset: 0x0016D8D8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0016F6E4 File Offset: 0x0016D8E4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Actions.Count;
			while (i < count)
			{
				this.Actions[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.Principals.Count;
			while (j < count2)
			{
				this.Principals[j].Accept(visitor);
				j++;
			}
			if (this.TargetObject != null)
			{
				this.TargetObject.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E7A RID: 7802
		private List<DatabaseAuditAction> _actions = new List<DatabaseAuditAction>();

		// Token: 0x04001E7B RID: 7803
		private List<SecurityPrincipal> _principals = new List<SecurityPrincipal>();

		// Token: 0x04001E7C RID: 7804
		private SecurityTargetObject _targetObject;
	}
}
