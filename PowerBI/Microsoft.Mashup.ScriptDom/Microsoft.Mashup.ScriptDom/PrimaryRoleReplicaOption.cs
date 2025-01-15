using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000494 RID: 1172
	[Serializable]
	internal class PrimaryRoleReplicaOption : AvailabilityReplicaOption
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x00171350 File Offset: 0x0016F550
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x00171358 File Offset: 0x0016F558
		public AllowConnectionsOptionKind AllowConnections
		{
			get
			{
				return this._allowConnections;
			}
			set
			{
				this._allowConnections = value;
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x00171361 File Offset: 0x0016F561
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0017136D File Offset: 0x0016F56D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF5 RID: 7925
		private AllowConnectionsOptionKind _allowConnections;
	}
}
