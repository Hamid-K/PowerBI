using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D1 RID: 1745
	[Serializable]
	internal sealed class IntermediateFormatVersion
	{
		// Token: 0x06005DB6 RID: 23990 RVA: 0x0017EB60 File Offset: 0x0017CD60
		internal IntermediateFormatVersion()
		{
			this.SetCurrent();
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0017EB6E File Offset: 0x0017CD6E
		internal IntermediateFormatVersion(int major, int minor, int build)
		{
			this.m_major = major;
			this.m_minor = minor;
			this.m_build = build;
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0017EB8B File Offset: 0x0017CD8B
		static IntermediateFormatVersion()
		{
			int current_build = 0;
			RevertImpersonationContext.Run(delegate
			{
				current_build = IntermediateFormatVersion.EncodeFileVersion(FileVersionInfo.GetVersionInfo(typeof(IntermediateFormatVersion).Assembly.Location));
			});
			IntermediateFormatVersion.m_current_build = current_build;
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x0017EBC1 File Offset: 0x0017CDC1
		// (set) Token: 0x06005DBA RID: 23994 RVA: 0x0017EBC9 File Offset: 0x0017CDC9
		internal int Major
		{
			get
			{
				return this.m_major;
			}
			set
			{
				this.m_major = value;
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x0017EBD2 File Offset: 0x0017CDD2
		// (set) Token: 0x06005DBC RID: 23996 RVA: 0x0017EBDA File Offset: 0x0017CDDA
		internal int Minor
		{
			get
			{
				return this.m_minor;
			}
			set
			{
				this.m_minor = value;
			}
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0017EBE3 File Offset: 0x0017CDE3
		// (set) Token: 0x06005DBE RID: 23998 RVA: 0x0017EBEB File Offset: 0x0017CDEB
		internal int Build
		{
			get
			{
				return this.m_build;
			}
			set
			{
				this.m_build = value;
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x0017EBF4 File Offset: 0x0017CDF4
		internal bool IsOldVersion
		{
			get
			{
				return this.CompareTo(IntermediateFormatVersion.m_current_major, IntermediateFormatVersion.m_current_minor, IntermediateFormatVersion.m_current_build) < 0;
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x0017EC11 File Offset: 0x0017CE11
		internal bool IsRS2000_Beta2_orOlder
		{
			get
			{
				return this.CompareTo(8, 0, 673) <= 0;
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x0017EC26 File Offset: 0x0017CE26
		internal bool IsRS2000_WithSpecialRecursiveAggregates
		{
			get
			{
				return this.CompareTo(8, 0, 700) >= 0;
			}
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x0017EC3B File Offset: 0x0017CE3B
		internal bool IsRS2000_WithNewChartYAxis
		{
			get
			{
				return this.CompareTo(8, 0, 713) >= 0;
			}
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x0017EC50 File Offset: 0x0017CE50
		internal bool IsRS2000_WithOtherPageChunkSplit
		{
			get
			{
				return this.CompareTo(8, 0, 716) >= 0;
			}
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x0017EC65 File Offset: 0x0017CE65
		internal bool IsRS2000_RTM_orOlder
		{
			get
			{
				return this.CompareTo(8, 0, 743) <= 0;
			}
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x0017EC7A File Offset: 0x0017CE7A
		internal bool IsRS2000_RTM_orNewer
		{
			get
			{
				return this.CompareTo(8, 0, 743) >= 0;
			}
		}

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x0017EC8F File Offset: 0x0017CE8F
		internal bool IsRS2000_WithUnusedFieldsOptimization
		{
			get
			{
				return this.CompareTo(8, 0, 801) >= 0;
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x0017ECA4 File Offset: 0x0017CEA4
		internal bool IsRS2000_WithImageInfo
		{
			get
			{
				return this.CompareTo(8, 0, 843) >= 0;
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x06005DC8 RID: 24008 RVA: 0x0017ECB9 File Offset: 0x0017CEB9
		internal bool IsRS2005_Beta2_orOlder
		{
			get
			{
				return this.CompareTo(9, 0, 852) <= 0;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x0017ECCF File Offset: 0x0017CECF
		internal bool IsRS2005_WithMultipleActions
		{
			get
			{
				return this.CompareTo(9, 0, 937) >= 0;
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06005DCA RID: 24010 RVA: 0x0017ECE5 File Offset: 0x0017CEE5
		internal bool IsRS2005_WithSpecialChunkSplit
		{
			get
			{
				return this.CompareTo(9, 0, 937) >= 0;
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x0017ECFB File Offset: 0x0017CEFB
		internal bool IsRS2005_IDW9_orOlder
		{
			get
			{
				return this.CompareTo(9, 0, 951) <= 0;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x0017ED11 File Offset: 0x0017CF11
		internal bool IsRS2005_WithTableDetailFix
		{
			get
			{
				return this.CompareTo(10, 2, 0) >= 0;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x0017ED23 File Offset: 0x0017CF23
		internal bool IsRS2005_WithPHFChunks
		{
			get
			{
				return this.CompareTo(10, 3, 0) >= 0;
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x0017ED35 File Offset: 0x0017CF35
		internal bool IsRS2005_WithTableOptimizations
		{
			get
			{
				return this.CompareTo(10, 4, 0) >= 0;
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x0017ED47 File Offset: 0x0017CF47
		internal bool IsRS2005_WithSharedDrillthroughParams
		{
			get
			{
				return this.CompareTo(10, 8, 0) >= 0;
			}
		}

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x0017ED59 File Offset: 0x0017CF59
		internal bool IsRS2005_WithSimpleTextBoxOptimizations
		{
			get
			{
				return this.CompareTo(10, 5, 0) >= 0;
			}
		}

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x0017ED6B File Offset: 0x0017CF6B
		internal bool IsRS2005_WithChartHeadingInstanceFix
		{
			get
			{
				return this.CompareTo(10, 6, 0) >= 0;
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0017ED7D File Offset: 0x0017CF7D
		internal bool IsRS2005_WithXmlDataElementOutputChange
		{
			get
			{
				return this.CompareTo(10, 7, 0) >= 0;
			}
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0017ED8F File Offset: 0x0017CF8F
		internal bool Is_WithUserSort
		{
			get
			{
				return this.CompareTo(9, 0, 970) >= 0;
			}
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0017EDA5 File Offset: 0x0017CFA5
		private static int EncodeFileVersion(FileVersionInfo fileVersion)
		{
			return ((fileVersion.FileMajorPart % 20 * 10 + fileVersion.FileMinorPart % 10) * 100000 + fileVersion.FileBuildPart % 100000) * 100 + fileVersion.FilePrivatePart % 100;
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0017EDE0 File Offset: 0x0017CFE0
		internal static void DecodeFileVersion(int version, out int major, out int minor, out int build, out int buildminor)
		{
			major = 0;
			minor = 0;
			build = 0;
			buildminor = 0;
			if (version <= 0)
			{
				return;
			}
			buildminor = version % 100;
			version -= buildminor;
			version /= 100;
			build = version % 100000;
			version -= build;
			version /= 100000;
			minor = version % 10;
			version -= minor;
			version /= 10;
			major = version % 20;
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0017EE43 File Offset: 0x0017D043
		internal void SetCurrent()
		{
			this.m_major = IntermediateFormatVersion.m_current_major;
			this.m_minor = IntermediateFormatVersion.m_current_minor;
			this.m_build = IntermediateFormatVersion.m_current_build;
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x0017EE68 File Offset: 0x0017D068
		private int CompareTo(int major, int minor, int build)
		{
			int num = this.Compare(this.m_major, major);
			if (num == 0)
			{
				num = this.Compare(this.m_minor, minor);
				if (num == 0 && this.m_major < 10)
				{
					num = this.Compare(this.m_build, build);
				}
			}
			return num;
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x0017EEB0 File Offset: 0x0017D0B0
		private int Compare(int x, int y)
		{
			if (x < y)
			{
				return -1;
			}
			if (x > y)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0017EEC0 File Offset: 0x0017D0C0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IntermediateFormatVersion, new MemberInfoList
			{
				new MemberInfo(MemberName.IntermediateFormatVersionMajor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionMinor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionBuild, Token.Int32)
			});
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x0017EF20 File Offset: 0x0017D120
		public override string ToString()
		{
			if (this.m_major < 10)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", this.m_major, this.m_minor, this.m_build);
			}
			int num;
			int num2;
			int num3;
			int num4;
			IntermediateFormatVersion.DecodeFileVersion(this.m_build, out num, out num2, out num3, out num4);
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", new object[] { this.m_major, this.m_minor, num3, num4 });
		}

		// Token: 0x04002FE8 RID: 12264
		private int m_major;

		// Token: 0x04002FE9 RID: 12265
		private int m_minor;

		// Token: 0x04002FEA RID: 12266
		private int m_build;

		// Token: 0x04002FEB RID: 12267
		private static readonly int m_current_major = 10;

		// Token: 0x04002FEC RID: 12268
		private static readonly int m_current_minor = 8;

		// Token: 0x04002FED RID: 12269
		private static readonly int m_current_build;
	}
}
