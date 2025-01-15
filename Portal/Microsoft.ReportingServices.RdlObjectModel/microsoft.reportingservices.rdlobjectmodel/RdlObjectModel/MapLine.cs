using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019B RID: 411
	public class MapLine : MapSpatialElement
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x000223CD File Offset: 0x000205CD
		public MapLine()
		{
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000223D5 File Offset: 0x000205D5
		internal MapLine(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000223DE File Offset: 0x000205DE
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000223EC File Offset: 0x000205EC
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseCustomLineTemplate
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00022400 File Offset: 0x00020600
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00022413 File Offset: 0x00020613
		public MapLineTemplate MapLineTemplate
		{
			get
			{
				return (MapLineTemplate)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00022422 File Offset: 0x00020622
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003C7 RID: 967
		internal new class Definition : DefinitionStore<MapLine, MapLine.Definition.Properties>
		{
			// Token: 0x0600186B RID: 6251 RVA: 0x0003B6D9 File Offset: 0x000398D9
			private Definition()
			{
			}

			// Token: 0x020004DF RID: 1247
			internal enum Properties
			{
				// Token: 0x04000FC5 RID: 4037
				VectorData,
				// Token: 0x04000FC6 RID: 4038
				MapFields,
				// Token: 0x04000FC7 RID: 4039
				UseCustomLineTemplate,
				// Token: 0x04000FC8 RID: 4040
				MapLineTemplate,
				// Token: 0x04000FC9 RID: 4041
				PropertyCount
			}
		}
	}
}
