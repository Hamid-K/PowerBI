using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000031 RID: 49
	public interface IGeometryProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600015B RID: 347
		// (remove) Token: 0x0600015C RID: 348
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		event Action<Geometry> ProduceGeometry;

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600015D RID: 349
		Geometry ConstructedGeometry { get; }
	}
}
