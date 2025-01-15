using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020B RID: 523
	internal sealed class DefaultJsonReaderFactory : IJsonReaderFactory
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x00040FF4 File Offset: 0x0003F1F4
		public IJsonReader CreateJsonReader(TextReader textReader, bool isIeee754Compatible)
		{
			return new JsonReader(textReader, isIeee754Compatible);
		}
	}
}
