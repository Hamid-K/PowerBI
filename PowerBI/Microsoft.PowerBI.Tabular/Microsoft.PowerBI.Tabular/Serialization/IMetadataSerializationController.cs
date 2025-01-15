using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000174 RID: 372
	internal interface IMetadataSerializationController
	{
		// Token: 0x06001796 RID: 6038
		void OnSerializationStart(object userContext, IReadOnlyCollection<string> logicalPaths, out object operationContext);

		// Token: 0x06001797 RID: 6039
		void OnDocumentSerializationStart(object userContext, object operationContext, string logicalPath, out object documentContext, out Stream document);

		// Token: 0x06001798 RID: 6040
		void OnDocumentSerializationEnd(object userContext, object operationContext, string logicalPath, object documentContext, Stream document, bool isSuccessfulSerialization);

		// Token: 0x06001799 RID: 6041
		void OnSerializationEnd(object userContext, object operationContext, bool isSuccessfulSerialization);

		// Token: 0x0600179A RID: 6042
		void OnSerializationError(object userContext, object operationContext, Exception e);
	}
}
