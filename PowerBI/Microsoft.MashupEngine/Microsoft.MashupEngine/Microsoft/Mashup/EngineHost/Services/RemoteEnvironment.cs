using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A81 RID: 6785
	internal class RemoteEnvironment : IRemoteServiceFactory
	{
		// Token: 0x0600AB1D RID: 43805 RVA: 0x00234A80 File Offset: 0x00232C80
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			proxyInitArgs.WriteString(Thread.CurrentThread.CurrentCulture.Name);
			proxyInitArgs.WriteString(Thread.CurrentThread.CurrentUICulture.Name);
			proxyInitArgs.WriteInt32(this.GetDisabledSecurityProtocols(engineHost));
			proxyInitArgs.WriteBool(this.GetEnableTls13Separately(engineHost));
			proxyInitArgs.WriteBool(this.GetUseSystemDefaultTlsSettings(engineHost));
			proxyInitArgs.WriteBool(this.GetCertificateValidationEnabled(engineHost));
			proxyInitArgs.WriteBool(this.GetRejectUnknownRevocationCertificates(engineHost));
			return EmptyStub.Instance;
		}

		// Token: 0x0600AB1E RID: 43806 RVA: 0x00234B00 File Offset: 0x00232D00
		private int GetDisabledSecurityProtocols(IEngineHost engineHost)
		{
			IConfigurationPropertyService configurationPropertyService = engineHost.QueryService<IConfigurationPropertyService>();
			object obj;
			if (configurationPropertyService != null && configurationPropertyService.Values.TryGetValue("DisabledSecurityProtocols", out obj) && Convert.GetTypeCode(obj) == TypeCode.Int32)
			{
				return (int)obj;
			}
			return -1;
		}

		// Token: 0x0600AB1F RID: 43807 RVA: 0x00234B40 File Offset: 0x00232D40
		private bool GetCertificateValidationEnabled(IEngineHost engineHost)
		{
			bool flag = true;
			return this.GetBooleanValue(engineHost, "CertificateValidationEnabled", flag);
		}

		// Token: 0x0600AB20 RID: 43808 RVA: 0x00234B5C File Offset: 0x00232D5C
		private bool GetEnableTls13Separately(IEngineHost engineHost)
		{
			bool flag = true;
			return this.GetBooleanValue(engineHost, "EnableTls13Separately", flag);
		}

		// Token: 0x0600AB21 RID: 43809 RVA: 0x00234B78 File Offset: 0x00232D78
		private bool GetRejectUnknownRevocationCertificates(IEngineHost engineHost)
		{
			bool flag = false;
			return this.GetBooleanValue(engineHost, "RejectOnUnknownRevocation", flag);
		}

		// Token: 0x0600AB22 RID: 43810 RVA: 0x00234B94 File Offset: 0x00232D94
		private bool GetUseSystemDefaultTlsSettings(IEngineHost engineHost)
		{
			bool flag = false;
			return this.GetBooleanValue(engineHost, "UseSystemDefaultTlsSettings", flag);
		}

		// Token: 0x0600AB23 RID: 43811 RVA: 0x00234BB0 File Offset: 0x00232DB0
		private bool GetBooleanValue(IEngineHost engineHost, string propertyName, bool defaultValue)
		{
			IConfigurationPropertyService configurationPropertyService = engineHost.QueryService<IConfigurationPropertyService>();
			object obj;
			if (configurationPropertyService != null && configurationPropertyService.Values.TryGetValue(propertyName, out obj) && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600AB24 RID: 43812 RVA: 0x00234BE8 File Offset: 0x00232DE8
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			CultureInfo cultureInfo = new CultureInfo(proxyInitArgs.ReadString());
			CultureInfo cultureInfo2 = new CultureInfo(proxyInitArgs.ReadString());
			int num = proxyInitArgs.ReadInt32();
			bool flag = proxyInitArgs.ReadBoolean();
			bool flag2 = proxyInitArgs.ReadBoolean();
			RemoteEnvironment.SetSecurityProtocols(engineHost, num, flag, flag2);
			bool flag3 = proxyInitArgs.ReadBool();
			bool flag4 = proxyInitArgs.ReadBool();
			if (flag3)
			{
				CertificateManager.EnableEvaluationContainerCertificateValidation(flag4);
			}
			else
			{
				CertificateManager.DisableEvaluationContainerCertificateValidation(flag4);
			}
			if (!RemoteEnvironment.initialized)
			{
				RemoteEnvironment.initialized = true;
				ServicePointManager.DefaultConnectionLimit = 64;
			}
			return new RemoteEnvironment.Proxy(cultureInfo, cultureInfo2);
		}

		// Token: 0x0600AB25 RID: 43813 RVA: 0x00234C64 File Offset: 0x00232E64
		private static void SetSecurityProtocols(IEngineHost engineHost, int threadDisabledSecurityProtocols, bool enableTls13Separately, bool useSystemDefault)
		{
			if (RemoteEnvironment.initialized && threadDisabledSecurityProtocols == RemoteEnvironment.disabledSecurityProtocols)
			{
				return;
			}
			RemoteEnvironment.disabledSecurityProtocols = threadDisabledSecurityProtocols;
			SecurityProtocolType securityProtocolType = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			if (RemoteEnvironment.disabledSecurityProtocols != -1)
			{
				securityProtocolType &= (SecurityProtocolType)(~(SecurityProtocolType)RemoteEnvironment.disabledSecurityProtocols);
			}
			if (enableTls13Separately)
			{
				RemoteEnvironment.SetSecurityProtocol(engineHost, securityProtocolType);
				securityProtocolType |= SecurityProtocolType.Tls13;
				if (RemoteEnvironment.disabledSecurityProtocols != -1)
				{
					securityProtocolType &= (SecurityProtocolType)(~(SecurityProtocolType)RemoteEnvironment.disabledSecurityProtocols);
				}
				RemoteEnvironment.SetSecurityProtocol(engineHost, securityProtocolType);
				return;
			}
			securityProtocolType |= SecurityProtocolType.Tls13;
			RemoteEnvironment.SetSecurityProtocol(engineHost, securityProtocolType);
		}

		// Token: 0x0600AB26 RID: 43814 RVA: 0x00234CE4 File Offset: 0x00232EE4
		private static void SetSecurityProtocol(IEngineHost engineHost, SecurityProtocolType securityProtocolType)
		{
			try
			{
				ServicePointManager.SecurityProtocol = securityProtocolType;
			}
			catch (NotSupportedException)
			{
				using (EvaluatorTracing.CreateTrace("RemoteEnvironment/SetSecurityProtocolTypeFailed", engineHost, TraceEventType.Verbose, null))
				{
				}
			}
		}

		// Token: 0x040058C0 RID: 22720
		private const int defaultConnectionLimit = 64;

		// Token: 0x040058C1 RID: 22721
		private const int Tls11 = 768;

		// Token: 0x040058C2 RID: 22722
		private const int Tls12 = 3072;

		// Token: 0x040058C3 RID: 22723
		private const int Tls13 = 12288;

		// Token: 0x040058C4 RID: 22724
		private static bool initialized;

		// Token: 0x040058C5 RID: 22725
		private static int disabledSecurityProtocols;

		// Token: 0x02001A82 RID: 6786
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AB28 RID: 43816 RVA: 0x00234D34 File Offset: 0x00232F34
			public Proxy(CultureInfo threadCulture, CultureInfo threadUICulture)
			{
				this.originalThreadCulture = Thread.CurrentThread.CurrentCulture;
				this.originalThreadUiCulture = Thread.CurrentThread.CurrentUICulture;
				Thread.CurrentThread.CurrentCulture = threadCulture;
				Thread.CurrentThread.CurrentUICulture = threadUICulture;
			}

			// Token: 0x0600AB29 RID: 43817 RVA: 0x00234D74 File Offset: 0x00232F74
			T IEngineHost.QueryService<T>()
			{
				return default(T);
			}

			// Token: 0x0600AB2A RID: 43818 RVA: 0x00234D8A File Offset: 0x00232F8A
			public void Dispose()
			{
				if (this.originalThreadCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = this.originalThreadCulture;
					Thread.CurrentThread.CurrentUICulture = this.originalThreadUiCulture;
					this.originalThreadCulture = null;
					this.originalThreadUiCulture = null;
				}
			}

			// Token: 0x040058C6 RID: 22726
			private CultureInfo originalThreadCulture;

			// Token: 0x040058C7 RID: 22727
			private CultureInfo originalThreadUiCulture;
		}
	}
}
