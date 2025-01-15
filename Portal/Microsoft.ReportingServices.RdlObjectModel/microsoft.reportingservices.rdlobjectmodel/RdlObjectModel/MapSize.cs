using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019E RID: 414
	public class MapSize : ReportObject
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x00022748 File Offset: 0x00020948
		public MapSize()
		{
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00022750 File Offset: 0x00020950
		internal MapSize(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00022759 File Offset: 0x00020959
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00022767 File Offset: 0x00020967
		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0002277B File Offset: 0x0002097B
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00022789 File Offset: 0x00020989
		public ReportExpression<double> Height
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002279D File Offset: 0x0002099D
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x000227AB File Offset: 0x000209AB
		[ReportExpressionDefaultValue(typeof(MapUnits), MapUnits.Percentage)]
		public ReportExpression<MapUnits> Unit
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapUnits>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000227BF File Offset: 0x000209BF
		public override void Initialize()
		{
			base.Initialize();
			this.Unit = MapUnits.Percentage;
		}

		// Token: 0x020003CA RID: 970
		internal class Definition : DefinitionStore<MapSize, MapSize.Definition.Properties>
		{
			// Token: 0x0600186E RID: 6254 RVA: 0x0003B6F1 File Offset: 0x000398F1
			private Definition()
			{
			}

			// Token: 0x020004E2 RID: 1250
			internal enum Properties
			{
				// Token: 0x04000FF3 RID: 4083
				Width,
				// Token: 0x04000FF4 RID: 4084
				Height,
				// Token: 0x04000FF5 RID: 4085
				Unit,
				// Token: 0x04000FF6 RID: 4086
				PropertyCount
			}
		}
	}
}
