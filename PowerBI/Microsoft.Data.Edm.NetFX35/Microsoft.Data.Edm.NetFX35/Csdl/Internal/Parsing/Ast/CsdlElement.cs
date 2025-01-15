using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000008 RID: 8
	internal abstract class CsdlElement
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000028E9 File Offset: 0x00000AE9
		public CsdlElement(CsdlLocation location)
		{
			this.location = location;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028F8 File Offset: 0x00000AF8
		public virtual bool HasDirectValueAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002900 File Offset: 0x00000B00
		public bool HasVocabularyAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlVocabularyAnnotationBase>();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002908 File Offset: 0x00000B08
		public IEnumerable<CsdlDirectValueAnnotation> ImmediateValueAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002910 File Offset: 0x00000B10
		public IEnumerable<CsdlVocabularyAnnotationBase> VocabularyAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlVocabularyAnnotationBase>();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002918 File Offset: 0x00000B18
		public EdmLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002920 File Offset: 0x00000B20
		public void AddAnnotation(CsdlDirectValueAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002929 File Offset: 0x00000B29
		public void AddAnnotation(CsdlVocabularyAnnotationBase annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002932 File Offset: 0x00000B32
		private IEnumerable<T> GetAnnotations<T>() where T : class
		{
			if (this.annotations == null)
			{
				return Enumerable.Empty<T>();
			}
			return Enumerable.OfType<T>(this.annotations);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000294D File Offset: 0x00000B4D
		private void AddUntypedAnnotation(object annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<object>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002970 File Offset: 0x00000B70
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

		// Token: 0x0400000B RID: 11
		protected List<object> annotations;

		// Token: 0x0400000C RID: 12
		protected EdmLocation location;
	}
}
