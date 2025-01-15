using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000255 RID: 597
	internal static class MetadataUtils
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x00045E48 File Offset: 0x00044048
		internal static bool TryGetODataAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, out string value)
		{
			object annotationValue = model.GetAnnotationValue(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName);
			if (annotationValue == null)
			{
				value = null;
				return false;
			}
			IEdmStringValue edmStringValue = annotationValue as IEdmStringValue;
			if (edmStringValue == null)
			{
				throw new ODataException(Strings.ODataAtomWriterMetadataUtils_InvalidAnnotationValue(localName, annotationValue.GetType().FullName));
			}
			value = edmStringValue.Value;
			return true;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00045E98 File Offset: 0x00044098
		internal static void SetODataAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, string value)
		{
			IEdmStringValue edmStringValue = null;
			if (value != null)
			{
				IEdmStringTypeReference @string = EdmCoreModel.Instance.GetString(true);
				edmStringValue = new EdmStringConstant(@string, value);
			}
			model.SetAnnotationValue(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName, edmStringValue);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00045EE0 File Offset: 0x000440E0
		internal static IEnumerable<IEdmDirectValueAnnotation> GetODataAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			IEnumerable<IEdmDirectValueAnnotation> enumerable = model.DirectValueAnnotations(annotatable);
			if (enumerable == null)
			{
				return null;
			}
			return Enumerable.Where<IEdmDirectValueAnnotation>(enumerable, (IEdmDirectValueAnnotation a) => a.NamespaceUri == "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00045F20 File Offset: 0x00044120
		internal static IEdmTypeReference GetEdmType(this ODataAnnotatable annotatable)
		{
			if (annotatable == null)
			{
				return null;
			}
			ODataTypeAnnotation annotation = annotatable.GetAnnotation<ODataTypeAnnotation>();
			if (annotation != null)
			{
				return annotation.Type;
			}
			return null;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00045F44 File Offset: 0x00044144
		internal static IEdmType ResolveTypeNameForWrite(IEdmModel model, string typeName)
		{
			EdmTypeKind edmTypeKind;
			return MetadataUtils.ResolveTypeName(model, null, typeName, null, ODataVersion.V3, out edmTypeKind);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00045F60 File Offset: 0x00044160
		internal static IEdmType ResolveTypeNameForRead(IEdmModel model, IEdmType expectedType, string typeName, ODataReaderBehavior readerBehavior, ODataVersion version, out EdmTypeKind typeKind)
		{
			Func<IEdmType, string, IEdmType> func = ((readerBehavior == null) ? null : readerBehavior.TypeResolver);
			return MetadataUtils.ResolveTypeName(model, expectedType, typeName, func, version, out typeKind);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00045F88 File Offset: 0x00044188
		internal static IEdmType ResolveTypeName(IEdmModel model, IEdmType expectedType, string typeName, Func<IEdmType, string, IEdmType> customTypeResolver, ODataVersion version, out EdmTypeKind typeKind)
		{
			IEdmType edmType = null;
			string text = ((version >= ODataVersion.V3) ? EdmLibraryExtensions.GetCollectionItemTypeName(typeName) : null);
			if (text == null)
			{
				if (customTypeResolver != null && model.IsUserModel())
				{
					edmType = customTypeResolver.Invoke(expectedType, typeName);
					if (edmType == null)
					{
						throw new ODataException(Strings.MetadataUtils_ResolveTypeName(typeName));
					}
				}
				else
				{
					edmType = model.FindType(typeName);
				}
				if (version < ODataVersion.V3 && edmType != null && edmType.IsSpatial())
				{
					edmType = null;
				}
				typeKind = ((edmType == null) ? EdmTypeKind.None : edmType.TypeKind);
			}
			else
			{
				typeKind = EdmTypeKind.Collection;
				IEdmType edmType2 = null;
				if (customTypeResolver != null && expectedType != null && expectedType.TypeKind == EdmTypeKind.Collection)
				{
					edmType2 = ((IEdmCollectionType)expectedType).ElementType.Definition;
				}
				EdmTypeKind edmTypeKind;
				IEdmType edmType3 = MetadataUtils.ResolveTypeName(model, edmType2, text, customTypeResolver, version, out edmTypeKind);
				if (edmType3 != null)
				{
					edmType = EdmLibraryExtensions.GetCollectionType(edmType3);
				}
			}
			return edmType;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004603C File Offset: 0x0004423C
		internal static IEdmFunctionImport[] CalculateAlwaysBindableOperationsForType(IEdmType bindingType, IEdmModel model, EdmTypeResolver edmTypeResolver)
		{
			List<IEdmFunctionImport> list = new List<IEdmFunctionImport>();
			foreach (IEdmEntityContainer edmEntityContainer in model.EntityContainers())
			{
				foreach (IEdmFunctionImport edmFunctionImport in edmEntityContainer.FunctionImports())
				{
					if (edmFunctionImport.IsBindable && model.IsAlwaysBindable(edmFunctionImport))
					{
						IEdmFunctionParameter edmFunctionParameter = Enumerable.FirstOrDefault<IEdmFunctionParameter>(edmFunctionImport.Parameters);
						if (edmFunctionParameter != null)
						{
							IEdmType definition = edmTypeResolver.GetParameterType(edmFunctionParameter).Definition;
							if (definition.IsAssignableFrom(bindingType))
							{
								list.Add(edmFunctionImport);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004610C File Offset: 0x0004430C
		internal static IEdmTypeReference LookupTypeOfValueTerm(string qualifiedTermName, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference = null;
			IEdmValueTerm edmValueTerm = model.FindValueTerm(qualifiedTermName);
			if (edmValueTerm != null)
			{
				edmTypeReference = edmValueTerm.Type;
			}
			return edmTypeReference;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004612E File Offset: 0x0004432E
		internal static IEdmTypeReference GetNullablePayloadTypeReference(IEdmType payloadType)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			return null;
		}
	}
}
