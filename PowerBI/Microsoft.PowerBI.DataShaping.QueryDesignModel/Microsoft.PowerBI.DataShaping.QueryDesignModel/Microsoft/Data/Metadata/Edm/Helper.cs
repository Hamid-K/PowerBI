using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Text;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B5 RID: 181
	internal static class Helper
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x0001E147 File Offset: 0x0001C347
		internal static bool IsEntityTypeBase(EdmType edmType)
		{
			return Helper.IsEntityType(edmType) || Helper.IsRelationshipType(edmType);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0001E159 File Offset: 0x0001C359
		internal static bool IsStructuralType(EdmType type)
		{
			return Helper.IsComplexType(type) || Helper.IsEntityType(type) || Helper.IsRelationshipType(type) || Helper.IsRowType(type);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001E17B File Offset: 0x0001C37B
		internal static bool IsComplexType(EdmType type)
		{
			return BuiltInTypeKind.ComplexType == type.BuiltInTypeKind;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001E186 File Offset: 0x0001C386
		internal static bool IsEntityType(EdmType type)
		{
			return BuiltInTypeKind.EntityType == type.BuiltInTypeKind;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001E192 File Offset: 0x0001C392
		internal static bool IsRelationshipType(EdmType type)
		{
			return BuiltInTypeKind.AssociationType == type.BuiltInTypeKind;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001E19D File Offset: 0x0001C39D
		internal static bool IsRefType(GlobalItem item)
		{
			return BuiltInTypeKind.RefType == item.BuiltInTypeKind;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E1A9 File Offset: 0x0001C3A9
		internal static bool IsRowType(GlobalItem item)
		{
			return BuiltInTypeKind.RowType == item.BuiltInTypeKind;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E1B5 File Offset: 0x0001C3B5
		internal static bool IsCollectionType(GlobalItem item)
		{
			return BuiltInTypeKind.CollectionType == item.BuiltInTypeKind;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
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

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E200 File Offset: 0x0001C400
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
				stringBuilder.Append(edmSchemaError.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001E274 File Offset: 0x0001C474
		internal static void DisposeXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			foreach (XmlReader xmlReader in xmlReaders)
			{
				((IDisposable)xmlReader).Dispose();
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		internal static bool IsNavigationProperty(EdmMember member)
		{
			return BuiltInTypeKind.NavigationProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		internal static bool IsEdmFunction(GlobalItem item)
		{
			return BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		internal static bool IsRelationshipEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E2DF File Offset: 0x0001C4DF
		internal static bool IsEntityContainer(GlobalItem item)
		{
			return BuiltInTypeKind.EntityContainer == item.BuiltInTypeKind;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001E2EB File Offset: 0x0001C4EB
		internal static bool IsRelationshipSet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.AssociationSet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001E2F6 File Offset: 0x0001C4F6
		internal static bool IsEntitySet(EntitySetBase entitySetBase)
		{
			return BuiltInTypeKind.EntitySet == entitySetBase.BuiltInTypeKind;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001E302 File Offset: 0x0001C502
		internal static bool IsAssociationEndMember(EdmMember member)
		{
			return member.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001E310 File Offset: 0x0001C510
		internal static string GetFileNameFromUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (uri.Scheme == "file")
			{
				return uri.LocalPath;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsolutePath;
			}
			throw new ArgumentException(Strings.UnacceptableUri(uri), "uri");
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001E36C File Offset: 0x0001C56C
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

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		internal static bool IsUnboundedFacetValue(Facet facet)
		{
			return facet.Value == EdmConstants.UnboundedValue;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001E3E7 File Offset: 0x0001C5E7
		internal static bool IsPrimitiveType(EdmType type)
		{
			return BuiltInTypeKind.PrimitiveType == type.BuiltInTypeKind;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
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

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001E420 File Offset: 0x0001C620
		internal static bool IsAssignableFrom(EdmType firstType, EdmType secondType)
		{
			return secondType != null && (firstType.Equals(secondType) || Helper.IsSubtypeOf(secondType, firstType));
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001E43C File Offset: 0x0001C63C
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

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001E494 File Offset: 0x0001C694
		internal static bool IsEdmProperty(EdmMember member)
		{
			return BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
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

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
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

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001E50B File Offset: 0x0001C70B
		internal static TypeUsage GetModelTypeUsage(TypeUsage typeUsage)
		{
			return typeUsage.GetModelTypeUsage();
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001E513 File Offset: 0x0001C713
		internal static TypeUsage GetModelTypeUsage(EdmMember member)
		{
			return Helper.GetModelTypeUsage(member.TypeUsage);
		}

		// Token: 0x040008BD RID: 2237
		internal static readonly ReadOnlyCollection<KeyValuePair<string, object>> EmptyKeyValueStringObjectList = new ReadOnlyCollection<KeyValuePair<string, object>>(new KeyValuePair<string, object>[0]);

		// Token: 0x040008BE RID: 2238
		internal static readonly ReadOnlyCollection<string> EmptyStringList = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x040008BF RID: 2239
		internal static readonly ReadOnlyCollection<FacetDescription> EmptyFacetDescriptionEnumerable = new ReadOnlyCollection<FacetDescription>(new FacetDescription[0]);

		// Token: 0x040008C0 RID: 2240
		internal static readonly ReadOnlyCollection<EdmFunction> EmptyEdmFunctionReadOnlyCollection = new ReadOnlyCollection<EdmFunction>(new EdmFunction[0]);

		// Token: 0x040008C1 RID: 2241
		internal static readonly ReadOnlyCollection<PrimitiveType> EmptyPrimitiveTypeReadOnlyCollection = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[0]);

		// Token: 0x040008C2 RID: 2242
		internal static readonly KeyValuePair<string, object>[] EmptyKeyValueStringObjectArray = new KeyValuePair<string, object>[0];

		// Token: 0x040008C3 RID: 2243
		internal const char PeriodSymbol = '.';

		// Token: 0x040008C4 RID: 2244
		internal const char CommaSymbol = ',';
	}
}
