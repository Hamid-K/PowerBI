using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NLog.LogReceiverService
{
	// Token: 0x02000099 RID: 153
	[CollectionDataContract(ItemName = "l", Namespace = "http://nlog-project.org/ws/")]
	public class StringCollection : Collection<string>
	{
	}
}
