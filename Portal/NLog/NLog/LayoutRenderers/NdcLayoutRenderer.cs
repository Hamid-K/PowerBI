using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D8 RID: 216
	[LayoutRenderer("ndc")]
	[ThreadSafe]
	public class NdcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00021247 File Offset: 0x0001F447
		public NdcLayoutRenderer()
		{
			this.Separator = " ";
			this.BottomFrames = -1;
			this.TopFrames = -1;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00021268 File Offset: 0x0001F468
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00021270 File Offset: 0x0001F470
		public int TopFrames { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00021279 File Offset: 0x0001F479
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00021281 File Offset: 0x0001F481
		public int BottomFrames { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002128A File Offset: 0x0001F48A
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00021292 File Offset: 0x0001F492
		public string Separator { get; set; }

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002129C File Offset: 0x0001F49C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.TopFrames == 1)
			{
				object obj = NestedDiagnosticsContext.PeekObject();
				if (obj != null)
				{
					NdcLayoutRenderer.AppendAsString(obj, base.GetFormatProvider(logEvent, null), builder);
				}
				return;
			}
			object[] allObjects = NestedDiagnosticsContext.GetAllObjects();
			if (allObjects.Length == 0)
			{
				return;
			}
			int num = 0;
			int num2 = allObjects.Length;
			if (this.TopFrames != -1)
			{
				num2 = Math.Min(this.TopFrames, allObjects.Length);
			}
			else if (this.BottomFrames != -1)
			{
				num = allObjects.Length - Math.Min(this.BottomFrames, allObjects.Length);
			}
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			string text = string.Empty;
			for (int i = num2 - 1; i >= num; i--)
			{
				builder.Append(text);
				NdcLayoutRenderer.AppendAsString(allObjects[i], formatProvider, builder);
				text = this.Separator;
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00021354 File Offset: 0x0001F554
		private static void AppendAsString(object message, IFormatProvider formatProvider, StringBuilder builder)
		{
			string text = Convert.ToString(message, formatProvider);
			builder.Append(text);
		}
	}
}
