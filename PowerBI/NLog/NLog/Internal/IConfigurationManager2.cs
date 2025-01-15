using System;
using System.Configuration;

namespace NLog.Internal
{
	// Token: 0x0200011E RID: 286
	internal interface IConfigurationManager2 : IConfigurationManager
	{
		// Token: 0x06000ECC RID: 3788
		ConnectionStringSettings LookupConnectionString(string name);
	}
}
