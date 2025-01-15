using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000068 RID: 104
	public interface IStreamSource : IDisposable
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A9 RID: 425
		Stream Stream { get; }
	}
}
