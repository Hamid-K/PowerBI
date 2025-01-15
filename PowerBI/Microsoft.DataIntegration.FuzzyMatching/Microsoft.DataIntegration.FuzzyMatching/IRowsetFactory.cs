using System;
using System.Xml;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000018 RID: 24
	public interface IRowsetFactory
	{
		// Token: 0x060000AB RID: 171
		bool TryCreate(XmlNode rowsetNode, out IRowsetDefinition rowset);
	}
}
