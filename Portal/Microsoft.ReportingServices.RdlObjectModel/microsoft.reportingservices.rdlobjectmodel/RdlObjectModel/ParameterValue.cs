using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001EC RID: 492
	public class ParameterValue : ReportObject
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002671E File Offset: 0x0002491E
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0002672C File Offset: 0x0002492C
		[DefaultValue(null)]
		public ReportExpression? Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression?>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00026740 File Offset: 0x00024940
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x0002674E File Offset: 0x0002494E
		[ReportExpressionDefaultValue]
		public ReportExpression Label
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

		// Token: 0x0600105A RID: 4186 RVA: 0x00026762 File Offset: 0x00024962
		public ParameterValue()
		{
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002676A File Offset: 0x0002496A
		internal ParameterValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003F7 RID: 1015
		internal class Definition : DefinitionStore<ParameterValue, ParameterValue.Definition.Properties>
		{
			// Token: 0x060018B9 RID: 6329 RVA: 0x0003BB87 File Offset: 0x00039D87
			private Definition()
			{
			}

			// Token: 0x02000509 RID: 1289
			internal enum Properties
			{
				// Token: 0x040010D2 RID: 4306
				Value,
				// Token: 0x040010D3 RID: 4307
				Label,
				// Token: 0x040010D4 RID: 4308
				LabelLocID
			}
		}
	}
}
