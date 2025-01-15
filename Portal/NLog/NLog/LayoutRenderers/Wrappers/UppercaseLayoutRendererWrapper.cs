using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000101 RID: 257
	[LayoutRenderer("uppercase")]
	[AmbientProperty("Uppercase")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class UppercaseLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000E22 RID: 3618 RVA: 0x0002348B File Offset: 0x0002168B
		public UppercaseLayoutRendererWrapper()
		{
			this.Culture = CultureInfo.InvariantCulture;
			this.Uppercase = true;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000234A5 File Offset: 0x000216A5
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x000234AD File Offset: 0x000216AD
		[DefaultValue(true)]
		public bool Uppercase { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000234B6 File Offset: 0x000216B6
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x000234BE File Offset: 0x000216BE
		public CultureInfo Culture { get; set; }

		// Token: 0x06000E27 RID: 3623 RVA: 0x000234C7 File Offset: 0x000216C7
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.Uppercase && builder.Length > orgLength)
			{
				this.TransformToUpperCase(builder, orgLength);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000234F0 File Offset: 0x000216F0
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x000234F4 File Offset: 0x000216F4
		private void TransformToUpperCase(StringBuilder target, int startPos)
		{
			CultureInfo culture = this.Culture;
			for (int i = startPos; i < target.Length; i++)
			{
				target[i] = char.ToUpper(target[i], culture);
			}
		}
	}
}
