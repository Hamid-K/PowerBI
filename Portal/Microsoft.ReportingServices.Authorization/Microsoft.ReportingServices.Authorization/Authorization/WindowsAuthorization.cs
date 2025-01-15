using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Permissions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200000C RID: 12
	[CLSCompliant(false)]
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class WindowsAuthorization : IAuthorizationExtension, IExtension
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021DC File Offset: 0x000003DC
		private IDirectoryServicesWrapper DirectoryServices { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021E5 File Offset: 0x000003E5
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021ED File Offset: 0x000003ED
		private IEnvironmentWrapper EnvironmentWrapper { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x000021F6 File Offset: 0x000003F6
		internal WindowsAuthorization(IDirectoryServicesWrapper directoryServices, IEnvironmentWrapper environmentWrapper)
		{
			this.DirectoryServices = directoryServices;
			this.EnvironmentWrapper = environmentWrapper;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000220C File Offset: 0x0000040C
		public WindowsAuthorization()
			: this(new DirectoryServicesWrapper(), new EnvironmentWrapper())
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002220 File Offset: 0x00000420
		public byte[] CreateSecurityDescriptor(AceCollection acl, SecurityItemType itemType, out string stringSecDesc)
		{
			byte[] array;
			Native.BuildAcl(new WindowsAcl(acl), itemType, out array, out stringSecDesc);
			return array;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002240 File Offset: 0x00000440
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, CatalogOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if (this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_CatOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Catalog, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022B0 File Offset: 0x000004B0
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, CatalogOperation[] requiredOperations)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				uint num = 0U;
				foreach (CatalogOperation catalogOperation in requiredOperations)
				{
					if ((catalogOperation != CatalogOperation.CreateRoles && catalogOperation != CatalogOperation.ReadRoleProperties && catalogOperation != CatalogOperation.UpdateRoleProperties) || !this.IsAdmin(userName, userToken))
					{
						num |= (uint)AuthzData.m_CatOper2PermMask[catalogOperation];
					}
				}
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Catalog, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002320 File Offset: 0x00000520
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, ReportOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if ((requiredOperation == ReportOperation.ReadProperties || requiredOperation == ReportOperation.ReadReportDefinition) && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_RptOper2PermMask[requiredOperation];
				ReportSecDescType reportSecDescType = ReportSecDescType.Primary;
				if ((ulong)requiredOperation >= (ulong)((long)AuthzConstants.MaxAceIndex))
				{
					reportSecDescType = ReportSecDescType.Secondary;
				}
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Report, secDesc, ref num, reportSecDescType);
			});
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002390 File Offset: 0x00000590
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, FolderOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if (requiredOperation == FolderOperation.ReadProperties && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_FldOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Folder, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002400 File Offset: 0x00000600
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, FolderOperation[] requiredOperations)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				uint num = 0U;
				foreach (FolderOperation folderOperation in requiredOperations)
				{
					if (folderOperation != FolderOperation.ReadProperties || !this.IsAdmin(userName, userToken))
					{
						num |= (uint)AuthzData.m_FldOper2PermMask[folderOperation];
					}
				}
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Folder, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002470 File Offset: 0x00000670
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, ResourceOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if (requiredOperation == ResourceOperation.ReadProperties && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_ResOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Resource, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E0 File Offset: 0x000006E0
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, ResourceOperation[] requiredOperations)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				uint num = 0U;
				foreach (ResourceOperation resourceOperation in requiredOperations)
				{
					if (resourceOperation != ResourceOperation.ReadProperties || !this.IsAdmin(userName, userToken))
					{
						num |= (uint)AuthzData.m_ResOper2PermMask[resourceOperation];
					}
				}
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Resource, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002550 File Offset: 0x00000750
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, DatasourceOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if ((requiredOperation == DatasourceOperation.ReadProperties || requiredOperation == DatasourceOperation.ReadContent) && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_DSOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Datasource, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C0 File Offset: 0x000007C0
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, ModelOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if (requiredOperation == ModelOperation.ReadProperties && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_ModelOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.Model, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002630 File Offset: 0x00000830
		public bool CheckAccess(string userName, IntPtr userToken, byte[] secDesc, ModelItemOperation requiredOperation)
		{
			bool result = false;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				if (requiredOperation == ModelItemOperation.ReadProperties && this.IsAdmin(userName, userToken))
				{
					result = true;
					return;
				}
				uint num = (uint)AuthzData.m_ModelItemOper2PermMask[requiredOperation];
				result = this.InnerCheckAccess(userName, userToken, SecurityItemType.ModelItem, secDesc, ref num, ReportSecDescType.Primary);
			});
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A0 File Offset: 0x000008A0
		public StringCollection GetPermissions(string userName, IntPtr userToken, SecurityItemType itemType, byte[] secDesc)
		{
			StringCollection permissions = null;
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			RevertImpersonationContext.Run(delegate
			{
				Hashtable hashtable;
				SdAndType.PrepareToRestoreBinaryPolicy(secDesc, out hashtable);
				try
				{
					if (WebRequestUtil.IsClientLocal() || userToken == IntPtr.Zero)
					{
						permissions = AuthzData.SecDesc2Operations(userName, itemType, ref hashtable);
					}
				}
				catch (Exception ex)
				{
					this.TraceAuthenticateByAliasError(userName, ex);
				}
				if (permissions == null && userToken != IntPtr.Zero)
				{
					permissions = AuthzData.SecDesc2Operations(userToken, itemType, ref hashtable);
				}
			});
			return permissions;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002050 File Offset: 0x00000250
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002052 File Offset: 0x00000252
		public string LocalizedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002710 File Offset: 0x00000910
		internal bool InnerCheckAccess(string userName, IntPtr userToken, SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, ReportSecDescType rptSecDescType)
		{
			RSTrace.SecurityTracer.Assert(secDesc != null, "secDesc != null");
			Hashtable hashtable;
			SdAndType.PrepareToRestoreBinaryPolicy(secDesc, out hashtable);
			byte[] array;
			byte[] array2;
			SdAndType.GetRightSecDesc(itemType, hashtable, out array, out array2);
			if (rptSecDescType == ReportSecDescType.Primary)
			{
				if (this.EnvironmentWrapper.IsClientLocal() || userToken == IntPtr.Zero)
				{
					try
					{
						return this.DirectoryServices.CheckAccess(itemType, array, ref rightsMask, userName);
					}
					catch (Exception ex)
					{
						this.TraceAuthenticateByAliasError(userName, ex);
					}
				}
				return this.DirectoryServices.CheckAccess(itemType, array, ref rightsMask, userToken);
			}
			if (rptSecDescType == ReportSecDescType.Secondary)
			{
				if (this.EnvironmentWrapper.IsClientLocal() || userToken == IntPtr.Zero)
				{
					try
					{
						return this.DirectoryServices.CheckAccess(itemType, array2, ref rightsMask, userName);
					}
					catch (Exception ex2)
					{
						this.TraceAuthenticateByAliasError(userName, ex2);
					}
				}
				return this.DirectoryServices.CheckAccess(itemType, array2, ref rightsMask, userToken);
			}
			throw new InternalCatalogException("Invalid security descriptor type.");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002810 File Offset: 0x00000A10
		private bool IsAdmin(string userName, IntPtr userToken)
		{
			if (!this.m_IsAdminComputed)
			{
				this.m_IsAdmin = this.IsAdminImpl(userName, userToken);
				this.m_IsAdminComputed = true;
			}
			return this.m_IsAdmin;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002838 File Offset: 0x00000A38
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal bool IsAdminImpl(string userName, IntPtr userToken)
		{
			if (this.EnvironmentWrapper.IsClientLocal() || userToken == IntPtr.Zero)
			{
				try
				{
					return this.DirectoryServices.IsAdmin(userName);
				}
				catch (Exception ex)
				{
					this.TraceAuthenticateByAliasError(userName, ex);
				}
			}
			return this.DirectoryServices.IsAdmin(userToken);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002898 File Offset: 0x00000A98
		private void TraceAuthenticateByAliasError(string userName, Exception ex)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Unable to authenticate {0} by alias. The Windows domain controller may be unavailable - attempting to authenticate using UserToken.", new object[] { userName });
			RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, ex.Message);
		}

		// Token: 0x04000039 RID: 57
		private bool m_IsAdmin;

		// Token: 0x0400003A RID: 58
		private bool m_IsAdminComputed;
	}
}
