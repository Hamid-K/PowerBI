using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000141 RID: 321
	internal static class DaxRef
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00030CF8 File Offset: 0x0002EEF8
		internal static StringComparer NameComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00030CFF File Offset: 0x0002EEFF
		internal static DaxTableRef Table(EntitySet entitySet, IConceptualEntity entity)
		{
			return new DaxTableRef(DaxRef.GetDaxName(entitySet, entity));
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00030D0D File Offset: 0x0002EF0D
		internal static DaxColumnRef Column(ConceptualTypeColumn column, EntitySet entitySet, IConceptualEntity entity)
		{
			return new DaxColumnRef(column.Name, DaxRef.Table(entitySet, entity));
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00030D21 File Offset: 0x0002EF21
		internal static DaxColumnRef Column(string columnName)
		{
			return new DaxColumnRef(columnName, DaxTableRef.Empty);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00030D2E File Offset: 0x0002EF2E
		internal static DaxMeasureRef Measure(EdmMeasure edmMeasure, EntitySet entitySet, IConceptualMeasure measure, IConceptualEntity entity)
		{
			return new DaxMeasureRef(DaxRef.GetDaxName(edmMeasure, measure), DaxRef.Table(entitySet, entity));
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00030D43 File Offset: 0x0002EF43
		private static string GetDaxName(EntitySet entitySet, IConceptualEntity entity)
		{
			return ((entity != null) ? entity.Name : null) ?? entitySet.ReferenceName;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00030D5B File Offset: 0x0002EF5B
		private static string GetDaxName(EdmProperty edmProperty, IConceptualProperty property)
		{
			return ((property != null) ? property.Name : null) ?? edmProperty.ReferenceName;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00030D73 File Offset: 0x0002EF73
		internal static string Parameter(string parameterName)
		{
			return DaxIdentifiers.CreateSquareBracketedIdentifierWithPrefix("@", parameterName);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00030D80 File Offset: 0x0002EF80
		private static bool IsFromEntity(ConceptualTypeColumn column, IConceptualEntity entity)
		{
			IConceptualProperty conceptualProperty;
			if (entity.TryGetProperty(column.Name, out conceptualProperty))
			{
				return true;
			}
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			return extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null && DaxRef.IsFromEntity(column, extensionConceptualEntity.ExtendedEntity);
		}

		// Token: 0x04000AD7 RID: 2775
		internal const string VariableDeclarationPrefix = "__";

		// Token: 0x04000AD8 RID: 2776
		private const string ParameterRefPrefix = "@";
	}
}
