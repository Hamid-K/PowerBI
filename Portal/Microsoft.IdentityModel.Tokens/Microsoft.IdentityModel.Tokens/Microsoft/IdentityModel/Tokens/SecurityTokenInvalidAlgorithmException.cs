using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	public class SecurityTokenInvalidAlgorithmException : SecurityTokenValidationException
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003DA6B File Offset: 0x0003BC6B
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0003DA73 File Offset: 0x0003BC73
		public string InvalidAlgorithm { get; set; }

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003DA7C File Offset: 0x0003BC7C
		public SecurityTokenInvalidAlgorithmException()
		{
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003DA84 File Offset: 0x0003BC84
		public SecurityTokenInvalidAlgorithmException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003DA8D File Offset: 0x0003BC8D
		public SecurityTokenInvalidAlgorithmException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003DA98 File Offset: 0x0003BC98
		protected SecurityTokenInvalidAlgorithmException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenInvalidAlgorithmException.InvalidAlgorithm")
				{
					this.InvalidAlgorithm = info.GetString("Microsoft.IdentityModel.SecurityTokenInvalidAlgorithmException.InvalidAlgorithm");
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003DAE1 File Offset: 0x0003BCE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (!string.IsNullOrEmpty(this.InvalidAlgorithm))
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidAlgorithmException.InvalidAlgorithm", this.InvalidAlgorithm);
			}
		}

		// Token: 0x040004FF RID: 1279
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenInvalidAlgorithmException.";

		// Token: 0x04000500 RID: 1280
		[NonSerialized]
		private const string _InvalidAlgorithmKey = "Microsoft.IdentityModel.SecurityTokenInvalidAlgorithmException.InvalidAlgorithm";
	}
}
