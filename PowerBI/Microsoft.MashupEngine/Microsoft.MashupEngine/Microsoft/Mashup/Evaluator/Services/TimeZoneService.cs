using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA4 RID: 7588
	internal sealed class TimeZoneService : ITimeZoneService
	{
		// Token: 0x0600BC27 RID: 48167 RVA: 0x00261352 File Offset: 0x0025F552
		public TimeZoneService(string name, TimeZoneInfo timeZone)
		{
			this.timeZone = new TimeZoneService.TimeZone(name, timeZone);
		}

		// Token: 0x17002E64 RID: 11876
		// (get) Token: 0x0600BC28 RID: 48168 RVA: 0x00261367 File Offset: 0x0025F567
		public ITimeZone DefaultTimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x0600BC29 RID: 48169 RVA: 0x00261370 File Offset: 0x0025F570
		public bool TryGetTimeZone(string name, out ITimeZone timeZone)
		{
			bool flag;
			try
			{
				timeZone = new TimeZoneService.TimeZone(name, TimeZoneInfo.FindSystemTimeZoneById(name));
				flag = true;
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				timeZone = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04005FC4 RID: 24516
		private readonly ITimeZone timeZone;

		// Token: 0x02001DA5 RID: 7589
		private sealed class TimeZone : ITimeZone
		{
			// Token: 0x0600BC2A RID: 48170 RVA: 0x002613C0 File Offset: 0x0025F5C0
			public TimeZone(string name, TimeZoneInfo value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x17002E65 RID: 11877
			// (get) Token: 0x0600BC2B RID: 48171 RVA: 0x002613D6 File Offset: 0x0025F5D6
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17002E66 RID: 11878
			// (get) Token: 0x0600BC2C RID: 48172 RVA: 0x002613DE File Offset: 0x0025F5DE
			public TimeZoneInfo Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04005FC5 RID: 24517
			private readonly string name;

			// Token: 0x04005FC6 RID: 24518
			private readonly TimeZoneInfo value;
		}
	}
}
