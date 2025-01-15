using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200211E RID: 8478
	internal class OpenSettings
	{
		// Token: 0x170032B9 RID: 12985
		// (get) Token: 0x0600D1D0 RID: 53712 RVA: 0x0029BBF4 File Offset: 0x00299DF4
		// (set) Token: 0x0600D1D1 RID: 53713 RVA: 0x0029BC10 File Offset: 0x00299E10
		public bool AutoSave
		{
			get
			{
				return this.autoSave == null || this.autoSave.Value;
			}
			set
			{
				this.autoSave = new bool?(value);
			}
		}

		// Token: 0x170032BA RID: 12986
		// (get) Token: 0x0600D1D2 RID: 53714 RVA: 0x0029BC1E File Offset: 0x00299E1E
		// (set) Token: 0x0600D1D3 RID: 53715 RVA: 0x0029BC3B File Offset: 0x00299E3B
		public MarkupCompatibilityProcessSettings MarkupCompatibilityProcessSettings
		{
			get
			{
				if (this._mcSettings == null)
				{
					this._mcSettings = new MarkupCompatibilityProcessSettings(MarkupCompatibilityProcessMode.NoProcess, FileFormatVersions.Office2007);
				}
				return this._mcSettings;
			}
			set
			{
				this._mcSettings = value;
			}
		}

		// Token: 0x170032BB RID: 12987
		// (get) Token: 0x0600D1D4 RID: 53716 RVA: 0x0029BC44 File Offset: 0x00299E44
		// (set) Token: 0x0600D1D5 RID: 53717 RVA: 0x0029BC4C File Offset: 0x00299E4C
		public long MaxCharactersInPart { get; set; }

		// Token: 0x0400695D RID: 26973
		private bool? autoSave;

		// Token: 0x0400695E RID: 26974
		private MarkupCompatibilityProcessSettings _mcSettings;
	}
}
