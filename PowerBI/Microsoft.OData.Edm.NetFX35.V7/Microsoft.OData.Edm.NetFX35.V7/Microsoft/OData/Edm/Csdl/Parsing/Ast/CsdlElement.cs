using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001DF RID: 479
	internal abstract class CsdlElement
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x00023D27 File Offset: 0x00021F27
		public CsdlElement(CsdlLocation location)
		{
			this.location = location;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00023D36 File Offset: 0x00021F36
		public virtual bool HasDirectValueAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00023D3E File Offset: 0x00021F3E
		public bool HasVocabularyAnnotations
		{
			get
			{
				return this.HasAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00023D46 File Offset: 0x00021F46
		public IEnumerable<CsdlDirectValueAnnotation> ImmediateValueAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlDirectValueAnnotation>();
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00023D4E File Offset: 0x00021F4E
		public IEnumerable<CsdlAnnotation> VocabularyAnnotations
		{
			get
			{
				return this.GetAnnotations<CsdlAnnotation>();
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00023D56 File Offset: 0x00021F56
		public EdmLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00023D5E File Offset: 0x00021F5E
		public void AddAnnotation(CsdlDirectValueAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00023D5E File Offset: 0x00021F5E
		public void AddAnnotation(CsdlAnnotation annotation)
		{
			this.AddUntypedAnnotation(annotation);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00023D67 File Offset: 0x00021F67
		private IEnumerable<T> GetAnnotations<T>() where T : class
		{
			if (this.annotations == null)
			{
				return Enumerable.Empty<T>();
			}
			return Enumerable.OfType<T>(this.annotations);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00023D82 File Offset: 0x00021F82
		private void AddUntypedAnnotation(object annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<object>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00023DA4 File Offset: 0x00021FA4
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

		// Token: 0x040006F8 RID: 1784
		protected List<object> annotations;

		// Token: 0x040006F9 RID: 1785
		protected EdmLocation location;
	}
}
