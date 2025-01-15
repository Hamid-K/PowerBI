using System;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x02000193 RID: 403
	internal sealed class Error
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x0002F54C File Offset: 0x0002D74C
		public Error(Token tok, string msg)
		{
			this.Token = tok;
			this.Message = msg;
			this.Args = null;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002F569 File Offset: 0x0002D769
		public Error(Token tok, string msg, params object[] args)
		{
			this.Token = tok;
			this.Message = msg;
			this.Args = args;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002F588 File Offset: 0x0002D788
		public string GetMessage()
		{
			string text = this.Message;
			if (Utils.Size<object>(this.Args) > 0)
			{
				text = string.Format(text, this.Args);
			}
			if (this.Token != null)
			{
				text = string.Format("at '{0}': {1}", this.Token, text);
			}
			return text;
		}

		// Token: 0x0400046B RID: 1131
		public readonly Token Token;

		// Token: 0x0400046C RID: 1132
		public readonly string Message;

		// Token: 0x0400046D RID: 1133
		public readonly object[] Args;
	}
}
