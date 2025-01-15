using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000174 RID: 372
	public class MsalUiRequiredException : MsalServiceException
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x0003E92A File Offset: 0x0003CB2A
		public MsalUiRequiredException(string errorCode, string errorMessage)
			: this(errorCode, errorMessage, null)
		{
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0003E935 File Offset: 0x0003CB35
		public MsalUiRequiredException(string errorCode, string errorMessage, Exception innerException)
			: this(errorCode, errorMessage, innerException, UiRequiredExceptionClassification.None)
		{
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0003E941 File Offset: 0x0003CB41
		public MsalUiRequiredException(string errorCode, string errorMessage, Exception innerException, UiRequiredExceptionClassification classification)
			: base(errorCode, errorMessage, innerException)
		{
			this._classification = classification;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0003E954 File Offset: 0x0003CB54
		public UiRequiredExceptionClassification Classification
		{
			get
			{
				if (string.Equals(base.SubError, "basic_action", StringComparison.OrdinalIgnoreCase))
				{
					return UiRequiredExceptionClassification.BasicAction;
				}
				if (string.Equals(base.SubError, "additional_action", StringComparison.OrdinalIgnoreCase))
				{
					return UiRequiredExceptionClassification.AdditionalAction;
				}
				if (string.Equals(base.SubError, "message_only", StringComparison.OrdinalIgnoreCase))
				{
					return UiRequiredExceptionClassification.MessageOnly;
				}
				if (string.Equals(base.SubError, "consent_required", StringComparison.OrdinalIgnoreCase))
				{
					return UiRequiredExceptionClassification.ConsentRequired;
				}
				if (string.Equals(base.SubError, "user_password_expired", StringComparison.OrdinalIgnoreCase))
				{
					return UiRequiredExceptionClassification.UserPasswordExpired;
				}
				return this._classification;
			}
		}

		// Token: 0x040006C5 RID: 1733
		private readonly UiRequiredExceptionClassification _classification;
	}
}
