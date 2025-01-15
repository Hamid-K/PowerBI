using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000029 RID: 41
	public interface IGeographyProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600010A RID: 266
		// (remove) Token: 0x0600010B RID: 267
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Not following the event-handler pattern")]
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		event Action<Geography> ProduceGeography;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010C RID: 268
		Geography ConstructedGeography { get; }
	}
}
