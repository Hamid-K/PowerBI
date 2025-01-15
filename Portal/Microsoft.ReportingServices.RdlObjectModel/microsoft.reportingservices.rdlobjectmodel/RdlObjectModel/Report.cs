using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C6 RID: 198
	public class Report : ReportObject
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001C474 File Offset: 0x0001A674
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001C47C File Offset: 0x0001A67C
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public SizeTypes ReportUnitType
		{
			get
			{
				return this.m_reportUnitType;
			}
			set
			{
				if (this.m_reportUnitType != value)
				{
					this.m_reportUnitType = value;
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001C48E File Offset: 0x0001A68E
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001C496 File Offset: 0x0001A696
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue(false)]
		public bool ReportTemplate
		{
			get
			{
				return this.m_reportTemplate;
			}
			set
			{
				this.m_reportTemplate = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001C49F File Offset: 0x0001A69F
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0001C4A7 File Offset: 0x0001A6A7
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
			set
			{
				this.m_reportServerUrl = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string ExpressionDialog
		{
			get
			{
				return this.m_expressionDialog;
			}
			set
			{
				this.m_expressionDialog = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001C4C1 File Offset: 0x0001A6C1
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0001C4C9 File Offset: 0x0001A6C9
		[XmlElement(ElementName = "MustUnderstand", Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string ReportDesignerMustUnderstand
		{
			get
			{
				return this.m_reportDesignerMustUnderstand;
			}
			set
			{
				this.m_reportDesignerMustUnderstand = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001C4D2 File Offset: 0x0001A6D2
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0001C4E6 File Offset: 0x0001A6E6
		[DefaultValue("")]
		[XmlAttribute]
		public string MustUnderstand
		{
			get
			{
				return (string)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0001C509 File Offset: 0x0001A709
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001C518 File Offset: 0x0001A718
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0001C52C File Offset: 0x0001A72C
		[DefaultValue("Arial")]
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily")]
		public string DefaultFontFamily
		{
			get
			{
				return (string)base.PropertyStore.GetObject(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001C53C File Offset: 0x0001A73C
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001C544 File Offset: 0x0001A744
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public string ThemeName { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001C54D File Offset: 0x0001A74D
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001C555 File Offset: 0x0001A755
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public string WebAuthoringVersion { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001C55E File Offset: 0x0001A75E
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0001C566 File Offset: 0x0001A766
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public string DefaultView { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001C56F File Offset: 0x0001A76F
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001C577 File Offset: 0x0001A777
		[XmlElement(typeof(AuthoringMetadata), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/authoringmetadata")]
		public AuthoringMetadata AuthoringMetadata { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001C580 File Offset: 0x0001A780
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0001C588 File Offset: 0x0001A788
		[XmlElement(typeof(RdlCollection<FilterSelection>), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		[XmlArrayItem("FilterSelection", typeof(FilterSelection), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public IList<FilterSelection> FilterSelections
		{
			get
			{
				return this.m_filterSelections;
			}
			set
			{
				this.m_filterSelections = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001C591 File Offset: 0x0001A791
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		[DefaultValue("")]
		public string Author
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0001C5B3 File Offset: 0x0001A7B3
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0001C5C1 File Offset: 0x0001A7C1
		public ReportExpression<int> AutoRefresh
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001C5D5 File Offset: 0x0001A7D5
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		[ReportExpressionDefaultValue]
		public ReportExpression InitialPageName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001C5F9 File Offset: 0x0001A7F9
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0001C60C File Offset: 0x0001A80C
		[XmlElement(typeof(RdlCollection<DataSource>))]
		public IList<DataSource> DataSources
		{
			get
			{
				return (IList<DataSource>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001C61B File Offset: 0x0001A81B
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0001C62E File Offset: 0x0001A82E
		[XmlElement(typeof(RdlCollection<DataSet>))]
		public IList<DataSet> DataSets
		{
			get
			{
				return (IList<DataSet>)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001C63D File Offset: 0x0001A83D
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0001C64F File Offset: 0x0001A84F
		[XmlIgnore]
		public virtual Body Body
		{
			get
			{
				return this.GetFirstSection("Body").Body;
			}
			set
			{
				this.GetFirstSection("Body").Body = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001C662 File Offset: 0x0001A862
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0001C676 File Offset: 0x0001A876
		[XmlElement(typeof(RdlCollection<ReportSection>))]
		public IList<ReportSection> ReportSections
		{
			get
			{
				return (IList<ReportSection>)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0001C686 File Offset: 0x0001A886
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0001C699 File Offset: 0x0001A899
		[XmlElement(typeof(RdlCollection<ReportParameter>))]
		public IList<ReportParameter> ReportParameters
		{
			get
			{
				return (IList<ReportParameter>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0001C6B7 File Offset: 0x0001A8B7
		[XmlElement(typeof(ReportParametersLayout))]
		public ReportParametersLayout ReportParametersLayout
		{
			get
			{
				return base.PropertyStore.GetObject<ReportParametersLayout>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001C6C7 File Offset: 0x0001A8C7
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x0001C6DA File Offset: 0x0001A8DA
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		[DefaultValue("")]
		public string Code
		{
			get
			{
				return (string)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001C70B File Offset: 0x0001A90B
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001C71D File Offset: 0x0001A91D
		[XmlIgnore]
		public virtual ReportSize Width
		{
			get
			{
				return this.GetFirstSection("Width").Width;
			}
			set
			{
				this.GetFirstSection("Width").Width = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001C730 File Offset: 0x0001A930
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001C742 File Offset: 0x0001A942
		[XmlIgnore]
		public virtual Page Page
		{
			get
			{
				return this.GetFirstSection("Page").Page;
			}
			set
			{
				this.GetFirstSection("Page").Page = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001C755 File Offset: 0x0001A955
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001C769 File Offset: 0x0001A969
		[XmlElement(typeof(RdlCollection<EmbeddedImage>))]
		public IList<EmbeddedImage> EmbeddedImages
		{
			get
			{
				return (IList<EmbeddedImage>)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001C779 File Offset: 0x0001A979
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0001C788 File Offset: 0x0001A988
		[ReportExpressionDefaultValue]
		public ReportExpression Language
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0001C79D File Offset: 0x0001A99D
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0001C7B1 File Offset: 0x0001A9B1
		[XmlElement(typeof(RdlCollection<string>))]
		[XmlArrayItem("CodeModule", typeof(string))]
		public IList<string> CodeModules
		{
			get
			{
				return (IList<string>)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001C7C1 File Offset: 0x0001A9C1
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001C7D5 File Offset: 0x0001A9D5
		[XmlElement(typeof(RdlCollection<Class>))]
		public IList<Class> Classes
		{
			get
			{
				return (IList<Class>)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001C7E5 File Offset: 0x0001A9E5
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0001C7F9 File Offset: 0x0001A9F9
		[XmlElement(typeof(RdlCollection<Variable>))]
		public IList<Variable> Variables
		{
			get
			{
				return (IList<Variable>)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001C809 File Offset: 0x0001AA09
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0001C818 File Offset: 0x0001AA18
		[DefaultValue(false)]
		public bool DeferVariableEvaluation
		{
			get
			{
				return base.PropertyStore.GetBoolean(14);
			}
			set
			{
				base.PropertyStore.SetBoolean(14, value);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001C828 File Offset: 0x0001AA28
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0001C837 File Offset: 0x0001AA37
		[DefaultValue(false)]
		public bool ConsumeContainerWhitespace
		{
			get
			{
				return base.PropertyStore.GetBoolean(15);
			}
			set
			{
				base.PropertyStore.SetBoolean(15, value);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001C847 File Offset: 0x0001AA47
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0001C85B File Offset: 0x0001AA5B
		[DefaultValue("")]
		public string DataTransform
		{
			get
			{
				return (string)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001C86B File Offset: 0x0001AA6B
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001C87F File Offset: 0x0001AA7F
		[DefaultValue("")]
		public string DataSchema
		{
			get
			{
				return (string)base.PropertyStore.GetObject(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001C88F File Offset: 0x0001AA8F
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001C8A3 File Offset: 0x0001AAA3
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001C8B3 File Offset: 0x0001AAB3
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0001C8C2 File Offset: 0x0001AAC2
		[DefaultValue(DataElementStyles.Attribute)]
		[ValidEnumValues("ReportDataElementOutputTypes")]
		public DataElementStyles DataElementStyle
		{
			get
			{
				return (DataElementStyles)base.PropertyStore.GetInteger(19);
			}
			set
			{
				((EnumProperty)DefinitionStore<Report, Report.Definition.Properties>.GetProperty(19)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(19, (int)value);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C8E5 File Offset: 0x0001AAE5
		public Report()
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C8ED File Offset: 0x0001AAED
		internal Report(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		public override void Initialize()
		{
			base.Initialize();
			this.ReportSections = new RdlCollection<ReportSection>();
			ReportSection reportSection = new ReportSection();
			this.ReportSections.Add(reportSection);
			this.DataSources = new RdlCollection<DataSource>();
			this.DataSets = new RdlCollection<DataSet>();
			this.ReportParameters = new RdlCollection<ReportParameter>();
			this.ReportParametersLayout = new ReportParametersLayout();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.EmbeddedImages = new RdlCollection<EmbeddedImage>();
			this.CodeModules = new RdlCollection<string>();
			this.Classes = new RdlCollection<Class>();
			this.Variables = new RdlCollection<Variable>();
			this.DataElementStyle = DataElementStyles.Attribute;
			this.FilterSelections = new RdlCollection<FilterSelection>();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		private ReportSection GetFirstSection(string propertyName)
		{
			IList<ReportSection> reportSections = this.ReportSections;
			if (reportSections == null || reportSections.Count == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot access {0} when no sections are defined", propertyName));
			}
			return reportSections[0];
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public DataSet GetDataSetByName(string name)
		{
			foreach (DataSet dataSet in this.DataSets)
			{
				if (StringUtil.CompareClsCompliantIdentifiers(dataSet.Name, name) == 0)
				{
					return dataSet;
				}
			}
			return null;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001CA38 File Offset: 0x0001AC38
		public DataSource GetDataSourceByName(string name)
		{
			foreach (DataSource dataSource in this.DataSources)
			{
				if (StringUtil.CompareClsCompliantIdentifiers(dataSource.Name, name) == 0)
				{
					return dataSource;
				}
			}
			return null;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001CA94 File Offset: 0x0001AC94
		public EmbeddedImage GetEmbeddedImageByName(string name)
		{
			foreach (EmbeddedImage embeddedImage in this.EmbeddedImages)
			{
				if (StringUtil.CompareClsCompliantIdentifiers(embeddedImage.Name, name) == 0)
				{
					return embeddedImage;
				}
			}
			return null;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public ReportParameter GetReportParameterByName(string name)
		{
			foreach (ReportParameter reportParameter in this.ReportParameters)
			{
				if (StringUtil.CompareClsCompliantIdentifiers(reportParameter.Name, name) == 0)
				{
					return reportParameter;
				}
			}
			return null;
		}

		// Token: 0x04000170 RID: 368
		private SizeTypes m_reportUnitType;

		// Token: 0x04000171 RID: 369
		private bool m_reportTemplate;

		// Token: 0x04000172 RID: 370
		private string m_reportServerUrl;

		// Token: 0x04000173 RID: 371
		private string m_expressionDialog;

		// Token: 0x04000174 RID: 372
		private string m_reportDesignerMustUnderstand;

		// Token: 0x04000175 RID: 373
		private IList<FilterSelection> m_filterSelections;

		// Token: 0x04000176 RID: 374
		public const string DefaultFontFamilyDefault = "Arial";

		// Token: 0x0200036F RID: 879
		internal class Definition : DefinitionStore<Report, Report.Definition.Properties>
		{
			// Token: 0x06001802 RID: 6146 RVA: 0x0003AF93 File Offset: 0x00039193
			private Definition()
			{
			}

			// Token: 0x0200048C RID: 1164
			internal enum Properties
			{
				// Token: 0x04000B2E RID: 2862
				Description,
				// Token: 0x04000B2F RID: 2863
				DescriptionLocID,
				// Token: 0x04000B30 RID: 2864
				Author,
				// Token: 0x04000B31 RID: 2865
				AutoRefresh,
				// Token: 0x04000B32 RID: 2866
				DataSources,
				// Token: 0x04000B33 RID: 2867
				DataSets,
				// Token: 0x04000B34 RID: 2868
				ReportParameters,
				// Token: 0x04000B35 RID: 2869
				CustomProperties,
				// Token: 0x04000B36 RID: 2870
				Code,
				// Token: 0x04000B37 RID: 2871
				EmbeddedImages,
				// Token: 0x04000B38 RID: 2872
				Language,
				// Token: 0x04000B39 RID: 2873
				CodeModules,
				// Token: 0x04000B3A RID: 2874
				Classes,
				// Token: 0x04000B3B RID: 2875
				Variables,
				// Token: 0x04000B3C RID: 2876
				DeferVariableEvaluation,
				// Token: 0x04000B3D RID: 2877
				ConsumeContainerWhitespace,
				// Token: 0x04000B3E RID: 2878
				DataTransform,
				// Token: 0x04000B3F RID: 2879
				DataSchema,
				// Token: 0x04000B40 RID: 2880
				DataElementName,
				// Token: 0x04000B41 RID: 2881
				DataElementStyle,
				// Token: 0x04000B42 RID: 2882
				ReportSections,
				// Token: 0x04000B43 RID: 2883
				InitialPageName,
				// Token: 0x04000B44 RID: 2884
				ReportParametersLayout,
				// Token: 0x04000B45 RID: 2885
				DefaultFontFamily,
				// Token: 0x04000B46 RID: 2886
				MustUnderstand,
				// Token: 0x04000B47 RID: 2887
				PropertyCount
			}
		}
	}
}
