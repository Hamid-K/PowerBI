using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000F RID: 15
	internal class Marker2005 : ChartMarker
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002AC7 File Offset: 0x00000CC7
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002AD5 File Offset: 0x00000CD5
		[DefaultValueConstant("DefaultZeroSize")]
		public new ReportSize Size
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002AE4 File Offset: 0x00000CE4
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public new EmptyColorStyle2005 Style
		{
			get
			{
				return (EmptyColorStyle2005)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002B06 File Offset: 0x00000D06
		public Marker2005()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002B0E File Offset: 0x00000D0E
		public Marker2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F8 RID: 760
		internal new class Definition : DefinitionStore<Marker2005, Marker2005.Definition.Properties>
		{
			// Token: 0x060016F4 RID: 5876 RVA: 0x0003643A File Offset: 0x0003463A
			private Definition()
			{
			}

			// Token: 0x0200042C RID: 1068
			public enum Properties
			{
				// Token: 0x04000843 RID: 2115
				Type = 3,
				// Token: 0x04000844 RID: 2116
				Size,
				// Token: 0x04000845 RID: 2117
				Style
			}
		}
	}
}
