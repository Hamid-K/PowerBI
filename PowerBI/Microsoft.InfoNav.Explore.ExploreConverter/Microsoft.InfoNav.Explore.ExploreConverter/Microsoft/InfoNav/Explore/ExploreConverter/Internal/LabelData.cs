using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200006F RID: 111
	internal sealed class LabelData
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000B9F6 File Offset: 0x00009BF6
		internal LabelData(string keyField, string labelDataField, string dataSetName)
		{
			this._keyField = keyField;
			this._labelDataField = labelDataField;
			this._dataSetName = dataSetName;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000BA13 File Offset: 0x00009C13
		public string KeyField
		{
			get
			{
				return this._keyField;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000BA1B File Offset: 0x00009C1B
		public string LabelDataField
		{
			get
			{
				return this._labelDataField;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000BA23 File Offset: 0x00009C23
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
		}

		// Token: 0x04000180 RID: 384
		private readonly string _keyField;

		// Token: 0x04000181 RID: 385
		private readonly string _labelDataField;

		// Token: 0x04000182 RID: 386
		private readonly string _dataSetName;
	}
}
