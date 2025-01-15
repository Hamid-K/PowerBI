using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000063 RID: 99
	[ImmutableObject(true)]
	public struct Optional<T>
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000A033 File Offset: 0x00008233
		private Optional(bool specified, T value)
		{
			this._specified = specified;
			this._value = value;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000A043 File Offset: 0x00008243
		public bool Specified
		{
			get
			{
				return this._specified;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000A04B File Offset: 0x0000824B
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000A053 File Offset: 0x00008253
		public static implicit operator Optional<T>(T value)
		{
			return new Optional<T>(true, value);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000A05C File Offset: 0x0000825C
		public override string ToString()
		{
			if (!this._specified)
			{
				return "(unspecified)";
			}
			if (this._value != null)
			{
				T value = this._value;
				return value.ToString();
			}
			return "(null)";
		}

		// Token: 0x040000CC RID: 204
		private readonly bool _specified;

		// Token: 0x040000CD RID: 205
		private readonly T _value;
	}
}
