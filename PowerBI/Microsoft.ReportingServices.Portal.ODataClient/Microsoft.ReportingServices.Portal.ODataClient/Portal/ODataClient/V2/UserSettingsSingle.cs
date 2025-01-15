using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005E RID: 94
	[OriginalName("UserSettingsSingle")]
	public class UserSettingsSingle : DataServiceQuerySingle<UserSettings>
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x000094EB File Offset: 0x000076EB
		public UserSettingsSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000094F5 File Offset: 0x000076F5
		public UserSettingsSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009500 File Offset: 0x00007700
		public UserSettingsSingle(DataServiceQuerySingle<UserSettings> query)
			: base(query)
		{
		}
	}
}
