using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009F RID: 159
	public sealed class RuleOperationErrorCollection : ComplexPropertyCollection<RuleOperationError>
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00018FAD File Offset: 0x00017FAD
		internal RuleOperationErrorCollection()
		{
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018FB5 File Offset: 0x00017FB5
		internal override RuleOperationError CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == "RuleOperationError")
			{
				return new RuleOperationError();
			}
			return null;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00018FCB File Offset: 0x00017FCB
		internal override RuleOperationError CreateDefaultComplexProperty()
		{
			return new RuleOperationError();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00018FD2 File Offset: 0x00017FD2
		internal override string GetCollectionItemXmlElementName(RuleOperationError operationError)
		{
			return "RuleOperationError";
		}
	}
}
