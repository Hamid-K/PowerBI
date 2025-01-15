using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200001C RID: 28
	public interface IDbSchemaCommand : IDisposable
	{
		// Token: 0x0600007B RID: 123
		void SetTelemetryIds(string clientActivityId, string currentActivityId, string rootActivityId);

		// Token: 0x0600007C RID: 124
		void SetTimeout(int timeout);

		// Token: 0x0600007D RID: 125
		void SetApplicationContext(string applicationContext);

		// Token: 0x0600007E RID: 126
		string GetModelMetadata(string catalogName, string perspectiveName, string supportedVersion, TranslationsBehavior translationsBehavior);

		// Token: 0x0600007F RID: 127
		DataSet GetDependencyGraph(string databaseName, string depedencyObjectType, string depedencyQuery);

		// Token: 0x06000080 RID: 128
		DataSet GetSchemaDataSet(string schemaName, IReadOnlyDictionary<string, object> restrictions);

		// Token: 0x06000081 RID: 129
		IReadOnlyList<DataSet> ExecuteBatchSchemaCommand(string commandText);

		// Token: 0x06000082 RID: 130
		void SaveDatabase(string dbName);

		// Token: 0x06000083 RID: 131
		DataSet GetAnnotations(string dbName);
	}
}
