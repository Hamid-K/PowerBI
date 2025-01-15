using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000199 RID: 409
	public class MapPolygon : MapSpatialElement
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x000222CF File Offset: 0x000204CF
		public MapPolygon()
		{
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000222D7 File Offset: 0x000204D7
		internal MapPolygon(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000222E0 File Offset: 0x000204E0
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x000222EE File Offset: 0x000204EE
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseCustomPolygonTemplate
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00022302 File Offset: 0x00020502
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00022315 File Offset: 0x00020515
		public MapPolygonTemplate MapPolygonTemplate
		{
			get
			{
				return (MapPolygonTemplate)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00022324 File Offset: 0x00020524
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00022332 File Offset: 0x00020532
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseCustomCenterPointTemplate
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00022346 File Offset: 0x00020546
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00022359 File Offset: 0x00020559
		public MapPointTemplate MapCenterPointTemplate
		{
			get
			{
				return (MapPointTemplate)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00022368 File Offset: 0x00020568
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003C5 RID: 965
		internal new class Definition : DefinitionStore<MapPolygon, MapPolygon.Definition.Properties>
		{
			// Token: 0x06001869 RID: 6249 RVA: 0x0003B6C9 File Offset: 0x000398C9
			private Definition()
			{
			}

			// Token: 0x020004DD RID: 1245
			internal enum Properties
			{
				// Token: 0x04000FB7 RID: 4023
				VectorData,
				// Token: 0x04000FB8 RID: 4024
				MapFields,
				// Token: 0x04000FB9 RID: 4025
				UseCustomPolygonTemplate,
				// Token: 0x04000FBA RID: 4026
				MapPolygonTemplate,
				// Token: 0x04000FBB RID: 4027
				UseCustomCenterPointTemplate,
				// Token: 0x04000FBC RID: 4028
				MapCenterPointTemplate,
				// Token: 0x04000FBD RID: 4029
				PropertyCount
			}
		}
	}
}
