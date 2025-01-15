using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000053 RID: 83
	internal sealed class ODataAtomPayloadKindDetectionDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000C76F File Offset: 0x0000A96F
		internal ODataAtomPayloadKindDetectionDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C778 File Offset: 0x0000A978
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			base.XmlReader.DisableInStreamErrorDetection = true;
			try
			{
				if (base.XmlReader.TryReadToNextElement())
				{
					if (string.CompareOrdinal("http://www.w3.org/2005/Atom", base.XmlReader.NamespaceURI) == 0)
					{
						if (string.CompareOrdinal("entry", base.XmlReader.LocalName) == 0)
						{
							return new ODataPayloadKind[] { ODataPayloadKind.Entry };
						}
						if (base.ReadingResponse && string.CompareOrdinal("feed", base.XmlReader.LocalName) == 0)
						{
							if (base.XmlReader.XmlBaseUri != null && base.XmlReader.XmlBaseUri.AbsoluteUri.Contains("$ref"))
							{
								return new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks };
							}
							return new ODataPayloadKind[1];
						}
					}
					else
					{
						if (string.CompareOrdinal("http://docs.oasis-open.org/odata/ns/data", base.XmlReader.NamespaceURI) == 0)
						{
							return this.DetectPropertyOrCollectionPayloadKind();
						}
						if (string.CompareOrdinal("http://docs.oasis-open.org/odata/ns/metadata", base.XmlReader.NamespaceURI) == 0)
						{
							if (string.CompareOrdinal("value", base.XmlReader.LocalName) == 0)
							{
								return this.DetectPropertyOrCollectionPayloadKind();
							}
							if (base.ReadingResponse && string.CompareOrdinal("error", base.XmlReader.LocalName) == 0)
							{
								return new ODataPayloadKind[] { ODataPayloadKind.Error };
							}
							if (string.CompareOrdinal("ref", base.XmlReader.LocalName) == 0)
							{
								return new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink };
							}
						}
						else if (string.CompareOrdinal("http://www.w3.org/2007/app", base.XmlReader.NamespaceURI) == 0 && base.ReadingResponse && string.CompareOrdinal("service", base.XmlReader.LocalName) == 0)
						{
							return new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument };
						}
					}
				}
			}
			catch (XmlException)
			{
			}
			finally
			{
				base.XmlReader.DisableInStreamErrorDetection = false;
			}
			return Enumerable.Empty<ODataPayloadKind>();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		private IEnumerable<ODataPayloadKind> DetectPropertyOrCollectionPayloadKind()
		{
			string text;
			bool flag;
			base.ReadNonEntityValueAttributes(out text, out flag);
			if (flag || text != null)
			{
				return new ODataPayloadKind[] { ODataPayloadKind.Property };
			}
			EdmTypeKind nonEntityValueKind = base.GetNonEntityValueKind();
			if (nonEntityValueKind != EdmTypeKind.Collection || !base.ReadingResponse)
			{
				return new ODataPayloadKind[] { ODataPayloadKind.Property };
			}
			return new ODataPayloadKind[]
			{
				ODataPayloadKind.Property,
				ODataPayloadKind.Collection
			};
		}
	}
}
