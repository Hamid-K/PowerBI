using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000E RID: 14
	internal class DataLabel2005 : ChartDataLabel
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002A0E File Offset: 0x00000C0E
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002A22 File Offset: 0x00000C22
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002A32 File Offset: 0x00000C32
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002A41 File Offset: 0x00000C41
		[ReportExpressionDefaultValue]
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002A56 File Offset: 0x00000C56
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002A65 File Offset: 0x00000C65
		[DefaultValue(false)]
		public new bool Visible
		{
			get
			{
				return base.PropertyStore.GetBoolean(11);
			}
			set
			{
				base.PropertyStore.SetBoolean(11, value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002A75 File Offset: 0x00000C75
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002A84 File Offset: 0x00000C84
		[DefaultValue(0)]
		public new int Rotation
		{
			get
			{
				return base.PropertyStore.GetInteger(13);
			}
			set
			{
				base.PropertyStore.SetInteger(13, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002A94 File Offset: 0x00000C94
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002AA7 File Offset: 0x00000CA7
		[XmlChildAttribute("Value", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string ValueLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public DataLabel2005()
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002ABE File Offset: 0x00000CBE
		public DataLabel2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F7 RID: 759
		internal new class Definition : DefinitionStore<DataLabel2005, DataLabel2005.Definition.Properties>
		{
			// Token: 0x060016F3 RID: 5875 RVA: 0x00036432 File Offset: 0x00034632
			private Definition()
			{
			}

			// Token: 0x0200042B RID: 1067
			public enum Properties
			{
				// Token: 0x0400083D RID: 2109
				Style = 9,
				// Token: 0x0400083E RID: 2110
				Value,
				// Token: 0x0400083F RID: 2111
				Visible,
				// Token: 0x04000840 RID: 2112
				Position,
				// Token: 0x04000841 RID: 2113
				Rotation
			}
		}
	}
}
