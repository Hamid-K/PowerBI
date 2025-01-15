using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.PrimitiveValueConverters;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001DC RID: 476
	internal class CsdlSemanticsSchema : CsdlSemanticsElement, IEdmCheckable
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x00019D00 File Offset: 0x00017F00
		public CsdlSemanticsSchema(CsdlSemanticsModel model, CsdlSchema schema)
			: base(schema)
		{
			this.model = model;
			this.schema = schema;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00019D6F File Offset: 0x00017F6F
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00019D77 File Offset: 0x00017F77
		public override CsdlElement Element
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00019D7F File Offset: 0x00017F7F
		public IEnumerable<IEdmSchemaType> Types
		{
			get
			{
				return this.typesCache.GetValue(this, CsdlSemanticsSchema.ComputeTypesFunc, null);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00019D93 File Offset: 0x00017F93
		public IEnumerable<IEdmOperation> Operations
		{
			get
			{
				return this.operationsCache.GetValue(this, CsdlSemanticsSchema.ComputeFunctionsFunc, null);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00019DA7 File Offset: 0x00017FA7
		public IEnumerable<IEdmValueTerm> ValueTerms
		{
			get
			{
				return this.valueTermsCache.GetValue(this, CsdlSemanticsSchema.ComputeValueTermsFunc, null);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00019DBB File Offset: 0x00017FBB
		public IEnumerable<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainersCache.GetValue(this, CsdlSemanticsSchema.ComputeEntityContainersFunc, null);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00019DCF File Offset: 0x00017FCF
		public string Namespace
		{
			get
			{
				return this.schema.Namespace;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00019DDC File Offset: 0x00017FDC
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00019DE3 File Offset: 0x00017FE3
		private Dictionary<string, object> LabeledExpressions
		{
			get
			{
				return this.labeledExpressionsCache.GetValue(this, CsdlSemanticsSchema.ComputeLabeledExpressionsFunc, null);
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00019DF7 File Offset: 0x00017FF7
		public IEnumerable<IEdmOperation> FindOperations(string name)
		{
			return this.FindSchemaElement<IEnumerable<IEdmOperation>>(name, new Func<CsdlSemanticsModel, string, IEnumerable<IEdmOperation>>(ExtensionMethods.FindOperationsInModelTree));
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00019E0C File Offset: 0x0001800C
		public IEdmSchemaType FindType(string name)
		{
			return this.FindSchemaElement<IEdmSchemaType>(name, new Func<CsdlSemanticsModel, string, IEdmSchemaType>(ExtensionMethods.FindTypeInModelTree));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00019E21 File Offset: 0x00018021
		public IEdmValueTerm FindValueTerm(string name)
		{
			return this.FindSchemaElement<IEdmValueTerm>(name, new Func<CsdlSemanticsModel, string, IEdmValueTerm>(CsdlSemanticsSchema.FindValueTerm));
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00019E36 File Offset: 0x00018036
		public IEdmEntityContainer FindEntityContainer(string name)
		{
			return this.FindSchemaElement<IEdmEntityContainer>(name, new Func<CsdlSemanticsModel, string, IEdmEntityContainer>(CsdlSemanticsSchema.FindEntityContainer));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00019E4C File Offset: 0x0001804C
		public T FindSchemaElement<T>(string name, Func<CsdlSemanticsModel, string, T> modelFinder)
		{
			string text = this.ReplaceAlias(name);
			if (text == null)
			{
				text = name;
			}
			return modelFinder.Invoke(this.model, text);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00019E73 File Offset: 0x00018073
		public string UnresolvedName(string qualifiedName)
		{
			if (qualifiedName == null)
			{
				return null;
			}
			return this.ReplaceAlias(qualifiedName) ?? qualifiedName;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00019E88 File Offset: 0x00018088
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

		// Token: 0x06000A05 RID: 2565 RVA: 0x00019EC8 File Offset: 0x000180C8
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x00019F07 File Offset: 0x00018107
		internal string ReplaceAlias(string name)
		{
			return this.model.ReplaceAlias(name);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00019F15 File Offset: 0x00018115
		private static IEdmValueTerm FindValueTerm(IEdmModel model, string name)
		{
			return model.FindValueTerm(name);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00019F1E File Offset: 0x0001811E
		private static IEdmEntityContainer FindEntityContainer(IEdmModel model, string name)
		{
			return model.FindEntityContainer(name);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00019F28 File Offset: 0x00018128
		private static void AddLabeledExpressions(CsdlExpressionBase expression, Dictionary<string, object> result)
		{
			if (expression == null)
			{
				return;
			}
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			switch (expressionKind)
			{
			case EdmExpressionKind.Record:
			{
				IL_011A:
				using (IEnumerator<CsdlPropertyValue> enumerator = ((CsdlRecordExpression)expression).PropertyValues.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CsdlPropertyValue csdlPropertyValue = enumerator.Current;
						CsdlSemanticsSchema.AddLabeledExpressions(csdlPropertyValue.Expression, result);
					}
					return;
				}
				goto IL_015B;
			}
			case EdmExpressionKind.Collection:
			{
				using (IEnumerator<CsdlExpressionBase> enumerator2 = ((CsdlCollectionExpression)expression).ElementValues.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CsdlExpressionBase csdlExpressionBase = enumerator2.Current;
						CsdlSemanticsSchema.AddLabeledExpressions(csdlExpressionBase, result);
					}
					return;
				}
				break;
			}
			default:
				switch (expressionKind)
				{
				case EdmExpressionKind.If:
					goto IL_015B;
				case EdmExpressionKind.Cast:
					CsdlSemanticsSchema.AddLabeledExpressions(((CsdlCastExpression)expression).Operand, result);
					return;
				case EdmExpressionKind.IsType:
					CsdlSemanticsSchema.AddLabeledExpressions(((CsdlIsTypeExpression)expression).Operand, result);
					return;
				case EdmExpressionKind.OperationApplication:
					break;
				case EdmExpressionKind.LabeledExpressionReference:
					return;
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
				break;
			}
			using (IEnumerator<CsdlExpressionBase> enumerator3 = ((CsdlApplyExpression)expression).Arguments.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					CsdlExpressionBase csdlExpressionBase2 = enumerator3.Current;
					CsdlSemanticsSchema.AddLabeledExpressions(csdlExpressionBase2, result);
				}
				return;
			}
			goto IL_011A;
			IL_015B:
			CsdlIfExpression csdlIfExpression = (CsdlIfExpression)expression;
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.Test, result);
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.IfTrue, result);
			CsdlSemanticsSchema.AddLabeledExpressions(csdlIfExpression.IfFalse, result);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001A10C File Offset: 0x0001830C
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

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001A15C File Offset: 0x0001835C
		private IEdmLabeledExpression WrapLabeledElementList(List<CsdlLabeledExpression> labeledExpressions, IEdmEntityType bindingContext)
		{
			IEdmLabeledExpression edmLabeledExpression;
			if (!this.ambiguousLabeledExpressions.TryGetValue(labeledExpressions, ref edmLabeledExpression))
			{
				foreach (CsdlLabeledExpression csdlLabeledExpression in labeledExpressions)
				{
					IEdmLabeledExpression edmLabeledExpression2 = this.WrapLabeledElement(csdlLabeledExpression, bindingContext);
					edmLabeledExpression = ((edmLabeledExpression == null) ? edmLabeledExpression2 : new AmbiguousLabeledExpressionBinding(edmLabeledExpression, edmLabeledExpression2));
				}
				this.ambiguousLabeledExpressions[labeledExpressions] = edmLabeledExpression;
			}
			return edmLabeledExpression;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001A1D8 File Offset: 0x000183D8
		private IEnumerable<IEdmValueTerm> ComputeValueTerms()
		{
			List<IEdmValueTerm> list = new List<IEdmValueTerm>();
			foreach (CsdlTerm csdlTerm in this.schema.Terms)
			{
				list.Add(new CsdlSemanticsValueTerm(this, csdlTerm));
			}
			return list;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001A238 File Offset: 0x00018438
		private IEnumerable<IEdmEntityContainer> ComputeEntityContainers()
		{
			List<IEdmEntityContainer> list = new List<IEdmEntityContainer>();
			foreach (CsdlEntityContainer csdlEntityContainer in this.schema.EntityContainers)
			{
				list.Add(new CsdlSemanticsEntityContainer(this, csdlEntityContainer));
			}
			return list;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001A298 File Offset: 0x00018498
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

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001A31C File Offset: 0x0001851C
		private IEnumerable<IEdmSchemaType> ComputeTypes()
		{
			List<IEdmSchemaType> list = new List<IEdmSchemaType>();
			foreach (CsdlTypeDefinition csdlTypeDefinition in this.schema.TypeDefinitions)
			{
				this.AttachDefaultPrimitiveValueConverter(csdlTypeDefinition);
				list.Add(new CsdlSemanticsTypeDefinitionDefinition(this, csdlTypeDefinition));
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

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001A448 File Offset: 0x00018648
		private void AttachDefaultPrimitiveValueConverter(CsdlTypeDefinition typeDefinition)
		{
			string name;
			if ((name = typeDefinition.Name) == null)
			{
				return;
			}
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
			this.Model.SetPrimitiveValueConverter(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { this.Namespace, typeDefinition.Name }), DefaultPrimitiveValueConverter.Instance);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001A4E8 File Offset: 0x000186E8
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
				foreach (CsdlProperty csdlProperty in csdlStructuredType.Properties)
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

		// Token: 0x040004D0 RID: 1232
		private readonly CsdlSemanticsModel model;

		// Token: 0x040004D1 RID: 1233
		private readonly CsdlSchema schema;

		// Token: 0x040004D2 RID: 1234
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> typesCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>>();

		// Token: 0x040004D3 RID: 1235
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmSchemaType>> ComputeTypesFunc = (CsdlSemanticsSchema me) => me.ComputeTypes();

		// Token: 0x040004D4 RID: 1236
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> operationsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmOperation>>();

		// Token: 0x040004D5 RID: 1237
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmOperation>> ComputeFunctionsFunc = (CsdlSemanticsSchema me) => me.ComputeOperations();

		// Token: 0x040004D6 RID: 1238
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> entityContainersCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>>();

		// Token: 0x040004D7 RID: 1239
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmEntityContainer>> ComputeEntityContainersFunc = (CsdlSemanticsSchema me) => me.ComputeEntityContainers();

		// Token: 0x040004D8 RID: 1240
		private readonly Cache<CsdlSemanticsSchema, IEnumerable<IEdmValueTerm>> valueTermsCache = new Cache<CsdlSemanticsSchema, IEnumerable<IEdmValueTerm>>();

		// Token: 0x040004D9 RID: 1241
		private static readonly Func<CsdlSemanticsSchema, IEnumerable<IEdmValueTerm>> ComputeValueTermsFunc = (CsdlSemanticsSchema me) => me.ComputeValueTerms();

		// Token: 0x040004DA RID: 1242
		private readonly Cache<CsdlSemanticsSchema, Dictionary<string, object>> labeledExpressionsCache = new Cache<CsdlSemanticsSchema, Dictionary<string, object>>();

		// Token: 0x040004DB RID: 1243
		private static readonly Func<CsdlSemanticsSchema, Dictionary<string, object>> ComputeLabeledExpressionsFunc = (CsdlSemanticsSchema me) => me.ComputeLabeledExpressions();

		// Token: 0x040004DC RID: 1244
		private readonly Dictionary<CsdlLabeledExpression, IEdmLabeledExpression> semanticsLabeledElements = new Dictionary<CsdlLabeledExpression, IEdmLabeledExpression>();

		// Token: 0x040004DD RID: 1245
		private readonly Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression> ambiguousLabeledExpressions = new Dictionary<List<CsdlLabeledExpression>, IEdmLabeledExpression>();
	}
}
