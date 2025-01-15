using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x020001F0 RID: 496
	public interface IJsonReaderFactory
	{
		// Token: 0x0600136F RID: 4975
		IJsonReader CreateJsonReader(TextReader textReader, bool isIeee754Compatible);
	}
}
