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
		// Token: 0x06000F44 RID: 3908 RVA: 0x0003479C File Offset: 0x0003299C
		public NTAuthenticationSession(SecurityMode mode, string remotePeerId, string packageName, NTAuthenticationConfiguration configuration)
		{
			this.remotePeerId = remotePeerId;
			this.package = NTAuthenticationSession.GetSecurityPackage(packageName);
			this.requirements = NTAuthenticationSession.CalculateRequirements(mode, this.package, configuration);
			this.mode = mode;
			this.credentialsHandle = NTAuthenticationSession.InitializeCredentialsHandle(this.package, configuration);
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000347F0 File Offset: 0x000329F0
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x000347F8 File Offset: 0x000329F8
		public bool IsHandshakeComplete { get; private set; }

		// Token: 0x06000F47 RID: 3911 RVA: 0x00034804 File Offset: 0x00032A04
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

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003487C File Offset: 0x00032A7C
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

		// Token: 0x06000F49 RID: 3913 RVA: 0x00034921 File Offset: 0x00032B21
		public SecurityContext ObtainSecurityContext()
		{
			return new SecurityContext(this.mode, ref this.credentialsHandle, ref this.contextHandle);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003493A File Offset: 0x00032B3A
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003495C File Offset: 0x00032B5C
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x000349A0 File Offset: 0x00032BA0
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

		// Token: 0x06000F4D RID: 3917 RVA: 0x00034AD8 File Offset: 0x00032CD8
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

		// Token: 0x06000F4E RID: 3918 RVA: 0x00034B8C File Offset: 0x00032D8C
		private static void EnsurePackageCapabilitySupport(SecurityPackageInfo package, SecurityPackageCapabilities capabilities)
		{
			SecurityPackageCapabilities securityPackageCapabilities = package.Capabilities & capabilities;
			if (securityPackageCapabilities != capabilities)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.MissingCapability, package.Name, capabilities & ~securityPackageCapabilities);
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00034BB8 File Offset: 0x00032DB8
		private static void EnsureContextRequirementsWereObtainedIfRequested(SecurityContextRequirements requirements, SecurityContextRequirements attributies, SecurityContextRequirements flags)
		{
			SecurityContextRequirements securityContextRequirements = requirements & flags;
			if (securityContextRequirements != SecurityContextRequirements.None && (attributies & securityContextRequirements) != securityContextRequirements)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.RequirementNotObtained, securityContextRequirements);
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00034BDC File Offset: 0x00032DDC
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

		// Token: 0x040008FB RID: 2299
		private static readonly SecurityPackageInfo[] SupportedSecurityPackages = SecurityPackageInfo.EnumerateSecurityPackages();

		// Token: 0x040008FC RID: 2300
		private readonly string remotePeerId;

		// Token: 0x040008FD RID: 2301
		private readonly SecurityPackageInfo package;

		// Token: 0x040008FE RID: 2302
		private readonly SecurityContextRequirements requirements;

		// Token: 0x040008FF RID: 2303
		private readonly SecurityMode mode;

		// Token: 0x04000900 RID: 2304
		private SecHandle credentialsHandle;

		// Token: 0x04000901 RID: 2305
		private SecHandle contextHandle;
	}
}
