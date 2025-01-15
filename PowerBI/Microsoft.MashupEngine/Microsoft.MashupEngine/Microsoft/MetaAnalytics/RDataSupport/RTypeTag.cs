using System;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x02000172 RID: 370
	public struct RTypeTag
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0000BDAF File Offset: 0x00009FAF
		public RTypeTag(int tt)
		{
			this.typeTag = tt;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public static implicit operator RTypeTag(int tt)
		{
			return new RTypeTag(tt);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public RType GetRType()
		{
			return (RType)(this.typeTag & 255);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000BDCE File Offset: 0x00009FCE
		public bool GetObjectFlag()
		{
			return (this.typeTag & 256) != 0;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000BDDF File Offset: 0x00009FDF
		public bool GetAttributeFlag()
		{
			return (this.typeTag & 512) != 0;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public bool GetTagFlag()
		{
			return (this.typeTag & 1024) != 0;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000BE01 File Offset: 0x0000A001
		public int GetLength()
		{
			return this.typeTag >> 8;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000BE0B File Offset: 0x0000A00B
		public RTypeTag.REncodingOptions GetEncoding()
		{
			if ((this.typeTag & 32768) != 0)
			{
				return RTypeTag.REncodingOptions.UTF8;
			}
			return RTypeTag.REncodingOptions.Latin1;
		}

		// Token: 0x0400044C RID: 1100
		private const int lengthOffset = 8;

		// Token: 0x0400044D RID: 1101
		private const byte type = 255;

		// Token: 0x0400044E RID: 1102
		public const ushort ObjFlag = 256;

		// Token: 0x0400044F RID: 1103
		public const ushort AttrFlag = 512;

		// Token: 0x04000450 RID: 1104
		public const ushort TagFlag = 1024;

		// Token: 0x04000451 RID: 1105
		public const ushort Latin1Flag = 16384;

		// Token: 0x04000452 RID: 1106
		public const ushort UTF8Flag = 32768;

		// Token: 0x04000453 RID: 1107
		private readonly int typeTag;

		// Token: 0x02000173 RID: 371
		public enum REncodingOptions
		{
			// Token: 0x04000455 RID: 1109
			Latin1,
			// Token: 0x04000456 RID: 1110
			UTF8
		}
	}
}
