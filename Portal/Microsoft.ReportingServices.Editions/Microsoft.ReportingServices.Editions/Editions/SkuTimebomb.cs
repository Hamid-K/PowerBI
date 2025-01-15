using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000012 RID: 18
	public class SkuTimebomb
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002FB5 File Offset: 0x000011B5
		public SkuTimebomb(DateTime installTime, int durationOfTimebombInDays)
		{
			this.InstallTime = installTime;
			this.DurationOfTimebombInDays = durationOfTimebombInDays;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FCB File Offset: 0x000011CB
		public static SkuTimebomb NeverExpires()
		{
			return new SkuTimebomb(DateTime.Today, 0);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static SkuTimebomb ExpiresInDays(int days)
		{
			if (days <= 0)
			{
				throw new ArgumentException("The expiration period must be larger than 0.");
			}
			return new SkuTimebomb(DateTime.Today, 0);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static SkuTimebomb ExpiresInDaysFrom(DateTime installTime, int days)
		{
			return new SkuTimebomb(installTime, days);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002FFD File Offset: 0x000011FD
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003005 File Offset: 0x00001205
		public DateTime InstallTime { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003016 File Offset: 0x00001216
		public int DurationOfTimebombInDays { get; private set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00003020 File Offset: 0x00001220
		public bool HasExpired()
		{
			return this.TimebombCanExpire && DateTime.Today > this.InstallTime.AddDays((double)this.DurationOfTimebombInDays);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003056 File Offset: 0x00001256
		public void ThrowIfExpired()
		{
			if (this.HasExpired())
			{
				throw new EvaluationLicenseExpiredException();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003068 File Offset: 0x00001268
		public override string ToString()
		{
			if (!this.TimebombCanExpire)
			{
				return "Timebomb does not expire.";
			}
			if (this.HasExpired())
			{
				return "Timebomb has expired.";
			}
			return string.Format("Timebomb expires on {0}.", this.InstallTime.AddDays((double)this.DurationOfTimebombInDays));
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000030B5 File Offset: 0x000012B5
		private bool TimebombCanExpire
		{
			get
			{
				return this.DurationOfTimebombInDays != 0;
			}
		}

		// Token: 0x04000056 RID: 86
		private const int TimebombDoesNotExpire = 0;
	}
}
