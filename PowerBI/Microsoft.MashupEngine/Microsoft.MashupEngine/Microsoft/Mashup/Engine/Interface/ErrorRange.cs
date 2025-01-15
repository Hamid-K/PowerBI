using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200007A RID: 122
	public class ErrorRange
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000306F File Offset: 0x0000126F
		public ErrorRange(int startPosition, int endPosition)
		{
			this.startPosition = startPosition;
			this.endPosition = endPosition;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003085 File Offset: 0x00001285
		public int StartPosition
		{
			get
			{
				return this.startPosition;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000308D File Offset: 0x0000128D
		public int EndPosition
		{
			get
			{
				return this.endPosition;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00003098 File Offset: 0x00001298
		public static ErrorRange GetErrorRange(string sourceText, SourceLocation errorLocation)
		{
			int num = 0;
			int num2 = 0;
			StringReader stringReader = new StringReader(sourceText);
			if (errorLocation != null)
			{
				TextRange range = errorLocation.Range;
				int num3 = 0;
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					int num4 = text.Length + 1;
					if (num3 < range.Start.Row)
					{
						num += num4;
					}
					else if (num3 == range.Start.Row)
					{
						num += range.Start.Column;
					}
					if (num3 < range.End.Row)
					{
						num2 += num4;
					}
					else if (num3 == range.End.Row)
					{
						num2 += range.End.Column;
					}
					num3++;
				}
			}
			return new ErrorRange(num, num2);
		}

		// Token: 0x04000153 RID: 339
		private int startPosition;

		// Token: 0x04000154 RID: 340
		private int endPosition;
	}
}
