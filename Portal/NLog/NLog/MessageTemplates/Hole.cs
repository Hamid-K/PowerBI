using System;

namespace NLog.MessageTemplates
{
	// Token: 0x02000082 RID: 130
	internal struct Hole
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00018987 File Offset: 0x00016B87
		public Hole(string name, string format, CaptureType captureType, short position, short alignment)
		{
			this.Name = name;
			this.Format = format;
			this.CaptureType = captureType;
			this.Index = position;
			this.Alignment = alignment;
		}

		// Token: 0x0400022A RID: 554
		public readonly string Name;

		// Token: 0x0400022B RID: 555
		public readonly string Format;

		// Token: 0x0400022C RID: 556
		public readonly CaptureType CaptureType;

		// Token: 0x0400022D RID: 557
		public readonly short Index;

		// Token: 0x0400022E RID: 558
		public readonly short Alignment;
	}
}
