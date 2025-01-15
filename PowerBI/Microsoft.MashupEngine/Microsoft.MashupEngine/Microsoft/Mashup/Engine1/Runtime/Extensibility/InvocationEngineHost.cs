using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x02001710 RID: 5904
	internal abstract class InvocationEngineHost : IEngineHost, ICredentialService, IResourcePermissionService, IQueryPermissionService, ICurrentEnvironmentService
	{
		// Token: 0x0600960C RID: 38412 RVA: 0x001F147E File Offset: 0x001EF67E
		protected InvocationEngineHost(IEngineHost engineHost, IEngine engine)
		{
			this.engineHost = engineHost;
			this.engine = engine;
		}

		// Token: 0x0600960D RID: 38413 RVA: 0x001F1494 File Offset: 0x001EF694
		public static InvocationEngineHost New(IEngineHost engineHost, IEngine engine, ResourceKindInfo resourceKindInfo, IResource resource, ResourceCredentialCollection credentials, bool allowChaining)
		{
			return new InvocationEngineHost.DataSourceInvocationHost(engineHost, engine, resourceKindInfo, resource, credentials, allowChaining);
		}

		// Token: 0x0600960E RID: 38414 RVA: 0x001F14A3 File Offset: 0x001EF6A3
		public static InvocationEngineHost New(IEngineHost engineHost, IEngine engine)
		{
			return new InvocationEngineHost.NonDataSourceInvocationHost(engineHost, engine);
		}

		// Token: 0x0600960F RID: 38415 RVA: 0x001F14AC File Offset: 0x001EF6AC
		public static IEnumerable<IResource> GetDataSourceChain(IEngineHost leafEngineHost, IResource resource)
		{
			if (resource != null)
			{
				yield return resource;
			}
			if (leafEngineHost == null)
			{
				yield break;
			}
			IExtensibilityService extensibilityService = leafEngineHost.QueryService<IExtensibilityService>();
			while (extensibilityService != null)
			{
				IResource thisResource = extensibilityService.CurrentResource;
				if (resource == null || (thisResource != null && !ResourceEqualityComparer.Instance.Equals(resource, thisResource)))
				{
					yield return thisResource;
					resource = thisResource;
				}
				InvocationEngineHost.DataSourceInvocationHost dataSourceInvocationHost = extensibilityService as InvocationEngineHost.DataSourceInvocationHost;
				if (dataSourceInvocationHost != null)
				{
					extensibilityService = dataSourceInvocationHost.EngineHost.QueryService<IExtensibilityService>();
				}
				else
				{
					extensibilityService = null;
				}
				thisResource = null;
			}
			yield break;
		}

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x06009610 RID: 38416 RVA: 0x001F14C3 File Offset: 0x001EF6C3
		public IRecordValue Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x06009611 RID: 38417 RVA: 0x001F14CB File Offset: 0x001EF6CB
		public void SetCredentialHandler(FunctionValue handler)
		{
			this.credentialsHandler = handler;
		}

		// Token: 0x06009612 RID: 38418 RVA: 0x001F14D4 File Offset: 0x001EF6D4
		public void SetPermissionHandler(FunctionValue handler)
		{
			this.permissionsHandler = handler;
		}

		// Token: 0x06009613 RID: 38419 RVA: 0x001F14DD File Offset: 0x001EF6DD
		public void SetEnvironment(IRecordValue environment)
		{
			this.environment = environment;
		}

		// Token: 0x06009614 RID: 38420 RVA: 0x001F14E8 File Offset: 0x001EF6E8
		public virtual T QueryService<T>() where T : class
		{
			if (typeof(T) == typeof(ICredentialService) || typeof(T) == typeof(IResourcePermissionService) || typeof(T) == typeof(IQueryPermissionService))
			{
				return (T)((object)this);
			}
			return this.engineHost.QueryService<T>();
		}

		// Token: 0x06009615 RID: 38421
		public abstract bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials);

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x06009616 RID: 38422 RVA: 0x001F1558 File Offset: 0x001EF758
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06009617 RID: 38423
		public abstract ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false);

		// Token: 0x06009618 RID: 38424 RVA: 0x000091AE File Offset: 0x000073AE
		public void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection updatedCredential)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009619 RID: 38425 RVA: 0x001F1560 File Offset: 0x001EF760
		public bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
		{
			return this.IsResourceAccessPermitted(resource, out credentials);
		}

		// Token: 0x0600961A RID: 38426 RVA: 0x001F156C File Offset: 0x001EF76C
		public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			if (this.permissionsHandler != null)
			{
				RecordValue recordValue;
				if (!PermissionConversion.TryConvertToRecord(resource, type, query, parameterCount, parameterNames, out recordValue))
				{
					return false;
				}
				try
				{
					Value value = this.permissionsHandler.Invoke(recordValue);
					if (!value.IsNull)
					{
						return value.AsLogical.AsBoolean;
					}
				}
				catch (ValueException)
				{
					return false;
				}
			}
			return HostResourceQueryPermissionService.IsQueryExecutionPermitted(this.engineHost, resource, type, query, parameterCount, parameterNames);
		}

		// Token: 0x0600961B RID: 38427
		protected abstract bool IsSupportedDataSource(string resourceKind);

		// Token: 0x0600961C RID: 38428 RVA: 0x001F15E4 File Offset: 0x001EF7E4
		protected bool TryHandleCredentials(IResource resource, out ResourceCredentialCollection credentials)
		{
			if (this.credentialsHandler != null)
			{
				RecordValue recordValue = null;
				if (this.IsSupportedDataSource(resource.Kind) && InvocationEngineHost.TryGetCredentials(this.credentialsHandler, resource, out recordValue))
				{
					if (CredentialConversion.TryConvertFromRecord(this.engine, resource, recordValue, out credentials))
					{
						return true;
					}
					throw new UnpermittedResourceAccessException(resource, Strings.Extensibility_InvalidCredentialType, null, null);
				}
			}
			credentials = null;
			return false;
		}

		// Token: 0x0600961D RID: 38429 RVA: 0x001F1642 File Offset: 0x001EF842
		private static RecordValue CreateResourceRecord(IResource resource)
		{
			return RecordValue.New(Keys.New("DataSource.Kind"), new Value[] { TextValue.New(resource.Kind) });
		}

		// Token: 0x0600961E RID: 38430 RVA: 0x001F1668 File Offset: 0x001EF868
		protected static bool TryGetCredentials(FunctionValue function, IResource resource, out RecordValue credential)
		{
			bool flag;
			try
			{
				Value value = function.Invoke(InvocationEngineHost.CreateResourceRecord(resource));
				if (value.IsNull)
				{
					credential = null;
					flag = false;
				}
				else
				{
					credential = value.AsRecord;
					flag = true;
				}
			}
			catch (ValueException ex)
			{
				throw new UnpermittedResourceAccessException(resource, ex.MessageString, null, ex);
			}
			return flag;
		}

		// Token: 0x04004FC9 RID: 20425
		private readonly IEngineHost engineHost;

		// Token: 0x04004FCA RID: 20426
		private readonly IEngine engine;

		// Token: 0x04004FCB RID: 20427
		private FunctionValue credentialsHandler;

		// Token: 0x04004FCC RID: 20428
		private FunctionValue permissionsHandler;

		// Token: 0x04004FCD RID: 20429
		private IRecordValue environment;

		// Token: 0x02001711 RID: 5905
		private sealed class DataSourceInvocationHost : InvocationEngineHost, IExtensibilityService, IRedirectPolicyService
		{
			// Token: 0x0600961F RID: 38431 RVA: 0x001F16C0 File Offset: 0x001EF8C0
			public DataSourceInvocationHost(IEngineHost engineHost, IEngine engine, ResourceKindInfo resourceKindInfo, IResource resource, ResourceCredentialCollection credentials, bool allowChaining)
				: base(engineHost, engine)
			{
				this.resourceKindInfo = resourceKindInfo;
				this.resource = resource;
				this.credentials = credentials;
				this.allowChaining = allowChaining;
			}

			// Token: 0x1700273F RID: 10047
			// (get) Token: 0x06009620 RID: 38432 RVA: 0x001F16E9 File Offset: 0x001EF8E9
			IResource IExtensibilityService.CurrentResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17002740 RID: 10048
			// (get) Token: 0x06009621 RID: 38433 RVA: 0x001F16F1 File Offset: 0x001EF8F1
			ResourceCredentialCollection IExtensibilityService.CurrentCredentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x17002741 RID: 10049
			// (get) Token: 0x06009622 RID: 38434 RVA: 0x001F16F9 File Offset: 0x001EF8F9
			public bool ImpersonateResource
			{
				get
				{
					return !this.allowChaining;
				}
			}

			// Token: 0x17002742 RID: 10050
			// (get) Token: 0x06009623 RID: 38435 RVA: 0x00002139 File Offset: 0x00000339
			bool IRedirectPolicyService.Legacy
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002743 RID: 10051
			// (get) Token: 0x06009624 RID: 38436 RVA: 0x001F1704 File Offset: 0x001EF904
			private bool AllowCompositeConnectors
			{
				get
				{
					if (this.allowCompositeConnectors == null)
					{
						this.allowCompositeConnectors = new bool?(base.EngineHost.GetConfigurationProperty("AllowCompositeConnectors", false));
					}
					return this.allowCompositeConnectors.Value;
				}
			}

			// Token: 0x17002744 RID: 10052
			// (get) Token: 0x06009625 RID: 38437 RVA: 0x001F173C File Offset: 0x001EF93C
			private bool IsTrustedModule
			{
				get
				{
					if (this.isTrustedModule == null)
					{
						ILibraryService libraryService = base.EngineHost.QueryService<ILibraryService>();
						string text;
						if (libraryService != null && ExtensionModule.TryGetModuleName(this.engineHost, out text))
						{
							ModuleTrustLevel trustLevel = libraryService.GetTrustLevel(text);
							this.isTrustedModule = new bool?(trustLevel == ModuleTrustLevel.FirstParty || trustLevel == ModuleTrustLevel.Certified);
						}
						else
						{
							this.isTrustedModule = new bool?(false);
						}
					}
					return this.isTrustedModule.Value;
				}
			}

			// Token: 0x06009626 RID: 38438 RVA: 0x001F17AC File Offset: 0x001EF9AC
			public override T QueryService<T>()
			{
				if (typeof(T) == typeof(IExtensibilityService) || typeof(T) == typeof(IRedirectPolicyService))
				{
					return (T)((object)this);
				}
				return base.QueryService<T>();
			}

			// Token: 0x06009627 RID: 38439 RVA: 0x001F17FC File Offset: 0x001EF9FC
			public override bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
			{
				if (base.TryHandleCredentials(resource, out credentials))
				{
					return true;
				}
				if (resource.Kind == this.resource.Kind && this.resourceKindInfo.IsSubset(this.resource.Path, resource.Path))
				{
					credentials = this.credentials;
					return true;
				}
				if (this.allowChaining)
				{
					return base.EngineHost.QueryService<IResourcePermissionService>().IsResourceAccessPermitted(resource, out credentials);
				}
				credentials = null;
				return false;
			}

			// Token: 0x06009628 RID: 38440 RVA: 0x001F1874 File Offset: 0x001EFA74
			public void RefreshCredential(bool forceRefresh)
			{
				this.RefreshCredential(this.resource, forceRefresh);
			}

			// Token: 0x06009629 RID: 38441 RVA: 0x001F1884 File Offset: 0x001EFA84
			public override ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
			{
				IResource resource2 = (this.allowChaining ? this.resource : resource);
				this.credentials = base.EngineHost.QueryService<ICredentialService>().RefreshCredential(resource2, forceRefresh);
				if (!this.allowChaining)
				{
					return this.credentials;
				}
				ResourceCredentialCollection resourceCredentialCollection;
				if (this.IsResourceAccessPermitted(resource, out resourceCredentialCollection))
				{
					return resourceCredentialCollection;
				}
				throw new UnpermittedResourceAccessException(resource, null, null, null);
			}

			// Token: 0x0600962A RID: 38442 RVA: 0x001F18E0 File Offset: 0x001EFAE0
			protected override bool IsSupportedDataSource(string resourceKind)
			{
				return resourceKind == this.resource.Kind || resourceKind == "AzureBlobs" || resourceKind == "AzureDataLakeStorage" || resourceKind == "SQL" || resourceKind == "MySql" || resourceKind == "AdoDotNet" || resourceKind == "Web" || resourceKind == "AnalysisServices" || resourceKind == "File" || resourceKind == "Folder" || resourceKind == "Lakehouse" || this.AllowCompositeConnectors || this.IsTrustedModule;
			}

			// Token: 0x04004FCE RID: 20430
			private const string compositeCredentialFeature = "AllowCompositeConnectors";

			// Token: 0x04004FCF RID: 20431
			private readonly ResourceKindInfo resourceKindInfo;

			// Token: 0x04004FD0 RID: 20432
			private readonly IResource resource;

			// Token: 0x04004FD1 RID: 20433
			private readonly bool allowChaining;

			// Token: 0x04004FD2 RID: 20434
			private ResourceCredentialCollection credentials;

			// Token: 0x04004FD3 RID: 20435
			private bool? isTrustedModule;

			// Token: 0x04004FD4 RID: 20436
			private bool? allowCompositeConnectors;
		}

		// Token: 0x02001712 RID: 5906
		private sealed class NonDataSourceInvocationHost : InvocationEngineHost
		{
			// Token: 0x0600962B RID: 38443 RVA: 0x001F1997 File Offset: 0x001EFB97
			public NonDataSourceInvocationHost(IEngineHost engineHost, IEngine engine)
				: base(engineHost, engine)
			{
			}

			// Token: 0x0600962C RID: 38444 RVA: 0x001F19A1 File Offset: 0x001EFBA1
			public override bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
			{
				if (base.TryHandleCredentials(resource, out credentials))
				{
					return true;
				}
				throw new UnpermittedResourceAccessException(resource, Strings.Extensibility_NotAvailable, null, null);
			}

			// Token: 0x0600962D RID: 38445 RVA: 0x000091AE File Offset: 0x000073AE
			public override ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600962E RID: 38446 RVA: 0x001F19C1 File Offset: 0x001EFBC1
			protected override bool IsSupportedDataSource(string resourceKind)
			{
				return resourceKind == "AdoDotNet";
			}
		}
	}
}
