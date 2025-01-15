using System;
using System.IO;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000093 RID: 147
	public interface IDatabaseContextWithLsdlContentType : IDatabaseContext, IDisposable
	{
		// Token: 0x0600029C RID: 668
		bool TryGetLinguisticSchemaReader(LanguageIdentifier language, out string contentType, out TextReader reader);
	}
}
