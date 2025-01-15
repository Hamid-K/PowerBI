using System;
using dotless.Core.Parser;

namespace dotless.Core.Stylizers
{
	// Token: 0x02000012 RID: 18
	public class HtmlStylizer : IStylizer
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000036EC File Offset: 0x000018EC
		public string Stylize(Zone zone)
		{
			string text = (string.IsNullOrEmpty(zone.FileName) ? "" : string.Format(" in '{0}'", zone.FileName));
			return string.Format("\r\n<div id=\"less-error-message\">\r\n  <h3>There is an error{0}</h3>\r\n  <p>{1} on line {3}, column {5}</p>\r\n  <div class=\"extract\">\r\n    <pre class=\"before\"><span>{2}</span>{6}</pre>\r\n    <pre class=\"line\"><span>{3}</span>{7}<span class=\"error\">{8}</span>{9}</pre>\r\n    <pre class=\"after\"><span>{4}</span>{10}</pre>\r\n  </div>\r\n</div>\r\n", new object[]
			{
				text,
				zone.Message,
				zone.LineNumber - 1,
				zone.LineNumber,
				zone.LineNumber + 1,
				zone.Position,
				zone.Extract.Before,
				zone.Extract.Line.Substring(0, zone.Position),
				zone.Extract.Line[zone.Position],
				zone.Extract.Line.Substring(zone.Position + 1),
				zone.Extract.After
			});
		}
	}
}
