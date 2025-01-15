using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AB RID: 427
	internal class CsdlSemanticsSchema : CsdlSemanticsElement, IEdmCheckable
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x000210AC File Offset: 0x0001F2AC
		public CsdlSemanticsSchema(CsdlSemanticsModel model, CsdlSchema schema)
			: base(schema)
		{
			this.model = model;
			this.schema = schema;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002111B File Offset: 0x0001F31B
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00021123 File Offset: 0x0001F323
		public override CsdlElement Element
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002112B File Offset: 0x0001F32B
		public IEnumerable<IEdmSchemaType> Types
		{
			get
			{
				return this.typesCache.GetValue(this, CsdlSemanticsSchema.ComputeTypesFunc, null);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002113F File Offset: 0x0001F33F
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return this.operationsCache.GetValue(this, CsdlSemanticsSchema.ComputeFunctionsFunc, null);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00021153 File Offset: 0x0001F353
		public IEnumerable<IEdmTerm> Terms
		{
			get
			{
				return this.termsCache.GetValue(this, CsdlSemanticsSchema.ComputeTermsFunc, null);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00021167 File Offset: 0x0001F367
		public IEnumerable<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainersCache.GetValue(this, CsdlSemanticsSchema.ComputeEntityContainersFunc, null);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002117B File Offset: 0x0001F37B
		public string Namespace
		{
			get
			{
				return this.schema.Namespace;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00021188 File Offset: 0x0001F388
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002118F File Offset: 0x0001F38F
		private Dictionary<string, object> LabeledExpressions
		{
			get
			{
				return this.labeledExpressionsCache.GetValue(this, CsdlSemanticsSchema.ComputeLabeledExpressionsFunc, null);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000211A3 File Offset: 0x0001F3A3
		public IEnumerable<IEdmOperation> FindOperations(string name)
		{
			return this.FindSchemaElement<IEnumerable<IEdmOperation>>(name, new Func<CsdlSemanticsModel, string, IEnumerable<IEdmOperation>>(ExtensionMethods.FindOperationsInModelTree));
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000211B8 File Offset: 0x0001F3B8
		public IEdmSchemaType FindType(string name)
		{
			return this.FindSchemaElement<IEdmSchemaType>(name, new Func<CsdlSemanticsModel, string, IEdmSchemaType>(ExtensionMethods.FindTypeInModelTree));
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000211CD File Offset: 0x0001F3CD
		public IEdmTerm FindTerm(string name)
		{
			return this.FindSchemaElement<IEdmTerm>(name, new Func<CsdlSemanticsModel, string, IEdmTerm>(CsdlSemanticsSchema.FindTerm));
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000211E2 File Offset: 0x0001F3E2
		public IEdmEntityContainer FindEntityContainer(string name)
		{
			return this.FindSchemaElement<IEdmEntityContainer>(name, new Func<CsdlSemanticsModel, string, IEdmEntityContainer>(CsdlSemanticsSchema.FindEntityContainer));
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000211F8 File Offset: 0x0001F3F8
		public T FindSchemaElement<T>(string name, Func<CsdlSemanticsModel, string, T> modelFinder)
		{
			string text = this.ReplaceAlias(name);
			return modelFinder(this.model, text);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002121A File Offset: 0x0001F41A
		public string UnresolvedName(string qualifiedName)
		{
			if (qualifiedName == null)
			{
				return null;
			}
			return this.ReplaceAlias(qualifiedName);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00021228 File Offset: 0x0001F428
		public IEdmLabeledExpression FindLabeledElement(string label, IEdmEntityType bindingContext)
		{
			object obj;
			if (!this.LabeledExpressions.TryGetValue(label, out obj))
			{
				return null;
			}
			CsdlLabeledExpression csdlLabeledExpression = obj as CsdlLabeledExpression;
			if (csdlLabeledExpression != null)
			{
				return this.WrapLabeledElement(csdlLabeledExpression, bindingContext);
			}
			return this.WrapLabeledElementList((List<CsdlLabeledExpression>)obj, bindingContext);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00021268 File Offset: 0x0001F468
		public IEdmLabeledExpression WrapLabeledElement(CsdlLabeledExpression labeledElement, IEdmEntityType bindingContext)
		{
			IEdmLabeledExpression edmLabeledExpression;
			if (!this.semanticsLabeledElements.TryGetValue(labeledElement, out edmLabeledExpression))
			{
				edmLabeledExpression = new CsdlSemanticsLabeledExpression(labeledElement.Label, labeledElement.Element, bindingContext, this);
				this.semanticsLabeledElements[labeledElement] = edmLabeledExpression;
			}
			return edmLabeledExpression;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000212A7 File Offset: 0x0001F4A7
		internal string ReplaceAlias(string name)
		{
			return this.model.ReplaceAlias(name);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000212B5 File Offset: 0x0001F4B5
		private static IEdmTerm FindTerm(IEdmModel model, string name)
		{
			return model.FindTerm(name);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000212BE File Offset: 0x0001F4BE
		private static IEdmEntityContainer FindEntityContainer(IEdmModel model, string name)
		{
			return model.FindEntityContainer(name);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000212C8 File Offset: 0x0001F4C8
		private static void AddLabeledExpressions(CsdlExpressionBase expression, Dictionary<string, object> result)
		{
			if (expression == null)
			{
				return;
			}
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.Record:
				goto IL_0118;
			case EdmExpressionKind.Collection:
			{
				using (IEnumerator<CsdlExpressionBase> enumerator = ((CsdlCollectionExpression)expression).ElementValues.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CsdlExpressionBase csdlExpressionBase = enumerator.Current;
						CsdlSemanticsSchema.AddLabeledExpressions(csdlExpressionBase, result);
					}
					return;
				}
				break;
			}
			case EdmExpressionKind.Path:
			case EdmExpressionKind.LabeledExpressionReference:
				return;
			case EdmExpressionKind.If:
				goto IL_0159;
			case EdmExpressionKind.Cast:
				CsdlSemanticsSchema.AddLabeledExpressions(((CsdlCastExpression)expression).Operand, result);
				return;
			case EdmExpressionKind.IsType:
				CsdlSemanticsSchema.AddLabeledExpressions(((CsdlIsTypeExpression)expression).Operand, result);
				return;
			case EdmExpressionKind.FunctionApplication:
				break;
			case EdmExpressionKind.Labeled:
			{
				CsdlLabeledExpression csdlLabeledExpression = (CsdlLabeledExpression)expression;
				string label = csdlLabeledExpression.Label;
				object obj;
				if (result.TryGetValue(label, out obj))
				{
					List<CsdlLabeledExpression> list = obj as List<CsdlLabeledExpression>;
					if (list == null)
					{
						list = new List<CsdlLabeledExpression>();
						list.Add((CsdlLabeledExpression)obj);
						result[label] = list;
					}
					list.Add(csdlLabeledExpression);
				}
				else
				{
					result[label] = csdlLabeledExpression;
				}
				CsdlSemanticsSchema.AddLabeledExpressions(csdlLabeledExpression.Element, result);
				return;
			}
			default:
				return;
			}
			using (IEnumerator<CsdlExpressionBase> enumerator2 = ((CsdlApplyExpression)expression).Arguments.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CsdlExpressionBase csdlExpressionBase2 = enumerator2.Current;
					CsdlSemanticsSchema.AddLabeledExpressions(csdlExpressionBase2, result);
				}
				return;
			}
			IL_0118:
			using (IEnumerator<CsdlPropertyValue> enumerator3 = ((CsdlRecordExpression)expression).PropertyValues.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					CsdlPropertyValue csdlPropertyValue = enumerator3.Current;
					CsdlSemanticsSchema.AddLabeledExpressions(csdlPropertyValue.Expression, result);
				}
				return;
			}
			IL_0159:
			CsdlIfExpression csdlIfExpression = (CsdlIfExpression)expression;
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.Test, result);
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.IfTrue, result);
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.IfFalse, result);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000214AC File Offset: 0x0001F6AC
		private static void AddLabeledExpressions(IEnumerable<CsdlAnnotation> annotations, Dictionary<string, object> result)
		{
			foreach (CsdlAnnotation csdlAnnotation in annotations)
			{
				if (csdlAnnotation != null)
				{
					CsdlSemanticsSchema.AddLabeledExpressions(csdlAnnotation.Expression, result);
				}
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000214FC File Offset: 0x0001F6FC
		private IEdmLabeledExpression WrapLabeledElementList(List<CsdlLabeledExpression> labeledExpressions, IEdmEntityType bindingContext)
		{
			IEdmLabeledExpression edmLabeledExpression;
			if (!this.ambiguousLabeledExpressions.TryGetValue(labeledExpressions, out edmLabeledExpression))
			{
				foreach (CsdlLabeledExpression csdlLabeledExpression in labeledExpressions)
				{
					IEdmLabeledExpression edmLabeledExpression2 = this.WrapLabeledElement(csdlLabeledExpression, bindingContext);
					IEdmLabeledExpression edmLabeledExpression4;
					if (edmLabeledExpression != null)
					{
						IEdmLabeledExpression edmLabeledExpression3 = new AmbiguousLabeledExpressionBinding(edmLabeledExpression, edmLabeledExpression2);
						edmLabeledExpression4 = edmLabeledExpression3;
					}
					else
					{
						edmLabeledExpression4 = edmLabeledExpression2;
					}
					edmLabeledExpression = edmLabeledExpression4;
				}
				this.ambiguousLabeledExpressions[labeledExpressions] = edmLabeledExpression;
			}
			return edmLabeledExpression;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002157C File Offset: 0x0001F77C
		private IEnumerable<IEdmTerm> ComputeTerms()
		{
			List<IEdmTerm> list = new List<IEdmTerm>();
			foreach (CsdlTerm csdlTerm in this.schema.Terms)
			{
				list.Add(new CsdlSemanticsTerm(this, csdlTerm));
			}
			return list;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000215DC File Offset: 0x0001F7DC
		private IEnumerable<IEdmEntityContainer> ComputeEntityContainers()
		{
			List<IEdmEntityContainer> list = new List<IEdmEntityContainer>();
			foreach (CsdlEntityContainer csdlEntityContainer in this.schema.EntityContainers)
			{
				list.Add(new CsdlSemanticsEntityContainer(this, csdlEntityContainer));
			}
			return list;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002163C File Offset: 0x0001F83C
		private IEnumerable<IEdmOperation> ComputeOperations()
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			foreach (CsdlOperation csdlOperation in this.schema.Operations)
			{
				CsdlAction csdlAction = csdlOperation as CsdlAction;
				if (csdlAction != null)
				{
					list.Add(new CsdlSemanticsAction(this, csdlAction));
				}
				else
				{
					CsdlFunction csdlFunction = csdlOperation as CsdlFunction;
					list.Add(new CsdlSemanticsFunction(this, csdlFunction));
				}
			}
			return list;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000216C0 File Offset: 0x0001F8C0
		private IEnumerable<IEdmSchemaType> ComputeTypes()
		{
			List<IEdmSchemaType> list = new List<IEdmSchemaType>();
			foreach (CsdlTypeDefinition csdlTypeDefinition in this.schema.TypeDefinitions)
			{
				CsdlSemanticsTypeDefinitionDefinition csdlSemanticsTypeDefinitionDefinition = new CsdlSemanticsTypeDefinitionDefinition(this, csdlTypeDefinition);
				this.AttachDefaultPrimitiveValueConverter(csdlTypeDefinition, csdlSemanticsTypeDefinitionDefinition);
				list.Add(csdlSemanticsTypeDefinitionDefinition);
			}
			foreach (CsdlStructuredType csdlStructuredType in this.schema.StructuredTypes)
			{
				CsdlEntityType csdlEntityType = csdlStructuredType as CsdlEntityType;
				if (csdlEntityType != null)
				{
					list.Add(new CsdlSemanticsEntityTypeDefinition(this, csdlEntityType));
				}
				else
				{
					CsdlComplexType csdlComplexType = csdlStructuredType as CsdlComplexType;
					if (csdlComplexType != null)
					{
						list.Add(new CsdlSemanticsComplexTypeDefinition(this, csdlComplexType));
					}
				}
			}
			foreach (CsdlEnumType csdlEnumType in this.schema.EnumTypes)
			{
				list.Add(new CsdlSemanticsEnumTypeDefinition(this, csdlEnumType));
			}
			return list;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000217F0 File Offset: 0x0001F9F0
		private void AttachDefaultPrimitiveValueConverter(CsdlTypeDefinition typeDefinition, IEdmTypeDefinition edmTypeDefinition)
		{
			string name = typeDefinition.Name;
			string text;
			if (!(name == "UInt16"))
			{
				if (!(name == "UInt32"))
				{
					if (!(name == "UInt64"))
					{
						return;
					}
					text = "Edm.Decimal";
				}
				else
				{
					text = "Edm.Int64";
				}
			}
			else
			{
				text = "Edm.Int32";
			}
			if (string.CompareOrdinal(text, typeDefinition.UnderlyingTypeName) != 0)
			{
				return;
			}
			this.Model.SetPrimitiveValueConverter(edmTypeDefinition, DefaultPrimitiveValueConverter.Instance);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00021864 File Offset: 0x0001FA64
		private Dictionary<string, object> ComputeLabeledExpressions()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (CsdlAnnotations csdlAnnotations in this.schema.OutOfLineAnnotations)
			{
				CsdlSemanticsSchema.AddLabeledExpressions(csdlAnnotations.Annotations, dictionary);
			}
			foreach (CsdlStructuredType csdlStructuredType in this.schema.StructuredTypes)
			{
				CsdlSemanticsSchema.AddLabeledExpressions(csdlStructuredType.VocabularyAnnotations, dictionary);
				foreach (CsdlProperty csdlProperty in csdlStructuredType.StructuralProperties)
				{
					CsdlSemanticsSchema.AddLabeledExpressions(csdlProperty.VocabularyAnnotations, dictionary);
				}
			}
			foreach (CsdlOperation csdlOperation in this.schema.Operations)
			{
				CsdlSemanticsSchema.AddLabeledExpressions(csdlOperation.VocabularyAnnotations, dictionary);
				foreach (CsdlOperationParameter csdlOperationParameter in csdlOperation.Parameters)
				{
					CsdlSemanticsSchema.AddLabeledExpressions(csdlOperationParameter.VocabularyAnnotations, dictionary);
				}
			}
			foreach (CsdlTerm csdlTerm in this.schema.Terms)
			{
				CsdlSemanticsSchema.AddLabeledExpressions(csdlTerm.VocabularyAnnotations, dictionary);
			}
			foreach (CsdlEntityContainer csdlEntityContainer in this.schema.EntityContainers)
			{
				CsdlSemanticsSchema.AddLabeledExpressions(csdlEntityContainer.VocabularyAnnotations, dictionary);
				foreach (CsdlEntitySet csdlEntitySet in csdlEntityContainer.EntitySets)
				{
					CsdlSemanticsSchema.AddLabeledExpressions(csdlEntitySet.VocabularyAnnotations, dictionary);
				}
				foreach (CsdlOperationImport csdlOperationImport in csdlEntityContainer.OperationImports)
				{
					CsdlSemanticsSchema.AddLabeledExpressions(csdlOperationImport.VocabularyAnnotations, dictionary);
					foreach (CsdlOperationParameter csdlOperationParameter2 in csdlOperationImport.Parameters)
					{
						CsdlSemanticsSchema.AddLabeledExpressions(csdlOperationParameter2.VocabularyAnnotations, dictionary);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040006F9 RID: 1785
		private readonly CsdlSemanticsModel model;

		// Token: 0x040006FA RID: 1786
		private readonly CsdlSchema schema;

		// Token: 0x040006FB RID: 1787
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> typesCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>>();

		// Token: 0x040006FC RID: 1788
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> ComputeTypesFunc = (CsdlSemanticsSchema me) => me.ComputeTypes();

		// Token: 0x040006FD RID: 1789
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> operationsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>>();

		// Token: 0x040006FE RID: 1790
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> ComputeFunctionsFunc = (CsdlSemanticsSchema me) => me.ComputeOperations();

		// Token: 0x040006FF RID: 1791
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> entityContainersCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>>();

		// Token: 0x04000700 RID: 1792
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> ComputeEntityContainersFunc = (CsdlSemanticsSchema me) => me.ComputeEntityContainers();

		// Token: 0x04000701 RID: 1793
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmTerm>> termsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmTerm>>();

		// Token: 0x04000702 RID: 1794
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmTerm>> ComputeTermsFunc = (CsdlSemanticsSchema me) => me.ComputeTerms();

		// Token: 0x04000703 RID: 1795
		private readonly Cache<CsdlSemanticsSchema, Dictionary<string, object>> labeledExpressionsCache = new Cache<CsdlSemanticsSchema, Dictionary<string, object>>();

		// Token: 0x04000704 RID: 1796
		private static readonly Func<CsdlSemanticsSchema, Dictionary<string, object>> ComputeLabeledExpressionsFunc = (CsdlSemanticsSchema me) => me.ComputeLabeledExpressions();

		// Token: 0x04000705 RID: 1797
		private readonly Dictionary<CsdlLabeledExpression, IEdmLabeledExpression> semanticsLabeledElements = new Dictionary<CsdlLabeledExpression, IEdmLabeledExpression>();

		// Token: 0x04000706 RID: 1798
		private readonly Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression> ambiguousLabeledExpressions = new Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression>();
	}
}
