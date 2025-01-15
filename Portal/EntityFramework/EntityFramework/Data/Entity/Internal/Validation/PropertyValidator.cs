using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000135 RID: 309
	internal class PropertyValidator
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x00036794 File Offset: 0x00034994
		public PropertyValidator(string propertyName, IEnumerable<IValidator> propertyValidators)
		{
			this._propertyValidators = propertyValidators;
			this._propertyName = propertyName;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000367AA File Offset: 0x000349AA
		public IEnumerable<IValidator> PropertyAttributeValidators
		{
			get
			{
				return this._propertyValidators;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000367B2 File Offset: 0x000349B2
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000367BC File Offset: 0x000349BC
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			foreach (IValidator validator in this._propertyValidators)
			{
				list.AddRange(validator.Validate(entityValidationContext, property));
			}
			return list;
		}

		// Token: 0x040009BC RID: 2492
		private readonly IEnumerable<IValidator> _propertyValidators;

		// Token: 0x040009BD RID: 2493
		private readonly string _propertyName;
	}
}
