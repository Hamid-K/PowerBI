using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200013E RID: 318
	internal static class ValidationHelper
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x000132AA File Offset: 0x000114AA
		internal static bool IsEdmSystemNamespace(string namespaceName)
		{
			return namespaceName == "Transient" || namespaceName == "Edm";
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000132C8 File Offset: 0x000114C8
		internal static bool AddMemberNameToHashSet(IEdmNamedElement item, HashSetInternal<string> memberNameList, ValidationContext context, EdmErrorCode errorCode, string errorString, bool suppressError)
		{
			IEdmSchemaElement edmSchemaElement = item as IEdmSchemaElement;
			string text = ((edmSchemaElement != null) ? edmSchemaElement.FullName() : item.Name);
			if (!memberNameList.Add(text))
			{
				if (!suppressError)
				{
					context.AddError(item.Location(), errorCode, errorString);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001330D File Offset: 0x0001150D
		internal static bool AllPropertiesAreNullable(IEnumerable<IEdmStructuralProperty> properties)
		{
			return properties.Where((IEdmStructuralProperty p) => !p.Type.IsNullable).Count<IEdmStructuralProperty>() == 0;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001333C File Offset: 0x0001153C
		internal static bool HasNullableProperty(IEnumerable<IEdmStructuralProperty> properties)
		{
			return properties.Where((IEdmStructuralProperty p) => p.Type.IsNullable).Count<IEdmStructuralProperty>() > 0;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001336B File Offset: 0x0001156B
		internal static bool PropertySetIsSubset(IEnumerable<IEdmStructuralProperty> set, IEnumerable<IEdmStructuralProperty> subset)
		{
			return subset.Except(set).Count<IEdmStructuralProperty>() <= 0;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00013380 File Offset: 0x00011580
		internal static bool PropertySetsAreEquivalent(IEnumerable<IEdmStructuralProperty> set1, IEnumerable<IEdmStructuralProperty> set2)
		{
			if (set1.Count<IEdmStructuralProperty>() != set2.Count<IEdmStructuralProperty>())
			{
				return false;
			}
			IEnumerator<IEdmStructuralProperty> enumerator = set2.GetEnumerator();
			foreach (IEdmStructuralProperty edmStructuralProperty in set1)
			{
				enumerator.MoveNext();
				if (edmStructuralProperty != enumerator.Current)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000133F0 File Offset: 0x000115F0
		internal static bool ValidateValueCanBeWrittenAsXmlElementAnnotation(IEdmValue value, string annotationNamespace, string annotationName, out EdmError error)
		{
			IEdmStringValue edmStringValue = value as IEdmStringValue;
			if (edmStringValue == null)
			{
				error = new EdmError(value.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue);
				return false;
			}
			string value2 = edmStringValue.Value;
			XmlReader xmlReader = XmlReader.Create(new StringReader(value2));
			bool flag;
			try
			{
				if (xmlReader.NodeType != XmlNodeType.Element)
				{
					while (xmlReader.Read() && xmlReader.NodeType != XmlNodeType.Element)
					{
					}
				}
				if (xmlReader.EOF)
				{
					error = new EdmError(value.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml);
					flag = false;
				}
				else
				{
					string namespaceURI = xmlReader.NamespaceURI;
					string localName = xmlReader.LocalName;
					if (EdmUtil.IsNullOrWhiteSpaceInternal(namespaceURI) || EdmUtil.IsNullOrWhiteSpaceInternal(localName))
					{
						error = new EdmError(value.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName);
						flag = false;
					}
					else if ((annotationNamespace != null && !(namespaceURI == annotationNamespace)) || (annotationName != null && !(localName == annotationName)))
					{
						error = new EdmError(value.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm);
						flag = false;
					}
					else
					{
						while (xmlReader.Read())
						{
						}
						error = null;
						flag = true;
					}
				}
			}
			catch (XmlException)
			{
				error = new EdmError(value.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00013524 File Offset: 0x00011724
		internal static bool IsInterfaceCritical(EdmError error)
		{
			return error.ErrorCode >= EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull && error.ErrorCode <= EdmErrorCode.InterfaceCriticalCycleInTypeHierarchy;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00013540 File Offset: 0x00011740
		internal static bool ItemExistsInReferencedModel(this IEdmModel model, string fullName, bool checkEntityContainer)
		{
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				if (edmModel.FindDeclaredType(fullName) != null || edmModel.FindDeclaredTerm(fullName) != null || (checkEntityContainer && edmModel.ExistsContainer(fullName)) || (edmModel.FindDeclaredOperations(fullName) ?? Enumerable.Empty<IEdmOperation>()).FirstOrDefault<IEdmOperation>() != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000135C4 File Offset: 0x000117C4
		internal static bool OperationOrNameExistsInReferencedModel(this IEdmModel model, IEdmOperation operation, string operationFullName)
		{
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				if (edmModel.FindDeclaredType(operationFullName) != null || edmModel.ExistsContainer(operationFullName) || edmModel.FindDeclaredTerm(operationFullName) != null)
				{
					return true;
				}
				IEnumerable<IEdmOperation> enumerable = edmModel.FindDeclaredOperations(operationFullName) ?? Enumerable.Empty<IEdmOperation>();
				if (DuplicateOperationValidator.IsDuplicateOperation(operation, enumerable))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001364C File Offset: 0x0001184C
		internal static bool TypeIndirectlyContainsTarget(IEdmEntityType source, IEdmEntityType target, HashSetInternal<IEdmEntityType> visited, IEdmModel context)
		{
			if (visited.Add(source))
			{
				if (source.IsOrInheritsFrom(target))
				{
					return true;
				}
				foreach (IEdmNavigationProperty edmNavigationProperty in source.NavigationProperties())
				{
					if (edmNavigationProperty.ContainsTarget && ValidationHelper.TypeIndirectlyContainsTarget(edmNavigationProperty.ToEntityType(), target, visited, context))
					{
						return true;
					}
				}
				foreach (IEdmStructuredType edmStructuredType in context.FindAllDerivedTypes(source))
				{
					IEdmEntityType edmEntityType = edmStructuredType as IEdmEntityType;
					if (edmEntityType != null && ValidationHelper.TypeIndirectlyContainsTarget(edmEntityType, target, visited, context))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001371C File Offset: 0x0001191C
		internal static IEdmEntityType ComputeNavigationPropertyTarget(IEdmNavigationProperty property)
		{
			IEdmType edmType = property.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return (IEdmEntityType)edmType;
		}
	}
}
