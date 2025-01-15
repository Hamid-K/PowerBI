using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006C RID: 108
	internal class PrimitiveTypeConverter
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000347F File Offset: 0x0000167F
		protected PrimitiveTypeConverter()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		internal virtual PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			string text = MaterializeAtom.ReadElementString(reader, true);
			if (text != null)
			{
				return new TextPrimitiveParserToken(text);
			}
			return null;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		internal virtual PrimitiveParserToken TokenizeFromText(string text)
		{
			return new TextPrimitiveParserToken(text);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		internal virtual object Parse(string text)
		{
			return text;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00006FEF File Offset: 0x000051EF
		internal virtual string ToString(object instance)
		{
			throw new NotImplementedException();
		}
	}
}
