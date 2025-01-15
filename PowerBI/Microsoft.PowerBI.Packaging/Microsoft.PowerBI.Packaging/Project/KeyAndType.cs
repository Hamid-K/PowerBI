using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200007A RID: 122
	public struct KeyAndType
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000A382 File Offset: 0x00008582
		public string Key { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000A38A File Offset: 0x0000858A
		public JTokenType KeyType { get; }

		// Token: 0x0600038B RID: 907 RVA: 0x0000A392 File Offset: 0x00008592
		public KeyAndType(string key, JTokenType type)
		{
			this.Key = key;
			this.KeyType = type;
		}
	}
}
