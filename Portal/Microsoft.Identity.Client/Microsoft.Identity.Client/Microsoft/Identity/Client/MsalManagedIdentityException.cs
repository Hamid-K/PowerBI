using System;
using System.ComponentModel;
using Microsoft.Identity.Client.ManagedIdentity;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200016F RID: 367
	[Obsolete("MsalManagedIdentityException is deprecated and will be removed in a future release. Catch MsalServiceException instead.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class MsalManagedIdentityException : MsalServiceException
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x0003E15C File Offset: 0x0003C35C
		public MsalManagedIdentityException(string errorCode, string errorMessage, ManagedIdentitySource source)
			: this(errorCode, errorMessage, null, source)
		{
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003E168 File Offset: 0x0003C368
		public MsalManagedIdentityException(string errorCode, string errorMessage, ManagedIdentitySource source, int statusCode)
			: this(errorCode, errorMessage, null, source, statusCode)
		{
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003E176 File Offset: 0x0003C376
		public MsalManagedIdentityException(string errorCode, string errorMessage, Exception innerException, ManagedIdentitySource source, int statusCode)
			: this(errorCode, errorMessage, innerException, source)
		{
			base.StatusCode = statusCode;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0003E18B File Offset: 0x0003C38B
		public MsalManagedIdentityException(string errorCode, string errorMessage, Exception innerException, ManagedIdentitySource source)
			: base(errorCode, errorMessage, innerException)
		{
			this.ManagedIdentitySource = source;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0003E19E File Offset: 0x0003C39E
		public new ManagedIdentitySource ManagedIdentitySource { get; }

		// Token: 0x06001229 RID: 4649 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
		protected override void UpdateIsRetryable()
		{
			int statusCode = base.StatusCode;
			if (statusCode <= 408)
			{
				if (statusCode != 404 && statusCode != 408)
				{
					goto IL_0043;
				}
			}
			else if (statusCode != 429 && statusCode != 500 && statusCode - 503 > 1)
			{
				goto IL_0043;
			}
			base.IsRetryable = true;
			return;
			IL_0043:
			base.IsRetryable = false;
		}
	}
}
