using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000049 RID: 73
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ConversationalContext
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003F80 File Offset: 0x00002180
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003F88 File Offset: 0x00002188
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<ContextEvent> ContextEvents { get; set; }
	}
}
