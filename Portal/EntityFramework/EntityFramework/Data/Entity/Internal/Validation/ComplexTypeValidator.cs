using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000130 RID: 304
	internal class ComplexTypeValidator : TypeValidator
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x000360C2 File Offset: 0x000342C2
		public ComplexTypeValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators)
			: base(propertyValidators, typeLevelValidators)
		{
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000360CC File Offset: 0x000342CC
		public new IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalPropertyEntry property)
		{
			return base.Validate(entityValidationContext, property);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000360D8 File Offset: 0x000342D8
		protected override void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors)
		{
			foreach (PropertyValidator propertyValidator in base.PropertyValidators)
			{
				InternalPropertyEntry internalPropertyEntry = parentProperty.Property(propertyValidator.PropertyName, null, false);
				validationErrors.AddRange(propertyValidator.Validate(entityValidationContext, internalPropertyEntry));
			}
		}
	}
}
