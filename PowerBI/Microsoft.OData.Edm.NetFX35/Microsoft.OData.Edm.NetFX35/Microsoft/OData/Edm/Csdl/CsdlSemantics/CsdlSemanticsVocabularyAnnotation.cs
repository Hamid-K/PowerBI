using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000C8 RID: 200
	internal abstract class CsdlSemanticsVocabularyAnnotation : CsdlSemanticsElement, IEdmVocabularyAnnotation, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000359 RID: 857 RVA: 0x000077D4 File Offset: 0x000059D4
		protected CsdlSemanticsVocabularyAnnotation(CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, CsdlAnnotation annotation, string qualifier)
			: base(annotation)
		{
			this.schema = schema;
			this.Annotation = annotation;
			this.qualifier = qualifier ?? annotation.Qualifier;
			this.targetContext = targetContext;
			this.annotationsContext = annotationsContext;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000782F File Offset: 0x00005A2F
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00007837 File Offset: 0x00005A37
		public override CsdlElement Element
		{
			get
			{
				return this.Annotation;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000783F File Offset: 0x00005A3F
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00007847 File Offset: 0x00005A47
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00007854 File Offset: 0x00005A54
		public IEdmTerm Term
		{
			get
			{
				return this.termCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTermFunc, null);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00007868 File Offset: 0x00005A68
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.targetCache.GetValue(this, CsdlSemanticsVocabularyAnnotation.ComputeTargetFunc, null);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000787C File Offset: 0x00005A7C
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000078B8 File Offset: 0x00005AB8
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

		// Token: 0x06000362 RID: 866
		protected abstract IEdmTerm ComputeTerm();

		// Token: 0x06000363 RID: 867 RVA: 0x000078E8 File Offset: 0x00005AE8
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
				IEdmValueTerm edmValueTerm = this.schema.FindValueTerm(text);
				if (edmValueTerm != null)
				{
					return edmValueTerm;
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
					IEdmOperationImport edmOperationImport = this.FindParameterizedOperationImport(array[1], new Func<string, IEnumerable<IEdmOperationImport>>(edmEntityContainer.FindOperationImportsExtended), new Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport>(this.CreateAmbiguousOperationImport));
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
						IEdmOperationImport edmOperationImport2 = this.FindParameterizedOperationImport(text3, new Func<string, IEnumerable<IEdmOperationImport>>(edmEntityContainer.FindOperationImportsExtended), new Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport>(this.CreateAmbiguousOperationImport));
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

		// Token: 0x06000364 RID: 868 RVA: 0x00007BB8 File Offset: 0x00005DB8
		private IEdmOperationImport FindParameterizedOperationImport(string parameterizedName, Func<string, IEnumerable<IEdmOperationImport>> findFunctions, Func<IEnumerable<IEdmOperationImport>, IEdmOperationImport> ambiguityCreator)
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

		// Token: 0x06000365 RID: 869 RVA: 0x00007BF0 File Offset: 0x00005DF0
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

		// Token: 0x06000366 RID: 870 RVA: 0x00007C84 File Offset: 0x00005E84
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

		// Token: 0x06000367 RID: 871 RVA: 0x00007CD4 File Offset: 0x00005ED4
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

		// Token: 0x06000368 RID: 872 RVA: 0x00007D24 File Offset: 0x00005F24
		private IEnumerable<IEdmOperation> FindParameterizedOperationFromList(IEnumerable<IEdmOperation> operations, string[] parameters)
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			foreach (IEdmOperation edmOperation in operations)
			{
				if (Enumerable.Count<IEdmOperationParameter>(edmOperation.Parameters) == Enumerable.Count<string>(parameters))
				{
					bool flag = true;
					IEnumerator<string> enumerator2 = ((IEnumerable<string>)parameters).GetEnumerator();
					foreach (IEdmOperationParameter edmOperationParameter in edmOperation.Parameters)
					{
						enumerator2.MoveNext();
						string[] array = enumerator2.Current.Split(new char[] { '(', ')' });
						string text;
						if ((text = array[0]) == null)
						{
							goto IL_0128;
						}
						if (!(text == "Collection"))
						{
							if (!(text == "Ref"))
							{
								goto IL_0128;
							}
							flag = edmOperationParameter.Type.IsEntityReference() && this.Schema.FindType(array[1]).IsEquivalentTo(edmOperationParameter.Type.AsEntityReference().EntityType());
						}
						else
						{
							flag = edmOperationParameter.Type.IsCollection() && this.Schema.FindType(array[1]).IsEquivalentTo(edmOperationParameter.Type.AsCollection().ElementType().Definition);
						}
						IL_0171:
						if (flag)
						{
							continue;
						}
						break;
						IL_0128:
						flag = EdmCoreModel.Instance.FindDeclaredType(enumerator2.Current).IsEquivalentTo(edmOperationParameter.Type.Definition) || this.Schema.FindType(enumerator2.Current).IsEquivalentTo(edmOperationParameter.Type.Definition);
						goto IL_0171;
					}
					if (flag)
					{
						list.Add(edmOperation);
					}
				}
			}
			return list;
		}

		// Token: 0x04000168 RID: 360
		protected readonly CsdlAnnotation Annotation;

		// Token: 0x04000169 RID: 361
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x0400016A RID: 362
		private readonly string qualifier;

		// Token: 0x0400016B RID: 363
		private readonly IEdmVocabularyAnnotatable targetContext;

		// Token: 0x0400016C RID: 364
		private readonly CsdlSemanticsAnnotations annotationsContext;

		// Token: 0x0400016D RID: 365
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm> termCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmTerm>();

		// Token: 0x0400016E RID: 366
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmTerm> ComputeTermFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTerm();

		// Token: 0x0400016F RID: 367
		private readonly Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> targetCache = new Cache<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable>();

		// Token: 0x04000170 RID: 368
		private static readonly Func<CsdlSemanticsVocabularyAnnotation, IEdmVocabularyAnnotatable> ComputeTargetFunc = (CsdlSemanticsVocabularyAnnotation me) => me.ComputeTarget();
	}
}
