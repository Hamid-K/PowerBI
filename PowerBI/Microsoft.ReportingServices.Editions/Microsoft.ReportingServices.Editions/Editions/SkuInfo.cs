using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200000E RID: 14
	public sealed class SkuInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002A5F File Offset: 0x00000C5F
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002A66 File Offset: 0x00000C66
		public static string ActualMachineName { get; set; } = Environment.MachineName;

		// Token: 0x06000033 RID: 51 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static SkuInfo DefaultsForSku(SkuType skuType, string instanceName)
		{
			SkuTimebomb skuTimebomb = SkuTimebomb.NeverExpires();
			if (skuType == SkuType.PbirsEvaluation || skuType == SkuType.SsrsEvaluation)
			{
				skuTimebomb = new SkuTimebomb(DateTime.Now, 180);
			}
			return new SkuInfo(instanceName, Environment.MachineName, skuType, skuTimebomb);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002AB6 File Offset: 0x00000CB6
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002ABE File Offset: 0x00000CBE
		public string InstanceId { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AC7 File Offset: 0x00000CC7
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002ACF File Offset: 0x00000CCF
		public string MachineName { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002AD8 File Offset: 0x00000CD8
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public SkuType SkuType { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public int SkuTypeId
		{
			get
			{
				return (int)this.SkuType;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public SkuInfo(string instanceId, string checksumString)
		{
			if (string.IsNullOrEmpty(instanceId))
			{
				throw new ArgumentNullException("instanceId");
			}
			if (string.IsNullOrEmpty(checksumString))
			{
				throw new ArgumentNullException("checksumString");
			}
			if (uint.Parse(checksumString.Substring(0, 11)) != 3842268890U)
			{
				throw new InvalidOperationException("Invalid SKU Data.");
			}
			uint num = uint.Parse(checksumString.Substring(11, 11));
			DateTime dateTime = DateTime.FromFileTime((long)(((ulong)uint.Parse(checksumString.Substring(22, 11)) << 32) | (ulong)num));
			string text = checksumString.Substring(33, 36);
			this.SkuType = SkuUtils.SkuWithGuid(text);
			string text2 = checksumString.Substring(69);
			int num2 = text2.IndexOf('\u0001');
			int num4;
			if (num2 != -1)
			{
				int num3 = text2.LastIndexOf('\u0001');
				this.MachineName = text2.Substring(0, num2);
				num4 = (int)uint.Parse(text2.Substring(num3 + 1));
			}
			else
			{
				this.MachineName = text2.Substring(0, 10);
				this.ExtendNameToMachineName();
				num4 = (int)uint.Parse(text2.Substring(10, 11));
			}
			this.InstanceId = instanceId;
			this.Timebomb = new SkuTimebomb(dateTime, num4);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C13 File Offset: 0x00000E13
		private void ExtendNameToMachineName()
		{
			if (SkuInfo.ActualMachineName.StartsWith(this.MachineName))
			{
				this.MachineName = SkuInfo.ActualMachineName;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C32 File Offset: 0x00000E32
		public SkuInfo(string instanceId, string machineName, SkuType skuType, SkuTimebomb timebomb)
		{
			this.InstanceId = instanceId;
			this.MachineName = machineName;
			this.SkuType = skuType;
			this.Timebomb = timebomb;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C58 File Offset: 0x00000E58
		public string GetStringRepresentation()
		{
			long num = this.Timebomb.InstallTime.ToFileTime();
			return string.Format("{0:D011}{1:D011}{2:D011}{3}{4}{5}{6:D011}", new object[]
			{
				3842268890U,
				(uint)(num & (long)((ulong)(-1))),
				(uint)(num >> 32),
				this.SkuType.GetAttribute<SkuDetails>().Guid.ToUpper(),
				this.MachineName,
				'\u0001',
				(uint)this.Timebomb.DurationOfTimebombInDays
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public string GetStringRepresentationWrong()
		{
			long num = this.Timebomb.InstallTime.ToFileTime();
			string text = this.MachineName;
			if (text.Length > 10)
			{
				text = text.Substring(0, 10);
			}
			else
			{
				text += new string('\u0001', 10 - text.Length);
			}
			return string.Format("{0:D011}{1:D011}{2:D011}{3}{4}{5:D011}", new object[]
			{
				3842268890U,
				(uint)(num & (long)((ulong)(-1))),
				(uint)(num >> 32),
				this.SkuType.GetAttribute<SkuDetails>().Guid.ToUpper(),
				text,
				(uint)this.Timebomb.DurationOfTimebombInDays
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002DB3 File Offset: 0x00000FB3
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002DBB File Offset: 0x00000FBB
		public SkuTimebomb Timebomb { get; private set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public override string ToString()
		{
			return string.Format("Instance: {0}, SKU:{1}, Timebomb:{2}", this.InstanceId, this.SkuType.GetStrings().FullName, this.Timebomb);
		}

		// Token: 0x0400003D RID: 61
		private const int MagicCookieStart = 0;

		// Token: 0x0400003E RID: 62
		private const int MagicCookieLength = 11;

		// Token: 0x0400003F RID: 63
		private const int FileTimeLowStart = 11;

		// Token: 0x04000040 RID: 64
		private const int FileTimeLowLength = 11;

		// Token: 0x04000041 RID: 65
		private const int FileTimeHighStart = 22;

		// Token: 0x04000042 RID: 66
		private const int FileTimeHighLength = 11;

		// Token: 0x04000043 RID: 67
		private const int GuidStart = 33;

		// Token: 0x04000044 RID: 68
		private const int GuidLength = 36;

		// Token: 0x04000045 RID: 69
		private const int MachineNameStart = 69;

		// Token: 0x04000046 RID: 70
		private const int MachineNameLength = 10;

		// Token: 0x04000047 RID: 71
		private const int TimeBombLength = 11;

		// Token: 0x04000048 RID: 72
		private const uint MagicCookie = 3842268890U;

		// Token: 0x04000049 RID: 73
		private const char MachineNamePaddingCharacter = '\u0001';
	}
}
