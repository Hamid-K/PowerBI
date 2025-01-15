using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000192 RID: 402
	internal class CsdlSemanticsVocabularyAnnotation : CsdlSemanticsElement, IEdmVocabularyAnnotation, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001D938 File Offset: 0x0001BB38
		public CsdlSemanticsVocabularyAnnotation(CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, CsdlAnnotation annotation, string qualifier)
			: base(annotation)
		{
			this.schema = schema;
			this.Annotation = annotation;
			this.qualifier = qualifier ?? annotation.Qualifier;
			this.targetContext = targetContext;
			this.annotationsContext = annotationsContext;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001D99E File Offset: 0x0001BB9E
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		public override CsdlElement Element
		{
			get
			{
				return this.Annotation;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001D9AE File Offset: 0x0001BBAE
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001D9B6 File Offset: 0x0001BBB6
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001D9C3 File Offset: 0x0001BBC3
		public IEdmTerm Term
		{
			get
			{
				return this.termCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTermFunc, null);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001D9D7 File Offset: 0x0001BBD7
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.targetCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTargetFunc, null);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0001D9EB File Offset: 0x0001BBEB
		public IEnumerable<EdmError> Errors
		{
			get
			{
				if (this.Term is IUnresolvedElement)
				{
					return this.Term.Errors();
				}
				if (this.Target is IUnresolvedElement)
				{
					return this.Target.Errors();
				}
				return Enumerable.Empty<EdmError>();
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public IEdmEntityType TargetBindingContext
		{
			get
			{
				IEdmVocabularyAnnotatable target = this.Target;
				IEdmEntityType edmEntityType = target as IEdmEntityType;
				if (edmEntityType == null)
				{
					IEdmNavigationSource edmNavigationSource = target as IEdmNavigationSource;
					if (edmNavigationSource != null)
					{
						edmEntityType = edmNavigationSource.EntityType();
					}
				}
				return edmEntityType;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0001DA54 File Offset: 0x0001BC54
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001DA68 File Offset: 0x0001BC68
		protected IEdmTerm ComputeTerm()
		{
			return this.Schema.FindTerm(this.Annotation.Term) ?? new UnresolvedVocabularyTerm(this.Schema.UnresolvedName(this.Annotation.Term));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001DA9F File Offset: 0x0001BC9F
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.Annotation.Expression, this.TargetBindingContext, this.Schema);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		private IEdmVocabularyAnnotatable ComputeTarget()
		{
			if (this.targetContext != null)
			{
				return this.targetContext;
			}
			string target = this.annotationsContext.Annotations.Target;
			string[] array = target.Split(new char[] { '/' });
			int num = array.Count<string>();
			if (num == 1)
			{
				string text = array[0];
				IEdmSchemaType edmSchemaType = this.schema.FindType(text);
				if (edmSchemaType != null)
				{
					return edmSchemaType;
				}
				IEdmTerm edmTerm = this.schema.FindTerm(text);
				if (edmTerm != null)
				{
					return edmTerm;
				}
				IEdmOperation edmOperation = this.FindParameterizedOperation(text, new Func<string, IEnumerable<IEdmOperation>>(this.Schema.FindOperations), new Func<IEnumerable<IEdmOperation>, IEdmOperation>(this.CreateAmbiguousOperation));
				if (edmOperation != null)
				{
					return edmOperation;
				}
				IEdmEntityContainer edmEntityContainer = this.schema.FindEntityContainer(text);
				if (edmEntityContainer != null)
				{
					return edmEntityContainer;
				}
				return new UnresolvedType(this.Schema.UnresolvedName(array[0]), base.Location);
			}
			else if (num == 2)
			{
				IEdmEntityContainer edmEntityContainer = this.schema.FindEntityContainer(array[0]);
				if (edmEntityContainer != null)
				{
					IEdmEntityContainerElement edmEntityContainerElement = edmEntityContainer.FindEntitySetExtended(array[1]);
					if (edmEntityContainerElement != null)
					{
						return edmEntityContainerElement;
					}
					IEdmOperationImport edmOperationImport = CsdlSemanticsVocabularyAnnotation.FindParameterizedOperationImport(array[1], new Func<string, IEnumerable<IEdmOperationImport>>(edmEntityContainer.FindOperationImportsExtended), new Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport>(this.CreateAmbiguousOperationImport));
					if (edmOperationImport != null)
					{
						return edmOperationImport;
					}
					return new UnresolvedEntitySet(array[1], edmEntityContainer, base.Location);
				}
				else
				{
					IEdmSchemaType edmSchemaType2 = this.schema.FindType(array[0]);
					if (edmSchemaType2 != null)
					{
						IEdmStructuredType edmStructuredType;
						IEdmEnumType edmEnumType;
						if ((edmStructuredType = edmSchemaType2 as IEdmStructuredType) != null)
						{
							IEdmProperty edmProperty = edmStructuredType.FindProperty(array[1]);
							if (edmProperty != null)
							{
								return edmProperty;
							}
							return new UnresolvedProperty(edmStructuredType, array[1], base.Location);
						}
						else if ((edmEnumType = edmSchemaType2 as IEdmEnumType) != null)
						{
							foreach (IEdmEnumMember edmEnumMember in edmEnumType.Members)
							{
								if (string.Equals(edmEnumMember.Name, array[1], StringComparison.OrdinalIgnoreCase))
								{
									return edmEnumMember;
								}
							}
							return new UnresolvedEnumMember(array[1], edmEnumType, base.Location);
						}
					}
					IEdmOperation edmOperation2 = this.FindParameterizedOperation(array[0], new Func<string, IEnumerable<IEdmOperation>>(this.Schema.FindOperations), new Func<IEnumerable<IEdmOperation>, IEdmOperation>(this.CreateAmbiguousOperation));
					if (edmOperation2 == null)
					{
						return new UnresolvedProperty(new UnresolvedEntityType(this.Schema.UnresolvedName(array[0]), base.Location), array[1], base.Location);
					}
					if (array[1] == "$ReturnType")
					{
						if (edmOperation2.ReturnType != null)
						{
							return edmOperation2.GetReturn();
						}
						return new UnresolvedReturn(edmOperation2, base.Location);
					}
					else
					{
						IEdmOperationParameter edmOperationParameter = edmOperation2.FindParameter(array[1]);
						if (edmOperationParameter != null)
						{
							return edmOperationParameter;
						}
						return new UnresolvedParameter(edmOperation2, array[1], base.Location);
					}
				}
			}
			else
			{
				if (num != 3)
				{
					return new BadElement(new EdmError[]
					{
						new EdmError(base.Location, EdmErrorCode.ImpossibleAnnotationsTarget, Strings.CsdlSemantics_ImpossibleAnnotationsTarget(target))
					});
				}
				string text2 = array[0];
				string text3 = array[1];
				string text4 = array[2];
				IEdmEntityContainer edmEntityContainer = this.Model.FindEntityContainer(text2);
				if (edmEntityContainer != null)
				{
					IEdmOperationImport edmOperationImport2 = CsdlSemanticsVocabularyAnnotation.FindParameterizedOperationImport(text3, new Func<string, IEnumerable<IEdmOperationImport>>(edmEntityContainer.FindOperationImportsExtended), new Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport>(this.CreateAmbiguousOperationImport));
					if (edmOperationImport2 != null)
					{
						if (text4 == "$ReturnType")
						{
							if (edmOperationImport2.Operation.ReturnType != null)
							{
								return edmOperationImport2.Operation.GetReturn();
							}
							return new UnresolvedReturn(edmOperationImport2.Operation, base.Location);
						}
						else
						{
							IEdmOperationParameter edmOperationParameter2 = edmOperationImport2.Operation.FindParameter(text4);
							if (edmOperationParameter2 != null)
							{
								return edmOperationParameter2;
							}
							return new UnresolvedParameter(edmOperationImport2.Operation, text4, base.Location);
						}
					}
				}
				string text5 = text2 + "/" + text3;
				UnresolvedOperation unresolvedOperation = new UnresolvedOperation(text5, Strings.Bad_UnresolvedOperation(text5), base.Location);
				if (text4 == "$ReturnType")
				{
					return new UnresolvedReturn(unresolvedOperation, base.Location);
				}
				return new UnresolvedParameter(unresolvedOperation, text4, base.Location);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001DE94 File Offset: 0x0001C094
		private static IEdmOperationImport FindParameterizedOperationImport(string parameterizedName, Func<string, IEnumerable<IEdmOperationImport>> findFunctions, Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport> ambiguityCreator)
		{
			IEnumerable<IEdmOperationImport> enumerable = findFunctions(parameterizedName);
			if (enumerable.Count<IEdmOperationImport>() == 0)
			{
				return null;
			}
			if (enumerable.Count<IEdmOperationImport>() == 1)
			{
				return enumerable.First<IEdmOperationImport>();
			}
			return ambiguityCreator(enumerable);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private IEdmOperation FindParameterizedOperation(string parameterizedName, Func<string, IEnumerable<IEdmOperation>> findFunctions, Func<IEnumerable<IEdmOperation>, IEdmOperation> ambiguityCreator)
		{
			int num = parameterizedName.IndexOf('(');
			int num2 = parameterizedName.LastIndexOf(')');
			if (num < 0)
			{
				return null;
			}
			string text = parameterizedName.Substring(0, num);
			string[] array = parameterizedName.Substring(num + 1, num2 - (num + 1)).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			IEnumerable<IEdmOperation> enumerable = this.FindParameterizedOperationFromList(findFunctions(text).Cast<IEdmOperation>(), array);
			if (enumerable.Count<IEdmOperation>() == 0)
			{
				return null;
			}
			if (enumerable.Count<IEdmOperation>() == 1)
			{
				return enumerable.First<IEdmOperation>();
			}
			return ambiguityCreator(enumerable);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001DF5C File Offset: 0x0001C15C
		private IEdmOperationImport CreateAmbiguousOperationImport(IEnumerable<IEdmOperationImport> operations)
		{
			IEnumerator<IEdmOperationImport> enumerator = operations.GetEnumerator();
			enumerator.MoveNext();
			IEdmOperationImport edmOperationImport = enumerator.Current;
			enumerator.MoveNext();
			IEdmOperationImport edmOperationImport2 = enumerator.Current;
			AmbiguousOperationImportBinding ambiguousOperationImportBinding = new AmbiguousOperationImportBinding(edmOperationImport, edmOperationImport2);
			while (enumerator.MoveNext())
			{
				IEdmOperationImport edmOperationImport3 = enumerator.Current;
				ambiguousOperationImportBinding.AddBinding(edmOperationImport3);
			}
			return ambiguousOperationImportBinding;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		private IEdmOperation CreateAmbiguousOperation(IEnumerable<IEdmOperation> operations)
		{
			IEnumerator<IEdmOperation> enumerator = operations.GetEnumerator();
			enumerator.MoveNext();
			IEdmOperation edmOperation = enumerator.Current;
			enumerator.MoveNext();
			IEdmOperation edmOperation2 = enumerator.Current;
			AmbiguousOperationBinding ambiguousOperationBinding = new AmbiguousOperationBinding(edmOperation, edmOperation2);
			while (enumerator.MoveNext())
			{
				IEdmOperation edmOperation3 = enumerator.Current;
				ambiguousOperationBinding.AddBinding(edmOperation3);
			}
			return ambiguousOperationBinding;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001DFFC File Offset: 0x0001C1FC
		private IEnumerable<IEdmOperation> FindParameterizedOperationFromList(IEnumerable<IEdmOperation> operations, string[] parameters)
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in operations)
			{
				if (edmOperation.Parameters.Count<IEdmOperationParameter>() == parameters.Count<string>())
				{
					bool flag = true;
					IEnumerator<string> enumerator2 = ((IEnumerable<string>)parameters).GetEnumerator();
					foreach (IEdmOperationParameter edmOperationParameter in edmOperation.Parameters)
					{
						enumerator2.MoveNext();
						string[] array = enumerator2.Current.Split(new char[] { '(', ')' });
						string text = array[0];
						if (!(text == "Collection"))
						{
							if (!(text == "Ref"))
							{
								flag = EdmCoreModel.Instance.FindDeclaredType(enumerator2.Current).IsEquivalentTo(edmOperationParameter.Type.Definition) || this.Schema.FindType(enumerator2.Current).IsEquivalentTo(edmOperationParameter.Type.Definition);
							}
							else
							{
								flag = edmOperationParameter.Type.IsEntityReference() && this.Schema.FindType(array[1]).IsEquivalentTo(edmOperationParameter.Type.AsEntityReference().EntityType());
							}
						}
						else
						{
							flag = edmOperationParameter.Type.IsCollection() && this.Schema.FindType(array[1]).IsEquivalentTo(edmOperationParameter.Type.AsCollection().ElementType().Definition);
						}
						if (!flag)
						{
							break;
						}
					}
					if (flag)
					{
						list.Add(edmOperation);
					}
				}
			}
			return list;
		}

		// Token: 0x0400068A RID: 1674
		protected readonly CsdlAnnotation Annotation;

		// Token: 0x0400068B RID: 1675
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x0400068C RID: 1676
		private readonly string qualifier;

		// Token: 0x0400068D RID: 1677
		private readonly IEdmVocabularyAnnotatable targetContext;

		// Token: 0x0400068E RID: 1678
		private readonly CsdlSemanticsAnnotations annotationsContext;

		// Token: 0x0400068F RID: 1679
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmExpression> valueCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmExpression>();

		// Token: 0x04000690 RID: 1680
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmExpression> ComputeValueFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeValue();

		// Token: 0x04000691 RID: 1681
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm> termCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm>();

		// Token: 0x04000692 RID: 1682
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmTerm> ComputeTermFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTerm();

		// Token: 0x04000693 RID: 1683
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> targetCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable>();

		// Token: 0x04000694 RID: 1684
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> ComputeTargetFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTarget();
	}
}
