using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A1 RID: 417
	public class MapLimits : ReportObject
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x000228E3 File Offset: 0x00020AE3
		public MapLimits()
		{
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000228EB File Offset: 0x00020AEB
		internal MapLimits(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000228F4 File Offset: 0x00020AF4
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00022902 File Offset: 0x00020B02
		public ReportExpression<double> MinimumX
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00022916 File Offset: 0x00020B16
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00022924 File Offset: 0x00020B24
		public ReportExpression<double> MinimumY
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

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00022938 File Offset: 0x00020B38
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00022946 File Offset: 0x00020B46
		public ReportExpression<double> MaximumX
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

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0002295A File Offset: 0x00020B5A
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00022968 File Offset: 0x00020B68
		public ReportExpression<double> MaximumY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002297C File Offset: 0x00020B7C
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003CD RID: 973
		internal class Definition : DefinitionStore<MapLimits, MapLimits.Definition.Properties>
		{
			// Token: 0x06001871 RID: 6257 RVA: 0x0003B709 File Offset: 0x00039909
			private Definition()
			{
			}

			// Token: 0x020004E5 RID: 1253
			internal enum Properties
			{
				// Token: 0x04001001 RID: 4097
				MinimumX,
				// Token: 0x04001002 RID: 4098
				MinimumY,
				// Token: 0x04001003 RID: 4099
				MaximumX,
				// Token: 0x04001004 RID: 4100
				MaximumY,
				// Token: 0x04001005 RID: 4101
				PropertyCount
			}
		}
	}
}
