using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Validation.Internal;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl
{
	// Token: 0x020000AA RID: 170
	public static class SerializationExtensionMethods
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00007282 File Offset: 0x00005482
		public static Version GetEdmxVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion");
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000072A1 File Offset: 0x000054A1
		public static void SetEdmxVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion", version);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000072C1 File Offset: 0x000054C1
		public static void SetNamespacePrefixMappings(this IEdmModel model, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix", mappings);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000072E1 File Offset: 0x000054E1
		public static IEnumerable<KeyValuePair<string, string>> GetNamespacePrefixMappings(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix");
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007300 File Offset: 0x00005500
		public static void SetDataServiceVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "DataServiceVersion", version);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007320 File Offset: 0x00005520
		public static Version GetDataServiceVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "DataServiceVersion");
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000733F File Offset: 0x0000553F
		public static void SetMaxDataServiceVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "MaxDataServiceVersion", version);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000735F File Offset: 0x0000555F
		public static Version GetMaxDataServiceVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "MaxDataServiceVersion");
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000737E File Offset: 0x0000557E
		public static void SetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model, EdmVocabularyAnnotationSerializationLocation? location)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation", location);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000073AF File Offset: 0x000055AF
		public static EdmVocabularyAnnotationSerializationLocation? GetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation") as EdmVocabularyAnnotationSerializationLocation?;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000073E4 File Offset: 0x000055E4
		public static void SetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model, string schemaNamespace)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace", schemaNamespace);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00007410 File Offset: 0x00005610
		public static string GetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace");
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000743B File Offset: 0x0000563B
		public static void SetAssociationName(this IEdmModel model, IEdmNavigationProperty property, string associationName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			EdmUtil.CheckArgumentNull<string>(associationName, "associationName");
			model.SetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationName", associationName);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00007474 File Offset: 0x00005674
		public static string GetAssociationName(this IEdmModel model, IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			property.PopulateCaches();
			string text = model.GetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationName");
			if (text == null)
			{
				IEdmNavigationProperty primary = property.GetPrimary();
				IEdmNavigationProperty partner = primary.Partner;
				text = SerializationExtensionMethods.GetQualifiedAndEscapedPropertyName(partner) + '_' + SerializationExtensionMethods.GetQualifiedAndEscapedPropertyName(primary);
			}
			return text;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000074DC File Offset: 0x000056DC
		public static void SetAssociationNamespace(this IEdmModel model, IEdmNavigationProperty property, string associationNamespace)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			EdmUtil.CheckArgumentNull<string>(associationNamespace, "associationNamespace");
			model.SetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationNamespace", associationNamespace);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00007514 File Offset: 0x00005714
		public static string GetAssociationNamespace(this IEdmModel model, IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			property.PopulateCaches();
			string text = model.GetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationNamespace");
			if (text == null)
			{
				text = property.GetPrimary().DeclaringEntityType().Namespace;
			}
			return text;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00007566 File Offset: 0x00005766
		public static string GetAssociationFullName(this IEdmModel model, IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			property.PopulateCaches();
			return model.GetAssociationNamespace(property) + "." + model.GetAssociationName(property);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000075A0 File Offset: 0x000057A0
		public static void SetAssociationAnnotations(this IEdmModel model, IEdmNavigationProperty property, IEnumerable<IEdmDirectValueAnnotation> annotations, IEnumerable<IEdmDirectValueAnnotation> end1Annotations, IEnumerable<IEdmDirectValueAnnotation> end2Annotations, IEnumerable<IEdmDirectValueAnnotation> constraintAnnotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			if ((annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(annotations) != null) || (end1Annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(end1Annotations) != null) || (end2Annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(end2Annotations) != null) || (constraintAnnotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(constraintAnnotations) != null))
			{
				model.SetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationAnnotations", new SerializationExtensionMethods.AssociationAnnotations
				{
					Annotations = annotations,
					End1Annotations = end1Annotations,
					End2Annotations = end2Annotations,
					ConstraintAnnotations = constraintAnnotations
				});
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000762C File Offset: 0x0000582C
		public static void GetAssociationAnnotations(this IEdmModel model, IEdmNavigationProperty property, out IEnumerable<IEdmDirectValueAnnotation> annotations, out IEnumerable<IEdmDirectValueAnnotation> end1Annotations, out IEnumerable<IEdmDirectValueAnnotation> end2Annotations, out IEnumerable<IEdmDirectValueAnnotation> constraintAnnotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			property.PopulateCaches();
			SerializationExtensionMethods.AssociationAnnotations annotationValue = model.GetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationAnnotations");
			if (annotationValue != null)
			{
				annotations = annotationValue.Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				end1Annotations = annotationValue.End1Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				end2Annotations = annotationValue.End2Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				constraintAnnotations = annotationValue.ConstraintAnnotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				return;
			}
			IEnumerable<IEdmDirectValueAnnotation> enumerable = Enumerable.Empty<IEdmDirectValueAnnotation>();
			annotations = enumerable;
			end1Annotations = enumerable;
			end2Annotations = enumerable;
			constraintAnnotations = enumerable;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000076C7 File Offset: 0x000058C7
		public static void SetAssociationEndName(this IEdmModel model, IEdmNavigationProperty property, string association)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			model.SetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationEndName", association);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000076F3 File Offset: 0x000058F3
		public static string GetAssociationEndName(this IEdmModel model, IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			property.PopulateCaches();
			return model.GetAnnotationValue(property, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationEndName") ?? property.Partner.Name;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00007734 File Offset: 0x00005934
		public static void SetAssociationSetName(this IEdmModel model, IEdmEntitySet entitySet, IEdmNavigationProperty property, string associationSet)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(entitySet, "entitySet");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			Dictionary<IEdmNavigationProperty, string> dictionary = model.GetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetName");
			if (dictionary == null)
			{
				dictionary = new Dictionary<IEdmNavigationProperty, string>(SerializationExtensionMethods.EdmNavigationPropertyHashComparer.Instance);
				model.SetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetName", dictionary);
			}
			dictionary[property] = associationSet;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000077A0 File Offset: 0x000059A0
		public static string GetAssociationSetName(this IEdmModel model, IEdmEntitySet entitySet, IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(entitySet, "entitySet");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			Dictionary<IEdmNavigationProperty, string> annotationValue = model.GetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetName");
			string text;
			if (annotationValue == null || !annotationValue.TryGetValue(property, ref text))
			{
				text = model.GetAssociationName(property) + "Set";
			}
			return text;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007804 File Offset: 0x00005A04
		public static void SetAssociationSetAnnotations(this IEdmModel model, IEdmEntitySet entitySet, IEdmNavigationProperty property, IEnumerable<IEdmDirectValueAnnotation> annotations, IEnumerable<IEdmDirectValueAnnotation> end1Annotations, IEnumerable<IEdmDirectValueAnnotation> end2Annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(entitySet, "property");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			if ((annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(annotations) != null) || (end1Annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(end1Annotations) != null) || (end2Annotations != null && Enumerable.FirstOrDefault<IEdmDirectValueAnnotation>(end2Annotations) != null))
			{
				Dictionary<IEdmNavigationProperty, SerializationExtensionMethods.AssociationSetAnnotations> dictionary = model.GetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetAnnotations");
				if (dictionary == null)
				{
					dictionary = new Dictionary<IEdmNavigationProperty, SerializationExtensionMethods.AssociationSetAnnotations>(SerializationExtensionMethods.EdmNavigationPropertyHashComparer.Instance);
					model.SetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetAnnotations", dictionary);
				}
				dictionary[property] = new SerializationExtensionMethods.AssociationSetAnnotations
				{
					Annotations = annotations,
					End1Annotations = end1Annotations,
					End2Annotations = end2Annotations
				};
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static void GetAssociationSetAnnotations(this IEdmModel model, IEdmEntitySet entitySet, IEdmNavigationProperty property, out IEnumerable<IEdmDirectValueAnnotation> annotations, out IEnumerable<IEdmDirectValueAnnotation> end1Annotations, out IEnumerable<IEdmDirectValueAnnotation> end2Annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(entitySet, "entitySet");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			Dictionary<IEdmNavigationProperty, SerializationExtensionMethods.AssociationSetAnnotations> annotationValue = model.GetAnnotationValue(entitySet, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AssociationSetAnnotations");
			SerializationExtensionMethods.AssociationSetAnnotations associationSetAnnotations;
			if (annotationValue != null && annotationValue.TryGetValue(property, ref associationSetAnnotations))
			{
				annotations = associationSetAnnotations.Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				end1Annotations = associationSetAnnotations.End1Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				end2Annotations = associationSetAnnotations.End2Annotations ?? Enumerable.Empty<IEdmDirectValueAnnotation>();
				return;
			}
			IEnumerable<IEdmDirectValueAnnotation> enumerable = Enumerable.Empty<IEdmDirectValueAnnotation>();
			annotations = enumerable;
			end1Annotations = enumerable;
			end2Annotations = enumerable;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000794C File Offset: 0x00005B4C
		public static IEdmNavigationProperty GetPrimary(this IEdmNavigationProperty property)
		{
			if (property.IsPrincipal)
			{
				return property;
			}
			IEdmNavigationProperty partner = property.Partner;
			if (partner.IsPrincipal)
			{
				return partner;
			}
			int num = string.Compare(property.Name, partner.Name, 4);
			if (num == 0)
			{
				num = string.Compare(property.DeclaringEntityType().FullName(), partner.DeclaringEntityType().FullName(), 4);
			}
			if (num <= 0)
			{
				return partner;
			}
			return property;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000079AE File Offset: 0x00005BAE
		public static void SetIsValueExplicit(this IEdmEnumMember member, IEdmModel model, bool? isExplicit)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit", isExplicit);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000079DF File Offset: 0x00005BDF
		public static bool? IsValueExplicit(this IEdmEnumMember member, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit") as bool?;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007A14 File Offset: 0x00005C14
		public static void SetIsSerializedAsElement(this IEdmValue value, IEdmModel model, bool isSerializedAsElement)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmError edmError;
			if (isSerializedAsElement && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(value, null, null, out edmError))
			{
				throw new InvalidOperationException(edmError.ToString());
			}
			model.SetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement", isSerializedAsElement);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007A6C File Offset: 0x00005C6C
		public static bool IsSerializedAsElement(this IEdmValue value, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (model.GetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement") as bool?) ?? false;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public static void SetNamespaceAlias(this IEdmModel model, string namespaceName, string alias)
		{
			VersioningDictionary<string, string> versioningDictionary = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			if (versioningDictionary == null)
			{
				versioningDictionary = VersioningDictionary<string, string>.Create(new Func<string, string, int>(string.CompareOrdinal));
			}
			if (alias == null)
			{
				versioningDictionary = versioningDictionary.Remove(namespaceName);
			}
			else
			{
				versioningDictionary = versioningDictionary.Set(namespaceName, alias);
			}
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias", versioningDictionary);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007B1C File Offset: 0x00005D1C
		public static string GetNamespaceAlias(this IEdmModel model, string namespaceName)
		{
			VersioningDictionary<string, string> annotationValue = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			return annotationValue.Get(namespaceName);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00007B42 File Offset: 0x00005D42
		internal static VersioningDictionary<string, string> GetNamespaceAliases(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007B64 File Offset: 0x00005D64
		internal static bool IsInline(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			return annotation.GetSerializationLocation(model) == EdmVocabularyAnnotationSerializationLocation.Inline || annotation.TargetString() == null;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007B99 File Offset: 0x00005D99
		internal static string TargetString(this IEdmVocabularyAnnotation annotation)
		{
			return EdmUtil.FullyQualifiedName(annotation.Target);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00007BA6 File Offset: 0x00005DA6
		private static void PopulateCaches(this IEdmNavigationProperty property)
		{
			IEdmNavigationProperty partner = property.Partner;
			bool isPrincipal = property.IsPrincipal;
			IEnumerable<IEdmStructuralProperty> dependentProperties = property.DependentProperties;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007BC0 File Offset: 0x00005DC0
		private static string GetQualifiedAndEscapedPropertyName(IEdmNavigationProperty property)
		{
			return string.Concat(new object[]
			{
				SerializationExtensionMethods.EscapeName(property.DeclaringEntityType().Namespace).Replace('.', '_'),
				'_',
				SerializationExtensionMethods.EscapeName(property.DeclaringEntityType().Name),
				'_',
				SerializationExtensionMethods.EscapeName(property.Name)
			});
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007C2B File Offset: 0x00005E2B
		private static string EscapeName(string name)
		{
			return name.Replace("_", "__");
		}

		// Token: 0x0400014C RID: 332
		private const char AssociationNameEscapeChar = '_';

		// Token: 0x0400014D RID: 333
		private const string AssociationNameEscapeString = "_";

		// Token: 0x0400014E RID: 334
		private const string AssociationNameEscapeStringEscaped = "__";

		// Token: 0x020000AB RID: 171
		private class AssociationAnnotations
		{
			// Token: 0x17000183 RID: 387
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x00007C3D File Offset: 0x00005E3D
			// (set) Token: 0x060002F5 RID: 757 RVA: 0x00007C45 File Offset: 0x00005E45
			public IEnumerable<IEdmDirectValueAnnotation> Annotations { get; set; }

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x00007C4E File Offset: 0x00005E4E
			// (set) Token: 0x060002F7 RID: 759 RVA: 0x00007C56 File Offset: 0x00005E56
			public IEnumerable<IEdmDirectValueAnnotation> End1Annotations { get; set; }

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x00007C5F File Offset: 0x00005E5F
			// (set) Token: 0x060002F9 RID: 761 RVA: 0x00007C67 File Offset: 0x00005E67
			public IEnumerable<IEdmDirectValueAnnotation> End2Annotations { get; set; }

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x060002FA RID: 762 RVA: 0x00007C70 File Offset: 0x00005E70
			// (set) Token: 0x060002FB RID: 763 RVA: 0x00007C78 File Offset: 0x00005E78
			public IEnumerable<IEdmDirectValueAnnotation> ConstraintAnnotations { get; set; }
		}

		// Token: 0x020000AC RID: 172
		private class AssociationSetAnnotations
		{
			// Token: 0x17000187 RID: 391
			// (get) Token: 0x060002FD RID: 765 RVA: 0x00007C89 File Offset: 0x00005E89
			// (set) Token: 0x060002FE RID: 766 RVA: 0x00007C91 File Offset: 0x00005E91
			public IEnumerable<IEdmDirectValueAnnotation> Annotations { get; set; }

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x060002FF RID: 767 RVA: 0x00007C9A File Offset: 0x00005E9A
			// (set) Token: 0x06000300 RID: 768 RVA: 0x00007CA2 File Offset: 0x00005EA2
			public IEnumerable<IEdmDirectValueAnnotation> End1Annotations { get; set; }

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000301 RID: 769 RVA: 0x00007CAB File Offset: 0x00005EAB
			// (set) Token: 0x06000302 RID: 770 RVA: 0x00007CB3 File Offset: 0x00005EB3
			public IEnumerable<IEdmDirectValueAnnotation> End2Annotations { get; set; }
		}

		// Token: 0x020000AD RID: 173
		private class EdmNavigationPropertyHashComparer : EqualityComparer<IEdmNavigationProperty>
		{
			// Token: 0x06000304 RID: 772 RVA: 0x00007CC4 File Offset: 0x00005EC4
			private EdmNavigationPropertyHashComparer()
			{
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000305 RID: 773 RVA: 0x00007CCC File Offset: 0x00005ECC
			internal static SerializationExtensionMethods.EdmNavigationPropertyHashComparer Instance
			{
				get
				{
					return SerializationExtensionMethods.EdmNavigationPropertyHashComparer.instance;
				}
			}

			// Token: 0x06000306 RID: 774 RVA: 0x00007CD4 File Offset: 0x00005ED4
			public override bool Equals(IEdmNavigationProperty left, IEdmNavigationProperty right)
			{
				string text = SerializationExtensionMethods.EdmNavigationPropertyHashComparer.GenerateHash(right);
				string text2 = SerializationExtensionMethods.EdmNavigationPropertyHashComparer.GenerateHash(left);
				return text == text2;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x00007CF8 File Offset: 0x00005EF8
			public override int GetHashCode(IEdmNavigationProperty obj)
			{
				string text = SerializationExtensionMethods.EdmNavigationPropertyHashComparer.GenerateHash(obj);
				return text.GetHashCode();
			}

			// Token: 0x06000308 RID: 776 RVA: 0x00007D12 File Offset: 0x00005F12
			private static string GenerateHash(IEdmNavigationProperty prop)
			{
				return prop.Name + "_" + prop.DeclaringEntityType().FullName();
			}

			// Token: 0x04000156 RID: 342
			private static SerializationExtensionMethods.EdmNavigationPropertyHashComparer instance = new SerializationExtensionMethods.EdmNavigationPropertyHashComparer();
		}
	}
}
