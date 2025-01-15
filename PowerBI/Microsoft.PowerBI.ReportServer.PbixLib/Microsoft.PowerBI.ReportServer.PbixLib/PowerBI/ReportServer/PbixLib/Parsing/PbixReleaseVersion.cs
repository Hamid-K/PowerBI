using System;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x02000009 RID: 9
	public class PbixReleaseVersion : IComparable<PbixReleaseVersion>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002207 File Offset: 0x00000407
		public int Year
		{
			get
			{
				return this.version.Major;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002214 File Offset: 0x00000414
		public int Month
		{
			get
			{
				return this.version.Minor;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002221 File Offset: 0x00000421
		public int Train
		{
			get
			{
				return this.version.Build;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000222E File Offset: 0x0000042E
		public PbixReleaseVersion(int year, int month, int train = 0)
		{
			this.version = new Version(year, month, train);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002244 File Offset: 0x00000444
		public int CompareTo(PbixReleaseVersion other)
		{
			return this.version.CompareTo(other.version);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002258 File Offset: 0x00000458
		public override string ToString()
		{
			int num = ((this.Train == 0) ? 2 : 3);
			return this.version.ToString(num);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002280 File Offset: 0x00000480
		public static bool TryParse(string version, out PbixReleaseVersion result, PbixReleaseVersion unknownVersion = null)
		{
			result = null;
			if (string.IsNullOrEmpty(version))
			{
				result = PbixReleaseVersion.MinVersion;
				return true;
			}
			if (version == "Unknown")
			{
				result = unknownVersion ?? PbixReleaseVersion.MinVersion;
				return true;
			}
			string[] array = version.Split(new char[] { '.' });
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (array.Length == 2 && int.TryParse(array[0], out num) && int.TryParse(array[1], out num2))
			{
				result = new PbixReleaseVersion(num, num2, 0);
				return true;
			}
			if (array.Length == 3 && int.TryParse(array[0], out num) && int.TryParse(array[1], out num2) && int.TryParse(array[2], out num3))
			{
				result = new PbixReleaseVersion(num, num2, num3);
				return true;
			}
			return false;
		}

		// Token: 0x04000060 RID: 96
		public static readonly PbixReleaseVersion MinVersion = new PbixReleaseVersion(1, 0, 0);

		// Token: 0x04000061 RID: 97
		public static readonly PbixReleaseVersion MaxVersion = new PbixReleaseVersion(int.MaxValue, 0, 0);

		// Token: 0x04000062 RID: 98
		private Version version;
	}
}
