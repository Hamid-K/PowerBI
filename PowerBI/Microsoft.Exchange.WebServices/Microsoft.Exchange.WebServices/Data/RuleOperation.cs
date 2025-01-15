using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000099 RID: 153
	public abstract class RuleOperation : ComplexProperty
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00018B88 File Offset: 0x00017B88
		internal RuleOperation()
		{
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000729 RID: 1833
		internal abstract string XmlElementName { get; }
	}
}
