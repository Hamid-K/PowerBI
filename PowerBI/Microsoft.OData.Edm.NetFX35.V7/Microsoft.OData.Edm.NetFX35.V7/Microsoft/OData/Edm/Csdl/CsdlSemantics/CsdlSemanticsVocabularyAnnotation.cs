using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000183 RID: 387
	internal class CsdlSemanticsVocabularyAnnotation : CsdlSemanticsElement, IEdmVocabularyAnnotation, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0001B830 File Offset: 0x00019A30
		public CsdlSemanticsVocabularyAnnotation(CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, CsdlAnnotation annotation, string qualifier)
			: base(annotation)
		{
			this.schema = schema;
			this.Annotation = annotation;
			this.qualifier = qualifier ?? annotation.Qualifier;
			this.targetContext = targetContext;
			this.annotationsContext = annotationsContext;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001B896 File Offset: 0x00019A96
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001B89E File Offset: 0x00019A9E
		public override CsdlElement Element
		{
			get
			{
				return this.Annotation;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001B8A6 File Offset: 0x00019AA6
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001B8AE File Offset: 0x00019AAE
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001B8BB File Offset: 0x00019ABB
		public IEdmTerm Term
		{
			get
			{
				return this.termCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTermFunc, null);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001B8CF File Offset: 0x00019ACF
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.targetCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTargetFunc, null);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001B8E3 File Offset: 0x00019AE3
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

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001B91C File Offset: 0x00019B1C
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001B94C File Offset: 0x00019B4C
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001B960 File Offset: 0x00019B60
		protected IEdmTerm ComputeTerm()
		{
			return this.Schema.FindTerm(this.Annotation.Term) ?? new UnresolvedVocabularyTerm(this.Schema.UnresolvedName(this.Annotation.Term));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001B997 File Offset: 0x00019B97
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.Annotation.Expression, this.TargetBindingContext, this.Schema);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		private IEdmVocabularyAnnotatable ComputeTarget()
		{
			if (this.targetContext != null)
			{
				return this.targetContext;
			}
			string target = this.annotationsContext.Annotations.Target;
			string[] array = target.Split(new char[] { '/' });
			int num = Enumerable.Count<string>(array);
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
					IEdmStructuredType edmStructuredType = this.schema.FindType(array[0]) as IEdmStructuredType;
					if (edmStructuredType != null)
					{
						IEdmProperty edmProperty = edmStructuredType.FindProperty(array[1]);
						if (edmProperty != null)
						{
							return edmProperty;
						}
						return new UnresolvedProperty(edmStructuredType, array[1], base.Location);
					}
					else
					{
						IEdmOperation edmOperation2 = this.FindParameterizedOperation(array[0], new Func<string, IEnumerable<IEdmOperation>>(this.Schema.FindOperations), new Func<IEnumerable<IEdmOperation>, IEdmOperation>(this.CreateAmbiguousOperation));
						if (edmOperation2 == null)
						{
							return new UnresolvedProperty(new UnresolvedEntityType(this.Schema.UnresolvedName(array[0]), base.Location), array[1], base.Location);
						}
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
				if (num == 3)
				{
					string text2 = array[0];
					string text3 = array[1];
					string text4 = array[2];
					IEdmEntityContainer edmEntityContainer = this.Model.FindEntityContainer(text2);
					if (edmEntityContainer != null)
					{
						IEdmOperationImport edmOperationImport2 = CsdlSemanticsVocabularyAnnotation.FindParameterizedOperationImport(text3, new Func<string, IEnumerable<IEdmOperationImport>>(edmEntityContainer.FindOperationImportsExtended), new Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport>(this.CreateAmbiguousOperationImport));
						if (edmOperationImport2 != null)
						{
							IEdmOperationParameter edmOperationParameter2 = edmOperationImport2.Operation.FindParameter(text4);
							if (edmOperationParameter2 != null)
							{
								return edmOperationParameter2;
							}
							return new UnresolvedParameter(edmOperationImport2.Operation, text4, base.Location);
						}
					}
					string text5 = text2 + "/" + text3;
					UnresolvedOperation unresolvedOperation = new UnresolvedOperation(text5, Strings.Bad_UnresolvedOperation(text5), base.Location);
					return new UnresolvedParameter(unresolvedOperation, text4, base.Location);
				}
				return new BadElement(new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.ImpossibleAnnotationsTarget, Strings.CsdlSemantics_ImpossibleAnnotationsTarget(target))
				});
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001BC7C File Offset: 0x00019E7C
		private static IEdmOperationImport FindParameterizedOperationImport(string parameterizedName, Func<string, IEnumerable<IEdmOperationImport>> findFunctions, Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport> ambiguityCreator)
		{
			IEnumerable<IEdmOperationImport> enumerable = findFunctions.Invoke(parameterizedName);
			if (Enumerable.Count<IEdmOperationImport>(enumerable) == 0)
			{
				return null;
			}
			if (Enumerable.Count<IEdmOperationImport>(enumerable) == 1)
			{
				return Enumerable.First<IEdmOperationImport>(enumerable);
			}
			return ambiguityCreator.Invoke(enumerable);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		private IEdmOperation FindParameterizedOperation(string parameterizedName, Func<string, IEnumerable<IEdmOperation>> findFunctions, Func<IEnumerable<IEdmOperation>, IEdmOperation> ambiguityCreator)
		{
			int num = parameterizedName.IndexOf('(');
			int num2 = parameterizedName.LastIndexOf(')');
			if (num < 0)
			{
				return null;
			}
			string text = parameterizedName.Substring(0, num);
			string[] array = parameterizedName.Substring(num + 1, num2 - (num + 1)).Split(new string[] { ", " }, 1);
			IEnumerable<IEdmOperation> enumerable = this.FindParameterizedOperationFromList(Enumerable.Cast<IEdmOperation>(findFunctions.Invoke(text)), array);
			if (Enumerable.Count<IEdmOperation>(enumerable) == 0)
			{
				return null;
			}
			if (Enumerable.Count<IEdmOperation>(enumerable) == 1)
			{
				return Enumerable.First<IEdmOperation>(enumerable);
			}
			return ambiguityCreator.Invoke(enumerable);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001BD44 File Offset: 0x00019F44
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

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001BD94 File Offset: 0x00019F94
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

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		private IEnumerable<IEdmOperation> FindParameterizedOperationFromList(IEnumerable<IEdmOperation> operations, string[] parameters)
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in operations)
			{
				if (Enumerable.Count<IEdmOperationParameter>(edmOperation.Parameters) == Enumerable.Count<string>(parameters))
				{
					bool flag = true;
					IEnumerator<string> enumerator2 = parameters.GetEnumerator();
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

		// Token: 0x0400060E RID: 1550
		protected readonly CsdlAnnotation Annotation;

		// Token: 0x0400060F RID: 1551
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000610 RID: 1552
		private readonly string qualifier;

		// Token: 0x04000611 RID: 1553
		private readonly IEdmVocabularyAnnotatable targetContext;

		// Token: 0x04000612 RID: 1554
		private readonly CsdlSemanticsAnnotations annotationsContext;

		// Token: 0x04000613 RID: 1555
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmExpression> valueCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmExpression>();

		// Token: 0x04000614 RID: 1556
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmExpression> ComputeValueFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeValue();

		// Token: 0x04000615 RID: 1557
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm> termCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm>();

		// Token: 0x04000616 RID: 1558
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmTerm> ComputeTermFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTerm();

		// Token: 0x04000617 RID: 1559
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> targetCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable>();

		// Token: 0x04000618 RID: 1560
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> ComputeTargetFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTarget();
	}
}
