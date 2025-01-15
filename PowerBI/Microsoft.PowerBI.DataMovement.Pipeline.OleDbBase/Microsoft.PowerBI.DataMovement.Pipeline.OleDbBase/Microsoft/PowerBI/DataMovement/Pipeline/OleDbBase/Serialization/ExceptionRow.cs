using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D4 RID: 212
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ExceptionRow : IExceptionRow
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000BB0B File Offset: 0x00009D0B
		public ExceptionRow(IDictionary<int, IDictionary<string, string>> exceptions)
		{
			this.exceptions = exceptions;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000BB1A File Offset: 0x00009D1A
		public IDictionary<int, IDictionary<string, string>> Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x040003B4 RID: 948
		private IDictionary<int, IDictionary<string, string>> exceptions;
	}
}
