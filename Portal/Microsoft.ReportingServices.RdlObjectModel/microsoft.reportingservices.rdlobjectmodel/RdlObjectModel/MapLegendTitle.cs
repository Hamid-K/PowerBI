using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A2 RID: 418
	public class MapLegendTitle : ReportObject
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x00022984 File Offset: 0x00020B84
		public MapLegendTitle()
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002298C File Offset: 0x00020B8C
		internal MapLegendTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00022995 File Offset: 0x00020B95
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x000229A8 File Offset: 0x00020BA8
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000229B7 File Offset: 0x00020BB7
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x000229C5 File Offset: 0x00020BC5
		public ReportExpression Caption
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000229D9 File Offset: 0x00020BD9
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x000229E7 File Offset: 0x00020BE7
		[ReportExpressionDefaultValue(typeof(MapLegendTitleSeparators), MapLegendTitleSeparators.None)]
		public ReportExpression<MapLegendTitleSeparators> TitleSeparator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLegendTitleSeparators>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000229FB File Offset: 0x00020BFB
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00022A09 File Offset: 0x00020C09
		[ReportExpressionDefaultValue(typeof(ReportColor), "Gray")]
		public ReportExpression<ReportColor> TitleSeparatorColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00022A1D File Offset: 0x00020C1D
		public override void Initialize()
		{
			base.Initialize();
			this.TitleSeparator = MapLegendTitleSeparators.None;
			this.TitleSeparatorColor = new ReportExpression<ReportColor>("Gray", CultureInfo.InvariantCulture);
		}

		// Token: 0x020003CE RID: 974
		internal class Definition : DefinitionStore<MapLegendTitle, MapLegendTitle.Definition.Properties>
		{
			// Token: 0x06001872 RID: 6258 RVA: 0x0003B711 File Offset: 0x00039911
			private Definition()
			{
			}

			// Token: 0x020004E6 RID: 1254
			internal enum Properties
			{
				// Token: 0x04001007 RID: 4103
				Style,
				// Token: 0x04001008 RID: 4104
				Caption,
				// Token: 0x04001009 RID: 4105
				TitleSeparator,
				// Token: 0x0400100A RID: 4106
				TitleSeparatorColor,
				// Token: 0x0400100B RID: 4107
				PropertyCount
			}
		}
	}
}
