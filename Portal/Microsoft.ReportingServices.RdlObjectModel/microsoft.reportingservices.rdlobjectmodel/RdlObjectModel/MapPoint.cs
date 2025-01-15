using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019A RID: 410
	public class MapPoint : MapSpatialElement
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x00022370 File Offset: 0x00020570
		public MapPoint()
		{
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00022378 File Offset: 0x00020578
		internal MapPoint(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00022381 File Offset: 0x00020581
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0002238F File Offset: 0x0002058F
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseCustomPointTemplate
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

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000223A3 File Offset: 0x000205A3
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x000223B6 File Offset: 0x000205B6
		public MapPointTemplate MapPointTemplate
		{
			get
			{
				return (MapPointTemplate)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000223C5 File Offset: 0x000205C5
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003C6 RID: 966
		internal new class Definition : DefinitionStore<MapPoint, MapPoint.Definition.Properties>
		{
			// Token: 0x0600186A RID: 6250 RVA: 0x0003B6D1 File Offset: 0x000398D1
			private Definition()
			{
			}

			// Token: 0x020004DE RID: 1246
			internal enum Properties
			{
				// Token: 0x04000FBF RID: 4031
				VectorData,
				// Token: 0x04000FC0 RID: 4032
				MapFields,
				// Token: 0x04000FC1 RID: 4033
				UseCustomPointTemplate,
				// Token: 0x04000FC2 RID: 4034
				MapPointTemplate,
				// Token: 0x04000FC3 RID: 4035
				PropertyCount
			}
		}
	}
}
