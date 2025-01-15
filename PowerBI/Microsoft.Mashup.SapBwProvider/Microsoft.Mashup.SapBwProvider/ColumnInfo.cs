using System;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000A RID: 10
	public struct ColumnInfo
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000038F4 File Offset: 0x00001AF4
		public ColumnInfo(SapBwDataType dataType, string keyName = null, int? precision = null)
		{
			this.dataType = dataType;
			this.keyName = (string.IsNullOrEmpty(keyName) ? null : keyName);
			this.precision = precision;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003916 File Offset: 0x00001B16
		public SapBwDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000391E File Offset: 0x00001B1E
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003926 File Offset: 0x00001B26
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x04000015 RID: 21
		private readonly SapBwDataType dataType;

		// Token: 0x04000016 RID: 22
		private readonly string keyName;

		// Token: 0x04000017 RID: 23
		private readonly int? precision;
	}
}
