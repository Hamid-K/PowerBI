using System;
using System.Threading;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000062 RID: 98
	// (Invoke) Token: 0x060002BA RID: 698
	public delegate IStatisticsAnnotationProvider StatisticsAnnotationProviderCreator(IConceptualSchema conceptualSchema, CancellationToken cancellationToken);
}
