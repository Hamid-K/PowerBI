using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000329 RID: 809
	public class ScheduleExpiration : ExpirationDefinition
	{
		// Token: 0x04000AF0 RID: 2800
		[XmlElement(typeof(ScheduleDefinition))]
		[XmlElement(typeof(ScheduleReference))]
		public ScheduleDefinitionOrReference Schedule;
	}
}
