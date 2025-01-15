using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x0200005B RID: 91
	internal static class XmlParserExtensions
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public static XmlParseException At(this XmlParseError code, TextPosition position)
		{
			string text = "Error while parsing the provided XML document.";
			return new XmlParseException(code.GetCode(), text, position);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public static int GetCode(this XmlParseError code)
		{
			return (int)code;
		}
	}
}
