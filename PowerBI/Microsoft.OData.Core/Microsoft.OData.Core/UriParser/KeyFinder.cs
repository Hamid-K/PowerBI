using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000156 RID: 342
	internal static class KeyFinder
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00031DC4 File Offset: 0x0002FFC4
		public static SegmentArgumentParser FindAndUseKeysFromRelatedSegment(SegmentArgumentParser rawKeyValuesFromUri, IEnumerable<IEdmStructuralProperty> targetEntityKeyProperties, IEdmNavigationProperty currentNavigationProperty, KeySegment keySegmentOfParentEntity)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(currentNavigationProperty, "currentNavigationProperty");
			ExceptionUtils.CheckArgumentNotNull<SegmentArgumentParser>(rawKeyValuesFromUri, "rawKeyValuesFromUri");
			ReadOnlyCollection<IEdmStructuralProperty> readOnlyCollection = ((targetEntityKeyProperties != null) ? new ReadOnlyCollection<IEdmStructuralProperty>(targetEntityKeyProperties.ToList<IEdmStructuralProperty>()) : new ReadOnlyCollection<IEdmStructuralProperty>(new List<IEdmStructuralProperty>()));
			bool flag = !rawKeyValuesFromUri.AreValuesNamed;
			if (flag && rawKeyValuesFromUri.ValueCount > 1)
			{
				return rawKeyValuesFromUri;
			}
			if (keySegmentOfParentEntity == null)
			{
				return rawKeyValuesFromUri;
			}
			List<EdmReferentialConstraintPropertyPair> list = KeyFinder.ExtractMatchingPropertyPairsFromNavProp(currentNavigationProperty, readOnlyCollection).ToList<EdmReferentialConstraintPropertyPair>();
			using (List<EdmReferentialConstraintPropertyPair>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmReferentialConstraintPropertyPair keyFromReferentialIntegrityConstraint2 = enumerator.Current;
					KeyValuePair<string, object> keyValuePair = keySegmentOfParentEntity.Keys.SingleOrDefault((KeyValuePair<string, object> x) => x.Key == keyFromReferentialIntegrityConstraint2.DependentProperty.Name);
					if (keyValuePair.Key != null && readOnlyCollection.Any((IEdmStructuralProperty x) => x.Name == keyFromReferentialIntegrityConstraint2.PrincipalProperty.Name))
					{
						rawKeyValuesFromUri.AddNamedValue(keyFromReferentialIntegrityConstraint2.PrincipalProperty.Name, KeyFinder.ConvertKeyValueToUriLiteral(keyValuePair.Value, rawKeyValuesFromUri.KeyAsSegment));
					}
				}
			}
			list.Clear();
			IEdmNavigationProperty partner = currentNavigationProperty.Partner;
			if (partner != null)
			{
				list.AddRange(KeyFinder.ExtractMatchingPropertyPairsFromReversedNavProp(partner, readOnlyCollection));
			}
			using (List<EdmReferentialConstraintPropertyPair>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					EdmReferentialConstraintPropertyPair keyFromReferentialIntegrityConstraint = enumerator2.Current;
					KeyValuePair<string, object> keyValuePair2 = keySegmentOfParentEntity.Keys.SingleOrDefault((KeyValuePair<string, object> x) => x.Key == keyFromReferentialIntegrityConstraint.PrincipalProperty.Name);
					if (keyValuePair2.Key != null && readOnlyCollection.Any((IEdmStructuralProperty x) => x.Name == keyFromReferentialIntegrityConstraint.DependentProperty.Name))
					{
						rawKeyValuesFromUri.AddNamedValue(keyFromReferentialIntegrityConstraint.DependentProperty.Name, KeyFinder.ConvertKeyValueToUriLiteral(keyValuePair2.Value, rawKeyValuesFromUri.KeyAsSegment));
					}
				}
			}
			if (flag)
			{
				if (rawKeyValuesFromUri.NamedValues == null)
				{
					return rawKeyValuesFromUri;
				}
				List<IEdmStructuralProperty> list2 = readOnlyCollection.Where((IEdmStructuralProperty x) => !rawKeyValuesFromUri.NamedValues.ContainsKey(x.Name)).ToList<IEdmStructuralProperty>();
				if (list2.Count != 1)
				{
					return rawKeyValuesFromUri;
				}
				rawKeyValuesFromUri.AddNamedValue(list2[0].Name, rawKeyValuesFromUri.PositionalValues[0]);
				rawKeyValuesFromUri.PositionalValues.Clear();
			}
			return rawKeyValuesFromUri;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00032068 File Offset: 0x00030268
		private static IEnumerable<EdmReferentialConstraintPropertyPair> ExtractMatchingPropertyPairsFromNavProp(IEdmNavigationProperty currentNavigationProperty, IEnumerable<IEdmStructuralProperty> targetKeyPropertyList)
		{
			if (currentNavigationProperty != null && currentNavigationProperty.ReferentialConstraint != null)
			{
				return currentNavigationProperty.ReferentialConstraint.PropertyPairs.Where((EdmReferentialConstraintPropertyPair x) => targetKeyPropertyList.Any((IEdmStructuralProperty y) => y == x.PrincipalProperty));
			}
			return new List<EdmReferentialConstraintPropertyPair>();
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000320B0 File Offset: 0x000302B0
		private static IEnumerable<EdmReferentialConstraintPropertyPair> ExtractMatchingPropertyPairsFromReversedNavProp(IEdmNavigationProperty currentNavigationProperty, IEnumerable<IEdmStructuralProperty> targetKeyPropertyList)
		{
			if (currentNavigationProperty != null && currentNavigationProperty.ReferentialConstraint != null)
			{
				return currentNavigationProperty.ReferentialConstraint.PropertyPairs.Where((EdmReferentialConstraintPropertyPair x) => targetKeyPropertyList.Any((IEdmStructuralProperty y) => y == x.DependentProperty));
			}
			return new List<EdmReferentialConstraintPropertyPair>();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000320F8 File Offset: 0x000302F8
		private static string ConvertKeyValueToUriLiteral(object value, bool keyAsSegment)
		{
			string text = value as string;
			if (keyAsSegment && text != null)
			{
				return text;
			}
			return ODataUriConversionUtils.ConvertToUriPrimitiveLiteral(value, ODataVersion.V4);
		}
	}
}
