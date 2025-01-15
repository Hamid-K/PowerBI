using System;
using System.Data;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000049 RID: 73
	internal sealed class NonDisposableDataReader : DelegatingDataReader
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000D8BE File Offset: 0x0000BABE
		public NonDisposableDataReader(IDataReader dataReader)
			: base(dataReader)
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D8C7 File Offset: 0x0000BAC7
		public override void Close()
		{
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D8C9 File Offset: 0x0000BAC9
		public override void Dispose()
		{
		}
	}
}
