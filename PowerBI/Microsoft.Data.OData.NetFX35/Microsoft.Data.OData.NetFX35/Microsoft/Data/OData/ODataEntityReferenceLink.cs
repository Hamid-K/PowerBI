using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025C RID: 604
	[DebuggerDisplay("{Url.OriginalString}")]
	public sealed class ODataEntityReferenceLink : ODataItem
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000473E7 File Offset: 0x000455E7
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x000473EF File Offset: 0x000455EF
		public Uri Url { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000473F8 File Offset: 0x000455F8
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00047400 File Offset: 0x00045600
		internal ODataEntityReferenceLinkSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataEntityReferenceLinkSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000711 RID: 1809
		private ODataEntityReferenceLinkSerializationInfo serializationInfo;
	}
}
