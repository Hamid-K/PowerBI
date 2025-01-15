using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AnalysisServices.AdomdClient.Interop;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000112 RID: 274
	internal sealed class NTAuthenticationSession : Disposable
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x00034ACC File Offset: 0x00032CCC
		public NTAuthenticationSession(SecurityMode mode, string remotePeerId, string packageName, NTAuthenticationConfiguration configuration)
		{
			this.remotePeerId = remotePeerId;
			this.package = NTAuthenticationSession.GetSecurityPackage(packageName);
			this.requirements = NTAuthenticationSession.CalculateRequirements(mode, this.package, configuration);
			this.mode = mode;
			this.credentialsHandle = NTAuthenticationSession.InitializeCredentialsHandle(this.package, configuration);
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00034B20 File Offset: 0x00032D20
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x00034B28 File Offset: 0x00032D28
		public bool IsHandshakeComplete { get; private set; }

		// Token: 0x06000F54 RID: 3924 RVA: 0x00034B34 File Offset: 0x00032D34
		public SecurityBuffer GetFirstOutgoingToken()
		{
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(SecurityBufferType.Token, this.package.MaxTokenLength)
			};
			SecurityContextRequirements securityContextRequirements;
			int num = SspiHelper.InitializeSecurityContext(this.credentialsHandle, this.remotePeerId, this.requirements, SecurityDataRepresentation.Network, out this.contextHandle, array, out securityContextRequirements);
			this.IsHandshakeComplete = this.contextHandle.IsInvalid || num == 0;
			if (this.IsHandshakeComplete)
			{
				this.EnsureRequirementsWereObtained(securityContextRequirements);
			}
			return array[0];
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00034BAC File Offset: 0x00032DAC
		public SecurityBuffer GetNextOutgoingToken(byte[] incomingToken)
		{
			if ((incomingToken == null || incomingToken.Length == 0) && !this.contextHandle.IsInvalid)
			{
				this.IsHandshakeComplete = true;
				return null;
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(SecurityBufferType.Token, incomingToken)
			};
			SecurityBuffer[] array2 = new SecurityBuffer[]
			{
				new SecurityBuffer(SecurityBufferType.Token, this.package.MaxTokenLength)
			};
			SecurityContextRequirements securityContextRequirements;
			int num = SspiHelper.InitializeSecurityContext(this.credentialsHandle, ref this.contextHandle, this.remotePeerId, this.requirements, SecurityDataRepresentation.Network, array, array2, out securityContextRequirements);
			this.IsHandshakeComplete = this.contextHandle.IsInvalid || num == 0;
			if (this.IsHandshakeComplete)
			{
				this.EnsureRequirementsWereObtained(securityContextRequirements);
			}
			return array2[0];
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00034C51 File Offset: 0x00032E51
		public SecurityContext ObtainSecurityContext()
		{
			return new SecurityContext(this.mode, ref this.credentialsHandle, ref this.contextHandle);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00034C6A File Offset: 0x00032E6A
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00034C8C File Offset: 0x00032E8C
		private static SecurityPackageInfo GetSecurityPackage(string packageName)
		{
			for (int i = 0; i < NTAuthenticationSession.SupportedSecurityPackages.Length; i++)
			{
				if (string.Compare(NTAuthenticationSession.SupportedSecurityPackages[i].Name, packageName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return NTAuthenticationSession.SupportedSecurityPackages[i];
				}
			}
			throw new SspiAuthenticationException(SspiAuthenticationError.InvalidPackageName, packageName);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00034CD0 File Offset: 0x00032ED0
		private static SecurityContextRequirements CalculateRequirements(SecurityMode mode, SecurityPackageInfo package, NTAuthenticationConfiguration configuration)
		{
			SecurityContextRequirements securityContextRequirements = SecurityContextRequirements.None;
			if (configuration.IsSChannel)
			{
				if (mode != SecurityMode.Block)
				{
					if (mode != SecurityMode.Stream)
					{
						throw new SspiAuthenticationException(string.Format("Invalid security-mode: '{0}' is not a valid TLS mode!", mode));
					}
					NTAuthenticationSession.EnsurePackageCapabilitySupport(package, SecurityPackageCapabilities.Stream);
					securityContextRequirements |= SecurityContextRequirements.Stream;
				}
				else
				{
					NTAuthenticationSession.EnsurePackageCapabilitySupport(package, SecurityPackageCapabilities.Connection);
					securityContextRequirements |= SecurityContextRequirements.Connection;
				}
				ImpersonationLevel impersonationLevel = configuration.ImpersonationLevel;
				if (impersonationLevel > ImpersonationLevel.Impersonate)
				{
					throw new SspiAuthenticationException(SspiAuthenticationError.InvalidConfiguration);
				}
				if (configuration.ProtectionLevel != ProtectionLevel.Privacy)
				{
					throw new SspiAuthenticationException(SspiAuthenticationError.InvalidConfiguration);
				}
				securityContextRequirements |= SecurityContextRequirements.ReplayDetect | SecurityContextRequirements.SequenceDetect | SecurityContextRequirements.Confidentiality | SecurityContextRequirements.UseSuppliedCreds | SecurityContextRequirements.Integrity;
			}
			else
			{
				securityContextRequirements |= SecurityContextRequirements.MutualAuth | SecurityContextRequirements.Connection;
				switch (configuration.ImpersonationLevel)
				{
				case ImpersonationLevel.Identify:
					securityContextRequirements |= SecurityContextRequirements.Identity;
					break;
				case ImpersonationLevel.Impersonate:
					NTAuthenticationSession.EnsurePackageCapabilitySupport(package, SecurityPackageCapabilities.Impersonation);
					break;
				case ImpersonationLevel.Delegate:
					securityContextRequirements |= SecurityContextRequirements.Delegate;
					break;
				}
				switch (configuration.ProtectionLevel)
				{
				case ProtectionLevel.Connection:
					securityContextRequirements |= SecurityContextRequirements.NoIntegrity;
					break;
				case ProtectionLevel.Integrity:
					NTAuthenticationSession.EnsurePackageCapabilitySupport(package, SecurityPackageCapabilities.Integrity);
					securityContextRequirements |= SecurityContextRequirements.ReplayDetect | SecurityContextRequirements.SequenceDetect | SecurityContextRequirements.Integrity;
					break;
				case ProtectionLevel.Privacy:
					NTAuthenticationSession.EnsurePackageCapabilitySupport(package, SecurityPackageCapabilities.Privacy);
					securityContextRequirements |= SecurityContextRequirements.ReplayDetect | SecurityContextRequirements.SequenceDetect | SecurityContextRequirements.Confidentiality | SecurityContextRequirements.Integrity;
					break;
				default:
					throw new SspiAuthenticationException(string.Format("Invalid protection-level: '{0}' is not a valid level!", configuration.ProtectionLevel));
				}
			}
			return securityContextRequirements;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00034E08 File Offset: 0x00033008
		private static SecHandle InitializeCredentialsHandle(SecurityPackageInfo package, NTAuthenticationConfiguration configuration)
		{
			SecHandle secHandle;
			if (configuration.IsSChannel)
			{
				if (string.IsNullOrEmpty(configuration.CertificateThumbprint))
				{
					SspiHelper.AcquireCredentialsHandle(null, SecurityCredentialUsage.Outbount, out secHandle);
				}
				else
				{
					if (configuration.ImpersonationLevel == ImpersonationLevel.Anonymous)
					{
						throw new SspiAuthenticationException(SspiAuthenticationError.InvalidConfiguration);
					}
					SspiHelper.AcquireCredentialsHandle(CertUtils.LoadCertificateByThumbprint(configuration.CertificateThumbprint, true), SecurityCredentialUsage.Outbount, out secHandle);
				}
			}
			else
			{
				if (configuration.ImpersonationLevel == ImpersonationLevel.Anonymous)
				{
					if (!NativeMethods.ImpersonateAnonymousToken(NativeMethods.GetCurrentThread()))
					{
						throw new Win32Exception();
					}
					try
					{
						SspiHelper.AcquireCredentialsHandle(package.Name, SecurityCredentialUsage.Outbount, out secHandle);
						return secHandle;
					}
					finally
					{
						if (!NativeMethods.RevertToSelf())
						{
							Process.GetCurrentProcess().Kill();
						}
					}
				}
				SspiHelper.AcquireCredentialsHandle(package.Name, SecurityCredentialUsage.Outbount, out secHandle);
			}
			return secHandle;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00034EBC File Offset: 0x000330BC
		private static void EnsurePackageCapabilitySupport(SecurityPackageInfo package, SecurityPackageCapabilities capabilities)
		{
			SecurityPackageCapabilities securityPackageCapabilities = package.Capabilities & capabilities;
			if (securityPackageCapabilities != capabilities)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.MissingCapability, package.Name, capabilities & ~securityPackageCapabilities);
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00034EE8 File Offset: 0x000330E8
		private static void EnsureContextRequirementsWereObtainedIfRequested(SecurityContextRequirements requirements, SecurityContextRequirements attributies, SecurityContextRequirements flags)
		{
			SecurityContextRequirements securityContextRequirements = requirements & flags;
			if (securityContextRequirements != SecurityContextRequirements.None && (attributies & securityContextRequirements) != securityContextRequirements)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.RequirementNotObtained, securityContextRequirements);
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00034F0C File Offset: 0x0003310C
		private void EnsureRequirementsWereObtained(SecurityContextRequirements attributies)
		{
			if (this.mode == SecurityMode.Stream)
			{
				NTAuthenticationSession.EnsureContextRequirementsWereObtainedIfRequested(this.requirements, attributies, SecurityContextRequirements.Stream);
			}
			NTAuthenticationSession.EnsureContextRequirementsWereObtainedIfRequested(this.requirements, attributies, SecurityContextRequirements.MutualAuth);
			NTAuthenticationSession.EnsureContextRequirementsWereObtainedIfRequested(this.requirements, attributies, SecurityContextRequirements.ReplayDetect);
			NTAuthenticationSession.EnsureContextRequirementsWereObtainedIfRequested(this.requirements, attributies, SecurityContextRequirements.SequenceDetect);
			NTAuthenticationSession.EnsureContextRequirementsWereObtainedIfRequested(this.requirements, attributies, SecurityContextRequirements.Confidentiality);
		}

		// Token: 0x04000908 RID: 2312
		private static readonly SecurityPackageInfo[] SupportedSecurityPackages = SecurityPackageInfo.EnumerateSecurityPackages();

		// Token: 0x04000909 RID: 2313
		private readonly string remotePeerId;

		// Token: 0x0400090A RID: 2314
		private readonly SecurityPackageInfo package;

		// Token: 0x0400090B RID: 2315
		private readonly SecurityContextRequirements requirements;

		// Token: 0x0400090C RID: 2316
		private readonly SecurityMode mode;

		// Token: 0x0400090D RID: 2317
		private SecHandle credentialsHandle;

		// Token: 0x0400090E RID: 2318
		private SecHandle contextHandle;
	}
}
