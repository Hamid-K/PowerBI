using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000170 RID: 368
	internal interface IMetadataDeserializationController
	{
		// Token: 0x06001768 RID: 5992
		void OnDeserializationStart(object userContext, out object operationContext, out IReadOnlyCollection<string> logicalPaths);

		// Token: 0x06001769 RID: 5993
		void OnDocumentDeserializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document);

		// Token: 0x0600176A RID: 5994
		void OnDocumentDeserializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulDeserialization);

		// Token: 0x0600176B RID: 5995
		void OnDeserializationEnd(object userContext, object operationContext, bool isSuccessfulDeserialization);

		// Token: 0x0600176C RID: 5996
		void OnDeserializationError(object userContext, object operationContext, Exception e);
	}
}
