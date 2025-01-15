using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000EE RID: 238
	internal struct DeploymentSettingsChange
	{
		// Token: 0x1700013A RID: 314
		public bool this[string key]
		{
			get
			{
				return (this._changeFlag & DeploymentSettingsChange.MapStringToEnum(key)) != DeploymentChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= DeploymentSettingsChange.MapStringToEnum(key);
					return;
				}
				this._changeFlag &= ~DeploymentSettingsChange.MapStringToEnum(key);
			}
		}

		// Token: 0x1700013B RID: 315
		public bool this[DeploymentChanges key]
		{
			get
			{
				return (this._changeFlag & key) != DeploymentChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= key;
					return;
				}
				this._changeFlag &= ~key;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001A9D1 File Offset: 0x00018BD1
		public DeploymentSettingsChange(DeploymentSettingsChange original)
		{
			this._changeFlag = original._changeFlag;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		private static DeploymentChanges MapStringToEnum(string name)
		{
			if (name != null)
			{
				if (name == "deploymentMode")
				{
					return DeploymentChanges.DeploymentModeChange;
				}
				if (name == "gracefulShutdown")
				{
					return DeploymentChanges.GracefulShutdownModeChange;
				}
			}
			throw new ArgumentException("Unknown Config Element", name);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001AA1D File Offset: 0x00018C1D
		public bool Changed
		{
			get
			{
				return this._changeFlag != DeploymentChanges.NoChange;
			}
		}

		// Token: 0x04000431 RID: 1073
		private DeploymentChanges _changeFlag;
	}
}
