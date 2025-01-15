using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000A RID: 10
	internal interface IQuerySchemaExtender
	{
		// Token: 0x0600000A RID: 10
		IConceptualColumn CreateColumn(string extendedEntity, ConceptualPrimitiveType conceptualType, string suggestedName, Expression dsqExpression);

		// Token: 0x0600000B RID: 11
		IConceptualMeasure CreateMeasure(string extendedEntity, ConceptualPrimitiveType conceptualType, string suggestedName, string dax);
	}
}
