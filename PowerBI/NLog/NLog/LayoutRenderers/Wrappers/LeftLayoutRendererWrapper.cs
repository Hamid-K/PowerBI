using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F5 RID: 245
	[LayoutRenderer("left")]
	[AmbientProperty("Truncate")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class LeftLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00022C05 File Offset: 0x00020E05
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00022C0D File Offset: 0x00020E0D
		[RequiredParameter]
		public int Length { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00022C16 File Offset: 0x00020E16
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00022C1E File Offset: 0x00020E1E
		public int Truncate
		{
			get
			{
				return this.Length;
			}
			set
			{
				this.Length = value;
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00022C27 File Offset: 0x00020E27
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			if (this.Length <= 0)
			{
				return;
			}
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (builder.Length - orgLength > this.Length)
			{
				builder.Length = orgLength + this.Length;
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00022C5F File Offset: 0x00020E5F
		protected override string Transform(string text)
		{
			throw new NotSupportedException();
		}
	}
}
