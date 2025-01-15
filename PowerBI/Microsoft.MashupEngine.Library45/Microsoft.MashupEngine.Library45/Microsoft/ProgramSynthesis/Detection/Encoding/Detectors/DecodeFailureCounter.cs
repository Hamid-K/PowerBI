using System;
using System.Text;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000ADE RID: 2782
	internal class DecodeFailureCounter : DecoderFallback
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x000D9B5D File Offset: 0x000D7D5D
		public DecodeFailureCounter(int bufferLength)
		{
			this._bufferLength = bufferLength;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x000D9B6C File Offset: 0x000D7D6C
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecodeFailureCounterFallbackBuffer(this);
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060045AD RID: 17837 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x000D9B74 File Offset: 0x000D7D74
		// (set) Token: 0x060045AF RID: 17839 RVA: 0x000D9B7C File Offset: 0x000D7D7C
		public int DecodeFailureCount { get; private set; }

		// Token: 0x060045B0 RID: 17840 RVA: 0x000D9B85 File Offset: 0x000D7D85
		public void BumpFailureCount(int index, int num = 1)
		{
			if (index < this._bufferLength - 4)
			{
				this.DecodeFailureCount += num;
			}
		}

		// Token: 0x04001FD3 RID: 8147
		private readonly int _bufferLength;
	}
}
