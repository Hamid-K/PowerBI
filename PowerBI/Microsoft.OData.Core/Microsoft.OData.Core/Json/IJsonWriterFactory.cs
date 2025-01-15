using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020F RID: 527
	[CLSCompliant(false)]
	public interface IJsonWriterFactory
	{
		// Token: 0x0600170D RID: 5901
		IJsonWriter CreateJsonWriter(TextWriter textWriter, bool isIeee754Compatible);
	}
}
