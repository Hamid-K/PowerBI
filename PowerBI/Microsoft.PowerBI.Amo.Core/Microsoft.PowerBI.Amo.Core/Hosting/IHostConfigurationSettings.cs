using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x0200015B RID: 347
	[Guid("DA800437-5FF3-4eda-BFD3-8B1927086DCF")]
	public interface IHostConfigurationSettings
	{
		// Token: 0x060011B4 RID: 4532
		void SetSetting(string settingName, object settingValue, params string[] indexes);

		// Token: 0x060011B5 RID: 4533
		object GetSetting(string settingName, params string[] indexes);
	}
}
