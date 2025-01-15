using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023B RID: 571
	public sealed class ODataCollectionStart : ODataAnnotatable
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00041E8D File Offset: 0x0004008D
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00041E95 File Offset: 0x00040095
		public string Name { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00041E9E File Offset: 0x0004009E
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00041EA6 File Offset: 0x000400A6
		internal ODataCollectionStartSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataCollectionStartSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000697 RID: 1687
		private ODataCollectionStartSerializationInfo serializationInfo;
	}
}
