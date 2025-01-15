using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AA RID: 682
	public sealed class GetUserAvailabilityResults
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x00042464 File Offset: 0x00041464
		internal GetUserAvailabilityResults()
		{
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0004246C File Offset: 0x0004146C
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00042474 File Offset: 0x00041474
		internal SuggestionsResponse SuggestionsResponse
		{
			get
			{
				return this.suggestionsResponse;
			}
			set
			{
				this.suggestionsResponse = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0004247D File Offset: 0x0004147D
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x00042485 File Offset: 0x00041485
		public ServiceResponseCollection<AttendeeAvailability> AttendeesAvailability
		{
			get
			{
				return this.attendeesAvailability;
			}
			internal set
			{
				this.attendeesAvailability = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0004248E File Offset: 0x0004148E
		public Collection<Suggestion> Suggestions
		{
			get
			{
				if (this.suggestionsResponse == null)
				{
					return null;
				}
				this.suggestionsResponse.ThrowIfNecessary();
				return this.suggestionsResponse.Suggestions;
			}
		}

		// Token: 0x040013B1 RID: 5041
		private ServiceResponseCollection<AttendeeAvailability> attendeesAvailability;

		// Token: 0x040013B2 RID: 5042
		private SuggestionsResponse suggestionsResponse;
	}
}
