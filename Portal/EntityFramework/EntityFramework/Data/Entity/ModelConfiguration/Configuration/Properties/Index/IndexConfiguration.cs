using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Index
{
	// Token: 0x0200020D RID: 525
	internal class IndexConfiguration : PropertyConfiguration
	{
		// Token: 0x06001BD0 RID: 7120 RVA: 0x0004D278 File Offset: 0x0004B478
		public IndexConfiguration()
		{
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0004D280 File Offset: 0x0004B480
		internal IndexConfiguration(IndexConfiguration source)
		{
			this._isUnique = source._isUnique;
			this._isClustered = source._isClustered;
			this._name = source._name;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0004D2AC File Offset: 0x0004B4AC
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0004D2B4 File Offset: 0x0004B4B4
		public bool? IsUnique
		{
			get
			{
				return this._isUnique;
			}
			set
			{
				Check.NotNull<bool>(value, "value");
				this._isUnique = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0004D2C9 File Offset: 0x0004B4C9
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0004D2D1 File Offset: 0x0004B4D1
		public bool? IsClustered
		{
			get
			{
				return this._isClustered;
			}
			set
			{
				Check.NotNull<bool>(value, "value");
				this._isClustered = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0004D2E6 File Offset: 0x0004B4E6
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x0004D2EE File Offset: 0x0004B4EE
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotNull<string>(value, "value");
				this._name = value;
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0004D303 File Offset: 0x0004B503
		internal virtual IndexConfiguration Clone()
		{
			return new IndexConfiguration(this);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0004D30B File Offset: 0x0004B50B
		internal void Configure(EdmProperty edmProperty, int indexOrder)
		{
			IndexConfiguration.AddAnnotationWithMerge(edmProperty, new IndexAnnotation(new IndexAttribute(this._name, indexOrder, this._isClustered, this._isUnique)));
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0004D330 File Offset: 0x0004B530
		internal void Configure(EntityType entityType)
		{
			IndexConfiguration.AddAnnotationWithMerge(entityType, new IndexAnnotation(new IndexAttribute(this._name, this._isClustered, this._isUnique)));
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0004D354 File Offset: 0x0004B554
		private static void AddAnnotationWithMerge(MetadataItem metadataItem, IndexAnnotation newAnnotation)
		{
			object annotation = metadataItem.Annotations.GetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index");
			if (annotation != null)
			{
				newAnnotation = (IndexAnnotation)((IndexAnnotation)annotation).MergeWith(newAnnotation);
			}
			metadataItem.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index", newAnnotation);
		}

		// Token: 0x04000AD7 RID: 2775
		private bool? _isUnique;

		// Token: 0x04000AD8 RID: 2776
		private bool? _isClustered;

		// Token: 0x04000AD9 RID: 2777
		private string _name;
	}
}
