using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000007 RID: 7
	internal class DynamicCredentialService : ICredentialService
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025E4 File Offset: 0x000007E4
		public DynamicCredentialService(ICredentialService credentialService, ConnectionContext connection, IEvaluationConstants evaluationConstants = null)
		{
			this.syncRoot = new object();
			this.credentialService = credentialService;
			this.connectionContext = connection;
			this.dynamicRefreshableCredentials = new Dictionary<IResource, ResourceCredentialCollection>(ResourceComparer.Instance);
			this.refreshManager = new CredentialRefreshManager();
			this.evaluationConstants = evaluationConstants;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002634 File Offset: 0x00000834
		public bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
		{
			if (this.credentialService.TryGetCredentials(resource, out credentials))
			{
				return true;
			}
			bool flag;
			try
			{
				credentials = this.connectionContext.GetCredentials(resource);
				if (credentials != null && credentials.Count > 0)
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.dynamicRefreshableCredentials[resource] = credentials;
					}
					flag = true;
				}
				else
				{
					flag = credentials != null;
				}
			}
			catch (Exception ex) when (ex is IProduceRuntimeException)
			{
				throw ((IProduceRuntimeException)ex).GetRuntimeException();
			}
			catch (Exception ex2) when (!(ex2 is RuntimeException) && SafeExceptions.IsSafeException(ex2))
			{
				ErrorException ex3 = ex2.ToErrorException();
				throw new ErrorException(ex3.Message, ex3.StackTrace, ex3.ClassName, true, true, ex3.InnerException);
			}
			return flag;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000274C File Offset: 0x0000094C
		public ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
		{
			object obj = this.syncRoot;
			ResourceCredentialCollection credentials;
			bool flag2;
			lock (obj)
			{
				flag2 = this.dynamicRefreshableCredentials.TryGetValue(resource, out credentials);
			}
			if (!flag2)
			{
				return this.credentialService.RefreshCredential(resource, forceRefresh);
			}
			return this.refreshManager.RefreshIfNeeded(credentials, forceRefresh, delegate
			{
				try
				{
					ResourceCredentialCollection resourceCredentialCollection = this.connectionContext.RefreshCredential(credentials);
					if (resourceCredentialCollection != null)
					{
						object obj2 = this.syncRoot;
						lock (obj2)
						{
							this.dynamicRefreshableCredentials[resource] = resourceCredentialCollection;
						}
						return resourceCredentialCollection;
					}
				}
				catch (Exception ex) when (ProviderTracing.TraceIsSafeException("DynamicCredentialService/RefreshCredential", ex, this.evaluationConstants, resource))
				{
				}
				return credentials;
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027E8 File Offset: 0x000009E8
		public void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection credentials)
		{
		}

		// Token: 0x0400000C RID: 12
		private readonly object syncRoot;

		// Token: 0x0400000D RID: 13
		private readonly ConnectionContext connectionContext;

		// Token: 0x0400000E RID: 14
		private readonly ICredentialService credentialService;

		// Token: 0x0400000F RID: 15
		private readonly Dictionary<IResource, ResourceCredentialCollection> dynamicRefreshableCredentials;

		// Token: 0x04000010 RID: 16
		private readonly CredentialRefreshManager refreshManager;

		// Token: 0x04000011 RID: 17
		private readonly IEvaluationConstants evaluationConstants;
	}
}
