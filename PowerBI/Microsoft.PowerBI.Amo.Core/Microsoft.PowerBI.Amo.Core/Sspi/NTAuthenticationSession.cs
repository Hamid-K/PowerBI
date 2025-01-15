using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AnalysisServices.Interop;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000107 RID: 263
	internal sealed class NTAuthenticationSession : Disposable
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x000373D0 File Offset: 0x000355D0
		public NTAuthenticationSession(SecurityMode mode, string remotePeerId, string packageName, NTAuthenticationConfiguration configuration)
		{
			this.remotePeerId = remotePeerId;
			this.package = NTAuthenticationSession.GetSecurityPackage(packageName);
			this.requirements = NTAuthenticationSession.CalculateRequirements(mode, this.package, configuration);
			this.mode = mode;
			this.credentialsHandle = NTAuthenticationSession.InitializeCredentialsHandle(this.package, configuration);
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00037424 File Offset: 0x00035624
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x0003742C File Offset: 0x0003562C
		public bool IsHandshakeComplete { get; private set; }

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00037438 File Offset: 0x00035638
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

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000374B0 File Offset: 0x000356B0
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00037555 File Offset: 0x00035755
		public SecurityContext ObtainSecurityContext()
		{
			return new SecurityContext(this.mode, ref this.credentialsHandle, ref this.contextHandle);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003756E File Offset: 0x0003576E
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00037590 File Offset: 0x00035790
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

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000375D4 File Offset: 0x000357D4
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

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003770C File Offset: 0x0003590C
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

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000377C0 File Offset: 0x000359C0
		private static void EnsurePackageCapabilitySupport(SecurityPackageInfo package, SecurityPackageCapabilities capabilities)
		{
			SecurityPackageCapabilities securityPackageCapabilities = package.Capabilities & capabilities;
			if (securityPackageCapabilities != capabilities)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.MissingCapability, package.Name, capabilities & ~securityPackageCapabilities);
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000377EC File Offset: 0x000359EC
		private static void EnsureContextRequirementsWereObtainedIfRequested(SecurityContextRequirements requirements, SecurityContextRequirements attributies, SecurityContextRequirements flags)
		{
			SecurityContextRequirements securityContextRequirements = requirements & flags;
			if (securityContextRequirements != SecurityContextRequirements.None && (attributies & securityContextRequirements) != securityContextRequirements)
			{
				throw new SspiAuthenticationException(SspiAuthenticationError.RequirementNotObtained, securityContextRequirements);
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00037810 File Offset: 0x00035A10
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

		// Token: 0x040008C1 RID: 2241
		private static readonly SecurityPackageInfo[] SupportedSecurityPackages = SecurityPackageInfo.EnumerateSecurityPackages();

		// Token: 0x040008C2 RID: 2242
		private readonly string remotePeerId;

		// Token: 0x040008C3 RID: 2243
		private readonly SecurityPackageInfo package;

		// Token: 0x040008C4 RID: 2244
		private readonly SecurityContextRequirements requirements;

		// Token: 0x040008C5 RID: 2245
		private readonly SecurityMode mode;

		// Token: 0x040008C6 RID: 2246
		private SecHandle credentialsHandle;

		// Token: 0x040008C7 RID: 2247
		private SecHandle contextHandle;
	}
}
