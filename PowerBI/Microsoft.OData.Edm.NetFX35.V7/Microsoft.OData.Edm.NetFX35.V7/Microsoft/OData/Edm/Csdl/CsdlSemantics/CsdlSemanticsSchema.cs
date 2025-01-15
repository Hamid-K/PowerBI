using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019A RID: 410
	internal class CsdlSemanticsSchema : CsdlSemanticsElement, IEdmCheckable
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0001E914 File Offset: 0x0001CB14
		public CsdlSemanticsSchema(CsdlSemanticsModel model, CsdlSchema schema)
			: base(schema)
		{
			this.model = model;
			this.schema = schema;
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0001E983 File Offset: 0x0001CB83
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0001E98B File Offset: 0x0001CB8B
		public override CsdlElement Element
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001E993 File Offset: 0x0001CB93
		public IEnumerable<IEdmSchemaType> Types
		{
			get
			{
				return this.typesCache.GetValue(this, CsdlSemanticsSchema.ComputeTypesFunc, null);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001E9A7 File Offset: 0x0001CBA7
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return this.operationsCache.GetValue(this, CsdlSemanticsSchema.ComputeFunctionsFunc, null);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0001E9BB File Offset: 0x0001CBBB
		public IEnumerable<IEdmTerm> Terms
		{
			get
			{
				return this.termsCache.GetValue(this, CsdlSemanticsSchema.ComputeTermsFunc, null);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001E9CF File Offset: 0x0001CBCF
		public IEnumerable<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainersCache.GetValue(this, CsdlSemanticsSchema.ComputeEntityContainersFunc, null);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001E9E3 File Offset: 0x0001CBE3
		public string Namespace
		{
			get
			{
				return this.schema.Namespace;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001E9F7 File Offset: 0x0001CBF7
		private Dictionary<string, object> LabeledExpressions
		{
			get
			{
				return this.labeledExpressionsCache.GetValue(this, CsdlSemanticsSchema.ComputeLabeledExpressionsFunc, null);
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001EA0B File Offset: 0x0001CC0B
		public IEnumerable<IEdmOperation> FindOperations(string name)
		{
			return this.FindSchemaElement<IEnumerable<IEdmOperation>>(name, new Func<CsdlSemanticsModel, string, IEnumerable<IEdmOperation>>(ExtensionMethods.FindOperationsInModelTree));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001EA20 File Offset: 0x0001CC20
		public IEdmSchemaType FindType(string name)
		{
			return this.FindSchemaElement<IEdmSchemaType>(name, new Func<CsdlSemanticsModel, string, IEdmSchemaType>(ExtensionMethods.FindTypeInModelTree));
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001EA35 File Offset: 0x0001CC35
		public IEdmTerm FindTerm(string name)
		{
			return this.FindSchemaElement<IEdmTerm>(name, new Func<CsdlSemanticsModel, string, IEdmTerm>(CsdlSemanticsSchema.FindTerm));
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001EA4A File Offset: 0x0001CC4A
		public IEdmEntityContainer FindEntityContainer(string name)
		{
			return this.FindSchemaElement<IEdmEntityContainer>(name, new Func<CsdlSemanticsModel, string, IEdmEntityContainer>(CsdlSemanticsSchema.FindEntityContainer));
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001EA60 File Offset: 0x0001CC60
		public T FindSchemaElement<T>(string name, Func<CsdlSemanticsModel, string, T> modelFinder)
		{
			string text = this.ReplaceAlias(name);
			if (text == null)
			{
				text = name;
			}
			return modelFinder.Invoke(this.model, text);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001EA87 File Offset: 0x0001CC87
		public string UnresolvedName(string qualifiedName)
		{
			if (qualifiedName == null)
			{
				return null;
			}
			return this.ReplaceAlias(qualifiedName) ?? qualifiedName;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		public IEdmLabeledExpression FindLabeledElement(string label, IEdmEntityType bindingContext)
		{
			object obj;
			if (!this.LabeledExpressions.TryGetValue(label, ref obj))
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

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001EADC File Offset: 0x0001CCDC
		public IEdmLabeledExpression WrapLabeledElement(CsdlLabeledExpression labeledElement, IEdmEntityType bindingContext)
		{
			IEdmLabeledExpression edmLabeledExpression;
			if (!this.semanticsLabeledElements.TryGetValue(labeledElement, ref edmLabeledExpression))
			{
				edmLabeledExpression = new CsdlSemanticsLabeledExpression(labeledElement.Label, labeledElement.Element, bindingContext, this);
				this.semanticsLabeledElements[labeledElement] = edmLabeledExpression;
			}
			return edmLabeledExpression;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001EB1B File Offset: 0x0001CD1B
		internal string ReplaceAlias(string name)
		{
			return this.model.ReplaceAlias(name);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001EB29 File Offset: 0x0001CD29
		private static IEdmTerm FindTerm(IEdmModel model, string name)
		{
			return model.FindTerm(name);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001EB32 File Offset: 0x0001CD32
		private static IEdmEntityContainer FindEntityContainer(IEdmModel model, string name)
		{
			return model.FindEntityContainer(name);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001EB3C File Offset: 0x0001CD3C
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
				if (result.TryGetValue(label, ref obj))
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

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001ED20 File Offset: 0x0001CF20
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

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001ED70 File Offset: 0x0001CF70
		private IEdmLabeledExpression WrapLabeledElementList(List<CsdlLabeledExpression> labeledExpressions, IEdmEntityType bindingContext)
		{
			IEdmLabeledExpression edmLabeledExpression;
			if (!this.ambiguousLabeledExpressions.TryGetValue(labeledExpressions, ref edmLabeledExpression))
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

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private IEnumerable<IEdmTerm> ComputeTerms()
		{
			List<IEdmTerm> list = new List<IEdmTerm>();
			foreach (CsdlTerm csdlTerm in this.schema.Terms)
			{
				list.Add(new CsdlSemanticsTerm(this, csdlTerm));
			}
			return list;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001EE50 File Offset: 0x0001D050
		private IEnumerable<IEdmEntityContainer> ComputeEntityContainers()
		{
			List<IEdmEntityContainer> list = new List<IEdmEntityContainer>();
			foreach (CsdlEntityContainer csdlEntityContainer in this.schema.EntityContainers)
			{
				list.Add(new CsdlSemanticsEntityContainer(this, csdlEntityContainer));
			}
			return list;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
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

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001EF34 File Offset: 0x0001D134
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

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001F064 File Offset: 0x0001D264
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

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
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

		// Token: 0x0400066E RID: 1646
		private readonly CsdlSemanticsModel model;

		// Token: 0x0400066F RID: 1647
		private readonly CsdlSchema schema;

		// Token: 0x04000670 RID: 1648
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> typesCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>>();

		// Token: 0x04000671 RID: 1649
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> ComputeTypesFunc = (CsdlSemanticsSchema me) => me.ComputeTypes();

		// Token: 0x04000672 RID: 1650
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> operationsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>>();

		// Token: 0x04000673 RID: 1651
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> ComputeFunctionsFunc = (CsdlSemanticsSchema me) => me.ComputeOperations();

		// Token: 0x04000674 RID: 1652
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> entityContainersCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>>();

		// Token: 0x04000675 RID: 1653
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> ComputeEntityContainersFunc = (CsdlSemanticsSchema me) => me.ComputeEntityContainers();

		// Token: 0x04000676 RID: 1654
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmTerm>> termsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmTerm>>();

		// Token: 0x04000677 RID: 1655
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmTerm>> ComputeTermsFunc = (CsdlSemanticsSchema me) => me.ComputeTerms();

		// Token: 0x04000678 RID: 1656
		private readonly Cache<CsdlSemanticsSchema, Dictionary<string, object>> labeledExpressionsCache = new Cache<CsdlSemanticsSchema, Dictionary<string, object>>();

		// Token: 0x04000679 RID: 1657
		private static readonly Func<CsdlSemanticsSchema, Dictionary<string, object>> ComputeLabeledExpressionsFunc = (CsdlSemanticsSchema me) => me.ComputeLabeledExpressions();

		// Token: 0x0400067A RID: 1658
		private readonly Dictionary<CsdlLabeledExpression, IEdmLabeledExpression> semanticsLabeledElements = new Dictionary<CsdlLabeledExpression, IEdmLabeledExpression>();

		// Token: 0x0400067B RID: 1659
		private readonly Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression> ambiguousLabeledExpressions = new Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression>();
	}
}
