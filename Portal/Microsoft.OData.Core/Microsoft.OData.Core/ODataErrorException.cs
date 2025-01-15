using System;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x02000082 RID: 130
	[DebuggerDisplay("{Message}")]
	public sealed class ODataErrorException : ODataException
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000BFB5 File Offset: 0x0000A1B5
		public ODataErrorException()
			: this(Strings.ODataErrorException_GeneralError)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		public ODataErrorException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public ODataErrorException(string message, Exception innerException)
			: this(message, innerException, new ODataError())
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000BFDB File Offset: 0x0000A1DB
		public ODataErrorException(ODataError error)
			: this(Strings.ODataErrorException_GeneralError, null, error)
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000BFEA File Offset: 0x0000A1EA
		public ODataErrorException(string message, ODataError error)
			: this(message, null, error)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000BFF5 File Offset: 0x0000A1F5
		public ODataErrorException(string message, Exception innerException, ODataError error)
			: base(message, innerException)
		{
			this.state.ODataError = error;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000C00B File Offset: 0x0000A20B
		public ODataError Error
		{
			get
			{
				return this.state.ODataError;
			}
		}

		// Token: 0x0400020E RID: 526
		private ODataErrorException.ODataErrorExceptionSafeSerializationState state;

		// Token: 0x020002AE RID: 686
		private struct ODataErrorExceptionSafeSerializationState
		{
			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00057094 File Offset: 0x00055294
			// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0005709C File Offset: 0x0005529C
			public ODataError ODataError { get; set; }
		}
	}
}
