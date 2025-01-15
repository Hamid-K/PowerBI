using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000191 RID: 401
	public abstract class MapDockableSubItem : MapSubItem
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x00021976 File Offset: 0x0001FB76
		public MapDockableSubItem()
		{
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002197E File Offset: 0x0001FB7E
		internal MapDockableSubItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00021987 File Offset: 0x0001FB87
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x0002199A File Offset: 0x0001FB9A
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000219A9 File Offset: 0x0001FBA9
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x000219B8 File Offset: 0x0001FBB8
		[ReportExpressionDefaultValue(typeof(MapPositions), MapPositions.TopCenter)]
		public ReportExpression<MapPositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapPositions>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000219CD File Offset: 0x0001FBCD
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x000219DC File Offset: 0x0001FBDC
		public ReportExpression<bool> DockOutsideViewport
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x000219F1 File Offset: 0x0001FBF1
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00021A00 File Offset: 0x0001FC00
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00021A15 File Offset: 0x0001FC15
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00021A24 File Offset: 0x0001FC24
		[ReportExpressionDefaultValue("")]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00021A39 File Offset: 0x0001FC39
		public override void Initialize()
		{
			base.Initialize();
			this.Position = MapPositions.TopCenter;
		}

		// Token: 0x020003BE RID: 958
		internal new class Definition : DefinitionStore<MapDockableSubItem, MapDockableSubItem.Definition.Properties>
		{
			// Token: 0x06001862 RID: 6242 RVA: 0x0003B691 File Offset: 0x00039891
			private Definition()
			{
			}

			// Token: 0x020004D6 RID: 1238
			internal enum Properties
			{
				// Token: 0x04000F3A RID: 3898
				Style,
				// Token: 0x04000F3B RID: 3899
				MapLocation,
				// Token: 0x04000F3C RID: 3900
				MapSize,
				// Token: 0x04000F3D RID: 3901
				LeftMargin,
				// Token: 0x04000F3E RID: 3902
				RightMargin,
				// Token: 0x04000F3F RID: 3903
				TopMargin,
				// Token: 0x04000F40 RID: 3904
				BottomMargin,
				// Token: 0x04000F41 RID: 3905
				ZIndex,
				// Token: 0x04000F42 RID: 3906
				ActionInfo,
				// Token: 0x04000F43 RID: 3907
				Position,
				// Token: 0x04000F44 RID: 3908
				DockOutsideViewport,
				// Token: 0x04000F45 RID: 3909
				Hidden,
				// Token: 0x04000F46 RID: 3910
				ToolTip
			}
		}
	}
}
