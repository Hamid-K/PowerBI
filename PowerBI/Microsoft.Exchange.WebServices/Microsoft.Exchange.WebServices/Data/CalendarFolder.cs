using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000190 RID: 400
	[ServiceObjectDefinition("CalendarFolder")]
	public class CalendarFolder : Folder
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x000336D4 File Offset: 0x000326D4
		public new static CalendarFolder Bind(ExchangeService service, FolderId id, PropertySet propertySet)
		{
			return service.BindToFolder<CalendarFolder>(id, propertySet);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000336DE File Offset: 0x000326DE
		public new static CalendarFolder Bind(ExchangeService service, FolderId id)
		{
			return CalendarFolder.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000336EC File Offset: 0x000326EC
		public new static CalendarFolder Bind(ExchangeService service, WellKnownFolderName name, PropertySet propertySet)
		{
			return CalendarFolder.Bind(service, new FolderId(name), propertySet);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000336FB File Offset: 0x000326FB
		public new static CalendarFolder Bind(ExchangeService service, WellKnownFolderName name)
		{
			return CalendarFolder.Bind(service, new FolderId(name), PropertySet.FirstClassProperties);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003370E File Offset: 0x0003270E
		public CalendarFolder(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00033718 File Offset: 0x00032718
		public FindItemsResults<Appointment> FindAppointments(CalendarView view)
		{
			EwsUtilities.ValidateParam(view, "view");
			ServiceResponseCollection<FindItemResponse<Appointment>> serviceResponseCollection = base.InternalFindItems<Appointment>(null, view, null);
			return serviceResponseCollection[0].Results;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00033746 File Offset: 0x00032746
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}
