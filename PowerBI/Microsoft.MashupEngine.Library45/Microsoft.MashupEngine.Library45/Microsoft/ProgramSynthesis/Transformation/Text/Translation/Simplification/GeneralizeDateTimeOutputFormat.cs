using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D93 RID: 7571
	internal class GeneralizeDateTimeOutputFormat : TTextDateTimeAlternativeSelector
	{
		// Token: 0x0600FE5B RID: 65115 RVA: 0x00364FDA File Offset: 0x003631DA
		private GeneralizeDateTimeOutputFormat()
		{
		}

		// Token: 0x17002A61 RID: 10849
		// (get) Token: 0x0600FE5C RID: 65116 RVA: 0x00364FE2 File Offset: 0x003631E2
		public static GeneralizeDateTimeOutputFormat Instance { get; } = new GeneralizeDateTimeOutputFormat();

		// Token: 0x0600FE5D RID: 65117 RVA: 0x00364FEC File Offset: 0x003631EC
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			outputDtFormat outputDtFormat;
			if (!Language.Build.Node.Is.outputDtFormat(p, out outputDtFormat))
			{
				return null;
			}
			DateTimeFormat value = outputDtFormat.Value;
			DateTimeFormat dateTimeFormat = base.GeneralizeDateTimeFormat(value);
			if (dateTimeFormat.FormatParts.SequenceEqual(value.FormatParts))
			{
				return null;
			}
			return new ProgramNode[] { Language.Build.Node.Rule.outputDtFormat(dateTimeFormat).Node };
		}

		// Token: 0x0600FE5E RID: 65118 RVA: 0x0036505F File Offset: 0x0036325F
		protected override bool HasPosixTranslation(DateTimeFormatPart part)
		{
			return part.PosixOutputFormatString != null;
		}

		// Token: 0x0600FE5F RID: 65119 RVA: 0x0036506C File Offset: 0x0036326C
		protected override DateTimeFormatPart PickAlternative(DateTimeFormatPart x, IEnumerable<DateTimeFormatPart> choices)
		{
			return choices.Where((DateTimeFormatPart p) => p.BaseFormatString.Length >= x.BaseFormatString.Length).ArgMin((DateTimeFormatPart p) => p.BaseFormatString.Length) ?? choices.First<DateTimeFormatPart>();
		}
	}
}
