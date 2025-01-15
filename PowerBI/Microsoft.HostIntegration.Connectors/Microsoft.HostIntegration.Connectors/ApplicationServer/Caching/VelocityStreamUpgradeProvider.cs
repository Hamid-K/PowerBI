using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C4 RID: 708
	internal class VelocityStreamUpgradeProvider : StreamSecurityUpgradeProvider
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x0004F26D File Offset: 0x0004D46D
		public VelocityStreamUpgradeProvider(VelocityStreamSecurityBindingElement bindingElement, BindingContext context, StreamSecurityUpgradeProvider innerProvider)
			: base(context.Binding)
		{
			this._bindingElement = bindingElement;
			this._innerProvider = innerProvider;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0004F289 File Offset: 0x0004D489
		public override StreamUpgradeAcceptor CreateUpgradeAcceptor()
		{
			return new VelocityStreamSecurityUpgradeAcceptor(this._bindingElement, (StreamSecurityUpgradeAcceptor)this._innerProvider.CreateUpgradeAcceptor());
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0004F2A6 File Offset: 0x0004D4A6
		public override StreamUpgradeInitiator CreateUpgradeInitiator(EndpointAddress remoteAddress, Uri via)
		{
			return this._innerProvider.CreateUpgradeInitiator(remoteAddress, via);
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0004F2B5 File Offset: 0x0004D4B5
		public override EndpointIdentity Identity
		{
			get
			{
				return this._innerProvider.Identity;
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0004F2C2 File Offset: 0x0004D4C2
		protected override void OnOpen(TimeSpan timeout)
		{
			this._innerProvider.Open(timeout);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0004F2D0 File Offset: 0x0004D4D0
		protected override void OnAbort()
		{
			this._innerProvider.Abort();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0004F2DD File Offset: 0x0004D4DD
		protected override void OnClose(TimeSpan timeout)
		{
			this._innerProvider.Close();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0004F2EA File Offset: 0x0004D4EA
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerProvider.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0004F2FA File Offset: 0x0004D4FA
		protected override void OnEndOpen(IAsyncResult result)
		{
			this._innerProvider.EndOpen(result);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0004F308 File Offset: 0x0004D508
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerProvider.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0004F318 File Offset: 0x0004D518
		protected override void OnEndClose(IAsyncResult result)
		{
			this._innerProvider.EndClose(result);
		}

		// Token: 0x04000E00 RID: 3584
		private VelocityStreamSecurityBindingElement _bindingElement;

		// Token: 0x04000E01 RID: 3585
		private StreamSecurityUpgradeProvider _innerProvider;
	}
}
