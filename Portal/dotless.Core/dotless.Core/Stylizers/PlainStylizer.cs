using System;
using dotless.Core.Parser;

namespace dotless.Core.Stylizers
{
	// Token: 0x02000014 RID: 20
	public class PlainStylizer : IStylizer
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000037F4 File Offset: 0x000019F4
		public string Stylize(Zone zone)
		{
			string text = (string.IsNullOrEmpty(zone.FileName) ? "" : string.Format(" in file '{0}'", zone.FileName));
			string text2 = "";
			if (zone.CallZone != null)
			{
				string text3 = "";
				if (zone.CallZone.FileName != zone.FileName && !string.IsNullOrEmpty(zone.CallZone.FileName))
				{
					text3 = string.Format(" in file '{0}'", zone.CallZone.FileName);
				}
				text2 = string.Format("\r\nfrom line {0}{2}:\r\n{0,5:[#]}: {1}", zone.CallZone.LineNumber, zone.CallZone.Extract.Line, text3);
			}
			return string.Format("\r\n{1} on line {4}{0}:\r\n{2,5:[#]}: {3}\r\n{4,5:[#]}: {5}\r\n       {6}^\r\n{7,5:[#]}: {8}{9}", new object[]
			{
				text,
				zone.Message,
				zone.LineNumber - 1,
				zone.Extract.Before,
				zone.LineNumber,
				zone.Extract.Line,
				new string('-', zone.Position),
				zone.LineNumber + 1,
				zone.Extract.After,
				text2
			});
		}
	}
}
