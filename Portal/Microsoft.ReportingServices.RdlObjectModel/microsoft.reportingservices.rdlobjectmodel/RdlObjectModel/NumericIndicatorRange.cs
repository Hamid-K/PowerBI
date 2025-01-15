using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015A RID: 346
	public class NumericIndicatorRange : ReportObject, INamedObject
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001F46F File Offset: 0x0001D66F
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0001F482 File Offset: 0x0001D682
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001F491 File Offset: 0x0001D691
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public GaugeInputValue StartValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001F4B3 File Offset: 0x0001D6B3
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0001F4C6 File Offset: 0x0001D6C6
		public GaugeInputValue EndValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001F4D5 File Offset: 0x0001D6D5
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0001F4E3 File Offset: 0x0001D6E3
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> DecimalDigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001F4F7 File Offset: 0x0001D6F7
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0001F505 File Offset: 0x0001D705
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> DigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001F519 File Offset: 0x0001D719
		public NumericIndicatorRange()
		{
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001F521 File Offset: 0x0001D721
		internal NumericIndicatorRange(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200038B RID: 907
		internal class Definition : DefinitionStore<NumericIndicatorRange, NumericIndicatorRange.Definition.Properties>
		{
			// Token: 0x0600182E RID: 6190 RVA: 0x0003B49B File Offset: 0x0003969B
			private Definition()
			{
			}

			// Token: 0x020004A4 RID: 1188
			internal enum Properties
			{
				// Token: 0x04000D19 RID: 3353
				Name,
				// Token: 0x04000D1A RID: 3354
				StartValue,
				// Token: 0x04000D1B RID: 3355
				EndValue,
				// Token: 0x04000D1C RID: 3356
				DecimalDigitColor,
				// Token: 0x04000D1D RID: 3357
				DigitColor,
				// Token: 0x04000D1E RID: 3358
				PropertyCount
			}
		}
	}
}
