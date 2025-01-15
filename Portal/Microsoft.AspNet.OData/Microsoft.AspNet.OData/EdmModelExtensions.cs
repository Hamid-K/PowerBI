using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000037 RID: 55
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class EdmModelExtensions
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00006510 File Offset: 0x00004710
		public static NavigationSourceLinkBuilderAnnotation GetNavigationSourceLinkBuilder(this IEdmModel model, IEdmNavigationSource navigationSource)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			NavigationSourceLinkBuilderAnnotation navigationSourceLinkBuilderAnnotation = model.GetAnnotationValue(navigationSource);
			if (navigationSourceLinkBuilderAnnotation == null)
			{
				navigationSourceLinkBuilderAnnotation = new NavigationSourceLinkBuilderAnnotation(navigationSource, model);
				model.SetNavigationSourceLinkBuilder(navigationSource, navigationSourceLinkBuilderAnnotation);
			}
			return navigationSourceLinkBuilderAnnotation;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006547 File Offset: 0x00004747
		public static void SetNavigationSourceLinkBuilder(this IEdmModel model, IEdmNavigationSource navigationSource, NavigationSourceLinkBuilderAnnotation navigationSourceLinkBuilder)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			model.SetAnnotationValue(navigationSource, navigationSourceLinkBuilder);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006560 File Offset: 0x00004760
		public static OperationLinkBuilder GetOperationLinkBuilder(this IEdmModel model, IEdmOperation operation)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (operation == null)
			{
				throw Error.ArgumentNull("operation");
			}
			OperationLinkBuilder operationLinkBuilder = model.GetAnnotationValue(operation);
			if (operationLinkBuilder == null)
			{
				operationLinkBuilder = EdmModelExtensions.GetDefaultOperationLinkBuilder(operation);
				model.SetOperationLinkBuilder(operation, operationLinkBuilder);
			}
			return operationLinkBuilder;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000065A4 File Offset: 0x000047A4
		public static void SetOperationLinkBuilder(this IEdmModel model, IEdmOperation operation, OperationLinkBuilder operationLinkBuilder)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			model.SetAnnotationValue(operation, operationLinkBuilder);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000065BC File Offset: 0x000047BC
		internal static ClrTypeCache GetTypeMappingCache(this IEdmModel model)
		{
			ClrTypeCache clrTypeCache = model.GetAnnotationValue(model);
			if (clrTypeCache == null)
			{
				clrTypeCache = new ClrTypeCache();
				model.SetAnnotationValue(model, clrTypeCache);
			}
			return clrTypeCache;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000065E3 File Offset: 0x000047E3
		internal static void SetOperationTitleAnnotation(this IEdmModel model, IEdmOperation action, OperationTitleAnnotation title)
		{
			model.SetAnnotationValue(action, title);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000065ED File Offset: 0x000047ED
		internal static OperationTitleAnnotation GetOperationTitleAnnotation(this IEdmModel model, IEdmOperation operation)
		{
			return model.GetAnnotationValue(operation);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000065F8 File Offset: 0x000047F8
		private static OperationLinkBuilder GetDefaultOperationLinkBuilder(IEdmOperation operation)
		{
			OperationLinkBuilder operationLinkBuilder = null;
			if (operation.Parameters != null)
			{
				if (operation.Parameters.First<IEdmOperationParameter>().Type.IsEntity())
				{
					if (operation is IEdmAction)
					{
						operationLinkBuilder = new OperationLinkBuilder((ResourceContext resourceContext) => resourceContext.GenerateActionLink(operation), true);
					}
					else
					{
						operationLinkBuilder = new OperationLinkBuilder((ResourceContext resourceContext) => resourceContext.GenerateFunctionLink(operation), true);
					}
				}
				else if (operation.Parameters.First<IEdmOperationParameter>().Type.IsCollection())
				{
					if (operation is IEdmAction)
					{
						operationLinkBuilder = new OperationLinkBuilder((ResourceSetContext reseourceSetContext) => reseourceSetContext.GenerateActionLink(operation), true);
					}
					else
					{
						operationLinkBuilder = new OperationLinkBuilder((ResourceSetContext reseourceSetContext) => reseourceSetContext.GenerateFunctionLink(operation), true);
					}
				}
			}
			return operationLinkBuilder;
		}
	}
}
