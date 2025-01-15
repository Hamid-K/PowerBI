using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Azure.Core;

namespace Azure.Messaging
{
	// Token: 0x0200003A RID: 58
	[NullableContext(2)]
	[Nullable(0)]
	public class MessageContent
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004F97 File Offset: 0x00003197
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004F9F File Offset: 0x0000319F
		public virtual BinaryData Data { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004FA8 File Offset: 0x000031A8
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00004FB0 File Offset: 0x000031B0
		public virtual ContentType? ContentType
		{
			get
			{
				return this.ContentTypeCore;
			}
			set
			{
				this.ContentTypeCore = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004FB9 File Offset: 0x000031B9
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00004FC1 File Offset: 0x000031C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual ContentType? ContentTypeCore { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004FCA File Offset: 0x000031CA
		public virtual bool IsReadOnly { get; }
	}
}
