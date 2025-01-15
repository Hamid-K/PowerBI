using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Packaging.Project;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200001B RID: 27
	public interface IExplorationSerializer
	{
		// Token: 0x06000092 RID: 146
		IDictionary<Uri, IStreamablePowerBIProjectPartContent> Serialize(string exploration, string mobileState);

		// Token: 0x06000093 RID: 147
		IDictionary<Uri, IStreamablePowerBIPackagePartContent> SerializeToPackage(string exploration, string mobileState);

		// Token: 0x06000094 RID: 148
		Task<ExplorationDeserializationResult<ExplorationContract>> DeserializeAsync(IDictionary<Uri, IStreamablePowerBIProjectPartContent> files, CancellationToken cancellationToken);

		// Token: 0x06000095 RID: 149
		Task<ExplorationDeserializationResult<string>> DeserializeToStringAsync(IDictionary<Uri, IStreamablePowerBIProjectPartContent> files, CancellationToken cancellationToken);

		// Token: 0x06000096 RID: 150
		Task<ExplorationDeserializationResult<ExplorationContract>> DeserializeAsync(IDictionary<Uri, IStreamablePowerBIPackagePartContent> files, CancellationToken cancellationToken);

		// Token: 0x06000097 RID: 151
		Task<ExplorationDeserializationResult<string>> DeserializeToStringAsync(IDictionary<Uri, IStreamablePowerBIPackagePartContent> files, CancellationToken cancellationToken);
	}
}
