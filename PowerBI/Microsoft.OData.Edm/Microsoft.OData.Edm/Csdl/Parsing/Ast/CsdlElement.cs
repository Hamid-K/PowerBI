using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001ED RID: 493
	internal abstract class CsdlElement
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x00025EB3 File Offset: 0x000240B3
		public CsdlElement(CsdlLocation location)
		{
			this.location = location;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00025EC2 File Offset: 0x000240C2
		public virtual bool HasDirectValueAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00025ECA File Offset: 0x000240CA
		public bool HasVocabularyAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00025ED2 File Offset: 0x000240D2
		public IEnumerable<CsdlDirectValueAnnotation> ImmediateValueAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00025EDA File Offset: 0x000240DA
		public IEnumerable<CsdlAnnotation> VocabularyAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00025EE2 File Offset: 0x000240E2
		public EdmLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00025EEA File Offset: 0x000240EA
		public void AddAnnotation(CsdlDirectValueAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00025EEA File Offset: 0x000240EA
		public void AddAnnotation(CsdlAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00025EF3 File Offset: 0x000240F3
		private IEnumerable<T> GetAnnotations<T>() where T : class
		{
			if (this.annotations == null)
			{
				return Enumerable.Empty<T>();
			}
			return this.annotations.OfType<T>();
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00025F0E File Offset: 0x0002410E
		private void AddUntypedAnnotation(object annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<object>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00025F30 File Offset: 0x00024130
		private bool HasAnnotations<T>()
		{
			if (this.annotations == null)
			{
				return false;
			}
			foreach (object obj in this.annotations)
			{
				if (obj is T)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400076F RID: 1903
		protected List<object> annotations;

		// Token: 0x04000770 RID: 1904
		protected EdmLocation location;
	}
}
