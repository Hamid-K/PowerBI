using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019F RID: 415
	public class MapLocation : ReportObject
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x000227D3 File Offset: 0x000209D3
		public MapLocation()
		{
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000227DB File Offset: 0x000209DB
		internal MapLocation(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x000227E4 File Offset: 0x000209E4
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x000227F2 File Offset: 0x000209F2
		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Left
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

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00022806 File Offset: 0x00020A06
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00022814 File Offset: 0x00020A14
		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Top
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00022828 File Offset: 0x00020A28
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00022836 File Offset: 0x00020A36
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

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002284A File Offset: 0x00020A4A
		public override void Initialize()
		{
			base.Initialize();
			this.Left = 0.0;
			this.Top = 0.0;
			this.Unit = MapUnits.Percentage;
		}

		// Token: 0x020003CB RID: 971
		internal class Definition : DefinitionStore<MapLocation, MapLocation.Definition.Properties>
		{
			// Token: 0x0600186F RID: 6255 RVA: 0x0003B6F9 File Offset: 0x000398F9
			private Definition()
			{
			}

			// Token: 0x020004E3 RID: 1251
			internal enum Properties
			{
				// Token: 0x04000FF8 RID: 4088
				Left,
				// Token: 0x04000FF9 RID: 4089
				Top,
				// Token: 0x04000FFA RID: 4090
				Unit,
				// Token: 0x04000FFB RID: 4091
				PropertyCount
			}
		}
	}
}
