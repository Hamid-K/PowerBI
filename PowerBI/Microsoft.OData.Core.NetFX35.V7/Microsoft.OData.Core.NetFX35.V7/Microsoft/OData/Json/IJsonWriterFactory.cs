using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x020001F1 RID: 497
	[CLSCompliant(false)]
	public interface IJsonWriterFactory
	{
		// Token: 0x06001370 RID: 4976
		IJsonWriter CreateJsonWriter(TextWriter textWriter, bool isIeee754Compatible);
	}
}
