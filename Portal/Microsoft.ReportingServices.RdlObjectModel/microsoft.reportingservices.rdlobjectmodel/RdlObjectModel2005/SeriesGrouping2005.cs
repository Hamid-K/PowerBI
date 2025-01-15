using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000006 RID: 6
	internal class SeriesGrouping2005 : ReportObject
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000025CC File Offset: 0x000007CC
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000025DF File Offset: 0x000007DF
		public DynamicSeries2005 DynamicSeries
		{
			get
			{
				return (DynamicSeries2005)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000025EE File Offset: 0x000007EE
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002601 File Offset: 0x00000801
		[XmlElement(typeof(RdlCollection<StaticMember2005>))]
		[XmlArrayItem("StaticMember", typeof(StaticMember2005))]
		public IList<StaticMember2005> StaticSeries
		{
			get
			{
				return (IList<StaticMember2005>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002610 File Offset: 0x00000810
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002623 File Offset: 0x00000823
		public Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002632 File Offset: 0x00000832
		public SeriesGrouping2005()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000263A File Offset: 0x0000083A
		public SeriesGrouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002643 File Offset: 0x00000843
		public override void Initialize()
		{
			base.Initialize();
			this.StaticSeries = new RdlCollection<StaticMember2005>();
		}

		// Token: 0x020002EF RID: 751
		internal class Definition : DefinitionStore<SeriesGrouping2005, SeriesGrouping2005.Definition.Properties>
		{
			// Token: 0x060016EB RID: 5867 RVA: 0x000363F2 File Offset: 0x000345F2
			private Definition()
			{
			}

			// Token: 0x02000423 RID: 1059
			public enum Properties
			{
				// Token: 0x0400081A RID: 2074
				DynamicSeries,
				// Token: 0x0400081B RID: 2075
				StaticSeries,
				// Token: 0x0400081C RID: 2076
				Style
			}
		}
	}
}
