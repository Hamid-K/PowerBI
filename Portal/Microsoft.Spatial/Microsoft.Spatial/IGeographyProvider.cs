using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200002D RID: 45
	public interface IGeographyProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000176 RID: 374
		// (remove) Token: 0x06000177 RID: 375
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		event Action<Geography> ProduceGeography;

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000178 RID: 376
		Geography ConstructedGeography { get; }
	}
}
