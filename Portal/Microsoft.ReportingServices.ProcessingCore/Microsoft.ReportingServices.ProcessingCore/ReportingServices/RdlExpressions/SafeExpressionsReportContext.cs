using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000574 RID: 1396
	internal sealed class SafeExpressionsReportContext : ISafeExpressionsReportContext
	{
		// Token: 0x060050A6 RID: 20646 RVA: 0x00152E41 File Offset: 0x00151041
		public SafeExpressionsReportContext(ObjectModelImpl reportProcessingObjectModel)
		{
			this._reportProcessingObjectModel = reportProcessingObjectModel;
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00152E50 File Offset: 0x00151050
		public object GetAggregate(string paramName)
		{
			return this._reportProcessingObjectModel.Aggregates[paramName];
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00152E63 File Offset: 0x00151063
		public object GetField(string fieldName)
		{
			return this._reportProcessingObjectModel.Fields[fieldName];
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00152E76 File Offset: 0x00151076
		public object GetGlobal(string globalName)
		{
			return this._reportProcessingObjectModel.Globals[globalName];
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00152E89 File Offset: 0x00151089
		public object GetLookup(string lookupName)
		{
			return this._reportProcessingObjectModel.Lookups[lookupName];
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x00152E9C File Offset: 0x0015109C
		public object GetParameter(string paramName)
		{
			return this._reportProcessingObjectModel.Parameters[paramName];
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x00152EAF File Offset: 0x001510AF
		public object GetUser(string userParameterName)
		{
			return this._reportProcessingObjectModel.User[userParameterName];
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x00152EC2 File Offset: 0x001510C2
		public object GetVariable(string fieldName)
		{
			return this._reportProcessingObjectModel.Variables[fieldName];
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x00152ED5 File Offset: 0x001510D5
		public int GetLevel(string scope)
		{
			return this._reportProcessingObjectModel.RecursiveLevel(scope);
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x00152EE3 File Offset: 0x001510E3
		public bool InScope(string scope)
		{
			return this._reportProcessingObjectModel.InScope(scope);
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00152EF1 File Offset: 0x001510F1
		public Type GetCollectionItemType(string collectionName)
		{
			return ReportCollectionTypes.GetCollectionItemType(collectionName);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00152EF9 File Offset: 0x001510F9
		public bool IsCollection(string collectionName)
		{
			return ReportCollectionTypes.IsCollection(collectionName);
		}

		// Token: 0x040028B0 RID: 10416
		private readonly ObjectModelImpl _reportProcessingObjectModel;
	}
}
