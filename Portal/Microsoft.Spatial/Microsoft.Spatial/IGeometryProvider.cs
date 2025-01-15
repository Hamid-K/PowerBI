using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200002E RID: 46
	public interface IGeometryProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000179 RID: 377
		// (remove) Token: 0x0600017A RID: 378
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		event Action<Geometry> ProduceGeometry;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600017B RID: 379
		Geometry ConstructedGeometry { get; }
	}
}
