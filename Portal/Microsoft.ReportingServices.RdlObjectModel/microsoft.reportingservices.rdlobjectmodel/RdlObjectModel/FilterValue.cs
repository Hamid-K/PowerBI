using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200020B RID: 523
	public class FilterValue
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00028168 File Offset: 0x00026368
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00028170 File Offset: 0x00026370
		public string Label { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00028179 File Offset: 0x00026379
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00028181 File Offset: 0x00026381
		[XmlElement(typeof(RdlCollection<FilterKey>), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[XmlArrayItem("FilterKey", typeof(FilterKey), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public IList<FilterKey> FilterKeys { get; set; }
	}
}
