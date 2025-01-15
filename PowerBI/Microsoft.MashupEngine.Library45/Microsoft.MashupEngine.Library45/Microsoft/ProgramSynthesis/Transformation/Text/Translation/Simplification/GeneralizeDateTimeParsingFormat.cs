using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D96 RID: 7574
	internal class GeneralizeDateTimeParsingFormat : TTextDateTimeAlternativeSelector
	{
		// Token: 0x0600FE66 RID: 65126 RVA: 0x00364FDA File Offset: 0x003631DA
		private GeneralizeDateTimeParsingFormat()
		{
		}

		// Token: 0x17002A62 RID: 10850
		// (get) Token: 0x0600FE67 RID: 65127 RVA: 0x0036510D File Offset: 0x0036330D
		public static GeneralizeDateTimeParsingFormat Instance { get; } = new GeneralizeDateTimeParsingFormat();

		// Token: 0x0600FE68 RID: 65128 RVA: 0x00365114 File Offset: 0x00363314
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			inputDtFormats inputDtFormats;
			if (!Language.Build.Node.Is.inputDtFormats(p, out inputDtFormats))
			{
				return null;
			}
			DateTimeFormat[] value = inputDtFormats.Value;
			HashSet<DateTimeFormat> hashSet = value.Select((DateTimeFormat x) => base.GeneralizeDateTimeFormat(x)).ConvertToHashSet<DateTimeFormat>();
			if (hashSet.SetEquals(value))
			{
				return null;
			}
			return new ProgramNode[] { Language.Build.Node.Rule.inputDtFormats(hashSet.ToArray<DateTimeFormat>()).Node };
		}

		// Token: 0x0600FE69 RID: 65129 RVA: 0x00365192 File Offset: 0x00363392
		protected override bool HasPosixTranslation(DateTimeFormatPart part)
		{
			return part.PosixParsingFormatString != null;
		}

		// Token: 0x0600FE6A RID: 65130 RVA: 0x003651A0 File Offset: 0x003633A0
		protected override DateTimeFormatPart PickAlternative(DateTimeFormatPart x, IEnumerable<DateTimeFormatPart> choices)
		{
			return choices.Where((DateTimeFormatPart p) => p.BaseFormatString.Length <= x.BaseFormatString.Length).ArgMin((DateTimeFormatPart p) => p.BaseFormatString.Length) ?? choices.First<DateTimeFormatPart>();
		}
	}
}
