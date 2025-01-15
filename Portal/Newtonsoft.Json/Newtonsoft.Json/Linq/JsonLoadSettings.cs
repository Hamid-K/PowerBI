using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C4 RID: 196
	public class JsonLoadSettings
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002B2B0 File Offset: 0x000294B0
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002B2CD File Offset: 0x000294CD
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002B2D5 File Offset: 0x000294D5
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

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B2F1 File Offset: 0x000294F1
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B2F9 File Offset: 0x000294F9
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

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002B315 File Offset: 0x00029515
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B31D File Offset: 0x0002951D
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

		// Token: 0x0400038C RID: 908
		private CommentHandling _commentHandling;

		// Token: 0x0400038D RID: 909
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x0400038E RID: 910
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
