using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000059 RID: 89
	public interface IColumnStatisticsDiscovererWrapper
	{
		// Token: 0x060002A1 RID: 673
		Task<ColumnStatisticsAnnotationProvider> DiscoverAsync(IConceptualSchema schema, string fileName, bool luciaSessionInUse, DateTime currentTime, TimeSpan refreshInterval, CancellationToken cancellationToken, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate = null);
	}
}
