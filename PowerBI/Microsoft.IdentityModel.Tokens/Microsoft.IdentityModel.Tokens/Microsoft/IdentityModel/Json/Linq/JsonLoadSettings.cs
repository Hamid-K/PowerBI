using System;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C4 RID: 196
	internal class JsonLoadSettings
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0002B198 File Offset: 0x00029398
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002B1B5 File Offset: 0x000293B5
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002B1BD File Offset: 0x000293BD
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
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002B1D9 File Offset: 0x000293D9
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002B1E1 File Offset: 0x000293E1
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
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002B1FD File Offset: 0x000293FD
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002B205 File Offset: 0x00029405
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

		// Token: 0x0400038B RID: 907
		private CommentHandling _commentHandling;

		// Token: 0x0400038C RID: 908
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x0400038D RID: 909
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
