using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client.Annotation
{
	// Token: 0x02000112 RID: 274
	internal static class AnnotationHelper
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002BC9C File Offset: 0x00029E9C
		internal static bool TryGetMetadataAnnotation<TResult>(DataServiceContext context, object source, string term, string qualifier, out TResult annotationValue)
		{
			ClientEdmStructuredValue clientEdmStructuredValue = null;
			PropertyInfo propertyInfo = null;
			MethodInfo methodInfo = null;
			Tuple<object, MemberInfo> tuple = source as Tuple<object, MemberInfo>;
			if (tuple != null)
			{
				object item = tuple.Item1;
				MemberInfo item2 = tuple.Item2;
				propertyInfo = item2 as PropertyInfo;
				methodInfo = item2 as MethodInfo;
				if (item != null)
				{
					IEdmType orCreateEdmType = context.Model.GetOrCreateEdmType(item.GetType());
					if (orCreateEdmType is IEdmStructuredType)
					{
						ClientTypeAnnotation clientTypeAnnotation = context.Model.GetClientTypeAnnotation(orCreateEdmType);
						clientEdmStructuredValue = new ClientEdmStructuredValue(item, context.Model, clientTypeAnnotation);
					}
				}
			}
			else
			{
				if (propertyInfo == null)
				{
					propertyInfo = source as PropertyInfo;
				}
				if (methodInfo == null)
				{
					methodInfo = source as MethodInfo;
				}
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation;
			if (propertyInfo != null)
			{
				edmVocabularyAnnotation = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForPropertyInfo(context, propertyInfo, term, qualifier);
				return AnnotationHelper.TryEvaluateMetadataAnnotation<TResult>(context, edmVocabularyAnnotation, clientEdmStructuredValue, out annotationValue);
			}
			if (methodInfo != null)
			{
				edmVocabularyAnnotation = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForMethodInfo(context, methodInfo, term, qualifier);
				return AnnotationHelper.TryEvaluateMetadataAnnotation<TResult>(context, edmVocabularyAnnotation, clientEdmStructuredValue, out annotationValue);
			}
			Type type = source as Type;
			Type type2 = type;
			if (type == null)
			{
				type = source.GetType();
				type2 = Nullable.GetUnderlyingType(type) ?? type;
				IEdmType orCreateEdmType2 = context.Model.GetOrCreateEdmType(type2);
				if (orCreateEdmType2 is IEdmStructuredType)
				{
					ClientTypeAnnotation clientTypeAnnotation2 = context.Model.GetClientTypeAnnotation(orCreateEdmType2);
					clientEdmStructuredValue = new ClientEdmStructuredValue(source, context.Model, clientTypeAnnotation2);
				}
			}
			edmVocabularyAnnotation = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForType(context, type2, term, qualifier);
			return AnnotationHelper.TryEvaluateMetadataAnnotation<TResult>(context, edmVocabularyAnnotation, clientEdmStructuredValue, out annotationValue);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		internal static IEdmOperation GetEdmOperation(DataServiceContext context, MethodInfo methodInfo)
		{
			IEdmModel serviceModel = context.Format.ServiceModel;
			if (serviceModel == null)
			{
				return null;
			}
			Type[] array = (from p in methodInfo.GetParameters()
				select p.ParameterType).ToArray<Type>();
			Type type = null;
			IEnumerable<Type> enumerable;
			if (methodInfo.IsDefined(typeof(ExtensionAttribute), false))
			{
				type = array.First<Type>();
				enumerable = array.Skip(1);
			}
			else
			{
				type = methodInfo.DeclaringType;
				enumerable = array;
			}
			Type declaringType = methodInfo.DeclaringType;
			string text = declaringType.Namespace + ".";
			if (context.ResolveName != null)
			{
				string text2 = context.ResolveName(declaringType);
				if (text2 != null)
				{
					int num = text2.LastIndexOf('.');
					text = ((num > 0) ? text2.Substring(0, num + 1) : "");
				}
			}
			string serverDefinedName = ClientTypeUtil.GetServerDefinedName(methodInfo);
			IEnumerable<IEdmOperation> enumerable2 = from o in serviceModel.FindOperations(text + serverDefinedName)
				where o.IsBound
				select o;
			while (type != null)
			{
				foreach (IEdmOperation edmOperation in enumerable2)
				{
					Type type2;
					if (AnnotationHelper.TryGetClrTypeFromEdmTypeReference(context, edmOperation.Parameters.First<IEdmOperationParameter>().Type, methodInfo.IsDefined(typeof(ExtensionAttribute), false), out type2) && type2 == type && enumerable.SequenceEqual(AnnotationHelper.GetNonBindingParameterTypeArray(context, edmOperation.Parameters, true)))
					{
						return edmOperation;
					}
				}
				if (methodInfo.IsDefined(typeof(ExtensionAttribute), false) && type.IsGenericType())
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					List<Type> list = type.GetGenericArguments().ToList<Type>();
					if (list.Count == 1)
					{
						Type baseType = list[0].GetBaseType();
						if (baseType != null)
						{
							type = genericTypeDefinition.MakeGenericType(new Type[] { baseType });
							continue;
						}
					}
				}
				return null;
			}
			return null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002C01C File Offset: 0x0002A21C
		internal static IEdmOperationImport GetEdmOperationImport(DataServiceContext context, MethodInfo methodInfo)
		{
			IEdmModel serviceModel = context.Format.ServiceModel;
			if (serviceModel == null)
			{
				return null;
			}
			string serverDefinedName = ClientTypeUtil.GetServerDefinedName(methodInfo);
			IEnumerable<IEdmOperationImport> enumerable = serviceModel.FindDeclaredOperationImports(serverDefinedName);
			Type[] array = (from p in methodInfo.GetParameters()
				select p.ParameterType).ToArray<Type>();
			foreach (IEdmOperationImport edmOperationImport in enumerable)
			{
				IEnumerable<IEdmOperationParameter> parameters = edmOperationImport.Operation.Parameters;
				Type[] nonBindingParameterTypeArray = AnnotationHelper.GetNonBindingParameterTypeArray(context, parameters, false);
				if (array.SequenceEqual(nonBindingParameterTypeArray))
				{
					return edmOperationImport;
				}
			}
			return null;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		private static IEdmVocabularyAnnotation GetOrInsertCachedMetadataAnnotationForType(DataServiceContext context, Type type, string term, string qualifier)
		{
			IEdmModel serviceModel = context.Format.ServiceModel;
			if (serviceModel == null)
			{
				return null;
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = AnnotationHelper.GetCachedMetadataAnnotation(context, type, term, qualifier);
			if (edmVocabularyAnnotation != null)
			{
				return edmVocabularyAnnotation;
			}
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = null;
			if (type.IsSubclassOf(typeof(DataServiceContext)))
			{
				edmVocabularyAnnotatable = serviceModel.EntityContainer;
			}
			else
			{
				string text = ((context.ResolveName == null) ? type.FullName : context.ResolveName(type));
				if (!string.IsNullOrWhiteSpace(text))
				{
					edmVocabularyAnnotatable = serviceModel.FindDeclaredType(text);
					if (edmVocabularyAnnotatable == null)
					{
						return null;
					}
				}
			}
			IEnumerable<IEdmVocabularyAnnotation> enumerable = from a in serviceModel.FindVocabularyAnnotations(edmVocabularyAnnotatable, term, qualifier)
				where a.Qualifier == qualifier && a.Target == edmVocabularyAnnotatable
				select a;
			if (enumerable.Count<IEdmVocabularyAnnotation>() == 0)
			{
				edmVocabularyAnnotation = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForType(context, type.GetBaseType(), term, qualifier);
			}
			else if (enumerable.Count<IEdmVocabularyAnnotation>() == 1)
			{
				edmVocabularyAnnotation = enumerable.Single<IEdmVocabularyAnnotation>();
			}
			AnnotationHelper.InsertMetadataAnnotation(context, type, edmVocabularyAnnotation);
			return edmVocabularyAnnotation;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		private static IEdmVocabularyAnnotation GetOrInsertCachedMetadataAnnotationForPropertyInfo(DataServiceContext context, PropertyInfo propertyInfo, string term, string qualifier)
		{
			IEdmModel serviceModel = context.Format.ServiceModel;
			if (serviceModel == null)
			{
				return null;
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = AnnotationHelper.GetCachedMetadataAnnotation(context, propertyInfo, term, qualifier);
			if (edmVocabularyAnnotation != null)
			{
				return edmVocabularyAnnotation;
			}
			string severSidePropertyName = ClientTypeUtil.GetServerDefinedName(propertyInfo);
			if (string.IsNullOrEmpty(severSidePropertyName))
			{
				return null;
			}
			Type declaringType = propertyInfo.DeclaringType;
			IEnumerable<IEdmVocabularyAnnotation> enumerable = null;
			if (declaringType.IsSubclassOf(typeof(DataServiceContext)))
			{
				IEdmEntityContainer entityContainer = serviceModel.EntityContainer;
				IEnumerable<IEdmEntityContainerElement> enumerable2 = entityContainer.Elements.Where((IEdmEntityContainerElement e) => e.Name == severSidePropertyName);
				if (enumerable2 != null && enumerable2.Count<IEdmEntityContainerElement>() == 1)
				{
					enumerable = from a in serviceModel.FindVocabularyAnnotations(enumerable2.Single<IEdmEntityContainerElement>(), term, qualifier)
						where a.Qualifier == qualifier
						select a;
				}
			}
			else
			{
				string text = ((context.ResolveName == null) ? declaringType.FullName : context.ResolveName(declaringType));
				IEdmSchemaType edmSchemaType = serviceModel.FindDeclaredType(text);
				if (edmSchemaType != null)
				{
					IEdmStructuredType edmStructuredType = edmSchemaType as IEdmStructuredType;
					if (edmStructuredType != null)
					{
						IEdmProperty edmProperty = edmStructuredType.FindProperty(severSidePropertyName);
						if (edmProperty != null)
						{
							enumerable = from a in serviceModel.FindVocabularyAnnotations(edmProperty, term, qualifier)
								where a.Qualifier == qualifier
								select a;
						}
					}
				}
			}
			if (enumerable != null && enumerable.Count<IEdmVocabularyAnnotation>() == 1)
			{
				edmVocabularyAnnotation = enumerable.Single<IEdmVocabularyAnnotation>();
				AnnotationHelper.InsertMetadataAnnotation(context, propertyInfo, edmVocabularyAnnotation);
				return edmVocabularyAnnotation;
			}
			return null;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002C348 File Offset: 0x0002A548
		private static IEdmVocabularyAnnotation GetOrInsertCachedMetadataAnnotationForMethodInfo(DataServiceContext context, MethodInfo methodInfo, string term, string qualifier)
		{
			IEdmModel serviceModel = context.Format.ServiceModel;
			if (serviceModel == null)
			{
				return null;
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = AnnotationHelper.GetCachedMetadataAnnotation(context, methodInfo, term, qualifier);
			if (edmVocabularyAnnotation != null)
			{
				return edmVocabularyAnnotation;
			}
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = context.GetEdmOperationOrOperationImport(methodInfo);
			if (edmVocabularyAnnotatable == null)
			{
				return null;
			}
			IEdmOperationImport edmOperationImport = edmVocabularyAnnotatable as IEdmOperationImport;
			IEnumerable<IEdmVocabularyAnnotation> enumerable = null;
			if (edmOperationImport != null)
			{
				enumerable = from a in serviceModel.FindVocabularyAnnotations(edmOperationImport, term, qualifier)
					where a.Qualifier == qualifier
					select a;
				if (!enumerable.Any<IEdmVocabularyAnnotation>())
				{
					edmVocabularyAnnotatable = edmOperationImport.Operation;
				}
			}
			if (enumerable == null || !enumerable.Any<IEdmVocabularyAnnotation>())
			{
				enumerable = from a in serviceModel.FindVocabularyAnnotations(edmVocabularyAnnotatable, term, qualifier)
					where a.Qualifier == qualifier
					select a;
			}
			if (enumerable != null && enumerable.Count<IEdmVocabularyAnnotation>() == 1)
			{
				edmVocabularyAnnotation = enumerable.Single<IEdmVocabularyAnnotation>();
				AnnotationHelper.InsertMetadataAnnotation(context, methodInfo, edmVocabularyAnnotation);
				return edmVocabularyAnnotation;
			}
			return null;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002C428 File Offset: 0x0002A628
		private static IEdmVocabularyAnnotation GetCachedMetadataAnnotation(DataServiceContext context, object key, string term, string qualifier = null)
		{
			if (key != null && context.MetadataAnnotationsDictionary.ContainsKey(key))
			{
				IEnumerable<IEdmVocabularyAnnotation> enumerable = context.MetadataAnnotationsDictionary[key].Where((IEdmVocabularyAnnotation a) => a.Term.FullName().Equals(term) && a.Qualifier == qualifier);
				if (enumerable.Count<IEdmVocabularyAnnotation>() == 1)
				{
					return enumerable.Single<IEdmVocabularyAnnotation>();
				}
			}
			return null;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002C48C File Offset: 0x0002A68C
		private static void InsertMetadataAnnotation(DataServiceContext context, object key, IEdmVocabularyAnnotation edmValueAnnotation)
		{
			if (edmValueAnnotation != null)
			{
				IList<IEdmVocabularyAnnotation> list;
				if (!context.MetadataAnnotationsDictionary.TryGetValue(key, out list))
				{
					list = new List<IEdmVocabularyAnnotation>();
					context.MetadataAnnotationsDictionary.Add(key, list);
				}
				list.Add(edmValueAnnotation);
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002C4C8 File Offset: 0x0002A6C8
		private static bool TryEvaluateMetadataAnnotation<TResult>(DataServiceContext context, IEdmVocabularyAnnotation edmValueAnnotation, ClientEdmStructuredValue clientEdmValue, out TResult annotationValue)
		{
			if (edmValueAnnotation == null)
			{
				annotationValue = default(TResult);
				return false;
			}
			EdmToClrEvaluator edmToClrEvaluator = AnnotationHelper.CreateEdmToClrEvaluator(context);
			try
			{
				annotationValue = edmToClrEvaluator.EvaluateToClrValue<TResult>(edmValueAnnotation.Value, clientEdmValue);
			}
			catch (InvalidOperationException)
			{
				annotationValue = default(TResult);
				return false;
			}
			return true;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002C51C File Offset: 0x0002A71C
		private static EdmToClrEvaluator CreateEdmToClrEvaluator(DataServiceContext context)
		{
			AnnotationHelper.AnnotationMaterializeHelper annotationMaterializeHelper = new AnnotationHelper.AnnotationMaterializeHelper(context);
			return new EdmToClrEvaluator(null, null, new Func<IEdmModel, IEdmType, string, string, IEdmExpression>(annotationMaterializeHelper.GetAnnnotationExpressionForType), new Func<IEdmModel, IEdmType, string, string, string, IEdmExpression>(annotationMaterializeHelper.GetAnnnotationExpressionForProperty), context.Model)
			{
				EdmToClrConverter = new EdmToClrConverter(new TryCreateObjectInstance(annotationMaterializeHelper.TryCreateObjectInstance), new TryGetClrPropertyInfo(annotationMaterializeHelper.TryGetClientPropertyInfo), new TryGetClrTypeName(annotationMaterializeHelper.TryGetClrTypeName))
			};
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002C588 File Offset: 0x0002A788
		private static Type[] GetNonBindingParameterTypeArray(DataServiceContext context, IEnumerable<IEdmOperationParameter> parameters, bool isBound = false)
		{
			List<Type> list = new List<Type>();
			for (int i = (isBound ? 1 : 0); i < parameters.Count<IEdmOperationParameter>(); i++)
			{
				Type type;
				if (AnnotationHelper.TryGetClrTypeFromEdmTypeReference(context, parameters.ElementAt(i).Type, false, out type))
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002C5D8 File Offset: 0x0002A7D8
		private static bool TryGetClrTypeFromEdmTypeReference(DataServiceContext context, IEdmTypeReference edmTypeReference, bool isBindingParameter, out Type clrType)
		{
			EdmTypeKind typeKind = edmTypeReference.Definition.TypeKind;
			if (typeKind == EdmTypeKind.None)
			{
				clrType = null;
				return false;
			}
			if (typeKind == EdmTypeKind.Primitive)
			{
				PrimitiveType primitiveType = null;
				if (PrimitiveType.TryGetPrimitiveType(edmTypeReference.Definition.FullName(), out primitiveType))
				{
					clrType = primitiveType.ClrType;
					if (edmTypeReference.IsNullable && ClientTypeUtil.CanAssignNull(clrType))
					{
						clrType = typeof(Nullable<>).MakeGenericType(new Type[] { clrType });
					}
					return true;
				}
			}
			Type type;
			if (typeKind == EdmTypeKind.Collection && AnnotationHelper.TryGetClrTypeFromEdmTypeReference(context, ((IEdmCollectionTypeReference)edmTypeReference).ElementType(), false, out type))
			{
				if (isBindingParameter)
				{
					clrType = typeof(DataServiceQuery<>).MakeGenericType(new Type[] { type });
				}
				else
				{
					clrType = typeof(List<>).MakeGenericType(new Type[] { type });
				}
				return true;
			}
			if (typeKind != EdmTypeKind.Complex && typeKind != EdmTypeKind.Entity && typeKind != EdmTypeKind.Enum)
			{
				clrType = null;
				return false;
			}
			clrType = AnnotationHelper.ResolveTypeFromName(context, edmTypeReference.FullName());
			if (clrType == null)
			{
				return false;
			}
			if (isBindingParameter)
			{
				clrType = typeof(DataServiceQuerySingle<>).MakeGenericType(new Type[] { clrType });
			}
			return true;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002C6EC File Offset: 0x0002A8EC
		private static Type ResolveTypeFromName(DataServiceContext context, string qualifiedTypeName)
		{
			Type type = context.ResolveTypeFromName(qualifiedTypeName);
			if (type == null)
			{
				int num = qualifiedTypeName.LastIndexOf('.');
				if (num > 0)
				{
					string text = qualifiedTypeName.Substring(0, num);
					type = context.DefaultResolveType(qualifiedTypeName, text, text);
				}
			}
			return type;
		}

		// Token: 0x020001DF RID: 479
		private class AnnotationMaterializeHelper
		{
			// Token: 0x06000F6B RID: 3947 RVA: 0x00032C07 File Offset: 0x00030E07
			internal AnnotationMaterializeHelper(DataServiceContext context)
			{
				this.dataServiceContext = context;
			}

			// Token: 0x06000F6C RID: 3948 RVA: 0x00032C18 File Offset: 0x00030E18
			internal bool TryGetClrTypeName(IEdmModel edmModel, string qualifiedEdmTypeName, out string typeNameInClientModel)
			{
				Type type = AnnotationHelper.ResolveTypeFromName(this.dataServiceContext, qualifiedEdmTypeName);
				typeNameInClientModel = ((type == null) ? null : type.FullName);
				return typeNameInClientModel != null;
			}

			// Token: 0x06000F6D RID: 3949 RVA: 0x00032C4B File Offset: 0x00030E4B
			internal bool TryGetClientPropertyInfo(Type type, string propertyName, out PropertyInfo propertyInfo)
			{
				propertyInfo = ClientTypeUtil.GetClientPropertyInfo(type, propertyName, this.dataServiceContext.UndeclaredPropertyBehavior);
				return propertyInfo != null;
			}

			// Token: 0x06000F6E RID: 3950 RVA: 0x00032C69 File Offset: 0x00030E69
			internal bool TryCreateObjectInstance(IEdmStructuredValue edmValue, Type clrType, EdmToClrConverter converter, out object objectInstance, out bool objectInstanceInitialized)
			{
				return AnnotationHelper.AnnotationMaterializeHelper.TryCreateClientObjectInstance(this.dataServiceContext, edmValue, clrType, out objectInstance, out objectInstanceInitialized);
			}

			// Token: 0x06000F6F RID: 3951 RVA: 0x00032C7C File Offset: 0x00030E7C
			internal IEdmExpression GetAnnnotationExpressionForType(IEdmModel edmModel, IEdmType edmType, string termName, string qualifier)
			{
				if (termName != null)
				{
					ClientTypeAnnotation clientTypeAnnotation = edmModel.GetClientTypeAnnotation(edmType);
					if (clientTypeAnnotation != null)
					{
						IEdmVocabularyAnnotation orInsertCachedMetadataAnnotationForType = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForType(this.dataServiceContext, clientTypeAnnotation.ElementType, termName, qualifier);
						if (orInsertCachedMetadataAnnotationForType != null)
						{
							return orInsertCachedMetadataAnnotationForType.Value;
						}
					}
				}
				return null;
			}

			// Token: 0x06000F70 RID: 3952 RVA: 0x00032CB8 File Offset: 0x00030EB8
			internal IEdmExpression GetAnnnotationExpressionForProperty(IEdmModel edmModel, IEdmType edmType, string propertyName, string termName, string qualifier)
			{
				if (termName != null)
				{
					ClientTypeAnnotation clientTypeAnnotation = edmModel.GetClientTypeAnnotation(edmType);
					if (clientTypeAnnotation != null)
					{
						PropertyInfo clientPropertyInfo = ClientTypeUtil.GetClientPropertyInfo(clientTypeAnnotation.ElementType, propertyName, this.dataServiceContext.UndeclaredPropertyBehavior);
						if (clientPropertyInfo != null)
						{
							IEdmVocabularyAnnotation orInsertCachedMetadataAnnotationForPropertyInfo = AnnotationHelper.GetOrInsertCachedMetadataAnnotationForPropertyInfo(this.dataServiceContext, clientPropertyInfo, termName, qualifier);
							if (orInsertCachedMetadataAnnotationForPropertyInfo != null)
							{
								return orInsertCachedMetadataAnnotationForPropertyInfo.Value;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06000F71 RID: 3953 RVA: 0x00032D14 File Offset: 0x00030F14
			private static bool TryCreateClientObjectInstance(DataServiceContext context, IEdmStructuredValue edmValue, Type clrType, out object objectInstance, out bool objectInstanceInitialized)
			{
				ClientEdmStructuredValue clientEdmStructuredValue = edmValue as ClientEdmStructuredValue;
				Type type = clrType;
				if (clientEdmStructuredValue != null)
				{
					ClientTypeAnnotation clientTypeAnnotation = context.Model.GetClientTypeAnnotation(edmValue.Type.Definition.FullName());
					type = clientTypeAnnotation.ElementType;
				}
				if (type.IsSubclassOf(clrType))
				{
					objectInstance = Activator.CreateInstance(type);
				}
				else
				{
					objectInstance = Activator.CreateInstance(clrType);
				}
				objectInstanceInitialized = false;
				return true;
			}

			// Token: 0x04000844 RID: 2116
			private DataServiceContext dataServiceContext;
		}
	}
}
