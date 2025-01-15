using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000120 RID: 288
	public sealed class ConfigStoreEntry
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
		public ConfigStoreEntry(string key, byte[] value, long version)
		{
			this._key = key;
			this._value = value;
			this._version = version;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001E115 File Offset: 0x0001C315
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001E11D File Offset: 0x0001C31D
		public long Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001E125 File Offset: 0x0001C325
		internal byte[] Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000675 RID: 1653
		private string _key;

		// Token: 0x04000676 RID: 1654
		private byte[] _value;

		// Token: 0x04000677 RID: 1655
		private long _version;
	}
}
