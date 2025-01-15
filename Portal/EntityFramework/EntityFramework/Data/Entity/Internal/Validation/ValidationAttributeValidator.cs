using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000138 RID: 312
	internal class ValidationAttributeValidator : IValidator
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x0003698C File Offset: 0x00034B8C
		public ValidationAttributeValidator(ValidationAttribute validationAttribute, DisplayAttribute displayAttribute)
		{
			this._validationAttribute = validationAttribute;
			this._displayAttribute = displayAttribute;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000369A4 File Offset: 0x00034BA4
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			if (!this.AttributeApplicable(entityValidationContext, property))
			{
				return Enumerable.Empty<DbValidationError>();
			}
			ValidationContext externalValidationContext = entityValidationContext.ExternalValidationContext;
			externalValidationContext.SetDisplayName(property, this._displayAttribute);
			object obj = ((property == null) ? entityValidationContext.InternalEntity.Entity : property.CurrentValue);
			ValidationResult validationResult = null;
			try
			{
				validationResult = this._validationAttribute.GetValidationResult(obj, externalValidationContext);
			}
			catch (Exception ex)
			{
				throw new DbUnexpectedValidationException(Strings.DbUnexpectedValidationException_ValidationAttribute(externalValidationContext.DisplayName, this._validationAttribute.GetType()), ex);
			}
			if (validationResult == ValidationResult.Success)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			return DbHelpers.SplitValidationResults(externalValidationContext.MemberName, new ValidationResult[] { validationResult });
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00036A50 File Offset: 0x00034C50
		protected virtual bool AttributeApplicable(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			InternalNavigationEntry internalNavigationEntry = property as InternalNavigationEntry;
			return !(this._validationAttribute is RequiredAttribute) || property == null || property.InternalEntityEntry == null || property.InternalEntityEntry.State == EntityState.Added || property.InternalEntityEntry.State == EntityState.Detached || internalNavigationEntry == null || internalNavigationEntry.IsLoaded;
		}

		// Token: 0x040009C1 RID: 2497
		private readonly DisplayAttribute _displayAttribute;

		// Token: 0x040009C2 RID: 2498
		private readonly ValidationAttribute _validationAttribute;
	}
}
