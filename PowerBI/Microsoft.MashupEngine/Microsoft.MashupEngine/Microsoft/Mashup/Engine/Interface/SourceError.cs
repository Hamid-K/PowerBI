using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000079 RID: 121
	public class SourceError : IError
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00002FE8 File Offset: 0x000011E8
		public SourceError(ErrorKind kind, SourceLocation location, string message)
		{
			this.kind = kind;
			this.location = location;
			this.message = message;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00003005 File Offset: 0x00001205
		public SourceError(ErrorKind kind, SourceLocation location, string message, string sourceText)
			: this(kind, location, message)
		{
			this.errorRange = ErrorRange.GetErrorRange(sourceText, this.location);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003023 File Offset: 0x00001223
		public SourceError(IError error, string sourceText)
			: this(error.Kind, error.Location, error.Message)
		{
			this.errorRange = ErrorRange.GetErrorRange(sourceText, error.Location);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000304F File Offset: 0x0000124F
		public ErrorKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003057 File Offset: 0x00001257
		public SourceLocation Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000305F File Offset: 0x0000125F
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003067 File Offset: 0x00001267
		public ErrorRange ErrorRange
		{
			get
			{
				return this.errorRange;
			}
		}

		// Token: 0x0400014F RID: 335
		private ErrorKind kind;

		// Token: 0x04000150 RID: 336
		private SourceLocation location;

		// Token: 0x04000151 RID: 337
		private ErrorRange errorRange;

		// Token: 0x04000152 RID: 338
		private string message;
	}
}
