using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000136 RID: 310
	internal abstract class TypeValidator
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x00036818 File Offset: 0x00034A18
		public TypeValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators)
		{
			this._typeLevelValidators = typeLevelValidators;
			this._propertyValidators = propertyValidators;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0003682E File Offset: 0x00034A2E
		public IEnumerable<IValidator> TypeLevelValidators
		{
			get
			{
				return this._typeLevelValidators;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00036836 File Offset: 0x00034A36
		public IEnumerable<PropertyValidator> PropertyValidators
		{
			get
			{
				return this._propertyValidators;
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00036840 File Offset: 0x00034A40
		protected IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalPropertyEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			this.ValidateProperties(entityValidationContext, property, list);
			if (!list.Any<DbValidationError>())
			{
				foreach (IValidator validator in this._typeLevelValidators)
				{
					list.AddRange(validator.Validate(entityValidationContext, property));
				}
			}
			return list;
		}

		// Token: 0x060014CC RID: 5324
		protected abstract void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors);

		// Token: 0x060014CD RID: 5325 RVA: 0x000368AC File Offset: 0x00034AAC
		public PropertyValidator GetPropertyValidator(string name)
		{
			return this._propertyValidators.SingleOrDefault((PropertyValidator v) => v.PropertyName == name);
		}

		// Token: 0x040009BE RID: 2494
		private readonly IEnumerable<IValidator> _typeLevelValidators;

		// Token: 0x040009BF RID: 2495
		private readonly IEnumerable<PropertyValidator> _propertyValidators;
	}
}
