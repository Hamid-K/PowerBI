using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000191 RID: 401
	internal static class ODataPathExtensions
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x00039AB3 File Offset: 0x00037CB3
		public static IEdmTypeReference EdmType(this ODataPath path)
		{
			return path.LastSegment.EdmType.ToTypeReference();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00039AC5 File Offset: 0x00037CC5
		public static IEdmNavigationSource NavigationSource(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<IEdmNavigationSource>(new DetermineNavigationSourceTranslator());
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00039AD7 File Offset: 0x00037CD7
		public static bool IsCollection(this ODataPath path)
		{
			return path.LastSegment.TranslateWith<bool>(new IsCollectionTranslator());
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00039AEC File Offset: 0x00037CEC
		public static ODataPath AppendNavigationPropertySegment(this ODataPath path, IEdmNavigationProperty navigationProperty, IEdmNavigationSource navigationSource)
		{
			ODataPath odataPath = new ODataPath(path);
			NavigationPropertySegment navigationPropertySegment = new NavigationPropertySegment(navigationProperty, navigationSource);
			odataPath.Add(navigationPropertySegment);
			return odataPath;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00039B10 File Offset: 0x00037D10
		public static ODataPath AppendPropertySegment(this ODataPath path, IEdmStructuralProperty property)
		{
			ODataPath odataPath = new ODataPath(path);
			PropertySegment propertySegment = new PropertySegment(property);
			odataPath.Add(propertySegment);
			return odataPath;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00039B34 File Offset: 0x00037D34
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

		// Token: 0x06001381 RID: 4993 RVA: 0x00039BA8 File Offset: 0x00037DA8
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

		// Token: 0x06001382 RID: 4994 RVA: 0x00039C20 File Offset: 0x00037E20
		public static ODataPath TrimEndingTypeSegment(this ODataPath path)
		{
			SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			return splitEndingSegmentOfTypeHandler.FirstPart;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00039C40 File Offset: 0x00037E40
		public static bool IsIndividualProperty(this ODataPath path)
		{
			ODataPathSegment lastSegment = path.TrimEndingTypeSegment().LastSegment;
			return lastSegment is PropertySegment || lastSegment is DynamicPathSegment;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00039C6C File Offset: 0x00037E6C
		public static bool IsUndeclared(this ODataPath path)
		{
			ODataPathSegment lastSegment = path.TrimEndingTypeSegment().LastSegment;
			return lastSegment is DynamicPathSegment;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00039C90 File Offset: 0x00037E90
		public static string ToContextUrlPathString(this ODataPath path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			PathSegmentToContextUrlPathTranslator defaultInstance = PathSegmentToContextUrlPathTranslator.DefaultInstance;
			ODataPathSegment odataPathSegment = null;
			bool flag = false;
			foreach (ODataPathSegment odataPathSegment2 in path)
			{
				OperationSegment operationSegment = odataPathSegment2 as OperationSegment;
				OperationImportSegment operationImportSegment = odataPathSegment2 as OperationImportSegment;
				if (operationImportSegment != null)
				{
					IEdmOperationImport edmOperationImport = operationImportSegment.OperationImports.FirstOrDefault<IEdmOperationImport>();
					EdmPathExpression edmPathExpression = edmOperationImport.EntitySet as EdmPathExpression;
					if (edmPathExpression != null)
					{
						stringBuilder.Append(edmPathExpression.Path);
					}
					else
					{
						stringBuilder = ((edmOperationImport.Operation.ReturnType != null) ? new StringBuilder(edmOperationImport.Operation.ReturnType.FullName()) : new StringBuilder("Edm.Untyped"));
						flag = true;
					}
				}
				else if (operationSegment != null)
				{
					IEdmOperation edmOperation = operationSegment.Operations.FirstOrDefault<IEdmOperation>();
					if (edmOperation.IsBound && odataPathSegment != null && edmOperation.Parameters.First<IEdmOperationParameter>().Type.Definition == odataPathSegment.EdmType)
					{
						if (edmOperation.EntitySetPath != null)
						{
							using (IEnumerator<string> enumerator2 = edmOperation.EntitySetPath.PathSegments.Skip(1).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string text = enumerator2.Current;
									stringBuilder.Append('/');
									stringBuilder.Append(text);
								}
								goto IL_01B5;
							}
						}
						if (operationSegment.EntitySet != null)
						{
							stringBuilder = new StringBuilder(operationSegment.EntitySet.Name);
						}
						else
						{
							stringBuilder = ((edmOperation.ReturnType != null) ? new StringBuilder(edmOperation.ReturnType.FullName()) : new StringBuilder("Edm.Untyped"));
							flag = true;
						}
					}
				}
				else if (flag)
				{
					stringBuilder = new StringBuilder(odataPathSegment2.EdmType.FullTypeName());
					flag = false;
				}
				else
				{
					stringBuilder.Append(odataPathSegment2.TranslateWith<string>(defaultInstance));
				}
				IL_01B5:
				odataPathSegment = odataPathSegment2;
			}
			return stringBuilder.ToString().TrimStart(new char[] { '/' });
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00039EBC File Offset: 0x000380BC
		public static string ToResourcePathString(this ODataPath path, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			return string.Concat(path.WalkWith<string>(new PathSegmentToResourcePathTranslator(urlKeyDelimiter)).ToArray<string>()).TrimStart(new char[] { '/' });
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00039EE4 File Offset: 0x000380E4
		public static ODataSelectPath ToSelectPath(this ODataExpandPath path)
		{
			return new ODataSelectPath(path);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00039EEC File Offset: 0x000380EC
		public static ODataExpandPath ToExpandPath(this ODataSelectPath path)
		{
			return new ODataExpandPath(path);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00039EF4 File Offset: 0x000380F4
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
