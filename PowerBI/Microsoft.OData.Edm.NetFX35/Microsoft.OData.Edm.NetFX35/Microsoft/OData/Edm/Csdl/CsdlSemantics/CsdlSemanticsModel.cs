using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B1 RID: 433
	[DebuggerDisplay("CsdlSemanticsModel({string.Join(\",\", DeclaredNamespaces)})")]
	internal class CsdlSemanticsModel : EdmModelBase, IEdmCheckable
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00017540 File Offset: 0x00015740
		public CsdlSemanticsModel(CsdlModel astModel, IEdmDirectValueAnnotationsManager annotationsManager, IEnumerable<IEdmModel> referencedModels)
			: base(referencedModels, annotationsManager)
		{
			this.astModel = astModel;
			this.SetEdmReferences(astModel.CurrentModelReferences);
			foreach (CsdlSchema csdlSchema in this.astModel.Schemata)
			{
				this.AddSchema(csdlSchema);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000175E4 File Offset: 0x000157E4
		public CsdlSemanticsModel(CsdlModel mainCsdlModel, IEdmDirectValueAnnotationsManager annotationsManager, IEnumerable<CsdlModel> referencedCsdlModels)
			: base(Enumerable.Empty<IEdmModel>(), annotationsManager)
		{
			this.astModel = mainCsdlModel;
			this.SetEdmReferences(this.astModel.CurrentModelReferences);
			foreach (CsdlModel csdlModel in referencedCsdlModels)
			{
				base.AddReferencedModel(new CsdlSemanticsModel(csdlModel, base.DirectValueAnnotationsManager, this));
			}
			foreach (IEdmInclude edmInclude in Enumerable.SelectMany<IEdmReference, IEdmInclude>(mainCsdlModel.CurrentModelReferences, (IEdmReference s) => s.Includes))
			{
				this.SetNamespaceAlias(edmInclude.Namespace, edmInclude.Alias);
			}
			foreach (CsdlSchema csdlSchema in this.astModel.Schemata)
			{
				this.AddSchema(csdlSchema);
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001778C File Offset: 0x0001598C
		private CsdlSemanticsModel(CsdlModel referencedCsdlModel, IEdmDirectValueAnnotationsManager annotationsManager, CsdlSemanticsModel mainCsdlSemanticsModel)
			: base(Enumerable.Empty<IEdmModel>(), annotationsManager)
		{
			this.mainEdmModel = mainCsdlSemanticsModel;
			this.astModel = referencedCsdlModel;
			this.SetEdmReferences(referencedCsdlModel.CurrentModelReferences);
			foreach (IEdmInclude edmInclude in Enumerable.SelectMany<IEdmReference, IEdmInclude>(referencedCsdlModel.ParentModelReferences, (IEdmReference s) => s.Includes))
			{
				string includeNs = edmInclude.Namespace;
				Enumerable.Any<CsdlSchema>(referencedCsdlModel.Schemata, (CsdlSchema s) => s.Namespace == includeNs);
			}
			foreach (IEdmInclude edmInclude2 in Enumerable.SelectMany<IEdmReference, IEdmInclude>(referencedCsdlModel.CurrentModelReferences, (IEdmReference s) => s.Includes))
			{
				this.SetNamespaceAlias(edmInclude2.Namespace, edmInclude2.Alias);
			}
			foreach (CsdlSchema csdlSchema in referencedCsdlModel.Schemata)
			{
				string schemaNamespace = csdlSchema.Namespace;
				IEdmInclude edmInclude3 = Enumerable.FirstOrDefault<IEdmInclude>(Enumerable.SelectMany<IEdmReference, IEdmInclude>(referencedCsdlModel.ParentModelReferences, (IEdmReference s) => s.Includes), (IEdmInclude s) => s.Namespace == schemaNamespace);
				if (edmInclude3 != null)
				{
					this.AddSchema(csdlSchema, false);
				}
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00017DEC File Offset: 0x00015FEC
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				foreach (CsdlSemanticsSchema schema in this.schemata)
				{
					foreach (IEdmSchemaType type in schema.Types)
					{
						yield return type;
					}
					foreach (IEdmSchemaElement function in schema.Operations)
					{
						yield return function;
					}
					foreach (IEdmSchemaElement valueTerm in schema.ValueTerms)
					{
						yield return valueTerm;
					}
					foreach (IEdmEntityContainer entityContainer in schema.EntityContainers)
					{
						yield return entityContainer;
					}
				}
				yield break;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00017E11 File Offset: 0x00016011
		public override IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return Enumerable.Select<CsdlSemanticsSchema, string>(this.schemata, (CsdlSemanticsSchema s) => s.Namespace);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00017E3C File Offset: 0x0001603C
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00018174 File Offset: 0x00016374
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

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001825C File Offset: 0x0001645C
		internal CsdlSemanticsModel MainModel
		{
			get
			{
				return this.mainEdmModel;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00018264 File Offset: 0x00016464
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			CsdlSemanticsElement csdlSemanticsElement = element as CsdlSemanticsElement;
			IEnumerable<IEdmVocabularyAnnotation> enumerable = ((csdlSemanticsElement != null && csdlSemanticsElement.Model == this) ? csdlSemanticsElement.InlineVocabularyAnnotations : Enumerable.Empty<IEdmVocabularyAnnotation>());
			string text = EdmUtil.FullyQualifiedName(element);
			List<CsdlSemanticsAnnotations> list;
			if (text != null && this.outOfLineAnnotations.TryGetValue(text, ref list))
			{
				List<IEdmVocabularyAnnotation> list2 = new List<IEdmVocabularyAnnotation>();
				foreach (CsdlSemanticsAnnotations csdlSemanticsAnnotations in list)
				{
					foreach (CsdlAnnotation csdlAnnotation in csdlSemanticsAnnotations.Annotations.Annotations)
					{
						list2.Add(this.WrapVocabularyAnnotation(csdlAnnotation, csdlSemanticsAnnotations.Context, null, csdlSemanticsAnnotations, csdlSemanticsAnnotations.Annotations.Qualifier));
					}
				}
				return Enumerable.Concat<IEdmVocabularyAnnotation>(enumerable, list2);
			}
			return enumerable;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00018380 File Offset: 0x00016580
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			List<IEdmStructuredType> list2;
			if (this.derivedTypeMappings.TryGetValue(((IEdmSchemaType)baseType).Name, ref list2))
			{
				list.AddRange(Enumerable.Where<IEdmStructuredType>(list2, (IEdmStructuredType t) => t.BaseType == baseType));
			}
			foreach (IEdmModel edmModel in base.ReferencedModels)
			{
				list.AddRange(edmModel.FindDirectlyDerivedTypes(baseType));
			}
			return list;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00018474 File Offset: 0x00016674
		public string ReplaceAlias(string name)
		{
			CsdlSemanticsModel.<>c__DisplayClass37 CS$<>8__locals1 = new CsdlSemanticsModel.<>c__DisplayClass37();
			CS$<>8__locals1.mappings = this.GetNamespaceAliases();
			VersioningList<string> usedNamespacesHavingAlias = this.GetUsedNamespacesHavingAlias();
			if (usedNamespacesHavingAlias == null || CS$<>8__locals1.mappings == null || !name.Contains("."))
			{
				return null;
			}
			string typeAlias = Enumerable.First<string>(name.Split(new char[] { '.' }));
			string text = Enumerable.FirstOrDefault<string>(usedNamespacesHavingAlias, delegate(string n)
			{
				string text2;
				return CS$<>8__locals1.mappings.TryGetValue(n, out text2) && text2 == typeAlias;
			});
			if (text == null)
			{
				return null;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				text,
				name.Substring(typeAlias.Length + 1)
			});
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00018538 File Offset: 0x00016738
		internal void AddToReferencedModels(IEnumerable<IEdmModel> models)
		{
			foreach (IEdmModel edmModel in models)
			{
				base.AddReferencedModel(edmModel);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00018580 File Offset: 0x00016780
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
				case EdmExpressionKind.ParameterReference:
					return new CsdlSemanticsParameterReferenceExpression((CsdlParameterReferenceExpression)expression, bindingContext, schema);
				case EdmExpressionKind.OperationReference:
					return new CsdlSemanticsOperationReferenceExpression((CsdlOperationReferenceExpression)expression, bindingContext, schema);
				case EdmExpressionKind.PropertyReference:
					return new CsdlSemanticsPropertyReferenceExpression((CsdlPropertyReferenceExpression)expression, bindingContext, schema);
				case EdmExpressionKind.EntitySetReference:
					return new CsdlSemanticsEntitySetReferenceExpression((CsdlEntitySetReferenceExpression)expression, bindingContext, schema);
				case EdmExpressionKind.EnumMemberReference:
				case EdmExpressionKind.EnumMember:
					return new CsdlSemanticsEnumMemberExpression((CsdlEnumMemberExpression)expression, bindingContext, schema);
				case EdmExpressionKind.If:
					return new CsdlSemanticsIfExpression((CsdlIfExpression)expression, bindingContext, schema);
				case EdmExpressionKind.Cast:
					return new CsdlSemanticsCastExpression((CsdlCastExpression)expression, bindingContext, schema);
				case EdmExpressionKind.IsType:
					return new CsdlSemanticsIsTypeExpression((CsdlIsTypeExpression)expression, bindingContext, schema);
				case EdmExpressionKind.OperationApplication:
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
				}
			}
			return null;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001879C File Offset: 0x0001699C
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

		// Token: 0x0600090E RID: 2318 RVA: 0x000188F4 File Offset: 0x00016AF4
		internal IEnumerable<IEdmVocabularyAnnotation> WrapInlineVocabularyAnnotations(CsdlSemanticsElement element, CsdlSemanticsSchema schema)
		{
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = element as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				IEnumerable<CsdlAnnotation> vocabularyAnnotations = element.Element.VocabularyAnnotations;
				if (Enumerable.FirstOrDefault<CsdlAnnotation>(vocabularyAnnotations) != null)
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

		// Token: 0x0600090F RID: 2319 RVA: 0x000189B8 File Offset: 0x00016BB8
		private IEdmVocabularyAnnotation WrapVocabularyAnnotation(CsdlAnnotation annotation, CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, string qualifier)
		{
			return EdmUtil.DictionaryGetOrUpdate<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation>(this.wrappedAnnotations, annotation, (CsdlAnnotation ann) => new CsdlSemanticsValueAnnotation(schema, targetContext, annotationsContext, ann, qualifier));
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00018A01 File Offset: 0x00016C01
		private void AddSchema(CsdlSchema schema)
		{
			this.AddSchema(schema, true);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00018A0C File Offset: 0x00016C0C
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
							if (!this.derivedTypeMappings.TryGetValue(text2, ref list))
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
			foreach (IEdmValueTerm edmValueTerm in csdlSemanticsSchema.ValueTerms)
			{
				base.RegisterElement(edmValueTerm);
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
					string text3 = csdlAnnotations.Target;
					string text4 = this.ReplaceAlias(text3);
					if (text4 != null)
					{
						text3 = text4;
					}
					List<CsdlSemanticsAnnotations> list2;
					if (!this.outOfLineAnnotations.TryGetValue(text3, ref list2))
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

		// Token: 0x0400046B RID: 1131
		private readonly CsdlSemanticsModel mainEdmModel;

		// Token: 0x0400046C RID: 1132
		private readonly CsdlModel astModel;

		// Token: 0x0400046D RID: 1133
		private readonly List<CsdlSemanticsSchema> schemata = new List<CsdlSemanticsSchema>();

		// Token: 0x0400046E RID: 1134
		private readonly Dictionary<string, List<CsdlSemanticsAnnotations>> outOfLineAnnotations = new Dictionary<string, List<CsdlSemanticsAnnotations>>();

		// Token: 0x0400046F RID: 1135
		private readonly Dictionary<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation> wrappedAnnotations = new Dictionary<CsdlAnnotation, CsdlSemanticsVocabularyAnnotation>();

		// Token: 0x04000470 RID: 1136
		private readonly Dictionary<string, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<string, List<IEdmStructuredType>>();
	}
}
