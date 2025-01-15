using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000019 RID: 25
	public sealed class InstanceId
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00004400 File Offset: 0x00002600
		public InstanceId(string value)
		{
			this._value = value;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000440F File Offset: 0x0000260F
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004417 File Offset: 0x00002617
		public RSPath ToPath()
		{
			return new RSPath(this._value);
		}

		// Token: 0x040000C3 RID: 195
		private readonly string _value;
	}
}
