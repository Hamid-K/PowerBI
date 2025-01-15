using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000B RID: 11
	internal class DataPoint2005 : ChartDataPoint
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002830 File Offset: 0x00000A30
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002844 File Offset: 0x00000A44
		[XmlElement(typeof(RdlCollection<DataValue2005>))]
		[XmlArrayItem("DataValue", typeof(DataValue2005))]
		public IList<DataValue2005> DataValues
		{
			get
			{
				return (IList<DataValue2005>)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002868 File Offset: 0x00000A68
		public DataLabel2005 DataLabel
		{
			get
			{
				return (DataLabel2005)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002878 File Offset: 0x00000A78
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000288C File Offset: 0x00000A8C
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000289C File Offset: 0x00000A9C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000028B0 File Offset: 0x00000AB0
		public new EmptyColorStyle2005 Style
		{
			get
			{
				return (EmptyColorStyle2005)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000028D4 File Offset: 0x00000AD4
		public Marker2005 Marker
		{
			get
			{
				return (Marker2005)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000028E4 File Offset: 0x00000AE4
		public DataPoint2005()
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000028EC File Offset: 0x00000AEC
		public DataPoint2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000028F5 File Offset: 0x00000AF5
		public override void Initialize()
		{
			base.Initialize();
			base.DataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x020002F4 RID: 756
		internal new class Definition : DefinitionStore<DataPoint2005, DataPoint2005.Definition.Properties>
		{
			// Token: 0x060016F0 RID: 5872 RVA: 0x0003641A File Offset: 0x0003461A
			private Definition()
			{
			}

			// Token: 0x02000428 RID: 1064
			public enum Properties
			{
				// Token: 0x0400082C RID: 2092
				Action = 11,
				// Token: 0x0400082D RID: 2093
				DataLabel,
				// Token: 0x0400082E RID: 2094
				PropertyCount,
				// Token: 0x0400082F RID: 2095
				Style,
				// Token: 0x04000830 RID: 2096
				Marker,
				// Token: 0x04000831 RID: 2097
				DataValues
			}
		}
	}
}
