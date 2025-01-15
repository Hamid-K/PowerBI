using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000145 RID: 325
	internal static class ODataPathExtensions
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x00029D0F File Offset: 0x00027F0F
		public static IEdmTypeReference EdmType(this ODataPath path)
		{
			return path.LastSegment.EdmType.ToTypeReference();
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00029D21 File Offset: 0x00027F21
		public static IEdmNavigationSource NavigationSource(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<IEdmNavigationSource>(new DetermineNavigationSourceTranslator());
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00029D33 File Offset: 0x00027F33
		public static bool IsCollection(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<bool>(new IsCollectionTranslator());
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00029D48 File Offset: 0x00027F48
		public static ODataPath AppendNavigationPropertySegment(this ODataPath path, IEdmNavigationProperty navigationProperty, IEdmNavigationSource navigationSource)
		{
			ODataPath odataPath = new ODataPath(path);
			NavigationPropertySegment navigationPropertySegment = new NavigationPropertySegment(navigationProperty, navigationSource);
			odataPath.Add(navigationPropertySegment);
			return odataPath;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00029D6C File Offset: 0x00027F6C
		public static ODataPath AppendPropertySegment(this ODataPath path, IEdmStructuralProperty property)
		{
			ODataPath odataPath = new ODataPath(path);
			PropertySegment propertySegment = new PropertySegment(property);
			odataPath.Add(propertySegment);
			return odataPath;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00029D90 File Offset: 0x00027F90
		public static ODataPath AppendKeySegment(this ODataPath path, IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
		{
			SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			KeySegment keySegment = new KeySegment(keys, edmType, navigationSource);
			ODataPath firstPart = splitEndingSegmentOfTypeHandler.FirstPart;
			firstPart.Add(keySegment);
			foreach (ODataPathSegment odataPathSegment in splitEndingSegmentOfTypeHandler.LastPart)
			{
				firstPart.Add(odataPathSegment);
			}
			return firstPart;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00029E04 File Offset: 0x00028004
		public static ODataPath TrimEndingKeySegment(this ODataPath path)
		{
			SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new SplitEndingSegmentOfTypeHandler<TypeSegment>();
			SplitEndingSegmentOfTypeHandler<KeySegment> splitEndingSegmentOfTypeHandler2 = new SplitEndingSegmentOfTypeHandler<KeySegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			splitEndingSegmentOfTypeHandler.FirstPart.WalkWith(splitEndingSegmentOfTypeHandler2);
			ODataPath firstPart = splitEndingSegmentOfTypeHandler2.FirstPart;
			foreach (ODataPathSegment odataPathSegment in splitEndingSegmentOfTypeHandler.LastPart)
			{
				firstPart.Add(odataPathSegment);
			}
			return firstPart;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00029E7C File Offset: 0x0002807C
		public static ODataPath TrimEndingTypeSegment(this ODataPath path)
		{
			SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			return splitEndingSegmentOfTypeHandler.FirstPart;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00029E9C File Offset: 0x0002809C
		public static bool IsIndividualProperty(this ODataPath path)
		{
			ODataPathSegment lastSegment = path.TrimEndingTypeSegment().LastSegment;
			return lastSegment is PropertySegment || lastSegment is DynamicPathSegment;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00029EC8 File Offset: 0x000280C8
		public static bool IsUndeclared(this ODataPath path)
		{
			ODataPathSegment lastSegment = path.TrimEndingTypeSegment().LastSegment;
			return lastSegment is DynamicPathSegment;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00029EEC File Offset: 0x000280EC
		public static string ToContextUrlPathString(this ODataPath path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			PathSegmentToContextUrlPathTranslator defaultInstance = PathSegmentToContextUrlPathTranslator.DefaultInstance;
			ODataPathSegment odataPathSegment = null;
			foreach (ODataPathSegment odataPathSegment2 in path)
			{
				OperationSegment operationSegment = odataPathSegment2 as OperationSegment;
				if (operationSegment != null)
				{
					bool flag = false;
					if (odataPathSegment != null)
					{
						foreach (IEdmOperation edmOperation in operationSegment.Operations)
						{
							if (edmOperation.IsBound && Enumerable.First<IEdmOperationParameter>(edmOperation.Parameters).Type.Definition == odataPathSegment.EdmType)
							{
								if (edmOperation.EntitySetPath != null)
								{
									foreach (string text in Enumerable.Skip<string>(edmOperation.EntitySetPath.PathSegments, 1))
									{
										stringBuilder.Append('/');
										stringBuilder.Append(text);
									}
									flag = true;
									break;
								}
								break;
							}
						}
					}
					if (!flag)
					{
						if (operationSegment.EntitySet == null)
						{
							return null;
						}
						stringBuilder = new StringBuilder();
						stringBuilder.Append(operationSegment.EntitySet.Name);
					}
				}
				else
				{
					stringBuilder.Append(odataPathSegment2.TranslateWith<string>(defaultInstance));
				}
				odataPathSegment = odataPathSegment2;
			}
			return stringBuilder.ToString().TrimStart(new char[] { '/' });
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002A0A8 File Offset: 0x000282A8
		public static string ToResourcePathString(this ODataPath path, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			return string.Concat(Enumerable.ToArray<string>(path.WalkWith<string>(new PathSegmentToResourcePathTranslator(urlKeyDelimiter)))).TrimStart(new char[] { '/' });
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002A0D0 File Offset: 0x000282D0
		public static ODataSelectPath ToSelectPath(this ODataExpandPath path)
		{
			return new ODataSelectPath(path);
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002A0D8 File Offset: 0x000282D8
		public static ODataExpandPath ToExpandPath(this ODataSelectPath path)
		{
			return new ODataExpandPath(path);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002A0E0 File Offset: 0x000282E0
		internal static IEdmNavigationSource TargetNavigationSource(this ODataPath path)
		{
			if (path == null)
			{
				return null;
			}
			return new ODataPathInfo(path).TargetNavigationSource;
		}
	}
}
