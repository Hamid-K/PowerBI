using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B9 RID: 185
	[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name gets too long with Parameters")]
	public sealed class EntityCollectionChangedParams
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001B3C8 File Offset: 0x000195C8
		internal EntityCollectionChangedParams(DataServiceContext context, object sourceEntity, string propertyName, string sourceEntitySet, ICollection collection, object targetEntity, string targetEntitySet, NotifyCollectionChangedAction action)
		{
			this.context = context;
			this.sourceEntity = sourceEntity;
			this.propertyName = propertyName;
			this.sourceEntitySet = sourceEntitySet;
			this.collection = collection;
			this.targetEntity = targetEntity;
			this.targetEntitySet = targetEntitySet;
			this.action = action;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001B418 File Offset: 0x00019618
		public DataServiceContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001B420 File Offset: 0x00019620
		public object SourceEntity
		{
			get
			{
				return this.sourceEntity;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001B428 File Offset: 0x00019628
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001B430 File Offset: 0x00019630
		public string SourceEntitySet
		{
			get
			{
				return this.sourceEntitySet;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001B438 File Offset: 0x00019638
		public object TargetEntity
		{
			get
			{
				return this.targetEntity;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001B440 File Offset: 0x00019640
		public string TargetEntitySet
		{
			get
			{
				return this.targetEntitySet;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001B448 File Offset: 0x00019648
		public ICollection Collection
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001B450 File Offset: 0x00019650
		public NotifyCollectionChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x040002BA RID: 698
		private readonly DataServiceContext context;

		// Token: 0x040002BB RID: 699
		private readonly object sourceEntity;

		// Token: 0x040002BC RID: 700
		private readonly string propertyName;

		// Token: 0x040002BD RID: 701
		private readonly string sourceEntitySet;

		// Token: 0x040002BE RID: 702
		private readonly ICollection collection;

		// Token: 0x040002BF RID: 703
		private readonly object targetEntity;

		// Token: 0x040002C0 RID: 704
		private readonly string targetEntitySet;

		// Token: 0x040002C1 RID: 705
		private readonly NotifyCollectionChangedAction action;
	}
}
