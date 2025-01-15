using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B4 RID: 180
	public abstract class DataRegion : ReportItem
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001B82A File Offset: 0x00019A2A
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001B839 File Offset: 0x00019A39
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return base.PropertyStore.GetBoolean(18);
			}
			set
			{
				base.PropertyStore.SetBoolean(18, value);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001B849 File Offset: 0x00019A49
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001B858 File Offset: 0x00019A58
		[ReportExpressionDefaultValue]
		public ReportExpression NoRowsMessage
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001B86D File Offset: 0x00019A6D
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001B881 File Offset: 0x00019A81
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001B891 File Offset: 0x00019A91
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001B8A5 File Offset: 0x00019AA5
		public PageBreak PageBreak
		{
			get
			{
				return (PageBreak)base.PropertyStore.GetObject(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001B8B5 File Offset: 0x00019AB5
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		[ReportExpressionDefaultValue]
		public ReportExpression PageName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001B8D9 File Offset: 0x00019AD9
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0001B8ED File Offset: 0x00019AED
		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001B8FD File Offset: 0x00019AFD
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001B911 File Offset: 0x00019B11
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001B921 File Offset: 0x00019B21
		public DataRegion()
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001B929 File Offset: 0x00019B29
		internal DataRegion(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001B932 File Offset: 0x00019B32
		public override void Initialize()
		{
			base.Initialize();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.Filters = new RdlCollection<Filter>();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001B950 File Offset: 0x00019B50
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.DataSetName);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001B96C File Offset: 0x00019B6C
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Report ancestor = base.GetAncestor<Report>();
			if (ancestor != null)
			{
				DataSet dataSetByName = ancestor.GetDataSetByName(this.DataSetName);
				if (dataSetByName != null && !dependencies.Contains(dataSetByName))
				{
					dependencies.Add(dataSetByName);
				}
			}
		}

		// Token: 0x02000365 RID: 869
		internal new class Definition : DefinitionStore<DataRegion, DataRegion.Definition.Properties>
		{
			// Token: 0x060017F8 RID: 6136 RVA: 0x0003AF43 File Offset: 0x00039143
			private Definition()
			{
			}

			// Token: 0x02000482 RID: 1154
			internal enum Properties
			{
				// Token: 0x04000AE0 RID: 2784
				Style,
				// Token: 0x04000AE1 RID: 2785
				Name,
				// Token: 0x04000AE2 RID: 2786
				ActionInfo,
				// Token: 0x04000AE3 RID: 2787
				Top,
				// Token: 0x04000AE4 RID: 2788
				Left,
				// Token: 0x04000AE5 RID: 2789
				Height,
				// Token: 0x04000AE6 RID: 2790
				Width,
				// Token: 0x04000AE7 RID: 2791
				ZIndex,
				// Token: 0x04000AE8 RID: 2792
				Visibility,
				// Token: 0x04000AE9 RID: 2793
				ToolTip,
				// Token: 0x04000AEA RID: 2794
				ToolTipLocID,
				// Token: 0x04000AEB RID: 2795
				DocumentMapLabel,
				// Token: 0x04000AEC RID: 2796
				DocumentMapLabelLocID,
				// Token: 0x04000AED RID: 2797
				Bookmark,
				// Token: 0x04000AEE RID: 2798
				RepeatWith,
				// Token: 0x04000AEF RID: 2799
				CustomProperties,
				// Token: 0x04000AF0 RID: 2800
				DataElementName,
				// Token: 0x04000AF1 RID: 2801
				DataElementOutput,
				// Token: 0x04000AF2 RID: 2802
				KeepTogether,
				// Token: 0x04000AF3 RID: 2803
				NoRowsMessage,
				// Token: 0x04000AF4 RID: 2804
				DataSetName,
				// Token: 0x04000AF5 RID: 2805
				PageBreak,
				// Token: 0x04000AF6 RID: 2806
				PageName,
				// Token: 0x04000AF7 RID: 2807
				Filters,
				// Token: 0x04000AF8 RID: 2808
				SortExpressions
			}
		}
	}
}
