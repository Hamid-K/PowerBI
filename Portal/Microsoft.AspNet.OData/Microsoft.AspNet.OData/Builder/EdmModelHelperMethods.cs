using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000144 RID: 324
	internal static class EdmModelHelperMethods
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0002E03C File Offset: 0x0002C23C
		public static IEdmModel BuildEdmModel(ODataModelBuilder builder)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			EdmModel edmModel = new EdmModel();
			EdmEntityContainer edmEntityContainer = new EdmEntityContainer(builder.Namespace, builder.ContainerName);
			EdmTypeMap typesAndProperties = EdmTypeBuilder.GetTypesAndProperties(builder.StructuralTypes.Concat(builder.EnumTypes));
			Dictionary<Type, IEdmType> dictionary = edmModel.AddTypes(typesAndProperties);
			IEnumerable<NavigationSourceAndAnnotations> enumerable = edmEntityContainer.AddEntitySetAndAnnotations(builder, dictionary);
			NavigationSourceAndAnnotations[] array = edmEntityContainer.AddSingletonAndAnnotations(builder, dictionary);
			IEnumerable<NavigationSourceAndAnnotations> enumerable2 = enumerable.Concat(array);
			IDictionary<string, EdmNavigationSource> navigationSourceMap = edmModel.GetNavigationSourceMap(typesAndProperties, enumerable2);
			edmModel.AddCoreVocabularyAnnotations(enumerable2, typesAndProperties);
			edmModel.AddCapabilitiesVocabularyAnnotations(enumerable2, typesAndProperties);
			edmModel.AddOperations(builder.Operations, edmEntityContainer, dictionary, navigationSourceMap);
			edmModel.AddElement(edmEntityContainer);
			edmModel.SetAnnotationValue(edmModel, new BindableOperationFinder(edmModel));
			return edmModel;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002E0EC File Offset: 0x0002C2EC
		private static void AddTypes(this EdmModel model, Dictionary<Type, IEdmType> types)
		{
			foreach (IEdmType edmType in types.Values)
			{
				model.AddType(edmType);
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002E140 File Offset: 0x0002C340
		private static NavigationSourceAndAnnotations[] AddEntitySetAndAnnotations(this EdmEntityContainer container, ODataModelBuilder builder, Dictionary<Type, IEdmType> edmTypeMap)
		{
			return (from e in EdmModelHelperMethods.AddEntitySets(builder.EntitySets, container, edmTypeMap)
				select new NavigationSourceAndAnnotations
				{
					NavigationSource = e.Item1,
					Configuration = e.Item2,
					LinkBuilder = new NavigationSourceLinkBuilderAnnotation(e.Item2),
					Url = new NavigationSourceUrlAnnotation
					{
						Url = e.Item2.GetUrl()
					}
				}).ToArray<NavigationSourceAndAnnotations>();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002E178 File Offset: 0x0002C378
		private static NavigationSourceAndAnnotations[] AddSingletonAndAnnotations(this EdmEntityContainer container, ODataModelBuilder builder, Dictionary<Type, IEdmType> edmTypeMap)
		{
			return (from e in EdmModelHelperMethods.AddSingletons(builder.Singletons, container, edmTypeMap)
				select new NavigationSourceAndAnnotations
				{
					NavigationSource = e.Item1,
					Configuration = e.Item2,
					LinkBuilder = new NavigationSourceLinkBuilderAnnotation(e.Item2),
					Url = new NavigationSourceUrlAnnotation
					{
						Url = e.Item2.GetUrl()
					}
				}).ToArray<NavigationSourceAndAnnotations>();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		private static IDictionary<string, EdmNavigationSource> GetNavigationSourceMap(this EdmModel model, EdmTypeMap edmMap, IEnumerable<NavigationSourceAndAnnotations> navigationSourceAndAnnotations)
		{
			Dictionary<string, EdmNavigationSource> dictionary = navigationSourceAndAnnotations.ToDictionary((NavigationSourceAndAnnotations e) => e.NavigationSource.Name, (NavigationSourceAndAnnotations e) => e.NavigationSource);
			foreach (NavigationSourceAndAnnotations navigationSourceAndAnnotations2 in navigationSourceAndAnnotations)
			{
				EdmNavigationSource navigationSource = navigationSourceAndAnnotations2.NavigationSource;
				model.SetAnnotationValue(navigationSource, navigationSourceAndAnnotations2.Url);
				model.SetNavigationSourceLinkBuilder(navigationSource, navigationSourceAndAnnotations2.LinkBuilder);
				EdmModelHelperMethods.AddNavigationBindings(edmMap, navigationSourceAndAnnotations2.Configuration, navigationSource, navigationSourceAndAnnotations2.LinkBuilder, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002E26C File Offset: 0x0002C46C
		private static void AddNavigationBindings(EdmTypeMap edmMap, NavigationSourceConfiguration navigationSourceConfiguration, EdmNavigationSource navigationSource, NavigationSourceLinkBuilderAnnotation linkBuilder, Dictionary<string, EdmNavigationSource> edmNavigationSourceMap)
		{
			foreach (NavigationPropertyBindingConfiguration navigationPropertyBindingConfiguration in navigationSourceConfiguration.Bindings)
			{
				NavigationPropertyConfiguration navigationProperty = navigationPropertyBindingConfiguration.NavigationProperty;
				bool containsTarget = navigationProperty.ContainsTarget;
				IEdmNavigationProperty edmNavigationProperty = (edmMap.EdmTypes[navigationProperty.DeclaringType.ClrType] as IEdmStructuredType).NavigationProperties().Single((IEdmNavigationProperty np) => np.Name == navigationProperty.Name);
				string text = EdmModelHelperMethods.ConvertBindingPath(edmMap, navigationPropertyBindingConfiguration);
				if (!containsTarget)
				{
					navigationSource.AddNavigationTarget(edmNavigationProperty, edmNavigationSourceMap[navigationPropertyBindingConfiguration.TargetNavigationSource.Name], new EdmPathExpression(text));
				}
				NavigationLinkBuilder navigationPropertyLink = navigationSourceConfiguration.GetNavigationPropertyLink(navigationProperty);
				if (navigationPropertyLink != null)
				{
					linkBuilder.AddNavigationPropertyLinkBuilder(edmNavigationProperty, navigationPropertyLink);
				}
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002E354 File Offset: 0x0002C554
		private static string ConvertBindingPath(EdmTypeMap edmMap, NavigationPropertyBindingConfiguration binding)
		{
			IList<string> list = new List<string>();
			foreach (MemberInfo memberInfo in binding.Path)
			{
				Type type = TypeHelper.AsType(memberInfo);
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				if (type != null)
				{
					IEdmType edmType = edmMap.EdmTypes[type];
					list.Add(edmType.FullTypeName());
				}
				else if (propertyInfo != null)
				{
					list.Add(edmMap.EdmProperties[propertyInfo].Name);
				}
			}
			return string.Join("/", list);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002E400 File Offset: 0x0002C600
		private static void AddOperationParameters(EdmOperation operation, OperationConfiguration operationConfiguration, Dictionary<Type, IEdmType> edmTypeMap)
		{
			foreach (ParameterConfiguration parameterConfiguration in operationConfiguration.Parameters)
			{
				bool nullable = parameterConfiguration.Nullable;
				IEdmTypeReference edmTypeReference = EdmModelHelperMethods.GetEdmTypeReference(edmTypeMap, parameterConfiguration.TypeConfiguration, nullable);
				if (parameterConfiguration.IsOptional)
				{
					if (parameterConfiguration.DefaultValue != null)
					{
						operation.AddOptionalParameter(parameterConfiguration.Name, edmTypeReference, parameterConfiguration.DefaultValue);
					}
					else
					{
						operation.AddOptionalParameter(parameterConfiguration.Name, edmTypeReference);
					}
				}
				else
				{
					IEdmOperationParameter edmOperationParameter = new EdmOperationParameter(operation, parameterConfiguration.Name, edmTypeReference);
					operation.AddParameter(edmOperationParameter);
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		private static void AddOperationLinkBuilder(IEdmModel model, IEdmOperation operation, OperationConfiguration operationConfiguration)
		{
			ActionConfiguration actionConfiguration = operationConfiguration as ActionConfiguration;
			IEdmAction edmAction = operation as IEdmAction;
			FunctionConfiguration functionConfiguration = operationConfiguration as FunctionConfiguration;
			IEdmFunction edmFunction = operation as IEdmFunction;
			if (operationConfiguration.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Entity)
			{
				if (actionConfiguration != null && actionConfiguration.GetActionLink() != null && edmAction != null)
				{
					model.SetOperationLinkBuilder(edmAction, new OperationLinkBuilder(actionConfiguration.GetActionLink(), actionConfiguration.FollowsConventions));
					return;
				}
				if (functionConfiguration != null && functionConfiguration.GetFunctionLink() != null && edmFunction != null)
				{
					model.SetOperationLinkBuilder(edmFunction, new OperationLinkBuilder(functionConfiguration.GetFunctionLink(), functionConfiguration.FollowsConventions));
					return;
				}
			}
			else if (operationConfiguration.BindingParameter.TypeConfiguration.Kind == EdmTypeKind.Collection && ((CollectionTypeConfiguration)operationConfiguration.BindingParameter.TypeConfiguration).ElementType.Kind == EdmTypeKind.Entity)
			{
				if (actionConfiguration != null && actionConfiguration.GetFeedActionLink() != null && edmAction != null)
				{
					model.SetOperationLinkBuilder(edmAction, new OperationLinkBuilder(actionConfiguration.GetFeedActionLink(), actionConfiguration.FollowsConventions));
					return;
				}
				if (functionConfiguration != null && functionConfiguration.GetFeedFunctionLink() != null && edmFunction != null)
				{
					model.SetOperationLinkBuilder(edmFunction, new OperationLinkBuilder(functionConfiguration.GetFeedFunctionLink(), functionConfiguration.FollowsConventions));
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		private static void ValidateOperationEntitySetPath(IEdmModel model, IEdmOperationImport operationImport, OperationConfiguration operationConfiguration)
		{
			IEdmOperationParameter edmOperationParameter;
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary;
			IEnumerable<EdmError> enumerable;
			if (operationConfiguration.EntitySetPath != null && !operationImport.TryGetRelativeEntitySetPath(model, out edmOperationParameter, out dictionary, out enumerable))
			{
				throw Error.InvalidOperation(SRResources.OperationHasInvalidEntitySetPath, new object[]
				{
					string.Join("/", operationConfiguration.EntitySetPath),
					operationConfiguration.FullyQualifiedName
				});
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002E60C File Offset: 0x0002C80C
		private static void AddOperations(this EdmModel model, IEnumerable<OperationConfiguration> configurations, EdmEntityContainer container, Dictionary<Type, IEdmType> edmTypeMap, IDictionary<string, EdmNavigationSource> edmNavigationSourceMap)
		{
			EdmModelHelperMethods.ValidateActionOverload(configurations.OfType<ActionConfiguration>());
			foreach (OperationConfiguration operationConfiguration in configurations)
			{
				IEdmTypeReference edmTypeReference = EdmModelHelperMethods.GetEdmTypeReference(edmTypeMap, operationConfiguration.ReturnType, operationConfiguration.ReturnType != null && operationConfiguration.ReturnNullable);
				IEdmExpression edmEntitySetExpression = EdmModelHelperMethods.GetEdmEntitySetExpression(edmNavigationSourceMap, operationConfiguration);
				IEdmPathExpression edmPathExpression = ((operationConfiguration.EntitySetPath != null) ? new EdmPathExpression(operationConfiguration.EntitySetPath) : null);
				EdmOperationImport edmOperationImport;
				switch (operationConfiguration.Kind)
				{
				case OperationKind.Action:
					edmOperationImport = EdmModelHelperMethods.CreateActionImport(operationConfiguration, container, edmTypeReference, edmEntitySetExpression, edmPathExpression);
					break;
				case OperationKind.Function:
					edmOperationImport = EdmModelHelperMethods.CreateFunctionImport((FunctionConfiguration)operationConfiguration, container, edmTypeReference, edmEntitySetExpression, edmPathExpression);
					break;
				case OperationKind.ServiceOperation:
					return;
				default:
					return;
				}
				EdmOperation edmOperation = (EdmOperation)edmOperationImport.Operation;
				if (operationConfiguration.IsBindable && operationConfiguration.Title != null && operationConfiguration.Title != operationConfiguration.Name)
				{
					model.SetOperationTitleAnnotation(edmOperation, new OperationTitleAnnotation(operationConfiguration.Title));
				}
				if (operationConfiguration.IsBindable && operationConfiguration.NavigationSource != null && edmNavigationSourceMap.ContainsKey(operationConfiguration.NavigationSource.Name))
				{
					model.SetAnnotationValue(edmOperation, new ReturnedEntitySetAnnotation(operationConfiguration.NavigationSource.Name));
				}
				EdmModelHelperMethods.AddOperationParameters(edmOperation, operationConfiguration, edmTypeMap);
				if (operationConfiguration.IsBindable)
				{
					EdmModelHelperMethods.AddOperationLinkBuilder(model, edmOperation, operationConfiguration);
					EdmModelHelperMethods.ValidateOperationEntitySetPath(model, edmOperationImport, operationConfiguration);
				}
				else
				{
					container.AddElement(edmOperationImport);
				}
				model.AddElement(edmOperation);
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002E7A8 File Offset: 0x0002C9A8
		private static EdmOperationImport CreateActionImport(OperationConfiguration operationConfiguration, EdmEntityContainer container, IEdmTypeReference returnReference, IEdmExpression expression, IEdmPathExpression pathExpression)
		{
			EdmAction edmAction = new EdmAction(operationConfiguration.Namespace, operationConfiguration.Name, returnReference, operationConfiguration.IsBindable, pathExpression);
			return new EdmActionImport(container, operationConfiguration.Name, edmAction, expression);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002E7E0 File Offset: 0x0002C9E0
		private static EdmOperationImport CreateFunctionImport(FunctionConfiguration function, EdmEntityContainer container, IEdmTypeReference returnReference, IEdmExpression expression, IEdmPathExpression pathExpression)
		{
			EdmFunction edmFunction = new EdmFunction(function.Namespace, function.Name, returnReference, function.IsBindable, pathExpression, function.IsComposable);
			return new EdmFunctionImport(container, function.Name, edmFunction, expression, function.IncludeInServiceDocument);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002E824 File Offset: 0x0002CA24
		private static void ValidateActionOverload(IEnumerable<ActionConfiguration> configurations)
		{
			ActionConfiguration[] array = configurations.Where((ActionConfiguration a) => !a.IsBindable).ToArray<ActionConfiguration>();
			if (array.Length != 0)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (ActionConfiguration actionConfiguration in array)
				{
					if (hashSet.Contains(actionConfiguration.Name))
					{
						throw Error.InvalidOperation(SRResources.MoreThanOneUnboundActionFound, new object[] { actionConfiguration.Name });
					}
					hashSet.Add(actionConfiguration.Name);
				}
			}
			ActionConfiguration[] array3 = configurations.Where((ActionConfiguration a) => a.IsBindable).ToArray<ActionConfiguration>();
			if (array3.Length != 0)
			{
				Dictionary<string, IList<IEdmTypeConfiguration>> dictionary = new Dictionary<string, IList<IEdmTypeConfiguration>>();
				foreach (ActionConfiguration actionConfiguration2 in array3)
				{
					IEdmTypeConfiguration typeConfiguration = actionConfiguration2.BindingParameter.TypeConfiguration;
					if (dictionary.ContainsKey(actionConfiguration2.Name))
					{
						IList<IEdmTypeConfiguration> list = dictionary[actionConfiguration2.Name];
						foreach (IEdmTypeConfiguration edmTypeConfiguration in list)
						{
							if (edmTypeConfiguration == typeConfiguration)
							{
								throw Error.InvalidOperation(SRResources.MoreThanOneOverloadActionBoundToSameTypeFound, new object[] { actionConfiguration2.Name, edmTypeConfiguration.FullName });
							}
						}
						list.Add(typeConfiguration);
					}
					else
					{
						IList<IEdmTypeConfiguration> list2 = new List<IEdmTypeConfiguration>();
						list2.Add(typeConfiguration);
						dictionary.Add(actionConfiguration2.Name, list2);
					}
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		private static Dictionary<Type, IEdmType> AddTypes(this EdmModel model, EdmTypeMap edmTypeMap)
		{
			Dictionary<Type, IEdmType> edmTypes = edmTypeMap.EdmTypes;
			model.AddTypes(edmTypes);
			model.AddClrTypeAnnotations(edmTypes);
			Dictionary<PropertyInfo, IEdmProperty> edmProperties = edmTypeMap.EdmProperties;
			model.AddClrPropertyInfoAnnotations(edmProperties);
			model.AddClrEnumMemberInfoAnnotations(edmTypeMap);
			model.AddPropertyRestrictionsAnnotations(edmTypeMap.EdmPropertiesRestrictions);
			model.AddPropertiesQuerySettings(edmTypeMap.EdmPropertiesQuerySettings);
			model.AddStructuredTypeQuerySettings(edmTypeMap.EdmStructuredTypeQuerySettings);
			model.AddDynamicPropertyDictionaryAnnotations(edmTypeMap.OpenTypes);
			return edmTypes;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002EA3C File Offset: 0x0002CC3C
		private static void AddType(this EdmModel model, IEdmType type)
		{
			if (type.TypeKind == EdmTypeKind.Complex)
			{
				model.AddElement(type as IEdmComplexType);
				return;
			}
			if (type.TypeKind == EdmTypeKind.Entity)
			{
				model.AddElement(type as IEdmEntityType);
				return;
			}
			if (type.TypeKind == EdmTypeKind.Enum)
			{
				model.AddElement(type as IEdmEnumType);
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002EA8A File Offset: 0x0002CC8A
		private static EdmEntitySet AddEntitySet(this EdmEntityContainer container, EntitySetConfiguration entitySet, IDictionary<Type, IEdmType> edmTypeMap)
		{
			return container.AddEntitySet(entitySet.Name, (IEdmEntityType)edmTypeMap[entitySet.EntityType.ClrType]);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		private static IEnumerable<Tuple<EdmEntitySet, EntitySetConfiguration>> AddEntitySets(IEnumerable<EntitySetConfiguration> entitySets, EdmEntityContainer container, Dictionary<Type, IEdmType> edmTypeMap)
		{
			return entitySets.Select((EntitySetConfiguration es) => Tuple.Create<EdmEntitySet, EntitySetConfiguration>(container.AddEntitySet(es, edmTypeMap), es));
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		private static EdmSingleton AddSingleton(this EdmEntityContainer container, SingletonConfiguration singletonType, IDictionary<Type, IEdmType> edmTypeMap)
		{
			return container.AddSingleton(singletonType.Name, (IEdmEntityType)edmTypeMap[singletonType.EntityType.ClrType]);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002EB08 File Offset: 0x0002CD08
		private static IEnumerable<Tuple<EdmSingleton, SingletonConfiguration>> AddSingletons(IEnumerable<SingletonConfiguration> singletons, EdmEntityContainer container, Dictionary<Type, IEdmType> edmTypeMap)
		{
			return singletons.Select((SingletonConfiguration sg) => Tuple.Create<EdmSingleton, SingletonConfiguration>(container.AddSingleton(sg, edmTypeMap), sg));
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		private static void AddClrTypeAnnotations(this EdmModel model, Dictionary<Type, IEdmType> edmTypes)
		{
			foreach (KeyValuePair<Type, IEdmType> keyValuePair in edmTypes)
			{
				IEdmType value = keyValuePair.Value;
				Type key = keyValuePair.Key;
				model.SetAnnotationValue(value, new ClrTypeAnnotation(key));
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		private static void AddClrPropertyInfoAnnotations(this EdmModel model, Dictionary<PropertyInfo, IEdmProperty> edmProperties)
		{
			foreach (KeyValuePair<PropertyInfo, IEdmProperty> keyValuePair in edmProperties)
			{
				IEdmProperty value = keyValuePair.Value;
				PropertyInfo key = keyValuePair.Key;
				if (value.Name != key.Name)
				{
					model.SetAnnotationValue(value, new ClrPropertyInfoAnnotation(key));
				}
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002EC18 File Offset: 0x0002CE18
		private static void AddClrEnumMemberInfoAnnotations(this EdmModel model, EdmTypeMap edmTypeMap)
		{
			if (edmTypeMap.EnumMembers == null || !edmTypeMap.EnumMembers.Any<KeyValuePair<Enum, IEdmEnumMember>>())
			{
				return;
			}
			foreach (IGrouping<Type, KeyValuePair<Enum, IEdmEnumMember>> grouping in from e in edmTypeMap.EnumMembers
				group e by e.Key.GetType())
			{
				IEdmType edmType = edmTypeMap.EdmTypes[grouping.Key];
				model.SetAnnotationValue(edmType, new ClrEnumMemberAnnotation(grouping.ToDictionary((KeyValuePair<Enum, IEdmEnumMember> e) => e.Key, (KeyValuePair<Enum, IEdmEnumMember> e) => e.Value)));
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002ED1C File Offset: 0x0002CF1C
		private static void AddDynamicPropertyDictionaryAnnotations(this EdmModel model, Dictionary<IEdmStructuredType, PropertyInfo> openTypes)
		{
			foreach (KeyValuePair<IEdmStructuredType, PropertyInfo> keyValuePair in openTypes)
			{
				IEdmStructuredType key = keyValuePair.Key;
				PropertyInfo value = keyValuePair.Value;
				model.SetAnnotationValue(key, new DynamicPropertyDictionaryAnnotation(value));
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002ED80 File Offset: 0x0002CF80
		private static void AddPropertiesQuerySettings(this EdmModel model, Dictionary<IEdmProperty, ModelBoundQuerySettings> edmPropertiesQuerySettings)
		{
			foreach (KeyValuePair<IEdmProperty, ModelBoundQuerySettings> keyValuePair in edmPropertiesQuerySettings)
			{
				IEdmProperty key = keyValuePair.Key;
				ModelBoundQuerySettings value = keyValuePair.Value;
				model.SetAnnotationValue(key, value);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
		private static void AddStructuredTypeQuerySettings(this EdmModel model, Dictionary<IEdmStructuredType, ModelBoundQuerySettings> edmStructuredTypeQuerySettings)
		{
			foreach (KeyValuePair<IEdmStructuredType, ModelBoundQuerySettings> keyValuePair in edmStructuredTypeQuerySettings)
			{
				IEdmStructuredType key = keyValuePair.Key;
				ModelBoundQuerySettings value = keyValuePair.Value;
				model.SetAnnotationValue(key, value);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002EE40 File Offset: 0x0002D040
		private static void AddPropertyRestrictionsAnnotations(this EdmModel model, Dictionary<IEdmProperty, QueryableRestrictions> edmPropertiesRestrictions)
		{
			foreach (KeyValuePair<IEdmProperty, QueryableRestrictions> keyValuePair in edmPropertiesRestrictions)
			{
				IEdmProperty key = keyValuePair.Key;
				QueryableRestrictions value = keyValuePair.Value;
				model.SetAnnotationValue(key, new QueryableRestrictionsAnnotation(value));
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
		private static void AddCoreVocabularyAnnotations(this EdmModel model, IEnumerable<NavigationSourceAndAnnotations> navigationSources, EdmTypeMap edmTypeMap)
		{
			if (navigationSources == null)
			{
				return;
			}
			foreach (NavigationSourceAndAnnotations navigationSourceAndAnnotations in navigationSources)
			{
				IEdmVocabularyAnnotatable edmVocabularyAnnotatable = navigationSourceAndAnnotations.NavigationSource as IEdmVocabularyAnnotatable;
				if (edmVocabularyAnnotatable != null)
				{
					NavigationSourceConfiguration configuration = navigationSourceAndAnnotations.Configuration;
					if (configuration != null)
					{
						model.AddOptimisticConcurrencyAnnotation(edmVocabularyAnnotatable, configuration, edmTypeMap);
					}
				}
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002EF0C File Offset: 0x0002D10C
		private static void AddOptimisticConcurrencyAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, NavigationSourceConfiguration navigationSourceConfiguration, EdmTypeMap edmTypeMap)
		{
			EntityTypeConfiguration entityType = navigationSourceConfiguration.EntityType;
			IEnumerable<StructuralPropertyConfiguration> enumerable = from property in entityType.Properties.OfType<StructuralPropertyConfiguration>()
				where property.ConcurrencyToken
				select property;
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in entityType.BaseTypes())
			{
				enumerable = enumerable.Concat(from property in structuralTypeConfiguration.Properties.OfType<StructuralPropertyConfiguration>()
					where property.ConcurrencyToken
					select property);
			}
			IList<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>();
			foreach (StructuralPropertyConfiguration structuralPropertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(structuralPropertyConfiguration.PropertyInfo, out edmProperty))
				{
					IEdmStructuralProperty edmStructuralProperty = edmProperty as IEdmStructuralProperty;
					if (edmStructuralProperty != null)
					{
						list.Add(edmStructuralProperty);
					}
				}
			}
			if (list.Any<IEdmStructuralProperty>())
			{
				IEdmExpression[] array = list.Select((IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
				IEdmCollectionExpression edmCollectionExpression = new EdmCollectionExpression(array);
				IEdmTerm concurrencyTerm = CoreVocabularyModel.ConcurrencyTerm;
				EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, concurrencyTerm, edmCollectionExpression);
				edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
				model.SetVocabularyAnnotation(edmVocabularyAnnotation);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002F08C File Offset: 0x0002D28C
		private static void AddCapabilitiesVocabularyAnnotations(this EdmModel model, IEnumerable<NavigationSourceAndAnnotations> navigationSources, EdmTypeMap edmTypeMap)
		{
			if (navigationSources == null)
			{
				return;
			}
			foreach (NavigationSourceAndAnnotations navigationSourceAndAnnotations in navigationSources)
			{
				IEdmEntitySet edmEntitySet = navigationSourceAndAnnotations.NavigationSource as IEdmEntitySet;
				if (edmEntitySet != null)
				{
					EntitySetConfiguration entitySetConfiguration = navigationSourceAndAnnotations.Configuration as EntitySetConfiguration;
					if (entitySetConfiguration != null)
					{
						model.AddCountRestrictionsAnnotation(edmEntitySet, entitySetConfiguration, edmTypeMap);
						model.AddNavigationRestrictionsAnnotation(edmEntitySet, entitySetConfiguration, edmTypeMap);
						model.AddFilterRestrictionsAnnotation(edmEntitySet, entitySetConfiguration, edmTypeMap);
						model.AddSortRestrictionsAnnotation(edmEntitySet, entitySetConfiguration, edmTypeMap);
						model.AddExpandRestrictionsAnnotation(edmEntitySet, entitySetConfiguration, edmTypeMap);
					}
				}
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002F11C File Offset: 0x0002D31C
		private static void AddCountRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, EntitySetConfiguration entitySetConfiguration, EdmTypeMap edmTypeMap)
		{
			IEnumerable<PropertyConfiguration> enumerable = entitySetConfiguration.EntityType.Properties.Where((PropertyConfiguration property) => property.NotCountable);
			IList<IEdmProperty> list = new List<IEdmProperty>();
			IList<IEdmNavigationProperty> list2 = new List<IEdmNavigationProperty>();
			foreach (PropertyConfiguration propertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(propertyConfiguration.PropertyInfo, out edmProperty) && edmProperty != null && edmProperty.Type.TypeKind() == EdmTypeKind.Collection)
				{
					if (edmProperty.PropertyKind == EdmPropertyKind.Navigation)
					{
						list2.Add((IEdmNavigationProperty)edmProperty);
					}
					else
					{
						list.Add(edmProperty);
					}
				}
			}
			if (list.Any<IEdmProperty>() || list2.Any<IEdmNavigationProperty>())
			{
				model.SetCountRestrictionsAnnotation(target, true, list, list2);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		private static void AddNavigationRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, EntitySetConfiguration entitySetConfiguration, EdmTypeMap edmTypeMap)
		{
			IEnumerable<PropertyConfiguration> enumerable = entitySetConfiguration.EntityType.Properties.Where((PropertyConfiguration property) => property.NotNavigable);
			IList<Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>> list = new List<Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>>();
			foreach (PropertyConfiguration propertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(propertyConfiguration.PropertyInfo, out edmProperty) && edmProperty != null && edmProperty.PropertyKind == EdmPropertyKind.Navigation)
				{
					list.Add(new Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>((IEdmNavigationProperty)edmProperty, CapabilitiesNavigationType.Recursive));
				}
			}
			if (list.Any<Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>>())
			{
				model.SetNavigationRestrictionsAnnotation(target, CapabilitiesNavigationType.Recursive, list);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		private static void AddFilterRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, EntitySetConfiguration entitySetConfiguration, EdmTypeMap edmTypeMap)
		{
			IEnumerable<PropertyConfiguration> enumerable = entitySetConfiguration.EntityType.Properties.Where((PropertyConfiguration property) => property.NonFilterable);
			IList<IEdmProperty> list = new List<IEdmProperty>();
			foreach (PropertyConfiguration propertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(propertyConfiguration.PropertyInfo, out edmProperty) && edmProperty != null)
				{
					list.Add(edmProperty);
				}
			}
			if (list.Any<IEdmProperty>())
			{
				model.SetFilterRestrictionsAnnotation(target, true, true, null, list);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002F360 File Offset: 0x0002D560
		private static void AddSortRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, EntitySetConfiguration entitySetConfiguration, EdmTypeMap edmTypeMap)
		{
			IEnumerable<PropertyConfiguration> enumerable = entitySetConfiguration.EntityType.Properties.Where((PropertyConfiguration property) => property.Unsortable);
			IList<IEdmProperty> list = new List<IEdmProperty>();
			foreach (PropertyConfiguration propertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(propertyConfiguration.PropertyInfo, out edmProperty) && edmProperty != null)
				{
					list.Add(edmProperty);
				}
			}
			if (list.Any<IEdmProperty>())
			{
				model.SetSortRestrictionsAnnotation(target, true, null, null, list);
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002F408 File Offset: 0x0002D608
		private static void AddExpandRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, EntitySetConfiguration entitySetConfiguration, EdmTypeMap edmTypeMap)
		{
			IEnumerable<PropertyConfiguration> enumerable = entitySetConfiguration.EntityType.Properties.Where((PropertyConfiguration property) => property.NotExpandable);
			IList<IEdmNavigationProperty> list = new List<IEdmNavigationProperty>();
			foreach (PropertyConfiguration propertyConfiguration in enumerable)
			{
				IEdmProperty edmProperty;
				if (edmTypeMap.EdmProperties.TryGetValue(propertyConfiguration.PropertyInfo, out edmProperty) && edmProperty != null && edmProperty.PropertyKind == EdmPropertyKind.Navigation)
				{
					list.Add((IEdmNavigationProperty)edmProperty);
				}
			}
			if (list.Any<IEdmNavigationProperty>())
			{
				model.SetExpandRestrictionsAnnotation(target, true, list);
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		private static IEdmExpression GetEdmEntitySetExpression(IDictionary<string, EdmNavigationSource> navigationSources, OperationConfiguration operationConfiguration)
		{
			if (operationConfiguration.NavigationSource != null)
			{
				EdmNavigationSource edmNavigationSource;
				if (!navigationSources.TryGetValue(operationConfiguration.NavigationSource.Name, out edmNavigationSource))
				{
					throw Error.InvalidOperation(SRResources.EntitySetNotFoundForName, new object[] { operationConfiguration.NavigationSource.Name });
				}
				EdmEntitySet edmEntitySet = edmNavigationSource as EdmEntitySet;
				if (edmEntitySet != null)
				{
					return new EdmPathExpression(edmEntitySet.Name);
				}
			}
			else if (operationConfiguration.EntitySetPath != null)
			{
				return new EdmPathExpression(operationConfiguration.EntitySetPath);
			}
			return null;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002F530 File Offset: 0x0002D730
		private static IEdmTypeReference GetEdmTypeReference(Dictionary<Type, IEdmType> availableTypes, IEdmTypeConfiguration configuration, bool nullable)
		{
			if (configuration == null)
			{
				return null;
			}
			EdmTypeKind kind = configuration.Kind;
			if (kind == EdmTypeKind.Collection)
			{
				CollectionTypeConfiguration collectionTypeConfiguration = (CollectionTypeConfiguration)configuration;
				return new EdmCollectionTypeReference(new EdmCollectionType(EdmModelHelperMethods.GetEdmTypeReference(availableTypes, collectionTypeConfiguration.ElementType, nullable)));
			}
			Type type = TypeHelper.GetUnderlyingTypeOrSelf(configuration.ClrType);
			if (!TypeHelper.IsEnum(type))
			{
				type = configuration.ClrType;
			}
			IEdmType edmType;
			if (availableTypes.TryGetValue(type, out edmType))
			{
				if (kind == EdmTypeKind.Complex)
				{
					return new EdmComplexTypeReference((IEdmComplexType)edmType, nullable);
				}
				if (kind == EdmTypeKind.Entity)
				{
					return new EdmEntityTypeReference((IEdmEntityType)edmType, nullable);
				}
				if (kind == EdmTypeKind.Enum)
				{
					return new EdmEnumTypeReference((IEdmEnumType)edmType, nullable);
				}
				throw Error.InvalidOperation(SRResources.UnsupportedEdmTypeKind, new object[] { kind.ToString() });
			}
			else
			{
				if (configuration.Kind == EdmTypeKind.Primitive)
				{
					EdmPrimitiveTypeKind typeKind = EdmTypeBuilder.GetTypeKind(((PrimitiveTypeConfiguration)configuration).ClrType);
					return EdmCoreModel.Instance.GetPrimitive(typeKind, nullable);
				}
				throw Error.InvalidOperation(SRResources.NoMatchingIEdmTypeFound, new object[] { configuration.FullName });
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002F628 File Offset: 0x0002D828
		internal static string GetNavigationSourceUrl(this IEdmModel model, IEdmNavigationSource navigationSource)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			NavigationSourceUrlAnnotation annotationValue = model.GetAnnotationValue(navigationSource);
			if (annotationValue == null)
			{
				return navigationSource.Name;
			}
			return annotationValue.Url;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002F669 File Offset: 0x0002D869
		internal static IEnumerable<IEdmAction> GetAvailableActions(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAvailableOperations(entityType, false).OfType<IEdmAction>();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002F678 File Offset: 0x0002D878
		internal static IEnumerable<IEdmFunction> GetAvailableFunctions(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAvailableOperations(entityType, false).OfType<IEdmFunction>();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002F687 File Offset: 0x0002D887
		internal static IEnumerable<IEdmOperation> GetAvailableOperationsBoundToCollection(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAvailableOperations(entityType, true);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002F694 File Offset: 0x0002D894
		internal static IEnumerable<IEdmOperation> GetAvailableOperations(this IEdmModel model, IEdmEntityType entityType, bool boundToCollection = false)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			BindableOperationFinder bindableOperationFinder = model.GetAnnotationValue(model);
			if (bindableOperationFinder == null)
			{
				bindableOperationFinder = new BindableOperationFinder(model);
				model.SetAnnotationValue(model, bindableOperationFinder);
			}
			if (boundToCollection)
			{
				return bindableOperationFinder.FindOperationsBoundToCollection(entityType);
			}
			return bindableOperationFinder.FindOperations(entityType);
		}
	}
}
