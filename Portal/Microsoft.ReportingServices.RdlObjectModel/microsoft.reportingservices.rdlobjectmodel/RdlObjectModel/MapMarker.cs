using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A5 RID: 421
	public class MapMarker : ReportObject
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00022B0C File Offset: 0x00020D0C
		public MapMarker()
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00022B14 File Offset: 0x00020D14
		internal MapMarker(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00022B1D File Offset: 0x00020D1D
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x00022B2B File Offset: 0x00020D2B
		[ReportExpressionDefaultValue(typeof(MapMarkerStyles), MapMarkerStyles.None)]
		public ReportExpression<MapMarkerStyles> MapMarkerStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapMarkerStyles>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00022B3F File Offset: 0x00020D3F
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00022B52 File Offset: 0x00020D52
		public MapMarkerImage MapMarkerImage
		{
			get
			{
				return (MapMarkerImage)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00022B61 File Offset: 0x00020D61
		public override void Initialize()
		{
			base.Initialize();
			this.MapMarkerStyle = MapMarkerStyles.None;
		}

		// Token: 0x020003D1 RID: 977
		internal class Definition : DefinitionStore<MapMarker, MapMarker.Definition.Properties>
		{
			// Token: 0x06001875 RID: 6261 RVA: 0x0003B729 File Offset: 0x00039929
			private Definition()
			{
			}

			// Token: 0x020004E9 RID: 1257
			internal enum Properties
			{
				// Token: 0x04001015 RID: 4117
				MapMarkerStyle,
				// Token: 0x04001016 RID: 4118
				MapMarkerImage,
				// Token: 0x04001017 RID: 4119
				PropertyCount
			}
		}
	}
}
