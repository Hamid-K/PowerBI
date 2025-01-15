using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Encoding.Detectors
{
	// Token: 0x02000ADB RID: 2779
	internal abstract class DecodeBasedDetector : IEncodingDetector
	{
		// Token: 0x0600459B RID: 17819 RVA: 0x000D99EA File Offset: 0x000D7BEA
		protected DecodeBasedDetector()
		{
			this.DetectedType = EncodingType.Unknown;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600459C RID: 17820
		public abstract int Precedence { get; }

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600459D RID: 17821
		public abstract double ErrorThreshold { get; }

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x000D99FD File Offset: 0x000D7BFD
		// (set) Token: 0x0600459F RID: 17823 RVA: 0x000D9A05 File Offset: 0x000D7C05
		public float Confidence { get; protected set; }

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x000D9A0E File Offset: 0x000D7C0E
		// (set) Token: 0x060045A1 RID: 17825 RVA: 0x000D9A16 File Offset: 0x000D7C16
		public EncodingType DetectedType { get; protected set; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060045A2 RID: 17826
		protected abstract IEnumerable<EncodingType> SupportedEncodings { get; }

		// Token: 0x060045A3 RID: 17827 RVA: 0x0000CC37 File Offset: 0x0000AE37
		protected virtual void ApplyHeuristics(byte[] buffer)
		{
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000D9A20 File Offset: 0x000D7C20
		public void ConsumeHeader(byte[] buffer)
		{
			if (this.DetectFromBom(buffer))
			{
				this.Confidence = 1f;
				return;
			}
			var <d0b5a5a7-a400-43f8-83fb-0d507b9ad859><>f__AnonymousType = this.SupportedEncodings.Select(delegate(EncodingType encodingType)
			{
				DecodeFailureCounter decodeFailureCounter = new DecodeFailureCounter(buffer.Length);
				Encoding.GetEncoding(encodingType.GetDotNetName(), new EncoderExceptionFallback(), decodeFailureCounter).GetString(buffer);
				float num = (float)decodeFailureCounter.DecodeFailureCount / (float)buffer.Length;
				if ((double)num >= this.ErrorThreshold)
				{
					return new
					{
						DetectedType = EncodingType.Unknown,
						Confidence = 0f
					};
				}
				return new
				{
					DetectedType = encodingType,
					Confidence = (float)Math.Pow(2.718281828459045, (double)(-(double)num) / this.ErrorThreshold)
				};
			}).ArgMax(t => t.Confidence);
			this.DetectedType = <d0b5a5a7-a400-43f8-83fb-0d507b9ad859><>f__AnonymousType.DetectedType;
			this.Confidence = <d0b5a5a7-a400-43f8-83fb-0d507b9ad859><>f__AnonymousType.Confidence;
			this.ApplyHeuristics(buffer);
		}

		// Token: 0x060045A5 RID: 17829
		protected abstract bool DetectFromBom(byte[] buffer);
	}
}
