using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A1 RID: 161
	internal sealed class RuleErrorCollection : ComplexPropertyCollection<RuleError>
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00019111 File Offset: 0x00018111
		internal RuleErrorCollection()
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00019119 File Offset: 0x00018119
		internal override RuleError CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == "Error")
			{
				return new RuleError();
			}
			return null;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001912F File Offset: 0x0001812F
		internal override RuleError CreateDefaultComplexProperty()
		{
			return new RuleError();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00019136 File Offset: 0x00018136
		internal override string GetCollectionItemXmlElementName(RuleError ruleValidationError)
		{
			return "Error";
		}
	}
}
