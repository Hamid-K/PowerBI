using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x0200124F RID: 4687
	public abstract class SimpleProgram : Program
	{
		// Token: 0x06008CF6 RID: 36086 RVA: 0x001D990C File Offset: 0x001D7B0C
		internal SimpleProgram(readFlatFile program, IReadOnlyList<string> columnNames, int skip, int skipFooter, bool filterEmptyLines, Optional<string> commentStr, IReadOnlyList<string> rawColumnNames, int skipEmptyAndCommentCount, bool hasEmptyLines, bool hasMultiLineRows, IEnumerable<string> newLineStrings)
			: base(program, columnNames, rawColumnNames)
		{
			this.Skip = skip;
			this.SkipFooter = skipFooter;
			this.FilterEmptyLines = filterEmptyLines;
			this.CommentStr = commentStr;
			this.SkipEmptyAndCommentCount = skipEmptyAndCommentCount;
			this.HasEmptyLines = hasEmptyLines;
			this.HasMultiLineRows = hasMultiLineRows;
			this.NewLineStrings = ((newLineStrings != null) ? newLineStrings.ToList<string>() : null);
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06008CF7 RID: 36087 RVA: 0x001D996E File Offset: 0x001D7B6E
		public int Skip { get; }

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06008CF8 RID: 36088 RVA: 0x001D9976 File Offset: 0x001D7B76
		public int SkipFooter { get; }

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06008CF9 RID: 36089 RVA: 0x001D997E File Offset: 0x001D7B7E
		public bool FilterEmptyLines { get; }

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06008CFA RID: 36090 RVA: 0x001D9986 File Offset: 0x001D7B86
		public Optional<string> CommentStr { get; }

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06008CFB RID: 36091 RVA: 0x001D998E File Offset: 0x001D7B8E
		public int SkipEmptyAndCommentCount { get; }

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06008CFC RID: 36092 RVA: 0x001D9996 File Offset: 0x001D7B96
		public bool HasEmptyLines { get; }

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06008CFD RID: 36093 RVA: 0x001D999E File Offset: 0x001D7B9E
		public bool HasMultiLineRows { get; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06008CFE RID: 36094 RVA: 0x001D99A6 File Offset: 0x001D7BA6
		public IReadOnlyList<string> NewLineStrings { get; }

		// Token: 0x06008CFF RID: 36095 RVA: 0x001D99AE File Offset: 0x001D7BAE
		public override bool IsPySparkSupported()
		{
			IReadOnlyList<string> newLineStrings = this.NewLineStrings;
			return newLineStrings != null && newLineStrings.Count == 1;
		}

		// Token: 0x06008D00 RID: 36096 RVA: 0x001D99C4 File Offset: 0x001D7BC4
		protected override XElement SerializeXML(ASTSerializationSettings serializationSettings)
		{
			XElement xelement = base.SerializeXML(serializationSettings);
			if (!serializationSettings.HasOmitLiterals)
			{
				xelement.Add(new object[]
				{
					new XAttribute("SkipEmptyAndCommentCount", this.SkipEmptyAndCommentCount.ToString()),
					new XAttribute("HasEmptyLines", this.HasEmptyLines.ToString()),
					new XAttribute("HasMultiLineRows", this.HasMultiLineRows.ToString())
				});
			}
			if (this.NewLineStrings != null)
			{
				xelement.Add(this.NewLineStrings.CollectionToXML("NewLineStrings", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<string, XAttribute>>()));
			}
			return xelement;
		}

		// Token: 0x06008D01 RID: 36097 RVA: 0x001D9A7C File Offset: 0x001D7C7C
		public override bool Equals(Program p)
		{
			SimpleProgram simpleProgram = p as SimpleProgram;
			return simpleProgram != null && base.Equals(simpleProgram) && this.SkipEmptyAndCommentCount == simpleProgram.SkipEmptyAndCommentCount && this.HasEmptyLines == simpleProgram.HasEmptyLines && this.HasMultiLineRows == simpleProgram.HasMultiLineRows && ValueEquality.Comparer.Equals(this.NewLineStrings, simpleProgram.NewLineStrings);
		}

		// Token: 0x06008D02 RID: 36098 RVA: 0x001D9AE0 File Offset: 0x001D7CE0
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num = HashHelpers.Combine(num, this.SkipEmptyAndCommentCount);
			num = HashHelpers.Combine(num, this.HasEmptyLines.GetHashCode());
			num = HashHelpers.Combine(num, this.HasMultiLineRows.GetHashCode());
			if (this.NewLineStrings != null)
			{
				num = HashHelpers.Combine(num, this.NewLineStrings.OrderDependentHashCode<string>());
			}
			return num;
		}
	}
}
