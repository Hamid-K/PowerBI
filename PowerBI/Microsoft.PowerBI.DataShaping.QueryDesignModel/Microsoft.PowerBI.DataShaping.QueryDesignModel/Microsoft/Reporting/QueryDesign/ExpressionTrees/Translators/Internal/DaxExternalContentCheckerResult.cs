using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012C RID: 300
	internal sealed class DaxExternalContentCheckerResult
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0002D0C5 File Offset: 0x0002B2C5
		private DaxExternalContentCheckerResult(DaxInvalidExternalContentErrorCode errorCode, int errorLine, int errorPosition, int lineCount)
		{
			this._errorCode = errorCode;
			this._errorLine = errorLine;
			this._errorPosition = errorPosition;
			this._lineCount = lineCount;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0002D0EA File Offset: 0x0002B2EA
		public bool IsSafe
		{
			get
			{
				return this._errorCode == DaxInvalidExternalContentErrorCode.None;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0002D0F5 File Offset: 0x0002B2F5
		public DaxInvalidExternalContentErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0002D0FD File Offset: 0x0002B2FD
		public int ErrorLine
		{
			get
			{
				return this._errorLine;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0002D105 File Offset: 0x0002B305
		public int ErrorPosition
		{
			get
			{
				return this._errorPosition;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0002D10D File Offset: 0x0002B30D
		public int LineCount
		{
			get
			{
				return this._lineCount;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002D115 File Offset: 0x0002B315
		internal static DaxExternalContentCheckerResult Safe(int lineCount)
		{
			return new DaxExternalContentCheckerResult(DaxInvalidExternalContentErrorCode.None, 0, 0, lineCount);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002D120 File Offset: 0x0002B320
		internal static DaxExternalContentCheckerResult Unsafe(DaxInvalidExternalContentErrorCode errorCode, int errorLine, int errorPosition)
		{
			return new DaxExternalContentCheckerResult(errorCode, errorLine, errorPosition, 0);
		}

		// Token: 0x04000A89 RID: 2697
		private readonly DaxInvalidExternalContentErrorCode _errorCode;

		// Token: 0x04000A8A RID: 2698
		private readonly int _errorLine;

		// Token: 0x04000A8B RID: 2699
		private readonly int _errorPosition;

		// Token: 0x04000A8C RID: 2700
		private readonly int _lineCount;
	}
}
