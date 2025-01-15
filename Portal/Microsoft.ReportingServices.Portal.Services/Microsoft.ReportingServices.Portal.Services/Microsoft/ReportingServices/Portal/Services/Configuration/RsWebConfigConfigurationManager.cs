using System;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x02000071 RID: 113
	internal sealed class RsWebConfigConfigurationManager : IWebConfigurationManager
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015B44 File Offset: 0x00013D44
		public string CustomAuthLoginUrl
		{
			get
			{
				return WebConfigUtil.LoginUrl;
			}
		}
	}
}
