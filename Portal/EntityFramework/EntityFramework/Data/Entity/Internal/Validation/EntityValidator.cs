using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000132 RID: 306
	internal class EntityValidator : TypeValidator
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x0003616B File Offset: 0x0003436B
		public EntityValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators)
			: base(propertyValidators, typeLevelValidators)
		{
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00036178 File Offset: 0x00034378
		public DbEntityValidationResult Validate(EntityValidationContext entityValidationContext)
		{
			IEnumerable<DbValidationError> enumerable = base.Validate(entityValidationContext, null);
			return new DbEntityValidationResult(entityValidationContext.InternalEntity, enumerable);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0003619C File Offset: 0x0003439C
		protected override void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors)
		{
			InternalEntityEntry internalEntity = entityValidationContext.InternalEntity;
			foreach (PropertyValidator propertyValidator in base.PropertyValidators)
			{
				validationErrors.AddRange(propertyValidator.Validate(entityValidationContext, internalEntity.Member(propertyValidator.PropertyName, null)));
			}
		}
	}
}
