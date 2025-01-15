using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019D RID: 413
	internal abstract class CsdlSemanticsOperation : CsdlSemanticsElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x0001EF88 File Offset: 0x0001D188
		public CsdlSemanticsOperation(CsdlSemanticsSchema context, CsdlOperation operation)
			: base(operation)
		{
			this.Context = context;
			this.operation = operation;
			CsdlSemanticsSchema context2 = this.Context;
			string text = ((context2 != null) ? context2.Namespace : null);
			CsdlOperation csdlOperation = this.operation;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlOperation != null) ? csdlOperation.Name : null);
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B6A RID: 2922
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0001EFFA File Offset: 0x0001D1FA
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001F007 File Offset: 0x0001D207
		public string Name
		{
			get
			{
				return this.operation.Name;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001F014 File Offset: 0x0001D214
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001F01C File Offset: 0x0001D21C
		public override CsdlElement Element
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0001F024 File Offset: 0x0001D224
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001F031 File Offset: 0x0001D231
		public bool IsBound
		{
			get
			{
				return this.operation.IsBound;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0001F03E File Offset: 0x0001D23E
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.entitySetPathCache.GetValue(this, CsdlSemanticsOperation.ComputeEntitySetPathFunc, null);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0001F052 File Offset: 0x0001D252
		public IEdmTypeReference ReturnType
		{
			get
			{
				if (this.operation.Return == null)
				{
					return null;
				}
				return this.Return.Type;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001F06E File Offset: 0x0001D26E
		public IEdmOperationReturn Return
		{
			get
			{
				return this.returnCache.GetValue(this, CsdlSemanticsOperation.ComputeReturnFunc, null);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001F082 File Offset: 0x0001D282
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parametersCache.GetValue(this, CsdlSemanticsOperation.ComputeParametersFunc, null);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001F096 File Offset: 0x0001D296
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0001F09E File Offset: 0x0001D29E
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		public IEdmOperationParameter FindParameter(string name)
		{
			return this.Parameters.SingleOrDefault((IEdmOperationParameter p) => p.Name == name);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001F0D9 File Offset: 0x0001D2D9
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001F0ED File Offset: 0x0001D2ED
		private IEdmPathExpression ComputeEntitySetPath()
		{
			if (this.operation.EntitySetPath != null)
			{
				return new CsdlSemanticsOperation.OperationPathExpression(this.operation.EntitySetPath)
				{
					Location = base.Location
				};
			}
			return null;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001F11A File Offset: 0x0001D31A
		private IEdmOperationReturn ComputeReturn()
		{
			if (this.operation.Return == null)
			{
				return null;
			}
			return new CsdlSemanticsOperationReturn(this, this.operation.Return);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001F13C File Offset: 0x0001D33C
		private IEnumerable<IEdmOperationParameter> ComputeParameters()
		{
			List<IEdmOperationParameter> list = new List<IEdmOperationParameter>();
			foreach (CsdlOperationParameter csdlOperationParameter in this.operation.Parameters)
			{
				if (csdlOperationParameter.IsOptional)
				{
					list.Add(new CsdlSemanticsOptionalParameter(this, csdlOperationParameter, csdlOperationParameter.DefaultValue));
				}
				else
				{
					list.Add(new CsdlSemanticsOperationParameter(this, csdlOperationParameter));
				}
			}
			string text = this.Namespace + "." + this.Name;
			string text2 = CsdlSemanticsOperation.ParameterizedTargetName(list);
			List<IEdmOperationParameter> list2 = new List<IEdmOperationParameter>(list.Count);
			foreach (IEdmOperationParameter edmOperationParameter in list)
			{
				string text3 = text + text2 + "/" + edmOperationParameter.Name;
				string text4 = text + "/" + edmOperationParameter.Name;
				string text5;
				if (this.TryGetOptionalParameterOutOfLineAnnotation(text3, text4, out text5))
				{
					CsdlSemanticsOperationParameter csdlSemanticsOperationParameter = (CsdlSemanticsOperationParameter)edmOperationParameter;
					list2.Add(new CsdlSemanticsOptionalParameter(this, (CsdlOperationParameter)csdlSemanticsOperationParameter.Element, text5));
				}
				else
				{
					list2.Add(edmOperationParameter);
				}
			}
			return list2;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001F288 File Offset: 0x0001D488
		private bool TryGetOptionalParameterOutOfLineAnnotation(string fullTargetName, string targetName, out string defaultValue)
		{
			defaultValue = null;
			bool flag = false;
			List<CsdlSemanticsAnnotations> list;
			bool flag2 = this.Model.OutOfLineAnnotations.TryGetValue(fullTargetName, out list) || this.Model.OutOfLineAnnotations.TryGetValue(targetName, out list);
			if (flag2)
			{
				foreach (CsdlSemanticsAnnotations csdlSemanticsAnnotations in list)
				{
					CsdlAnnotation csdlAnnotation = csdlSemanticsAnnotations.Annotations.Annotations.FirstOrDefault((CsdlAnnotation a) => a.Term == CoreVocabularyModel.OptionalParameterTerm.ShortQualifiedName() || a.Term == CoreVocabularyModel.OptionalParameterTerm.FullName());
					if (csdlAnnotation != null)
					{
						flag = true;
						CsdlRecordExpression csdlRecordExpression = csdlAnnotation.Expression as CsdlRecordExpression;
						if (csdlRecordExpression == null)
						{
							break;
						}
						using (IEnumerator<CsdlPropertyValue> enumerator2 = csdlRecordExpression.PropertyValues.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								CsdlPropertyValue csdlPropertyValue = enumerator2.Current;
								if (csdlPropertyValue.Property == "DefaultValue")
								{
									CsdlConstantExpression csdlConstantExpression = csdlPropertyValue.Expression as CsdlConstantExpression;
									if (csdlConstantExpression != null)
									{
										defaultValue = csdlConstantExpression.Value;
									}
								}
							}
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		internal static string ParameterizedTargetName(IList<IEdmOperationParameter> parameters)
		{
			int num = 0;
			int num2 = parameters.Count<IEdmOperationParameter>();
			StringBuilder stringBuilder = new StringBuilder("(");
			foreach (IEdmOperationParameter edmOperationParameter in parameters)
			{
				string text;
				if (edmOperationParameter.Type == null)
				{
					text = "Edm.Untyped";
				}
				else if (edmOperationParameter.Type.IsCollection())
				{
					text = "Collection(" + edmOperationParameter.Type.AsCollection().ElementType().FullName() + ")";
				}
				else if (edmOperationParameter.Type.IsEntityReference())
				{
					text = "Ref(" + edmOperationParameter.Type.AsEntityReference().EntityType().FullName() + ")";
				}
				else
				{
					text = edmOperationParameter.Type.FullName();
				}
				stringBuilder.Append(text);
				num++;
				if (num < num2)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x040006BF RID: 1727
		private readonly string fullName;

		// Token: 0x040006C0 RID: 1728
		private readonly CsdlOperation operation;

		// Token: 0x040006C1 RID: 1729
		private readonly Cache<CsdlSemanticsOperation, IEdmPathExpression> entitySetPathCache = new Cache<CsdlSemanticsOperation, IEdmPathExpression>();

		// Token: 0x040006C2 RID: 1730
		private static readonly Func<CsdlSemanticsOperation, IEdmPathExpression> ComputeEntitySetPathFunc = (CsdlSemanticsOperation me) => me.ComputeEntitySetPath();

		// Token: 0x040006C3 RID: 1731
		private readonly Cache<CsdlSemanticsOperation, IEdmOperationReturn> returnCache = new Cache<CsdlSemanticsOperation, IEdmOperationReturn>();

		// Token: 0x040006C4 RID: 1732
		private static readonly Func<CsdlSemanticsOperation, IEdmOperationReturn> ComputeReturnFunc = (CsdlSemanticsOperation me) => me.ComputeReturn();

		// Token: 0x040006C5 RID: 1733
		private readonly Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> parametersCache = new Cache<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>>();

		// Token: 0x040006C6 RID: 1734
		private static readonly Func<CsdlSemanticsOperation, IEnumerable<IEdmOperationParameter>> ComputeParametersFunc = (CsdlSemanticsOperation me) => me.ComputeParameters();

		// Token: 0x020002E3 RID: 739
		private sealed class OperationPathExpression : EdmPathExpression, IEdmLocatable
		{
			// Token: 0x06001121 RID: 4385 RVA: 0x0001206F File Offset: 0x0001026F
			internal OperationPathExpression(string path)
				: base(path)
			{
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06001122 RID: 4386 RVA: 0x0002DC95 File Offset: 0x0002BE95
			// (set) Token: 0x06001123 RID: 4387 RVA: 0x0002DC9D File Offset: 0x0002BE9D
			public EdmLocation Location { get; set; }
		}
	}
}
