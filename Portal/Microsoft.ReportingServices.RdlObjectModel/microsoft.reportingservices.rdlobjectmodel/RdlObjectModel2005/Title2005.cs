using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000C RID: 12
	internal class Title2005 : ChartTitle
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002913 File Offset: 0x00000B13
		[ReportExpressionDefaultValue]
		public new ReportExpression Caption
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000293C File Offset: 0x00000B3C
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000294C File Offset: 0x00000B4C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000295B File Offset: 0x00000B5B
		[DefaultValue(TitlePositions2005.Center)]
		public new TitlePositions2005 Position
		{
			get
			{
				return (TitlePositions2005)base.PropertyStore.GetInteger(16);
			}
			set
			{
				base.PropertyStore.SetInteger(16, (int)value);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000296B File Offset: 0x00000B6B
		public Title2005()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002973 File Offset: 0x00000B73
		public Title2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F5 RID: 757
		internal new class Definition : DefinitionStore<Title2005, Title2005.Definition.Properties>
		{
			// Token: 0x060016F1 RID: 5873 RVA: 0x00036422 File Offset: 0x00034622
			private Definition()
			{
			}

			// Token: 0x02000429 RID: 1065
			public enum Properties
			{
				// Token: 0x04000833 RID: 2099
				Caption = 14,
				// Token: 0x04000834 RID: 2100
				Style,
				// Token: 0x04000835 RID: 2101
				Position
			}
		}
	}
}
