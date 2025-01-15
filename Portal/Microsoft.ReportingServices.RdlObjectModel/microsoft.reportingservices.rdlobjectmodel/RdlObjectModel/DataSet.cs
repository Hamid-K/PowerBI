using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BA RID: 186
	public class DataSet : DataSetBase
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001B9DD File Offset: 0x00019BDD
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		public Query Query
		{
			get
			{
				return (Query)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
				if (value != null)
				{
					base.PropertyStore.SetObject(10, null);
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001BA10 File Offset: 0x00019C10
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001BA24 File Offset: 0x00019C24
		public SharedDataSet SharedDataSet
		{
			get
			{
				return (SharedDataSet)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
				if (value != null)
				{
					base.PropertyStore.SetObject(7, null);
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001BA44 File Offset: 0x00019C44
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001BA57 File Offset: 0x00019C57
		[XmlElement(typeof(RdlCollection<Field>))]
		public IList<Field> Fields
		{
			get
			{
				return (IList<Field>)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001BA66 File Offset: 0x00019C66
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001BA7A File Offset: 0x00019C7A
		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001BA8A File Offset: 0x00019C8A
		public DataSet()
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001BA92 File Offset: 0x00019C92
		internal DataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001BA9B File Offset: 0x00019C9B
		public override void Initialize()
		{
			base.Initialize();
			this.Fields = new RdlCollection<Field>();
			this.Filters = new RdlCollection<Filter>();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001BABC File Offset: 0x00019CBC
		private bool FieldExistsByName(IList<Field> list, string name)
		{
			using (IEnumerator<Field> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001BB10 File Offset: 0x00019D10
		public bool IsSharedDataSourceReference()
		{
			if (this.SharedDataSet != null)
			{
				return true;
			}
			DataSource dataSource = this.Query.DataSource;
			return dataSource != null && !string.IsNullOrEmpty(dataSource.DataSourceReference);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001BB46 File Offset: 0x00019D46
		public override QueryBase GetQuery()
		{
			return this.Query;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001BB4E File Offset: 0x00019D4E
		public IList<QueryParameter> GetQueryParameters()
		{
			if (this.Query != null)
			{
				return this.Query.QueryParameters;
			}
			if (this.SharedDataSet != null)
			{
				return this.SharedDataSet.QueryParameters;
			}
			return null;
		}

		// Token: 0x02000366 RID: 870
		internal new class Definition : DefinitionStore<DataSet, DataSet.Definition.Properties>
		{
			// Token: 0x02000483 RID: 1155
			public enum Properties
			{
				// Token: 0x04000AFA RID: 2810
				Name,
				// Token: 0x04000AFB RID: 2811
				CaseSensitivity,
				// Token: 0x04000AFC RID: 2812
				Collation,
				// Token: 0x04000AFD RID: 2813
				AccentSensitivity,
				// Token: 0x04000AFE RID: 2814
				KanatypeSensitivity,
				// Token: 0x04000AFF RID: 2815
				WidthSensitivity,
				// Token: 0x04000B00 RID: 2816
				InterpretSubtotalsAsDetails,
				// Token: 0x04000B01 RID: 2817
				Query,
				// Token: 0x04000B02 RID: 2818
				Fields,
				// Token: 0x04000B03 RID: 2819
				Filters,
				// Token: 0x04000B04 RID: 2820
				SharedDataSet
			}
		}
	}
}
