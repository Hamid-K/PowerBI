using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization.Internal
{
	// Token: 0x020000E7 RID: 231
	internal class ApplicationRootMethodInvoker
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00017941 File Offset: 0x00015B41
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00017949 File Offset: 0x00015B49
		public IApplicationRootHost ApplicationRootHost { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00017952 File Offset: 0x00015B52
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001795A File Offset: 0x00015B5A
		public ApplicationRoot ApplicationRoot { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00017963 File Offset: 0x00015B63
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001796B File Offset: 0x00015B6B
		private ActivityFactory ActivityFactory { get; set; }

		// Token: 0x06000695 RID: 1685 RVA: 0x00017974 File Offset: 0x00015B74
		public ApplicationRootMethodInvoker([NotNull] IApplicationRootHost applicationRootHost, [NotNull] ApplicationRoot applicationRoot, [NotNull] string name)
		{
			Ensure.ArgNotNull<IApplicationRootHost>(applicationRootHost, "applicationRootHost");
			Ensure.ArgNotNull<ApplicationRoot>(applicationRoot, "applicationRoot");
			Ensure.ArgNotNullOrEmpty(name, "name");
			this.ApplicationRootHost = applicationRootHost;
			this.ApplicationRoot = applicationRoot;
			string text = name + ".ActivityFactory";
			this.ActivityFactory = new ActivityFactory(text);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000179D0 File Offset: 0x00015BD0
		public void Initialize(string[] args, ApplicationSwitchesTypes appSwitchesTypes)
		{
			ApplicationRootInitializeActivity applicationRootInitializeActivity = new ApplicationRootInitializeActivity();
			using (this.ActivityFactory.CreateSyncActivity(applicationRootInitializeActivity))
			{
				using (new LifecycleEventTracer(this.ApplicationRoot.Name))
				{
					ApplicationRoot applicationRoot = this.ApplicationRoot;
					IApplicationRootHost applicationRootHost = this.ApplicationRootHost;
					string[] array = args;
					if (args == null)
					{
						(array = new string[1])[0] = "";
					}
					applicationRoot.Initialize(applicationRootHost, array, appSwitchesTypes);
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00017A58 File Offset: 0x00015C58
		public void Start(Action beforeOnPostStart)
		{
			ApplicationRootStartActivity applicationRootStartActivity = new ApplicationRootStartActivity();
			using (this.ActivityFactory.CreateSyncActivity(applicationRootStartActivity))
			{
				using (new LifecycleEventTracer(this.ApplicationRoot.Name))
				{
					this.ApplicationRoot.Start(beforeOnPostStart);
				}
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public void Stop()
		{
			ApplicationRootStopActivity applicationRootStopActivity = new ApplicationRootStopActivity();
			using (this.ActivityFactory.CreateSyncActivity(applicationRootStopActivity))
			{
				using (new LifecycleEventTracer(this.ApplicationRoot.Name))
				{
					this.ApplicationRoot.Stop();
				}
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00017B38 File Offset: 0x00015D38
		public void WaitForStopToComplete()
		{
			ApplicationRootWaitForStopToCompleteActivity applicationRootWaitForStopToCompleteActivity = new ApplicationRootWaitForStopToCompleteActivity();
			using (this.ActivityFactory.CreateSyncActivity(applicationRootWaitForStopToCompleteActivity))
			{
				using (new LifecycleEventTracer(this.ApplicationRoot.Name))
				{
					this.ApplicationRoot.WaitForStopToComplete();
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00017BA8 File Offset: 0x00015DA8
		public void Shutdown()
		{
			ApplicationRootShutdownActivity applicationRootShutdownActivity = new ApplicationRootShutdownActivity();
			using (this.ActivityFactory.CreateSyncActivity(applicationRootShutdownActivity))
			{
				using (new LifecycleEventTracer(this.ApplicationRoot.Name))
				{
					this.ApplicationRoot.Shutdown();
				}
			}
		}
	}
}
