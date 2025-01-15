using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Azure.Identity
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class CredentialUnavailableException : AuthenticationFailedException
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000A0B6 File Offset: 0x000082B6
		public CredentialUnavailableException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public CredentialUnavailableException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A0CC File Offset: 0x000082CC
		internal static CredentialUnavailableException CreateAggregateException(string message, IList<CredentialUnavailableException> exceptions)
		{
			if (exceptions.Count == 1)
			{
				return exceptions[0];
			}
			StringBuilder stringBuilder = new StringBuilder(message);
			foreach (CredentialUnavailableException ex in exceptions)
			{
				stringBuilder.Append(Environment.NewLine).Append("- ").Append(ex.Message);
			}
			AggregateException ex2 = new AggregateException("Multiple exceptions were encountered while attempting to authenticate.", exceptions);
			return new CredentialUnavailableException(stringBuilder.ToString(), ex2);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A160 File Offset: 0x00008360
		protected CredentialUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
