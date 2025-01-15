using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200045D RID: 1117
	internal interface IDuplexSession : IInputSession, IInputConnection, IOutputSession, IOutputConnection, ISession, ITransportConnection, ITransportObject
	{
	}
}
