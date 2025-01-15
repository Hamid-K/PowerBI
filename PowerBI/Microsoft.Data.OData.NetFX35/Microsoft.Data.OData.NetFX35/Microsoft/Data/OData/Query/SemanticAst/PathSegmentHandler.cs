using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000044 RID: 68
	public abstract class PathSegmentHandler
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000763C File Offset: 0x0000583C
		public virtual void Handle(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007643 File Offset: 0x00005843
		public virtual void Handle(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000764A File Offset: 0x0000584A
		public virtual void Handle(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007651 File Offset: 0x00005851
		public virtual void Handle(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007658 File Offset: 0x00005858
		public virtual void Handle(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000765F File Offset: 0x0000585F
		public virtual void Handle(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007666 File Offset: 0x00005866
		public virtual void Handle(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000766D File Offset: 0x0000586D
		public virtual void Handle(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007674 File Offset: 0x00005874
		public virtual void Handle(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000767B File Offset: 0x0000587B
		public virtual void Handle(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007682 File Offset: 0x00005882
		public virtual void Handle(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007689 File Offset: 0x00005889
		public virtual void Handle(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007690 File Offset: 0x00005890
		public virtual void Handle(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
