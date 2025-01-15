using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AC RID: 428
	public class MapGridLines : ReportObject
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x00022E52 File Offset: 0x00021052
		public MapGridLines()
		{
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00022E5A File Offset: 0x0002105A
		internal MapGridLines(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00022E63 File Offset: 0x00021063
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00022E76 File Offset: 0x00021076
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

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00022E85 File Offset: 0x00021085
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00022E93 File Offset: 0x00021093
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00022EA7 File Offset: 0x000210A7
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00022EB5 File Offset: 0x000210B5
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00022EC9 File Offset: 0x000210C9
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00022ED7 File Offset: 0x000210D7
		public ReportExpression<bool> ShowLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00022EEB File Offset: 0x000210EB
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00022EF9 File Offset: 0x000210F9
		[ReportExpressionDefaultValue(typeof(MapLabelPositions), MapLabelPositions.Near)]
		public ReportExpression<MapLabelPositions> LabelPosition
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLabelPositions>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00022F0D File Offset: 0x0002110D
		public override void Initialize()
		{
			base.Initialize();
			this.LabelPosition = MapLabelPositions.Near;
		}

		// Token: 0x020003D8 RID: 984
		internal class Definition : DefinitionStore<MapGridLines, MapGridLines.Definition.Properties>
		{
			// Token: 0x0600187C RID: 6268 RVA: 0x0003B761 File Offset: 0x00039961
			private Definition()
			{
			}

			// Token: 0x020004F0 RID: 1264
			internal enum Properties
			{
				// Token: 0x04001035 RID: 4149
				Style,
				// Token: 0x04001036 RID: 4150
				Hidden,
				// Token: 0x04001037 RID: 4151
				Interval,
				// Token: 0x04001038 RID: 4152
				ShowLabels,
				// Token: 0x04001039 RID: 4153
				LabelPosition,
				// Token: 0x0400103A RID: 4154
				PropertyCount
			}
		}
	}
}
