using System;
using System.Data;

namespace Dapper
{
	// Token: 0x02000012 RID: 18
	public interface IWrappedDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600012C RID: 300
		IDataReader Reader { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600012D RID: 301
		IDbCommand Command { get; }
	}
}
