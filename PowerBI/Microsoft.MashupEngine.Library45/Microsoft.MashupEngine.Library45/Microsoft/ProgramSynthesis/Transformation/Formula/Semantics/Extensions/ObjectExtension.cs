using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001790 RID: 6032
	public static class ObjectExtension
	{
		// Token: 0x0600C7CC RID: 51148 RVA: 0x002AE2A0 File Offset: 0x002AC4A0
		public static string ToPythonPseudoLiteral(this object subject)
		{
			string text;
			if (subject is decimal)
			{
				text = ((decimal)subject).ToString(CultureInfo.InvariantCulture);
			}
			else if (subject is double)
			{
				text = ((double)subject).ToString(CultureInfo.InvariantCulture);
			}
			else if (subject is float)
			{
				text = ((float)subject).ToString(CultureInfo.InvariantCulture);
			}
			else if (subject is DateTime)
			{
				DateTime dateTime = (DateTime)subject;
				text = dateTime.ToLiteral(null);
			}
			else
			{
				text = subject.ToLiteral(null);
			}
			return text;
		}

		// Token: 0x0600C7CD RID: 51149 RVA: 0x002AE33C File Offset: 0x002AC53C
		public static string ToCSharpLiteral(this object subject)
		{
			string text;
			if (subject != null)
			{
				if (subject is decimal)
				{
					decimal num = (decimal)subject;
					text = num.ToCSharpLiteral();
				}
				else if (subject is double)
				{
					double num2 = (double)subject;
					text = num2.ToCSharpLiteral();
				}
				else if (subject is DateTime)
				{
					DateTime dateTime = (DateTime)subject;
					text = "DateTime.Parse(\"" + dateTime.ToLiteral(null) + "\")";
				}
				else if (subject is Time)
				{
					Time time = (Time)subject;
					text = string.Concat(new string[]
					{
						"new Time(",
						time.Hours.ToLiteral(null),
						", ",
						time.Minutes.ToLiteral(null),
						", ",
						time.Seconds.ToLiteral(null),
						")"
					});
				}
				else
				{
					text = subject.ToLiteral(null);
				}
			}
			else
			{
				text = "null";
			}
			return text;
		}

		// Token: 0x0600C7CE RID: 51150 RVA: 0x002AE451 File Offset: 0x002AC651
		public static string ToCSharpLiteral(this double subject)
		{
			return subject.ToString("#,##0.#####################", CultureInfo.InvariantCulture).Replace(",", "_") + "d";
		}

		// Token: 0x0600C7CF RID: 51151 RVA: 0x002AE47D File Offset: 0x002AC67D
		public static string ToCSharpLiteral(this decimal subject)
		{
			return subject.ToString("#,##0.#####################################################", CultureInfo.InvariantCulture).Replace(",", "_") + "m";
		}

		// Token: 0x0600C7D0 RID: 51152 RVA: 0x002AE4AC File Offset: 0x002AC6AC
		public static string ToCSharpPseudoLiteral(this object subject)
		{
			string text;
			if (subject != null)
			{
				if (subject is double)
				{
					double num = (double)subject;
					text = num.ToCSharpLiteral();
				}
				else if (subject is decimal)
				{
					decimal num2 = (decimal)subject;
					text = num2.ToCSharpLiteral();
				}
				else if (subject is Time)
				{
					text = ((Time)subject).ToString();
				}
				else
				{
					string text2 = subject as string;
					if (text2 == null)
					{
						text = subject.ToLiteral(null);
					}
					else
					{
						text = text2.ToLiteral(null).Replace("\\\\", "\\");
					}
				}
			}
			else
			{
				text = "<null>";
			}
			return text;
		}

		// Token: 0x04004E95 RID: 20117
		private static readonly string _newline = Environment.NewLine;
	}
}
