using System;
using System.Runtime.CompilerServices;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x0200006D RID: 109
	[NullableContext(1)]
	[Nullable(0)]
	internal class AzureSasCredentialSynchronousPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000AC6D File Offset: 0x00008E6D
		public AzureSasCredentialSynchronousPolicy(AzureSasCredential credential)
		{
			Argument.AssertNotNull<AzureSasCredential>(credential, "credential");
			this._credential = credential;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AC88 File Offset: 0x00008E88
		public override void OnSendingRequest(HttpMessage message)
		{
			string text = message.Request.Uri.Query;
			string text2 = this._credential.Signature;
			if (text2.StartsWith("?", StringComparison.InvariantCulture))
			{
				text2 = text2.Substring(1);
			}
			if (!text.Contains(text2))
			{
				object obj;
				if (message.TryGetProperty(typeof(AzureSasCredentialSynchronousPolicy.AzureSasSignatureHistory), out obj))
				{
					string text3 = obj as string;
					if (text3 != null && text.Contains(text3))
					{
						text = text.Replace(text3, text2);
						goto IL_0090;
					}
				}
				text = (string.IsNullOrEmpty(text) ? ("?" + text2) : (text + "&" + text2));
				IL_0090:
				message.Request.Uri.Query = text;
				message.SetProperty(typeof(AzureSasCredentialSynchronousPolicy.AzureSasSignatureHistory), text2);
				base.OnSendingRequest(message);
			}
		}

		// Token: 0x04000182 RID: 386
		private readonly AzureSasCredential _credential;

		// Token: 0x020000FA RID: 250
		[NullableContext(0)]
		private class AzureSasSignatureHistory
		{
		}
	}
}
