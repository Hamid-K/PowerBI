using System;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000010 RID: 16
	internal interface IMessageWriter : IDisposable
	{
		// Token: 0x06000050 RID: 80
		void WriteMessage(string name, object value);

		// Token: 0x06000051 RID: 81
		Stream CreateWritableStream(string name);
	}
}
