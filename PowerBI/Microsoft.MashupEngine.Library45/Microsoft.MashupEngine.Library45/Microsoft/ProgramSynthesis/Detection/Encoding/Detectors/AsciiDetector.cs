using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AD9 RID: 2777
	internal class AsciiDetector : ForbiddenByteBasedDetector
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x000D9930 File Offset: 0x000D7B30
		internal AsciiDetector()
		{
			IEnumerable<int> enumerable = Enumerable.Range(0, 9).Concat(Enumerable.Range(14, 18)).AppendItem(11)
				.Concat(Enumerable.Range(127, 129));
			Func<int, byte> func;
			if ((func = AsciiDetector.<>O.<0>__ToByte) == null)
			{
				func = (AsciiDetector.<>O.<0>__ToByte = new Func<int, byte>(Convert.ToByte));
			}
			base..ctor(enumerable.Select(func));
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x000D9991 File Offset: 0x000D7B91
		public override int Precedence
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000D9998 File Offset: 0x000D7B98
		public override void ConsumeHeader(byte[] buffer)
		{
			float num = (float)buffer.Where((byte b) => !base.ForbiddenBytes.Contains(b)).Count<byte>() / (float)buffer.Length;
			if (num >= 0.8f)
			{
				base.DetectedType = EncodingType.Ascii;
				base.Confidence = num;
			}
		}

		// Token: 0x04001FCB RID: 8139
		private const float Threshold = 0.8f;

		// Token: 0x02000ADA RID: 2778
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001FCC RID: 8140
			public static Func<int, byte> <0>__ToByte;
		}
	}
}
