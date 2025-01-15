using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003130 RID: 12592
	[Serializable]
	internal class ByteValueRestriction : SimpleValueRestriction<byte, ByteValue>
	{
		// Token: 0x17009960 RID: 39264
		// (get) Token: 0x0601B50C RID: 111884 RVA: 0x00002105 File Offset: 0x00000305
		protected override byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17009961 RID: 39265
		// (get) Token: 0x0601B50D RID: 111885 RVA: 0x003736D0 File Offset: 0x003718D0
		protected override byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x17009962 RID: 39266
		// (get) Token: 0x0601B50E RID: 111886 RVA: 0x0012AF0D File Offset: 0x0012910D
		// (set) Token: 0x0601B50F RID: 111887 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.UnsignedByte;
			}
			set
			{
			}
		}
	}
}
