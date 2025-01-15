using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000029 RID: 41
	internal sealed class Lifetime
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000819E File Offset: 0x0000639E
		private Lifetime(int addedVersion, int removedVersion)
		{
			this.m_addedVersion = addedVersion;
			this.m_removedVersion = removedVersion;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000081B4 File Offset: 0x000063B4
		public int AddedVersion
		{
			get
			{
				return this.m_addedVersion;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000081BC File Offset: 0x000063BC
		public bool HasAddedVersion
		{
			get
			{
				return this.m_addedVersion != 0;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000081C7 File Offset: 0x000063C7
		public int RemovedVersion
		{
			get
			{
				return this.m_removedVersion;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000081CF File Offset: 0x000063CF
		public bool HasRemovedVersion
		{
			get
			{
				return this.m_removedVersion != 0;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000081DC File Offset: 0x000063DC
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

		// Token: 0x060001BF RID: 447 RVA: 0x0000821C File Offset: 0x0000641C
		public static Lifetime AddedIn(int addedVersion)
		{
			Global.Tracer.Assert(addedVersion > 0, "Invalid addedVersion");
			return new Lifetime(addedVersion, 0);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008238 File Offset: 0x00006438
		public static Lifetime RemovedIn(int removedVersion)
		{
			Global.Tracer.Assert(removedVersion > 0, "Invalid addedVersion");
			return new Lifetime(0, removedVersion);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008254 File Offset: 0x00006454
		public static Lifetime Spanning(int addedVersion, int removedVersion)
		{
			Global.Tracer.Assert(addedVersion > 0, "Invalid addedVersion");
			Global.Tracer.Assert(removedVersion > 0, "Invalid removedVersion");
			Global.Tracer.Assert(removedVersion > addedVersion, "removedVersion must be later than addedVersion");
			return new Lifetime(addedVersion, removedVersion);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000082A1 File Offset: 0x000064A1
		public static Lifetime Unspecified
		{
			get
			{
				return Lifetime.UnspecifiedInstance;
			}
		}

		// Token: 0x0400012B RID: 299
		private readonly int m_addedVersion;

		// Token: 0x0400012C RID: 300
		private readonly int m_removedVersion;

		// Token: 0x0400012D RID: 301
		private static readonly Lifetime UnspecifiedInstance = new Lifetime(0, 0);
	}
}
