using System;
using System.Collections.Generic;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200000E RID: 14
	public sealed class DataSourceSettingNeededEventArgs : EventArgs
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000048E9 File Offset: 0x00002AE9
		internal DataSourceSettingNeededEventArgs(MashupSecurityException details)
		{
			this.details = details;
			this.newSettings = new Dictionary<DataSource, DataSourceSetting>();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004903 File Offset: 0x00002B03
		public MashupSecurityException Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000490B File Offset: 0x00002B0B
		public IDictionary<DataSource, DataSourceSetting> NewSettings
		{
			get
			{
				return this.newSettings;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly MashupSecurityException details;

		// Token: 0x0400004B RID: 75
		private readonly Dictionary<DataSource, DataSourceSetting> newSettings;
	}
}
