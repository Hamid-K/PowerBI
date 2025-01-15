using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C9 RID: 1225
	internal static class Helper
	{
		// Token: 0x06003C82 RID: 15490 RVA: 0x000C8A44 File Offset: 0x000C6C44
		internal static string GetAttributeValue(XPathNavigator nav, string attributeName)
		{
			nav = nav.Clone();
			string text = null;
			if (nav.MoveToAttribute(attributeName, string.Empty))
			{
				text = nav.Value;
			}
			return text;
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000C8A74 File Offset: 0x000C6C74
		internal static object GetTypedAttributeValue(XPathNavigator nav, string attributeName, Type clrType)
		{
			nav = nav.Clone();
			object obj = null;
			if (nav.MoveToAttribute(attributeName, string.Empty))
			{
				obj = nav.ValueAs(clrType);
			}
			return obj;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x000C8AA4 File Offset: 0x000C6CA4
		internal static FacetDescription GetFacet(IEnumerable<FacetDescription> facetCollection, string facetName)
		{
			foreach (FacetDescription facetDescription in facetCollection)
			{
				if (facetDescription.FacetName == facetName)
				{
					return facetDescription;
				}
			}
			return null;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000C8AFC File Offset: 0x000C6CFC
		internal static bool IsAssignableFrom(EdmType firstType, EdmType secondType)
		{
			return secondType != null && (firstType.Equals(secondType) || Helper.IsSubtypeOf(secondType, firstType));
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x000C8B18 File Offset: 0x000C6D18
		internal static bool IsSubtypeOf(EdmType firstType, EdmType secondType)
		{
			if (secondType == null)
			{
				return false;
			}
			for (EdmType edmType = firstType.BaseType; edmType != null; edmType = edmType.BaseType)
			{
				if (edmType == secondType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000C8B44 File Offset: 0x000C6D44
		internal static IList GetAllStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return ((AssociationType)edmType).AssociationEndMembers;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return ((ComplexType)edmType).Properties;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return ((EntityType)edmType).Properties;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return ((RowType)edmType).Properties;
				}
			}
			return Helper.EmptyArrayEdmProperty;
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000C8BA8 File Offset: 0x000C6DA8
		internal static AssociationEndMember GetEndThatShouldBeMappedToKey(AssociationType associationType)
		{
			if (associationType.AssociationEndMembers.Any((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.One)))
			{
				return associationType.AssociationEndMembers.SingleOrDefault((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.Many) || it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.ZeroOrOne));
			}
			if (associationType.AssociationEndMembers.Any((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.ZeroOrOne)))
			{
				return associationType.AssociationEndMembers.SingleOrDefault((AssociationEndMember it) => it.RelationshipMultiplicity.Equals(RelationshipMultiplicity.Many));
			}
			return null;
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x000C8C64 File Offset: 0x000C6E64
		internal static string GetCommaDelimitedString(IEnumerable<string> stringList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in stringList)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
		internal static IEnumerable<T> Concat<T>(params IEnumerable<T>[] sources)
		{
			foreach (IEnumerable<T> enumerable in sources)
			{
				if (enumerable != null)
				{
					foreach (T t in enumerable)
					{
						yield return t;
					}
					IEnumerator<T> enumerator = null;
				}
			}
			IEnumerable<T>[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000C8CE0 File Offset: 0x000C6EE0
		internal static void DisposeXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			foreach (XmlReader xmlReader in xmlReaders)
			{
				((IDisposable)xmlReader).Dispose();
			}
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000C8D28 File Offset: 0x000C6F28
		internal static bool IsStructuralType(EdmType type)
		{
			return Helper.IsComplexType(type) || Helper.IsEntityType(type) || Helper.IsRelationshipType(type) || Helper.IsRowType(type);
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000C8D4A File Offset: 0x000C6F4A
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x000C8D55 File Offset: 0x000C6F55
		internal static bool IsEntityType(EdmType type)
		{
			return BuiltInTypeKind.EntityType == type.BuiltInTypeKind;
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000C8D61 File Offset: 0x000C6F61
		internal static bool IsComplexType(EdmType type)
		{
			return BuiltInTypeKind.ComplexType == type.BuiltInTypeKind;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000C8D6C File Offset: 0x000C6F6C
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000C8D78 File Offset: 0x000C6F78
		internal static bool IsRefType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000C8D84 File Offset: 0x000C6F84
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000C8D90 File Offset: 0x000C6F90
		internal static bool IsAssociationType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000C8D9B File Offset: 0x000C6F9B
		internal static bool IsRelationshipType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000C8DA6 File Offset: 0x000C6FA6
		internal static bool IsEdmProperty(EdmMember member)
		{
			return BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000C8DB2 File Offset: 0x000C6FB2
		internal static bool IsRelationshipEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000C8DBD File Offset: 0x000C6FBD
		internal static bool IsAssociationEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000C8DC8 File Offset: 0x000C6FC8
		internal static bool IsNavigationProperty(EdmMember member)
		{
			return BuiltInTypeKind.NavigationProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000C8DD4 File Offset: 0x000C6FD4
		internal static bool IsEntityTypeBase(EdmType edmType)
		{
			return Helper.IsEntityType(edmType) || Helper.IsRelationshipType(edmType);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000C8DE6 File Offset: 0x000C6FE6
		internal static bool IsTransientType(EdmType edmType)
		{
			return Helper.IsCollectionType(edmType) || Helper.IsRefType(edmType) || Helper.IsRowType(edmType);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000C8E00 File Offset: 0x000C7000
		internal static bool IsAssociationSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000C8E0B File Offset: 0x000C700B
		internal static bool IsEntitySet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.EntitySet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000C8E17 File Offset: 0x000C7017
		internal static bool IsRelationshipSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000C8E22 File Offset: 0x000C7022
		internal static bool IsEntityContainer(GlobalItem item)
		{
			return BuiltInTypeKind.EntityContainer == item.BuiltInTypeKind;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000C8E2E File Offset: 0x000C702E
		internal static bool IsEdmFunction(GlobalItem item)
		{
			return BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000C8E3A File Offset: 0x000C703A
		internal static string GetFileNameFromUri(Uri uri)
		{
			Check.NotNull<Uri>(uri, "uri");
			if (uri.IsFile)
			{
				return uri.LocalPath;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsolutePath;
			}
			throw new ArgumentException(Strings.UnacceptableUri(uri), "uri");
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000C8E76 File Offset: 0x000C7076
		internal static bool IsEnumType(EdmType edmType)
		{
			return BuiltInTypeKind.EnumType == edmType.BuiltInTypeKind;
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000C8E82 File Offset: 0x000C7082
		internal static bool IsUnboundedFacetValue(Facet facet)
		{
			return facet.Value == EdmConstants.UnboundedValue;
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000C8E91 File Offset: 0x000C7091
		internal static bool IsVariableFacetValue(Facet facet)
		{
			return facet.Value == EdmConstants.VariableValue;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000C8EA0 File Offset: 0x000C70A0
		internal static bool IsScalarType(EdmType edmType)
		{
			return Helper.IsEnumType(edmType) || Helper.IsPrimitiveType(edmType);
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000C8EB2 File Offset: 0x000C70B2
		internal static bool IsHierarchyIdType(PrimitiveType type)
		{
			return type.PrimitiveTypeKind == PrimitiveTypeKind.HierarchyId;
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000C8EBE File Offset: 0x000C70BE
		internal static bool IsSpatialType(PrimitiveType type)
		{
			return Helper.IsGeographicType(type) || Helper.IsGeometricType(type);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000C8ED0 File Offset: 0x000C70D0
		internal static bool IsSpatialType(EdmType type, out bool isGeographic)
		{
			PrimitiveType primitiveType = type as PrimitiveType;
			if (primitiveType == null)
			{
				isGeographic = false;
				return false;
			}
			isGeographic = Helper.IsGeographicType(primitiveType);
			return isGeographic || Helper.IsGeometricType(primitiveType);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000C8F00 File Offset: 0x000C7100
		internal static bool IsGeographicType(PrimitiveType type)
		{
			return Helper.IsGeographicTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000C8F0D File Offset: 0x000C710D
		internal static bool AreSameSpatialUnionType(PrimitiveType firstType, PrimitiveType secondType)
		{
			return (Helper.IsGeographicTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeographicTypeKind(secondType.PrimitiveTypeKind)) || (Helper.IsGeometricTypeKind(firstType.PrimitiveTypeKind) && Helper.IsGeometricTypeKind(secondType.PrimitiveTypeKind));
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000C8F48 File Offset: 0x000C7148
		internal static bool IsGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geography || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000C8F57 File Offset: 0x000C7157
		internal static bool IsGeometricType(PrimitiveType type)
		{
			return Helper.IsGeometricTypeKind(type.PrimitiveTypeKind);
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000C8F64 File Offset: 0x000C7164
		internal static bool IsGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind == PrimitiveTypeKind.Geometry || Helper.IsStrongGeometricTypeKind(kind);
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000C8F73 File Offset: 0x000C7173
		internal static bool IsStrongSpatialTypeKind(PrimitiveTypeKind kind)
		{
			return Helper.IsStrongGeometricTypeKind(kind) || Helper.IsStrongGeographicTypeKind(kind);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000C8F85 File Offset: 0x000C7185
		private static bool IsStrongGeometricTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeometryPoint && kind <= PrimitiveTypeKind.GeometryCollection;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000C8F96 File Offset: 0x000C7196
		private static bool IsStrongGeographicTypeKind(PrimitiveTypeKind kind)
		{
			return kind >= PrimitiveTypeKind.GeographyPoint && kind <= PrimitiveTypeKind.GeographyCollection;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000C8FA7 File Offset: 0x000C71A7
		internal static bool IsHierarchyIdType(TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && ((PrimitiveType)type.EdmType).PrimitiveTypeKind == PrimitiveTypeKind.HierarchyId;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000C8FCE File Offset: 0x000C71CE
		internal static bool IsSpatialType(TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && Helper.IsSpatialType((PrimitiveType)type.EdmType);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000C8FF4 File Offset: 0x000C71F4
		internal static bool IsSpatialType(TypeUsage type, out PrimitiveTypeKind spatialType)
		{
			if (type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				PrimitiveType primitiveType = (PrimitiveType)type.EdmType;
				if (Helper.IsGeographicTypeKind(primitiveType.PrimitiveTypeKind) || Helper.IsGeometricTypeKind(primitiveType.PrimitiveTypeKind))
				{
					spatialType = primitiveType.PrimitiveTypeKind;
					return true;
				}
			}
			spatialType = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000C9044 File Offset: 0x000C7244
		internal static string ToString(ParameterDirection value)
		{
			switch (value)
			{
			case ParameterDirection.Input:
				return "Input";
			case ParameterDirection.Output:
				return "Output";
			case ParameterDirection.InputOutput:
				return "InputOutput";
			case ParameterDirection.ReturnValue:
				return "ReturnValue";
			}
			return value.ToString();
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000C9098 File Offset: 0x000C7298
		internal static string ToString(ParameterMode value)
		{
			switch (value)
			{
			case ParameterMode.In:
				return "In";
			case ParameterMode.Out:
				return "Out";
			case ParameterMode.InOut:
				return "InOut";
			case ParameterMode.ReturnValue:
				return "ReturnValue";
			default:
				return value.ToString();
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000C90D7 File Offset: 0x000C72D7
		internal static bool IsSupportedEnumUnderlyingType(PrimitiveTypeKind typeKind)
		{
			return typeKind == PrimitiveTypeKind.Byte || typeKind == PrimitiveTypeKind.SByte || typeKind == PrimitiveTypeKind.Int16 || typeKind == PrimitiveTypeKind.Int32 || typeKind == PrimitiveTypeKind.Int64;
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000C90F2 File Offset: 0x000C72F2
		internal static bool IsEnumMemberValueInRange(PrimitiveTypeKind underlyingTypeKind, long value)
		{
			return value >= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][0] && value <= Helper._enumUnderlyingTypeRanges[underlyingTypeKind][1];
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000C9119 File Offset: 0x000C7319
		internal static PrimitiveType AsPrimitive(EdmType type)
		{
			if (!Helper.IsEnumType(type))
			{
				return (PrimitiveType)type;
			}
			return Helper.GetUnderlyingEdmTypeForEnumType(type);
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000C9130 File Offset: 0x000C7330
		internal static PrimitiveType GetUnderlyingEdmTypeForEnumType(EdmType type)
		{
			return ((EnumType)type).UnderlyingType;
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000C9140 File Offset: 0x000C7340
		internal static PrimitiveType GetSpatialNormalizedPrimitiveType(EdmType type)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			if (Helper.IsGeographicType(primitiveType) && primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Geography)
			{
				return PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Geography);
			}
			if (Helper.IsGeometricType(primitiveType) && primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Geometry)
			{
				return PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Geometry);
			}
			return primitiveType;
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000C918C File Offset: 0x000C738C
		internal static string CombineErrorMessage(IEnumerable<EdmSchemaError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
			int num = 0;
			foreach (EdmSchemaError edmSchemaError in errors)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(edmSchemaError);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000C91FC File Offset: 0x000C73FC
		internal static string CombineErrorMessage(IEnumerable<EdmItemError> errors)
		{
			StringBuilder stringBuilder = new StringBuilder(Environment.NewLine);
			int num = 0;
			foreach (EdmItemError edmItemError in errors)
			{
				if (num++ != 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(edmItemError.Message);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x000C9270 File Offset: 0x000C7470
		internal static IEnumerable<KeyValuePair<T, S>> PairEnumerations<T, S>(IBaseList<T> left, IEnumerable<S> right)
		{
			IEnumerator leftEnumerator = left.GetEnumerator();
			IEnumerator<S> rightEnumerator = right.GetEnumerator();
			while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
			{
				yield return new KeyValuePair<T, S>((T)((object)leftEnumerator.Current), rightEnumerator.Current);
			}
			yield break;
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x000C9287 File Offset: 0x000C7487
		internal static TypeUsage GetModelTypeUsage(TypeUsage typeUsage)
		{
			return typeUsage.ModelTypeUsage;
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000C928F File Offset: 0x000C748F
		internal static TypeUsage GetModelTypeUsage(EdmMember member)
		{
			return Helper.GetModelTypeUsage(member.TypeUsage);
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x000C929C File Offset: 0x000C749C
		internal static TypeUsage ValidateAndConvertTypeUsage(EdmProperty edmProperty, EdmProperty columnProperty)
		{
			return Helper.ValidateAndConvertTypeUsage(edmProperty.TypeUsage, columnProperty.TypeUsage);
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000C92B0 File Offset: 0x000C74B0
		internal static TypeUsage ValidateAndConvertTypeUsage(TypeUsage cspaceType, TypeUsage sspaceType)
		{
			TypeUsage typeUsage = sspaceType;
			if (sspaceType.EdmType.DataSpace == DataSpace.SSpace)
			{
				typeUsage = sspaceType.ModelTypeUsage;
			}
			if (Helper.ValidateScalarTypesAreCompatible(cspaceType, typeUsage))
			{
				return typeUsage;
			}
			return null;
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x000C92E0 File Offset: 0x000C74E0
		private static bool ValidateScalarTypesAreCompatible(TypeUsage cspaceType, TypeUsage storeType)
		{
			if (Helper.IsEnumType(cspaceType.EdmType))
			{
				return TypeSemantics.IsSubTypeOf(TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(cspaceType.EdmType)), storeType);
			}
			return TypeSemantics.IsSubTypeOf(cspaceType, storeType);
		}

		// Token: 0x040014D2 RID: 5330
		internal static readonly EdmMember[] EmptyArrayEdmProperty = new EdmMember[0];

		// Token: 0x040014D3 RID: 5331
		private static readonly Dictionary<PrimitiveTypeKind, long[]> _enumUnderlyingTypeRanges = new Dictionary<PrimitiveTypeKind, long[]>
		{
			{
				PrimitiveTypeKind.Byte,
				new long[] { 0L, 255L }
			},
			{
				PrimitiveTypeKind.SByte,
				new long[] { -128L, 127L }
			},
			{
				PrimitiveTypeKind.Int16,
				new long[] { -32768L, 32767L }
			},
			{
				PrimitiveTypeKind.Int32,
				new long[] { -2147483648L, 2147483647L }
			},
			{
				PrimitiveTypeKind.Int64,
				new long[] { long.MinValue, long.MaxValue }
			}
		};

		// Token: 0x040014D4 RID: 5332
		internal static readonly ReadOnlyCollection<KeyValuePair<string, object>> EmptyKeyValueStringObjectList = new ReadOnlyCollection<KeyValuePair<string, object>>(new KeyValuePair<string, object>[0]);

		// Token: 0x040014D5 RID: 5333
		internal static readonly ReadOnlyCollection<string> EmptyStringList = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x040014D6 RID: 5334
		internal static readonly ReadOnlyCollection<FacetDescription> EmptyFacetDescriptionEnumerable = new ReadOnlyCollection<FacetDescription>(new FacetDescription[0]);

		// Token: 0x040014D7 RID: 5335
		internal static readonly ReadOnlyCollection<EdmFunction> EmptyEdmFunctionReadOnlyCollection = new ReadOnlyCollection<EdmFunction>(new EdmFunction[0]);

		// Token: 0x040014D8 RID: 5336
		internal static readonly ReadOnlyCollection<PrimitiveType> EmptyPrimitiveTypeReadOnlyCollection = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[0]);

		// Token: 0x040014D9 RID: 5337
		internal static readonly KeyValuePair<string, object>[] EmptyKeyValueStringObjectArray = new KeyValuePair<string, object>[0];

		// Token: 0x040014DA RID: 5338
		internal const char PeriodSymbol = '.';

		// Token: 0x040014DB RID: 5339
		internal const char CommaSymbol = ',';
	}
}
