using System;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C3 RID: 707
	internal class VelocityStreamSecurityUpgradeAcceptor : StreamSecurityUpgradeAcceptor
	{
		// Token: 0x06001A27 RID: 6695 RVA: 0x0004F173 File Offset: 0x0004D373
		public VelocityStreamSecurityUpgradeAcceptor(VelocityStreamSecurityBindingElement bindingElement, StreamSecurityUpgradeAcceptor innerAcceptor)
		{
			this._bindingElement = bindingElement;
			this._innerAcceptor = innerAcceptor;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0004F18C File Offset: 0x0004D38C
		public override Stream AcceptUpgrade(Stream stream)
		{
			Stream stream2 = null;
			try
			{
				stream2 = this._innerAcceptor.AcceptUpgrade(stream);
			}
			catch (SecurityTokenValidationException ex)
			{
				string @string = GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "AnonymousIdentity");
				SecurityUtil.ThrowSecurityAccessDeniedException(this._bindingElement.IsClientToServerChannel, @string, ex);
			}
			SecurityMessageProperty remoteSecurity = this._innerAcceptor.GetRemoteSecurity();
			this._bindingElement.Authorize(remoteSecurity.ServiceSecurityContext.WindowsIdentity);
			return stream2;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004F204 File Offset: 0x0004D404
		public override SecurityMessageProperty GetRemoteSecurity()
		{
			return this._innerAcceptor.GetRemoteSecurity();
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0004F211 File Offset: 0x0004D411
		public override bool CanUpgrade(string contentType)
		{
			return this._innerAcceptor.CanUpgrade(contentType);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0004F21F File Offset: 0x0004D41F
		public override IAsyncResult BeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state)
		{
			return this._innerAcceptor.BeginAcceptUpgrade(stream, callback, state);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0004F230 File Offset: 0x0004D430
		public override Stream EndAcceptUpgrade(IAsyncResult result)
		{
			Stream stream = this._innerAcceptor.EndAcceptUpgrade(result);
			SecurityMessageProperty remoteSecurity = this._innerAcceptor.GetRemoteSecurity();
			this._bindingElement.Authorize(remoteSecurity.ServiceSecurityContext.WindowsIdentity);
			return stream;
		}

		// Token: 0x04000DFE RID: 3582
		private VelocityStreamSecurityBindingElement _bindingElement;

		// Token: 0x04000DFF RID: 3583
		private StreamSecurityUpgradeAcceptor _innerAcceptor;
	}
}
