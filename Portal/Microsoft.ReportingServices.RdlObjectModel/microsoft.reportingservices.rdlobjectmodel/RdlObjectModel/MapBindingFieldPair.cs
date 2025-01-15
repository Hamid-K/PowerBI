using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A4 RID: 420
	public class MapBindingFieldPair : ReportObject
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x00022AAF File Offset: 0x00020CAF
		public MapBindingFieldPair()
		{
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00022AB7 File Offset: 0x00020CB7
		internal MapBindingFieldPair(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00022AC0 File Offset: 0x00020CC0
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00022ACE File Offset: 0x00020CCE
		public ReportExpression FieldName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00022AE2 File Offset: 0x00020CE2
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00022AF0 File Offset: 0x00020CF0
		public ReportExpression BindingExpression
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

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00022B04 File Offset: 0x00020D04
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D0 RID: 976
		internal class Definition : DefinitionStore<MapBindingFieldPair, MapBindingFieldPair.Definition.Properties>
		{
			// Token: 0x06001874 RID: 6260 RVA: 0x0003B721 File Offset: 0x00039921
			private Definition()
			{
			}

			// Token: 0x020004E8 RID: 1256
			internal enum Properties
			{
				// Token: 0x04001011 RID: 4113
				FieldName,
				// Token: 0x04001012 RID: 4114
				BindingExpression,
				// Token: 0x04001013 RID: 4115
				PropertyCount
			}
		}
	}
}
