using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200000A RID: 10
	internal sealed class ODataTextStreamReader : TextReader
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002C6C File Offset: 0x00000E6C
		internal ODataTextStreamReader(Func<char[], int, int, int> reader)
		{
			this.reader = reader;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C7B File Offset: 0x00000E7B
		public override int Read(char[] buffer, int offset, int count)
		{
			return this.reader(buffer, offset, count);
		}

		// Token: 0x04000017 RID: 23
		private Func<char[], int, int, int> reader;
	}
}
