using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000139 RID: 313
	internal interface IBatchQueryExtensionSchemaExpressionGenerator
	{
		// Token: 0x06000BA8 RID: 2984
		QueryExpressionContext Generate(ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity);
	}
}
