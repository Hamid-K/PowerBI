using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000175 RID: 373
	public interface IMetadataSerializationHost
	{
		// Token: 0x0600179B RID: 6043
		void OperationStartNotification(bool isSerializing, object context, IReadOnlyCollection<string> logicalPaths);

		// Token: 0x0600179C RID: 6044
		void DocumentStartNotification(bool isSerializing, object context, string logicalPath);

		// Token: 0x0600179D RID: 6045
		void DocumentEndNotification(bool isSerializing, object context, string logicalPath, bool isSuccessful);

		// Token: 0x0600179E RID: 6046
		void OperationEndNotification(bool isSerializing, object context, bool isSuccessful);

		// Token: 0x0600179F RID: 6047
		void ErrorNotification(bool isSerializing, object context, Exception e);
	}
}
