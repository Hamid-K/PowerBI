using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200164F RID: 5711
	internal class ConnectionTestedTableValue : WrappingTableValue
	{
		// Token: 0x0600901B RID: 36891 RVA: 0x00117ED0 File Offset: 0x001160D0
		public ConnectionTestedTableValue(TableValue table)
			: base(table)
		{
		}

		// Token: 0x0600901C RID: 36892 RVA: 0x001DEB50 File Offset: 0x001DCD50
		protected override TableValue New(TableValue table)
		{
			return new ConnectionTestedTableValue(table);
		}

		// Token: 0x0600901D RID: 36893 RVA: 0x0000336E File Offset: 0x0000156E
		public override void TestConnection()
		{
		}
	}
}
