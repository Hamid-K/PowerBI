using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.Spatial;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x0200012B RID: 299
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "The class coupling is due to mapping primitive types, lot of different types there.")]
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Following EdmLib standards.")]
	internal static class EdmLibraryExtensions
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x000292E8 File Offset: 0x000274E8
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Need to use the static constructor for the phone platform.")]
		static EdmLibraryExtensions()
		{
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool), EdmLibraryExtensions.BooleanTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte), EdmLibraryExtensions.ByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal), EdmLibraryExtensions.DecimalTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double), EdmLibraryExtensions.DoubleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short), EdmLibraryExtensions.Int16TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int), EdmLibraryExtensions.Int32TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long), EdmLibraryExtensions.Int64TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte), EdmLibraryExtensions.SByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(string), EdmLibraryExtensions.StringTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float), EdmLibraryExtensions.SingleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Duration), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte[]), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Binary), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Stream), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Stream), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Duration), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Date), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Date), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Date?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Date), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeOfDay), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeOfDay?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay), true));
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000297F3 File Offset: 0x000279F3
		internal static string FullNavigationSourceName(this IEdmNavigationSource navigationSource)
		{
			return string.Join(".", Enumerable.ToArray<string>(navigationSource.Path.Path));
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00029B0C File Offset: 0x00027D0C
		internal static IEnumerable<IEdmFunctionImport> FilterFunctionsByParameterNames(this IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<string> parameterNames, bool caseInsensitive)
		{
			IList<string> parameterNameList = Enumerable.ToList<string>(parameterNames);
			foreach (IEdmFunctionImport functionImport in functionImports)
			{
				IEnumerable<IEdmOperationParameter> parametersToMatch = functionImport.Operation.Parameters;
				if (functionImport.Function.IsBound)
				{
					parametersToMatch = Enumerable.Skip<IEdmOperationParameter>(parametersToMatch, 1);
				}
				List<IEdmOperationParameter> operationImportParameters = Enumerable.ToList<IEdmOperationParameter>(parametersToMatch);
				if (operationImportParameters.Count == parameterNameList.Count)
				{
					if (!Enumerable.Any<IEdmOperationParameter>(operationImportParameters, (IEdmOperationParameter p) => Enumerable.All<string>(parameterNameList, (string k) => !string.Equals(k, p.Name, caseInsensitive ? 5 : 4))))
					{
						yield return functionImport;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00029B38 File Offset: 0x00027D38
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Parameter type is needed to get binding type.")]
		internal static IEnumerable<IEdmOperation> FilterBoundOperationsWithSameTypeHierarchyToTypeClosestToBindingType(this IEnumerable<IEdmOperation> operations, IEdmType bindingType)
		{
			IEdmStructuredType edmStructuredType = bindingType as IEdmStructuredType;
			if (bindingType.TypeKind == EdmTypeKind.Collection)
			{
				edmStructuredType = ((IEdmCollectionType)bindingType).ElementType.Definition as IEdmStructuredType;
			}
			if (edmStructuredType == null)
			{
				return operations;
			}
			Dictionary<IEdmType, List<IEdmOperation>> dictionary = new Dictionary<IEdmType, List<IEdmOperation>>(new EdmLibraryExtensions.EdmTypeEqualityComparer());
			IEdmType edmType = null;
			int num = int.MaxValue;
			foreach (IEdmOperation edmOperation in operations)
			{
				if (edmOperation.IsBound && Enumerable.Any<IEdmOperationParameter>(edmOperation.Parameters))
				{
					IEdmType definition = Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.Definition;
					IEdmStructuredType edmStructuredType2 = definition as IEdmStructuredType;
					if (definition.TypeKind == EdmTypeKind.Collection)
					{
						IEdmCollectionType edmCollectionType = definition as IEdmCollectionType;
						edmStructuredType2 = edmCollectionType.ElementType.Definition as IEdmStructuredType;
					}
					if (edmStructuredType2 != null && edmStructuredType.IsOrInheritsFrom(edmStructuredType2))
					{
						int num2 = edmStructuredType.InheritanceLevelFromSpecifiedInheritedType(edmStructuredType2);
						if (num > num2)
						{
							num = num2;
							edmType = definition;
						}
						if (!dictionary.ContainsKey(definition))
						{
							dictionary[definition] = new List<IEdmOperation>();
						}
						dictionary[definition].Add(edmOperation);
					}
				}
			}
			if (edmType != null)
			{
				return dictionary[edmType];
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00029C80 File Offset: 0x00027E80
		internal static IEnumerable<IEdmFunction> FilterFunctionsByParameterNames(this IEnumerable<IEdmFunction> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			return Enumerable.Cast<IEdmFunction>(Enumerable.Cast<IEdmOperation>(functions).FilterOperationsByParameterNames(parameters, caseInsensitive));
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00029F88 File Offset: 0x00028188
		internal static IEnumerable<IEdmOperation> FilterOperationsByParameterNames(this IEnumerable<IEdmOperation> operations, IEnumerable<string> parameters, bool caseInsensitive)
		{
			IList<string> parametersList = Enumerable.ToList<string>(parameters);
			foreach (IEdmOperation operation in operations)
			{
				IEnumerable<IEdmOperationParameter> parametersToMatch = operation.Parameters;
				if (operation.IsBound)
				{
					parametersToMatch = Enumerable.Skip<IEdmOperationParameter>(parametersToMatch, 1);
				}
				List<IEdmOperationParameter> functionImportParameters = Enumerable.ToList<IEdmOperationParameter>(parametersToMatch);
				if (functionImportParameters.Count == parametersList.Count)
				{
					if (!Enumerable.Any<IEdmOperationParameter>(functionImportParameters, (IEdmOperationParameter p) => Enumerable.All<string>(parametersList, (string k) => !string.Equals(k, p.Name, caseInsensitive ? 5 : 4))))
					{
						yield return operation;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002A190 File Offset: 0x00028390
		internal static IEnumerable<IEdmOperation> EnsureOperationsBoundWithBindingParameter(this IEnumerable<IEdmOperation> operations)
		{
			foreach (IEdmOperation operation in operations)
			{
				if (!operation.IsBound)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(operation.Name));
				}
				if (!Enumerable.Any<IEdmOperationParameter>(operation.Parameters))
				{
					throw new ODataException(Strings.EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(operation.Name));
				}
				yield return operation;
			}
			yield break;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002A1AD File Offset: 0x000283AD
		internal static IEnumerable<IEdmOperation> ResolveOperations(this IEdmModel model, string namespaceQualifiedOperationName)
		{
			return model.ResolveOperations(namespaceQualifiedOperationName, true);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002A1E8 File Offset: 0x000283E8
		internal static IEnumerable<IEdmOperation> ResolveOperations(this IEdmModel model, string operationName, bool allowParameterTypeNames)
		{
			if (string.IsNullOrEmpty(operationName))
			{
				return Enumerable.Empty<IEdmOperation>();
			}
			int num = operationName.IndexOf('(');
			string text;
			if (num > 0)
			{
				if (!allowParameterTypeNames)
				{
					return Enumerable.Empty<IEdmOperation>();
				}
				text = operationName.Substring(0, num);
			}
			else
			{
				text = operationName;
			}
			IEnumerable<IEdmOperation> enumerable = model.FindDeclaredOperations(text);
			if (enumerable == null)
			{
				return Enumerable.Empty<IEdmOperation>();
			}
			if (num > 0)
			{
				return Enumerable.Where<IEdmOperation>(enumerable, (IEdmOperation f) => (f.IsFunction() && f.FullNameWithNonBindingParameters().Equals(operationName, 4)) || f.IsAction());
			}
			return EdmLibraryExtensions.ValidateOperationGroupReturnsOnlyOnKind(enumerable, text);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002A284 File Offset: 0x00028484
		internal static bool AllHaveEqualReturnTypeAndAttributes(this IList<IEdmOperationImport> operationImports)
		{
			if (!Enumerable.Any<IEdmOperationImport>(operationImports))
			{
				return true;
			}
			IEdmType edmType = ((operationImports[0].Operation.ReturnType == null) ? null : operationImports[0].Operation.ReturnType.Definition);
			bool flag = operationImports[0].IsFunctionImport();
			bool flag2 = operationImports[0].IsActionImport();
			foreach (IEdmOperationImport edmOperationImport in operationImports)
			{
				if (edmOperationImport.IsFunctionImport() != flag)
				{
					return false;
				}
				if (edmOperationImport.IsActionImport() != flag2)
				{
					return false;
				}
				if (edmType != null)
				{
					if (edmOperationImport.Operation.ReturnType.Definition.FullTypeName() != edmType.FullTypeName())
					{
						return false;
					}
				}
				else if (edmOperationImport.Operation.ReturnType != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002A378 File Offset: 0x00028578
		internal static bool AllHaveEqualReturnTypeAndAttributes(this IList<IEdmOperation> operations)
		{
			if (!Enumerable.Any<IEdmOperation>(operations))
			{
				return true;
			}
			IEdmType edmType = ((operations[0].ReturnType == null) ? null : operations[0].ReturnType.Definition);
			bool flag = operations[0].IsFunction();
			bool flag2 = operations[0].IsAction();
			foreach (IEdmOperation edmOperation in operations)
			{
				if (edmOperation.IsFunction() != flag)
				{
					return false;
				}
				if (edmOperation.IsAction() != flag2)
				{
					return false;
				}
				if (edmType != null)
				{
					if (edmOperation.ReturnType.Definition.FullTypeName() != edmType.FullTypeName())
					{
						return false;
					}
				}
				else if (edmOperation.ReturnType != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002A458 File Offset: 0x00028658
		internal static string NameWithParameters(this IEdmOperation operation)
		{
			return operation.Name + operation.ParameterTypesToString();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002A46B File Offset: 0x0002866B
		internal static string FullNameWithParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.ParameterTypesToString();
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002A47E File Offset: 0x0002867E
		internal static string FullNameWithNonBindingParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.NonBindingParameterNamesToString();
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002A491 File Offset: 0x00028691
		internal static string NameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.Name + operationImport.ParameterTypesToString();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002A4A4 File Offset: 0x000286A4
		internal static string FullNameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.FullName() + operationImport.ParameterTypesToString();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002A4B8 File Offset: 0x000286B8
		internal static IEnumerable<IEdmFunction> RemoveActions(this IEnumerable<IEdmOperation> source, out IList<IEdmAction> actionItems)
		{
			List<IEdmFunction> list = new List<IEdmFunction>();
			actionItems = new List<IEdmAction>();
			foreach (IEdmOperation edmOperation in source)
			{
				if (edmOperation.IsAction())
				{
					actionItems.Add((IEdmAction)edmOperation);
				}
				else
				{
					list.Add((IEdmFunction)edmOperation);
				}
			}
			return list;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002A52C File Offset: 0x0002872C
		internal static IEnumerable<IEdmFunctionImport> RemoveActionImports(this IEnumerable<IEdmOperationImport> source, out IList<IEdmActionImport> actionImportItems)
		{
			List<IEdmFunctionImport> list = new List<IEdmFunctionImport>();
			actionImportItems = new List<IEdmActionImport>();
			foreach (IEdmOperationImport edmOperationImport in source)
			{
				if (edmOperationImport.IsActionImport())
				{
					actionImportItems.Add((IEdmActionImport)edmOperationImport);
				}
				else
				{
					list.Add((IEdmFunctionImport)edmOperationImport);
				}
			}
			return list;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002A5A0 File Offset: 0x000287A0
		internal static bool IsUserModel(this IEdmModel model)
		{
			return !(model is EdmCoreModel);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002A5B0 File Offset: 0x000287B0
		internal static bool IsPrimitiveType(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case 3:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 18:
				return true;
			}
			return EdmLibraryExtensions.PrimitiveTypeReferenceMap.ContainsKey(clrType) || typeof(ISpatial).IsAssignableFrom(clrType);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002A630 File Offset: 0x00028830
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for primitive type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmPrimitiveTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002A650 File Offset: 0x00028850
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for complex type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmComplexTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002A66F File Offset: 0x0002886F
		internal static bool IsAssignableFrom(this IEdmTypeReference baseType, IEdmTypeReference subtype)
		{
			return baseType.Definition.IsAssignableFrom(subtype.Definition);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002A684 File Offset: 0x00028884
		internal static bool IsAssignableFrom(this IEdmType baseType, IEdmType subtype)
		{
			baseType = baseType.AsActualType();
			subtype = subtype.AsActualType();
			EdmTypeKind typeKind = baseType.TypeKind;
			EdmTypeKind typeKind2 = subtype.TypeKind;
			if (typeKind != typeKind2)
			{
				return false;
			}
			switch (typeKind)
			{
			case EdmTypeKind.Primitive:
				return ((IEdmPrimitiveType)baseType).IsAssignableFrom((IEdmPrimitiveType)subtype);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ((IEdmStructuredType)baseType).IsAssignableFrom((IEdmStructuredType)subtype);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)baseType).ElementType.Definition.IsAssignableFrom(((IEdmCollectionType)subtype).ElementType.Definition);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Type));
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002A72C File Offset: 0x0002892C
		internal static IEdmStructuredType GetCommonBaseType(this IEdmStructuredType firstType, IEdmStructuredType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmStructuredType edmStructuredType = firstType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(secondType))
				{
					return edmStructuredType;
				}
			}
			for (IEdmStructuredType edmStructuredType = secondType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(firstType))
				{
					return edmStructuredType;
				}
			}
			return null;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002A778 File Offset: 0x00028978
		internal static IEdmPrimitiveType GetCommonBaseType(this IEdmPrimitiveType firstType, IEdmPrimitiveType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmPrimitiveType edmPrimitiveType = firstType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(secondType))
				{
					return edmPrimitiveType;
				}
			}
			for (IEdmPrimitiveType edmPrimitiveType = secondType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(firstType))
				{
					return edmPrimitiveType;
				}
			}
			return null;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002A7C4 File Offset: 0x000289C4
		internal static IEdmPrimitiveType BaseType(this IEdmPrimitiveType type)
		{
			switch (type.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.None:
			case EdmPrimitiveTypeKind.Binary:
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.String:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.Duration:
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.Date:
			case EdmPrimitiveTypeKind.TimeOfDay:
				return null;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_BaseType));
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002A933 File Offset: 0x00028B33
		internal static IEdmComplexTypeReference AsComplexOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Complex)
			{
				return null;
			}
			return typeReference.AsComplex();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002A94C File Offset: 0x00028B4C
		internal static IEdmCollectionTypeReference AsCollectionOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Collection)
			{
				return null;
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollection();
			if (!edmCollectionTypeReference.IsNonEntityCollectionType())
			{
				return null;
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002A97B File Offset: 0x00028B7B
		internal static IEdmType AsActualType(this IEdmType type)
		{
			if (type.TypeKind != EdmTypeKind.TypeDefinition)
			{
				return type;
			}
			return ((IEdmTypeDefinition)type).UnderlyingType;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002A993 File Offset: 0x00028B93
		internal static bool IsElementTypeEquivalentTo(this IEdmType type, IEdmType other)
		{
			return type.TypeKind == EdmTypeKind.Collection && other.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)type).ElementType.Definition.IsEquivalentTo(((IEdmCollectionType)other).ElementType.Definition);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		internal static object ConvertToUnderlyingTypeIfUIntValue(this IEdmModel model, object value, IEdmTypeReference expectedTypeReference = null)
		{
			if (model == null)
			{
				return value;
			}
			object obj;
			try
			{
				if (expectedTypeReference == null)
				{
					expectedTypeReference = model.ResolveUIntTypeDefinition(value);
				}
				if (expectedTypeReference != null)
				{
					obj = model.GetPrimitiveValueConverter(expectedTypeReference).ConvertToUnderlyingType(value);
				}
				else
				{
					obj = value;
				}
			}
			catch (OverflowException)
			{
				throw new ODataException(Strings.EdmLibraryExtensions_ValueOverflowForUnderlyingType(value, expectedTypeReference.FullName()));
			}
			return obj;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002AA2C File Offset: 0x00028C2C
		internal static ODataPrimitiveValue ConvertToUnderlyingTypeIfUIntValue(this IEdmModel model, ODataPrimitiveValue value, IEdmTypeReference expectedTypeReference = null)
		{
			object obj = model.ConvertToUnderlyingTypeIfUIntValue(value.Value, expectedTypeReference);
			if (obj == value.Value)
			{
				return value;
			}
			return new ODataPrimitiveValue(obj);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002AA80 File Offset: 0x00028C80
		internal static IEdmTypeDefinitionReference ResolveUIntTypeDefinition(this IEdmModel model, object value)
		{
			if (model == null)
			{
				return null;
			}
			if (value == null)
			{
				return null;
			}
			if (!(value is ushort) && !(value is uint) && !(value is ulong))
			{
				return null;
			}
			IEdmTypeDefinition edmTypeDefinition = Enumerable.SingleOrDefault<IEdmSchemaElement>(model.SchemaElements, (IEdmSchemaElement e) => string.CompareOrdinal(e.Name, value.GetType().Name) == 0) as IEdmTypeDefinition;
			if (edmTypeDefinition != null)
			{
				return new EdmTypeDefinitionReference(edmTypeDefinition, true);
			}
			return null;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002AAFB File Offset: 0x00028CFB
		internal static IEdmSchemaType ResolvePrimitiveTypeName(string typeName)
		{
			return EdmCoreModel.Instance.FindDeclaredType(typeName);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002AB08 File Offset: 0x00028D08
		internal static IEdmTypeReference GetCollectionItemType(this IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference.ElementType();
			}
			return null;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002AB28 File Offset: 0x00028D28
		internal static IEdmCollectionType GetCollectionType(IEdmType itemType)
		{
			IEdmTypeReference edmTypeReference = itemType.ToTypeReference();
			return EdmLibraryExtensions.GetCollectionType(edmTypeReference);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002AB42 File Offset: 0x00028D42
		internal static IEdmCollectionType GetCollectionType(IEdmTypeReference itemTypeReference)
		{
			if (!itemTypeReference.IsODataPrimitiveTypeKind() && !itemTypeReference.IsODataComplexTypeKind() && !itemTypeReference.IsODataEnumTypeKind() && !itemTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ODataException(Strings.EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveEnumComplex);
			}
			return new EdmCollectionType(itemTypeReference);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002AB78 File Offset: 0x00028D78
		internal static bool IsGeographyType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		internal static bool IsGeometryType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002AC16 File Offset: 0x00028E16
		internal static string GetCollectionItemTypeName(string typeName)
		{
			return EdmLibraryExtensions.GetCollectionItemTypeName(typeName, false);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002AC20 File Offset: 0x00028E20
		internal static string GetCollectionTypeFullName(string typeName)
		{
			if (typeName != null)
			{
				string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(typeName);
				if (collectionItemTypeName != null)
				{
					IEdmSchemaType edmSchemaType = EdmCoreModel.Instance.FindDeclaredType(collectionItemTypeName);
					if (edmSchemaType != null)
					{
						return EdmLibraryExtensions.GetCollectionTypeName(edmSchemaType.FullName());
					}
				}
			}
			return typeName;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002AC56 File Offset: 0x00028E56
		internal static bool OperationsBoundToEntityTypeMustBeContainerQualified(this IEdmEntityType entityType)
		{
			return entityType.IsOpen;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002AC5E File Offset: 0x00028E5E
		internal static string ODataShortQualifiedName(this IEdmTypeReference typeReference)
		{
			return typeReference.Definition.ODataShortQualifiedName();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002AC6C File Offset: 0x00028E6C
		internal static string ODataShortQualifiedName(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				string text = edmCollectionType.ElementType.ODataShortQualifiedName();
				if (text == null)
				{
					return null;
				}
				return EdmLibraryExtensions.GetCollectionTypeName(text);
			}
			else
			{
				IEdmSchemaElement edmSchemaElement = type as IEdmSchemaElement;
				if (edmSchemaElement == null)
				{
					return null;
				}
				return edmSchemaElement.ShortQualifiedName();
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "The clone logic should stay in one place.")]
		internal static IEdmTypeReference Clone(this IEdmTypeReference typeReference, bool nullable)
		{
			if (typeReference == null)
			{
				return null;
			}
			switch (typeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = typeReference.PrimitiveKind();
				IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)typeReference.Definition;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Binary:
				{
					IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)typeReference;
					return new EdmBinaryTypeReference(edmPrimitiveType, nullable, edmBinaryTypeReference.IsUnbounded, edmBinaryTypeReference.MaxLength);
				}
				case EdmPrimitiveTypeKind.Boolean:
				case EdmPrimitiveTypeKind.Byte:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.SByte:
				case EdmPrimitiveTypeKind.Single:
				case EdmPrimitiveTypeKind.Stream:
				case EdmPrimitiveTypeKind.Date:
					return new EdmPrimitiveTypeReference(edmPrimitiveType, nullable);
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Duration:
				case EdmPrimitiveTypeKind.TimeOfDay:
				{
					IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)typeReference;
					return new EdmTemporalTypeReference(edmPrimitiveType, nullable, edmTemporalTypeReference.Precision);
				}
				case EdmPrimitiveTypeKind.Decimal:
				{
					IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)typeReference;
					return new EdmDecimalTypeReference(edmPrimitiveType, nullable, edmDecimalTypeReference.Precision, edmDecimalTypeReference.Scale);
				}
				case EdmPrimitiveTypeKind.String:
				{
					IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)typeReference;
					return new EdmStringTypeReference(edmPrimitiveType, nullable, edmStringTypeReference.IsUnbounded, edmStringTypeReference.MaxLength, edmStringTypeReference.IsUnicode);
				}
				case EdmPrimitiveTypeKind.Geography:
				case EdmPrimitiveTypeKind.GeographyPoint:
				case EdmPrimitiveTypeKind.GeographyLineString:
				case EdmPrimitiveTypeKind.GeographyPolygon:
				case EdmPrimitiveTypeKind.GeographyCollection:
				case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				case EdmPrimitiveTypeKind.GeographyMultiLineString:
				case EdmPrimitiveTypeKind.GeographyMultiPoint:
				case EdmPrimitiveTypeKind.Geometry:
				case EdmPrimitiveTypeKind.GeometryPoint:
				case EdmPrimitiveTypeKind.GeometryLineString:
				case EdmPrimitiveTypeKind.GeometryPolygon:
				case EdmPrimitiveTypeKind.GeometryCollection:
				case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				case EdmPrimitiveTypeKind.GeometryMultiLineString:
				case EdmPrimitiveTypeKind.GeometryMultiPoint:
				{
					IEdmSpatialTypeReference edmSpatialTypeReference = (IEdmSpatialTypeReference)typeReference;
					return new EdmSpatialTypeReference(edmPrimitiveType, nullable, edmSpatialTypeReference.SpatialReferenceIdentifier);
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_PrimitiveTypeKind));
				}
				break;
			}
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)typeReference.Definition, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)typeReference.Definition, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)typeReference.Definition);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)typeReference.Definition, nullable);
			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference((IEdmEnumType)typeReference.Definition, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_TypeKind));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002AEAC File Offset: 0x000290AC
		internal static string FunctionImportGroupName(this IEnumerable<IEdmOperationImport> functionImportGroup)
		{
			return Enumerable.First<IEdmOperationImport>(functionImportGroup).Name;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002AEC8 File Offset: 0x000290C8
		internal static string OperationGroupFullName(this IEnumerable<IEdmOperation> operationGroup)
		{
			return Enumerable.First<IEdmOperation>(operationGroup).FullName();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002AEE4 File Offset: 0x000290E4
		internal static string OperationImportGroupFullName(this IEnumerable<IEdmOperationImport> operationImportGroup)
		{
			return Enumerable.First<IEdmOperationImport>(operationImportGroup).FullName();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002AF00 File Offset: 0x00029100
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for structured types only.")]
		internal static bool IsAssignableFrom(this IEdmStructuredType baseType, IEdmStructuredType subtype)
		{
			if (baseType.TypeKind != subtype.TypeKind)
			{
				return false;
			}
			if (!baseType.IsODataEntityTypeKind() && !baseType.IsODataComplexTypeKind())
			{
				return false;
			}
			for (IEdmStructuredType edmStructuredType = subtype; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsEquivalentTo(baseType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002AF4C File Offset: 0x0002914C
		internal static bool IsSpatialType(this IEdmPrimitiveType primitiveType)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return true;
			}
			return false;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002AFF4 File Offset: 0x000291F4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Need to keep code together.")]
		internal static bool IsAssignableFrom(this IEdmPrimitiveType baseType, IEdmPrimitiveType subtype)
		{
			if (baseType.IsEquivalentTo(subtype))
			{
				return true;
			}
			if (!baseType.IsSpatialType() || !subtype.IsSpatialType())
			{
				return false;
			}
			EdmPrimitiveTypeKind primitiveKind = baseType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = subtype.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Geography:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geography || primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint;
			case EdmPrimitiveTypeKind.GeographyLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString;
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString;
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint;
			case EdmPrimitiveTypeKind.Geometry:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geometry || primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint;
			case EdmPrimitiveTypeKind.GeometryLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString;
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString;
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Primitive));
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002B158 File Offset: 0x00029358
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002B16B File Offset: 0x0002936B
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002B174 File Offset: 0x00029374
		internal static bool IsOpenType(this IEdmType type)
		{
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.IsOpen;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002B194 File Offset: 0x00029394
		internal static bool IsStream(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002B1B7 File Offset: 0x000293B7
		internal static string FullName(this IEdmEntityContainerElement containerElement)
		{
			return containerElement.Container.Name + "." + containerElement.Name;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002B1D4 File Offset: 0x000293D4
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "cyclomatic complexity")]
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case 3:
				return EdmLibraryExtensions.BooleanTypeReference;
			case 5:
				return EdmLibraryExtensions.SByteTypeReference;
			case 6:
				return EdmLibraryExtensions.ByteTypeReference;
			case 7:
				return EdmLibraryExtensions.Int16TypeReference;
			case 9:
				return EdmLibraryExtensions.Int32TypeReference;
			case 11:
				return EdmLibraryExtensions.Int64TypeReference;
			case 13:
				return EdmLibraryExtensions.SingleTypeReference;
			case 14:
				return EdmLibraryExtensions.DoubleTypeReference;
			case 15:
				return EdmLibraryExtensions.DecimalTypeReference;
			case 18:
				return EdmLibraryExtensions.StringTypeReference;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference;
			if (EdmLibraryExtensions.PrimitiveTypeReferenceMap.TryGetValue(clrType, ref edmPrimitiveTypeReference))
			{
				return edmPrimitiveTypeReference;
			}
			IEdmPrimitiveType edmPrimitiveType = null;
			if (typeof(GeographyPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPoint);
			}
			else if (typeof(GeographyLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyLineString);
			}
			else if (typeof(GeographyPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPolygon);
			}
			else if (typeof(GeographyMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPoint);
			}
			else if (typeof(GeographyMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiLineString);
			}
			else if (typeof(GeographyMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPolygon);
			}
			else if (typeof(GeographyCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			}
			else if (typeof(Geography).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			}
			else if (typeof(GeometryPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPoint);
			}
			else if (typeof(GeometryLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryLineString);
			}
			else if (typeof(GeometryPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPolygon);
			}
			else if (typeof(GeometryMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPoint);
			}
			else if (typeof(GeometryMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiLineString);
			}
			else if (typeof(GeometryMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPolygon);
			}
			else if (typeof(GeometryCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			}
			else if (typeof(Geometry).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			}
			if (edmPrimitiveType == null)
			{
				return null;
			}
			return EdmLibraryExtensions.ToTypeReference(edmPrimitiveType, true);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002B4C4 File Offset: 0x000296C4
		internal static IEdmTypeReference ToTypeReference(this IEdmType type, bool nullable)
		{
			if (type == null)
			{
				return null;
			}
			switch (type.TypeKind)
			{
			case EdmTypeKind.Primitive:
				return EdmLibraryExtensions.ToTypeReference((IEdmPrimitiveType)type, nullable);
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)type, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)type, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)type);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)type, nullable);
			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference((IEdmEnumType)type, nullable);
			case EdmTypeKind.TypeDefinition:
				return new EdmTypeDefinitionReference((IEdmTypeDefinition)type, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_ToTypeReference));
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002B570 File Offset: 0x00029770
		internal static string GetCollectionTypeName(string itemTypeName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { itemTypeName });
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002B598 File Offset: 0x00029798
		internal static IEnumerable<IEdmOperationImport> ResolveOperationImports(this IEdmEntityContainer container, string operationImportName)
		{
			return container.ResolveOperationImports(operationImportName, true);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002B5A4 File Offset: 0x000297A4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "allowParameterTypeNames", Justification = "Used in the ODL version of the method.")]
		internal static IEnumerable<IEdmOperationImport> ResolveOperationImports(this IEdmEntityContainer container, string operationImportName, bool allowParameterTypeNames)
		{
			if (string.IsNullOrEmpty(operationImportName))
			{
				return Enumerable.Empty<IEdmOperationImport>();
			}
			int num = operationImportName.IndexOf('(');
			string text = operationImportName;
			if (num > 0)
			{
				if (!allowParameterTypeNames)
				{
					return Enumerable.Empty<IEdmOperationImport>();
				}
				text = operationImportName.Substring(0, num);
			}
			string text2 = null;
			string text3 = text;
			int num2 = text.LastIndexOf('.');
			if (num2 > -1)
			{
				text3 = text.Substring(num2, text.Length - num2).TrimStart(new char[] { '.' });
				text2 = text.Substring(0, num2);
			}
			if (text2 != null && !container.Name.Equals(text2) && !container.FullName().Equals(text2))
			{
				return Enumerable.Empty<IEdmOperationImport>();
			}
			IEnumerable<IEdmOperationImport> enumerable = container.FindOperationImports(text3);
			if (num > 0)
			{
				return enumerable.FilterByOperationParameterTypes(text, operationImportName);
			}
			return enumerable;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002B664 File Offset: 0x00029864
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Class coupling is with all the primitive Clr types only.")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not too complex for what this method does.")]
		internal static Type GetPrimitiveClrType(IEdmPrimitiveType primitiveType, bool isNullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return typeof(byte[]);
			case EdmPrimitiveTypeKind.Boolean:
				if (!isNullable)
				{
					return typeof(bool);
				}
				return typeof(bool?);
			case EdmPrimitiveTypeKind.Byte:
				if (!isNullable)
				{
					return typeof(byte);
				}
				return typeof(byte?);
			case EdmPrimitiveTypeKind.DateTimeOffset:
				if (!isNullable)
				{
					return typeof(DateTimeOffset);
				}
				return typeof(DateTimeOffset?);
			case EdmPrimitiveTypeKind.Decimal:
				if (!isNullable)
				{
					return typeof(decimal);
				}
				return typeof(decimal?);
			case EdmPrimitiveTypeKind.Double:
				if (!isNullable)
				{
					return typeof(double);
				}
				return typeof(double?);
			case EdmPrimitiveTypeKind.Guid:
				if (!isNullable)
				{
					return typeof(Guid);
				}
				return typeof(Guid?);
			case EdmPrimitiveTypeKind.Int16:
				if (!isNullable)
				{
					return typeof(short);
				}
				return typeof(short?);
			case EdmPrimitiveTypeKind.Int32:
				if (!isNullable)
				{
					return typeof(int);
				}
				return typeof(int?);
			case EdmPrimitiveTypeKind.Int64:
				if (!isNullable)
				{
					return typeof(long);
				}
				return typeof(long?);
			case EdmPrimitiveTypeKind.SByte:
				if (!isNullable)
				{
					return typeof(sbyte);
				}
				return typeof(sbyte?);
			case EdmPrimitiveTypeKind.Single:
				if (!isNullable)
				{
					return typeof(float);
				}
				return typeof(float?);
			case EdmPrimitiveTypeKind.String:
				return typeof(string);
			case EdmPrimitiveTypeKind.Stream:
				return typeof(Stream);
			case EdmPrimitiveTypeKind.Duration:
				if (!isNullable)
				{
					return typeof(TimeSpan);
				}
				return typeof(TimeSpan?);
			case EdmPrimitiveTypeKind.Geography:
				return typeof(Geography);
			case EdmPrimitiveTypeKind.GeographyPoint:
				return typeof(GeographyPoint);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return typeof(GeographyLineString);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return typeof(GeographyPolygon);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return typeof(GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return typeof(GeographyMultiPolygon);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return typeof(GeographyMultiLineString);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return typeof(GeographyMultiPoint);
			case EdmPrimitiveTypeKind.Geometry:
				return typeof(Geometry);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return typeof(GeometryPoint);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return typeof(GeometryLineString);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return typeof(GeometryPolygon);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return typeof(GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return typeof(GeometryMultiPolygon);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return typeof(GeometryMultiLineString);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return typeof(GeometryMultiPoint);
			case EdmPrimitiveTypeKind.Date:
				if (!isNullable)
				{
					return typeof(Date);
				}
				return typeof(Date?);
			case EdmPrimitiveTypeKind.TimeOfDay:
				if (!isNullable)
				{
					return typeof(TimeOfDay);
				}
				return typeof(TimeOfDay?);
			default:
				return null;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002BB4C File Offset: 0x00029D4C
		private static IEnumerable<IEdmOperation> ValidateOperationGroupReturnsOnlyOnKind(IEnumerable<IEdmOperation> operations, string operationNameWithoutParameterTypes)
		{
			EdmSchemaElementKind? operationKind = default(EdmSchemaElementKind?);
			foreach (IEdmOperation operation in operations)
			{
				if (operationKind == null)
				{
					operationKind = new EdmSchemaElementKind?(operation.SchemaElementKind);
				}
				else if (operation.SchemaElementKind != operationKind)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid(operationNameWithoutParameterTypes));
				}
				yield return operation;
			}
			yield break;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002BB80 File Offset: 0x00029D80
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "This method is used for matching the name of the operation to something written by the server. So using the name is safe without resolving the type from the client.")]
		private static string ParameterTypesToString(this IEdmOperation operation)
		{
			return '(' + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(operation.Parameters, (IEdmOperationParameter p) => p.Type.FullName()))) + ')';
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002BBE0 File Offset: 0x00029DE0
		private static string NonBindingParameterNamesToString(this IEdmOperation operation)
		{
			IEnumerable<IEdmOperationParameter> enumerable = (operation.IsBound ? Enumerable.Skip<IEdmOperationParameter>(operation.Parameters, 1) : operation.Parameters);
			return '(' + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(enumerable, (IEdmOperationParameter p) => p.Name))) + ')';
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002BC50 File Offset: 0x00029E50
		private static string GetCollectionItemTypeName(string typeName, bool isNested)
		{
			int length = "Collection".Length;
			if (typeName == null || !typeName.StartsWith("Collection(", 4) || typeName.get_Chars(typeName.Length - 1) != ')' || typeName.Length == length + 2)
			{
				return null;
			}
			if (isNested)
			{
				throw new ODataException(Strings.ValidationUtils_NestedCollectionsAreNotSupported);
			}
			string text = typeName.Substring(length + 1, typeName.Length - (length + 2));
			EdmLibraryExtensions.GetCollectionItemTypeName(text, true);
			return text;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002BCD0 File Offset: 0x00029ED0
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "This method is used for matching the name of the operation import to something written by the server. So using the name is safe without resolving the type from the client.")]
		private static string ParameterTypesToString(this IEdmOperationImport operationImport)
		{
			return '(' + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(operationImport.Operation.Parameters, (IEdmOperationParameter p) => p.Type.FullName()))) + ')';
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002BF88 File Offset: 0x0002A188
		private static IEnumerable<IEdmOperationImport> FilterByOperationParameterTypes(this IEnumerable<IEdmOperationImport> operationImports, string operationNameWithoutParameterTypes, string originalFullOperationImportName)
		{
			foreach (IEdmOperationImport operationImport in operationImports)
			{
				if (operationNameWithoutParameterTypes.IndexOf(".", 4) > -1)
				{
					if (operationImport.FullNameWithParameters().Equals(originalFullOperationImportName, 4) || (operationImport.Container.Name + "." + operationImport.NameWithParameters()).Equals(originalFullOperationImportName, 4))
					{
						yield return operationImport;
					}
				}
				else if (operationImport.NameWithParameters().Equals(originalFullOperationImportName, 4))
				{
					yield return operationImport;
				}
			}
			yield break;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		private static int InheritanceLevelFromSpecifiedInheritedType(this IEdmStructuredType structuredType, IEdmStructuredType rootType)
		{
			IEdmStructuredType edmStructuredType = structuredType;
			int num = 0;
			while (edmStructuredType.InheritsFrom(rootType))
			{
				edmStructuredType = edmStructuredType.BaseType;
				num++;
			}
			return num;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		private static EdmPrimitiveTypeReference ToTypeReference(IEdmPrimitiveType primitiveType, bool nullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return new EdmBinaryTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.Date:
				return new EdmPrimitiveTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Duration:
			case EdmPrimitiveTypeKind.TimeOfDay:
				return new EdmTemporalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.Decimal:
				return new EdmDecimalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.String:
				return new EdmStringTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return new EdmSpatialTypeReference(primitiveType, nullable);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_PrimitiveTypeReference));
			}
		}

		// Token: 0x040004AA RID: 1194
		private const string CollectionTypeQualifier = "Collection";

		// Token: 0x040004AB RID: 1195
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x040004AC RID: 1196
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);

		// Token: 0x040004AD RID: 1197
		private static readonly EdmPrimitiveTypeReference BooleanTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false);

		// Token: 0x040004AE RID: 1198
		private static readonly EdmPrimitiveTypeReference ByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false);

		// Token: 0x040004AF RID: 1199
		private static readonly EdmPrimitiveTypeReference DecimalTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false);

		// Token: 0x040004B0 RID: 1200
		private static readonly EdmPrimitiveTypeReference DoubleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false);

		// Token: 0x040004B1 RID: 1201
		private static readonly EdmPrimitiveTypeReference Int16TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false);

		// Token: 0x040004B2 RID: 1202
		private static readonly EdmPrimitiveTypeReference Int32TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false);

		// Token: 0x040004B3 RID: 1203
		private static readonly EdmPrimitiveTypeReference Int64TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false);

		// Token: 0x040004B4 RID: 1204
		private static readonly EdmPrimitiveTypeReference SByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false);

		// Token: 0x040004B5 RID: 1205
		private static readonly EdmPrimitiveTypeReference StringTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true);

		// Token: 0x040004B6 RID: 1206
		private static readonly EdmPrimitiveTypeReference SingleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false);

		// Token: 0x0200012C RID: 300
		private sealed class EdmTypeEqualityComparer : IEqualityComparer<IEdmType>
		{
			// Token: 0x06000B85 RID: 2949 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
			public bool Equals(IEdmType x, IEdmType y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x0002C0C9 File Offset: 0x0002A2C9
			public int GetHashCode(IEdmType obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
