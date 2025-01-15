using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	public class AzureSasCredential
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000023DA File Offset: 0x000005DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Signature
		{
			get
			{
				return Volatile.Read<string>(ref this._signature);
			}
			private set
			{
				Volatile.Write<string>(ref this._signature, value);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000023E8 File Offset: 0x000005E8
		public AzureSasCredential(string signature)
		{
			Argument.AssertNotNullOrWhiteSpace(signature, "signature");
			this.Signature = signature;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002402 File Offset: 0x00000602
		public void Update(string signature)
		{
			Argument.AssertNotNullOrWhiteSpace(signature, "signature");
			this.Signature = signature;
		}

		// Token: 0x0400002D RID: 45
		private string _signature;
	}
}
