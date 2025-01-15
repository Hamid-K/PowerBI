using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000036 RID: 54
	internal sealed class ODataAtomCollectionSerializer : ODataAtomPropertyAndValueSerializer
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00006EE3 File Offset: 0x000050E3
		internal ODataAtomCollectionSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006EEC File Offset: 0x000050EC
		internal void WriteCollectionStart()
		{
			base.XmlWriter.WriteStartElement("m", "value", "http://docs.oasis-open.org/odata/ns/metadata");
			base.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.OData | ODataAtomSerializer.DefaultNamespaceFlags.GeoRss | ODataAtomSerializer.DefaultNamespaceFlags.Gml);
		}
	}
}
