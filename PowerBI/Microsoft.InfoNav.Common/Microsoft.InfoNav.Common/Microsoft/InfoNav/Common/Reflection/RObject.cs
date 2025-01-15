using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.InfoNav.Common.Reflection
{
	// Token: 0x02000080 RID: 128
	[ImmutableObject(true)]
	public sealed class RObject
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		private RObject(object @object)
		{
			this.Value = @object;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000C5F7 File Offset: 0x0000A7F7
		public object Value { get; }

		// Token: 0x1700009F RID: 159
		public RObject this[string property]
		{
			get
			{
				return this.GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
			}
		}

		// Token: 0x170000A0 RID: 160
		public RObject this[int index]
		{
			get
			{
				IList list = this.Value as IList;
				if (list == null)
				{
					return RObject.Null;
				}
				if (index < 0 || index >= list.Count)
				{
					return RObject.Null;
				}
				return RObject.Create(list[index]);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000C650 File Offset: 0x0000A850
		public RObject GetProperty(string property, BindingFlags bindingAttr = BindingFlags.Default)
		{
			if (string.IsNullOrEmpty(property) || this == RObject.Null)
			{
				return RObject.Null;
			}
			PropertyInfo property2 = this.Value.GetType().GetProperty(property, bindingAttr);
			return RObject.Create((property2 != null) ? property2.GetValue(this.Value) : null);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000C69C File Offset: 0x0000A89C
		public static RObject Create(object @object)
		{
			if (@object == null)
			{
				return RObject.Null;
			}
			return new RObject(@object);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public T GetValueOrDefaultAs<T>()
		{
			T t;
			if (this.TryGetValueOrDefaultAs<T>(out t))
			{
				return t;
			}
			return default(T);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public bool TryGetValueOrDefaultAs<T>(out T result)
		{
			object value = this.Value;
			if (value is T)
			{
				T t = (T)((object)value);
				result = t;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x04000113 RID: 275
		public static readonly RObject Null = new RObject(null);
	}
}
