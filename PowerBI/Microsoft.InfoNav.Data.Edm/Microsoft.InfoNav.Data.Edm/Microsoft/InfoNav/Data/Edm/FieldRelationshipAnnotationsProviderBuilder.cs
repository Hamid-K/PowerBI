using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000A RID: 10
	internal sealed class FieldRelationshipAnnotationsProviderBuilder
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002668 File Offset: 0x00000868
		internal static FieldRelationshipAnnotations BuildFieldRelationshipAnnotations(IConceptualSchema schema)
		{
			Dictionary<IConceptualColumn, WritableFieldRelationshipAnnotation> dictionary = new Dictionary<IConceptualColumn, WritableFieldRelationshipAnnotation>();
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					ConceptualColumn conceptualColumn = conceptualProperty as ConceptualColumn;
					if (conceptualColumn != null)
					{
						IList<string> list = conceptualColumn.ParsedEdmStructuralProperty.RelatedToProperties.Evaluate<string>();
						if (list.Count != 0)
						{
							List<IConceptualColumn> list2 = new List<IConceptualColumn>(list.Count);
							foreach (string text in list)
							{
								IConceptualProperty conceptualProperty2;
								conceptualEntity.TryGetPropertyByEdmName(text, out conceptualProperty2);
								IConceptualColumn conceptualColumn2 = ((conceptualProperty2 != null) ? conceptualProperty2.AsColumn() : null);
								Contract.Check(conceptualColumn2 != null, "Fail to get IConceptualColumn from EdmField {0}");
								WritableFieldRelationshipAnnotation writableFieldRelationshipAnnotation;
								if (!dictionary.TryGetValue(conceptualColumn2, out writableFieldRelationshipAnnotation))
								{
									writableFieldRelationshipAnnotation = new WritableFieldRelationshipAnnotation();
									dictionary.Add(conceptualColumn2, writableFieldRelationshipAnnotation);
								}
								writableFieldRelationshipAnnotation.SetRelatedToSource(conceptualColumn);
								list2.Add(conceptualColumn2);
							}
							WritableFieldRelationshipAnnotation writableFieldRelationshipAnnotation2;
							if (!dictionary.TryGetValue(conceptualColumn, out writableFieldRelationshipAnnotation2))
							{
								writableFieldRelationshipAnnotation2 = new WritableFieldRelationshipAnnotation();
								dictionary.Add(conceptualColumn, writableFieldRelationshipAnnotation2);
							}
							writableFieldRelationshipAnnotation2.SetRelatedToFields(list2);
						}
					}
				}
			}
			FieldRelationshipAnnotationsProviderBuilder.CompleteInitializeAnnotations(dictionary);
			return new FieldRelationshipAnnotations(dictionary.ToDictionary((KeyValuePair<IConceptualColumn, WritableFieldRelationshipAnnotation> kv) => kv.Key, (KeyValuePair<IConceptualColumn, WritableFieldRelationshipAnnotation> kv) => kv.Value));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002858 File Offset: 0x00000A58
		private static void CompleteInitializeAnnotations(Dictionary<IConceptualColumn, WritableFieldRelationshipAnnotation> annotations)
		{
			foreach (IConceptualColumn conceptualColumn in annotations.Keys)
			{
				if (annotations[conceptualColumn].RelatedToFields.IsNullOrEmpty<IConceptualColumn>())
				{
					FieldRelationshipAnnotationsProviderBuilder.SetLeafMemberships(annotations, conceptualColumn, conceptualColumn);
				}
			}
			FieldRelationshipAnnotationsProviderBuilder.ComputeAllFieldsOnPath(annotations);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028C8 File Offset: 0x00000AC8
		private static void SetLeafMemberships(Dictionary<IConceptualColumn, WritableFieldRelationshipAnnotation> annotations, IConceptualColumn column, IConceptualColumn leaf)
		{
			annotations[column].LeafMemberships.Add(leaf);
			if (annotations[column].RelatedToSource != null)
			{
				FieldRelationshipAnnotationsProviderBuilder.SetLeafMemberships(annotations, annotations[column].RelatedToSource, leaf);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002900 File Offset: 0x00000B00
		private static void ComputeAllFieldsOnPath(Dictionary<IConceptualColumn, WritableFieldRelationshipAnnotation> annotations)
		{
			foreach (IConceptualColumn conceptualColumn in annotations.Keys)
			{
				List<IConceptualColumn> list = new List<IConceptualColumn>();
				foreach (IConceptualColumn conceptualColumn2 in annotations[conceptualColumn].LeafMemberships)
				{
					while (conceptualColumn2 != null)
					{
						list.Add(conceptualColumn2);
						conceptualColumn2 = annotations[conceptualColumn2].RelatedToSource;
					}
				}
				annotations[conceptualColumn].SetAllFieldsOnPath(list);
			}
		}
	}
}
