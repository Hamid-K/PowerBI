using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000035 RID: 53
	internal class Body2005 : Body
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00003B80 File Offset: 0x00001D80
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00003B8E File Offset: 0x00001D8E
		[DefaultValue(1)]
		[ValidValues(1, 100)]
		public int Columns
		{
			get
			{
				return base.PropertyStore.GetInteger(3);
			}
			set
			{
				((IntProperty)DefinitionStore<Body2005, Body2005.Definition.Properties>.GetProperty(3)).Validate(this, value);
				base.PropertyStore.SetInteger(3, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00003BAF File Offset: 0x00001DAF
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00003BBD File Offset: 0x00001DBD
		public ReportSize ColumnSpacing
		{
			get
			{
				return base.PropertyStore.GetSize(4);
			}
			set
			{
				base.PropertyStore.SetSize(4, value);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00003BCC File Offset: 0x00001DCC
		public Body2005()
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public Body2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003BDD File Offset: 0x00001DDD
		public override void Initialize()
		{
			base.Initialize();
			this.Columns = 1;
		}

		// Token: 0x0200030F RID: 783
		public new class Definition : DefinitionStore<Body2005, Body2005.Definition.Properties>
		{
			// Token: 0x0600170B RID: 5899 RVA: 0x000364F2 File Offset: 0x000346F2
			private Definition()
			{
			}

			// Token: 0x02000443 RID: 1091
			public enum Properties
			{
				// Token: 0x040008AD RID: 2221
				Columns = 3,
				// Token: 0x040008AE RID: 2222
				ColumnSpacing,
				// Token: 0x040008AF RID: 2223
				PropertyCount
			}
		}
	}
}
