using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E4E RID: 7758
	public sealed class VersionRange
	{
		// Token: 0x0600BE9A RID: 48794 RVA: 0x00268A73 File Offset: 0x00266C73
		public VersionRange(Version minVersion, Version maxVersion)
		{
			this.minVersion = minVersion;
			this.maxVersion = maxVersion;
		}

		// Token: 0x0600BE9B RID: 48795 RVA: 0x00268A89 File Offset: 0x00266C89
		public VersionRange(Version minVersion)
			: this(minVersion, VersionRange.MakeMaxVersion(minVersion))
		{
		}

		// Token: 0x0600BE9C RID: 48796 RVA: 0x00268A98 File Offset: 0x00266C98
		public static bool TryParse(string versionText, out VersionRange dependency)
		{
			Match match = VersionRange.parser.Match(versionText);
			if (match.Success)
			{
				string value = match.Groups["low"].Value;
				Version version = null;
				string value2 = match.Groups["high"].Value;
				Version version2 = null;
				if ((value.Length > 0 || value2.Length > 0) && (value.Length == 0 || Versioning.TryParseVersion(value, out version)) && (value2.Length == 0 || Versioning.TryParseVersion(value2, out version2)))
				{
					string value3 = match.Groups["prefix"].Value;
					bool flag = match.Groups["comma"].Value.Length > 0;
					string value4 = match.Groups["suffix"].Value;
					if ((flag && value3.Length != 0 && value4.Length != 0) || (!flag && value3.Length == 0 && value4.Length == 0) || (!flag && value3 == "[" && value4 == "]"))
					{
						if (!flag)
						{
							version2 = ((value4 == "]") ? version : VersionRange.MaximumVersion);
						}
						else if (version2 == null)
						{
							version = VersionRange.AdjustLow(version, value3);
							version2 = VersionRange.MaximumVersion;
						}
						else if (version == null)
						{
							version = new Version(0, 0, 0, 0);
							version2 = VersionRange.AdjustHigh(version2, value4);
						}
						else
						{
							version = VersionRange.AdjustLow(version, value3);
							version2 = VersionRange.AdjustHigh(version2, value4);
						}
						dependency = new VersionRange(version, version2);
						return true;
					}
				}
			}
			dependency = null;
			return false;
		}

		// Token: 0x17002EDE RID: 11998
		// (get) Token: 0x0600BE9D RID: 48797 RVA: 0x00268C49 File Offset: 0x00266E49
		public Version MinVersion
		{
			get
			{
				return this.minVersion;
			}
		}

		// Token: 0x17002EDF RID: 11999
		// (get) Token: 0x0600BE9E RID: 48798 RVA: 0x00268C51 File Offset: 0x00266E51
		public Version MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
		}

		// Token: 0x0600BE9F RID: 48799 RVA: 0x00268C5C File Offset: 0x00266E5C
		public override string ToString()
		{
			int[] versionParts = VersionRange.GetVersionParts(this.minVersion);
			int[] versionParts2 = VersionRange.GetVersionParts(this.maxVersion);
			char c = ']';
			if (versionParts2[3] == 2147483647)
			{
				int num = 3;
				while (num > 0 && versionParts2[num] == 2147483647)
				{
					versionParts2[num] = 0;
					num--;
				}
				if (num == 0 && versionParts2[0] == 2147483647)
				{
					return new Version(versionParts[0], versionParts[1], versionParts[2], versionParts[3]).ToString();
				}
				versionParts2[num]++;
				c = ')';
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}, {1}{2}", new Version(versionParts[0], versionParts[1], versionParts[2], versionParts[3]), new Version(versionParts2[0], versionParts2[1], versionParts2[2], versionParts2[3]), c);
		}

		// Token: 0x0600BEA0 RID: 48800 RVA: 0x00268D12 File Offset: 0x00266F12
		public bool IsAllowed(Version version)
		{
			return version >= this.minVersion && version <= this.maxVersion;
		}

		// Token: 0x0600BEA1 RID: 48801 RVA: 0x00268D30 File Offset: 0x00266F30
		private static Version AdjustLow(Version version, string prefix)
		{
			if (prefix == "(")
			{
				int[] versionParts = VersionRange.GetVersionParts(version);
				int i = 3;
				while (i >= 0)
				{
					if (versionParts[i] != 2147483647)
					{
						versionParts[i]++;
						break;
					}
					versionParts[i--] = 0;
				}
				if (i >= 0)
				{
					version = new Version(versionParts[0], versionParts[1], versionParts[2], versionParts[3]);
				}
			}
			return version;
		}

		// Token: 0x0600BEA2 RID: 48802 RVA: 0x00268D94 File Offset: 0x00266F94
		private static Version AdjustHigh(Version version, string suffix)
		{
			if (suffix == ")")
			{
				int[] versionParts = VersionRange.GetVersionParts(version);
				int i = 3;
				while (i >= 0)
				{
					if (versionParts[i] != 0)
					{
						versionParts[i]--;
						break;
					}
					versionParts[i--] = int.MaxValue;
				}
				if (i >= 0)
				{
					version = new Version(versionParts[0], versionParts[1], versionParts[2], versionParts[3]);
				}
			}
			return version;
		}

		// Token: 0x0600BEA3 RID: 48803 RVA: 0x00268DF8 File Offset: 0x00266FF8
		private static int[] GetVersionParts(Version version)
		{
			return new int[]
			{
				version.Major,
				(version.Minor < 0) ? 0 : version.Minor,
				(version.Build < 0) ? 0 : version.Build,
				(version.Revision < 0) ? 0 : version.Revision
			};
		}

		// Token: 0x0600BEA4 RID: 48804 RVA: 0x00268E53 File Offset: 0x00267053
		private static Version MakeMaxVersion(Version version)
		{
			return VersionRange.AdjustHigh(new Version(version.Major + 1, 0), ")");
		}

		// Token: 0x04006110 RID: 24848
		public static readonly VersionRange DefaultVersion = new VersionRange(new Version(1, 0), VersionRange.MaximumVersion);

		// Token: 0x04006111 RID: 24849
		private static readonly Regex parser = new Regex("^(?<prefix>[[(]?)(?<low>[^][,)(]*)(?<comma>[,]?)(?<ws>[ ]*)(?<high>[^][,)(]*)(?<suffix>[])]?)$");

		// Token: 0x04006112 RID: 24850
		private static readonly Version MaximumVersion = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);

		// Token: 0x04006113 RID: 24851
		private readonly Version minVersion;

		// Token: 0x04006114 RID: 24852
		private readonly Version maxVersion;
	}
}
