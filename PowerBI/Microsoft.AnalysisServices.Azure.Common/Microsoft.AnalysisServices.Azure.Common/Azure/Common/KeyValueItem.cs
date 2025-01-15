using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005A RID: 90
	public sealed class KeyValueItem
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0001007C File Offset: 0x0000E27C
		public KeyValueItem(string key, byte[] value)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(key, "Key cannot be empty or null");
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001009D File Offset: 0x0000E29D
		public KeyValueItem(IPersistable item)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(item.Key, "Key cannot be empty or null");
			this.Key = item.Key;
			this.Value = item.Serialize();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000100CD File Offset: 0x0000E2CD
		public KeyValueItem(string key)
			: this(key, null)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000100D7 File Offset: 0x0000E2D7
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000100DF File Offset: 0x0000E2DF
		public string Key { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000100E8 File Offset: 0x0000E2E8
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public byte[] Value { get; set; }
	}
}
