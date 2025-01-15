using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Validation.Internal
{
	// Token: 0x0200026F RID: 623
	internal static class ValidationHelper
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x00026DD4 File Offset: 0x00024FD4
		internal static bool IsEdmSystemNamespace(string namespaceName)
		{
			return namespaceName == "Transient" || namespaceName == "Edm";
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00026DF0 File Offset: 0x00024FF0
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

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00026E45 File Offset: 0x00025045
		internal static bool AllPropertiesAreNullable(IEnumerable<IEdmStructuralProperty> properties)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(properties, (IEdmStructuralProperty p) => !p.Type.IsNullable)) == 0;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00026E7F File Offset: 0x0002507F
		internal static bool HasNullableProperty(IEnumerable<IEdmStructuralProperty> properties)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(properties, (IEdmStructuralProperty p) => p.Type.IsNullable)) > 0;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00026EAC File Offset: 0x000250AC
		internal static bool PropertySetIsSubset(IEnumerable<IEdmStructuralProperty> set, IEnumerable<IEdmStructuralProperty> subset)
		{
			return Enumerable.Count<IEdmStructuralProperty>(Enumerable.Except<IEdmStructuralProperty>(subset, set)) <= 0;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00026EC0 File Offset: 0x000250C0
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

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00026F30 File Offset: 0x00025130
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

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00027064 File Offset: 0x00025264
		internal static bool IsInterfaceCritical(EdmError error)
		{
			return error.ErrorCode >= EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull && error.ErrorCode <= EdmErrorCode.InterfaceCriticalCycleInTypeHierarchy;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00027080 File Offset: 0x00025280
		internal static bool ItemExistsInReferencedModel(this IEdmModel model, string fullName, bool checkEntityContainer)
		{
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				if (edmModel.FindDeclaredType(fullName) != null || edmModel.FindDeclaredValueTerm(fullName) != null || (checkEntityContainer && edmModel.ExistsContainer(fullName)) || Enumerable.FirstOrDefault<IEdmOperation>(edmModel.FindDeclaredOperations(fullName) ?? Enumerable.Empty<IEdmOperation>()) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00027104 File Offset: 0x00025304
		internal static bool OperationOrNameExistsInReferencedModel(this IEdmModel model, IEdmOperation operation, string operationFullName)
		{
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				if (edmModel.FindDeclaredType(operationFullName) != null || edmModel.ExistsContainer(operationFullName) || edmModel.FindDeclaredValueTerm(operationFullName) != null)
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

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002718C File Offset: 0x0002538C
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

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00027264 File Offset: 0x00025464
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
