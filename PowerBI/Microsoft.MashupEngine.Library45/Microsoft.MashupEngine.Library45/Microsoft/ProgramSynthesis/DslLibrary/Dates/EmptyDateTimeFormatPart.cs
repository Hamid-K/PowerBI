using System;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200081B RID: 2075
	public class EmptyDateTimeFormatPart : ConstantDateTimeFormatPart
	{
		// Token: 0x06002CB4 RID: 11444 RVA: 0x0007DD48 File Offset: 0x0007BF48
		private EmptyDateTimeFormatPart()
			: base(string.Empty)
		{
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x0007DD55 File Offset: 0x0007BF55
		public static EmptyDateTimeFormatPart Instance { get; } = new EmptyDateTimeFormatPart();

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0007DD5C File Offset: 0x0007BF5C
		public override XElement RenderXML()
		{
			return new XElement("EmptyFormatPart", new XCData(string.Empty));
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0007DD77 File Offset: 0x0007BF77
		public new static EmptyDateTimeFormatPart TryParseFromXML(XElement literal)
		{
			if (!(literal.Name == "EmptyFormatPart"))
			{
				return null;
			}
			return new EmptyDateTimeFormatPart();
		}

		// Token: 0x0400155F RID: 5471
		internal const string EmptyPartXMLName = "EmptyFormatPart";
	}
}
