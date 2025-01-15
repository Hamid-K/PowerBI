using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x020001BE RID: 446
	public abstract class PathSegmentTranslator<T>
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x0003A31E File Offset: 0x0003851E
		public virtual T Translate(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003A325 File Offset: 0x00038525
		public virtual T Translate(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003A32C File Offset: 0x0003852C
		public virtual T Translate(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003A333 File Offset: 0x00038533
		public virtual T Translate(SingletonSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003A33A File Offset: 0x0003853A
		public virtual T Translate(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0003A341 File Offset: 0x00038541
		public virtual T Translate(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003A348 File Offset: 0x00038548
		public virtual T Translate(OperationImportSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003A34F File Offset: 0x0003854F
		public virtual T Translate(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003A356 File Offset: 0x00038556
		public virtual T Translate(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0003A35D File Offset: 0x0003855D
		public virtual T Translate(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003A364 File Offset: 0x00038564
		public virtual T Translate(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003A36B File Offset: 0x0003856B
		public virtual T Translate(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0003A372 File Offset: 0x00038572
		public virtual T Translate(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0003A379 File Offset: 0x00038579
		public virtual T Translate(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003A380 File Offset: 0x00038580
		public virtual T Translate(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003A387 File Offset: 0x00038587
		public virtual T Translate(PathTemplateSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
