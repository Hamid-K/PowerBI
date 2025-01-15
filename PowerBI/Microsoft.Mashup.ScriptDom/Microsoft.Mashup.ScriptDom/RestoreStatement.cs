using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034D RID: 845
	[Serializable]
	internal class RestoreStatement : TSqlStatement
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x0016990A File Offset: 0x00167B0A
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x00169912 File Offset: 0x00167B12
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x00169922 File Offset: 0x00167B22
		public IList<DeviceInfo> Devices
		{
			get
			{
				return this._devices;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x0016992A File Offset: 0x00167B2A
		public IList<BackupRestoreFileInfo> Files
		{
			get
			{
				return this._files;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x00169932 File Offset: 0x00167B32
		public IList<RestoreOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x0016993A File Offset: 0x00167B3A
		// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x00169942 File Offset: 0x00167B42
		public RestoreStatementKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0016994B File Offset: 0x00167B4B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00169958 File Offset: 0x00167B58
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			int i = 0;
			int count = this.Devices.Count;
			while (i < count)
			{
				this.Devices[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.Files.Count;
			while (j < count2)
			{
				this.Files[j].Accept(visitor);
				j++;
			}
			int k = 0;
			int count3 = this.Options.Count;
			while (k < count3)
			{
				this.Options[k].Accept(visitor);
				k++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CDC RID: 7388
		private IdentifierOrValueExpression _databaseName;

		// Token: 0x04001CDD RID: 7389
		private List<DeviceInfo> _devices = new List<DeviceInfo>();

		// Token: 0x04001CDE RID: 7390
		private List<BackupRestoreFileInfo> _files = new List<BackupRestoreFileInfo>();

		// Token: 0x04001CDF RID: 7391
		private List<RestoreOption> _options = new List<RestoreOption>();

		// Token: 0x04001CE0 RID: 7392
		private RestoreStatementKind _kind;
	}
}
