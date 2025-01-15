using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B8 RID: 184
	[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Name gets too long with Parameters")]
	public sealed class EntityChangedParams
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0001B360 File Offset: 0x00019560
		internal EntityChangedParams(DataServiceContext context, object entity, string propertyName, object propertyValue, string sourceEntitySet, string targetEntitySet)
		{
			this.context = context;
			this.entity = entity;
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
			this.sourceEntitySet = sourceEntitySet;
			this.targetEntitySet = targetEntitySet;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001B395 File Offset: 0x00019595
		public DataServiceContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001B39D File Offset: 0x0001959D
		public object Entity
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001B3A5 File Offset: 0x000195A5
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001B3AD File Offset: 0x000195AD
		public object PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001B3B5 File Offset: 0x000195B5
		public string SourceEntitySet
		{
			get
			{
				return this.sourceEntitySet;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001B3BD File Offset: 0x000195BD
		public string TargetEntitySet
		{
			get
			{
				return this.targetEntitySet;
			}
		}

		// Token: 0x040002B4 RID: 692
		private readonly DataServiceContext context;

		// Token: 0x040002B5 RID: 693
		private readonly object entity;

		// Token: 0x040002B6 RID: 694
		private readonly string propertyName;

		// Token: 0x040002B7 RID: 695
		private readonly object propertyValue;

		// Token: 0x040002B8 RID: 696
		private readonly string sourceEntitySet;

		// Token: 0x040002B9 RID: 697
		private readonly string targetEntitySet;
	}
}
