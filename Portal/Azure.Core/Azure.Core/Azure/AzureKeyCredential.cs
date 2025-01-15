using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Azure.Core;

namespace Azure
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class AzureKeyCredential
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000231A File Offset: 0x0000051A
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002327 File Offset: 0x00000527
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Key
		{
			get
			{
				return Volatile.Read<string>(ref this._key);
			}
			private set
			{
				Volatile.Write<string>(ref this._key, value);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002335 File Offset: 0x00000535
		public AzureKeyCredential(string key)
		{
			this.Update(key);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002344 File Offset: 0x00000544
		public void Update(string key)
		{
			Argument.AssertNotNullOrEmpty(key, "key");
			this.Key = key;
		}

		// Token: 0x0400002B RID: 43
		private string _key;
	}
}
