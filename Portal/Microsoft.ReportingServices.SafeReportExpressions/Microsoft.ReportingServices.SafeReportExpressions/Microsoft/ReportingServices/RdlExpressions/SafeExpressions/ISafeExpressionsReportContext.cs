using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000008 RID: 8
	public interface ISafeExpressionsReportContext
	{
		// Token: 0x06000015 RID: 21
		object GetAggregate(string aggregateName);

		// Token: 0x06000016 RID: 22
		object GetField(string fieldName);

		// Token: 0x06000017 RID: 23
		object GetGlobal(string globalName);

		// Token: 0x06000018 RID: 24
		object GetLookup(string lookupName);

		// Token: 0x06000019 RID: 25
		object GetParameter(string parameterName);

		// Token: 0x0600001A RID: 26
		object GetUser(string userParameterName);

		// Token: 0x0600001B RID: 27
		object GetVariable(string variableName);

		// Token: 0x0600001C RID: 28
		int GetLevel(string scope);

		// Token: 0x0600001D RID: 29
		bool InScope(string scope);

		// Token: 0x0600001E RID: 30
		Type GetCollectionItemType(string collectionName);

		// Token: 0x0600001F RID: 31
		bool IsCollection(string collectionName);
	}
}
