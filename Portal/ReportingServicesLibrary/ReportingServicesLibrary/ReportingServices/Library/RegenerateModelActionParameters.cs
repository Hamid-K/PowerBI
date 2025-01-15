using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000140 RID: 320
	internal sealed class RegenerateModelActionParameters : UpdateModelDefinitionActionParameters
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0002EE1E File Offset: 0x0002D01E
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}
	}
}
