using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D84 RID: 7556
	public class OverrideDefaultCultureService : ICultureService
	{
		// Token: 0x0600BBB4 RID: 48052 RVA: 0x0025FB37 File Offset: 0x0025DD37
		public OverrideDefaultCultureService(ICultureService cultureService, string defaultCulture)
		{
			this.cultureService = cultureService;
			this.defaultCulture = defaultCulture;
		}

		// Token: 0x17002E5C RID: 11868
		// (get) Token: 0x0600BBB5 RID: 48053 RVA: 0x0025FB4D File Offset: 0x0025DD4D
		public ICulture DefaultCulture
		{
			get
			{
				return this.GetCulture(this.defaultCulture);
			}
		}

		// Token: 0x0600BBB6 RID: 48054 RVA: 0x0025FB5B File Offset: 0x0025DD5B
		public ICulture GetCulture(string name)
		{
			return this.cultureService.GetCulture(name);
		}

		// Token: 0x04005F7B RID: 24443
		private readonly ICultureService cultureService;

		// Token: 0x04005F7C RID: 24444
		private readonly string defaultCulture;
	}
}
