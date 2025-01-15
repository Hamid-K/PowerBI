using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000A0 RID: 160
	public class Example : Example<IRow, object>
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000D7F3 File Offset: 0x0000B9F3
		[JsonConstructor]
		public Example(IRow input, object output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D800 File Offset: 0x0000BA00
		public string ToAnonymizedString()
		{
			string text = "{0} -> {1}";
			object[] array = new object[2];
			int num = 0;
			string text2 = base.Input.ToString();
			array[num] = ((text2 != null) ? text2.ToAnonymizedString() : null);
			int num2 = 1;
			object output = base.Output;
			object obj;
			if (output == null)
			{
				obj = null;
			}
			else
			{
				string text3 = output.ToString();
				if (text3 == null)
				{
					obj = null;
				}
				else
				{
					string text4 = text3.ToAnonymizedString();
					obj = ((text4 != null) ? text4.ToLiteral(null) : null);
				}
			}
			array[num2] = obj;
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}
	}
}
