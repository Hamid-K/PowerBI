using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x02000209 RID: 521
	public interface IJsonStreamReader : IJsonReader
	{
		// Token: 0x060016FC RID: 5884
		Stream CreateReadStream();

		// Token: 0x060016FD RID: 5885
		TextReader CreateTextReader();

		// Token: 0x060016FE RID: 5886
		bool CanStream();
	}
}
