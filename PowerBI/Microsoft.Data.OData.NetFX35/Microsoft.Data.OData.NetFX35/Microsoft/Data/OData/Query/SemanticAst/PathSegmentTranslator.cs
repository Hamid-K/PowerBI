using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200001F RID: 31
	public abstract class PathSegmentTranslator<T>
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004011 File Offset: 0x00002211
		public virtual T Translate(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004018 File Offset: 0x00002218
		public virtual T Translate(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000401F File Offset: 0x0000221F
		public virtual T Translate(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004026 File Offset: 0x00002226
		public virtual T Translate(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000402D File Offset: 0x0000222D
		public virtual T Translate(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004034 File Offset: 0x00002234
		public virtual T Translate(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000403B File Offset: 0x0000223B
		public virtual T Translate(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004042 File Offset: 0x00002242
		public virtual T Translate(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004049 File Offset: 0x00002249
		public virtual T Translate(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004050 File Offset: 0x00002250
		public virtual T Translate(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004057 File Offset: 0x00002257
		public virtual T Translate(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000405E File Offset: 0x0000225E
		public virtual T Translate(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004065 File Offset: 0x00002265
		public virtual T Translate(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
