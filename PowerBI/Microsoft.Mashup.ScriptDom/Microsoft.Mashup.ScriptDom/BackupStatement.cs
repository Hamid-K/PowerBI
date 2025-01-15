using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034A RID: 842
	[Serializable]
	internal abstract class BackupStatement : TSqlStatement
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x0016977E File Offset: 0x0016797E
		// (set) Token: 0x06002BDD RID: 11229 RVA: 0x00169786 File Offset: 0x00167986
		public IdentifierOrValueExpression DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseName = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x00169796 File Offset: 0x00167996
		public IList<BackupOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0016979E File Offset: 0x0016799E
		public IList<MirrorToClause> MirrorToClauses
		{
			get
			{
				return this._mirrorToClauses;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x001697A6 File Offset: 0x001679A6
		public IList<DeviceInfo> Devices
		{
			get
			{
				return this._devices;
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x001697B0 File Offset: 0x001679B0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.MirrorToClauses.Count;
			while (j < count2)
			{
				this.MirrorToClauses[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = this.Devices.Count;
			while (k < count3)
			{
				this.Devices[k].Accept(visitor);
				k++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CD7 RID: 7383
		private IdentifierOrValueExpression _databaseName;

		// Token: 0x04001CD8 RID: 7384
		private List<BackupOption> _options = new List<BackupOption>();

		// Token: 0x04001CD9 RID: 7385
		private List<MirrorToClause> _mirrorToClauses = new List<MirrorToClause>();

		// Token: 0x04001CDA RID: 7386
		private List<DeviceInfo> _devices = new List<DeviceInfo>();
	}
}
