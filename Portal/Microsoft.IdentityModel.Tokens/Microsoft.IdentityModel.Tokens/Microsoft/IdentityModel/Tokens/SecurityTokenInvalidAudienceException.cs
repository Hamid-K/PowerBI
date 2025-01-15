using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	public class SecurityTokenInvalidAudienceException : SecurityTokenValidationException
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003DB09 File Offset: 0x0003BD09
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0003DB11 File Offset: 0x0003BD11
		public string InvalidAudience { get; set; }

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003DB1A File Offset: 0x0003BD1A
		public SecurityTokenInvalidAudienceException()
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003DB22 File Offset: 0x0003BD22
		public SecurityTokenInvalidAudienceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003DB2B File Offset: 0x0003BD2B
		public SecurityTokenInvalidAudienceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003DB38 File Offset: 0x0003BD38
		protected SecurityTokenInvalidAudienceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenInvalidAudienceException.InvalidAudience")
				{
					this.InvalidAudience = info.GetString("Microsoft.IdentityModel.SecurityTokenInvalidAudienceException.InvalidAudience");
				}
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003DB81 File Offset: 0x0003BD81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (!string.IsNullOrEmpty(this.InvalidAudience))
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidAudienceException.InvalidAudience", this.InvalidAudience);
			}
		}

		// Token: 0x04000502 RID: 1282
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenInvalidAudienceException.";

		// Token: 0x04000503 RID: 1283
		[NonSerialized]
		private const string _InvalidAudienceKey = "Microsoft.IdentityModel.SecurityTokenInvalidAudienceException.InvalidAudience";
	}
}
