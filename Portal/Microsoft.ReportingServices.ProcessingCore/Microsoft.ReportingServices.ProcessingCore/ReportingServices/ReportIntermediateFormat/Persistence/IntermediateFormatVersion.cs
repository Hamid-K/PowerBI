using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000543 RID: 1347
	[Serializable]
	internal class IntermediateFormatVersion : IPersistable
	{
		// Token: 0x0600497D RID: 18813 RVA: 0x001368C6 File Offset: 0x00134AC6
		internal IntermediateFormatVersion()
		{
			this.SetCurrent();
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x001368D4 File Offset: 0x00134AD4
		internal IntermediateFormatVersion(int major, int minor, int build)
		{
			this.m_major = major;
			this.m_minor = minor;
			this.m_build = build;
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x001368F4 File Offset: 0x00134AF4
		static IntermediateFormatVersion()
		{
			int majorVersion = PersistenceConstants.MajorVersion;
			int minorVersion = PersistenceConstants.MinorVersion;
			int current_build = 0;
			RevertImpersonationContext.Run(delegate
			{
				current_build = IntermediateFormatVersion.EncodeFileVersion(FileVersionInfo.GetVersionInfo(typeof(IntermediateFormatVersion).Assembly.Location));
			});
			IntermediateFormatVersion.m_current = new IntermediateFormatVersion(majorVersion, minorVersion, current_build);
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x00136945 File Offset: 0x00134B45
		// (set) Token: 0x06004981 RID: 18817 RVA: 0x0013694D File Offset: 0x00134B4D
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

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x00136956 File Offset: 0x00134B56
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x0013695E File Offset: 0x00134B5E
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

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x00136967 File Offset: 0x00134B67
		// (set) Token: 0x06004985 RID: 18821 RVA: 0x0013696F File Offset: 0x00134B6F
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

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x00136978 File Offset: 0x00134B78
		internal bool IsOldVersion
		{
			get
			{
				return this.CompareTo(IntermediateFormatVersion.m_current.Major, IntermediateFormatVersion.m_current.Minor, IntermediateFormatVersion.m_current.Build) < 0;
			}
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x001369A4 File Offset: 0x00134BA4
		internal static IntermediateFormatVersion Current
		{
			get
			{
				return IntermediateFormatVersion.m_current;
			}
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x001369AB File Offset: 0x00134BAB
		internal static IntermediateFormatVersion SQL16
		{
			get
			{
				if (IntermediateFormatVersion.m_sql16 == null)
				{
					IntermediateFormatVersion.m_sql16 = new IntermediateFormatVersion(12, 3, 0);
				}
				return IntermediateFormatVersion.m_sql16;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x001369C7 File Offset: 0x00134BC7
		internal static IntermediateFormatVersion RTM2008
		{
			get
			{
				if (IntermediateFormatVersion.m_rtm2008 == null)
				{
					IntermediateFormatVersion.m_rtm2008 = new IntermediateFormatVersion(11, 2, 0);
				}
				return IntermediateFormatVersion.m_rtm2008;
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x001369E3 File Offset: 0x00134BE3
		internal static IntermediateFormatVersion BIRefresh
		{
			get
			{
				if (IntermediateFormatVersion.m_biRefresh == null)
				{
					IntermediateFormatVersion.m_biRefresh = new IntermediateFormatVersion(12, 1, 0);
				}
				return IntermediateFormatVersion.m_biRefresh;
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x001369FF File Offset: 0x00134BFF
		private static int EncodeFileVersion(FileVersionInfo fileVersion)
		{
			return ((fileVersion.FileMajorPart % 20 * 10 + fileVersion.FileMinorPart % 10) * 100000 + fileVersion.FileBuildPart % 100000) * 100 + fileVersion.FilePrivatePart % 100;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00136A38 File Offset: 0x00134C38
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

		// Token: 0x0600498D RID: 18829 RVA: 0x00136A9B File Offset: 0x00134C9B
		internal void SetCurrent()
		{
			this.m_major = IntermediateFormatVersion.m_current.Major;
			this.m_minor = IntermediateFormatVersion.m_current.Minor;
			this.m_build = IntermediateFormatVersion.m_current.Build;
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x00136AD0 File Offset: 0x00134CD0
		internal int CompareTo(int major, int minor, int build)
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

		// Token: 0x0600498F RID: 18831 RVA: 0x00136B18 File Offset: 0x00134D18
		internal int CompareTo(IntermediateFormatVersion version)
		{
			int num = this.Compare(this.m_major, version.Major);
			if (num == 0)
			{
				num = this.Compare(this.m_minor, version.Minor);
				if (num == 0 && this.m_major < 10)
				{
					num = this.Compare(this.m_build, version.Build);
				}
			}
			return num;
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x00136B6F File Offset: 0x00134D6F
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

		// Token: 0x06004991 RID: 18833 RVA: 0x00136B80 File Offset: 0x00134D80
		public override string ToString()
		{
			if (this.m_major < 10)
			{
				return string.Concat(new string[]
				{
					this.m_major.ToString(CultureInfo.InvariantCulture),
					".",
					this.m_minor.ToString(CultureInfo.InvariantCulture),
					".",
					this.m_build.ToString(CultureInfo.InvariantCulture)
				});
			}
			int num;
			int num2;
			int num3;
			int num4;
			IntermediateFormatVersion.DecodeFileVersion(this.m_build, out num, out num2, out num3, out num4);
			return string.Concat(new string[]
			{
				this.m_major.ToString(CultureInfo.InvariantCulture),
				".",
				this.m_minor.ToString(CultureInfo.InvariantCulture),
				".",
				num3.ToString(CultureInfo.InvariantCulture),
				".",
				num4.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00136C68 File Offset: 0x00134E68
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IntermediateFormatVersion, ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.IntermediateFormatVersionMajor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionMinor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionBuild, Token.Int32)
			});
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00136CC8 File Offset: 0x00134EC8
		public void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(IntermediateFormatVersion.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.IntermediateFormatVersionMajor:
					writer.Write(this.m_major);
					break;
				case MemberName.IntermediateFormatVersionMinor:
					writer.Write(this.m_minor);
					break;
				case MemberName.IntermediateFormatVersionBuild:
					writer.Write(this.m_build);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00136D4C File Offset: 0x00134F4C
		public void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(IntermediateFormatVersion.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.IntermediateFormatVersionMajor:
					this.m_major = reader.ReadInt32();
					break;
				case MemberName.IntermediateFormatVersionMinor:
					this.m_minor = reader.ReadInt32();
					break;
				case MemberName.IntermediateFormatVersionBuild:
					this.m_build = reader.ReadInt32();
					break;
				default:
					Global.Tracer.Assert(false, string.Format("ReportIntermediateFormat.Persistence does not support {0}.{1}.", memberName.GetType(), memberName));
					break;
				}
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00136DED File Offset: 0x00134FED
		public void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00136DFA File Offset: 0x00134FFA
		public ObjectType GetObjectType()
		{
			return ObjectType.IntermediateFormatVersion;
		}

		// Token: 0x040020A0 RID: 8352
		private int m_major;

		// Token: 0x040020A1 RID: 8353
		private int m_minor;

		// Token: 0x040020A2 RID: 8354
		private int m_build;

		// Token: 0x040020A3 RID: 8355
		[NonSerialized]
		private static readonly Declaration m_Declaration = IntermediateFormatVersion.GetDeclaration();

		// Token: 0x040020A4 RID: 8356
		[NonSerialized]
		private static readonly IntermediateFormatVersion m_current;

		// Token: 0x040020A5 RID: 8357
		[NonSerialized]
		private static IntermediateFormatVersion m_rtm2008;

		// Token: 0x040020A6 RID: 8358
		[NonSerialized]
		private static IntermediateFormatVersion m_biRefresh;

		// Token: 0x040020A7 RID: 8359
		[NonSerialized]
		private static IntermediateFormatVersion m_sql16;
	}
}
