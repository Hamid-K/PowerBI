using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA2 RID: 7586
	internal sealed class ReportingCultureService : ICultureService
	{
		// Token: 0x0600BC21 RID: 48161 RVA: 0x002612A4 File Offset: 0x0025F4A4
		public ReportingCultureService(IEngineHost engineHost)
		{
			this.cultureService = engineHost.QueryService<ICultureService>();
			this.reportCultureAccess = engineHost.QueryService<IReportCultureAccess>();
		}

		// Token: 0x17002E63 RID: 11875
		// (get) Token: 0x0600BC22 RID: 48162 RVA: 0x002612C4 File Offset: 0x0025F4C4
		public ICulture DefaultCulture
		{
			get
			{
				this.ReportAccess();
				return this.cultureService.DefaultCulture;
			}
		}

		// Token: 0x0600BC23 RID: 48163 RVA: 0x002612D7 File Offset: 0x0025F4D7
		public ICulture GetCulture(string name)
		{
			this.ReportAccess();
			return this.cultureService.GetCulture(name);
		}

		// Token: 0x0600BC24 RID: 48164 RVA: 0x002612EB File Offset: 0x0025F4EB
		private void ReportAccess()
		{
			if (this.reportCultureAccess != null)
			{
				this.reportCultureAccess.CultureAccessed();
				this.reportCultureAccess = null;
			}
		}

		// Token: 0x04005FC0 RID: 24512
		private readonly ICultureService cultureService;

		// Token: 0x04005FC1 RID: 24513
		private IReportCultureAccess reportCultureAccess;
	}
}
