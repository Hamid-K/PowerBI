using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D9 RID: 217
	[LayoutRenderer("ndlc")]
	[ThreadSafe]
	public class NdlcLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00021371 File Offset: 0x0001F571
		public NdlcLayoutRenderer()
		{
			this.Separator = " ";
			this.BottomFrames = -1;
			this.TopFrames = -1;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00021392 File Offset: 0x0001F592
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0002139A File Offset: 0x0001F59A
		public int TopFrames { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x000213A3 File Offset: 0x0001F5A3
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x000213AB File Offset: 0x0001F5AB
		public int BottomFrames { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000213B4 File Offset: 0x0001F5B4
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x000213BC File Offset: 0x0001F5BC
		public string Separator { get; set; }

		// Token: 0x06000D02 RID: 3330 RVA: 0x000213C8 File Offset: 0x0001F5C8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.TopFrames == 1)
			{
				object obj = NestedDiagnosticsLogicalContext.PeekObject();
				if (obj != null)
				{
					NdlcLayoutRenderer.AppendAsString(obj, base.GetFormatProvider(logEvent, null), builder);
				}
				return;
			}
			object[] allObjects = NestedDiagnosticsLogicalContext.GetAllObjects();
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
				NdlcLayoutRenderer.AppendAsString(allObjects[i], formatProvider, builder);
				text = this.Separator;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00021480 File Offset: 0x0001F680
		private static void AppendAsString(object message, IFormatProvider formatProvider, StringBuilder builder)
		{
			string text = Convert.ToString(message, formatProvider);
			builder.Append(text);
		}
	}
}
