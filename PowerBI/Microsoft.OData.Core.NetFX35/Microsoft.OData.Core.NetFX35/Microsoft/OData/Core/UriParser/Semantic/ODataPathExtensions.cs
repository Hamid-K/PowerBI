using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriBuilder;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024D RID: 589
	internal static class ODataPathExtensions
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x0004A36C File Offset: 0x0004856C
		public static IEdmTypeReference EdmType(this ODataPath path)
		{
			return path.LastSegment.EdmType.ToTypeReference();
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004A37E File Offset: 0x0004857E
		public static IEdmNavigationSource NavigationSource(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<IEdmNavigationSource>(new DetermineNavigationSourceTranslator());
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004A390 File Offset: 0x00048590
		public static bool IsCollection(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<bool>(new IsCollectionTranslator());
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004A3A4 File Offset: 0x000485A4
		public static ODataPath AppendNavigationPropertySegment(this ODataPath path, IEdmNavigationProperty navigationProperty, IEdmNavigationSource navigationSource)
		{
			ODataPath odataPath = new ODataPath(path);
			NavigationPropertySegment navigationPropertySegment = new NavigationPropertySegment(navigationProperty, navigationSource);
			odataPath.Add(navigationPropertySegment);
			return odataPath;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0004A3C8 File Offset: 0x000485C8
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

		// Token: 0x060014F1 RID: 5361 RVA: 0x0004A440 File Offset: 0x00048640
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

		// Token: 0x060014F2 RID: 5362 RVA: 0x0004A4BC File Offset: 0x000486BC
		public static ODataPath TrimEndingTypeSegment(this ODataPath path)
		{
			SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			return splitEndingSegmentOfTypeHandler.FirstPart;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004A4DC File Offset: 0x000486DC
		public static bool IsIndividualProperty(this ODataPath path)
		{
			ODataPathSegment lastSegment = path.TrimEndingTypeSegment().LastSegment;
			return lastSegment is PropertySegment || lastSegment is OpenPropertySegment;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004A508 File Offset: 0x00048708
		public static string ToContextUrlPathString(this ODataPath path)
		{
			return string.Concat(Enumerable.ToArray<string>(path.WalkWith<string>(PathSegmentToContextUrlPathTranslator.DefaultInstance))).TrimStart(new char[] { '/' });
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0004A53C File Offset: 0x0004873C
		public static string ToResourcePathString(this ODataPath path, ODataUrlConventions urlConventions)
		{
			return string.Concat(Enumerable.ToArray<string>(path.WalkWith<string>(new PathSegmentToResourcePathTranslator(urlConventions.UrlConvention)))).TrimStart(new char[] { '/' });
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0004A576 File Offset: 0x00048776
		public static ODataSelectPath ToSelectPath(this ODataExpandPath path)
		{
			return new ODataSelectPath(path);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0004A57E File Offset: 0x0004877E
		public static ODataExpandPath ToExpandPath(this ODataSelectPath path)
		{
			return new ODataExpandPath(path);
		}
	}
}
