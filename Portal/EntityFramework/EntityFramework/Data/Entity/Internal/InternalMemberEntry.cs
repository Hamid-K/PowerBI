using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000107 RID: 263
	internal abstract class InternalMemberEntry
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x000317D1 File Offset: 0x0002F9D1
		protected InternalMemberEntry(InternalEntityEntry internalEntityEntry, MemberEntryMetadata memberMetadata)
		{
			this._internalEntityEntry = internalEntityEntry;
			this._memberMetadata = memberMetadata;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000317E7 File Offset: 0x0002F9E7
		public virtual string Name
		{
			get
			{
				return this._memberMetadata.MemberName;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060012D4 RID: 4820
		// (set) Token: 0x060012D5 RID: 4821
		public abstract object CurrentValue { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000317F4 File Offset: 0x0002F9F4
		public virtual InternalEntityEntry InternalEntityEntry
		{
			get
			{
				return this._internalEntityEntry;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000317FC File Offset: 0x0002F9FC
		public virtual MemberEntryMetadata EntryMetadata
		{
			get
			{
				return this._memberMetadata;
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00031804 File Offset: 0x0002FA04
		public virtual IEnumerable<DbValidationError> GetValidationErrors()
		{
			ValidationProvider validationProvider = this.InternalEntityEntry.InternalContext.ValidationProvider;
			PropertyValidator propertyValidator = validationProvider.GetPropertyValidator(this._internalEntityEntry, this);
			if (propertyValidator == null)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			return propertyValidator.Validate(validationProvider.GetEntityValidationContext(this._internalEntityEntry, null), this);
		}

		// Token: 0x060012D9 RID: 4825
		public abstract DbMemberEntry CreateDbMemberEntry();

		// Token: 0x060012DA RID: 4826
		public abstract DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>() where TEntity : class;

		// Token: 0x04000931 RID: 2353
		private readonly InternalEntityEntry _internalEntityEntry;

		// Token: 0x04000932 RID: 2354
		private readonly MemberEntryMetadata _memberMetadata;
	}
}
