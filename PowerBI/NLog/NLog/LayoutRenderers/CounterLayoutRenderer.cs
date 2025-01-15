using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BA RID: 186
	[LayoutRenderer("counter")]
	public class CounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001EAB1 File Offset: 0x0001CCB1
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0001EAB9 File Offset: 0x0001CCB9
		[DefaultValue(1)]
		public int Value { get; set; } = 1;

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001EAC2 File Offset: 0x0001CCC2
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0001EACA File Offset: 0x0001CCCA
		[DefaultValue(1)]
		public int Increment { get; set; } = 1;

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001EAD3 File Offset: 0x0001CCD3
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001EADB File Offset: 0x0001CCDB
		public Layout Sequence { get; set; }

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			int nextValue = this.GetNextValue(logEvent);
			builder.AppendInvariant(nextValue);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0001EB00 File Offset: 0x0001CD00
		private int GetNextValue(LogEventInfo logEvent)
		{
			int num;
			if (this.Sequence != null)
			{
				num = CounterLayoutRenderer.GetNextSequenceValue(this.Sequence.Render(logEvent), this.Value, this.Increment);
			}
			else
			{
				num = this.Value;
				this.Value += this.Increment;
			}
			return num;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001EB50 File Offset: 0x0001CD50
		private static int GetNextSequenceValue(string sequenceName, int defaultValue, int increment)
		{
			Dictionary<string, int> dictionary = CounterLayoutRenderer.sequences;
			int num3;
			lock (dictionary)
			{
				int num;
				if (!CounterLayoutRenderer.sequences.TryGetValue(sequenceName, out num))
				{
					num = defaultValue;
				}
				int num2 = num;
				num += increment;
				CounterLayoutRenderer.sequences[sequenceName] = num;
				num3 = num2;
			}
			return num3;
		}

		// Token: 0x040002E4 RID: 740
		private static Dictionary<string, int> sequences = new Dictionary<string, int>();
	}
}
