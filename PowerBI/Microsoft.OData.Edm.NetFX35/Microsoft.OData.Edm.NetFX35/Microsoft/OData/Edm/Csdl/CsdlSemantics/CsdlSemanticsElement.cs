using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000007 RID: 7
	internal abstract class CsdlSemanticsElement : IEdmElement, IEdmLocatable
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021E5 File Offset: 0x000003E5
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19
		public abstract CsdlSemanticsModel Model { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20
		public abstract CsdlElement Element { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002216 File Offset: 0x00000416
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002238 File Offset: 0x00000438
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002261 File Offset: 0x00000461
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

		// Token: 0x06000018 RID: 24 RVA: 0x0000227F File Offset: 0x0000047F
		protected static List<T> AllocateAndAdd<T>(List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
			return list;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002293 File Offset: 0x00000493
		protected static List<T> AllocateAndAdd<T>(List<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.AddRange(items);
			return list;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A7 File Offset: 0x000004A7
		protected virtual IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022B8 File Offset: 0x000004B8
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

		// Token: 0x04000004 RID: 4
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> inlineVocabularyAnnotationsCache;

		// Token: 0x04000005 RID: 5
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> ComputeInlineVocabularyAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeInlineVocabularyAnnotations();

		// Token: 0x04000006 RID: 6
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> directValueAnnotationsCache;

		// Token: 0x04000007 RID: 7
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> ComputeDirectValueAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeDirectValueAnnotations();

		// Token: 0x04000008 RID: 8
		private static readonly IEnumerable<IEdmVocabularyAnnotation> emptyVocabularyAnnotations = Enumerable.Empty<IEdmVocabularyAnnotation>();
	}
}
