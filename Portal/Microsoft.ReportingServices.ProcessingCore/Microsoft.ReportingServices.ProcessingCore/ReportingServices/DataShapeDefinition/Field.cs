using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059B RID: 1435
	[DataContract]
	internal sealed class Field
	{
		// Token: 0x060051E7 RID: 20967 RVA: 0x0015A342 File Offset: 0x00158542
		internal Field(string id)
		{
			this.m_id = id;
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x0015A351 File Offset: 0x00158551
		internal Field(string id, string dataField, Expression value, string aggregateIndicatorField)
			: this(id)
		{
			this.m_dataField = dataField;
			this.m_value = value;
			this.m_aggregateIndicatorField = aggregateIndicatorField;
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x060051E9 RID: 20969 RVA: 0x0015A370 File Offset: 0x00158570
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x0015A378 File Offset: 0x00158578
		internal string DataField
		{
			get
			{
				return this.m_dataField;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x0015A380 File Offset: 0x00158580
		internal Expression Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x0015A388 File Offset: 0x00158588
		internal string AggregateIndicatorField
		{
			get
			{
				return this.m_aggregateIndicatorField;
			}
		}

		// Token: 0x0400295C RID: 10588
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x0400295D RID: 10589
		[DataMember(Name = "DataField", Order = 2)]
		private readonly string m_dataField;

		// Token: 0x0400295E RID: 10590
		[DataMember(Name = "AgregateIndicatorField", Order = 3)]
		private readonly string m_aggregateIndicatorField;

		// Token: 0x0400295F RID: 10591
		[DataMember(Name = "Value", Order = 4)]
		private readonly Expression m_value;
	}
}
