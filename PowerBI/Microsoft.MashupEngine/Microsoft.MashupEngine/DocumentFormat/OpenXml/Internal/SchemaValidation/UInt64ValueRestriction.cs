using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003137 RID: 12599
	[Serializable]
	internal class UInt64ValueRestriction : SimpleValueRestriction<ulong, UInt64Value>
	{
		// Token: 0x17009975 RID: 39285
		// (get) Token: 0x0601B52F RID: 111919 RVA: 0x001819C2 File Offset: 0x0017FBC2
		protected override ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x17009976 RID: 39286
		// (get) Token: 0x0601B530 RID: 111920 RVA: 0x003764A2 File Offset: 0x003746A2
		protected override ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x17009977 RID: 39287
		// (get) Token: 0x0601B531 RID: 111921 RVA: 0x001422C0 File Offset: 0x001404C0
		// (set) Token: 0x0601B532 RID: 111922 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.UnsignedLong;
			}
			set
			{
			}
		}
	}
}
