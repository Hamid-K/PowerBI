using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200020E RID: 526
	public class FilterSelection
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x000281F7 File Offset: 0x000263F7
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x000281FF File Offset: 0x000263FF
		public string DataSourceName { get; set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00028208 File Offset: 0x00026408
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00028210 File Offset: 0x00026410
		public string DatasetName { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00028219 File Offset: 0x00026419
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00028221 File Offset: 0x00026421
		public string EntityName { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0002822A File Offset: 0x0002642A
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00028232 File Offset: 0x00026432
		public string PropertyName { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0002823B File Offset: 0x0002643B
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00028243 File Offset: 0x00026443
		public string ParentPropertyName { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0002824C File Offset: 0x0002644C
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00028254 File Offset: 0x00026454
		public FilterType FilterType { get; set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0002825D File Offset: 0x0002645D
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00028265 File Offset: 0x00026465
		public FilterOperator FilterOperator { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0002826E File Offset: 0x0002646E
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00028276 File Offset: 0x00026476
		[XmlElement(typeof(RdlCollection<FilterValue>), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[XmlArrayItem("FilterValue", typeof(FilterValue), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public IList<FilterValue> FilterValues
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0002827F File Offset: 0x0002647F
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00028287 File Offset: 0x00026487
		public string AdvancedConditionValue { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00028290 File Offset: 0x00026490
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00028298 File Offset: 0x00026498
		[XmlElement(typeof(RdlCollection<AdvancedCondition>), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[XmlArrayItem("AdvancedCondition", typeof(AdvancedCondition), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public IList<AdvancedCondition> AdvancedConditions
		{
			get
			{
				return this.advancedConditions;
			}
			set
			{
				this.advancedConditions = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000282A1 File Offset: 0x000264A1
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x000282A9 File Offset: 0x000264A9
		public string ParameterName { get; set; }

		// Token: 0x040005AB RID: 1451
		private IList<FilterValue> values;

		// Token: 0x040005AC RID: 1452
		private IList<AdvancedCondition> advancedConditions;
	}
}
