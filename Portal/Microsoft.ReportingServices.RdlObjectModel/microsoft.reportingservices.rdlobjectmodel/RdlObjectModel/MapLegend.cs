using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000196 RID: 406
	public class MapLegend : MapDockableSubItem, INamedObject
	{
		// Token: 0x06000D2D RID: 3373 RVA: 0x00021FF3 File Offset: 0x000201F3
		public MapLegend()
		{
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00021FFB File Offset: 0x000201FB
		internal MapLegend(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00022004 File Offset: 0x00020204
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00022013 File Offset: 0x00020213
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return base.PropertyStore.GetObject<string>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00022023 File Offset: 0x00020223
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00022032 File Offset: 0x00020232
		[ReportExpressionDefaultValue(typeof(MapLegendLayouts), MapLegendLayouts.AutoTable)]
		public ReportExpression<MapLegendLayouts> Layout
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLegendLayouts>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00022047 File Offset: 0x00020247
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0002205B File Offset: 0x0002025B
		public MapLegendTitle MapLegendTitle
		{
			get
			{
				return (MapLegendTitle)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0002206B File Offset: 0x0002026B
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0002207A File Offset: 0x0002027A
		public ReportExpression<bool> AutoFitTextDisabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0002208F File Offset: 0x0002028F
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0002209E File Offset: 0x0002029E
		[ReportExpressionDefaultValue(typeof(ReportSize), "7pt")]
		public ReportExpression<ReportSize> MinFontSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x000220B3 File Offset: 0x000202B3
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x000220C2 File Offset: 0x000202C2
		public ReportExpression<bool> InterlacedRows
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x000220D7 File Offset: 0x000202D7
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x000220E6 File Offset: 0x000202E6
		[ReportExpressionDefaultValue(typeof(ReportColor), "LightGray")]
		public ReportExpression<ReportColor> InterlacedRowsColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x000220FB File Offset: 0x000202FB
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0002210A File Offset: 0x0002030A
		public ReportExpression<bool> EquallySpacedItems
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0002211F File Offset: 0x0002031F
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0002212E File Offset: 0x0002032E
		[ReportExpressionDefaultValue(typeof(int), "25")]
		public ReportExpression<int> TextWrapThreshold
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00022144 File Offset: 0x00020344
		public override void Initialize()
		{
			base.Initialize();
			this.Layout = MapLegendLayouts.AutoTable;
			this.MinFontSize = new ReportExpression<ReportSize>("7pt", CultureInfo.InvariantCulture);
			this.InterlacedRowsColor = new ReportExpression<ReportColor>("LightGray", CultureInfo.InvariantCulture);
			this.TextWrapThreshold = 25;
		}

		// Token: 0x020003C3 RID: 963
		internal new class Definition : DefinitionStore<MapLegend, MapLegend.Definition.Properties>
		{
			// Token: 0x06001867 RID: 6247 RVA: 0x0003B6B9 File Offset: 0x000398B9
			private Definition()
			{
			}

			// Token: 0x020004DB RID: 1243
			internal enum Properties
			{
				// Token: 0x04000F9C RID: 3996
				Style,
				// Token: 0x04000F9D RID: 3997
				MapLocation,
				// Token: 0x04000F9E RID: 3998
				MapSize,
				// Token: 0x04000F9F RID: 3999
				LeftMargin,
				// Token: 0x04000FA0 RID: 4000
				RightMargin,
				// Token: 0x04000FA1 RID: 4001
				TopMargin,
				// Token: 0x04000FA2 RID: 4002
				BottomMargin,
				// Token: 0x04000FA3 RID: 4003
				ZIndex,
				// Token: 0x04000FA4 RID: 4004
				ActionInfo,
				// Token: 0x04000FA5 RID: 4005
				MapPosition,
				// Token: 0x04000FA6 RID: 4006
				DockOutsideViewport,
				// Token: 0x04000FA7 RID: 4007
				Hidden,
				// Token: 0x04000FA8 RID: 4008
				ToolTip,
				// Token: 0x04000FA9 RID: 4009
				Name,
				// Token: 0x04000FAA RID: 4010
				Layout,
				// Token: 0x04000FAB RID: 4011
				MapLegendTitle,
				// Token: 0x04000FAC RID: 4012
				AutoFitTextDisabled,
				// Token: 0x04000FAD RID: 4013
				MinFontSize,
				// Token: 0x04000FAE RID: 4014
				InterlacedRows,
				// Token: 0x04000FAF RID: 4015
				InterlacedRowsColor,
				// Token: 0x04000FB0 RID: 4016
				EquallySpacedItems,
				// Token: 0x04000FB1 RID: 4017
				TextWrapThreshold,
				// Token: 0x04000FB2 RID: 4018
				PropertyCount
			}
		}
	}
}
