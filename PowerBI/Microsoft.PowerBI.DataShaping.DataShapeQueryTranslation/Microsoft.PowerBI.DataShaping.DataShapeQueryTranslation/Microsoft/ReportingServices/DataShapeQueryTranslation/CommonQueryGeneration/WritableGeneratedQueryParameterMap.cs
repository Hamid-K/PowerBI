using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x02000116 RID: 278
	internal sealed class WritableGeneratedQueryParameterMap : GeneratedQueryParameterMap
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x00028C5F File Offset: 0x00026E5F
		public void Add(string dsqName, QueryParameterReferenceExpression parameterRef)
		{
			this._parametersByDsqName.Add(dsqName, parameterRef);
		}
	}
}
