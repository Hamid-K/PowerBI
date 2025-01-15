using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE7 RID: 2791
	internal class Utf32DecodeBasedDetector : DecodeBasedDetector
	{
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x000D9F09 File Offset: 0x000D8109
		public override double ErrorThreshold
		{
			get
			{
				return 0.05;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x000D9F14 File Offset: 0x000D8114
		protected override IEnumerable<EncodingType> SupportedEncodings
		{
			get
			{
				return Utf32DecodeBasedDetector.AllSupportedEncodings;
			}
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000D9F1C File Offset: 0x000D811C
		protected override bool DetectFromBom(byte[] buffer)
		{
			if (buffer.Length >= 4 && buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 254 && buffer[3] == 255)
			{
				base.DetectedType = EncodingType.Utf32Be;
				return true;
			}
			if (buffer.Length >= 4 && buffer[0] == 255 && buffer[1] == 254 && buffer[2] == 0 && buffer[3] == 0)
			{
				base.DetectedType = EncodingType.Utf32Le;
				return true;
			}
			return false;
		}

		// Token: 0x04001FE1 RID: 8161
		private static readonly EncodingType[] AllSupportedEncodings = new EncodingType[]
		{
			EncodingType.Utf32Le,
			EncodingType.Utf32Be
		};
	}
}
