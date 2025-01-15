using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000073 RID: 115
	internal class ODataOptionalParameter
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000E31A File Offset: 0x0000C51A
		public IReadOnlyList<IEdmOptionalParameter> OptionalParameters
		{
			get
			{
				return this._optionalParameters;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000E322 File Offset: 0x0000C522
		public void Add(IEdmOptionalParameter parameter)
		{
			this._optionalParameters.Add(parameter);
		}

		// Token: 0x040000E3 RID: 227
		private List<IEdmOptionalParameter> _optionalParameters = new List<IEdmOptionalParameter>();
	}
}
