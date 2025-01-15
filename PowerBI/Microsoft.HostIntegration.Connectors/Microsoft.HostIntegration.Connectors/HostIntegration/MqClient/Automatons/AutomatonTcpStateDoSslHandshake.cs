using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD3 RID: 2771
	internal class AutomatonTcpStateDoSslHandshake : StateAsCodeDriver
	{
		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0016BAA9 File Offset: 0x00169CA9
		public override string Name
		{
			get
			{
				return this.stateNumber.ToString();
			}
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0016BABC File Offset: 0x00169CBC
		internal AutomatonTcpStateDoSslHandshake(AutomatonTcp driver, AutomatonTcpContext contextToUse)
			: base(driver)
		{
			this.automaton = driver;
			this.context = contextToUse;
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0016BADC File Offset: 0x00169CDC
		public override int Process(ref int eventToProcess)
		{
			AutomatonTcpEvent automatonTcpEvent = (AutomatonTcpEvent)eventToProcess;
			AutomatonTcpState automatonTcpState = this.stateNumber;
			try
			{
				while (automatonTcpState == this.stateNumber && automatonTcpEvent != AutomatonTcpEvent.Stop)
				{
					AutomatonTcpEvent automatonTcpEvent2;
					int num;
					switch (automatonTcpEvent)
					{
					case AutomatonTcpEvent.Connected:
						this.ActionSetWaitConnectEvent();
						automatonTcpState = AutomatonTcpState.DataTransferSsl;
						automatonTcpEvent2 = AutomatonTcpEvent.StartTransfer;
						num = 2;
						break;
					case AutomatonTcpEvent.DataReceived:
					case AutomatonTcpEvent.Failed:
						goto IL_0067;
					case AutomatonTcpEvent.SslFailed:
						this.ActionCloseClient();
						automatonTcpState = AutomatonTcpState.FailedConnect;
						automatonTcpEvent2 = AutomatonTcpEvent.SslFailed;
						num = 3;
						break;
					case AutomatonTcpEvent.StartHandshake:
						this.ActionGetSslAndAuthenticate();
						if (this.PostConditionSucceeded())
						{
							automatonTcpEvent2 = AutomatonTcpEvent.Connected;
							num = 0;
						}
						else
						{
							automatonTcpEvent2 = AutomatonTcpEvent.SslFailed;
							num = 1;
						}
						break;
					default:
						goto IL_0067;
					}
					automatonTcpEvent = automatonTcpEvent2;
					if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
					{
						this.context.TracePoint.Trace(TraceFlags.Information, AutomatonTcpStateDoSslHandshake.traceLines[num]);
						continue;
					}
					continue;
					IL_0067:
					throw new InvalidOperationException("Invalid Event Number: " + eventToProcess.ToString(CultureInfo.InvariantCulture));
				}
			}
			catch (Exception ex)
			{
				InvalidProgramException ex2 = new InvalidProgramException("State: DoSslHandshake, Event: " + automatonTcpEvent.ToString() + " - Exception: " + ex.Message, ex);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			eventToProcess = (int)automatonTcpEvent;
			return (int)automatonTcpState;
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x0016BC18 File Offset: 0x00169E18
		public void ActionGetSslAndAuthenticate()
		{
			try
			{
				this.context.SslStream = new SslStream(this.context.TcpClient.GetStream(), false, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), new LocalCertificateSelectionCallback(AutomatonTcpStateDoSslHandshake.SelectLocalCertificate), EncryptionPolicy.RequireEncryption);
				SslProtocols sslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
				bool flag = true;
				this.context.SslStream.AuthenticateAsClient(this.context.ConnectionParameters.Server, this.context.ConnectionParameters.CertificateCollection, sslProtocols, flag);
				if (this.context.TracePoint.IsEnabled(TraceFlags.Information))
				{
					this.context.TracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "Server Cert: {0}", this.context.SslStream.RemoteCertificate.Subject));
					this.context.TracePoint.Trace(TraceFlags.Information, string.Format(CultureInfo.InvariantCulture, "SslProtocol: {0}", this.context.SslStream.SslProtocol.ToString()));
				}
			}
			catch (AuthenticationException ex)
			{
				this.context.TcpConnectException = ex;
			}
			catch (InvalidOperationException ex2)
			{
				this.context.TcpConnectException = ex2;
			}
			catch (IOException ex3)
			{
				this.context.TcpConnectException = ex3;
			}
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x0016BD7C File Offset: 0x00169F7C
		public void ActionSetWaitConnectEvent()
		{
			this.context.ConnectedEvent.Set();
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0016BD8F File Offset: 0x00169F8F
		public void ActionCloseClient()
		{
			this.context.TcpClient.Close();
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0016BDA1 File Offset: 0x00169FA1
		public bool PostConditionSucceeded()
		{
			return this.context.TcpConnectException == null;
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x0016BDB4 File Offset: 0x00169FB4
		public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			List<string> serverCertificateThumbprints = this.context.ConnectionParameters.ServerCertificateThumbprints;
			if (serverCertificateThumbprints != null && serverCertificateThumbprints.Count != 0)
			{
				string certHashString = certificate.GetCertHashString();
				if (serverCertificateThumbprints.Contains(certHashString))
				{
					if (this.context.TracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this.context.TracePoint.Trace(TraceFlags.Verbose, string.Format(CultureInfo.InvariantCulture, "Server Cert Thumbprint: {0} is allowed", certHashString));
					}
					return true;
				}
				if (this.context.TracePoint.IsEnabled(TraceFlags.Error))
				{
					this.context.TracePoint.Trace(TraceFlags.Error, string.Format(CultureInfo.InvariantCulture, "Server Cert Thumbprint: {0} was not found", certHashString));
				}
			}
			if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) > SslPolicyErrors.None)
			{
				if (chain == null || chain.ChainStatus == null || chain.ChainStatus.Length == 0)
				{
					return false;
				}
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if (x509ChainStatus.Status != X509ChainStatusFlags.RevocationStatusUnknown && x509ChainStatus.Status != X509ChainStatusFlags.OfflineRevocation)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x0016BEBC File Offset: 0x0016A0BC
		public static X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			if (localCertificates != null && localCertificates.Count > 0)
			{
				if (acceptableIssuers != null && acceptableIssuers.Length != 0)
				{
					using (X509CertificateCollection.X509CertificateEnumerator enumerator = localCertificates.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							X509Certificate x509Certificate = enumerator.Current;
							string issuer = x509Certificate.Issuer;
							if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
							{
								return x509Certificate;
							}
						}
						goto IL_0061;
					}
				}
				return localCertificates[0];
			}
			IL_0061:
			return null;
		}

		// Token: 0x040044A0 RID: 17568
		private AutomatonTcpState stateNumber = AutomatonTcpState.DoSslHandshake;

		// Token: 0x040044A1 RID: 17569
		private AutomatonTcp automaton;

		// Token: 0x040044A2 RID: 17570
		private AutomatonTcpContext context;

		// Token: 0x040044A3 RID: 17571
		private static string[] traceLines = new string[] { "State: DoSslHandshake, Evt: StartHandshake, Act: GetSslAndAuthenticate, Post: Succeeded, Evt: Connected", "State: DoSslHandshake, Evt: StartHandshake, Act: GetSslAndAuthenticate, Evt: SslFailed", "State: DoSslHandshake, Evt: Connected, Act: SetWaitConnectEvent, State: DataTransferSsl, Evt: StartTransfer", "State: DoSslHandshake, Evt: SslFailed, Act: CloseClient, State: FailedConnect, Evt: SslFailed" };
	}
}
