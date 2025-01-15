using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000151 RID: 337
	[Obsolete("This expception is no longer being thrown by Microsoft.IdentityModel and will be removed in the next major version see: https://aka.ms/SecurityTokenUnableToValidateException", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class SecurityTokenUnableToValidateException : SecurityTokenInvalidSignatureException
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0003E00F File Offset: 0x0003C20F
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x0003E017 File Offset: 0x0003C217
		public ValidationFailure ValidationFailure { get; set; }

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003E020 File Offset: 0x0003C220
		public SecurityTokenUnableToValidateException()
		{
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003E028 File Offset: 0x0003C228
		public SecurityTokenUnableToValidateException(ValidationFailure validationFailure, string message)
			: base(message)
		{
			this.ValidationFailure = validationFailure;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003E038 File Offset: 0x0003C238
		public SecurityTokenUnableToValidateException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003E041 File Offset: 0x0003C241
		public SecurityTokenUnableToValidateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003E04C File Offset: 0x0003C24C
		protected SecurityTokenUnableToValidateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenUnableToValidateException.ValidationFailure")
				{
					this.ValidationFailure = (ValidationFailure)info.GetValue("Microsoft.IdentityModel.SecurityTokenUnableToValidateException.ValidationFailure", typeof(ValidationFailure));
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Microsoft.IdentityModel.SecurityTokenUnableToValidateException.ValidationFailure", this.ValidationFailure);
		}

		// Token: 0x04000514 RID: 1300
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenUnableToValidateException.";

		// Token: 0x04000515 RID: 1301
		[NonSerialized]
		private const string _ValidationFailureKey = "Microsoft.IdentityModel.SecurityTokenUnableToValidateException.ValidationFailure";
	}
}
