using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020CF RID: 8399
	public interface ILibraryProvider : IDisposable
	{
		// Token: 0x17003176 RID: 12662
		// (get) Token: 0x0600CDC8 RID: 52680
		string Identifier { get; }

		// Token: 0x0600CDC9 RID: 52681
		IEnumerable<ILibrary> GetLibraries();

		// Token: 0x0600CDCA RID: 52682
		bool TryGetLibrary(string identifier, out ILibrary library);

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600CDCB RID: 52683
		// (remove) Token: 0x0600CDCC RID: 52684
		event EventHandler<LibraryChangedEventArgs> Changed;
	}
}
