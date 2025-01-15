using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	public class SecurityTokenInvalidIssuerException : SecurityTokenValidationException
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003DBA9 File Offset: 0x0003BDA9
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0003DBB1 File Offset: 0x0003BDB1
		public string InvalidIssuer { get; set; }

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003DBBA File Offset: 0x0003BDBA
		public SecurityTokenInvalidIssuerException()
		{
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003DBC2 File Offset: 0x0003BDC2
		public SecurityTokenInvalidIssuerException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003DBCB File Offset: 0x0003BDCB
		public SecurityTokenInvalidIssuerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		protected SecurityTokenInvalidIssuerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenInvalidIssuerException.InvalidIssuer")
				{
					this.InvalidIssuer = info.GetString("Microsoft.IdentityModel.SecurityTokenInvalidIssuerException.InvalidIssuer");
				}
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003DC21 File Offset: 0x0003BE21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (!string.IsNullOrEmpty(this.InvalidIssuer))
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidIssuerException.InvalidIssuer", this.InvalidIssuer);
			}
		}

		// Token: 0x04000505 RID: 1285
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenInvalidIssuerException.";

		// Token: 0x04000506 RID: 1286
		[NonSerialized]
		private const string _InvalidIssuerKey = "Microsoft.IdentityModel.SecurityTokenInvalidIssuerException.InvalidIssuer";
	}
}
