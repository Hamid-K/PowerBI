using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FE RID: 1022
	public class Report
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x0007F5C3 File Offset: 0x0007D7C3
		// (set) Token: 0x0600205B RID: 8283 RVA: 0x0007F5CB File Offset: 0x0007D7CB
		[DesignOnly(true)]
		[DefaultValue(typeof(Unit), "0.125in")]
		public Unit GridSpacing
		{
			get
			{
				return this.m_GridSpacing;
			}
			set
			{
				this.m_GridSpacing = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0007F5D4 File Offset: 0x0007D7D4
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x0007F5DC File Offset: 0x0007D7DC
		[DesignOnly(true)]
		[DefaultValue(true)]
		public bool DrawGrid
		{
			get
			{
				return this.m_DrawGrid;
			}
			set
			{
				this.m_DrawGrid = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x0007F5E5 File Offset: 0x0007D7E5
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x0007F5ED File Offset: 0x0007D7ED
		[DesignOnly(true)]
		[DefaultValue(true)]
		public bool SnapToGrid
		{
			get
			{
				return this.m_SnapToGrid;
			}
			set
			{
				this.m_SnapToGrid = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x0007F5F6 File Offset: 0x0007D7F6
		// (set) Token: 0x06002061 RID: 8289 RVA: 0x0007F5FE File Offset: 0x0007D7FE
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0007F607 File Offset: 0x0007D807
		// (set) Token: 0x06002063 RID: 8291 RVA: 0x0007F60F File Offset: 0x0007D80F
		[DefaultValue(typeof(Unit), "0")]
		public Unit TopMargin
		{
			get
			{
				return this.m_MarginTop;
			}
			set
			{
				this.m_MarginTop = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0007F618 File Offset: 0x0007D818
		// (set) Token: 0x06002065 RID: 8293 RVA: 0x0007F620 File Offset: 0x0007D820
		[DefaultValue(typeof(Unit), "0")]
		public Unit BottomMargin
		{
			get
			{
				return this.m_MarginBottom;
			}
			set
			{
				this.m_MarginBottom = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x0007F629 File Offset: 0x0007D829
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x0007F631 File Offset: 0x0007D831
		[DefaultValue(typeof(Unit), "0")]
		public Unit LeftMargin
		{
			get
			{
				return this.m_MarginLeft;
			}
			set
			{
				this.m_MarginLeft = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0007F63A File Offset: 0x0007D83A
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0007F642 File Offset: 0x0007D842
		[DefaultValue(typeof(Unit), "0")]
		public Unit RightMargin
		{
			get
			{
				return this.m_MarginRight;
			}
			set
			{
				this.m_MarginRight = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0007F64B File Offset: 0x0007D84B
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0007F653 File Offset: 0x0007D853
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0007F65C File Offset: 0x0007D85C
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x0007F664 File Offset: 0x0007D864
		[DefaultValue("")]
		public string Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x0007F66D File Offset: 0x0007D86D
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x0007F675 File Offset: 0x0007D875
		[DefaultValue("")]
		public string Author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0007F67E File Offset: 0x0007D87E
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x0007F686 File Offset: 0x0007D886
		[DefaultValue("")]
		public string AutoRefresh
		{
			get
			{
				return this.m_autoRefresh;
			}
			set
			{
				this.m_autoRefresh = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x0007F68F File Offset: 0x0007D88F
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x0007F697 File Offset: 0x0007D897
		public Unit PageWidth
		{
			get
			{
				return this.m_pageWidth;
			}
			set
			{
				Utils.ValidateValueRange("PageWidth", value, new Unit(0), null);
				if (this.m_pageWidth != value)
				{
					this.m_pageWidth = value;
				}
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0007F6C5 File Offset: 0x0007D8C5
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x0007F6CD File Offset: 0x0007D8CD
		public Unit PageHeight
		{
			get
			{
				return this.m_pageHeight;
			}
			set
			{
				Utils.ValidateValueRange("PageHeight", value, new Unit(0), null);
				this.m_pageHeight = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0007F6ED File Offset: 0x0007D8ED
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x0007F6F5 File Offset: 0x0007D8F5
		public ReportParameters ReportParameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0007F6FE File Offset: 0x0007D8FE
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x0007F706 File Offset: 0x0007D906
		public PageHeader PageHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0007F70F File Offset: 0x0007D90F
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x0007F717 File Offset: 0x0007D917
		public PageFooter PageFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0007F720 File Offset: 0x0007D920
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Body Body
		{
			get
			{
				return this.m_body;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0007F728 File Offset: 0x0007D928
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x0007F730 File Offset: 0x0007D930
		public DataSources DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0007F739 File Offset: 0x0007D939
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x0007F741 File Offset: 0x0007D941
		public DataSets DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0007F74A File Offset: 0x0007D94A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customProperties == null)
				{
					this.m_customProperties = new CustomPropertyCollection();
				}
				return this.m_customProperties;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x0007F765 File Offset: 0x0007D965
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x0007F76D File Offset: 0x0007D96D
		public List<EmbeddedImage> EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
			set
			{
				this.m_embeddedImages = value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x0007F776 File Offset: 0x0007D976
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x0007F77E File Offset: 0x0007D97E
		[XmlArrayItem("CodeModule", typeof(string))]
		public List<string> CodeModules
		{
			get
			{
				return this.m_codeModules;
			}
			set
			{
				this.m_codeModules = value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0007F787 File Offset: 0x0007D987
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x0007F78F File Offset: 0x0007D98F
		public List<Class> Classes
		{
			get
			{
				return this.m_classes;
			}
			set
			{
				this.m_classes = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0007F798 File Offset: 0x0007D998
		// (set) Token: 0x06002089 RID: 8329 RVA: 0x0007F7A0 File Offset: 0x0007D9A0
		[DefaultValue("")]
		public string DataTransform
		{
			get
			{
				return this.m_dataTransform;
			}
			set
			{
				this.m_dataTransform = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0007F7A9 File Offset: 0x0007D9A9
		// (set) Token: 0x0600208B RID: 8331 RVA: 0x0007F7B1 File Offset: 0x0007D9B1
		[DefaultValue("")]
		public string DataSchema
		{
			get
			{
				return this.m_dataSchema;
			}
			set
			{
				this.m_dataSchema = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0007F7BA File Offset: 0x0007D9BA
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x0007F7C2 File Offset: 0x0007D9C2
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0007F7CB File Offset: 0x0007D9CB
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x0007F7D3 File Offset: 0x0007D9D3
		[DefaultValue(DataElementStyles.AttributeNormal)]
		public DataElementStyles DataElementStyle
		{
			get
			{
				return this.m_dataElementStyle;
			}
			set
			{
				this.m_dataElementStyle = value;
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0007F7DC File Offset: 0x0007D9DC
		public Report()
		{
			this.m_dataSources = new DataSources();
			this.m_dataSets = new DataSets();
			this.m_body = new Body();
		}

		// Token: 0x04000E20 RID: 3616
		private Unit m_pageWidth;

		// Token: 0x04000E21 RID: 3617
		private Unit m_pageHeight;

		// Token: 0x04000E22 RID: 3618
		private DataSets m_dataSets;

		// Token: 0x04000E23 RID: 3619
		private DataSources m_dataSources;

		// Token: 0x04000E24 RID: 3620
		private CustomPropertyCollection m_customProperties;

		// Token: 0x04000E25 RID: 3621
		private string m_autoRefresh;

		// Token: 0x04000E26 RID: 3622
		private string m_description;

		// Token: 0x04000E27 RID: 3623
		public string Language;

		// Token: 0x04000E28 RID: 3624
		private string m_author;

		// Token: 0x04000E29 RID: 3625
		private string m_code;

		// Token: 0x04000E2A RID: 3626
		private ReportParameters m_parameters;

		// Token: 0x04000E2B RID: 3627
		private PageHeader m_pageHeader;

		// Token: 0x04000E2C RID: 3628
		private PageFooter m_pageFooter;

		// Token: 0x04000E2D RID: 3629
		private List<EmbeddedImage> m_embeddedImages = new List<EmbeddedImage>();

		// Token: 0x04000E2E RID: 3630
		private List<string> m_codeModules;

		// Token: 0x04000E2F RID: 3631
		private List<Class> m_classes;

		// Token: 0x04000E30 RID: 3632
		private string m_dataTransform;

		// Token: 0x04000E31 RID: 3633
		private string m_dataSchema;

		// Token: 0x04000E32 RID: 3634
		private string m_dataElementName;

		// Token: 0x04000E33 RID: 3635
		private DataElementStyles m_dataElementStyle;

		// Token: 0x04000E34 RID: 3636
		private Body m_body;

		// Token: 0x04000E35 RID: 3637
		private Unit m_width;

		// Token: 0x04000E36 RID: 3638
		private Unit m_GridSpacing = Report.Definition.GridSpacing.Default;

		// Token: 0x04000E37 RID: 3639
		private bool m_DrawGrid = true;

		// Token: 0x04000E38 RID: 3640
		private bool m_SnapToGrid = true;

		// Token: 0x04000E39 RID: 3641
		private Unit m_MarginTop;

		// Token: 0x04000E3A RID: 3642
		private Unit m_MarginBottom;

		// Token: 0x04000E3B RID: 3643
		private Unit m_MarginLeft;

		// Token: 0x04000E3C RID: 3644
		private Unit m_MarginRight;

		// Token: 0x02000521 RID: 1313
		public class Definition
		{
			// Token: 0x06002520 RID: 9504 RVA: 0x000025F4 File Offset: 0x000007F4
			protected Definition()
			{
			}

			// Token: 0x0400129D RID: 4765
			public static readonly UnitPropertyDef GridSpacing = new UnitPropertyDef("GridSpacing", new Unit(0.5, UnitType.Cm), new Unit(2.0, UnitType.Inch), new Unit(0.125, UnitType.Inch));

			// Token: 0x0400129E RID: 4766
			public static readonly StringExprPropertyDef Language = new StringExprPropertyDef("Language", null);
		}
	}
}
