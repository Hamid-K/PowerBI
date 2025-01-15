using System;
using System.Runtime.CompilerServices;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x0200006C RID: 108
	[NullableContext(1)]
	[Nullable(0)]
	internal class AzureKeyCredentialPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000ABDA File Offset: 0x00008DDA
		public AzureKeyCredentialPolicy(AzureKeyCredential credential, string name, [Nullable(2)] string prefix = null)
		{
			Argument.AssertNotNull<AzureKeyCredential>(credential, "credential");
			Argument.AssertNotNullOrEmpty(name, "name");
			this._credential = credential;
			this._name = name;
			this._prefix = prefix;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AC10 File Offset: 0x00008E10
		public override void OnSendingRequest(HttpMessage message)
		{
			base.OnSendingRequest(message);
			message.Request.Headers.SetValue(this._name, (this._prefix != null) ? (this._prefix + " " + this._credential.Key) : this._credential.Key);
		}

		// Token: 0x0400017F RID: 383
		private readonly string _name;

		// Token: 0x04000180 RID: 384
		private readonly AzureKeyCredential _credential;

		// Token: 0x04000181 RID: 385
		[Nullable(2)]
		private readonly string _prefix;
	}
}
