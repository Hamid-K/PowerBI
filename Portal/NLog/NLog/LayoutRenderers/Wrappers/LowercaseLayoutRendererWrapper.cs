using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F6 RID: 246
	[LayoutRenderer("lowercase")]
	[AmbientProperty("Lowercase")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class LowercaseLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00022C6E File Offset: 0x00020E6E
		public LowercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Lowercase = true;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00022C88 File Offset: 0x00020E88
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00022C90 File Offset: 0x00020E90
		[DefaultValue(true)]
		public bool Lowercase { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00022C99 File Offset: 0x00020E99
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x00022CA1 File Offset: 0x00020EA1
		public CultureInfo Culture { get; set; }

		// Token: 0x06000DDB RID: 3547 RVA: 0x00022CAA File Offset: 0x00020EAA
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.Lowercase && builder.Length > orgLength)
			{
				this.TransformToLowerCase(builder, orgLength);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00022CD3 File Offset: 0x00020ED3
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00022CD8 File Offset: 0x00020ED8
		private void TransformToLowerCase(StringBuilder target, int startPos)
		{
			CultureInfo culture = this.Culture;
			for (int i = startPos; i < target.Length; i++)
			{
				target[i] = char.ToLower(target[i], culture);
			}
		}
	}
}
