using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003136 RID: 12598
	[Serializable]
	internal class UInt32ValueRestriction : SimpleValueRestriction<uint, UInt32Value>
	{
		// Token: 0x17009972 RID: 39282
		// (get) Token: 0x0601B52A RID: 111914 RVA: 0x00002105 File Offset: 0x00000305
		protected override uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17009973 RID: 39283
		// (get) Token: 0x0601B52B RID: 111915 RVA: 0x0017811C File Offset: 0x0017631C
		protected override uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17009974 RID: 39284
		// (get) Token: 0x0601B52C RID: 111916 RVA: 0x0006808E File Offset: 0x0006628E
		// (set) Token: 0x0601B52D RID: 111917 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.UnsignedInt;
			}
			set
			{
			}
		}
	}
}
