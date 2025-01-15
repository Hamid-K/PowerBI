using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQuery.Builder
{
	// Token: 0x0200003E RID: 62
	internal static class DsqExpressionBuilderExtensions
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00007829 File Offset: 0x00005A29
		public static EntitySetExpressionNode DsqReference(this IConceptualEntity entity)
		{
			return ExpressionNodeBuilder.EntitySet(entity.EntityContainerName, entity.EdmName, entity);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000783D File Offset: 0x00005A3D
		public static PropertyExpressionNode DsqReference(this IConceptualProperty property)
		{
			return ExpressionNodeBuilder.ModelProperty(property.Entity.EntityContainerName, property.Entity.EdmName, property.EdmName, property);
		}
	}
}
