using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000EB RID: 235
	internal struct VersionPropertiesChange
	{
		// Token: 0x1700012F RID: 303
		public bool this[string key]
		{
			get
			{
				return (this._changeFlag & VersionPropertiesChange.MapStringToEnum(key)) != VersionPropertiesChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= VersionPropertiesChange.MapStringToEnum(key);
					return;
				}
				this._changeFlag &= ~VersionPropertiesChange.MapStringToEnum(key);
			}
		}

		// Token: 0x17000130 RID: 304
		public bool this[VersionPropertiesChanges key]
		{
			get
			{
				return (this._changeFlag & key) != VersionPropertiesChanges.NoChange;
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

		// Token: 0x06000697 RID: 1687 RVA: 0x0001A36C File Offset: 0x0001856C
		public VersionPropertiesChange(VersionPropertiesChange original)
		{
			this._changeFlag = original._changeFlag;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001A37C File Offset: 0x0001857C
		private static VersionPropertiesChanges MapStringToEnum(string name)
		{
			if (name != null)
			{
				if (name == "beginClientVersion")
				{
					return VersionPropertiesChanges.BeginClientVersionChange;
				}
				if (name == "endClientVersion")
				{
					return VersionPropertiesChanges.EndClientVersionChange;
				}
				if (name == "beginServerVersion")
				{
					return VersionPropertiesChanges.BeginServerVersionChange;
				}
				if (name == "endServerVersion")
				{
					return VersionPropertiesChanges.EndServerVersionChange;
				}
			}
			throw new ArgumentException("Unknown Config Element", "name");
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001A3DB File Offset: 0x000185DB
		public bool Changed
		{
			get
			{
				return this._changeFlag != VersionPropertiesChanges.NoChange;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001A3EC File Offset: 0x000185EC
		internal bool CanUpdateConfigDynamically()
		{
			VersionPropertiesChange versionPropertiesChange = new VersionPropertiesChange(this);
			versionPropertiesChange[VersionPropertiesChanges.BeginClientVersionChange] = false;
			versionPropertiesChange[VersionPropertiesChanges.EndClientVersionChange] = false;
			versionPropertiesChange[VersionPropertiesChanges.BeginServerVersionChange] = false;
			versionPropertiesChange[VersionPropertiesChanges.EndServerVersionChange] = false;
			return !versionPropertiesChange.Changed;
		}

		// Token: 0x0400042C RID: 1068
		private VersionPropertiesChanges _changeFlag;
	}
}
