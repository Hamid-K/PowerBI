using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000175 RID: 373
	internal abstract class CsdlSemanticsElement : IEdmElement, IEdmLocatable
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x0001AE18 File Offset: 0x00019018
		protected CsdlSemanticsElement(CsdlElement element)
		{
			if (element != null)
			{
				if (element.HasDirectValueAnnotations)
				{
					this.directValueAnnotationsCache = new Cache<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>>();
				}
				if (element.HasVocabularyAnnotations)
				{
					this.inlineVocabularyAnnotationsCache = new Cache<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>>();
				}
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009D7 RID: 2519
		public abstract CsdlSemanticsModel Model { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009D8 RID: 2520
		public abstract CsdlElement Element { get; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001AE49 File Offset: 0x00019049
		public IEnumerable<IEdmVocabularyAnnotation> InlineVocabularyAnnotations
		{
			get
			{
				if (this.inlineVocabularyAnnotationsCache == null)
				{
					return CsdlSemanticsElement.emptyVocabularyAnnotations;
				}
				return this.inlineVocabularyAnnotationsCache.GetValue(this, CsdlSemanticsElement.ComputeInlineVocabularyAnnotationsFunc, null);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001AE6B File Offset: 0x0001906B
		public EdmLocation Location
		{
			get
			{
				if (this.Element == null || this.Element.Location == null)
				{
					return new ObjectLocation(this);
				}
				return this.Element.Location;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0001AE94 File Offset: 0x00019094
		public IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations
		{
			get
			{
				if (this.directValueAnnotationsCache == null)
				{
					return null;
				}
				return this.directValueAnnotationsCache.GetValue(this, CsdlSemanticsElement.ComputeDirectValueAnnotationsFunc, null);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001AEB2 File Offset: 0x000190B2
		protected static List<T> AllocateAndAdd<T>(List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
			return list;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001AEC6 File Offset: 0x000190C6
		protected static List<T> AllocateAndAdd<T>(List<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.AddRange(items);
			return list;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001AEDA File Offset: 0x000190DA
		protected virtual IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, null);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001AEEC File Offset: 0x000190EC
		protected IEnumerable<IEdmDirectValueAnnotation> ComputeDirectValueAnnotations()
		{
			if (this.Element == null)
			{
				return null;
			}
			List<CsdlDirectValueAnnotation> list = Enumerable.ToList<CsdlDirectValueAnnotation>(this.Element.ImmediateValueAnnotations);
			CsdlElementWithDocumentation csdlElementWithDocumentation = this.Element as CsdlElementWithDocumentation;
			CsdlDocumentation csdlDocumentation = ((csdlElementWithDocumentation != null) ? csdlElementWithDocumentation.Documentation : null);
			if (csdlDocumentation != null || Enumerable.FirstOrDefault<CsdlDirectValueAnnotation>(list) != null)
			{
				List<IEdmDirectValueAnnotation> list2 = new List<IEdmDirectValueAnnotation>();
				foreach (CsdlDirectValueAnnotation csdlDirectValueAnnotation in list)
				{
					list2.Add(new CsdlSemanticsDirectValueAnnotation(csdlDirectValueAnnotation, this.Model));
				}
				if (csdlDocumentation != null)
				{
					list2.Add(new CsdlSemanticsDocumentation(csdlDocumentation, this.Model));
				}
				return list2;
			}
			return null;
		}

		// Token: 0x040005DF RID: 1503
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> inlineVocabularyAnnotationsCache;

		// Token: 0x040005E0 RID: 1504
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> ComputeInlineVocabularyAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeInlineVocabularyAnnotations();

		// Token: 0x040005E1 RID: 1505
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> directValueAnnotationsCache;

		// Token: 0x040005E2 RID: 1506
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> ComputeDirectValueAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeDirectValueAnnotations();

		// Token: 0x040005E3 RID: 1507
		private static readonly IEnumerable<IEdmVocabularyAnnotation> emptyVocabularyAnnotations = Enumerable.Empty<IEdmVocabularyAnnotation>();
	}
}
