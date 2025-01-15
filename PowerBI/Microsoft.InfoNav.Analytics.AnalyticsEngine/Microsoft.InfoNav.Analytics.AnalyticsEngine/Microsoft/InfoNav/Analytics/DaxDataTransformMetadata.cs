using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000010 RID: 16
	internal sealed class DaxDataTransformMetadata : IDaxDataTransformMetadata
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000266D File Offset: 0x0000086D
		internal DaxDataTransformMetadata(string functionName, IReadOnlyList<IDaxDataTransformParameterMetadata> parameters)
		{
			this._functionName = functionName;
			this._parameters = parameters;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002683 File Offset: 0x00000883
		public string DaxFunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000268B File Offset: 0x0000088B
		public IReadOnlyList<IDaxDataTransformParameterMetadata> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly string _functionName;

		// Token: 0x04000054 RID: 84
		private readonly IReadOnlyList<IDaxDataTransformParameterMetadata> _parameters;
	}
}
