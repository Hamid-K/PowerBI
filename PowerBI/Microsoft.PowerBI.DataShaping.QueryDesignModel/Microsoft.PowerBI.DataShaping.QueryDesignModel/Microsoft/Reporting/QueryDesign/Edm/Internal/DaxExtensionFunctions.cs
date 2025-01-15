using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.InfoNav.Data.Contracts.Utils;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200021D RID: 541
	internal sealed class DaxExtensionFunctions
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x000444C7 File Offset: 0x000426C7
		internal DaxExtensionFunctions(XElement daxExtensionFunctionsElement)
			: this(DaxExtensionFunctions.ParseDaxExtensionFunctionsElement(daxExtensionFunctionsElement))
		{
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000444D5 File Offset: 0x000426D5
		internal DaxExtensionFunctions(IReadOnlyList<string> supportedTransformFunctions)
		{
			this.SupportedDaxExtensionFunctions = supportedTransformFunctions;
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x000444E4 File Offset: 0x000426E4
		internal IReadOnlyList<string> SupportedDaxExtensionFunctions { get; }

		// Token: 0x0600190A RID: 6410 RVA: 0x000444EC File Offset: 0x000426EC
		private static List<string> ParseDaxExtensionFunctionsElement(XElement daxExtensionFunctionsElement)
		{
			return CsdlParserUtil.ParseDaxExtensionFunctionsNode(daxExtensionFunctionsElement, Extensions.DaxExtensionFunctionElem);
		}
	}
}
