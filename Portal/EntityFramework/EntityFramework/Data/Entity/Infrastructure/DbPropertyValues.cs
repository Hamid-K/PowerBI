using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022F RID: 559
	public class DbPropertyValues
	{
		// Token: 0x06001D5C RID: 7516 RVA: 0x000534CC File Offset: 0x000516CC
		internal DbPropertyValues(InternalPropertyValues internalValues)
		{
			this._internalValues = internalValues;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000534DB File Offset: 0x000516DB
		public object ToObject()
		{
			return this._internalValues.ToObject();
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000534E8 File Offset: 0x000516E8
		public void SetValues(object obj)
		{
			Check.NotNull<object>(obj, "obj");
			this._internalValues.SetValues(obj);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00053502 File Offset: 0x00051702
		public DbPropertyValues Clone()
		{
			return new DbPropertyValues(this._internalValues.Clone());
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00053514 File Offset: 0x00051714
		public void SetValues(DbPropertyValues propertyValues)
		{
			Check.NotNull<DbPropertyValues>(propertyValues, "propertyValues");
			this._internalValues.SetValues(propertyValues._internalValues);
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00053533 File Offset: 0x00051733
		public IEnumerable<string> PropertyNames
		{
			get
			{
				return this._internalValues.PropertyNames;
			}
		}

		// Token: 0x17000698 RID: 1688
		public object this[string propertyName]
		{
			get
			{
				Check.NotEmpty(propertyName, "propertyName");
				object obj = this._internalValues[propertyName];
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj = new DbPropertyValues(internalPropertyValues);
				}
				return obj;
			}
			set
			{
				Check.NotEmpty(propertyName, "propertyName");
				this._internalValues[propertyName] = value;
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00053593 File Offset: 0x00051793
		public TValue GetValue<TValue>(string propertyName)
		{
			return (TValue)((object)this[propertyName]);
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x000535A1 File Offset: 0x000517A1
		internal InternalPropertyValues InternalPropertyValues
		{
			get
			{
				return this._internalValues;
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000535A9 File Offset: 0x000517A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000535B1 File Offset: 0x000517B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000535BA File Offset: 0x000517BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000535C2 File Offset: 0x000517C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B20 RID: 2848
		private readonly InternalPropertyValues _internalValues;
	}
}
