using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class SecurityTokenNotYetValidException : SecurityTokenValidationException
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		public DateTime NotBefore { get; set; }

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003DF05 File Offset: 0x0003C105
		public SecurityTokenNotYetValidException()
			: base("SecurityToken is not yet valid")
		{
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003DF12 File Offset: 0x0003C112
		public SecurityTokenNotYetValidException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003DF1B File Offset: 0x0003C11B
		public SecurityTokenNotYetValidException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003DF28 File Offset: 0x0003C128
		protected SecurityTokenNotYetValidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenNotYetValidException.NotBefore")
				{
					this.NotBefore = (DateTime)info.GetValue("Microsoft.IdentityModel.SecurityTokenNotYetValidException.NotBefore", typeof(DateTime));
				}
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003DF80 File Offset: 0x0003C180
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Microsoft.IdentityModel.SecurityTokenNotYetValidException.NotBefore", this.NotBefore);
		}

		// Token: 0x04000511 RID: 1297
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenNotYetValidException.";

		// Token: 0x04000512 RID: 1298
		[NonSerialized]
		private const string _NotBeforeKey = "Microsoft.IdentityModel.SecurityTokenNotYetValidException.NotBefore";
	}
}
