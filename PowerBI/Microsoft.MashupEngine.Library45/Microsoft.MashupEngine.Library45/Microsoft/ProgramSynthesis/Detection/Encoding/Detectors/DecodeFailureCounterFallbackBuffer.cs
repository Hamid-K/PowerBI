using System;
using System.Text;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000ADF RID: 2783
	internal class DecodeFailureCounterFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x000D9BA0 File Offset: 0x000D7DA0
		private DecodeFailureCounter Counter { get; }

		// Token: 0x060045B2 RID: 17842 RVA: 0x000D9BA8 File Offset: 0x000D7DA8
		public DecodeFailureCounterFallbackBuffer(DecodeFailureCounter counter)
		{
			this.Counter = counter;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x000D9BB7 File Offset: 0x000D7DB7
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Counter.BumpFailureCount(index, bytesUnknown.Length);
			this._remaining++;
			return true;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x000D9BD7 File Offset: 0x000D7DD7
		public override char GetNextChar()
		{
			if (this._remaining > 0)
			{
				this._remaining--;
				return '\ufffd';
			}
			return '\0';
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x000D9BF7 File Offset: 0x000D7DF7
		public override bool MovePrevious()
		{
			this._remaining++;
			return true;
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x000D9C08 File Offset: 0x000D7E08
		public override int Remaining
		{
			get
			{
				return this._remaining;
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x000D9C10 File Offset: 0x000D7E10
		public override void Reset()
		{
			this._remaining = 0;
		}

		// Token: 0x04001FD6 RID: 8150
		private int _remaining;
	}
}
