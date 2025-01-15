using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Text.Json
{
	// Token: 0x0200003F RID: 63
	[NullableContext(2)]
	[Nullable(0)]
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000092E5 File Offset: 0x000074E5
		public JsonException(string message, string path, long? lineNumber, long? bytePositionInLine, Exception innerException)
			: base(message, innerException)
		{
			this._message = message;
			this.LineNumber = lineNumber;
			this.BytePositionInLine = bytePositionInLine;
			this.Path = path;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000930D File Offset: 0x0000750D
		public JsonException(string message, string path, long? lineNumber, long? bytePositionInLine)
			: base(message)
		{
			this._message = message;
			this.LineNumber = lineNumber;
			this.BytePositionInLine = bytePositionInLine;
			this.Path = path;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009333 File Offset: 0x00007533
		public JsonException(string message, Exception innerException)
			: base(message, innerException)
		{
			this._message = message;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009344 File Offset: 0x00007544
		public JsonException(string message)
			: base(message)
		{
			this._message = message;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009354 File Offset: 0x00007554
		public JsonException()
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000935C File Offset: 0x0000755C
		[NullableContext(1)]
		protected JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.LineNumber = (long?)info.GetValue("LineNumber", typeof(long?));
			this.BytePositionInLine = (long?)info.GetValue("BytePositionInLine", typeof(long?));
			this.Path = info.GetString("Path");
			this.SetMessage(info.GetString("ActualMessage"));
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000093D3 File Offset: 0x000075D3
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000093DB File Offset: 0x000075DB
		internal bool AppendPathInformation { get; set; }

		// Token: 0x06000315 RID: 789 RVA: 0x000093E4 File Offset: 0x000075E4
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("LineNumber", this.LineNumber, typeof(long?));
			info.AddValue("BytePositionInLine", this.BytePositionInLine, typeof(long?));
			info.AddValue("Path", this.Path, typeof(string));
			info.AddValue("ActualMessage", this.Message, typeof(string));
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000946F File Offset: 0x0000766F
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00009477 File Offset: 0x00007677
		public long? LineNumber { get; internal set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00009480 File Offset: 0x00007680
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00009488 File Offset: 0x00007688
		public long? BytePositionInLine { get; internal set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00009491 File Offset: 0x00007691
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00009499 File Offset: 0x00007699
		public string Path { get; internal set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000094A2 File Offset: 0x000076A2
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				return this._message ?? base.Message;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000094B4 File Offset: 0x000076B4
		internal void SetMessage(string message)
		{
			this._message = message;
		}

		// Token: 0x04000139 RID: 313
		internal string _message;
	}
}
