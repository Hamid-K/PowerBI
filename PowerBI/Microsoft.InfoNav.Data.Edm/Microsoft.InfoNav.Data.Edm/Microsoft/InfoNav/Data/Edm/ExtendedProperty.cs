using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000029 RID: 41
	[ImmutableObject(true)]
	public class ExtendedProperty
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00008602 File Offset: 0x00006802
		public ExtendedProperty(string name, string value, ExtendedPropertyType type)
		{
			this._name = name;
			this._value = value;
			this._type = type;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000861F File Offset: 0x0000681F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00008627 File Offset: 0x00006827
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000862F File Offset: 0x0000682F
		public ExtendedPropertyType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000170 RID: 368
		private readonly ExtendedPropertyType _type;

		// Token: 0x04000171 RID: 369
		private readonly string _name;

		// Token: 0x04000172 RID: 370
		private readonly string _value;
	}
}
