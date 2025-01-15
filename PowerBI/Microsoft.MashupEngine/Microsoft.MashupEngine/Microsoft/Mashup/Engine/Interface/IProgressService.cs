using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000089 RID: 137
	public interface IProgressService
	{
		// Token: 0x060001F9 RID: 505
		IHostProgress GetHostProgress(string progressType, string dataSource);
	}
}
