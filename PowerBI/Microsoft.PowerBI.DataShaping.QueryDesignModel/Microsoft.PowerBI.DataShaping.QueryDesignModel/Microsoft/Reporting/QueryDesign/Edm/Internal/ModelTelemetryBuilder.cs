using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200024A RID: 586
	internal static class ModelTelemetryBuilder
	{
		// Token: 0x060019CD RID: 6605 RVA: 0x00046DA0 File Offset: 0x00044FA0
		internal static ModelTelemetry BuildModelTelemetry(EntityDataModel model, IConceptualSchema schema)
		{
			ModelTelemetry modelTelemetry = new ModelTelemetry();
			modelTelemetry.Caption = (((schema != null) ? schema.DisplayName : null) ?? model.Caption).MarkAsCustomerContent();
			IConceptualSchema conceptualSchema = ((model != null) ? model.ConceptualSchemaCache : null) as IConceptualSchema;
			if (conceptualSchema != null || schema != null)
			{
				ModelTelemetryBuilder.PopulateTelemetryFromConceptualSchema(modelTelemetry, schema ?? conceptualSchema);
			}
			else
			{
				ModelTelemetryBuilder.PopulateTelemetryFromEdmModel(model, modelTelemetry);
			}
			return modelTelemetry;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00046E04 File Offset: 0x00045004
		private static void PopulateTelemetryFromEdmModel(EntityDataModel model, ModelTelemetry modelTelemetry)
		{
			modelTelemetry.EntityCount = model.EntitySets.Count;
			modelTelemetry.EntityNameTotalLength = 0;
			modelTelemetry.PropertyCount = 0;
			modelTelemetry.PropertyNameTotalLength = 0;
			bool flag = model.ModelCapabilities.IsMultidimensional();
			modelTelemetry.Kind = ModelTelemetryBuilder.GetModelKind(flag);
			HashSet<string> hashSet;
			modelTelemetry.DegenerateDimensionCount = ModelTelemetryBuilder.GetDegenerateDimensionCount(model.EntitySets, flag, out hashSet);
			modelTelemetry.NormalizedRelationshipCount = 0;
			foreach (EntitySet entitySet in model.EntitySets)
			{
				modelTelemetry.EntityNameTotalLength += entitySet.ReferenceName.Length;
				foreach (EdmPropertyInstance edmPropertyInstance in entitySet.GetProperties())
				{
					int num = modelTelemetry.PropertyCount;
					modelTelemetry.PropertyCount = num + 1;
					modelTelemetry.PropertyNameTotalLength += edmPropertyInstance.Property.ReferenceName.Length;
				}
			}
			modelTelemetry.Relationships1to1 = 0;
			modelTelemetry.Relationships1toM = 0;
			modelTelemetry.RelationshipsMtoM = 0;
			foreach (AssociationSet associationSet in model.AssociationSets.Where((AssociationSet a) => a.State == AssociationState.Active))
			{
				AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[0];
				AssociationSetEnd associationSetEnd2 = associationSet.AssociationSetEnds[1];
				if (ModelTelemetryBuilder.HasCardinalityMany(associationSetEnd) && ModelTelemetryBuilder.HasCardinalityMany(associationSetEnd2))
				{
					int num = modelTelemetry.RelationshipsMtoM;
					modelTelemetry.RelationshipsMtoM = num + 1;
				}
				else if (ModelTelemetryBuilder.HasCardinalityOne(associationSetEnd) && ModelTelemetryBuilder.HasCardinalityOne(associationSetEnd2))
				{
					int num = modelTelemetry.Relationships1to1;
					modelTelemetry.Relationships1to1 = num + 1;
				}
				else
				{
					int num = modelTelemetry.Relationships1toM;
					modelTelemetry.Relationships1toM = num + 1;
				}
				if (flag && hashSet.Contains(associationSetEnd.EntitySet.ReferenceName, EdmItem.ReferenceNameComparer) && hashSet.Contains(associationSetEnd2.EntitySet.ReferenceName, EdmItem.ReferenceNameComparer))
				{
					int num = modelTelemetry.NormalizedRelationshipCount;
					modelTelemetry.NormalizedRelationshipCount = num + 1;
				}
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00047064 File Offset: 0x00045264
		private static void PopulateTelemetryFromConceptualSchema(ModelTelemetry modelTelemetry, IConceptualSchema schema)
		{
			modelTelemetry.EntityCount = schema.Entities.Count;
			modelTelemetry.EntityNameTotalLength = 0;
			modelTelemetry.PropertyCount = 0;
			modelTelemetry.PropertyNameTotalLength = 0;
			modelTelemetry.Relationships1to1 = 0;
			modelTelemetry.Relationships1toM = 0;
			modelTelemetry.RelationshipsMtoM = 0;
			bool flag = schema.Capabilities.IsMultidimensional();
			modelTelemetry.Kind = ModelTelemetryBuilder.GetModelKind(flag);
			HashSet<string> hashSet;
			modelTelemetry.DegenerateDimensionCount = ModelTelemetryBuilder.GetDegenerateDimensionCount(schema.Entities, flag, out hashSet);
			modelTelemetry.NormalizedRelationshipCount = 0;
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				modelTelemetry.EntityNameTotalLength += conceptualEntity.Name.Length;
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					int num = modelTelemetry.PropertyCount;
					modelTelemetry.PropertyCount = num + 1;
					modelTelemetry.PropertyNameTotalLength += conceptualProperty.Name.Length;
				}
				foreach (IConceptualNavigationProperty conceptualNavigationProperty in conceptualEntity.NavigationProperties)
				{
					EdmConceptualNavigationProperty edmConceptualNavigationProperty = conceptualNavigationProperty as EdmConceptualNavigationProperty;
					if (edmConceptualNavigationProperty != null && conceptualNavigationProperty.IsActive && !hashSet2.Contains(edmConceptualNavigationProperty.AssociationName, ConceptualNameComparer.Instance))
					{
						if (conceptualNavigationProperty.SourceMultiplicity == ConceptualMultiplicity.Many && conceptualNavigationProperty.TargetMultiplicity == ConceptualMultiplicity.Many)
						{
							int num = modelTelemetry.RelationshipsMtoM;
							modelTelemetry.RelationshipsMtoM = num + 1;
						}
						else if ((conceptualNavigationProperty.SourceMultiplicity == ConceptualMultiplicity.One || conceptualNavigationProperty.SourceMultiplicity == ConceptualMultiplicity.ZeroOrOne) && (conceptualNavigationProperty.TargetMultiplicity == ConceptualMultiplicity.One || conceptualNavigationProperty.TargetMultiplicity == ConceptualMultiplicity.ZeroOrOne))
						{
							int num = modelTelemetry.Relationships1to1;
							modelTelemetry.Relationships1to1 = num + 1;
						}
						else
						{
							int num = modelTelemetry.Relationships1toM;
							modelTelemetry.Relationships1toM = num + 1;
						}
						if (flag && hashSet.Contains(conceptualEntity.EdmName, ConceptualNameComparer.Instance) && hashSet.Contains(conceptualNavigationProperty.TargetEntity.EdmName, ConceptualNameComparer.Instance))
						{
							int num = modelTelemetry.NormalizedRelationshipCount;
							modelTelemetry.NormalizedRelationshipCount = num + 1;
						}
						hashSet2.Add(edmConceptualNavigationProperty.AssociationName);
					}
				}
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00047304 File Offset: 0x00045504
		private static int GetDegenerateDimensionCount(EntitySetCollection entitySets, bool isMD, out HashSet<string> dimensionEntities)
		{
			if (!isMD)
			{
				dimensionEntities = null;
				return 0;
			}
			dimensionEntities = new HashSet<string>();
			int num = 0;
			foreach (EntitySet entitySet in entitySets)
			{
				if (entitySet.ElementType.Members.Count == entitySet.ElementType.Fields.Count)
				{
					dimensionEntities.Add(entitySet.ReferenceName);
				}
				else if (entitySet.ElementType.Fields.Any((EdmField f) => !f.IsRowNumber()))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000473C0 File Offset: 0x000455C0
		private static int GetDegenerateDimensionCount(IReadOnlyList<IConceptualEntity> entitySets, bool isMD, out HashSet<string> dimensionEntities)
		{
			if (!isMD)
			{
				dimensionEntities = null;
				return 0;
			}
			dimensionEntities = new HashSet<string>();
			int num = 0;
			foreach (IConceptualEntity conceptualEntity in entitySets)
			{
				IEnumerable<IConceptualColumn> enumerable = conceptualEntity.Properties.OfType<IConceptualColumn>();
				if (conceptualEntity.Properties.Count == enumerable.Count<IConceptualColumn>())
				{
					dimensionEntities.Add(conceptualEntity.EdmName);
				}
				else if (enumerable.Any((IConceptualColumn c) => !c.IsRowNumber))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00047470 File Offset: 0x00045670
		private static string GetModelKind(bool isMultidimensional)
		{
			if (isMultidimensional)
			{
				return "MD";
			}
			return "Tabular";
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00047480 File Offset: 0x00045680
		private static bool HasCardinalityOne(AssociationSetEnd x)
		{
			return x.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One || x.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000474A0 File Offset: 0x000456A0
		private static bool HasCardinalityMany(AssociationSetEnd x)
		{
			return x.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many;
		}
	}
}
