using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000547 RID: 1351
	internal sealed class Lifetime
	{
		// Token: 0x060049A5 RID: 18853 RVA: 0x001373BE File Offset: 0x001355BE
		private Lifetime(int addedVersion, int removedVersion)
		{
			this.m_addedVersion = addedVersion;
			this.m_removedVersion = removedVersion;
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x001373D4 File Offset: 0x001355D4
		public int AddedVersion
		{
			get
			{
				return this.m_addedVersion;
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x001373DC File Offset: 0x001355DC
		public bool HasAddedVersion
		{
			get
			{
				return this.m_addedVersion != 0;
			}
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x001373E7 File Offset: 0x001355E7
		public int RemovedVersion
		{
			get
			{
				return this.m_removedVersion;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x001373EF File Offset: 0x001355EF
		public bool HasRemovedVersion
		{
			get
			{
				return this.m_removedVersion != 0;
			}
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x001373FC File Offset: 0x001355FC
		public bool IncludesVersion(int compatVersion)
		{
			if (compatVersion == 0)
			{
				return true;
			}
			bool flag = this.m_addedVersion == 0 || this.m_addedVersion <= compatVersion;
			bool flag2 = this.m_removedVersion == 0 || this.m_removedVersion > compatVersion;
			return flag && flag2;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0013743C File Offset: 0x0013563C
		public static Lifetime AddedIn(int addedVersion)
		{
			Global.Tracer.Assert(addedVersion > 0, "Invalid addedVersion");
			return new Lifetime(addedVersion, 0);
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00137458 File Offset: 0x00135658
		public static Lifetime RemovedIn(int removedVersion)
		{
			Global.Tracer.Assert(removedVersion > 0, "Invalid addedVersion");
			return new Lifetime(0, removedVersion);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00137474 File Offset: 0x00135674
		public static Lifetime Spanning(int addedVersion, int removedVersion)
		{
			Global.Tracer.Assert(addedVersion > 0, "Invalid addedVersion");
			Global.Tracer.Assert(removedVersion > 0, "Invalid removedVersion");
			Global.Tracer.Assert(removedVersion > addedVersion, "removedVersion must be later than addedVersion");
			return new Lifetime(addedVersion, removedVersion);
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x001374C1 File Offset: 0x001356C1
		public static Lifetime Unspecified
		{
			get
			{
				return Lifetime.UnspecifiedInstance;
			}
		}

		// Token: 0x040020AB RID: 8363
		private readonly int m_addedVersion;

		// Token: 0x040020AC RID: 8364
		private readonly int m_removedVersion;

		// Token: 0x040020AD RID: 8365
		private static readonly Lifetime UnspecifiedInstance = new Lifetime(0, 0);
	}
}
