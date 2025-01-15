using System;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000C3 RID: 195
	internal class JsonLoadSettings
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0002AAC5 File Offset: 0x00028CC5
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0002AACD File Offset: 0x00028CCD
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002AAE9 File Offset: 0x00028CE9
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002AAF1 File Offset: 0x00028CF1
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002AB0D File Offset: 0x00028D0D
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0002AB15 File Offset: 0x00028D15
		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Replace || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		// Token: 0x04000370 RID: 880
		private CommentHandling _commentHandling;

		// Token: 0x04000371 RID: 881
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x04000372 RID: 882
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
