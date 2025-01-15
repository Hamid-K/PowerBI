using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003139 RID: 12601
	[Serializable]
	internal class DoubleValueRestriction : SimpleValueRestriction<double, DoubleValue>
	{
		// Token: 0x1700997B RID: 39291
		// (get) Token: 0x0601B53A RID: 111930 RVA: 0x003764C4 File Offset: 0x003746C4
		protected override double MinValue
		{
			get
			{
				return double.MinValue;
			}
		}

		// Token: 0x1700997C RID: 39292
		// (get) Token: 0x0601B53B RID: 111931 RVA: 0x003764CF File Offset: 0x003746CF
		protected override double MaxValue
		{
			get
			{
				return double.MaxValue;
			}
		}

		// Token: 0x1700997D RID: 39293
		// (get) Token: 0x0601B53C RID: 111932 RVA: 0x002436D1 File Offset: 0x002418D1
		// (set) Token: 0x0601B53D RID: 111933 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Double;
			}
			set
			{
			}
		}

		// Token: 0x0601B53E RID: 111934 RVA: 0x00375E7C File Offset: 0x0037407C
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			return attributeValue.HasValue;
		}
	}
}
