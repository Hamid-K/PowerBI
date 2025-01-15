using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public class AzureNamedKeyCredential
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002358 File Offset: 0x00000558
		public string Name
		{
			get
			{
				return Volatile.Read<Tuple<string, string>>(ref this._namedKey).Item1;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000236A File Offset: 0x0000056A
		public AzureNamedKeyCredential(string name, string key)
		{
			this.Update(name, key);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000237A File Offset: 0x0000057A
		public void Update(string name, string key)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			Argument.AssertNotNullOrEmpty(key, "key");
			Volatile.Write<Tuple<string, string>>(ref this._namedKey, Tuple.Create<string, string>(name, key));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000023A4 File Offset: 0x000005A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out string name, out string key)
		{
			Tuple<string, string> tuple = Volatile.Read<Tuple<string, string>>(ref this._namedKey);
			name = tuple.Item1;
			key = tuple.Item2;
		}

		// Token: 0x0400002C RID: 44
		private Tuple<string, string> _namedKey;
	}
}
