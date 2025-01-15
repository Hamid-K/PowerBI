using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000002 RID: 2
	internal abstract class CsdlElement
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CsdlElement(CsdlLocation location)
		{
			this.location = location;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		public virtual bool HasDirectValueAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E7 File Offset: 0x000002E7
		public bool HasVocabularyAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020EF File Offset: 0x000002EF
		public IEnumerable<CsdlDirectValueAnnotation> ImmediateValueAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F7 File Offset: 0x000002F7
		public IEnumerable<CsdlAnnotation> VocabularyAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020FF File Offset: 0x000002FF
		public EdmLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002107 File Offset: 0x00000307
		public void AddAnnotation(CsdlDirectValueAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public void AddAnnotation(CsdlAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002119 File Offset: 0x00000319
		private IEnumerable<T> GetAnnotations<T>() where T : class
		{
			if (this.annotations == null)
			{
				return Enumerable.Empty<T>();
			}
			return Enumerable.OfType<T>(this.annotations);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002134 File Offset: 0x00000334
		private void AddUntypedAnnotation(object annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<object>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002158 File Offset: 0x00000358
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

		// Token: 0x04000001 RID: 1
		protected List<object> annotations;

		// Token: 0x04000002 RID: 2
		protected EdmLocation location;
	}
}
