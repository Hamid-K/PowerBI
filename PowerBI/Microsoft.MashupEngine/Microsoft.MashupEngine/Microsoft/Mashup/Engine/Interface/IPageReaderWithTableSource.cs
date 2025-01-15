using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000067 RID: 103
	public interface IPageReaderWithTableSource : IPageReader, IDisposable
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A8 RID: 424
		ITableSource TableSource { get; }
	}
}
