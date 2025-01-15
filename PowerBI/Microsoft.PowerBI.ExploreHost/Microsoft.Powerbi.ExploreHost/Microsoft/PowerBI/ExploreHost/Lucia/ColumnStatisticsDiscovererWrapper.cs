using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004A RID: 74
	internal sealed class ColumnStatisticsDiscovererWrapper : IColumnStatisticsDiscovererWrapper
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00007508 File Offset: 0x00005708
		internal ColumnStatisticsDiscovererWrapper(Func<IConceptualSchema, string, bool, IColumnStatisticsDiscoverer> createDiscoverer)
		{
			this._createDiscoverer = createDiscoverer;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007517 File Offset: 0x00005717
		public Task<ColumnStatisticsAnnotationProvider> DiscoverAsync(IConceptualSchema schema, string filePath, bool luciaSessionInUse, DateTime currentTime, TimeSpan refreshInterval, CancellationToken cancellationToken, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate = null)
		{
			return this._createDiscoverer(schema, filePath, luciaSessionInUse).DiscoverAsync(schema, currentTime, refreshInterval, cancellationToken, schemaItemsToInvalidate, !luciaSessionInUse);
		}

		// Token: 0x040000E3 RID: 227
		private readonly Func<IConceptualSchema, string, bool, IColumnStatisticsDiscoverer> _createDiscoverer;
	}
}
