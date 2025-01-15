using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal class NoViableAltException : RecognitionException
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00004B5C File Offset: 0x00002D5C
		public NoViableAltException(IToken t, string fileName_)
			: base("NoViableAlt", fileName_, t.getLine(), t.getColumn())
		{
			this.token = t;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B7D File Offset: 0x00002D7D
		protected NoViableAltException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004B87 File Offset: 0x00002D87
		public override string Message
		{
			get
			{
				if (this.token != null)
				{
					return "unexpected token: " + this.token.ToString();
				}
				return "unexpected token: (null)";
			}
		}

		// Token: 0x04000082 RID: 130
		public IToken token;
	}
}
