using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.Lucia.Hosting;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200005F RID: 95
	public interface IDomainModelManager : IDisposable
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002AD RID: 685
		DomainModelManagerStatus Status { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002AE RID: 686
		Exception VerifyRuntimeException { get; }

		// Token: 0x060002AF RID: 687
		Task<IReference<IDatabaseContext>> GetDatabaseContextAsync();

		// Token: 0x060002B0 RID: 688
		Task<IReference<IDataIndexContainer>> GetDataIndexAsync();

		// Token: 0x060002B1 RID: 689
		Task NotifyModelChanging();

		// Token: 0x060002B2 RID: 690
		Task NotifyModelChanged(string filePath, DateTime lastModifiedTime, ImmutableHashSet<SchemaItem> schemaItemsToInvalidate, Action domainModelUpdatedCallback, bool luciaSessionInUse);

		// Token: 0x060002B3 RID: 691
		Task NotifyFileDiscarded();

		// Token: 0x060002B4 RID: 692
		Task NotifyFilePathChanged(string oldFilePath, string newFilepath);
	}
}
