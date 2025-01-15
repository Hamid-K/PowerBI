using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000184 RID: 388
	internal abstract class CsdlSemanticsElement : IEdmElement, IEdmLocatable
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x0001CF60 File Offset: 0x0001B160
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A93 RID: 2707
		public abstract CsdlSemanticsModel Model { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A94 RID: 2708
		public abstract CsdlElement Element { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001CF91 File Offset: 0x0001B191
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0001CFB3 File Offset: 0x0001B1B3
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001CFDC File Offset: 0x0001B1DC
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

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		protected static List<T> AllocateAndAdd<T>(List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
			return list;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001D00E File Offset: 0x0001B20E
		protected static List<T> AllocateAndAdd<T>(List<T> list, IEnumerable<T> items)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.AddRange(items);
			return list;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001D022 File Offset: 0x0001B222
		protected virtual IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, null);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001D034 File Offset: 0x0001B234
		protected IEnumerable<IEdmDirectValueAnnotation> ComputeDirectValueAnnotations()
		{
			if (this.Element == null)
			{
				return null;
			}
			List<CsdlDirectValueAnnotation> list = this.Element.ImmediateValueAnnotations.ToList<CsdlDirectValueAnnotation>();
			if (list.FirstOrDefault<CsdlDirectValueAnnotation>() != null)
			{
				List<IEdmDirectValueAnnotation> list2 = new List<IEdmDirectValueAnnotation>();
				foreach (CsdlDirectValueAnnotation csdlDirectValueAnnotation in list)
				{
					list2.Add(new CsdlSemanticsDirectValueAnnotation(csdlDirectValueAnnotation, this.Model));
				}
				return list2;
			}
			return null;
		}

		// Token: 0x0400065B RID: 1627
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> inlineVocabularyAnnotationsCache;

		// Token: 0x0400065C RID: 1628
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmVocabularyAnnotation>> ComputeInlineVocabularyAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeInlineVocabularyAnnotations();

		// Token: 0x0400065D RID: 1629
		private readonly Cache<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> directValueAnnotationsCache;

		// Token: 0x0400065E RID: 1630
		private static readonly Func<CsdlSemanticsElement, IEnumerable<IEdmDirectValueAnnotation>> ComputeDirectValueAnnotationsFunc = (CsdlSemanticsElement me) => me.ComputeDirectValueAnnotations();

		// Token: 0x0400065F RID: 1631
		private static readonly IEnumerable<IEdmVocabularyAnnotation> emptyVocabularyAnnotations = Enumerable.Empty<IEdmVocabularyAnnotation>();
	}
}
