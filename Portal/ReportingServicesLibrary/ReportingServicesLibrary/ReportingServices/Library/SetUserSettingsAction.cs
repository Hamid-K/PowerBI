using System;
using Microsoft.ReportingServices.Hybrid.OAuth;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C8 RID: 200
	internal sealed class SetUserSettingsAction : RSSoapAction<SetUserSettingsActionParameters>
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x0002279A File Offset: 0x0002099A
		public SetUserSettingsAction(RSService service)
			: base("SetUserSettingsAction", service)
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000227A8 File Offset: 0x000209A8
		internal override void PerformActionNow()
		{
			UserProperties userProperties = new UserProperties(base.ActionParameters.Properties);
			base.Service.SecMgr.SetServiceToken((userProperties.AADAuthToken != null) ? ServiceToken.FromJson(userProperties.AADAuthToken) : null);
			userProperties.Remove("AADAuthToken");
			base.Service.SecMgr.SetUserSettings(userProperties);
		}
	}
}
