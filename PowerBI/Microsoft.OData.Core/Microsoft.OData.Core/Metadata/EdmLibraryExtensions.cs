using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000FB RID: 251
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "The class coupling is due to mapping primitive types, lot of different types there.")]
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Following EdmLib standards.")]
	internal static class EdmLibraryExtensions
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x00023058 File Offset: 0x00021258
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

		// Token: 0x06000E9C RID: 3740 RVA: 0x00023563 File Offset: 0x00021763
		internal static IEnumerable<IEdmOperationImport> FilterOperationsByParameterNames(this IEnumerable<IEdmOperationImport> operationImports, IEnumerable<string> parameterNames, bool caseInsensitive)
		{
			IList<string> parameterNameList = parameterNames.ToList<string>();
			foreach (IEdmOperationImport edmOperationImport in operationImports)
			{
				if (EdmLibraryExtensions.ParametersSatisfyFunction(edmOperationImport.Operation, parameterNameList, caseInsensitive))
				{
					yield return edmOperationImport;
				}
			}
			IEnumerator<IEdmOperationImport> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00023584 File Offset: 0x00021784
		internal static IEnumerable<IEdmOperationImport> FindBestOverloadBasedOnParameters(this IEnumerable<IEdmOperationImport> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			IEnumerable<IEdmOperationImport> enumerable = functions.Where((IEdmOperationImport f) => f.Operation.Parameters.Count<IEdmOperationParameter>() == parameters.Count<string>());
			if (enumerable.Count<IEdmOperationImport>() <= 0)
			{
				return functions;
			}
			return enumerable;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000235C0 File Offset: 0x000217C0
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
				if (edmOperation.IsBound)
				{
					IEdmOperationParameter edmOperationParameter = edmOperation.Parameters.FirstOrDefault<IEdmOperationParameter>();
					if (edmOperationParameter != null)
					{
						IEdmType definition = edmOperationParameter.Type.Definition;
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
			}
			if (edmType != null)
			{
				return dictionary[edmType];
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00023704 File Offset: 0x00021904
		internal static IEnumerable<IEdmOperation> FindBestOverloadBasedOnParameters(this IEnumerable<IEdmOperation> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			IEnumerable<IEdmOperation> enumerable = functions.Where((IEdmOperation f) => f.Parameters.Count<IEdmOperationParameter>() == parameters.Count<string>() + (f.IsBound ? 1 : 0));
			if (enumerable.Count<IEdmOperation>() <= 0)
			{
				return functions;
			}
			return enumerable;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002373D File Offset: 0x0002193D
		internal static IEnumerable<IEdmOperation> FilterOperationsByParameterNames(this IEnumerable<IEdmOperation> operations, IEnumerable<string> parameters, bool caseInsensitive)
		{
			IList<string> parameterNameList = parameters.ToList<string>();
			foreach (IEdmOperation edmOperation in operations)
			{
				if (EdmLibraryExtensions.ParametersSatisfyFunction(edmOperation, parameterNameList, caseInsensitive))
				{
					yield return edmOperation;
				}
			}
			IEnumerator<IEdmOperation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002375C File Offset: 0x0002195C
		internal static void EnsureOperationsBoundWithBindingParameter(this IEnumerable<IEdmOperation> operations)
		{
			foreach (IEdmOperation edmOperation in operations)
			{
				if (!edmOperation.IsBound)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
				if (edmOperation.Parameters.FirstOrDefault<IEdmOperationParameter>() == null)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000237D4 File Offset: 0x000219D4
		internal static IEnumerable<IEdmOperation> ResolveOperations(this IEdmModel model, string namespaceQualifiedOperationName)
		{
			return model.ResolveOperations(namespaceQualifiedOperationName, true);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000237E0 File Offset: 0x000219E0
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
				return enumerable.Where((IEdmOperation f) => (f.IsFunction() && f.FullNameWithNonBindingParameters().Equals(operationName, StringComparison.Ordinal)) || f.IsAction());
			}
			return EdmLibraryExtensions.ValidateOperationGroupReturnsOnlyOnKind(enumerable, text);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002386E File Offset: 0x00021A6E
		internal static string NameWithParameters(this IEdmOperation operation)
		{
			return operation.Name + operation.ParameterTypesToString();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00023881 File Offset: 0x00021A81
		internal static string FullNameWithParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.ParameterTypesToString();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00023894 File Offset: 0x00021A94
		internal static string FullNameWithNonBindingParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.NonBindingParameterNamesToString();
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000238A7 File Offset: 0x00021AA7
		internal static string NameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.Name + operationImport.ParameterTypesToString();
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000238BA File Offset: 0x00021ABA
		internal static string FullNameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.FullName() + operationImport.ParameterTypesToString();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000238D0 File Offset: 0x00021AD0
		internal static IEnumerable<IEdmOperation> RemoveActions(this IEnumerable<IEdmOperation> source, out IList<IEdmOperation> actionItems)
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			actionItems = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in source)
			{
				if (edmOperation.IsAction())
				{
					actionItems.Add(edmOperation);
				}
				else
				{
					list.Add(edmOperation);
				}
			}
			return list;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00023938 File Offset: 0x00021B38
		internal static IEnumerable<IEdmOperationImport> RemoveActionImports(this IEnumerable<IEdmOperationImport> source, out IList<IEdmOperationImport> actionImportItems)
		{
			List<IEdmOperationImport> list = new List<IEdmOperationImport>();
			actionImportItems = new List<IEdmOperationImport>();
			foreach (IEdmOperationImport edmOperationImport in source)
			{
				if (edmOperationImport.IsActionImport())
				{
					actionImportItems.Add(edmOperationImport);
				}
				else
				{
					list.Add(edmOperationImport);
				}
			}
			return list;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000239A0 File Offset: 0x00021BA0
		internal static bool IsUserModel(this IEdmModel model)
		{
			return !(model is EdmCoreModel);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000239B0 File Offset: 0x00021BB0
		internal static bool IsPrimitiveType(Type clrType)
		{
			return clrType == typeof(ushort) || clrType == typeof(uint) || clrType == typeof(ulong) || EdmLibraryExtensions.PrimitiveTypeReferenceMap.ContainsKey(clrType) || typeof(ISpatial).IsAssignableFrom(clrType);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00023A08 File Offset: 0x00021C08
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for primitive type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmPrimitiveTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00023A28 File Offset: 0x00021C28
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for complex type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmComplexTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00023A47 File Offset: 0x00021C47
		internal static bool IsAssignableFrom(this IEdmTypeReference baseType, IEdmTypeReference subtype)
		{
			return baseType.Definition.IsAssignableFrom(subtype.Definition);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00023A5C File Offset: 0x00021C5C
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
			case EdmTypeKind.Untyped:
				return ((IEdmStructuredType)baseType).IsAssignableFrom((IEdmStructuredType)subtype);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)baseType).ElementType.Definition.IsAssignableFrom(((IEdmCollectionType)subtype).ElementType.Definition);
			case EdmTypeKind.Enum:
				return baseType.IsEquivalentTo(subtype);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Type));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00023B1C File Offset: 0x00021D1C
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

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00023B68 File Offset: 0x00021D68
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

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00023BB4 File Offset: 0x00021DB4
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

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00023D24 File Offset: 0x00021F24
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

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00023D53 File Offset: 0x00021F53
		internal static bool IsElementTypeEquivalentTo(this IEdmType type, IEdmType other)
		{
			return type.TypeKind == EdmTypeKind.Collection && other.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)type).ElementType.Definition.IsEquivalentTo(((IEdmCollectionType)other).ElementType.Definition);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00023D90 File Offset: 0x00021F90
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

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00023DEC File Offset: 0x00021FEC
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
			IEdmTypeDefinition edmTypeDefinition = model.SchemaElements.SingleOrDefault((IEdmSchemaElement e) => string.CompareOrdinal(e.Name, value.GetType().Name) == 0) as IEdmTypeDefinition;
			if (edmTypeDefinition != null)
			{
				return new EdmTypeDefinitionReference(edmTypeDefinition, true);
			}
			return null;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00023E67 File Offset: 0x00022067
		internal static IEdmSchemaType ResolvePrimitiveTypeName(string typeName)
		{
			return EdmCoreModel.Instance.FindDeclaredType(typeName);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00023E74 File Offset: 0x00022074
		internal static IEdmTypeReference GetCollectionItemType(this IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference.ElementType();
			}
			return null;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00023E94 File Offset: 0x00022094
		internal static IEdmCollectionType GetCollectionType(IEdmType itemType)
		{
			IEdmTypeReference edmTypeReference = itemType.ToTypeReference(true);
			return EdmLibraryExtensions.GetCollectionType(edmTypeReference);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00023EAF File Offset: 0x000220AF
		internal static IEdmCollectionType GetCollectionType(IEdmTypeReference itemTypeReference)
		{
			return new EdmCollectionType(itemTypeReference);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00023EB7 File Offset: 0x000220B7
		internal static string GetCollectionItemTypeName(string typeName)
		{
			return EdmLibraryExtensions.GetCollectionItemTypeName(typeName, false);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00023EC0 File Offset: 0x000220C0
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

		// Token: 0x06000EBE RID: 3774 RVA: 0x00023EF6 File Offset: 0x000220F6
		internal static bool OperationsBoundToStructuredTypeMustBeContainerQualified(this IEdmStructuredType structuredType)
		{
			return structuredType.IsOpen;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00023EFE File Offset: 0x000220FE
		internal static string ODataShortQualifiedName(this IEdmTypeReference typeReference)
		{
			return typeReference.Definition.ODataShortQualifiedName();
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00023F0C File Offset: 0x0002210C
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

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00023F50 File Offset: 0x00022150
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
				case EdmPrimitiveTypeKind.PrimitiveType:
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

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00024148 File Offset: 0x00022348
		internal static string OperationGroupFullName(this IEnumerable<IEdmOperation> operationGroup)
		{
			return operationGroup.First<IEdmOperation>().FullName();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00024164 File Offset: 0x00022364
		internal static string OperationImportGroupFullName(this IEnumerable<IEdmOperationImport> operationImportGroup)
		{
			return operationImportGroup.First<IEdmOperationImport>().FullName();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00024180 File Offset: 0x00022380
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for structured types only.")]
		internal static bool IsAssignableFrom(this IEdmStructuredType baseType, IEdmStructuredType subtype)
		{
			if (baseType.TypeKind == EdmTypeKind.Untyped)
			{
				return true;
			}
			if (baseType.TypeKind != subtype.TypeKind)
			{
				return false;
			}
			if (subtype.IsEquivalentTo(baseType))
			{
				return true;
			}
			if (baseType == EdmCoreModel.Instance.GetComplexType() || baseType == EdmCoreModel.Instance.GetEntityType())
			{
				return true;
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

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000241FC File Offset: 0x000223FC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Need to keep code together.")]
		internal static bool IsAssignableFrom(this IEdmPrimitiveType baseType, IEdmPrimitiveType subtype)
		{
			if (baseType.IsEquivalentTo(subtype))
			{
				return true;
			}
			if (baseType.PrimitiveKind == EdmPrimitiveTypeKind.PrimitiveType)
			{
				return true;
			}
			if (!baseType.IsSpatial() || !subtype.IsSpatial())
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

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002436A File Offset: 0x0002256A
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002437D File Offset: 0x0002257D
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00024386 File Offset: 0x00022586
		internal static string FullName(this IEdmEntityContainerElement containerElement)
		{
			return containerElement.Container.Name + "." + containerElement.Name;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000243A4 File Offset: 0x000225A4
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "cyclomatic complexity")]
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference;
			if (EdmLibraryExtensions.PrimitiveTypeReferenceMap.TryGetValue(clrType, out edmPrimitiveTypeReference))
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

		// Token: 0x06000ECA RID: 3786 RVA: 0x00024604 File Offset: 0x00022804
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
			case EdmTypeKind.Untyped:
			{
				IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
				if (edmStructuredType != null)
				{
					return new EdmUntypedStructuredTypeReference(edmStructuredType);
				}
				return new EdmUntypedTypeReference((IEdmUntypedType)type);
			}
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_ToTypeReference));
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000246D0 File Offset: 0x000228D0
		internal static string GetCollectionTypeName(string itemTypeName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { itemTypeName });
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000246EB File Offset: 0x000228EB
		internal static IEnumerable<IEdmOperationImport> ResolveOperationImports(this IEdmEntityContainer container, string operationImportName)
		{
			return container.ResolveOperationImports(operationImportName, true);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000246F8 File Offset: 0x000228F8
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

		// Token: 0x06000ECE RID: 3790 RVA: 0x000247B4 File Offset: 0x000229B4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Not too complex for what this method does.")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Class coupling is with all the primitive Clr types only.")]
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

		// Token: 0x06000ECF RID: 3791 RVA: 0x00024A89 File Offset: 0x00022C89
		private static IEnumerable<IEdmOperation> ValidateOperationGroupReturnsOnlyOnKind(IEnumerable<IEdmOperation> operations, string operationNameWithoutParameterTypes)
		{
			EdmSchemaElementKind? operationKind = null;
			foreach (IEdmOperation edmOperation in operations)
			{
				if (operationKind == null)
				{
					operationKind = new EdmSchemaElementKind?(edmOperation.SchemaElementKind);
				}
				else if (edmOperation.SchemaElementKind != operationKind)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_OperationGroupReturningActionsAndFunctionsModelInvalid(operationNameWithoutParameterTypes));
				}
				yield return edmOperation;
			}
			IEnumerator<IEdmOperation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00024AA0 File Offset: 0x00022CA0
		private static string ParameterTypesToString(this IEdmOperation operation)
		{
			return "(" + string.Join(",", operation.Parameters.Select((IEdmOperationParameter p) => p.Type.FullName()).ToArray<string>()) + ")";
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00024AF8 File Offset: 0x00022CF8
		private static string NonBindingParameterNamesToString(this IEdmOperation operation)
		{
			IEnumerable<IEdmOperationParameter> enumerable = (operation.IsBound ? operation.Parameters.Skip(1) : operation.Parameters);
			return "(" + string.Join(",", enumerable.Select((IEdmOperationParameter p) => p.Name).ToArray<string>()) + ")";
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00024B68 File Offset: 0x00022D68
		private static string GetCollectionItemTypeName(string typeName, bool isNested)
		{
			int length = "Collection".Length;
			if (typeName == null || !typeName.StartsWith("Collection(", StringComparison.Ordinal) || typeName[typeName.Length - 1] != ')' || typeName.Length == length + 2)
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

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00024BDC File Offset: 0x00022DDC
		private static string ParameterTypesToString(this IEdmOperationImport operationImport)
		{
			return "(" + string.Join(",", operationImport.Operation.Parameters.Select((IEdmOperationParameter p) => p.Type.FullName()).ToArray<string>()) + ")";
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00024C36 File Offset: 0x00022E36
		private static IEnumerable<IEdmOperationImport> FilterByOperationParameterTypes(this IEnumerable<IEdmOperationImport> operationImports, string operationNameWithoutParameterTypes, string originalFullOperationImportName)
		{
			foreach (IEdmOperationImport operationImport in operationImports)
			{
				if (operationNameWithoutParameterTypes.IndexOf(".", StringComparison.Ordinal) > -1)
				{
					if (operationImport.FullNameWithParameters().Equals(originalFullOperationImportName, StringComparison.Ordinal) || (operationImport.Container.Name + "." + operationImport.NameWithParameters()).Equals(originalFullOperationImportName, StringComparison.Ordinal))
					{
						yield return operationImport;
					}
				}
				else if (operationImport.NameWithParameters().Equals(originalFullOperationImportName, StringComparison.Ordinal))
				{
					yield return operationImport;
				}
				operationImport = null;
			}
			IEnumerator<IEdmOperationImport> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00024C54 File Offset: 0x00022E54
		private static bool ParametersSatisfyFunction(IEdmOperation operation, IList<string> parameterNameList, bool caseInsensitive)
		{
			IEnumerable<IEdmOperationParameter> enumerable = operation.Parameters;
			if (operation.IsBound)
			{
				enumerable = enumerable.Skip(1);
			}
			List<IEdmOperationParameter> functionParameters = enumerable.ToList<IEdmOperationParameter>();
			return !functionParameters.Where((IEdmOperationParameter p) => !(p is IEdmOptionalParameter)).Any((IEdmOperationParameter p) => parameterNameList.All((string k) => !string.Equals(k, p.Name, caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))) && !parameterNameList.Any((string k) => functionParameters.All((IEdmOperationParameter p) => !string.Equals(k, p.Name, caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)));
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00024CF4 File Offset: 0x00022EF4
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

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00024D1C File Offset: 0x00022F1C
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
			case EdmPrimitiveTypeKind.PrimitiveType:
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

		// Token: 0x04000754 RID: 1876
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);

		// Token: 0x04000755 RID: 1877
		private static readonly EdmPrimitiveTypeReference BooleanTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false);

		// Token: 0x04000756 RID: 1878
		private static readonly EdmPrimitiveTypeReference ByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false);

		// Token: 0x04000757 RID: 1879
		private static readonly EdmPrimitiveTypeReference DecimalTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false);

		// Token: 0x04000758 RID: 1880
		private static readonly EdmPrimitiveTypeReference DoubleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false);

		// Token: 0x04000759 RID: 1881
		private static readonly EdmPrimitiveTypeReference Int16TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false);

		// Token: 0x0400075A RID: 1882
		private static readonly EdmPrimitiveTypeReference Int32TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false);

		// Token: 0x0400075B RID: 1883
		private static readonly EdmPrimitiveTypeReference Int64TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false);

		// Token: 0x0400075C RID: 1884
		private static readonly EdmPrimitiveTypeReference SByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false);

		// Token: 0x0400075D RID: 1885
		private static readonly EdmPrimitiveTypeReference StringTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true);

		// Token: 0x0400075E RID: 1886
		private static readonly EdmPrimitiveTypeReference SingleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false);

		// Token: 0x0400075F RID: 1887
		private const string CollectionTypeQualifier = "Collection";

		// Token: 0x04000760 RID: 1888
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x02000365 RID: 869
		private sealed class EdmTypeEqualityComparer : IEqualityComparer<IEdmType>
		{
			// Token: 0x06001EDE RID: 7902 RVA: 0x00059A43 File Offset: 0x00057C43
			public bool Equals(IEdmType x, IEdmType y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x06001EDF RID: 7903 RVA: 0x00059A4C File Offset: 0x00057C4C
			public int GetHashCode(IEdmType obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
