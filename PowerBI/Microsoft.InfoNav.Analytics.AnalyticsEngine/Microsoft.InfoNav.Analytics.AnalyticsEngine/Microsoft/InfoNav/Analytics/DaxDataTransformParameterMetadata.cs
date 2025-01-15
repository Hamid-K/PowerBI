using System;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000012 RID: 18
	internal sealed class DaxDataTransformParameterMetadata : IDaxDataTransformParameterMetadata
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000028A1 File Offset: 0x00000AA1
		internal DaxDataTransformParameterMetadata(string name)
		{
			this._name = name;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028B0 File Offset: 0x00000AB0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly string _name;
	}
}
