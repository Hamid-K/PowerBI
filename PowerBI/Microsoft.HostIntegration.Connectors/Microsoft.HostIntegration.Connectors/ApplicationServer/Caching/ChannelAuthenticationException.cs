using System;
using System.Runtime.Serialization;
using System.Security.Authentication;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	internal class ChannelAuthenticationException : AuthenticationException
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002D1EE File Offset: 0x0002B3EE
		internal ErrStatus ErrorStatus
		{
			get
			{
				return this._errStatus;
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002D1F6 File Offset: 0x0002B3F6
		public ChannelAuthenticationException(ErrStatus status, string message)
			: this(status, message, null)
		{
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002D201 File Offset: 0x0002B401
		public ChannelAuthenticationException(ErrStatus status, string message, Exception innerException)
			: base(message, innerException)
		{
			this._errStatus = status;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002D212 File Offset: 0x0002B412
		protected ChannelAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000933 RID: 2355
		private readonly ErrStatus _errStatus;
	}
}
