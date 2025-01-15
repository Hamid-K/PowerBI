using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048F RID: 1167
	[Serializable]
	internal class AvailabilityReplica : TSqlFragment
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x001711F5 File Offset: 0x0016F3F5
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x001711FD File Offset: 0x0016F3FD
		public StringLiteral ServerName
		{
			get
			{
				return this._serverName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._serverName = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x0017120D File Offset: 0x0016F40D
		public IList<AvailabilityReplicaOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x00171215 File Offset: 0x0016F415
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x00171224 File Offset: 0x0016F424
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ServerName != null)
			{
				this.ServerName.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EEF RID: 7919
		private StringLiteral _serverName;

		// Token: 0x04001EF0 RID: 7920
		private List<AvailabilityReplicaOption> _options = new List<AvailabilityReplicaOption>();
	}
}
