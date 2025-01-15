using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200001A RID: 26
	public sealed class InstanceName
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004424 File Offset: 0x00002624
		public InstanceName(string value)
		{
			this._value = value;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004433 File Offset: 0x00002633
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040000C4 RID: 196
		private readonly string _value;
	}
}
