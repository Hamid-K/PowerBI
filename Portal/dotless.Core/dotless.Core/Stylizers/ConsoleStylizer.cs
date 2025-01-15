using System;
using System.Collections.Generic;
using System.Text;
using dotless.Core.Parser;

namespace dotless.Core.Stylizers
{
	// Token: 0x02000011 RID: 17
	public class ConsoleStylizer : IStylizer
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000034C4 File Offset: 0x000016C4
		public ConsoleStylizer()
		{
			this.styles = new Dictionary<string, int[]>
			{
				{
					"bold",
					new int[] { 1, 22 }
				},
				{
					"inverse",
					new int[] { 7, 27 }
				},
				{
					"underline",
					new int[] { 4, 24 }
				},
				{
					"yellow",
					new int[] { 33, 39 }
				},
				{
					"green",
					new int[] { 32, 39 }
				},
				{
					"red",
					new int[] { 31, 39 }
				},
				{
					"grey",
					new int[] { 90, 39 }
				},
				{
					"reset",
					new int[2]
				}
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035B0 File Offset: 0x000017B0
		private string Stylize(string str, string style)
		{
			return string.Concat(new object[]
			{
				"\u001b[",
				this.styles[style][0],
				"m",
				str,
				"\u001b[",
				this.styles[style][1],
				"m"
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003618 File Offset: 0x00001818
		public string Stylize(Zone zone)
		{
			Extract extract = zone.Extract;
			int position = zone.Position;
			string text = extract.Line.Substring(0, position);
			string text2 = extract.Line.Substring(position + 1);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Stylize(extract.Before, "grey"));
			stringBuilder.Append(this.Stylize(text, "green"));
			stringBuilder.Append(this.Stylize(this.Stylize(extract.Line[position].ToString(), "inverse") + text2, "yellow"));
			stringBuilder.Append(this.Stylize(extract.After, "grey"));
			stringBuilder.Append(this.Stylize("", "reset"));
			return stringBuilder.ToString();
		}

		// Token: 0x04000018 RID: 24
		private Dictionary<string, int[]> styles;
	}
}
