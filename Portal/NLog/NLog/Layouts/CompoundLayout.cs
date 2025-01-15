using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x0200009F RID: 159
	[Layout("CompoundLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class CompoundLayout : Layout
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0001ADA5 File Offset: 0x00018FA5
		public CompoundLayout()
		{
			this.Layouts = new List<Layout>();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		[ArrayParameter(typeof(Layout), "layout")]
		public IList<Layout> Layouts { get; private set; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001ADCC File Offset: 0x00018FCC
		protected override void InitializeLayout()
		{
			base.InitializeLayout();
			foreach (Layout layout in this.Layouts)
			{
				layout.Initialize(base.LoggingConfiguration);
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001AE24 File Offset: 0x00019024
		internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
		{
			base.PrecalculateBuilderInternal(logEvent, target);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001AE2E File Offset: 0x0001902E
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return base.RenderAllocateBuilder(logEvent, null);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001AE38 File Offset: 0x00019038
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			for (int i = 0; i < this.Layouts.Count; i++)
			{
				this.Layouts[i].RenderAppendBuilder(logEvent, target, false);
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001AE70 File Offset: 0x00019070
		protected override void CloseLayout()
		{
			foreach (Layout layout in this.Layouts)
			{
				layout.Close();
			}
			base.CloseLayout();
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001AEC0 File Offset: 0x000190C0
		public override string ToString()
		{
			return base.ToStringWithNestedItems<Layout>(this.Layouts, (Layout l) => l.ToString());
		}
	}
}
