using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C4 RID: 196
	public class FuncLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x0001FA70 File Offset: 0x0001DC70
		public FuncLayoutRenderer(string layoutRendererName, Func<LogEventInfo, LoggingConfiguration, object> renderMethod)
		{
			this.RenderMethod = renderMethod;
			this.LayoutRendererName = layoutRendererName;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001FA86 File Offset: 0x0001DC86
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0001FA8E File Offset: 0x0001DC8E
		public string LayoutRendererName { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0001FA97 File Offset: 0x0001DC97
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0001FA9F File Offset: 0x0001DC9F
		public Func<LogEventInfo, LoggingConfiguration, object> RenderMethod { get; private set; }

		// Token: 0x06000C4C RID: 3148 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue(logEvent);
			if (value != null)
			{
				builder.Append(value);
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		private object GetValue(LogEventInfo logEvent)
		{
			Func<LogEventInfo, LoggingConfiguration, object> renderMethod = this.RenderMethod;
			if (renderMethod == null)
			{
				return null;
			}
			return renderMethod(logEvent, base.LoggingConfiguration);
		}
	}
}
