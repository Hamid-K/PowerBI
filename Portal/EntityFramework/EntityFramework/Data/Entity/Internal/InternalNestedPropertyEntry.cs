using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000109 RID: 265
	internal class InternalNestedPropertyEntry : InternalPropertyEntry
	{
		// Token: 0x060012E7 RID: 4839 RVA: 0x00031A12 File Offset: 0x0002FC12
		public InternalNestedPropertyEntry(InternalPropertyEntry parentPropertyEntry, PropertyEntryMetadata propertyMetadata)
			: base(parentPropertyEntry.InternalEntityEntry, propertyMetadata)
		{
			this._parentPropertyEntry = parentPropertyEntry;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00031A28 File Offset: 0x0002FC28
		public override InternalPropertyEntry ParentPropertyEntry
		{
			get
			{
				return this._parentPropertyEntry;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00031A30 File Offset: 0x0002FC30
		public override InternalPropertyValues ParentCurrentValues
		{
			get
			{
				InternalPropertyValues parentCurrentValues = this._parentPropertyEntry.ParentCurrentValues;
				return (InternalPropertyValues)((parentCurrentValues == null) ? null : parentCurrentValues[this._parentPropertyEntry.Name]);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00031A68 File Offset: 0x0002FC68
		public override InternalPropertyValues ParentOriginalValues
		{
			get
			{
				InternalPropertyValues parentOriginalValues = this._parentPropertyEntry.ParentOriginalValues;
				return (InternalPropertyValues)((parentOriginalValues == null) ? null : parentOriginalValues[this._parentPropertyEntry.Name]);
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00031AA0 File Offset: 0x0002FCA0
		protected override Func<object, object> CreateGetter()
		{
			Func<object, object> parentGetter = this._parentPropertyEntry.Getter;
			if (parentGetter == null)
			{
				return null;
			}
			Func<object, object> getter;
			if (!DbHelpers.GetPropertyGetters(base.EntryMetadata.DeclaringType).TryGetValue(this.Name, out getter))
			{
				return null;
			}
			return delegate(object o)
			{
				object obj = parentGetter(o);
				if (obj != null)
				{
					return getter(obj);
				}
				return null;
			};
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00031B00 File Offset: 0x0002FD00
		protected override Action<object, object> CreateSetter()
		{
			Func<object, object> parentGetter = this._parentPropertyEntry.Getter;
			if (parentGetter == null)
			{
				return null;
			}
			Action<object, object> setter;
			if (!DbHelpers.GetPropertySetters(base.EntryMetadata.DeclaringType).TryGetValue(this.Name, out setter))
			{
				return null;
			}
			return delegate(object o, object v)
			{
				if (parentGetter(o) == null)
				{
					throw Error.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(this.Name, this.ParentPropertyEntry.Name);
				}
				setter(parentGetter(o), v);
			};
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00031B66 File Offset: 0x0002FD66
		public override bool EntityPropertyIsModified()
		{
			return this._parentPropertyEntry.EntityPropertyIsModified();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00031B73 File Offset: 0x0002FD73
		public override void SetEntityPropertyModified()
		{
			this._parentPropertyEntry.SetEntityPropertyModified();
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00031B80 File Offset: 0x0002FD80
		public override void RejectEntityPropertyChanges()
		{
			this.CurrentValue = this.OriginalValue;
			this.UpdateComplexPropertyState();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00031B94 File Offset: 0x0002FD94
		public override void UpdateComplexPropertyState()
		{
			this._parentPropertyEntry.UpdateComplexPropertyState();
		}

		// Token: 0x04000938 RID: 2360
		private readonly InternalPropertyEntry _parentPropertyEntry;
	}
}
