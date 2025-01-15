using System;
using System.Collections.Generic;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F0 RID: 240
	[LayoutRenderer("var")]
	[ThreadSafe]
	public class VariableLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00022786 File Offset: 0x00020986
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0002278E File Offset: 0x0002098E
		[RequiredParameter]
		[DefaultParameter]
		public string Name { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00022797 File Offset: 0x00020997
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0002279F File Offset: 0x0002099F
		public string Default { get; set; }

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000227A8 File Offset: 0x000209A8
		protected override void InitializeLayoutRenderer()
		{
			SimpleLayout simpleLayout;
			if (this.TryGetLayout(out simpleLayout) && simpleLayout != null)
			{
				simpleLayout.Initialize(base.LoggingConfiguration);
				if (!simpleLayout.ThreadSafe)
				{
					InternalLogger.Warn<string, SimpleLayout>("${{var={0}}} should be declared as <variable name=\"var_{0}\" value=\"...\" /> and used like this ${{var_{0}}}. Because of unsafe Layout={1}", this.Name, simpleLayout);
				}
			}
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000227F0 File Offset: 0x000209F0
		private bool TryGetLayout(out SimpleLayout layout)
		{
			layout = null;
			if (this.Name == null)
			{
				return false;
			}
			LoggingConfiguration loggingConfiguration = base.LoggingConfiguration;
			if (loggingConfiguration == null)
			{
				return false;
			}
			IDictionary<string, SimpleLayout> variables = loggingConfiguration.Variables;
			bool? flag = ((variables != null) ? new bool?(variables.TryGetValue(this.Name, out layout)) : null);
			bool flag2 = true;
			return (flag.GetValueOrDefault() == flag2) & (flag != null);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00022850 File Offset: 0x00020A50
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.Name != null)
			{
				SimpleLayout simpleLayout;
				if (this.TryGetLayout(out simpleLayout))
				{
					if (simpleLayout != null)
					{
						simpleLayout.RenderAppendBuilder(logEvent, builder, false);
						return;
					}
				}
				else if (this.Default != null)
				{
					builder.Append(this.Default);
				}
			}
		}
	}
}
