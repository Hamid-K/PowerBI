using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001FD RID: 509
	internal static class KeyFinder
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x000436C8 File Offset: 0x000418C8
		public static SegmentArgumentParser FindAndUseKeysFromRelatedSegment(SegmentArgumentParser rawKeyValuesFromUri, IEnumerable<IEdmStructuralProperty> targetEntityKeyProperties, IEdmNavigationProperty currentNavigationProperty, KeySegment keySegmentOfParentEntity)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(currentNavigationProperty, "currentNavigationProperty");
			ExceptionUtils.CheckArgumentNotNull<SegmentArgumentParser>(rawKeyValuesFromUri, "rawKeyValuesFromUri");
			ReadOnlyCollection<IEdmStructuralProperty> readOnlyCollection = ((targetEntityKeyProperties != null) ? new ReadOnlyCollection<IEdmStructuralProperty>(Enumerable.ToList<IEdmStructuralProperty>(targetEntityKeyProperties)) : new ReadOnlyCollection<IEdmStructuralProperty>(new List<IEdmStructuralProperty>()));
			bool flag = !rawKeyValuesFromUri.AreValuesNamed;
			if (flag && rawKeyValuesFromUri.ValueCount > 1)
			{
				return rawKeyValuesFromUri;
			}
			if (keySegmentOfParentEntity == null)
			{
				return rawKeyValuesFromUri;
			}
			List<EdmReferentialConstraintPropertyPair> list = Enumerable.ToList<EdmReferentialConstraintPropertyPair>(KeyFinder.ExtractMatchingPropertyPairsFromNavProp(currentNavigationProperty, readOnlyCollection));
			using (List<EdmReferentialConstraintPropertyPair>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmReferentialConstraintPropertyPair keyFromReferentialIntegrityConstraint2 = enumerator.Current;
					KeyValuePair<string, object> keyValuePair = Enumerable.SingleOrDefault<KeyValuePair<string, object>>(keySegmentOfParentEntity.Keys, (KeyValuePair<string, object> x) => x.Key == keyFromReferentialIntegrityConstraint2.DependentProperty.Name);
					if (keyValuePair.Key != null)
					{
						if (Enumerable.Any<IEdmStructuralProperty>(readOnlyCollection, (IEdmStructuralProperty x) => x.Name == keyFromReferentialIntegrityConstraint2.PrincipalProperty.Name))
						{
							rawKeyValuesFromUri.AddNamedValue(keyFromReferentialIntegrityConstraint2.PrincipalProperty.Name, KeyFinder.ConvertKeyValueToUriLiteral(keyValuePair.Value, rawKeyValuesFromUri.KeyAsSegment));
						}
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
					KeyValuePair<string, object> keyValuePair2 = Enumerable.SingleOrDefault<KeyValuePair<string, object>>(keySegmentOfParentEntity.Keys, (KeyValuePair<string, object> x) => x.Key == keyFromReferentialIntegrityConstraint.PrincipalProperty.Name);
					if (keyValuePair2.Key != null)
					{
						if (Enumerable.Any<IEdmStructuralProperty>(readOnlyCollection, (IEdmStructuralProperty x) => x.Name == keyFromReferentialIntegrityConstraint.DependentProperty.Name))
						{
							rawKeyValuesFromUri.AddNamedValue(keyFromReferentialIntegrityConstraint.DependentProperty.Name, KeyFinder.ConvertKeyValueToUriLiteral(keyValuePair2.Value, rawKeyValuesFromUri.KeyAsSegment));
						}
					}
				}
			}
			if (flag)
			{
				if (rawKeyValuesFromUri.NamedValues == null)
				{
					return rawKeyValuesFromUri;
				}
				List<IEdmStructuralProperty> list2 = Enumerable.ToList<IEdmStructuralProperty>(Enumerable.Where<IEdmStructuralProperty>(readOnlyCollection, (IEdmStructuralProperty x) => !rawKeyValuesFromUri.NamedValues.ContainsKey(x.Name)));
				if (list2.Count != 1)
				{
					return rawKeyValuesFromUri;
				}
				rawKeyValuesFromUri.AddNamedValue(list2[0].Name, rawKeyValuesFromUri.PositionalValues[0]);
				rawKeyValuesFromUri.PositionalValues.Clear();
			}
			return rawKeyValuesFromUri;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00043A20 File Offset: 0x00041C20
		private static IEnumerable<EdmReferentialConstraintPropertyPair> ExtractMatchingPropertyPairsFromNavProp(IEdmNavigationProperty currentNavigationProperty, IEnumerable<IEdmStructuralProperty> targetKeyPropertyList)
		{
			if (currentNavigationProperty != null && currentNavigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Where<EdmReferentialConstraintPropertyPair>(currentNavigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair x) => Enumerable.Any<IEdmStructuralProperty>(targetKeyPropertyList, (IEdmStructuralProperty y) => y == x.PrincipalProperty));
			}
			return new List<EdmReferentialConstraintPropertyPair>();
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00043AC8 File Offset: 0x00041CC8
		private static IEnumerable<EdmReferentialConstraintPropertyPair> ExtractMatchingPropertyPairsFromReversedNavProp(IEdmNavigationProperty currentNavigationProperty, IEnumerable<IEdmStructuralProperty> targetKeyPropertyList)
		{
			if (currentNavigationProperty != null && currentNavigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Where<EdmReferentialConstraintPropertyPair>(currentNavigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair x) => Enumerable.Any<IEdmStructuralProperty>(targetKeyPropertyList, (IEdmStructuralProperty y) => y == x.DependentProperty));
			}
			return new List<EdmReferentialConstraintPropertyPair>();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00043B18 File Offset: 0x00041D18
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
