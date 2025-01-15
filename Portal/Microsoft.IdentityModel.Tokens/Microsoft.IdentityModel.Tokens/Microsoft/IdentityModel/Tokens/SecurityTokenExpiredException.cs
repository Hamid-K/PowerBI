using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class SecurityTokenExpiredException : SecurityTokenValidationException
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003D9C5 File Offset: 0x0003BBC5
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0003D9CD File Offset: 0x0003BBCD
		public DateTime Expires { get; set; }

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003D9D6 File Offset: 0x0003BBD6
		public SecurityTokenExpiredException()
			: base("SecurityToken has Expired")
		{
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003D9E3 File Offset: 0x0003BBE3
		public SecurityTokenExpiredException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003D9EC File Offset: 0x0003BBEC
		public SecurityTokenExpiredException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003D9F8 File Offset: 0x0003BBF8
		protected SecurityTokenExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenExpiredException.Expires")
				{
					this.Expires = (DateTime)info.GetValue("Microsoft.IdentityModel.SecurityTokenExpiredException.Expires", typeof(DateTime));
				}
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003DA50 File Offset: 0x0003BC50
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Microsoft.IdentityModel.SecurityTokenExpiredException.Expires", this.Expires);
		}

		// Token: 0x040004FC RID: 1276
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenExpiredException.";

		// Token: 0x040004FD RID: 1277
		[NonSerialized]
		private const string _ExpiresKey = "Microsoft.IdentityModel.SecurityTokenExpiredException.Expires";
	}
}
