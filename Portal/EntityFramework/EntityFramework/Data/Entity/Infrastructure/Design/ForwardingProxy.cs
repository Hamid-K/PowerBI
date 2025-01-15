using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200029D RID: 669
	internal class ForwardingProxy<T> : RealProxy
	{
		// Token: 0x06002167 RID: 8551 RVA: 0x0005DC90 File Offset: 0x0005BE90
		public ForwardingProxy(object target)
			: base(typeof(T))
		{
			this._target = (MarshalByRefObject)target;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0005DCAE File Offset: 0x0005BEAE
		public override IMessage Invoke(IMessage msg)
		{
			new MethodCallMessageWrapper((IMethodCallMessage)msg).Uri = RemotingServices.GetObjectUri(this._target);
			return RemotingServices.GetEnvoyChainForProxy(this._target).SyncProcessMessage(msg);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0005DCDC File Offset: 0x0005BEDC
		public new T GetTransparentProxy()
		{
			return (T)((object)base.GetTransparentProxy());
		}

		// Token: 0x04000B9E RID: 2974
		private readonly MarshalByRefObject _target;
	}
}
