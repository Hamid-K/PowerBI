using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000494 RID: 1172
	public abstract class ChannelOperationsBase<T> : IResourceOperations<T>
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000814FC File Offset: 0x0007F6FC
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x00081504 File Offset: 0x0007F704
		public IEnumerable<Type> KnownTypesList { get; private set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0008150D File Offset: 0x0007F70D
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x00081515 File Offset: 0x0007F715
		public EndpointInfo EndpointInfo { get; private set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0008151E File Offset: 0x0007F71E
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x00081526 File Offset: 0x0007F726
		public ICertificateProvider CertificateProvider { get; private set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0008152F File Offset: 0x0007F72F
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x00081537 File Offset: 0x0007F737
		public string CertificateKey { get; private set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00081540 File Offset: 0x0007F740
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x00081548 File Offset: 0x0007F748
		public ICommunicationFrameworkEventsKit EventsKit { get; private set; }

		// Token: 0x0600241A RID: 9242 RVA: 0x00081551 File Offset: 0x0007F751
		protected ChannelOperationsBase(IEnumerable<Type> knownTypesList, EndpointInfo endpointInfo, ICertificateProvider certificateProvider, string certificateKey, ICommunicationFrameworkEventsKit eventsKit)
		{
			this.KnownTypesList = knownTypesList;
			this.EndpointInfo = endpointInfo;
			this.CertificateProvider = certificateProvider;
			this.CertificateKey = certificateKey;
			this.EventsKit = eventsKit;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00081580 File Offset: 0x0007F780
		public T CreateResource([NotNull] object state)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<object>(state, "state");
			EndpointIdentifier endpointIdentifier = state as EndpointIdentifier;
			IBindingData bindingData = BindingFactory.CreateBinding(this.EndpointInfo);
			Uri uri = endpointIdentifier.Uri;
			EndpointAddress endpointAddress = (this.EndpointInfo.BindingType.Equals(BindingType.Tcp) ? new EndpointAddress(uri, EndpointIdentity.CreateSpnIdentity(this.EndpointInfo.SpnIdentityName), new AddressHeader[0]) : new EndpointAddress(uri, new AddressHeader[0]));
			ChannelFactory<T> channelFactory = new ChannelFactory<T>(bindingData.Binding, endpointAddress);
			if (this.KnownTypesList != null)
			{
				CommunicationUtilities.AddKnownTypesToEndPoint(channelFactory.Endpoint, this.KnownTypesList);
			}
			if (this.CertificateKey != null)
			{
				ClientCertificateData certificateData = this.CertificateProvider.GetCertificateData(this.CertificateKey);
				if (certificateData == null)
				{
					CommunicationFrameworkMissingCertificateException ex = new CommunicationFrameworkMissingCertificateException(this.CertificateKey);
					this.EventsKit.NotifySecurityError(ex);
					throw ex;
				}
				channelFactory.Credentials.ClientCertificate.Certificate = certificateData.ClientCertificate;
				ServicePointManager.CheckCertificateRevocationList = certificateData.CertificateOptions.HasFlag(CertificateDataOptions.VerifyServiceCertificateRevocation);
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.ValidateServerCertificate);
			}
			bindingData.AddBehaviors(channelFactory.Endpoint);
			this.ExtendWcfBehaviors(channelFactory, bindingData);
			return channelFactory.CreateChannel();
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000816C0 File Offset: 0x0007F8C0
		public void DestroyResource([NotNull] T channel)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T>(channel, "channel");
			ICommunicationObject communicationObject = channel as ICommunicationObject;
			if (communicationObject.State == CommunicationState.Faulted)
			{
				communicationObject.Abort();
				return;
			}
			try
			{
				communicationObject.Close(ChannelOperationsBase<T>.s_closeTimeout);
			}
			catch (CommunicationException ex)
			{
				ChannelOperationsBase<T>.OnChannelCloseError(ex, communicationObject);
			}
			catch (TimeoutException ex2)
			{
				ChannelOperationsBase<T>.OnChannelCloseError(ex2, communicationObject);
			}
			catch (ArgumentException ex3)
			{
				ExtendedDiagnostics.EnsureOperation(ex3.ParamName == "chars", "ICommunicationObject.Close() threw unexpected exception: {0}".FormatWithInvariantCulture(new object[] { ex3 }));
				ChannelOperationsBase<T>.OnChannelCloseError(ex3, communicationObject);
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00081770 File Offset: 0x0007F970
		public bool IsHealtyResource([NotNull] T channel)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T>(channel, "channel");
			CommunicationState state = (channel as ICommunicationObject).State;
			return state != CommunicationState.Faulted && state != CommunicationState.Closed && state != CommunicationState.Closing;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000817AA File Offset: 0x0007F9AA
		public bool HandleResourceFailure(T channel, Exception e)
		{
			return !this.IsHealtyResource(channel) || e is CommunicationException;
		}

		// Token: 0x0600241F RID: 9247
		protected abstract void ExtendWcfBehaviors(ChannelFactory channelFactory, IBindingData bindingDataData);

		// Token: 0x06002420 RID: 9248 RVA: 0x000817C0 File Offset: 0x0007F9C0
		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None || (!this.CertificateProvider.GetCertificateData(this.CertificateKey).CertificateOptions.HasFlag(CertificateDataOptions.VerifyServiceCertificateName) && sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000817F7 File Offset: 0x0007F9F7
		private static void OnChannelCloseError(Exception ex, ICommunicationObject communicationChannel)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Error closing channel: {0}", new object[] { ex });
			communicationChannel.Abort();
		}

		// Token: 0x04000CB4 RID: 3252
		private static TimeSpan s_closeTimeout = TimeSpan.FromSeconds(30.0);
	}
}
