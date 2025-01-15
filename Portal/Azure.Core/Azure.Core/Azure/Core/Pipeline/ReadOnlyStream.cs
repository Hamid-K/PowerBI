using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000099 RID: 153
	internal abstract class ReadOnlyStream : Stream
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		[NullableContext(1)]
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000EDE3 File Offset: 0x0000CFE3
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		public override void Flush()
		{
		}
	}
}
