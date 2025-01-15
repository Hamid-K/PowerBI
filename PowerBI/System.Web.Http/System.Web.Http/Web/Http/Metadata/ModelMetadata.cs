using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Validation;

namespace System.Web.Http.Metadata
{
	// Token: 0x02000047 RID: 71
	public class ModelMetadata
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00006048 File Offset: 0x00004248
		public ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			if (provider == null)
			{
				throw Error.ArgumentNull("provider");
			}
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			this.Provider = provider;
			this._containerType = containerType;
			this._modelAccessor = modelAccessor;
			this._modelType = modelType;
			this._propertyName = propertyName;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000060AA File Offset: 0x000042AA
		internal ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, EfficientTypePropertyKey<Type, string> cacheKey)
			: this(provider, containerType, modelAccessor, modelType, propertyName)
		{
			if (cacheKey == null)
			{
				throw Error.ArgumentNull("cacheKey");
			}
			this._cacheKey = cacheKey;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000060D0 File Offset: 0x000042D0
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				if (this._additionalValues == null)
				{
					this._additionalValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
				}
				return this._additionalValues;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000060F0 File Offset: 0x000042F0
		public Type ContainerType
		{
			get
			{
				return this._containerType;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000060F8 File Offset: 0x000042F8
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006100 File Offset: 0x00004300
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				return this._convertEmptyStringToNull;
			}
			set
			{
				this._convertEmptyStringToNull = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006109 File Offset: 0x00004309
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00006111 File Offset: 0x00004311
		public virtual string Description { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000611A File Offset: 0x0000431A
		public virtual bool IsComplexType
		{
			get
			{
				return !TypeHelper.HasStringConverter(this.ModelType);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000612A File Offset: 0x0000432A
		public bool IsNullableValueType
		{
			get
			{
				return TypeHelper.IsNullableValueType(this.ModelType);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006137 File Offset: 0x00004337
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000613F File Offset: 0x0000433F
		public virtual bool IsReadOnly { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006148 File Offset: 0x00004348
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00006170 File Offset: 0x00004370
		public object Model
		{
			get
			{
				if (this._modelAccessor != null)
				{
					this._model = this._modelAccessor();
					this._modelAccessor = null;
				}
				return this._model;
			}
			set
			{
				this._model = value;
				this._modelAccessor = null;
				this._properties = null;
				this._realModelType = null;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000618E File Offset: 0x0000438E
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006196 File Offset: 0x00004396
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = this.Provider.GetMetadataForProperties(this.Model, this.RealModelType);
				}
				return this._properties;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000061C3 File Offset: 0x000043C3
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000061CB File Offset: 0x000043CB
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000061D3 File Offset: 0x000043D3
		protected ModelMetadataProvider Provider { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000061DC File Offset: 0x000043DC
		internal Type RealModelType
		{
			get
			{
				if (this._realModelType == null)
				{
					this._realModelType = this.ModelType;
					if (this.Model != null && !TypeHelper.IsNullableValueType(this.ModelType))
					{
						this._realModelType = this.Model.GetType();
					}
				}
				return this._realModelType;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000622F File Offset: 0x0000442F
		internal EfficientTypePropertyKey<Type, string> CacheKey
		{
			get
			{
				if (this._cacheKey == null)
				{
					this._cacheKey = ModelMetadata.CreateCacheKey(this.ContainerType, this.ModelType, this.PropertyName);
				}
				return this._cacheKey;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000625C File Offset: 0x0000445C
		public virtual string GetDisplayName()
		{
			return this.PropertyName ?? this.ModelType.Name;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006274 File Offset: 0x00004474
		public virtual IEnumerable<ModelValidator> GetValidators(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			return validatorProviders.SelectMany((ModelValidatorProvider provider) => provider.GetValidators(this, validatorProviders));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000062BF File Offset: 0x000044BF
		private static EfficientTypePropertyKey<Type, string> CreateCacheKey(Type containerType, Type modelType, string propertyName)
		{
			return new EfficientTypePropertyKey<Type, string>(containerType ?? modelType, propertyName);
		}

		// Token: 0x04000062 RID: 98
		private readonly Type _containerType;

		// Token: 0x04000063 RID: 99
		private readonly Type _modelType;

		// Token: 0x04000064 RID: 100
		private readonly string _propertyName;

		// Token: 0x04000065 RID: 101
		private EfficientTypePropertyKey<Type, string> _cacheKey;

		// Token: 0x04000066 RID: 102
		private Dictionary<string, object> _additionalValues;

		// Token: 0x04000067 RID: 103
		private bool _convertEmptyStringToNull = true;

		// Token: 0x04000068 RID: 104
		private object _model;

		// Token: 0x04000069 RID: 105
		private Func<object> _modelAccessor;

		// Token: 0x0400006A RID: 106
		private IEnumerable<ModelMetadata> _properties;

		// Token: 0x0400006B RID: 107
		private Type _realModelType;
	}
}
