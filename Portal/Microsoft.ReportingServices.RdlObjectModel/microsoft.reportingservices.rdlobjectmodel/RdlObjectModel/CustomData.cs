using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000AE RID: 174
	public class CustomData : DataRegionBody
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001B57E File Offset: 0x0001977E
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001B591 File Offset: 0x00019791
		public string DataSetName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001B5AE File Offset: 0x000197AE
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001B5C1 File Offset: 0x000197C1
		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001B5D0 File Offset: 0x000197D0
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0001B5E3 File Offset: 0x000197E3
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001B5F2 File Offset: 0x000197F2
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0001B605 File Offset: 0x00019805
		public DataHierarchy DataColumnHierarchy
		{
			get
			{
				return (DataHierarchy)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001B614 File Offset: 0x00019814
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001B627 File Offset: 0x00019827
		public DataHierarchy DataRowHierarchy
		{
			get
			{
				return (DataHierarchy)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001B636 File Offset: 0x00019836
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0001B649 File Offset: 0x00019849
		[XmlElement(typeof(RdlCollection<IList<IList<DataValue>>>))]
		[XmlArrayItem("DataRow", typeof(DataRow), NestingLevel = 0)]
		[XmlArrayItem("DataCell", typeof(DataCell), NestingLevel = 1)]
		public IList<IList<IList<DataValue>>> DataRows
		{
			get
			{
				return (IList<IList<IList<DataValue>>>)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001B658 File Offset: 0x00019858
		public CustomData()
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001B660 File Offset: 0x00019860
		internal CustomData(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001B669 File Offset: 0x00019869
		public override void Initialize()
		{
			base.Initialize();
			this.DataSetName = "";
			this.Filters = new RdlCollection<Filter>();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.DataRows = new RdlCollection<IList<IList<DataValue>>>();
		}

		// Token: 0x0200035F RID: 863
		internal class Definition : DefinitionStore<CustomData, CustomData.Definition.Properties>
		{
			// Token: 0x060017E2 RID: 6114 RVA: 0x0003AC33 File Offset: 0x00038E33
			private Definition()
			{
			}

			// Token: 0x0200047E RID: 1150
			internal enum Properties
			{
				// Token: 0x04000ACD RID: 2765
				DataSetName,
				// Token: 0x04000ACE RID: 2766
				Filters,
				// Token: 0x04000ACF RID: 2767
				SortExpressions,
				// Token: 0x04000AD0 RID: 2768
				DataColumnHierarchy,
				// Token: 0x04000AD1 RID: 2769
				DataRowHierarchy,
				// Token: 0x04000AD2 RID: 2770
				DataRows,
				// Token: 0x04000AD3 RID: 2771
				PropertyCount
			}
		}
	}
}
