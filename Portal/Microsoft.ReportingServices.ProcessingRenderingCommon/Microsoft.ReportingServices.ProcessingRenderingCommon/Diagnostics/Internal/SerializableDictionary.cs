using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C7 RID: 199
	public sealed class SerializableDictionary : Dictionary<string, int>, IXmlSerializable
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00012BC7 File Offset: 0x00010DC7
		internal SerializableDictionary()
		{
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00012BCF File Offset: 0x00010DCF
		internal SerializableDictionary(IDictionary<string, int> dictionary)
			: base(dictionary)
		{
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00012BD8 File Offset: 0x00010DD8
		internal SerializableDictionary(IEqualityComparer<string> comparer)
			: base(comparer)
		{
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00012BE4 File Offset: 0x00010DE4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this)
			{
				writer.WriteStartElement(keyValuePair.Key);
				writer.WriteValue(keyValuePair.Value);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00012C4C File Offset: 0x00010E4C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00012C53 File Offset: 0x00010E53
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}
	}
}
