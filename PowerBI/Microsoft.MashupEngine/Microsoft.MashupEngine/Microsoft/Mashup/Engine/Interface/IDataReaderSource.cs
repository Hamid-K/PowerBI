using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000066 RID: 102
	public interface IDataReaderSource : IDisposable
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A6 RID: 422
		ITableSource TableSource { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A7 RID: 423
		IPageReader PageReader { get; }
	}
}
