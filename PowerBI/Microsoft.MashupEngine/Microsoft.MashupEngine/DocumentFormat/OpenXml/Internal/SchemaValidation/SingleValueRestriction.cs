using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003138 RID: 12600
	[Serializable]
	internal class SingleValueRestriction : SimpleValueRestriction<float, SingleValue>
	{
		// Token: 0x17009978 RID: 39288
		// (get) Token: 0x0601B534 RID: 111924 RVA: 0x003764AE File Offset: 0x003746AE
		protected override float MinValue
		{
			get
			{
				return float.MinValue;
			}
		}

		// Token: 0x17009979 RID: 39289
		// (get) Token: 0x0601B535 RID: 111925 RVA: 0x003764B5 File Offset: 0x003746B5
		protected override float MaxValue
		{
			get
			{
				return float.MaxValue;
			}
		}

		// Token: 0x1700997A RID: 39290
		// (get) Token: 0x0601B536 RID: 111926 RVA: 0x002435AE File Offset: 0x002417AE
		// (set) Token: 0x0601B537 RID: 111927 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Float;
			}
			set
			{
			}
		}

		// Token: 0x0601B538 RID: 111928 RVA: 0x00375E7C File Offset: 0x0037407C
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			return attributeValue.HasValue;
		}
	}
}
