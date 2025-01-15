using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.Experimental.OData.Query.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000007 RID: 7
	public static class ODataQueryUtils
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		public static bool GetCanReflectOnInstanceTypeProperty(this IEdmProperty property, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmPropertyAnnotation annotationValue = model.GetAnnotationValue(property);
			return annotationValue != null && annotationValue.CanReflectOnProperty;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002164 File Offset: 0x00000364
		public static void SetCanReflectOnInstanceTypeProperty(this IEdmProperty property, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmPropertyAnnotation odataQueryEdmPropertyAnnotation = model.GetAnnotationValue(property);
			if (odataQueryEdmPropertyAnnotation == null)
			{
				if (canReflect)
				{
					odataQueryEdmPropertyAnnotation = new ODataQueryEdmPropertyAnnotation
					{
						CanReflectOnProperty = true
					};
					model.SetAnnotationValue(property, odataQueryEdmPropertyAnnotation);
					return;
				}
			}
			else
			{
				odataQueryEdmPropertyAnnotation.CanReflectOnProperty = canReflect;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public static ODataServiceOperationResultKind? GetServiceOperationResultKind(this IEdmFunctionImport serviceOperation, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(serviceOperation, "functionImport");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmServiceOperationAnnotation annotationValue = model.GetAnnotationValue(serviceOperation);
			if (annotationValue != null)
			{
				return new ODataServiceOperationResultKind?(annotationValue.ResultKind);
			}
			return default(ODataServiceOperationResultKind?);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public static void SetServiceOperationResultKind(this IEdmFunctionImport serviceOperation, IEdmModel model, ODataServiceOperationResultKind resultKind)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(serviceOperation, "serviceOperation");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataQueryEdmServiceOperationAnnotation annotationValue = model.GetAnnotationValue(serviceOperation);
			if (annotationValue == null)
			{
				ODataQueryEdmServiceOperationAnnotation odataQueryEdmServiceOperationAnnotation = new ODataQueryEdmServiceOperationAnnotation
				{
					ResultKind = resultKind
				};
				model.SetAnnotationValue(serviceOperation, odataQueryEdmServiceOperationAnnotation);
				return;
			}
			annotationValue.ResultKind = resultKind;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
		public static IEdmFunctionImport ResolveServiceOperation(this IEdmModel model, string operationName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			IEdmFunctionImport edmFunctionImport = model.TryResolveServiceOperation(operationName);
			if (edmFunctionImport == null)
			{
				throw new ODataException(Strings.ODataQueryUtils_DidNotFindServiceOperation(operationName));
			}
			return edmFunctionImport;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002284 File Offset: 0x00000484
		public static IEdmFunctionImport TryResolveServiceOperation(this IEdmModel model, string operationName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			IEnumerable<IEdmEntityContainer> enumerable = model.EntityContainers();
			if (enumerable == null)
			{
				return null;
			}
			IEdmFunctionImport edmFunctionImport = null;
			foreach (IEdmEntityContainer edmEntityContainer in enumerable)
			{
				IEnumerable<IEdmFunctionImport> enumerable2 = edmEntityContainer.FindFunctionImports(operationName);
				if (enumerable2 != null)
				{
					foreach (IEdmFunctionImport edmFunctionImport2 in enumerable2)
					{
						if (ODataQueryUtils.IsServiceOperation(edmFunctionImport2, model))
						{
							if (edmFunctionImport != null)
							{
								throw new ODataException(Strings.ODataQueryUtils_FoundMultipleServiceOperations(operationName));
							}
							edmFunctionImport = edmFunctionImport2;
						}
					}
				}
			}
			return edmFunctionImport;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002350 File Offset: 0x00000550
		public static Type GetInstanceType(this IEdmTypeReference typeReference, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (typeReference.TypeKind() == EdmTypeKind.Primitive)
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitive();
				return EdmLibraryExtensions.GetPrimitiveClrType(edmPrimitiveTypeReference);
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(typeReference.Definition);
			if (annotationValue != null)
			{
				return annotationValue.InstanceType;
			}
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023A4 File Offset: 0x000005A4
		public static Type GetInstanceType(this IEdmType type, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				return EdmLibraryExtensions.GetPrimitiveClrType((IEdmPrimitiveTypeReference)type.ToTypeReference(false));
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(type);
			if (annotationValue != null)
			{
				return annotationValue.InstanceType;
			}
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F8 File Offset: 0x000005F8
		public static void SetInstanceType(this IEdmType type, IEdmModel model, Type instanceType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				throw new ODataException(Strings.ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType);
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(type);
			if (annotationValue == null)
			{
				if (instanceType != null)
				{
					ODataQueryEdmTypeAnnotation odataQueryEdmTypeAnnotation = new ODataQueryEdmTypeAnnotation
					{
						InstanceType = instanceType
					};
					model.SetAnnotationValue(type, odataQueryEdmTypeAnnotation);
					return;
				}
			}
			else
			{
				annotationValue.InstanceType = instanceType;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000245C File Offset: 0x0000065C
		public static bool GetCanReflectOnInstanceType(this IEdmTypeReference typeReference, IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (typeReference.TypeKind() == EdmTypeKind.Primitive)
			{
				return true;
			}
			ODataQueryEdmTypeAnnotation annotationValue = model.GetAnnotationValue(typeReference.Definition);
			return annotationValue != null && annotationValue.CanReflectOnInstanceType;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A2 File Offset: 0x000006A2
		public static void SetCanReflectOnInstanceType(this IEdmTypeReference typeReference, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			typeReference.Definition.SetCanReflectOnInstanceType(model, canReflect);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024C8 File Offset: 0x000006C8
		public static void SetCanReflectOnInstanceType(this IEdmType type, IEdmModel model, bool canReflect)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (type.TypeKind == EdmTypeKind.Primitive)
			{
				throw new ODataException(Strings.ODataQueryUtils_CannotSetMetadataAnnotationOnPrimitiveType);
			}
			ODataQueryEdmTypeAnnotation odataQueryEdmTypeAnnotation = model.GetAnnotationValue(type);
			if (odataQueryEdmTypeAnnotation == null)
			{
				if (canReflect)
				{
					odataQueryEdmTypeAnnotation = new ODataQueryEdmTypeAnnotation
					{
						CanReflectOnInstanceType = true
					};
					model.SetAnnotationValue(type, odataQueryEdmTypeAnnotation);
					return;
				}
			}
			else
			{
				odataQueryEdmTypeAnnotation.CanReflectOnInstanceType = canReflect;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000252C File Offset: 0x0000072C
		public static IEdmEntitySet ResolveEntitySet(this IEdmModel model, string entitySetName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			IEdmEntitySet edmEntitySet = model.TryResolveEntitySet(entitySetName);
			if (edmEntitySet == null)
			{
				throw new ODataException(Strings.ODataQueryUtils_DidNotFindEntitySet(entitySetName));
			}
			return edmEntitySet;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002568 File Offset: 0x00000768
		public static IEdmEntitySet TryResolveEntitySet(this IEdmModel model, string entitySetName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			IEnumerable<IEdmEntityContainer> enumerable = model.EntityContainers();
			if (enumerable == null)
			{
				return null;
			}
			IEdmEntitySet edmEntitySet = null;
			foreach (IEdmEntityContainer edmEntityContainer in enumerable)
			{
				edmEntitySet = edmEntityContainer.FindEntitySet(entitySetName);
				if (edmEntitySet != null)
				{
					break;
				}
			}
			return edmEntitySet;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025DC File Offset: 0x000007DC
		private static bool IsServiceOperation(IEdmFunctionImport functionImport, IEdmModel model)
		{
			ODataQueryEdmServiceOperationAnnotation annotationValue = model.GetAnnotationValue(functionImport);
			return annotationValue != null;
		}
	}
}
