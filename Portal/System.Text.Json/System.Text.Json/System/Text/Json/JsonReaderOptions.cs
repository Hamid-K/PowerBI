using System;

namespace System.Text.Json
{
	// Token: 0x02000046 RID: 70
	public struct JsonReaderOptions
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B02D File Offset: 0x0000922D
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000B035 File Offset: 0x00009235
		public JsonCommentHandling CommentHandling
		{
			readonly get
			{
				return this._commentHandling;
			}
			set
			{
				if (value > JsonCommentHandling.Allow)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_CommentEnumMustBeInRange("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B04C File Offset: 0x0000924C
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000B054 File Offset: 0x00009254
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B06B File Offset: 0x0000926B
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000B073 File Offset: 0x00009273
		public bool AllowTrailingCommas { readonly get; set; }

		// Token: 0x0400015D RID: 349
		internal const int DefaultMaxDepth = 64;

		// Token: 0x0400015E RID: 350
		private int _maxDepth;

		// Token: 0x0400015F RID: 351
		private JsonCommentHandling _commentHandling;
	}
}
