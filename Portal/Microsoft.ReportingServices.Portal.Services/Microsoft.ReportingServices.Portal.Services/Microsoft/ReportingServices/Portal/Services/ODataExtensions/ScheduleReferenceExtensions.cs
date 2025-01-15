using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004B RID: 75
	internal static class ScheduleReferenceExtensions
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		public static global::Model.ScheduleReference ToWebApi(this Microsoft.ReportingServices.Library.Soap.ScheduleDefinitionOrReference scheduleReference)
		{
			if (scheduleReference == null)
			{
				throw new ArgumentNullException("scheduleReference");
			}
			if (scheduleReference is Microsoft.ReportingServices.Library.Soap.NoSchedule)
			{
				return null;
			}
			if (scheduleReference is Microsoft.ReportingServices.Library.Soap.ScheduleReference)
			{
				return new global::Model.ScheduleReference
				{
					ScheduleID = ((Microsoft.ReportingServices.Library.Soap.ScheduleReference)scheduleReference).ScheduleID,
					Definition = null
				};
			}
			if (scheduleReference is Microsoft.ReportingServices.Library.Soap.ScheduleDefinition)
			{
				return new global::Model.ScheduleReference
				{
					ScheduleID = null,
					Definition = ((Microsoft.ReportingServices.Library.Soap.ScheduleDefinition)scheduleReference).ToWebAPI()
				};
			}
			return null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00011B46 File Offset: 0x0000FD46
		public static Microsoft.ReportingServices.Library.Soap.ScheduleDefinitionOrReference ToSoapApi(this global::Model.ScheduleReference scheduleReference)
		{
			if (scheduleReference == null)
			{
				return new Microsoft.ReportingServices.Library.Soap.NoSchedule();
			}
			if (scheduleReference.ScheduleID == null)
			{
				return scheduleReference.Definition.ToSoapAPI();
			}
			return new Microsoft.ReportingServices.Library.Soap.ScheduleReference
			{
				ScheduleID = scheduleReference.ScheduleID,
				Definition = null
			};
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00011B7D File Offset: 0x0000FD7D
		public static Microsoft.SqlServer.ReportingServices2010.ScheduleDefinitionOrReference ToSoapProxyApi(this global::Model.ScheduleReference scheduleReference)
		{
			if (scheduleReference == null)
			{
				return new Microsoft.SqlServer.ReportingServices2010.NoSchedule();
			}
			if (scheduleReference.ScheduleID == null)
			{
				return scheduleReference.Definition.ToSoapProxyAPI();
			}
			return new Microsoft.SqlServer.ReportingServices2010.ScheduleReference
			{
				ScheduleID = scheduleReference.ScheduleID,
				Definition = null
			};
		}
	}
}
