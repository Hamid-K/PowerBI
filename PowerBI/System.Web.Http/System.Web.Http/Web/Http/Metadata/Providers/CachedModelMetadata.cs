using System;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x0200004B RID: 75
	public abstract class CachedModelMetadata<TPrototypeCache> : ModelMetadata
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00006738 File Offset: 0x00004938
		protected CachedModelMetadata(CachedModelMetadata<TPrototypeCache> prototype, Func<object> modelAccessor)
			: base(prototype.Provider, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName, prototype.CacheKey)
		{
			this.PrototypeCache = prototype.PrototypeCache;
			this._isComplexType = prototype.IsComplexType;
			this._isComplexTypeComputed = true;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006789 File Offset: 0x00004989
		protected CachedModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, TPrototypeCache prototypeCache)
			: base(provider, containerType, null, modelType, propertyName)
		{
			this.PrototypeCache = prototypeCache;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000679F File Offset: 0x0000499F
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000067C2 File Offset: 0x000049C2
		public sealed override bool ConvertEmptyStringToNull
		{
			get
			{
				if (!this._convertEmptyStringToNullComputed)
				{
					this._convertEmptyStringToNull = this.ComputeConvertEmptyStringToNull();
					this._convertEmptyStringToNullComputed = true;
				}
				return this._convertEmptyStringToNull;
			}
			set
			{
				this._convertEmptyStringToNull = value;
				this._convertEmptyStringToNullComputed = true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000067D2 File Offset: 0x000049D2
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000067F5 File Offset: 0x000049F5
		public sealed override string Description
		{
			get
			{
				if (!this._descriptionComputed)
				{
					this._description = this.ComputeDescription();
					this._descriptionComputed = true;
				}
				return this._description;
			}
			set
			{
				this._description = value;
				this._descriptionComputed = true;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006805 File Offset: 0x00004A05
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006828 File Offset: 0x00004A28
		public sealed override bool IsReadOnly
		{
			get
			{
				if (!this._isReadOnlyComputed)
				{
					this._isReadOnly = this.ComputeIsReadOnly();
					this._isReadOnlyComputed = true;
				}
				return this._isReadOnly;
			}
			set
			{
				this._isReadOnly = value;
				this._isReadOnlyComputed = true;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006838 File Offset: 0x00004A38
		public sealed override bool IsComplexType
		{
			get
			{
				if (!this._isComplexTypeComputed)
				{
					this._isComplexType = this.ComputeIsComplexType();
					this._isComplexTypeComputed = true;
				}
				return this._isComplexType;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000685B File Offset: 0x00004A5B
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00006863 File Offset: 0x00004A63
		protected TPrototypeCache PrototypeCache { get; set; }

		// Token: 0x06000219 RID: 537 RVA: 0x0000686C File Offset: 0x00004A6C
		protected virtual bool ComputeConvertEmptyStringToNull()
		{
			return base.ConvertEmptyStringToNull;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006874 File Offset: 0x00004A74
		protected virtual string ComputeDescription()
		{
			return base.Description;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000687C File Offset: 0x00004A7C
		protected virtual bool ComputeIsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006884 File Offset: 0x00004A84
		protected virtual bool ComputeIsComplexType()
		{
			return base.IsComplexType;
		}

		// Token: 0x04000075 RID: 117
		private bool _convertEmptyStringToNull;

		// Token: 0x04000076 RID: 118
		private string _description;

		// Token: 0x04000077 RID: 119
		private bool _isReadOnly;

		// Token: 0x04000078 RID: 120
		private bool _isComplexType;

		// Token: 0x04000079 RID: 121
		private bool _convertEmptyStringToNullComputed;

		// Token: 0x0400007A RID: 122
		private bool _descriptionComputed;

		// Token: 0x0400007B RID: 123
		private bool _isReadOnlyComputed;

		// Token: 0x0400007C RID: 124
		private bool _isComplexTypeComputed;
	}
}
