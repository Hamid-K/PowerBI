using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001D1 RID: 465
	public sealed class DaxCapabilities
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x0003E40E File Offset: 0x0003C60E
		internal DaxCapabilities(HashSet<DaxFunctionKind> supportedFunctions, HashSet<ModelCapabilitiesKind> supportedCapabilities)
		{
			this._supportedFunctions = supportedFunctions;
			this._supportedCapabilities = supportedCapabilities;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0003E424 File Offset: 0x0003C624
		public bool IsSupported(DaxFunctionKind function)
		{
			return this._supportedFunctions.Contains(function);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0003E432 File Offset: 0x0003C632
		public bool IsSupported(ModelCapabilitiesKind capability)
		{
			return this._supportedCapabilities.Contains(capability);
		}

		// Token: 0x04000C0A RID: 3082
		private readonly HashSet<DaxFunctionKind> _supportedFunctions;

		// Token: 0x04000C0B RID: 3083
		private readonly HashSet<ModelCapabilitiesKind> _supportedCapabilities;
	}
}
