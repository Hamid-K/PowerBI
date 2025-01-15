using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C5 RID: 197
	[LayoutRenderer("gc")]
	[ThreadSafe]
	public class GarbageCollectorInfoLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001FAE2 File Offset: 0x0001DCE2
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0001FAEA File Offset: 0x0001DCEA
		[DefaultValue("TotalMemory")]
		public GarbageCollectorProperty Property { get; set; }

		// Token: 0x06000C50 RID: 3152 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			long value = this.GetValue();
			if (value >= 0L && value < (long)((ulong)(-1)))
			{
				builder.AppendInvariant((uint)value);
				return;
			}
			builder.Append(value.ToString());
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		private long GetValue()
		{
			long num = 0L;
			switch (this.Property)
			{
			case GarbageCollectorProperty.TotalMemory:
				num = GC.GetTotalMemory(false);
				break;
			case GarbageCollectorProperty.TotalMemoryForceCollection:
				num = GC.GetTotalMemory(true);
				break;
			case GarbageCollectorProperty.CollectionCount0:
				num = (long)GC.CollectionCount(0);
				break;
			case GarbageCollectorProperty.CollectionCount1:
				num = (long)GC.CollectionCount(1);
				break;
			case GarbageCollectorProperty.CollectionCount2:
				num = (long)GC.CollectionCount(2);
				break;
			case GarbageCollectorProperty.MaxGeneration:
				num = (long)GC.MaxGeneration;
				break;
			}
			return num;
		}
	}
}
