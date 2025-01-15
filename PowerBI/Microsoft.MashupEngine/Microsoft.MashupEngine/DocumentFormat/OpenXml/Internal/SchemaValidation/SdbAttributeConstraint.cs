using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310D RID: 12557
	internal class SdbAttributeConstraint : SdbData
	{
		// Token: 0x170098BD RID: 39101
		// (get) Token: 0x0601B3BE RID: 111550 RVA: 0x00373413 File Offset: 0x00371613
		// (set) Token: 0x0601B3BF RID: 111551 RVA: 0x0037341B File Offset: 0x0037161B
		public XsdAttributeUse AttributeUse { get; set; }

		// Token: 0x170098BE RID: 39102
		// (get) Token: 0x0601B3C0 RID: 111552 RVA: 0x00373424 File Offset: 0x00371624
		// (set) Token: 0x0601B3C1 RID: 111553 RVA: 0x0037342C File Offset: 0x0037162C
		public ushort SimpleTypeIndex { get; set; }

		// Token: 0x170098BF RID: 39103
		// (get) Token: 0x0601B3C2 RID: 111554 RVA: 0x00373435 File Offset: 0x00371635
		// (set) Token: 0x0601B3C3 RID: 111555 RVA: 0x0037343D File Offset: 0x0037163D
		public byte FileFormatVersion { get; set; }

		// Token: 0x0601B3C4 RID: 111556 RVA: 0x0036FD3E File Offset: 0x0036DF3E
		public SdbAttributeConstraint()
		{
		}

		// Token: 0x0601B3C5 RID: 111557 RVA: 0x00373446 File Offset: 0x00371646
		public SdbAttributeConstraint(XsdAttributeUse xsdAttributeUse, ushort simpleTypeIndex, byte fileFormatVersion)
		{
			this.AttributeUse = xsdAttributeUse;
			this.SimpleTypeIndex = simpleTypeIndex;
			this.FileFormatVersion = fileFormatVersion;
		}

		// Token: 0x170098C0 RID: 39104
		// (get) Token: 0x0601B3C6 RID: 111558 RVA: 0x0000244F File Offset: 0x0000064F
		public static int TypeSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170098C1 RID: 39105
		// (get) Token: 0x0601B3C7 RID: 111559 RVA: 0x00373463 File Offset: 0x00371663
		public override int DataSize
		{
			get
			{
				return SdbAttributeConstraint.TypeSize;
			}
		}

		// Token: 0x0601B3C8 RID: 111560 RVA: 0x0037346C File Offset: 0x0037166C
		public override byte[] GetBytes()
		{
			return base.GetBytes(new byte[][]
			{
				this.AttributeUse.Bytes(),
				this.SimpleTypeIndex.Bytes(),
				this.FileFormatVersion.Bytes()
			});
		}

		// Token: 0x0601B3C9 RID: 111561 RVA: 0x003734B1 File Offset: 0x003716B1
		public override void LoadFromBytes(byte[] value, int startIndex)
		{
			this.AttributeUse = (XsdAttributeUse)SdbData.LoadByte(value, ref startIndex);
			this.SimpleTypeIndex = SdbData.LoadSdbIndex(value, ref startIndex);
			this.FileFormatVersion = SdbData.LoadByte(value, ref startIndex);
		}
	}
}
