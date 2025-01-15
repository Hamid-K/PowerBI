using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029E RID: 670
	public sealed class ExtendedTestContext : Context, IDisposable
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x0003F47C File Offset: 0x0003D67C
		public ExtendedTestContext()
			: this(null, null, true)
		{
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0003F487 File Offset: 0x0003D687
		public ExtendedTestContext(MethodBase testMethod)
			: this(testMethod, testMethod.DeclaringType, true)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003F498 File Offset: 0x0003D698
		public ExtendedTestContext(MethodBase testMethod, Type testClass, bool createSyncActivity)
		{
			if (testMethod != null)
			{
				this.m_testMethod = testMethod.Name;
				testClass = testMethod.DeclaringType;
			}
			if (testClass != null)
			{
				this.m_testClass = testClass.Name;
				this.m_testAssembly = testClass.Assembly.FullName;
				this.m_testDir = testClass.Assembly.Location;
			}
			this.m_testId = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff").ToString();
			string name = Thread.CurrentThread.Name;
			if (name != null)
			{
				Match match = ExtendedTestContext.s_threadNameRegex.Match(name);
				if (!match.Success)
				{
					throw new InvalidOperationException("Current thread name does not match expected regular expression. Are we really running under MSTest?");
				}
				this.m_testName = match.Groups["testName"].Value;
				this.m_testId = match.Groups["testId"].Value;
				base.PushContextMember<string>(ExtendedTestContext.c_testMethod, this.m_testMethod);
				base.PushContextMember<string>(ExtendedTestContext.c_testClass, this.m_testClass);
				base.PushContextMember<string>(ExtendedTestContext.c_testAssembly, this.m_testAssembly);
				base.PushContextMember<string>(ExtendedTestContext.c_testDir, this.m_testDir);
				base.PushContextMember<string>(ExtendedTestContext.c_testNameKey, this.m_testName);
				base.PushContextMember<string>(ExtendedTestContext.c_testIdKey, this.m_testId);
				this.m_contextPushed = true;
			}
			if (createSyncActivity)
			{
				this.m_syncActivity = new SyncActivity(new Guid(this.m_testId), new ActivityType("TEST"));
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003F61C File Offset: 0x0003D81C
		public void Dispose()
		{
			if (this.m_syncActivity != null)
			{
				this.m_syncActivity.Dispose();
				this.m_syncActivity = null;
			}
			if (this.m_contextPushed)
			{
				base.PopContextMember<string>(ExtendedTestContext.c_testIdKey);
				base.PopContextMember<string>(ExtendedTestContext.c_testNameKey);
				base.PopContextMember<string>(ExtendedTestContext.c_testDir);
				base.PopContextMember<string>(ExtendedTestContext.c_testAssembly);
				base.PopContextMember<string>(ExtendedTestContext.c_testClass);
				base.PopContextMember<string>(ExtendedTestContext.c_testMethod);
				this.m_contextPushed = false;
			}
			ExtendedGC.CollectEverything();
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0003F69F File Offset: 0x0003D89F
		public string TestName
		{
			get
			{
				return this.m_testName;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0003F6A7 File Offset: 0x0003D8A7
		public string TestId
		{
			get
			{
				return this.m_testId;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0003F6AF File Offset: 0x0003D8AF
		public Guid ActivityId
		{
			get
			{
				return this.m_syncActivity.Activity.ActivityId;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0003F6C1 File Offset: 0x0003D8C1
		public ActivityType ActivityType
		{
			get
			{
				return this.m_syncActivity.Activity.ActivityType;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private static readonly int c_testNameKey = UtilsContext.KeyNames.Length;

		// Token: 0x040006B8 RID: 1720
		private static readonly int c_testIdKey = UtilsContext.KeyNames.Length + 1;

		// Token: 0x040006B9 RID: 1721
		private static readonly int c_testMethod = UtilsContext.KeyNames.Length + 2;

		// Token: 0x040006BA RID: 1722
		private static readonly int c_testClass = UtilsContext.KeyNames.Length + 3;

		// Token: 0x040006BB RID: 1723
		private static readonly int c_testAssembly = UtilsContext.KeyNames.Length + 4;

		// Token: 0x040006BC RID: 1724
		private static readonly int c_testDir = UtilsContext.KeyNames.Length + 5;

		// Token: 0x040006BD RID: 1725
		private static Regex s_threadNameRegex = new Regex("Agent: adapter run thread for test '(?<testName>[0-9A-Za-z_-]*)' with id '(?<testId>[0-9a-f-]*)'");

		// Token: 0x040006BE RID: 1726
		private string m_testName;

		// Token: 0x040006BF RID: 1727
		private string m_testId;

		// Token: 0x040006C0 RID: 1728
		private string m_testMethod;

		// Token: 0x040006C1 RID: 1729
		private string m_testClass;

		// Token: 0x040006C2 RID: 1730
		private string m_testAssembly;

		// Token: 0x040006C3 RID: 1731
		private string m_testDir;

		// Token: 0x040006C4 RID: 1732
		private SyncActivity m_syncActivity;

		// Token: 0x040006C5 RID: 1733
		private bool m_contextPushed;
	}
}
