using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000030 RID: 48
	public interface IGeographyProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000158 RID: 344
		// (remove) Token: 0x06000159 RID: 345
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		event Action<Geography> ProduceGeography;

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600015A RID: 346
		Geography ConstructedGeography { get; }
	}
}
