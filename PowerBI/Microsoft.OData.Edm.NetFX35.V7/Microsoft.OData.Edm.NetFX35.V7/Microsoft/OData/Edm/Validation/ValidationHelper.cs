using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000DD RID: 221
	internal static class ValidationHelper
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00011142 File Offset: 0x0000F342
		internal static bool IsEdmSystemNamespace(string namespaceName)
		{
			return namespaceName == "Transient" || namespaceName == "Edm";
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00011160 File Offset: 0x0000F360
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

		// Token: 0x06000666 RID: 1638 RVA: 0x000111A5 File Offset: 0x0000F3A5
		internal static bool AllPropertiesAreNullable(IEnumerable<IEdmStructuralProperty> properties)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(properties, (IEdmStructuralProperty p) => !p.Type.IsNullable)) == 0;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000111D4 File Offset: 0x0000F3D4
		internal static bool HasNullableProperty(IEnumerable<IEdmStructuralProperty> properties)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(properties, (IEdmStructuralProperty p) => p.Type.IsNullable)) > 0;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00011203 File Offset: 0x0000F403
		internal static bool PropertySetIsSubset(IEnumerable<IEdmStructuralProperty> set, IEnumerable<IEdmStructuralProperty> subset)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Except<IEdmStructuralProperty>(subset, set)) <= 0;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00011218 File Offset: 0x0000F418
		internal static bool PropertySetsAreEquivalent(IEnumerable<IEdmStructuralProperty> set1, IEnumerable<IEdmStructuralProperty> set2)
		{
			if (Enumerable.Count<IEdmStructuralProperty>(set1) != Enumerable.Count<IEdmStructuralProperty>(set2))
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

		// Token: 0x0600066A RID: 1642 RVA: 0x00011288 File Offset: 0x0000F488
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
				if (xmlReader.NodeType != 1)
				{
					while (xmlReader.Read() && xmlReader.NodeType != 1)
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

		// Token: 0x0600066B RID: 1643 RVA: 0x000113BC File Offset: 0x0000F5BC
		internal static bool IsInterfaceCritical(EdmError error)
		{
			return error.ErrorCode >= EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull && error.ErrorCode <= EdmErrorCode.InterfaceCriticalCycleInTypeHierarchy;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000113D8 File Offset: 0x0000F5D8
		internal static bool ItemExistsInReferencedModel(this IEdmModel model, string fullName, bool checkEntityContainer)
		{
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				if (edmModel.FindDeclaredType(fullName) != null || edmModel.FindDeclaredTerm(fullName) != null || (checkEntityContainer && edmModel.ExistsContainer(fullName)) || Enumerable.FirstOrDefault<IEdmOperation>(edmModel.FindDeclaredOperations(fullName) ?? Enumerable.Empty<IEdmOperation>()) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001145C File Offset: 0x0000F65C
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

		// Token: 0x0600066E RID: 1646 RVA: 0x000114E4 File Offset: 0x0000F6E4
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

		// Token: 0x0600066F RID: 1647 RVA: 0x000115B4 File Offset: 0x0000F7B4
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
