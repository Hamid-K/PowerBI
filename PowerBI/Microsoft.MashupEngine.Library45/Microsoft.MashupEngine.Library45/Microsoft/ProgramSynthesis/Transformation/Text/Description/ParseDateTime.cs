using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8E RID: 7310
	public class ParseDateTime : TransformationDescription
	{
		// Token: 0x17002948 RID: 10568
		// (get) Token: 0x0600F76E RID: 63342 RVA: 0x0034BAEF File Offset: 0x00349CEF
		[JsonIgnore]
		public ParsePartialDateTime ParseProgramNode
		{
			get
			{
				return GrammarBuilders.Instance(base.ProgramNode.Grammar).Node.CastRule.ParsePartialDateTime(base.ProgramNode);
			}
		}

		// Token: 0x17002949 RID: 10569
		// (get) Token: 0x0600F76F RID: 63343 RVA: 0x0034BB16 File Offset: 0x00349D16
		[JsonIgnore]
		public int FormatIndex { get; }

		// Token: 0x1700294A RID: 10570
		// (get) Token: 0x0600F770 RID: 63344 RVA: 0x0034BB1E File Offset: 0x00349D1E
		[JsonIgnore]
		public int? FormatPartIndex { get; }

		// Token: 0x0600F771 RID: 63345 RVA: 0x0034BB28 File Offset: 0x00349D28
		internal ParseDateTime(ParsePartialDateTime programNode, string columnName, int formatIdx, int? formatPartIdx)
			: base(programNode.Node, columnName, TransformationCategory.Parse, TransformationKind.ParseDateTime)
		{
			this.FormatIndex = formatIdx;
			this.FormatPartIndex = formatPartIdx;
			this.Formats = programNode.inputDtFormats.Value;
			this.FormatForPart = this.Formats[formatIdx];
			if (formatPartIdx != null)
			{
				this.FormatForPart = new DateTimeFormat(Seq.Of<DateTimeFormatPart>(new DateTimeFormatPart[] { this.FormatForPart.FormatParts[formatPartIdx.Value] }));
			}
		}

		// Token: 0x1700294B RID: 10571
		// (get) Token: 0x0600F772 RID: 63346 RVA: 0x0034BBB5 File Offset: 0x00349DB5
		private DateTimeFormat FormatForPart { get; }

		// Token: 0x1700294C RID: 10572
		// (get) Token: 0x0600F773 RID: 63347 RVA: 0x0034BBBD File Offset: 0x00349DBD
		[JsonIgnore]
		internal IReadOnlyList<DateTimeFormat> Formats { get; }

		// Token: 0x1700294D RID: 10573
		// (get) Token: 0x0600F774 RID: 63348 RVA: 0x0034BBC5 File Offset: 0x00349DC5
		protected override object ExtraIdentity
		{
			get
			{
				return Record.Create<int, int?>(this.FormatIndex, this.FormatPartIndex);
			}
		}
	}
}
