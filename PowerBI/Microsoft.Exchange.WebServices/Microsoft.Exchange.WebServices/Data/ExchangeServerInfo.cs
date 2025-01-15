using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D3 RID: 211
	public sealed class ExchangeServerInfo
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00020563 File Offset: 0x0001F563
		internal ExchangeServerInfo()
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002056C File Offset: 0x0001F56C
		internal static ExchangeServerInfo Parse(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(reader.HasAttributes, "ExchangeServerVersion.Parse", "Current element doesn't have attributes");
			return new ExchangeServerInfo
			{
				MajorVersion = reader.ReadAttributeValue<int>("MajorVersion"),
				MinorVersion = reader.ReadAttributeValue<int>("MinorVersion"),
				MajorBuildNumber = reader.ReadAttributeValue<int>("MajorBuildNumber"),
				MinorBuildNumber = reader.ReadAttributeValue<int>("MinorBuildNumber"),
				VersionString = reader.ReadAttributeValue("Version")
			};
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000205EC File Offset: 0x0001F5EC
		internal static ExchangeServerInfo Parse(JsonObject jsonObject)
		{
			return new ExchangeServerInfo
			{
				MajorVersion = jsonObject.ReadAsInt("MajorVersion"),
				MinorVersion = jsonObject.ReadAsInt("MinorVersion"),
				MajorBuildNumber = jsonObject.ReadAsInt("MajorBuildNumber"),
				MinorBuildNumber = jsonObject.ReadAsInt("MinorBuildNumber"),
				VersionString = jsonObject.ReadAsString("Version")
			};
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00020655 File Offset: 0x0001F655
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0002065D File Offset: 0x0001F65D
		public int MajorVersion
		{
			get
			{
				return this.majorVersion;
			}
			internal set
			{
				this.majorVersion = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00020666 File Offset: 0x0001F666
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0002066E File Offset: 0x0001F66E
		public int MinorVersion
		{
			get
			{
				return this.minorVersion;
			}
			internal set
			{
				this.minorVersion = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00020677 File Offset: 0x0001F677
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0002067F File Offset: 0x0001F67F
		public int MajorBuildNumber
		{
			get
			{
				return this.majorBuildNumber;
			}
			internal set
			{
				this.majorBuildNumber = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00020688 File Offset: 0x0001F688
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00020690 File Offset: 0x0001F690
		public int MinorBuildNumber
		{
			get
			{
				return this.minorBuildNumber;
			}
			internal set
			{
				this.minorBuildNumber = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00020699 File Offset: 0x0001F699
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x000206A1 File Offset: 0x0001F6A1
		public string VersionString
		{
			get
			{
				return this.versionString;
			}
			internal set
			{
				this.versionString = value;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000206AC File Offset: 0x0001F6AC
		public override string ToString()
		{
			return string.Format("{0:d}.{1:d2}.{2:d4}.{3:d3}", new object[] { this.MajorVersion, this.MinorVersion, this.MajorBuildNumber, this.MinorBuildNumber });
		}

		// Token: 0x040002F9 RID: 761
		private int majorVersion;

		// Token: 0x040002FA RID: 762
		private int minorVersion;

		// Token: 0x040002FB RID: 763
		private int majorBuildNumber;

		// Token: 0x040002FC RID: 764
		private int minorBuildNumber;

		// Token: 0x040002FD RID: 765
		private string versionString;
	}
}
