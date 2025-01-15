using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000495 RID: 1173
	[Serializable]
	internal class SecondaryRoleReplicaOption : AvailabilityReplicaOption
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x0017137E File Offset: 0x0016F57E
		// (set) Token: 0x0600337B RID: 13179 RVA: 0x00171386 File Offset: 0x0016F586
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

		// Token: 0x0600337C RID: 13180 RVA: 0x0017138F File Offset: 0x0016F58F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x0017139B File Offset: 0x0016F59B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF6 RID: 7926
		private AllowConnectionsOptionKind _allowConnections;
	}
}
