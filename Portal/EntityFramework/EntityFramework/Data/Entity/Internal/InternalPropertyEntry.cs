using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200010A RID: 266
	internal abstract class InternalPropertyEntry : InternalMemberEntry
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x00031BA1 File Offset: 0x0002FDA1
		protected InternalPropertyEntry(InternalEntityEntry internalEntityEntry, PropertyEntryMetadata propertyMetadata)
			: base(internalEntityEntry, propertyMetadata)
		{
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060012F2 RID: 4850
		public abstract InternalPropertyEntry ParentPropertyEntry { get; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060012F3 RID: 4851
		public abstract InternalPropertyValues ParentCurrentValues { get; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060012F4 RID: 4852
		public abstract InternalPropertyValues ParentOriginalValues { get; }

		// Token: 0x060012F5 RID: 4853
		protected abstract Func<object, object> CreateGetter();

		// Token: 0x060012F6 RID: 4854
		protected abstract Action<object, object> CreateSetter();

		// Token: 0x060012F7 RID: 4855
		public abstract bool EntityPropertyIsModified();

		// Token: 0x060012F8 RID: 4856
		public abstract void SetEntityPropertyModified();

		// Token: 0x060012F9 RID: 4857
		public abstract void RejectEntityPropertyChanges();

		// Token: 0x060012FA RID: 4858
		public abstract void UpdateComplexPropertyState();

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00031BAB File Offset: 0x0002FDAB
		public Func<object, object> Getter
		{
			get
			{
				if (!this._getterIsCached)
				{
					this._getter = this.CreateGetter();
					this._getterIsCached = true;
				}
				return this._getter;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00031BCE File Offset: 0x0002FDCE
		public Action<object, object> Setter
		{
			get
			{
				if (!this._setterIsCached)
				{
					this._setter = this.CreateSetter();
					this._setterIsCached = true;
				}
				return this._setter;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00031C38 File Offset: 0x0002FE38
		public virtual object OriginalValue
		{
			get
			{
				this.ValidateNotDetachedAndInModel("OriginalValue");
				InternalPropertyValues parentOriginalValues = this.ParentOriginalValues;
				object obj = ((parentOriginalValues == null) ? null : parentOriginalValues[this.Name]);
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj = internalPropertyValues.ToObject();
				}
				return obj;
			}
			set
			{
				this.ValidateNotDetachedAndInModel("OriginalValue");
				this.CheckNotSettingComplexPropertyToNull(value);
				InternalPropertyValues parentOriginalValues = this.ParentOriginalValues;
				if (parentOriginalValues == null)
				{
					throw Error.DbPropertyValues_CannotSetPropertyOnNullOriginalValue(this.Name, this.ParentPropertyEntry.Name);
				}
				this.SetPropertyValueUsingValues(parentOriginalValues, value);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00031C80 File Offset: 0x0002FE80
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00031D10 File Offset: 0x0002FF10
		public override object CurrentValue
		{
			get
			{
				if (this.Getter != null)
				{
					return this.Getter(this.InternalEntityEntry.Entity);
				}
				if (!this.InternalEntityEntry.IsDetached && this.EntryMetadata.IsMapped)
				{
					InternalPropertyValues parentCurrentValues = this.ParentCurrentValues;
					object obj = ((parentCurrentValues == null) ? null : parentCurrentValues[this.Name]);
					InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
					if (internalPropertyValues != null)
					{
						obj = internalPropertyValues.ToObject();
					}
					return obj;
				}
				throw Error.DbPropertyEntry_CannotGetCurrentValue(this.Name, base.EntryMetadata.DeclaringType.Name);
			}
			set
			{
				this.CheckNotSettingComplexPropertyToNull(value);
				if (!this.EntryMetadata.IsMapped || this.InternalEntityEntry.IsDetached || this.InternalEntityEntry.State == EntityState.Deleted)
				{
					if (!this.SetCurrentValueOnClrObject(value))
					{
						throw Error.DbPropertyEntry_CannotSetCurrentValue(this.Name, base.EntryMetadata.DeclaringType.Name);
					}
				}
				else
				{
					InternalPropertyValues parentCurrentValues = this.ParentCurrentValues;
					if (parentCurrentValues == null)
					{
						throw Error.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(this.Name, this.ParentPropertyEntry.Name);
					}
					this.SetPropertyValueUsingValues(parentCurrentValues, value);
					if (this.EntryMetadata.IsComplex)
					{
						this.SetCurrentValueOnClrObject(value);
					}
				}
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00031DAF File Offset: 0x0002FFAF
		private void CheckNotSettingComplexPropertyToNull(object value)
		{
			if (value == null && this.EntryMetadata.IsComplex)
			{
				throw Error.DbPropertyValues_ComplexObjectCannotBeNull(this.Name, base.EntryMetadata.DeclaringType.Name);
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00031DE0 File Offset: 0x0002FFE0
		private bool SetCurrentValueOnClrObject(object value)
		{
			if (this.Setter == null)
			{
				return false;
			}
			if (this.Getter == null || !DbHelpers.PropertyValuesEqual(value, this.Getter(this.InternalEntityEntry.Entity)))
			{
				this.Setter(this.InternalEntityEntry.Entity, value);
				if (this.EntryMetadata.IsMapped && (this.InternalEntityEntry.State == EntityState.Modified || this.InternalEntityEntry.State == EntityState.Unchanged))
				{
					this.IsModified = true;
				}
			}
			return true;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00031E68 File Offset: 0x00030068
		private void SetPropertyValueUsingValues(InternalPropertyValues internalValues, object value)
		{
			InternalPropertyValues internalPropertyValues = internalValues[this.Name] as InternalPropertyValues;
			if (internalPropertyValues == null)
			{
				internalValues[this.Name] = value;
				return;
			}
			if (!internalPropertyValues.ObjectType.IsAssignableFrom(value.GetType()))
			{
				throw Error.DbPropertyValues_AttemptToSetValuesFromWrongObject(value.GetType().Name, internalPropertyValues.ObjectType.Name);
			}
			internalPropertyValues.SetValues(value);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00031ECE File Offset: 0x000300CE
		public virtual InternalPropertyEntry Property(string property, Type requestedType = null, bool requireComplex = false)
		{
			return this.InternalEntityEntry.Property(this, property, requestedType ?? typeof(object), requireComplex);
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00031EED File Offset: 0x000300ED
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00031F11 File Offset: 0x00030111
		public virtual bool IsModified
		{
			get
			{
				return !this.InternalEntityEntry.IsDetached && this.EntryMetadata.IsMapped && this.EntityPropertyIsModified();
			}
			set
			{
				this.ValidateNotDetachedAndInModel("IsModified");
				if (value)
				{
					this.SetEntityPropertyModified();
					return;
				}
				if (this.IsModified)
				{
					this.RejectEntityPropertyChanges();
				}
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00031F38 File Offset: 0x00030138
		private void ValidateNotDetachedAndInModel(string method)
		{
			if (!this.EntryMetadata.IsMapped)
			{
				throw Error.DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(method, base.EntryMetadata.MemberName, this.InternalEntityEntry.EntityType.Name);
			}
			if (this.InternalEntityEntry.IsDetached)
			{
				throw Error.DbPropertyEntry_NotSupportedForDetached(method, base.EntryMetadata.MemberName, this.InternalEntityEntry.EntityType.Name);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00031FA3 File Offset: 0x000301A3
		public new PropertyEntryMetadata EntryMetadata
		{
			get
			{
				return (PropertyEntryMetadata)base.EntryMetadata;
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00031FB0 File Offset: 0x000301B0
		public override DbMemberEntry CreateDbMemberEntry()
		{
			if (!this.EntryMetadata.IsComplex)
			{
				return new DbPropertyEntry(this);
			}
			return new DbComplexPropertyEntry(this);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00031FCC File Offset: 0x000301CC
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			if (!this.EntryMetadata.IsComplex)
			{
				return new DbPropertyEntry<TEntity, TProperty>(this);
			}
			return new DbComplexPropertyEntry<TEntity, TProperty>(this);
		}

		// Token: 0x04000939 RID: 2361
		private bool _getterIsCached;

		// Token: 0x0400093A RID: 2362
		private Func<object, object> _getter;

		// Token: 0x0400093B RID: 2363
		private bool _setterIsCached;

		// Token: 0x0400093C RID: 2364
		private Action<object, object> _setter;
	}
}
