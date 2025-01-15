using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNet.OData.Builder.Conventions;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000123 RID: 291
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class LinkGenerationHelpers
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00028D1C File Offset: 0x00026F1C
		public static Uri GenerateSelfLink(this ResourceContext resourceContext, bool includeCast)
		{
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			if (resourceContext.InternalUrlHelper == null)
			{
				throw Error.Argument("resourceContext", SRResources.UrlHelperNull, new object[] { typeof(ResourceContext).Name });
			}
			IList<ODataPathSegment> list = resourceContext.GenerateBaseODataPathSegments();
			bool flag = resourceContext.StructuredType == resourceContext.NavigationSource.EntityType();
			if (includeCast && !flag)
			{
				list.Add(new TypeSegment(resourceContext.StructuredType, null));
			}
			string text = resourceContext.InternalUrlHelper.CreateODataLink(list);
			if (text == null)
			{
				return null;
			}
			return new Uri(text);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00028DB4 File Offset: 0x00026FB4
		public static Uri GenerateNavigationPropertyLink(this ResourceContext resourceContext, IEdmNavigationProperty navigationProperty, bool includeCast)
		{
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			if (resourceContext.InternalUrlHelper == null)
			{
				throw Error.Argument("resourceContext", SRResources.UrlHelperNull, new object[] { typeof(ResourceContext).Name });
			}
			IList<ODataPathSegment> list = resourceContext.GenerateBaseODataPathSegments();
			if (includeCast)
			{
				list.Add(new TypeSegment(resourceContext.StructuredType, null));
			}
			list.Add(new NavigationPropertySegment(navigationProperty, null));
			string text = resourceContext.InternalUrlHelper.CreateODataLink(list);
			if (text == null)
			{
				return null;
			}
			return new Uri(text);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00028E40 File Offset: 0x00027040
		public static Uri GenerateActionLink(this ResourceSetContext resourceSetContext, IEdmOperation action)
		{
			if (resourceSetContext == null)
			{
				throw Error.ArgumentNull("resourceSetContext");
			}
			if (action == null)
			{
				throw Error.ArgumentNull("action");
			}
			IEdmOperationParameter edmOperationParameter = action.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (edmOperationParameter == null || !edmOperationParameter.Type.IsCollection() || !((IEdmCollectionType)edmOperationParameter.Type.Definition).ElementType.IsEntity())
			{
				throw Error.Argument("action", SRResources.ActionNotBoundToCollectionOfEntity, new object[] { action.Name });
			}
			return resourceSetContext.GenerateActionLink(edmOperationParameter.Type, action);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00028ED0 File Offset: 0x000270D0
		internal static Uri GenerateActionLink(this ResourceSetContext feedContext, string bindingParameterType, string actionName)
		{
			if (feedContext.EntitySetBase is IEdmContainedEntitySet)
			{
				return null;
			}
			if (feedContext.EdmModel == null)
			{
				return null;
			}
			IEdmModel edmModel = feedContext.EdmModel;
			string collectionElementTypeName = DeserializationHelpers.GetCollectionElementTypeName(bindingParameterType, false);
			IEdmTypeReference edmTypeReference = new EdmCollectionTypeReference(new EdmCollectionType(edmModel.FindDeclaredType(collectionElementTypeName).ToEdmTypeReference(true)));
			IEdmOperation edmOperation = edmModel.FindDeclaredOperations(actionName).First<IEdmOperation>();
			return feedContext.GenerateActionLink(edmTypeReference, edmOperation);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00028F30 File Offset: 0x00027130
		internal static Uri GenerateActionLink(this ResourceSetContext resourceSetContext, IEdmTypeReference bindingParameterType, IEdmOperation action)
		{
			if (resourceSetContext.EntitySetBase is IEdmContainedEntitySet)
			{
				return null;
			}
			IList<ODataPathSegment> list = new List<ODataPathSegment>();
			resourceSetContext.GenerateBaseODataPathSegmentsForFeed(list);
			if (resourceSetContext.EntitySetBase.Type.FullTypeName() != bindingParameterType.FullName())
			{
				list.Add(new TypeSegment(bindingParameterType.Definition, resourceSetContext.EntitySetBase));
			}
			OperationSegment operationSegment = new OperationSegment(action, null);
			list.Add(operationSegment);
			string text = resourceSetContext.InternalUrlHelper.CreateODataLink(list);
			if (text != null)
			{
				return new Uri(text);
			}
			return null;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00028FB4 File Offset: 0x000271B4
		public static Uri GenerateFunctionLink(this ResourceSetContext resourceSetContext, IEdmOperation function)
		{
			if (resourceSetContext == null)
			{
				throw Error.ArgumentNull("resourceSetContext");
			}
			if (function == null)
			{
				throw Error.ArgumentNull("function");
			}
			IEdmOperationParameter edmOperationParameter = function.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (edmOperationParameter == null || !edmOperationParameter.Type.IsCollection() || !((IEdmCollectionType)edmOperationParameter.Type.Definition).ElementType.IsEntity())
			{
				throw Error.Argument("function", SRResources.FunctionNotBoundToCollectionOfEntity, new object[] { function.Name });
			}
			return resourceSetContext.GenerateFunctionLink(edmOperationParameter.Type, function, function.Parameters.Select((IEdmOperationParameter p) => p.Name));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002906C File Offset: 0x0002726C
		internal static Uri GenerateFunctionLink(this ResourceSetContext resourceSetContext, IEdmTypeReference bindingParameterType, IEdmOperation functionImport, IEnumerable<string> parameterNames)
		{
			if (resourceSetContext.EntitySetBase is IEdmContainedEntitySet)
			{
				return null;
			}
			IList<ODataPathSegment> list = new List<ODataPathSegment>();
			resourceSetContext.GenerateBaseODataPathSegmentsForFeed(list);
			if (resourceSetContext.EntitySetBase.Type.FullTypeName() != bindingParameterType.Definition.FullTypeName())
			{
				list.Add(new TypeSegment(bindingParameterType.Definition, null));
			}
			IList<OperationSegmentParameter> list2 = new List<OperationSegmentParameter>();
			foreach (string text in parameterNames.Skip(1))
			{
				string text2 = "@" + text;
				list2.Add(new OperationSegmentParameter(text, new ConstantNode(text2, text2)));
			}
			OperationSegment operationSegment = new OperationSegment(new IEdmOperation[] { functionImport }, list2, null);
			list.Add(operationSegment);
			string text3 = resourceSetContext.InternalUrlHelper.CreateODataLink(list);
			if (text3 != null)
			{
				return new Uri(text3);
			}
			return null;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00029164 File Offset: 0x00027364
		internal static Uri GenerateFunctionLink(this ResourceSetContext feedContext, string bindingParameterType, string functionName, IEnumerable<string> parameterNames)
		{
			if (feedContext.EntitySetBase is IEdmContainedEntitySet)
			{
				return null;
			}
			if (feedContext.EdmModel == null)
			{
				return null;
			}
			IEdmModel edmModel = feedContext.EdmModel;
			string collectionElementTypeName = DeserializationHelpers.GetCollectionElementTypeName(bindingParameterType, false);
			IEdmTypeReference edmTypeReference = new EdmCollectionTypeReference(new EdmCollectionType(edmModel.FindDeclaredType(collectionElementTypeName).ToEdmTypeReference(true)));
			IEdmOperation edmOperation = edmModel.FindDeclaredOperations(functionName).First<IEdmOperation>();
			return feedContext.GenerateFunctionLink(edmTypeReference, edmOperation, parameterNames);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000291C8 File Offset: 0x000273C8
		public static Uri GenerateActionLink(this ResourceContext resourceContext, IEdmOperation action)
		{
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			if (action == null)
			{
				throw Error.ArgumentNull("action");
			}
			IEdmOperationParameter edmOperationParameter = action.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (edmOperationParameter == null || !edmOperationParameter.Type.IsEntity())
			{
				throw Error.Argument("action", SRResources.ActionNotBoundToEntity, new object[] { action.Name });
			}
			return resourceContext.GenerateActionLink(edmOperationParameter.Type, action);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002923C File Offset: 0x0002743C
		internal static Uri GenerateActionLink(this ResourceContext resourceContext, IEdmTypeReference bindingParameterType, IEdmOperation action)
		{
			if (resourceContext.NavigationSource is IEdmContainedEntitySet)
			{
				return null;
			}
			IList<ODataPathSegment> list = resourceContext.GenerateBaseODataPathSegments();
			if (resourceContext.NavigationSource.EntityType() != bindingParameterType.Definition)
			{
				list.Add(new TypeSegment((IEdmEntityType)bindingParameterType.Definition, null));
			}
			OperationSegment operationSegment = new OperationSegment(new IEdmOperation[] { action }, null);
			list.Add(operationSegment);
			string text = resourceContext.InternalUrlHelper.CreateODataLink(list);
			if (text != null)
			{
				return new Uri(text);
			}
			return null;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000292BC File Offset: 0x000274BC
		internal static Uri GenerateActionLink(this ResourceContext resourceContext, string bindingParameterType, string actionName)
		{
			if (resourceContext.NavigationSource is IEdmContainedEntitySet)
			{
				return null;
			}
			if (resourceContext.EdmModel == null)
			{
				return null;
			}
			IEdmModel edmModel = resourceContext.EdmModel;
			IEdmTypeReference edmTypeReference = edmModel.FindDeclaredType(bindingParameterType).ToEdmTypeReference(true);
			IEdmOperation edmOperation = edmModel.FindDeclaredOperations(actionName).First<IEdmOperation>();
			return resourceContext.GenerateActionLink(edmTypeReference, edmOperation);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002930C File Offset: 0x0002750C
		public static Uri GenerateFunctionLink(this ResourceContext resourceContext, IEdmOperation function)
		{
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			if (function == null)
			{
				throw Error.ArgumentNull("function");
			}
			IEdmOperationParameter edmOperationParameter = function.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (edmOperationParameter == null || !edmOperationParameter.Type.IsEntity())
			{
				throw Error.Argument("function", SRResources.FunctionNotBoundToEntity, new object[] { function.Name });
			}
			return resourceContext.GenerateFunctionLink(edmOperationParameter.Type.FullName(), function.FullName(), function.Parameters.Select((IEdmOperationParameter p) => p.Name));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000293B4 File Offset: 0x000275B4
		internal static Uri GenerateFunctionLink(this ResourceContext resourceContext, IEdmTypeReference bindingParameterType, IEdmOperation function, IEnumerable<string> parameterNames)
		{
			IList<ODataPathSegment> list = resourceContext.GenerateBaseODataPathSegments();
			if (resourceContext.NavigationSource.EntityType() != bindingParameterType.Definition)
			{
				list.Add(new TypeSegment(bindingParameterType.Definition, null));
			}
			IList<OperationSegmentParameter> list2 = new List<OperationSegmentParameter>();
			foreach (string text in parameterNames.Skip(1))
			{
				string text2 = "@" + text;
				list2.Add(new OperationSegmentParameter(text, new ConstantNode(text2, text2)));
			}
			OperationSegment operationSegment = new OperationSegment(new IEdmOperation[] { function }, list2, null);
			list.Add(operationSegment);
			string text3 = resourceContext.InternalUrlHelper.CreateODataLink(list);
			if (text3 != null)
			{
				return new Uri(text3);
			}
			return null;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00029488 File Offset: 0x00027688
		internal static Uri GenerateFunctionLink(this ResourceContext resourceContext, string bindingParameterType, string functionName, IEnumerable<string> parameterNames)
		{
			if (resourceContext.EdmModel == null)
			{
				return null;
			}
			IEdmModel edmModel = resourceContext.EdmModel;
			IEdmTypeReference edmTypeReference = edmModel.FindDeclaredType(bindingParameterType).ToEdmTypeReference(true);
			IEdmOperation edmOperation = edmModel.FindDeclaredOperations(functionName).First<IEdmOperation>();
			return resourceContext.GenerateFunctionLink(edmTypeReference, edmOperation, parameterNames);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000294C8 File Offset: 0x000276C8
		internal static IList<ODataPathSegment> GenerateBaseODataPathSegments(this ResourceContext resourceContext)
		{
			IList<ODataPathSegment> list = new List<ODataPathSegment>();
			if (resourceContext.NavigationSource.NavigationSourceKind() == EdmNavigationSourceKind.Singleton)
			{
				list.Add(new SingletonSegment((IEdmSingleton)resourceContext.NavigationSource));
			}
			else
			{
				resourceContext.GenerateBaseODataPathSegmentsForEntity(list);
			}
			return list;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002950C File Offset: 0x0002770C
		private static void GenerateBaseODataPathSegmentsForNonSingletons(Microsoft.AspNet.OData.Routing.ODataPath path, IEdmNavigationSource navigationSource, IList<ODataPathSegment> odataPath)
		{
			bool flag = false;
			bool flag2 = false;
			if (path != null)
			{
				ReadOnlyCollection<ODataPathSegment> segments = path.Segments;
				int count = segments.Count;
				int num = -1;
				for (int i = 0; i < count; i++)
				{
					ODataPathSegment odataPathSegment = segments[i];
					IEdmNavigationSource edmNavigationSource = null;
					EntitySetSegment entitySetSegment = odataPathSegment as EntitySetSegment;
					if (entitySetSegment != null)
					{
						edmNavigationSource = entitySetSegment.EntitySet;
					}
					NavigationPropertySegment navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
					if (navigationPropertySegment != null)
					{
						edmNavigationSource = navigationPropertySegment.NavigationSource;
					}
					if (flag2)
					{
						odataPath.Add(odataPathSegment);
					}
					else if (navigationPropertySegment != null && navigationPropertySegment.NavigationProperty.ContainsTarget)
					{
						flag2 = true;
						if (num != -1)
						{
							for (int j = num; j <= i; j++)
							{
								odataPath.Add(segments[j]);
							}
						}
					}
					if (edmNavigationSource != null)
					{
						num = i;
						if (edmNavigationSource == navigationSource)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag || !flag2)
			{
				odataPath.Clear();
				if (navigationSource is IEdmContainedEntitySet)
				{
					IEdmEntitySet edmEntitySet = new EdmEntitySet(new EdmEntityContainer("NS", "Default"), navigationSource.Name, navigationSource.EntityType());
					odataPath.Add(new EntitySetSegment(edmEntitySet));
					return;
				}
				odataPath.Add(new EntitySetSegment((IEdmEntitySet)navigationSource));
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002962F File Offset: 0x0002782F
		private static void GenerateBaseODataPathSegmentsForEntity(this ResourceContext resourceContext, IList<ODataPathSegment> odataPath)
		{
			LinkGenerationHelpers.GenerateBaseODataPathSegmentsForNonSingletons(resourceContext.SerializerContext.Path, resourceContext.NavigationSource, odataPath);
			odataPath.Add(new KeySegment(ConventionsHelpers.GetEntityKey(resourceContext), resourceContext.StructuredType as IEdmEntityType, null));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00029665 File Offset: 0x00027865
		private static void GenerateBaseODataPathSegmentsForFeed(this ResourceSetContext feedContext, IList<ODataPathSegment> odataPath)
		{
			LinkGenerationHelpers.GenerateBaseODataPathSegmentsForNonSingletons(feedContext.InternalRequest.Context.Path, feedContext.EntitySetBase, odataPath);
		}
	}
}
