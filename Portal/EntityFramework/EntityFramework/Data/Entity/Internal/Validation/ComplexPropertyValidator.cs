using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200012F RID: 303
	internal class ComplexPropertyValidator : PropertyValidator
	{
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00036056 File Offset: 0x00034256
		public ComplexTypeValidator ComplexTypeValidator
		{
			get
			{
				return this._complexTypeValidator;
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0003605E File Offset: 0x0003425E
		public ComplexPropertyValidator(string propertyName, IEnumerable<IValidator> propertyValidators, ComplexTypeValidator complexTypeValidator)
			: base(propertyName, propertyValidators)
		{
			this._complexTypeValidator = complexTypeValidator;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00036070 File Offset: 0x00034270
		public override IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			list.AddRange(base.Validate(entityValidationContext, property));
			if (!list.Any<DbValidationError>() && property.CurrentValue != null && this._complexTypeValidator != null)
			{
				list.AddRange(this._complexTypeValidator.Validate(entityValidationContext, (InternalPropertyEntry)property));
			}
			return list;
		}

		// Token: 0x040009B8 RID: 2488
		private readonly ComplexTypeValidator _complexTypeValidator;
	}
}
