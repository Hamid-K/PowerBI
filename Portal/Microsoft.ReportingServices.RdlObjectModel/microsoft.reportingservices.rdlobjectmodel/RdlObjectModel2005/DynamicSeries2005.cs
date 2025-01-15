using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000007 RID: 7
	internal class DynamicSeries2005 : ReportObject
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002656 File Offset: 0x00000856
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002669 File Offset: 0x00000869
		public Grouping2005 Grouping
		{
			get
			{
				return (Grouping2005)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002686 File Offset: 0x00000886
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002699 File Offset: 0x00000899
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> Sorting
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000026B6 File Offset: 0x000008B6
		public ReportExpression Label
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000026CA File Offset: 0x000008CA
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000026DD File Offset: 0x000008DD
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000026EC File Offset: 0x000008EC
		public DynamicSeries2005()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000026F4 File Offset: 0x000008F4
		public DynamicSeries2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000026FD File Offset: 0x000008FD
		public override void Initialize()
		{
			base.Initialize();
			this.Grouping = new Grouping2005();
		}

		// Token: 0x020002F0 RID: 752
		internal class Definition : DefinitionStore<DynamicSeries2005, DynamicSeries2005.Definition.Properties>
		{
			// Token: 0x060016EC RID: 5868 RVA: 0x000363FA File Offset: 0x000345FA
			private Definition()
			{
			}

			// Token: 0x02000424 RID: 1060
			public enum Properties
			{
				// Token: 0x0400081E RID: 2078
				Grouping,
				// Token: 0x0400081F RID: 2079
				Sorting,
				// Token: 0x04000820 RID: 2080
				Label,
				// Token: 0x04000821 RID: 2081
				LabelLocID
			}
		}
	}
}
