using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000A RID: 10
	internal class ChartSeries2005 : ChartSeries
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000027E0 File Offset: 0x000009E0
		[XmlElement(typeof(RdlCollection<DataPoint2005>))]
		[XmlArrayItem("DataPoint", typeof(DataPoint2005))]
		public IList<DataPoint2005> DataPoints
		{
			get
			{
				return (IList<DataPoint2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000027FD File Offset: 0x000009FD
		[DefaultValue(PlotTypes2005.Auto)]
		public PlotTypes2005 PlotType
		{
			get
			{
				return (PlotTypes2005)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000280C File Offset: 0x00000A0C
		public ChartSeries2005()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002814 File Offset: 0x00000A14
		public ChartSeries2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000281D File Offset: 0x00000A1D
		public override void Initialize()
		{
			base.Initialize();
			this.DataPoints = new RdlCollection<DataPoint2005>();
		}

		// Token: 0x020002F3 RID: 755
		internal new class Definition : DefinitionStore<ChartSeries2005, ChartSeries2005.Definition.Properties>
		{
			// Token: 0x060016EF RID: 5871 RVA: 0x00036412 File Offset: 0x00034612
			private Definition()
			{
			}

			// Token: 0x02000427 RID: 1063
			public enum Properties
			{
				// Token: 0x04000829 RID: 2089
				DataPoints,
				// Token: 0x0400082A RID: 2090
				PlotType
			}
		}
	}
}
