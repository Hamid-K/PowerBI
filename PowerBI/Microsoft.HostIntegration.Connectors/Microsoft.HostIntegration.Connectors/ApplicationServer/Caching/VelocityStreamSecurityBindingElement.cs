using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Principal;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C5 RID: 709
	internal class VelocityStreamSecurityBindingElement : StreamUpgradeBindingElement, ISecurityProvider
	{
		// Token: 0x06001A38 RID: 6712 RVA: 0x0004F328 File Offset: 0x0004D528
		public BindingElementCollection InitializeCommunication(BindingElementCollection collection)
		{
			int num = collection.Count - 1;
			collection.Insert(num, this);
			collection.Insert(0, new EndPointIdentityBindingElement(new ServerIdentityProvider(this._configurationManager)));
			return collection;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0004F360 File Offset: 0x0004D560
		public VelocityStreamSecurityBindingElement(ServiceConfigurationManager configurationManager, bool isClientToServerChannel)
		{
			this._isClientToServerChannel = isClientToServerChannel;
			this._configurationManager = configurationManager;
			this._element = new WindowsStreamSecurityBindingElement();
			if (this._configurationManager.AdvancedProperties.SecurityProperties.DataCacheProtectionLevel == DataCacheProtectionLevel.None)
			{
				this._element.ProtectionLevel = ProtectionLevel.None;
				return;
			}
			if (this._configurationManager.AdvancedProperties.SecurityProperties.DataCacheProtectionLevel == DataCacheProtectionLevel.Sign)
			{
				this._element.ProtectionLevel = ProtectionLevel.Sign;
				return;
			}
			if (this._configurationManager.AdvancedProperties.SecurityProperties.DataCacheProtectionLevel == DataCacheProtectionLevel.EncryptAndSign)
			{
				this._element.ProtectionLevel = ProtectionLevel.EncryptAndSign;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0004F3FC File Offset: 0x0004D5FC
		public RemoteAuthorization GetRemoteAuthorization(WindowsPrincipal principal)
		{
			RemoteAuthorization remoteAuthorization = RemoteAuthorization.Unauthorized;
			if (principal.IsInRole(WindowsBuiltInRole.Administrator))
			{
				remoteAuthorization = RemoteAuthorization.Admin;
			}
			else
			{
				foreach (object obj in this._configurationManager.AdvancedProperties.SecurityProperties.Authorization)
				{
					AllowElement allowElement = (AllowElement)obj;
					if (principal.IsInRole(allowElement.Users))
					{
						remoteAuthorization = RemoteAuthorization.Client;
						break;
					}
				}
			}
			return remoteAuthorization;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0004F460 File Offset: 0x0004D660
		public void Authorize(WindowsIdentity identity)
		{
			bool flag = false;
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(identity);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.ServerChannel.Security", "Authorize {0} called", new object[] { identity.Name });
			}
			if (this._isClientToServerChannel)
			{
				flag = this.GetRemoteAuthorization(windowsPrincipal) != RemoteAuthorization.Unauthorized;
			}
			if (!flag && !this._isClientToServerChannel && windowsPrincipal.IsInRole(WindowsIdentity.GetCurrent().User))
			{
				flag = true;
			}
			if (!flag)
			{
				List<IHostConfiguration> listOfHosts = this._configurationManager.GetListOfHosts();
				for (int i = 0; i < listOfHosts.Count; i++)
				{
					if (windowsPrincipal.IsInRole(listOfHosts[i].Account))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.ServerChannel.Security", "Authorize {0} succeeded, Protection Level={1}", new object[]
					{
						identity.Name,
						this._element.ProtectionLevel
					});
					return;
				}
			}
			else
			{
				SecurityUtil.ThrowSecurityAccessDeniedException(this._isClientToServerChannel, identity.Name, null);
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0004F563 File Offset: 0x0004D763
		protected VelocityStreamSecurityBindingElement(VelocityStreamSecurityBindingElement elementToBeCloned)
			: base(elementToBeCloned)
		{
			this._configurationManager = elementToBeCloned._configurationManager;
			this._element = (WindowsStreamSecurityBindingElement)elementToBeCloned._element.Clone();
			this._isClientToServerChannel = elementToBeCloned._isClientToServerChannel;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0004F59A File Offset: 0x0004D79A
		public bool IsClientToServerChannel
		{
			get
			{
				return this._isClientToServerChannel;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0004F5A2 File Offset: 0x0004D7A2
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x0004F5AF File Offset: 0x0004D7AF
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this._element.ProtectionLevel;
			}
			set
			{
				this._element.ProtectionLevel = value;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0004F5BD File Offset: 0x0004D7BD
		public override BindingElement Clone()
		{
			return new VelocityStreamSecurityBindingElement(this);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0004F5C5 File Offset: 0x0004D7C5
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be null");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0004F5E7 File Offset: 0x0004D7E7
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be null");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0004F609 File Offset: 0x0004D809
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be null");
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0004F62B File Offset: 0x0004D82B
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentException("Context cannot be null");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0004F64D File Offset: 0x0004D84D
		public override StreamUpgradeProvider BuildClientStreamUpgradeProvider(BindingContext context)
		{
			return new VelocityStreamUpgradeProvider(this, context, (StreamSecurityUpgradeProvider)this._element.BuildClientStreamUpgradeProvider(context));
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0004F667 File Offset: 0x0004D867
		public override StreamUpgradeProvider BuildServerStreamUpgradeProvider(BindingContext context)
		{
			return new VelocityStreamUpgradeProvider(this, context, (StreamSecurityUpgradeProvider)this._element.BuildServerStreamUpgradeProvider(context));
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0004F681 File Offset: 0x0004D881
		public override T GetProperty<T>(BindingContext context)
		{
			return this._element.GetProperty<T>(context);
		}

		// Token: 0x04000E02 RID: 3586
		private ServiceConfigurationManager _configurationManager;

		// Token: 0x04000E03 RID: 3587
		private WindowsStreamSecurityBindingElement _element;

		// Token: 0x04000E04 RID: 3588
		private bool _isClientToServerChannel;
	}
}
