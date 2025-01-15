using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Utilities;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000139 RID: 313
	internal class ValidationProvider
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x00036AA6 File Offset: 0x00034CA6
		public ValidationProvider(EntityValidatorBuilder builder = null, AttributeProvider attributeProvider = null)
		{
			this._entityValidators = new Dictionary<Type, EntityValidator>();
			this._entityValidatorBuilder = builder ?? new EntityValidatorBuilder(attributeProvider ?? new AttributeProvider());
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00036AD4 File Offset: 0x00034CD4
		public virtual EntityValidator GetEntityValidator(InternalEntityEntry entityEntry)
		{
			Type entityType = entityEntry.EntityType;
			EntityValidator entityValidator = null;
			if (this._entityValidators.TryGetValue(entityType, out entityValidator))
			{
				return entityValidator;
			}
			entityValidator = this._entityValidatorBuilder.BuildEntityValidator(entityEntry);
			this._entityValidators[entityType] = entityValidator;
			return entityValidator;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00036B18 File Offset: 0x00034D18
		public virtual PropertyValidator GetPropertyValidator(InternalEntityEntry owningEntity, InternalMemberEntry property)
		{
			EntityValidator entityValidator = this.GetEntityValidator(owningEntity);
			if (entityValidator == null)
			{
				return null;
			}
			return this.GetValidatorForProperty(entityValidator, property);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00036B3C File Offset: 0x00034D3C
		protected virtual PropertyValidator GetValidatorForProperty(EntityValidator entityValidator, InternalMemberEntry memberEntry)
		{
			InternalNestedPropertyEntry internalNestedPropertyEntry = memberEntry as InternalNestedPropertyEntry;
			if (internalNestedPropertyEntry == null)
			{
				return entityValidator.GetPropertyValidator(memberEntry.Name);
			}
			ComplexPropertyValidator complexPropertyValidator = this.GetValidatorForProperty(entityValidator, internalNestedPropertyEntry.ParentPropertyEntry) as ComplexPropertyValidator;
			if (complexPropertyValidator == null || complexPropertyValidator.ComplexTypeValidator == null)
			{
				return null;
			}
			return complexPropertyValidator.ComplexTypeValidator.GetPropertyValidator(memberEntry.Name);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00036B91 File Offset: 0x00034D91
		public virtual EntityValidationContext GetEntityValidationContext(InternalEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return new EntityValidationContext(entityEntry, new ValidationContext(entityEntry.Entity, null, items));
		}

		// Token: 0x040009C3 RID: 2499
		private readonly Dictionary<Type, EntityValidator> _entityValidators;

		// Token: 0x040009C4 RID: 2500
		private readonly EntityValidatorBuilder _entityValidatorBuilder;
	}
}
