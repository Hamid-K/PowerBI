using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000081 RID: 129
	public class Chart : DataRegion
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00017E79 File Offset: 0x00016079
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00017E8D File Offset: 0x0001608D
		public ChartCategoryHierarchy ChartCategoryHierarchy
		{
			get
			{
				return (ChartCategoryHierarchy)base.PropertyStore.GetObject(25);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00017EAB File Offset: 0x000160AB
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00017EBF File Offset: 0x000160BF
		public ChartSeriesHierarchy ChartSeriesHierarchy
		{
			get
			{
				return (ChartSeriesHierarchy)base.PropertyStore.GetObject(26);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00017EDD File Offset: 0x000160DD
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00017EF1 File Offset: 0x000160F1
		public ChartData ChartData
		{
			get
			{
				return (ChartData)base.PropertyStore.GetObject(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00017F01 File Offset: 0x00016101
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00017F15 File Offset: 0x00016115
		[XmlElement(typeof(RdlCollection<ChartArea>))]
		public IList<ChartArea> ChartAreas
		{
			get
			{
				return (IList<ChartArea>)base.PropertyStore.GetObject(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00017F25 File Offset: 0x00016125
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00017F39 File Offset: 0x00016139
		[XmlElement(typeof(RdlCollection<ChartLegend>))]
		public IList<ChartLegend> ChartLegends
		{
			get
			{
				return (IList<ChartLegend>)base.PropertyStore.GetObject(29);
			}
			set
			{
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00017F49 File Offset: 0x00016149
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00017F5D File Offset: 0x0001615D
		[XmlElement(typeof(RdlCollection<ChartTitle>))]
		public IList<ChartTitle> ChartTitles
		{
			get
			{
				return (IList<ChartTitle>)base.PropertyStore.GetObject(30);
			}
			set
			{
				base.PropertyStore.SetObject(30, value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00017F6D File Offset: 0x0001616D
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00017F7C File Offset: 0x0001617C
		[ReportExpressionDefaultValue(typeof(ChartPalettes), ChartPalettes.Default)]
		public ReportExpression<ChartPalettes> Palette
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartPalettes>>(31);
			}
			set
			{
				base.PropertyStore.SetObject(31, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00017F91 File Offset: 0x00016191
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00017FA5 File Offset: 0x000161A5
		[XmlElement(typeof(RdlCollection<ReportExpression<ReportColor>>))]
		[XmlArrayItem("ChartCustomPaletteColor", typeof(ReportExpression<ReportColor>))]
		public IList<ReportExpression<ReportColor>> ChartCustomPaletteColors
		{
			get
			{
				return (IList<ReportExpression<ReportColor>>)base.PropertyStore.GetObject(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00017FB5 File Offset: 0x000161B5
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00017FC4 File Offset: 0x000161C4
		[ReportExpressionDefaultValue(typeof(ChartPaletteHatchBehaviorTypes), ChartPaletteHatchBehaviorTypes.Default)]
		public ReportExpression<ChartPaletteHatchBehaviorTypes> PaletteHatchBehavior
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartPaletteHatchBehaviorTypes>>(33);
			}
			set
			{
				base.PropertyStore.SetObject(33, value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00017FD9 File Offset: 0x000161D9
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00017FE8 File Offset: 0x000161E8
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> DynamicHeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00017FFD File Offset: 0x000161FD
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001800C File Offset: 0x0001620C
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> DynamicWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(35);
			}
			set
			{
				base.PropertyStore.SetObject(35, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00018021 File Offset: 0x00016221
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00018035 File Offset: 0x00016235
		public ChartBorderSkin ChartBorderSkin
		{
			get
			{
				return (ChartBorderSkin)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00018045 File Offset: 0x00016245
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00018059 File Offset: 0x00016259
		public ChartTitle ChartNoDataMessage
		{
			get
			{
				return (ChartTitle)base.PropertyStore.GetObject(37);
			}
			set
			{
				base.PropertyStore.SetObject(37, value);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018069 File Offset: 0x00016269
		public Chart()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00018071 File Offset: 0x00016271
		internal Chart(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001807C File Offset: 0x0001627C
		public override void Initialize()
		{
			base.Initialize();
			this.ChartCategoryHierarchy = new ChartCategoryHierarchy();
			this.ChartSeriesHierarchy = new ChartSeriesHierarchy();
			this.ChartAreas = new RdlCollection<ChartArea>();
			this.ChartLegends = new RdlCollection<ChartLegend>();
			this.ChartTitles = new RdlCollection<ChartTitle>();
			this.ChartCustomPaletteColors = new RdlCollection<ReportExpression<ReportColor>>();
		}

		// Token: 0x02000334 RID: 820
		internal new class Definition : DefinitionStore<Chart, Chart.Definition.Properties>
		{
			// Token: 0x0600179F RID: 6047 RVA: 0x0003A68C File Offset: 0x0003888C
			private Definition()
			{
			}

			// Token: 0x02000456 RID: 1110
			internal enum Properties
			{
				// Token: 0x04000914 RID: 2324
				Style,
				// Token: 0x04000915 RID: 2325
				Name,
				// Token: 0x04000916 RID: 2326
				ActionInfo,
				// Token: 0x04000917 RID: 2327
				Top,
				// Token: 0x04000918 RID: 2328
				Left,
				// Token: 0x04000919 RID: 2329
				Height,
				// Token: 0x0400091A RID: 2330
				Width,
				// Token: 0x0400091B RID: 2331
				ZIndex,
				// Token: 0x0400091C RID: 2332
				Visibility,
				// Token: 0x0400091D RID: 2333
				ToolTip,
				// Token: 0x0400091E RID: 2334
				ToolTipLocID,
				// Token: 0x0400091F RID: 2335
				DocumentMapLabel,
				// Token: 0x04000920 RID: 2336
				DocumentMapLabelLocID,
				// Token: 0x04000921 RID: 2337
				Bookmark,
				// Token: 0x04000922 RID: 2338
				RepeatWith,
				// Token: 0x04000923 RID: 2339
				CustomProperties,
				// Token: 0x04000924 RID: 2340
				DataElementName,
				// Token: 0x04000925 RID: 2341
				DataElementOutput,
				// Token: 0x04000926 RID: 2342
				KeepTogether,
				// Token: 0x04000927 RID: 2343
				NoRowsMessage,
				// Token: 0x04000928 RID: 2344
				DataSetName,
				// Token: 0x04000929 RID: 2345
				PageBreak,
				// Token: 0x0400092A RID: 2346
				PageName,
				// Token: 0x0400092B RID: 2347
				Filters,
				// Token: 0x0400092C RID: 2348
				SortExpressions,
				// Token: 0x0400092D RID: 2349
				ChartCategoryHierarchy,
				// Token: 0x0400092E RID: 2350
				ChartSeriesHierarchy,
				// Token: 0x0400092F RID: 2351
				ChartData,
				// Token: 0x04000930 RID: 2352
				ChartAreas,
				// Token: 0x04000931 RID: 2353
				ChartLegends,
				// Token: 0x04000932 RID: 2354
				ChartTitles,
				// Token: 0x04000933 RID: 2355
				Palette,
				// Token: 0x04000934 RID: 2356
				ChartCustomPaletteColors,
				// Token: 0x04000935 RID: 2357
				PaletteHatchBehavior,
				// Token: 0x04000936 RID: 2358
				DynamicHeight,
				// Token: 0x04000937 RID: 2359
				DynamicWidth,
				// Token: 0x04000938 RID: 2360
				ChartBorderSkin,
				// Token: 0x04000939 RID: 2361
				ChartNoDataMessage,
				// Token: 0x0400093A RID: 2362
				PropertyCount
			}
		}
	}
}
