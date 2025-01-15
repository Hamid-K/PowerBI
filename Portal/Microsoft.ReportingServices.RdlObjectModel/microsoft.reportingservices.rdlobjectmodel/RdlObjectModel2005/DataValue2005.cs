using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000012 RID: 18
	internal class DataValue2005 : ChartDataPointValues
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002B9A File Offset: 0x00000D9A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002BAE File Offset: 0x00000DAE
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002BBE File Offset: 0x00000DBE
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public string Value
		{
			get
			{
				return (string)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public DataValue2005()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002BEA File Offset: 0x00000DEA
		public DataValue2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002FB RID: 763
		internal new class Definition : DefinitionStore<DataValue2005, DataValue2005.Definition.Properties>
		{
			// Token: 0x060016F7 RID: 5879 RVA: 0x00036452 File Offset: 0x00034652
			private Definition()
			{
			}

			// Token: 0x0200042F RID: 1071
			public enum Properties
			{
				// Token: 0x0400084C RID: 2124
				Name = 9,
				// Token: 0x0400084D RID: 2125
				Value
			}
		}
	}
}
