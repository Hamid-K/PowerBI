using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200000B RID: 11
	[ImmutableObject(true)]
	internal sealed class DataTransformPlugin : IDataTransformPlugin
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002350 File Offset: 0x00000550
		internal DataTransformPlugin(IMetadataTransform metaDataTransform, IDataTransform dataTransform)
		{
			this._metaDataTransform = metaDataTransform;
			this._dataTransform = dataTransform;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002366 File Offset: 0x00000566
		public IDataTransform CreateDataTransform()
		{
			return this._dataTransform;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236E File Offset: 0x0000056E
		public IMetadataTransform CreateMetadataTransform()
		{
			return this._metaDataTransform;
		}

		// Token: 0x0400004B RID: 75
		private readonly IMetadataTransform _metaDataTransform;

		// Token: 0x0400004C RID: 76
		private readonly IDataTransform _dataTransform;
	}
}
