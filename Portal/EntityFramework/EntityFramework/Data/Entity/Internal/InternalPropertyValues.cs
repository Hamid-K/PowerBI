using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200010B RID: 267
	internal abstract class InternalPropertyValues
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x00031FE8 File Offset: 0x000301E8
		protected InternalPropertyValues(InternalContext internalContext, Type type, bool isEntityValues)
		{
			this._internalContext = internalContext;
			this._type = type;
			this._isEntityValues = isEntityValues;
		}

		// Token: 0x0600130C RID: 4876
		protected abstract IPropertyValuesItem GetItemImpl(string propertyName);

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600130D RID: 4877
		public abstract ISet<string> PropertyNames { get; }

		// Token: 0x0600130E RID: 4878 RVA: 0x00032008 File Offset: 0x00030208
		public object ToObject()
		{
			object obj = this.CreateObject();
			IDictionary<string, Action<object, object>> propertySetters = DbHelpers.GetPropertySetters(this._type);
			foreach (string text in this.PropertyNames)
			{
				object obj2 = this.GetItem(text).Value;
				InternalPropertyValues internalPropertyValues = obj2 as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj2 = internalPropertyValues.ToObject();
				}
				Action<object, object> action;
				if (propertySetters.TryGetValue(text, out action))
				{
					action(obj, obj2);
				}
			}
			return obj;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003209C File Offset: 0x0003029C
		private object CreateObject()
		{
			if (this._isEntityValues)
			{
				return this._internalContext.CreateObject(this._type);
			}
			Func<object> func;
			if (!InternalPropertyValues._nonEntityFactories.TryGetValue(this._type, out func))
			{
				func = Expression.Lambda<Func<object>>(Expression.New(this._type.GetDeclaredConstructor(new Type[0])), null).Compile();
				InternalPropertyValues._nonEntityFactories.TryAdd(this._type, func);
			}
			return func();
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00032114 File Offset: 0x00030314
		public void SetValues(object value)
		{
			IDictionary<string, Func<object, object>> propertyGetters = DbHelpers.GetPropertyGetters(value.GetType());
			foreach (string text in this.PropertyNames)
			{
				Func<object, object> func;
				if (propertyGetters.TryGetValue(text, out func))
				{
					object obj = func(value);
					IPropertyValuesItem item = this.GetItem(text);
					if (obj == null && item.IsComplex)
					{
						throw Error.DbPropertyValues_ComplexObjectCannotBeNull(text, this._type.Name);
					}
					InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
					if (internalPropertyValues == null)
					{
						this.SetValue(item, obj);
					}
					else
					{
						internalPropertyValues.SetValues(obj);
					}
				}
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000321CC File Offset: 0x000303CC
		public InternalPropertyValues Clone()
		{
			return new ClonedPropertyValues(this, null);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000321D8 File Offset: 0x000303D8
		public void SetValues(InternalPropertyValues values)
		{
			if (!this._type.IsAssignableFrom(values.ObjectType))
			{
				throw Error.DbPropertyValues_AttemptToSetValuesFromWrongType(values.ObjectType.Name, this._type.Name);
			}
			foreach (string text in this.PropertyNames)
			{
				IPropertyValuesItem item = values.GetItem(text);
				if (item.Value == null && item.IsComplex)
				{
					throw Error.DbPropertyValues_NestedPropertyValuesNull(text, this._type.Name);
				}
				this[text] = item.Value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		public object this[string propertyName]
		{
			get
			{
				return this.GetItem(propertyName).Value;
			}
			set
			{
				DbPropertyValues dbPropertyValues = value as DbPropertyValues;
				if (dbPropertyValues != null)
				{
					value = dbPropertyValues.InternalPropertyValues;
				}
				IPropertyValuesItem item = this.GetItem(propertyName);
				InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
				if (internalPropertyValues == null)
				{
					this.SetValue(item, value);
					return;
				}
				InternalPropertyValues internalPropertyValues2 = value as InternalPropertyValues;
				if (internalPropertyValues2 == null)
				{
					throw Error.DbPropertyValues_AttemptToSetNonValuesOnComplexProperty();
				}
				internalPropertyValues.SetValues(internalPropertyValues2);
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000322EA File Offset: 0x000304EA
		public IPropertyValuesItem GetItem(string propertyName)
		{
			if (!this.PropertyNames.Contains(propertyName))
			{
				throw Error.DbPropertyValues_PropertyDoesNotExist(propertyName, this._type.Name);
			}
			return this.GetItemImpl(propertyName);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00032314 File Offset: 0x00030514
		private void SetValue(IPropertyValuesItem item, object newValue)
		{
			if (!DbHelpers.PropertyValuesEqual(item.Value, newValue))
			{
				if (item.Value == null && item.IsComplex)
				{
					throw Error.DbPropertyValues_NestedPropertyValuesNull(item.Name, this._type.Name);
				}
				if (newValue != null && !item.Type.IsAssignableFrom(newValue.GetType()))
				{
					throw Error.DbPropertyValues_WrongTypeForAssignment(newValue.GetType().Name, item.Name, item.Type.Name, this._type.Name);
				}
				item.Value = newValue;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x000323A0 File Offset: 0x000305A0
		public Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x000323A8 File Offset: 0x000305A8
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x000323B0 File Offset: 0x000305B0
		public bool IsEntityValues
		{
			get
			{
				return this._isEntityValues;
			}
		}

		// Token: 0x0400093D RID: 2365
		private static readonly ConcurrentDictionary<Type, Func<object>> _nonEntityFactories = new ConcurrentDictionary<Type, Func<object>>();

		// Token: 0x0400093E RID: 2366
		private readonly InternalContext _internalContext;

		// Token: 0x0400093F RID: 2367
		private readonly Type _type;

		// Token: 0x04000940 RID: 2368
		private readonly bool _isEntityValues;
	}
}
