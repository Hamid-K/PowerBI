using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public class SecurityTokenInvalidLifetimeException : SecurityTokenValidationException
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003DC49 File Offset: 0x0003BE49
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003DC51 File Offset: 0x0003BE51
		public DateTime? NotBefore { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003DC5A File Offset: 0x0003BE5A
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0003DC62 File Offset: 0x0003BE62
		public DateTime? Expires { get; set; }

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003DC6B File Offset: 0x0003BE6B
		public SecurityTokenInvalidLifetimeException()
		{
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003DC73 File Offset: 0x0003BE73
		public SecurityTokenInvalidLifetimeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		public SecurityTokenInvalidLifetimeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003DC88 File Offset: 0x0003BE88
		protected SecurityTokenInvalidLifetimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.NotBefore"))
				{
					if (name == "Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.Expires")
					{
						this.Expires = new DateTime?((DateTime)info.GetValue("Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.Expires", typeof(DateTime)));
					}
				}
				else
				{
					this.NotBefore = new DateTime?((DateTime)info.GetValue("Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.NotBefore", typeof(DateTime)));
				}
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003DD20 File Offset: 0x0003BF20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.NotBefore != null)
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.NotBefore", this.NotBefore.Value);
			}
			if (this.Expires != null)
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.Expires", this.Expires.Value);
			}
		}

		// Token: 0x04000508 RID: 1288
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.";

		// Token: 0x04000509 RID: 1289
		[NonSerialized]
		private const string _NotBeforeKey = "Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.NotBefore";

		// Token: 0x0400050A RID: 1290
		[NonSerialized]
		private const string _ExpiresKey = "Microsoft.IdentityModel.SecurityTokenInvalidLifetimeException.Expires";
	}
}
