using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	public class SecurityTokenInvalidTypeException : SecurityTokenValidationException
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0003DDE7 File Offset: 0x0003BFE7
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0003DDEF File Offset: 0x0003BFEF
		public string InvalidType { get; set; }

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
		public SecurityTokenInvalidTypeException()
		{
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003DE00 File Offset: 0x0003C000
		public SecurityTokenInvalidTypeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003DE09 File Offset: 0x0003C009
		public SecurityTokenInvalidTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003DE14 File Offset: 0x0003C014
		protected SecurityTokenInvalidTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "Microsoft.IdentityModel.SecurityTokenInvalidTypeException.InvalidType")
				{
					this.InvalidType = info.GetString("Microsoft.IdentityModel.SecurityTokenInvalidTypeException.InvalidType");
				}
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003DE5D File Offset: 0x0003C05D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (!string.IsNullOrEmpty(this.InvalidType))
			{
				info.AddValue("Microsoft.IdentityModel.SecurityTokenInvalidTypeException.InvalidType", this.InvalidType);
			}
		}

		// Token: 0x0400050E RID: 1294
		[NonSerialized]
		private const string _Prefix = "Microsoft.IdentityModel.SecurityTokenInvalidTypeException.";

		// Token: 0x0400050F RID: 1295
		[NonSerialized]
		private const string _InvalidTypeKey = "Microsoft.IdentityModel.SecurityTokenInvalidTypeException.InvalidType";
	}
}
