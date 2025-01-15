using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x020006E1 RID: 1761
	[Serializable]
	internal sealed class OdbcError
	{
		// Token: 0x060034EB RID: 13547 RVA: 0x000AA7EC File Offset: 0x000A89EC
		public OdbcError(string message, string state, int nativeerror)
		{
			this.message = message;
			this.state = state;
			this.nativeError = nativeerror;
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000AA809 File Offset: 0x000A8A09
		public string Message
		{
			get
			{
				return this.message ?? string.Empty;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000AA81A File Offset: 0x000A8A1A
		public string SQLState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000AA822 File Offset: 0x000A8A22
		public int NativeError
		{
			get
			{
				return this.nativeError;
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000AA82A File Offset: 0x000A8A2A
		public string ToString(Odbc32.RetCode retcode)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} [{1}] {2}", Odbc32.RetcodeToString(retcode), this.SQLState, this.Message);
		}

		// Token: 0x04001B69 RID: 7017
		private readonly string message;

		// Token: 0x04001B6A RID: 7018
		private readonly string state;

		// Token: 0x04001B6B RID: 7019
		private readonly int nativeError;
	}
}
