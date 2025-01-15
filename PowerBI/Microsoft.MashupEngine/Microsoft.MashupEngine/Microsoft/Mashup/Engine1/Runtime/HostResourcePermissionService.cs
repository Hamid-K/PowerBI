using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200132F RID: 4911
	public static class HostResourcePermissionService
	{
		// Token: 0x060081C2 RID: 33218 RVA: 0x001B8FB0 File Offset: 0x001B71B0
		public static bool IsResourceAccessPermitted(IEngineHost hostEnvironment, IResource resource, out ResourceCredentialCollection credentials)
		{
			IResourcePermissionService resourcePermissionService = hostEnvironment.QueryService<IResourcePermissionService>();
			if (resourcePermissionService != null)
			{
				return resourcePermissionService.IsResourceAccessPermitted(resource, out credentials);
			}
			credentials = null;
			return false;
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x001B8FD4 File Offset: 0x001B71D4
		public static ResourceCredentialCollection VerifyPermissionAndGetCredentials(IEngineHost hostEnvironment, IResource resource, IDataSourceLocation location = null)
		{
			return HostResourcePermissionService.VerifyPermissionAndGetCredentials(hostEnvironment, null, resource, location);
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x001B8FE0 File Offset: 0x001B71E0
		public static ResourceCredentialCollection VerifyPermissionAndGetCredentials(IEngineHost hostEnvironment, IResource origin, IResource resource, IDataSourceLocation location = null)
		{
			ResourceCredentialCollection resourceCredentialCollection;
			if (!HostResourcePermissionService.IsResourceAccessPermitted(hostEnvironment, resource, out resourceCredentialCollection))
			{
				string text = null;
				string text2 = null;
				if (location != null)
				{
					text2 = location.ToJson();
				}
				throw DataSourceException.NewUnpermittedAccessError(hostEnvironment, text, origin, text2, resource, null, null, null);
			}
			return resourceCredentialCollection;
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x001B9018 File Offset: 0x001B7218
		public static IDisposable WaitForGovernedHandle(IEngineHost hostEnvironment, IResource resource)
		{
			IConnectionGovernanceService connectionGovernanceService = hostEnvironment.QueryService<IConnectionGovernanceService>();
			if (connectionGovernanceService == null)
			{
				return new HostResourcePermissionService.DummyGovernedHandle();
			}
			IDisposable disposable;
			using (ITask<IDisposable> task = connectionGovernanceService.BeginGetGovernedHandle(resource, GlobalThreadId.CurrentThreadId))
			{
				task.Wait();
				disposable = hostEnvironment.RegisterForCleanup(task.Result);
			}
			return disposable;
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x001B9074 File Offset: 0x001B7274
		public static bool InsecureRedirects(IEngineHost hostEnvironment)
		{
			IRedirectPolicyService redirectPolicyService = hostEnvironment.QueryService<IRedirectPolicyService>();
			return redirectPolicyService != null && redirectPolicyService.Legacy;
		}

		// Token: 0x02001330 RID: 4912
		private sealed class DummyGovernedHandle : IDisposable
		{
			// Token: 0x060081C7 RID: 33223 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}
		}
	}
}
