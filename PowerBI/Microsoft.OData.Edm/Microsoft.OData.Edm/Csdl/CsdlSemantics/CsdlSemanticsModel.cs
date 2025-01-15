using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A1 RID: 417
	[DebuggerDisplay("CsdlSemanticsModel({string.Join(\",\", DeclaredNamespaces)})")]
	internal class CsdlSemanticsModel : EdmModelBase, IEdmCheckable
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public CsdlSemanticsModel(CsdlModel astModel, IEdmDirectValueAnnotationsManager annotationsManager, IEnumerable<IEdmModel> referencedModels, bool includeDefaultVocabularies = true)
			: base(referencedModels, annotationsManager, includeDefaultVocabularies)
		{
			this.astModel = astModel;
			this.SetEdmReferences(astModel.CurrentModelReferences);
			foreach (CsdlSchema csdlSchema in this.astModel.Schemata)
			{
				this.AddSchema(csdlSchema);
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001F73C File Offset: 0x0001D93C
		public CsdlSemanticsModel(CsdlModel mainCsdlModel, IEdmDirectValueAnnotationsManager annotationsManager, IEnumerable<CsdlModel> referencedCsdlModels, bool includeDefaultVocabularies)
			: base(Enumerable.Empty<IEdmModel>(), annotationsManager, includeDefaultVocabularies)
		{
			this.astModel = mainCsdlModel;
			this.SetEdmReferences(this.astModel.CurrentModelReferences);
			foreach (CsdlModel csdlModel in referencedCsdlModels)
			{
				CsdlSemanticsModel csdlSemanticsModel = new CsdlSemanticsModel(csdlModel, base.DirectValueAnnotationsManager, this, includeDefaultVocabularies);
				base.AddReferencedModel(csdlSemanticsModel);
			}
			foreach (IEdmInclude edmInclude in mainCsdlModel.CurrentModelReferences.SelectMany((IEdmReference s) => s.Includes))
			{
				this.SetNamespaceAlias(edmInclude.Namespace, edmInclude.Alias);
			}
			foreach (CsdlSchema csdlSchema in this.astModel.Schemata)
			{
				this.AddSchema(csdlSchema);
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001F89C File Offset: 0x0001DA9C
		private CsdlSemanticsModel(CsdlModel referencedCsdlModel, IEdmDirectValueAnnotationsManager annotationsManager, CsdlSemanticsModel mainCsdlSemanticsModel, bool includeDefaultVocabularies)
			: base(Enumerable.Empty<IEdmModel>(), annotationsManager, includeDefaultVocabularies)
		{
			this.mainEdmModel = mainCsdlSemanticsModel;
			this.astModel = referencedCsdlModel;
			this.SetEdmReferences(referencedCsdlModel.CurrentModelReferences);
			foreach (IEdmInclude edmInclude in referencedCsdlModel.ParentModelReferences.SelectMany((IEdmReference s) => s.Includes))
			{
				string includeNs = edmInclude.Namespace;
				referencedCsdlModel.Schemata.Any((CsdlSchema s) => s.Namespace == includeNs);
			}
			foreach (IEdmInclude edmInclude2 in referencedCsdlModel.CurrentModelReferences.SelectMany((IEdmReference s) => s.Includes))
			{
				this.SetNamespaceAlias(edmInclude2.Namespace, edmInclude2.Alias);
			}
			foreach (CsdlSchema csdlSchema in referencedCsdlModel.Schemata)
			{
				string schemaNamespace = csdlSchema.Namespace;
				IEdmInclude edmInclude3 = referencedCsdlModel.ParentModelReferences.SelectMany((IEdmReference s) => s.Includes).FirstOrDefault((IEdmInclude s) => s.Namespace == schemaNamespace);
				if (edmInclude3 != null)
				{
					this.AddSchema(csdlSchema, false);
				}
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				foreach (CsdlSemanticsSchema schema in this.schemata)
				{
					foreach (IEdmSchemaType edmSchemaType in schema.Types)
					{
						yield return edmSchemaType;
					}
					IEnumerator<IEdmSchemaType> enumerator2 = null;
					foreach (IEdmSchemaElement edmSchemaElement in schema.Operations)
					{
						yield return edmSchemaElement;
					}
					IEnumerator<IEdmOperation> enumerator3 = null;
					foreach (IEdmSchemaElement edmSchemaElement2 in schema.Terms)
					{
						yield return edmSchemaElement2;
					}
					IEnumerator<IEdmTerm> enumerator4 = null;
					foreach (IEdmEntityContainer edmEntityContainer in schema.EntityContainers)
					{
						yield return edmEntityContainer;
					}
					IEnumerator<IEdmEntityContainer> enumerator5 = null;
					schema = null;
				}
				List<CsdlSemanticsSchema>.Enumerator enumerator = default(List<CsdlSemanticsSchema>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0001FAAD File Offset: 0x0001DCAD
		public override IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return this.schemata.Select((CsdlSemanticsSchema s) => s.Namespace);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001FAD9 File Offset: 0x0001DCD9
		public IDictionary<string, List<CsdlSemanticsAnnotations>> OutOfLineAnnotations
		{
			get
			{
				return this.outOfLineAnnotations;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		public override IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				List<IEdmVocabularyAnnotation> list = new List<IEdmVocabularyAnnotation>();
				foreach (CsdlSemanticsSchema csdlSemanticsSchema in this.schemata)
				{
					foreach (CsdlAnnotations csdlAnnotations in ((CsdlSchema)csdlSemanticsSchema.Element).OutOfLineAnnotations)
					{
						CsdlSemanticsAnnotations csdlSemanticsAnnotations = new CsdlSemanticsAnnotations(csdlSemanticsSchema, csdlAnnotations);
						foreach (CsdlAnnotation csdlAnnotation in csdlAnnotations.Annotations)
						{
							IEdmVocabularyAnnotation edmVocabularyAnnotation = this.WrapVocabularyAnnotation(csdlAnnotation, csdlSemanticsSchema, null, csdlSemanticsAnnotations, csdlAnnotations.Qualifier);
							edmVocabularyAnnotation.SetSerializationLocation(this, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.OutOfLine));
							edmVocabularyAnnotation.SetSchemaNamespace(this, csdlSemanticsSchema.Namespace);
							list.Add(edmVocabularyAnnotation);
						}
					}
				}
				foreach (IEdmSchemaElement edmSchemaElement in this.SchemaElements)
				{
					list.AddRange(((CsdlSemanticsElement)edmSchemaElement).InlineVocabularyAnnotations);
					CsdlSemanticsStructuredTypeDefinition csdlSemanticsStructuredTypeDefinition = edmSchemaElement as CsdlSemanticsStructuredTypeDefinition;
					if (csdlSemanticsStructuredTypeDefinition != null)
					{
						foreach (IEdmProperty edmProperty in csdlSemanticsStructuredTypeDefinition.DeclaredProperties)
						{
							list.AddRange(((CsdlSemanticsElement)edmProperty).InlineVocabularyAnnotations);
						}
					}
					CsdlSemanticsOperation csdlSemanticsOperation = edmSchemaElement as CsdlSemanticsOperation;
					if (csdlSemanticsOperation != null)
					{
						foreach (IEdmOperationParameter edmOperationParameter in csdlSemanticsOperation.Parameters)
						{
							list.AddRange(((CsdlSemanticsElement)edmOperationParameter).InlineVocabularyAnnotations);
						}
					}
					CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = edmSchemaElement as CsdlSemanticsEntityContainer;
					if (csdlSemanticsEntityContainer != null)
					{
						foreach (IEdmEntityContainerElement edmEntityContainerElement in csdlSemanticsEntityContainer.Elements)
						{
							list.AddRange(((CsdlSemanticsElement)edmEntityContainerElement).InlineVocabularyAnnotations);
						}
					}
					CsdlSemanticsEnumTypeDefinition csdlSemanticsEnumTypeDefinition = edmSchemaElement as CsdlSemanticsEnumTypeDefinition;
					if (csdlSemanticsEnumTypeDefinition != null)
					{
						foreach (IEdmEnumMember edmEnumMember in csdlSemanticsEnumTypeDefinition.Members)
						{
							list.AddRange(((CsdlSemanticsElement)edmEnumMember).InlineVocabularyAnnotations);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001FE20 File Offset: 0x0001E020
		public IEnumerable<EdmError> Errors
		{
			get
			{
				List<EdmError> list = new List<EdmError>();
				HashSetInternal<string> hashSetInternal = new HashSetInternal<string>();
				VersioningList<string> usedNamespacesHavingAlias = this.GetUsedNamespacesHavingAlias();
				VersioningDictionary<string, string> namespaceAliases = this.GetNamespaceAliases();
				if (usedNamespacesHavingAlias != null && namespaceAliases != null)
				{
					foreach (string text in usedNamespacesHavingAlias)
					{
						string text2;
						if (namespaceAliases.TryGetValue(text, out text2) && !hashSetInternal.Add(text2))
						{
							list.Add(new EdmError(this.Location(), EdmErrorCode.DuplicateAlias, Strings.CsdlSemantics_DuplicateAlias(text, text2)));
						}
					}
				}
				foreach (CsdlSemanticsSchema csdlSemanticsSchema in this.schemata)
				{
					list.AddRange(csdlSemanticsSchema.Errors());
				}
				return list;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001FF08 File Offset: 0x0001E108
		internal CsdlSemanticsModel MainModel
		{
			get
			{
				return this.mainEdmModel;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001FF10 File Offset: 0x0001E110
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			CsdlSemanticsElement csdlSemanticsElement = element as CsdlSemanticsElement;
			IEnumerable<IEdmVocabularyAnnotation> enumerable = ((csdlSemanticsElement != null && csdlSemanticsElement.Model == this) ? csdlSemanticsElement.InlineVocabularyAnnotations : Enumerable.Empty<IEdmVocabularyAnnotation>());
			string text = EdmUtil.FullyQualifiedName(element);
			List<CsdlSemanticsAnnotations> list;
			if (text != null && this.outOfLineAnnotations.TryGetValue(text, out list))
			{
				List<IEdmVocabularyAnnotation> list2 = new List<IEdmVocabularyAnnotation>();
				foreach (CsdlSemanticsAnnotations csdlSemanticsAnnotations in list)
				{
					foreach (CsdlAnnotation csdlAnnotation in csdlSemanticsAnnotations.Annotations.Annotations)
					{
						IEdmVocabularyAnnotation edmVocabularyAnnotation = this.WrapVocabularyAnnotation(csdlAnnotation, csdlSemanticsAnnotations.Context, null, csdlSemanticsAnnotations, csdlSemanticsAnnotations.Annotations.Qualifier);
						edmVocabularyAnnotation.SetSerializationLocation(this, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.OutOfLine));
						list2.Add(edmVocabularyAnnotation);
					}
				}
				return enumerable.Concat(list2);
			}
			return enumerable;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00020024 File Offset: 0x0001E224
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			List<IEdmStructuredType> list2;
			if (this.derivedTypeMappings.TryGetValue(((IEdmSchemaType)baseType).Name, out list2))
			{
				list.AddRange(list2.Where((IEdmStructuredType t) => t.BaseType == baseType));
			}
			foreach (IEdmModel edmModel in base.ReferencedModels)
			{
				list.AddRange(edmModel.FindDirectlyDerivedTypes(baseType));
			}
			return list;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000200CC File Offset: 0x0001E2CC
		internal void AddToReferencedModels(IEnumerable<IEdmModel> models)
		{
			foreach (IEdmModel edmModel in models)
			{
				base.AddReferencedModel(edmModel);
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00020114 File Offset: 0x0001E314
		internal static IEdmExpression WrapExpression(CsdlExpressionBase expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
		{
			if (expression != null)
			{
				switch (expression.ExpressionKind)
				{
				case EdmExpressionKind.BinaryConstant:
					return new CsdlSemanticsBinaryConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.BooleanConstant:
					return new CsdlSemanticsBooleanConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.DateTimeOffsetConstant:
					return new CsdlSemanticsDateTimeOffsetConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.DecimalConstant:
					return new CsdlSemanticsDecimalConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.FloatingConstant:
					return new CsdlSemanticsFloatingConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.GuidConstant:
					return new CsdlSemanticsGuidConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.IntegerConstant:
					return new CsdlSemanticsIntConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.StringConstant:
					return new CsdlSemanticsStringConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.DurationConstant:
					return new CsdlSemanticsDurationConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.Null:
					return new CsdlSemanticsNullExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.Record:
					return new CsdlSemanticsRecordExpression((CsdlRecordExpression)expression, bindingContext, schema);
				case EdmExpressionKind.Collection:
					return new CsdlSemanticsCollectionExpression((CsdlCollectionExpression)expression, bindingContext, schema);
				case EdmExpressionKind.Path:
					return new CsdlSemanticsPathExpression((CsdlPathExpression)expression, bindingContext, schema);
				case EdmExpressionKind.If:
					return new CsdlSemanticsIfExpression((CsdlIfExpression)expression, bindingContext, schema);
				case EdmExpressionKind.Cast:
					return new CsdlSemanticsCastExpression((CsdlCastExpression)expression, bindingContext, schema);
				case EdmExpressionKind.IsType:
					return new CsdlSemanticsIsTypeExpression((CsdlIsTypeExpression)expression, bindingContext, schema);
				case EdmExpressionKind.FunctionApplication:
					return new CsdlSemanticsApplyExpression((CsdlApplyExpression)expression, bindingContext, schema);
				case EdmExpressionKind.LabeledExpressionReference:
					return new CsdlSemanticsLabeledExpressionReferenceExpression((CsdlLabeledExpressionReferenceExpression)expression, bindingContext, schema);
				case EdmExpressionKind.Labeled:
					return schema.WrapLabeledElement((CsdlLabeledExpression)expression, bindingContext);
				case EdmExpressionKind.PropertyPath:
					return new CsdlSemanticsPropertyPathExpression((CsdlPropertyPathExpression)expression, bindingContext, schema);
				case EdmExpressionKind.NavigationPropertyPath:
					return new CsdlSemanticsNavigationPropertyPathExpression((CsdlNavigationPropertyPathExpression)expression, bindingContext, schema);
				case EdmExpressionKind.DateConstant:
					return new CsdlSemanticsDateConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.TimeOfDayConstant:
					return new CsdlSemanticsTimeOfDayConstantExpression((CsdlConstantExpression)expression, schema);
				case EdmExpressionKind.EnumMember:
					return new CsdlSemanticsEnumMemberExpression((CsdlEnumMemberExpression)expression, bindingContext, schema);
				case EdmExpressionKind.AnnotationPath:
					return new CsdlSemanticsAnnotationPathExpression((CsdlAnnotationPathExpression)expression, bindingContext, schema);
				}
			}
			return null;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000202F4 File Offset: 0x0001E4F4
		internal static IEdmTypeReference WrapTypeReference(CsdlSemanticsSchema schema, CsdlTypeReference type)
		{
			CsdlNamedTypeReference csdlNamedTypeReference = type as CsdlNamedTypeReference;
			if (csdlNamedTypeReference != null)
			{
				CsdlPrimitiveTypeReference csdlPrimitiveTypeReference = csdlNamedTypeReference as CsdlPrimitiveTypeReference;
				if (csdlPrimitiveTypeReference != null)
				{
					switch (csdlPrimitiveTypeReference.Kind)
					{
					case EdmPrimitiveTypeKind.Binary:
						return new CsdlSemanticsBinaryTypeReference(schema, (CsdlBinaryTypeReference)csdlPrimitiveTypeReference);
					case EdmPrimitiveTypeKind.Boolean:
					case EdmPrimitiveTypeKind.Byte:
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Guid:
					case EdmPrimitiveTypeKind.Int16:
					case EdmPrimitiveTypeKind.Int32:
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.SByte:
					case EdmPrimitiveTypeKind.Single:
					case EdmPrimitiveTypeKind.Stream:
					case EdmPrimitiveTypeKind.Date:
						return new CsdlSemanticsPrimitiveTypeReference(schema, csdlPrimitiveTypeReference);
					case EdmPrimitiveTypeKind.DateTimeOffset:
					case EdmPrimitiveTypeKind.Duration:
					case EdmPrimitiveTypeKind.TimeOfDay:
						return new CsdlSemanticsTemporalTypeReference(schema, (CsdlTemporalTypeReference)csdlPrimitiveTypeReference);
					case EdmPrimitiveTypeKind.Decimal:
						return new CsdlSemanticsDecimalTypeReference(schema, (CsdlDecimalTypeReference)csdlPrimitiveTypeReference);
					case EdmPrimitiveTypeKind.String:
						return new CsdlSemanticsStringTypeReference(schema, (CsdlStringTypeReference)csdlPrimitiveTypeReference);
					case EdmPrimitiveTypeKind.Geography:
					case EdmPrimitiveTypeKind.GeographyPoint:
					case EdmPrimitiveTypeKind.GeographyLineString:
					case EdmPrimitiveTypeKind.GeographyPolygon:
					case EdmPrimitiveTypeKind.GeographyCollection:
					case EdmPrimitiveTypeKind.GeographyMultiPolygon:
					case EdmPrimitiveTypeKind.GeographyMultiLineString:
					case EdmPrimitiveTypeKind.GeographyMultiPoint:
					case EdmPrimitiveTypeKind.Geometry:
					case EdmPrimitiveTypeKind.GeometryPoint:
					case EdmPrimitiveTypeKind.GeometryLineString:
					case EdmPrimitiveTypeKind.GeometryPolygon:
					case EdmPrimitiveTypeKind.GeometryCollection:
					case EdmPrimitiveTypeKind.GeometryMultiPolygon:
					case EdmPrimitiveTypeKind.GeometryMultiLineString:
					case EdmPrimitiveTypeKind.GeometryMultiPoint:
						return new CsdlSemanticsSpatialTypeReference(schema, (CsdlSpatialTypeReference)csdlPrimitiveTypeReference);
					}
				}
				else
				{
					CsdlUntypedTypeReference csdlUntypedTypeReference = csdlNamedTypeReference as CsdlUntypedTypeReference;
					if (csdlUntypedTypeReference != null)
					{
						return new CsdlSemanticsUntypedTypeReference(schema, csdlUntypedTypeReference);
					}
					if (schema.FindType(csdlNamedTypeReference.FullName) is IEdmTypeDefinition)
					{
						return new CsdlSemanticsTypeDefinitionReference(schema, csdlNamedTypeReference);
					}
				}
				return new CsdlSemanticsNamedTypeReference(schema, csdlNamedTypeReference);
			}
			CsdlExpressionTypeReference csdlExpressionTypeReference = type as CsdlExpressionTypeReference;
			if (csdlExpressionTypeReference != null)
			{
				CsdlCollectionType csdlCollectionType = csdlExpressionTypeReference.TypeExpression as CsdlCollectionType;
				if (csdlCollectionType != null)
				{
					return new CsdlSemanticsCollectionTypeExpression(csdlExpressionTypeReference, new CsdlSemanticsCollectionTypeDefinition(schema, csdlCollectionType));
				}
				CsdlEntityReferenceType csdlEntityReferenceType = csdlExpressionTypeReference.TypeExpression as CsdlEntityReferenceType;
				if (csdlEntityReferenceType != null)
				{
					return new CsdlSemanticsEntityReferenceTypeExpression(csdlExpressionTypeReference, new CsdlSemanticsEntityReferenceTypeDefinition(schema, csdlEntityReferenceType));
				}
			}
			return null;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002047C File Offset: 0x0001E67C
		internal IEnumerable<IEdmVocabularyAnnotation> WrapInlineVocabularyAnnotations(CsdlSemanticsElement element, CsdlSemanticsSchema schema)
		{
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = element as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				IEnumerable<CsdlAnnotation> vocabularyAnnotations = element.Element.VocabularyAnnotations;
				if (vocabularyAnnotations.FirstOrDefault<CsdlAnnotation>() != null)
				{
					List<IEdmVocabularyAnnotation> list = new List<IEdmVocabularyAnnotation>();
					foreach (CsdlAnnotation csdlAnnotation in vocabularyAnnotations)
					{
						IEdmVocabularyAnnotation edmVocabularyAnnotation = this.WrapVocabularyAnnotation(csdlAnnotation, schema, edmVocabularyAnnotatable, null, csdlAnnotation.Qualifier);
						edmVocabularyAnnotation.SetSerializationLocation(this, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
						list.Add(edmVocabularyAnnotation);
					}
					return list;
				}
			}
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00020514 File Offset: 0x0001E714
		private IEdmVocabularyAnnotation WrapVocabularyAnnotation(CsdlAnnotation annotation, CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, string qualifier)
		{
			return EdmUtil.DictionaryGetOrUpdate<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation>(this.wrappedAnnotations, annotation, (CsdlAnnotation ann) => new CsdlSemanticsVocabularyAnnotation(schema, targetContext, annotationsContext, ann, qualifier));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002055D File Offset: 0x0001E75D
		private void AddSchema(CsdlSchema schema)
		{
			this.AddSchema(schema, true);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00020568 File Offset: 0x0001E768
		private void AddSchema(CsdlSchema schema, bool addAnnotations)
		{
			CsdlSemanticsSchema csdlSemanticsSchema = new CsdlSemanticsSchema(this, schema);
			this.schemata.Add(csdlSemanticsSchema);
			foreach (IEdmSchemaType edmSchemaType in csdlSemanticsSchema.Types)
			{
				CsdlSemanticsStructuredTypeDefinition csdlSemanticsStructuredTypeDefinition = edmSchemaType as CsdlSemanticsStructuredTypeDefinition;
				if (csdlSemanticsStructuredTypeDefinition != null)
				{
					string baseTypeName = ((CsdlNamedStructuredType)csdlSemanticsStructuredTypeDefinition.Element).BaseTypeName;
					if (baseTypeName != null)
					{
						string text;
						string text2;
						EdmUtil.TryGetNamespaceNameFromQualifiedName(baseTypeName, out text, out text2);
						if (text2 != null)
						{
							List<IEdmStructuredType> list;
							if (!this.derivedTypeMappings.TryGetValue(text2, out list))
							{
								list = new List<IEdmStructuredType>();
								this.derivedTypeMappings[text2] = list;
							}
							list.Add(csdlSemanticsStructuredTypeDefinition);
						}
					}
				}
				base.RegisterElement(edmSchemaType);
			}
			foreach (IEdmOperation edmOperation in csdlSemanticsSchema.Operations)
			{
				base.RegisterElement(edmOperation);
			}
			foreach (IEdmTerm edmTerm in csdlSemanticsSchema.Terms)
			{
				base.RegisterElement(edmTerm);
			}
			foreach (IEdmEntityContainer edmEntityContainer in csdlSemanticsSchema.EntityContainers)
			{
				base.RegisterElement(edmEntityContainer);
			}
			if (!string.IsNullOrEmpty(schema.Alias))
			{
				this.SetNamespaceAlias(schema.Namespace, schema.Alias);
			}
			if (addAnnotations)
			{
				foreach (CsdlAnnotations csdlAnnotations in schema.OutOfLineAnnotations)
				{
					string text3 = this.ReplaceAlias(csdlAnnotations.Target);
					List<CsdlSemanticsAnnotations> list2;
					if (!this.outOfLineAnnotations.TryGetValue(text3, out list2))
					{
						list2 = new List<CsdlSemanticsAnnotations>();
						this.outOfLineAnnotations[text3] = list2;
					}
					list2.Add(new CsdlSemanticsAnnotations(csdlSemanticsSchema, csdlAnnotations));
				}
			}
			Version edmVersion = this.GetEdmVersion();
			if (edmVersion == null || edmVersion < schema.Version)
			{
				this.SetEdmVersion(schema.Version);
			}
		}

		// Token: 0x040006D1 RID: 1745
		private readonly CsdlSemanticsModel mainEdmModel;

		// Token: 0x040006D2 RID: 1746
		private readonly CsdlModel astModel;

		// Token: 0x040006D3 RID: 1747
		private readonly List<CsdlSemanticsSchema> schemata = new List<CsdlSemanticsSchema>();

		// Token: 0x040006D4 RID: 1748
		private readonly Dictionary<string, List<CsdlSemanticsAnnotations>> outOfLineAnnotations = new Dictionary<string, List<CsdlSemanticsAnnotations>>();

		// Token: 0x040006D5 RID: 1749
		private readonly ConcurrentDictionary<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation> wrappedAnnotations = new ConcurrentDictionary<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation>();

		// Token: 0x040006D6 RID: 1750
		private readonly Dictionary<string, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<string, List<IEdmStructuredType>>();
	}
}
