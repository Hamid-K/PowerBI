using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200002A RID: 42
	public interface IGeometryProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600010D RID: 269
		// (remove) Token: 0x0600010E RID: 270
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		event Action<Geometry> ProduceGeometry;

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010F RID: 271
		Geometry ConstructedGeometry { get; }
	}
}
