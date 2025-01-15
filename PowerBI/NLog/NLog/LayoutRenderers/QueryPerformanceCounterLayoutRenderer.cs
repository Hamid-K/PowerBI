using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E3 RID: 227
	[LayoutRenderer("qpc")]
	public class QueryPerformanceCounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00021C5D File Offset: 0x0001FE5D
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00021C65 File Offset: 0x0001FE65
		[DefaultValue(true)]
		public bool Normalize { get; set; } = true;

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00021C6E File Offset: 0x0001FE6E
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x00021C76 File Offset: 0x0001FE76
		[DefaultValue(false)]
		public bool Difference { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00021C7F File Offset: 0x0001FE7F
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00021C8A File Offset: 0x0001FE8A
		[DefaultValue(true)]
		public bool Seconds
		{
			get
			{
				return !this.raw;
			}
			set
			{
				this.raw = !value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00021C96 File Offset: 0x0001FE96
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00021C9E File Offset: 0x0001FE9E
		[DefaultValue(4)]
		public int Precision { get; set; } = 4;

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00021CA7 File Offset: 0x0001FEA7
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00021CAF File Offset: 0x0001FEAF
		[DefaultValue(true)]
		public bool AlignDecimalPoint { get; set; } = true;

		// Token: 0x06000D4D RID: 3405 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			ulong num;
			if (!NativeMethods.QueryPerformanceFrequency(out num))
			{
				throw new InvalidOperationException("Cannot determine high-performance counter frequency.");
			}
			ulong num2;
			if (!NativeMethods.QueryPerformanceCounter(out num2))
			{
				throw new InvalidOperationException("Cannot determine high-performance counter value.");
			}
			this.frequency = num;
			this.firstQpcValue = num2;
			this.lastQpcValue = num2;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00021D0C File Offset: 0x0001FF0C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			ulong? value = this.GetValue();
			if (value != null)
			{
				string text;
				if (this.Seconds)
				{
					text = Convert.ToString(this.ToSeconds(value.Value), CultureInfo.InvariantCulture);
					if (this.AlignDecimalPoint)
					{
						int num = text.IndexOf('.');
						if (num == -1)
						{
							text = text + "." + new string('0', this.Precision);
						}
						else
						{
							text += new string('0', this.Precision - (text.Length - 1 - num));
						}
					}
				}
				else
				{
					text = Convert.ToString(value, CultureInfo.InvariantCulture);
				}
				builder.Append(text);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00021DB6 File Offset: 0x0001FFB6
		private double ToSeconds(ulong qpcValue)
		{
			return Math.Round(qpcValue / this.frequency, this.Precision);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		private ulong? GetValue()
		{
			ulong num;
			if (!NativeMethods.QueryPerformanceCounter(out num))
			{
				return null;
			}
			ulong num2 = num;
			if (this.Difference)
			{
				num -= this.lastQpcValue;
			}
			else if (this.Normalize)
			{
				num -= this.firstQpcValue;
			}
			this.lastQpcValue = num2;
			return new ulong?(num);
		}

		// Token: 0x0400038C RID: 908
		private bool raw;

		// Token: 0x0400038D RID: 909
		private ulong firstQpcValue;

		// Token: 0x0400038E RID: 910
		private ulong lastQpcValue;

		// Token: 0x0400038F RID: 911
		private double frequency = 1.0;
	}
}
