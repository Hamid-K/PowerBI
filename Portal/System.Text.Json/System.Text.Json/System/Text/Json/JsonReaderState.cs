using System;

namespace System.Text.Json
{
	// Token: 0x02000047 RID: 71
	public struct JsonReaderState
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000B07C File Offset: 0x0000927C
		public JsonReaderState(JsonReaderOptions options = default(JsonReaderOptions))
		{
			this._lineNumber = 0L;
			this._bytePositionInLine = 0L;
			this._inObject = false;
			this._isNotPrimitive = false;
			this._valueIsEscaped = false;
			this._trailingCommaBeforeComment = false;
			this._tokenType = JsonTokenType.None;
			this._previousTokenType = JsonTokenType.None;
			this._readerOptions = options;
			this._bitStack = default(BitStack);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000B0D6 File Offset: 0x000092D6
		public JsonReaderOptions Options
		{
			get
			{
				return this._readerOptions;
			}
		}

		// Token: 0x04000161 RID: 353
		internal long _lineNumber;

		// Token: 0x04000162 RID: 354
		internal long _bytePositionInLine;

		// Token: 0x04000163 RID: 355
		internal bool _inObject;

		// Token: 0x04000164 RID: 356
		internal bool _isNotPrimitive;

		// Token: 0x04000165 RID: 357
		internal bool _valueIsEscaped;

		// Token: 0x04000166 RID: 358
		internal bool _trailingCommaBeforeComment;

		// Token: 0x04000167 RID: 359
		internal JsonTokenType _tokenType;

		// Token: 0x04000168 RID: 360
		internal JsonTokenType _previousTokenType;

		// Token: 0x04000169 RID: 361
		internal JsonReaderOptions _readerOptions;

		// Token: 0x0400016A RID: 362
		internal BitStack _bitStack;
	}
}
