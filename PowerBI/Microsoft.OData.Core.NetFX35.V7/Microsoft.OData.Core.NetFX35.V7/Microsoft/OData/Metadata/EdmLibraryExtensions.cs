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
	// Token: 0x020001D3 RID: 467
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "The class coupling is due to mapping primitive types, lot of different types there.")]
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Edm", Justification = "Following EdmLib standards.")]
	internal static class EdmLibraryExtensions
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00032F9C File Offset: 0x0003119C
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

		// Token: 0x0600122F RID: 4655 RVA: 0x000334A7 File Offset: 0x000316A7
		internal static IEnumerable<IEdmFunctionImport> FilterFunctionsByParameterNames(this IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<string> parameterNames, bool caseInsensitive)
		{
			IList<string> parameterNameList = Enumerable.ToList<string>(parameterNames);
			foreach (IEdmFunctionImport edmFunctionImport in functionImports)
			{
				if (EdmLibraryExtensions.ParametersSatisfyFunction(edmFunctionImport.Operation, parameterNameList, caseInsensitive))
				{
					yield return edmFunctionImport;
				}
			}
			IEnumerator<IEdmFunctionImport> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000334C8 File Offset: 0x000316C8
		internal static IEnumerable<IEdmOperationImport> FindBestOverloadBasedOnParameters(this IEnumerable<IEdmOperationImport> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			IEnumerable<IEdmOperationImport> enumerable = Enumerable.Where<IEdmOperationImport>(functions, (IEdmOperationImport f) => Enumerable.Count<IEdmOperationParameter>(f.Operation.Parameters) == Enumerable.Count<string>(parameters));
			if (Enumerable.Count<IEdmOperationImport>(enumerable) <= 0)
			{
				return functions;
			}
			return enumerable;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00033504 File Offset: 0x00031704
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

		// Token: 0x06001232 RID: 4658 RVA: 0x0003364C File Offset: 0x0003184C
		internal static IEnumerable<IEdmOperation> FindBestOverloadBasedOnParameters(this IEnumerable<IEdmOperation> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			IEnumerable<IEdmOperation> enumerable = Enumerable.Where<IEdmOperation>(functions, (IEdmOperation f) => Enumerable.Count<IEdmOperationParameter>(f.Parameters) == Enumerable.Count<string>(parameters) + (f.IsBound ? 1 : 0));
			if (Enumerable.Count<IEdmOperation>(enumerable) <= 0)
			{
				return functions;
			}
			return enumerable;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00033685 File Offset: 0x00031885
		internal static IEnumerable<IEdmFunction> FilterFunctionsByParameterNames(this IEnumerable<IEdmFunction> functions, IEnumerable<string> parameters, bool caseInsensitive = false)
		{
			return Enumerable.Cast<IEdmFunction>(Enumerable.Cast<IEdmOperation>(functions).FilterOperationsByParameterNames(parameters, caseInsensitive));
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00033699 File Offset: 0x00031899
		internal static IEnumerable<IEdmOperation> FilterOperationsByParameterNames(this IEnumerable<IEdmOperation> operations, IEnumerable<string> parameters, bool caseInsensitive)
		{
			IList<string> parameterNameList = Enumerable.ToList<string>(parameters);
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

		// Token: 0x06001235 RID: 4661 RVA: 0x000336B7 File Offset: 0x000318B7
		internal static IEnumerable<IEdmOperation> EnsureOperationsBoundWithBindingParameter(this IEnumerable<IEdmOperation> operations)
		{
			foreach (IEdmOperation edmOperation in operations)
			{
				if (!edmOperation.IsBound)
				{
					throw new ODataException(Strings.EdmLibraryExtensions_UnBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
				if (!Enumerable.Any<IEdmOperationParameter>(edmOperation.Parameters))
				{
					throw new ODataException(Strings.EdmLibraryExtensions_NoParameterBoundOperationsFoundFromIEdmModelFindMethodIsInvalid(edmOperation.Name));
				}
				yield return edmOperation;
			}
			IEnumerator<IEdmOperation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000336C7 File Offset: 0x000318C7
		internal static IEnumerable<IEdmOperation> ResolveOperations(this IEdmModel model, string namespaceQualifiedOperationName)
		{
			return model.ResolveOperations(namespaceQualifiedOperationName, true);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000336D4 File Offset: 0x000318D4
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

		// Token: 0x06001238 RID: 4664 RVA: 0x00033762 File Offset: 0x00031962
		internal static string NameWithParameters(this IEdmOperation operation)
		{
			return operation.Name + operation.ParameterTypesToString();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00033775 File Offset: 0x00031975
		internal static string FullNameWithParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.ParameterTypesToString();
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00033788 File Offset: 0x00031988
		internal static string FullNameWithNonBindingParameters(this IEdmOperation operation)
		{
			return operation.FullName() + operation.NonBindingParameterNamesToString();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0003379B File Offset: 0x0003199B
		internal static string NameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.Name + operationImport.ParameterTypesToString();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000337AE File Offset: 0x000319AE
		internal static string FullNameWithParameters(this IEdmOperationImport operationImport)
		{
			return operationImport.FullName() + operationImport.ParameterTypesToString();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000337C4 File Offset: 0x000319C4
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

		// Token: 0x0600123E RID: 4670 RVA: 0x00033838 File Offset: 0x00031A38
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

		// Token: 0x0600123F RID: 4671 RVA: 0x000338AC File Offset: 0x00031AAC
		internal static bool IsUserModel(this IEdmModel model)
		{
			return !(model is EdmCoreModel);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000338BC File Offset: 0x00031ABC
		internal static bool IsPrimitiveType(Type clrType)
		{
			return clrType == typeof(ushort) || clrType == typeof(uint) || clrType == typeof(ulong) || EdmLibraryExtensions.PrimitiveTypeReferenceMap.ContainsKey(clrType) || typeof(ISpatial).IsAssignableFrom(clrType);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00033914 File Offset: 0x00031B14
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for primitive type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmPrimitiveTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00033934 File Offset: 0x00031B34
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Extension method for complex type references only.")]
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmComplexTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00033953 File Offset: 0x00031B53
		internal static bool IsAssignableFrom(this IEdmTypeReference baseType, IEdmTypeReference subtype)
		{
			return baseType.Definition.IsAssignableFrom(subtype.Definition);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00033968 File Offset: 0x00031B68
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
			case EdmTypeKind.Enum:
				return baseType.IsEquivalentTo(subtype);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Type));
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00033A20 File Offset: 0x00031C20
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

		// Token: 0x06001246 RID: 4678 RVA: 0x00033A6C File Offset: 0x00031C6C
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

		// Token: 0x06001247 RID: 4679 RVA: 0x00033AB8 File Offset: 0x00031CB8
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

		// Token: 0x06001248 RID: 4680 RVA: 0x00033C28 File Offset: 0x00031E28
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

		// Token: 0x06001249 RID: 4681 RVA: 0x00033C57 File Offset: 0x00031E57
		internal static bool IsElementTypeEquivalentTo(this IEdmType type, IEdmType other)
		{
			return type.TypeKind == EdmTypeKind.Collection && other.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)type).ElementType.Definition.IsEquivalentTo(((IEdmCollectionType)other).ElementType.Definition);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00033C94 File Offset: 0x00031E94
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

		// Token: 0x0600124B RID: 4683 RVA: 0x00033CF0 File Offset: 0x00031EF0
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

		// Token: 0x0600124C RID: 4684 RVA: 0x00033D6B File Offset: 0x00031F6B
		internal static IEdmSchemaType ResolvePrimitiveTypeName(string typeName)
		{
			return EdmCoreModel.Instance.FindDeclaredType(typeName);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00033D78 File Offset: 0x00031F78
		internal static IEdmTypeReference GetCollectionItemType(this IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference.ElementType();
			}
			return null;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00033D98 File Offset: 0x00031F98
		internal static IEdmCollectionType GetCollectionType(IEdmType itemType)
		{
			IEdmTypeReference edmTypeReference = itemType.ToTypeReference(true);
			return EdmLibraryExtensions.GetCollectionType(edmTypeReference);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00033DB3 File Offset: 0x00031FB3
		internal static IEdmCollectionType GetCollectionType(IEdmTypeReference itemTypeReference)
		{
			return new EdmCollectionType(itemTypeReference);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00033DBB File Offset: 0x00031FBB
		internal static string GetCollectionItemTypeName(string typeName)
		{
			return EdmLibraryExtensions.GetCollectionItemTypeName(typeName, false);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00033DC4 File Offset: 0x00031FC4
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

		// Token: 0x06001252 RID: 4690 RVA: 0x00033DFA File Offset: 0x00031FFA
		internal static bool OperationsBoundToStructuredTypeMustBeContainerQualified(this IEdmStructuredType structuredType)
		{
			return structuredType.IsOpen;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00033E02 File Offset: 0x00032002
		internal static string ODataShortQualifiedName(this IEdmTypeReference typeReference)
		{
			return typeReference.Definition.ODataShortQualifiedName();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00033E10 File Offset: 0x00032010
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

		// Token: 0x06001255 RID: 4693 RVA: 0x00033E54 File Offset: 0x00032054
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

		// Token: 0x06001256 RID: 4694 RVA: 0x00034048 File Offset: 0x00032248
		internal static string OperationGroupFullName(this IEnumerable<IEdmOperation> operationGroup)
		{
			return Enumerable.First<IEdmOperation>(operationGroup).FullName();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00034064 File Offset: 0x00032264
		internal static string OperationImportGroupFullName(this IEnumerable<IEdmOperationImport> operationImportGroup)
		{
			return Enumerable.First<IEdmOperationImport>(operationImportGroup).FullName();
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00034080 File Offset: 0x00032280
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

		// Token: 0x06001259 RID: 4697 RVA: 0x000340E0 File Offset: 0x000322E0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Need to keep code together.")]
		internal static bool IsAssignableFrom(this IEdmPrimitiveType baseType, IEdmPrimitiveType subtype)
		{
			if (baseType.IsEquivalentTo(subtype))
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

		// Token: 0x0600125A RID: 4698 RVA: 0x00034242 File Offset: 0x00032442
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00034255 File Offset: 0x00032455
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003425E File Offset: 0x0003245E
		internal static string FullName(this IEdmEntityContainerElement containerElement)
		{
			return containerElement.Container.Name + "." + containerElement.Name;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003427C File Offset: 0x0003247C
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "cyclomatic complexity")]
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
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

		// Token: 0x0600125E RID: 4702 RVA: 0x000344DC File Offset: 0x000326DC
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

		// Token: 0x0600125F RID: 4703 RVA: 0x000345A8 File Offset: 0x000327A8
		internal static string GetCollectionTypeName(string itemTypeName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { itemTypeName });
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000345C3 File Offset: 0x000327C3
		internal static IEnumerable<IEdmOperationImport> ResolveOperationImports(this IEdmEntityContainer container, string operationImportName)
		{
			return container.ResolveOperationImports(operationImportName, true);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000345D0 File Offset: 0x000327D0
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

		// Token: 0x06001262 RID: 4706 RVA: 0x0003468C File Offset: 0x0003288C
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

		// Token: 0x06001263 RID: 4707 RVA: 0x00034961 File Offset: 0x00032B61
		private static IEnumerable<IEdmOperation> ValidateOperationGroupReturnsOnlyOnKind(IEnumerable<IEdmOperation> operations, string operationNameWithoutParameterTypes)
		{
			EdmSchemaElementKind? operationKind = default(EdmSchemaElementKind?);
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

		// Token: 0x06001264 RID: 4708 RVA: 0x00034978 File Offset: 0x00032B78
		private static string ParameterTypesToString(this IEdmOperation operation)
		{
			return "(" + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(operation.Parameters, (IEdmOperationParameter p) => p.Type.FullName()))) + ")";
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000349D0 File Offset: 0x00032BD0
		private static string NonBindingParameterNamesToString(this IEdmOperation operation)
		{
			IEnumerable<IEdmOperationParameter> enumerable = (operation.IsBound ? Enumerable.Skip<IEdmOperationParameter>(operation.Parameters, 1) : operation.Parameters);
			return "(" + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(enumerable, (IEdmOperationParameter p) => p.Name))) + ")";
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00034A40 File Offset: 0x00032C40
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

		// Token: 0x06001267 RID: 4711 RVA: 0x00034AB4 File Offset: 0x00032CB4
		private static string ParameterTypesToString(this IEdmOperationImport operationImport)
		{
			return "(" + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmOperationParameter, string>(operationImport.Operation.Parameters, (IEdmOperationParameter p) => p.Type.FullName()))) + ")";
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00034B0E File Offset: 0x00032D0E
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
				operationImport = null;
			}
			IEnumerator<IEdmOperationImport> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00034B2C File Offset: 0x00032D2C
		private static bool ParametersSatisfyFunction(IEdmOperation operation, IList<string> parameterNameList, bool caseInsensitive)
		{
			IEnumerable<IEdmOperationParameter> enumerable = operation.Parameters;
			if (operation.IsBound)
			{
				enumerable = Enumerable.Skip<IEdmOperationParameter>(enumerable, 1);
			}
			List<IEdmOperationParameter> functionParameters = Enumerable.ToList<IEdmOperationParameter>(enumerable);
			return !Enumerable.Any<IEdmOperationParameter>(Enumerable.Where<IEdmOperationParameter>(functionParameters, (IEdmOperationParameter p) => !(p is IEdmOptionalParameter)), (IEdmOperationParameter p) => Enumerable.All<string>(parameterNameList, (string k) => !string.Equals(k, p.Name, caseInsensitive ? 5 : 4))) && !Enumerable.Any<string>(parameterNameList, (string k) => Enumerable.All<IEdmOperationParameter>(functionParameters, (IEdmOperationParameter p) => !string.Equals(k, p.Name, caseInsensitive ? 5 : 4)));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00034BCC File Offset: 0x00032DCC
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

		// Token: 0x0600126B RID: 4715 RVA: 0x00034BF4 File Offset: 0x00032DF4
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

		// Token: 0x04000960 RID: 2400
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);

		// Token: 0x04000961 RID: 2401
		private static readonly EdmPrimitiveTypeReference BooleanTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false);

		// Token: 0x04000962 RID: 2402
		private static readonly EdmPrimitiveTypeReference ByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false);

		// Token: 0x04000963 RID: 2403
		private static readonly EdmPrimitiveTypeReference DecimalTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false);

		// Token: 0x04000964 RID: 2404
		private static readonly EdmPrimitiveTypeReference DoubleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false);

		// Token: 0x04000965 RID: 2405
		private static readonly EdmPrimitiveTypeReference Int16TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false);

		// Token: 0x04000966 RID: 2406
		private static readonly EdmPrimitiveTypeReference Int32TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false);

		// Token: 0x04000967 RID: 2407
		private static readonly EdmPrimitiveTypeReference Int64TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false);

		// Token: 0x04000968 RID: 2408
		private static readonly EdmPrimitiveTypeReference SByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false);

		// Token: 0x04000969 RID: 2409
		private static readonly EdmPrimitiveTypeReference StringTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true);

		// Token: 0x0400096A RID: 2410
		private static readonly EdmPrimitiveTypeReference SingleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false);

		// Token: 0x0400096B RID: 2411
		private const string CollectionTypeQualifier = "Collection";

		// Token: 0x0400096C RID: 2412
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x02000302 RID: 770
		private sealed class EdmTypeEqualityComparer : IEqualityComparer<IEdmType>
		{
			// Token: 0x060019D0 RID: 6608 RVA: 0x0004A3DF File Offset: 0x000485DF
			public bool Equals(IEdmType x, IEdmType y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x060019D1 RID: 6609 RVA: 0x0004A3E8 File Offset: 0x000485E8
			public int GetHashCode(IEdmType obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
