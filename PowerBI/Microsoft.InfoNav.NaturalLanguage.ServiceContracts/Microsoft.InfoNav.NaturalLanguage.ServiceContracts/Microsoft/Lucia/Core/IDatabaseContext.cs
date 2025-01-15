using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000092 RID: 146
	public interface IDatabaseContext : IDisposable
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000298 RID: 664
		string DatabaseName { get; }

		// Token: 0x06000299 RID: 665
		DateTime GetLastUpdateTime();

		// Token: 0x0600029A RID: 666
		IEnumerable<XmlReader> GetConceptualSchemaReaders();

		// Token: 0x0600029B RID: 667
		bool TryGetLinguisticSchemaReaders(out ReadOnlyCollection<XmlReader> readers);
	}
}
