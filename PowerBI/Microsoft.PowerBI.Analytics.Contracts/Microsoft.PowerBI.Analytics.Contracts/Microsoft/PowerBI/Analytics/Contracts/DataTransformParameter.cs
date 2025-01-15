using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000009 RID: 9
	public sealed class DataTransformParameter
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000021B3 File Offset: 0x000003B3
		public DataTransformParameter(string name, object value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021C9 File Offset: 0x000003C9
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021D1 File Offset: 0x000003D1
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly string _name;

		// Token: 0x04000035 RID: 53
		private readonly object _value;
	}
}
