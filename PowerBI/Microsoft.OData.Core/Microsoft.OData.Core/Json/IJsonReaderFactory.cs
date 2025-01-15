using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020E RID: 526
	public interface IJsonReaderFactory
	{
		// Token: 0x0600170C RID: 5900
		IJsonReader CreateJsonReader(TextReader textReader, bool isIeee754Compatible);
	}
}
