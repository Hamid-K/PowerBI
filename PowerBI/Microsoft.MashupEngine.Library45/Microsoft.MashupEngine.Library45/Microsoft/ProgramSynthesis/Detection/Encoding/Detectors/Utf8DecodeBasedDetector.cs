using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE8 RID: 2792
	internal class Utf8DecodeBasedDetector : DecodeBasedDetector
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x000D9F99 File Offset: 0x000D8199
		public override int Precedence { get; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060045DF RID: 17887 RVA: 0x000D9D8C File Offset: 0x000D7F8C
		public override double ErrorThreshold
		{
			get
			{
				return 0.01;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x000D9FA1 File Offset: 0x000D81A1
		protected override IEnumerable<EncodingType> SupportedEncodings
		{
			get
			{
				return Utf8DecodeBasedDetector.AllSupportedEncodings;
			}
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x000D9FA8 File Offset: 0x000D81A8
		protected override void ApplyHeuristics(byte[] buffer)
		{
			if (base.DetectedType != EncodingType.Utf8)
			{
				return;
			}
			int num = buffer.Count((byte b) => b == 0);
			if (num == 0)
			{
				return;
			}
			base.Confidence = (float)Math.Pow(2.718281828459045, (double)(-(double)num));
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x000DA001 File Offset: 0x000D8201
		protected override bool DetectFromBom(byte[] buffer)
		{
			if (buffer[0] != 239 || buffer[1] != 187 || buffer[2] != 191)
			{
				return false;
			}
			base.DetectedType = EncodingType.Utf8;
			return true;
		}

		// Token: 0x04001FE3 RID: 8163
		private static readonly EncodingType[] AllSupportedEncodings = new EncodingType[] { EncodingType.Utf8 };
	}
}
