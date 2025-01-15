using System;
using System.Configuration;

namespace Microsoft.BusinessIntelligence
{
	// Token: 0x02000004 RID: 4
	internal class ExposureControlProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000212A File Offset: 0x0000032A
		private ExposureControlProvider()
		{
			this.SetExposureControl();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public static ExposureControlProvider Instance
		{
			get
			{
				return ExposureControlProvider.instance;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000213F File Offset: 0x0000033F
		internal static string ExposureControlConfigKey
		{
			get
			{
				return "ExposureControlLevel";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002146 File Offset: 0x00000346
		internal ExposureControl ExposureControl
		{
			get
			{
				if (ExposureControlProvider.exposureControl == null)
				{
					throw new InvalidOperationException("Exposure control instance is not initialized yet!");
				}
				return ExposureControlProvider.exposureControl;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002163 File Offset: 0x00000363
		private void SetExposureControl(ExposureLevel exposureLevel)
		{
			if (ExposureControlProvider.exposureControl != null)
			{
				throw new InvalidOperationException("You cannot set the exposure control instance more than once!");
			}
			ExposureControlProvider.exposureControl = this.CreateExposureControl(exposureLevel);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002187 File Offset: 0x00000387
		private void SetExposureControl()
		{
			this.SetExposureControl(this.CreateExposureLevelFromStringValue(ConfigurationManager.AppSettings[ExposureControlProvider.ExposureControlConfigKey]));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A4 File Offset: 0x000003A4
		private ExposureLevel CreateExposureLevelFromStringValue(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				value = value.Trim();
			}
			ExposureLevel exposureLevel;
			if (string.Equals(value, "1"))
			{
				exposureLevel = ExposureLevel.Preview;
			}
			else if (string.Equals(value, "2"))
			{
				exposureLevel = ExposureLevel.Dogfood;
			}
			else if (string.Equals(value, "3"))
			{
				exposureLevel = ExposureLevel.Development;
			}
			else
			{
				exposureLevel = ExposureLevel.Production;
			}
			return exposureLevel;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
		private ExposureControl CreateExposureControl(ExposureLevel exposureLevel)
		{
			ExposureControl exposureControl;
			if (ExposureLevel.Preview == exposureLevel)
			{
				exposureControl = new ExposureControl(ExposureLevel.Preview);
			}
			else if (ExposureLevel.Dogfood == exposureLevel)
			{
				exposureControl = new ExposureControl(ExposureLevel.Dogfood);
			}
			else if (ExposureLevel.Development == exposureLevel)
			{
				exposureControl = new ExposureControl(ExposureLevel.Development);
			}
			else
			{
				exposureControl = new ExposureControl(ExposureLevel.Production);
			}
			return exposureControl;
		}

		// Token: 0x04000029 RID: 41
		private static volatile ExposureControl exposureControl;

		// Token: 0x0400002A RID: 42
		private static readonly ExposureControlProvider instance = new ExposureControlProvider();
	}
}
