using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000AE5 RID: 2789
	internal class Utf16DecodeBasedDetector : DecodeBasedDetector
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x000D9D88 File Offset: 0x000D7F88
		public override int Precedence
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x000D9D8C File Offset: 0x000D7F8C
		public override double ErrorThreshold
		{
			get
			{
				return 0.01;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x000D9D97 File Offset: 0x000D7F97
		protected override IEnumerable<EncodingType> SupportedEncodings
		{
			get
			{
				return Utf16DecodeBasedDetector.AllSupportedEncodings;
			}
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x000D9DA0 File Offset: 0x000D7FA0
		protected override void ApplyHeuristics(byte[] buffer)
		{
			if (base.DetectedType == EncodingType.Utf16Le || base.DetectedType == EncodingType.Utf16Be)
			{
				int num = buffer.TakeEvery(2).Count((byte b) => b == 0);
				int num2 = buffer.Skip(1).TakeEvery(2).Count((byte b) => b == 0);
				if (num2 == 0)
				{
					if (base.DetectedType == EncodingType.Utf16Le)
					{
						base.DetectedType = EncodingType.Utf16Be;
					}
					return;
				}
				if (num == 0)
				{
					if (base.DetectedType == EncodingType.Utf16Be)
					{
						base.DetectedType = EncodingType.Utf16Le;
					}
					return;
				}
				if (base.DetectedType == EncodingType.Utf16Be && (double)((float)num2 / (float)num) > 1.414)
				{
					base.DetectedType = EncodingType.Utf16Le;
					return;
				}
				if (base.DetectedType == EncodingType.Utf16Le && (double)((float)num / (float)num2) > 1.414)
				{
					base.DetectedType = EncodingType.Utf16Be;
				}
			}
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x000D9E8C File Offset: 0x000D808C
		protected override bool DetectFromBom(byte[] buffer)
		{
			if (buffer.Length >= 2 && buffer[0] == 254 && buffer[1] == 255)
			{
				base.DetectedType = EncodingType.Utf16Be;
				return true;
			}
			if (buffer.Length >= 2 && buffer[0] == 255 && buffer[1] == 254)
			{
				base.DetectedType = EncodingType.Utf16Le;
				return true;
			}
			return false;
		}

		// Token: 0x04001FDD RID: 8157
		private static readonly EncodingType[] AllSupportedEncodings = new EncodingType[]
		{
			EncodingType.Utf16Le,
			EncodingType.Utf16Be
		};
	}
}
