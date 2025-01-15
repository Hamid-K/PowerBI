using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal class IntermediateFormatVersion : IPersistable
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00007BF5 File Offset: 0x00005DF5
		internal IntermediateFormatVersion()
		{
			this.SetCurrent();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007C03 File Offset: 0x00005E03
		internal IntermediateFormatVersion(int major, int minor, int build)
		{
			this.m_major = major;
			this.m_minor = minor;
			this.m_build = build;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007C20 File Offset: 0x00005E20
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007C71 File Offset: 0x00005E71
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007C79 File Offset: 0x00005E79
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007C82 File Offset: 0x00005E82
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007C8A File Offset: 0x00005E8A
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007C93 File Offset: 0x00005E93
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00007C9B File Offset: 0x00005E9B
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007CA4 File Offset: 0x00005EA4
		internal bool IsOldVersion
		{
			get
			{
				return this.CompareTo(IntermediateFormatVersion.m_current.Major, IntermediateFormatVersion.m_current.Minor, IntermediateFormatVersion.m_current.Build) < 0;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007CD0 File Offset: 0x00005ED0
		internal static IntermediateFormatVersion Current
		{
			get
			{
				return IntermediateFormatVersion.m_current;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007CD7 File Offset: 0x00005ED7
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007CF3 File Offset: 0x00005EF3
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007D0F File Offset: 0x00005F0F
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

		// Token: 0x060001A6 RID: 422 RVA: 0x00007D2B File Offset: 0x00005F2B
		private static int EncodeFileVersion(FileVersionInfo fileVersion)
		{
			return ((fileVersion.FileMajorPart % 20 * 10 + fileVersion.FileMinorPart % 10) * 100000 + fileVersion.FileBuildPart % 100000) * 100 + fileVersion.FilePrivatePart % 100;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007D64 File Offset: 0x00005F64
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

		// Token: 0x060001A8 RID: 424 RVA: 0x00007DC7 File Offset: 0x00005FC7
		internal void SetCurrent()
		{
			this.m_major = IntermediateFormatVersion.m_current.Major;
			this.m_minor = IntermediateFormatVersion.m_current.Minor;
			this.m_build = IntermediateFormatVersion.m_current.Build;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007DFC File Offset: 0x00005FFC
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

		// Token: 0x060001AA RID: 426 RVA: 0x00007E44 File Offset: 0x00006044
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

		// Token: 0x060001AB RID: 427 RVA: 0x00007E9B File Offset: 0x0000609B
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

		// Token: 0x060001AC RID: 428 RVA: 0x00007EAC File Offset: 0x000060AC
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

		// Token: 0x060001AD RID: 429 RVA: 0x00007F94 File Offset: 0x00006194
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IntermediateFormatVersion, ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.IntermediateFormatVersionMajor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionMinor, Token.Int32),
				new MemberInfo(MemberName.IntermediateFormatVersionBuild, Token.Int32)
			});
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007FE4 File Offset: 0x000061E4
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

		// Token: 0x060001AF RID: 431 RVA: 0x00008064 File Offset: 0x00006264
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

		// Token: 0x060001B0 RID: 432 RVA: 0x000080FC File Offset: 0x000062FC
		public void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008109 File Offset: 0x00006309
		public ObjectType GetObjectType()
		{
			return ObjectType.IntermediateFormatVersion;
		}

		// Token: 0x04000120 RID: 288
		private int m_major;

		// Token: 0x04000121 RID: 289
		private int m_minor;

		// Token: 0x04000122 RID: 290
		private int m_build;

		// Token: 0x04000123 RID: 291
		[NonSerialized]
		private static readonly Declaration m_Declaration = IntermediateFormatVersion.GetDeclaration();

		// Token: 0x04000124 RID: 292
		[NonSerialized]
		private static readonly IntermediateFormatVersion m_current;

		// Token: 0x04000125 RID: 293
		[NonSerialized]
		private static IntermediateFormatVersion m_rtm2008;

		// Token: 0x04000126 RID: 294
		[NonSerialized]
		private static IntermediateFormatVersion m_biRefresh;

		// Token: 0x04000127 RID: 295
		[NonSerialized]
		private static IntermediateFormatVersion m_sql16;
	}
}
