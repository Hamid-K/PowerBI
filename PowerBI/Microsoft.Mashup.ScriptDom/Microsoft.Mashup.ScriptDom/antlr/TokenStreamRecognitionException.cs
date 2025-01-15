using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	internal class TokenStreamRecognitionException : TokenStreamException
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00005321 File Offset: 0x00003521
		public TokenStreamRecognitionException(RecognitionException re)
			: base(re.Message)
		{
			this.recog = re;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005336 File Offset: 0x00003536
		protected TokenStreamRecognitionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005340 File Offset: 0x00003540
		public override string ToString()
		{
			return this.recog.ToString();
		}

		// Token: 0x04000096 RID: 150
		public RecognitionException recog;
	}
}
