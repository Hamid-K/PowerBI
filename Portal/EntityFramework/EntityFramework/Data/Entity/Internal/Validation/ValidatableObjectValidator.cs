using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000137 RID: 311
	internal class ValidatableObjectValidator : IValidator
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x000368DD File Offset: 0x00034ADD
		public ValidatableObjectValidator(DisplayAttribute displayAttribute)
		{
			this._displayAttribute = displayAttribute;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x000368EC File Offset: 0x00034AEC
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			if (property != null && property.CurrentValue == null)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			ValidationContext externalValidationContext = entityValidationContext.ExternalValidationContext;
			externalValidationContext.SetDisplayName(property, this._displayAttribute);
			IValidatableObject validatableObject = (IValidatableObject)((property == null) ? entityValidationContext.InternalEntity.Entity : property.CurrentValue);
			IEnumerable<ValidationResult> enumerable = null;
			try
			{
				enumerable = validatableObject.Validate(externalValidationContext);
			}
			catch (Exception ex)
			{
				throw new DbUnexpectedValidationException(Strings.DbUnexpectedValidationException_IValidatableObject(externalValidationContext.DisplayName, ObjectContextTypeCache.GetObjectType(validatableObject.GetType())), ex);
			}
			return DbHelpers.SplitValidationResults(externalValidationContext.MemberName, enumerable ?? Enumerable.Empty<ValidationResult>());
		}

		// Token: 0x040009C0 RID: 2496
		private readonly DisplayAttribute _displayAttribute;
	}
}
