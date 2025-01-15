using System;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200081A RID: 2074
	public class SpacerDateTimeFormatPart : ConstantDateTimeFormatPart
	{
		// Token: 0x06002CAF RID: 11439 RVA: 0x0007DCE4 File Offset: 0x0007BEE4
		public SpacerDateTimeFormatPart(string spacer)
			: base(spacer)
		{
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0007DCED File Offset: 0x0007BEED
		public SpacerDateTimeFormatPart(StringRegion spacer)
			: base(spacer)
		{
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x0007DCF6 File Offset: 0x0007BEF6
		public override XElement RenderXML()
		{
			return new XElement("SpacerFormatPart", new XCData(base.ConstantString));
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x0007DD12 File Offset: 0x0007BF12
		public new static SpacerDateTimeFormatPart TryParseFromXML(XElement literal)
		{
			if (!(literal.Name == "SpacerFormatPart"))
			{
				return null;
			}
			return new SpacerDateTimeFormatPart(literal.Value);
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x0007DD38 File Offset: 0x0007BF38
		public bool IsEmpty
		{
			get
			{
				return base.ConstantString.Length == 0;
			}
		}

		// Token: 0x0400155D RID: 5469
		internal const string SpacerXMLName = "SpacerFormatPart";
	}
}
