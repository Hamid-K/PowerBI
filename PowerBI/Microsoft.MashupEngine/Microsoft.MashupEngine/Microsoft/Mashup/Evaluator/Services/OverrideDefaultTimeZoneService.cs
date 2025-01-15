using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D85 RID: 7557
	public class OverrideDefaultTimeZoneService : ITimeZoneService
	{
		// Token: 0x0600BBB7 RID: 48055 RVA: 0x0025FB69 File Offset: 0x0025DD69
		public OverrideDefaultTimeZoneService(ITimeZoneService timeZoneService, ITimeZone defaultTimeZone)
		{
			this.timeZoneService = timeZoneService;
			this.defaultTimeZone = defaultTimeZone;
		}

		// Token: 0x17002E5D RID: 11869
		// (get) Token: 0x0600BBB8 RID: 48056 RVA: 0x0025FB7F File Offset: 0x0025DD7F
		public ITimeZone DefaultTimeZone
		{
			get
			{
				return this.defaultTimeZone;
			}
		}

		// Token: 0x0600BBB9 RID: 48057 RVA: 0x0025FB87 File Offset: 0x0025DD87
		public bool TryGetTimeZone(string name, out ITimeZone timeZone)
		{
			return this.timeZoneService.TryGetTimeZone(name, out timeZone);
		}

		// Token: 0x04005F7D RID: 24445
		private readonly ITimeZoneService timeZoneService;

		// Token: 0x04005F7E RID: 24446
		private readonly ITimeZone defaultTimeZone;
	}
}
