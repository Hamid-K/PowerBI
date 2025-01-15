using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000247 RID: 583
	internal abstract class CanWriteFileStream : Stream
	{
		// Token: 0x06001540 RID: 5440
		public abstract void WriteFile(string name);
	}
}
