using System;

namespace System.Text.Json
{
	// Token: 0x0200003A RID: 58
	public struct JsonDocumentOptions
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000877A File Offset: 0x0000697A
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00008782 File Offset: 0x00006982
		public JsonCommentHandling CommentHandling
		{
			readonly get
			{
				return this._commentHandling;
			}
			set
			{
				if (value > JsonCommentHandling.Skip)
				{
					throw new ArgumentOutOfRangeException("value", SR.JsonDocumentDoesNotSupportComments);
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000879F File Offset: 0x0000699F
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000087A7 File Offset: 0x000069A7
		public int MaxDepth
		{
			readonly get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_MaxDepthMustBePositive("value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000087BE File Offset: 0x000069BE
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000087C6 File Offset: 0x000069C6
		public bool AllowTrailingCommas { readonly get; set; }

		// Token: 0x060002B4 RID: 692 RVA: 0x000087D0 File Offset: 0x000069D0
		internal JsonReaderOptions GetReaderOptions()
		{
			return new JsonReaderOptions
			{
				AllowTrailingCommas = this.AllowTrailingCommas,
				CommentHandling = this.CommentHandling,
				MaxDepth = this.MaxDepth
			};
		}

		// Token: 0x04000126 RID: 294
		internal const int DefaultMaxDepth = 64;

		// Token: 0x04000127 RID: 295
		private int _maxDepth;

		// Token: 0x04000128 RID: 296
		private JsonCommentHandling _commentHandling;
	}
}
