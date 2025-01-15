using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Pdf;
using Microsoft.ProgramSynthesis.Extraction.Pdf;

namespace Microsoft.Mashup.Engine1.Library.Pdf
{
	// Token: 0x0200203D RID: 8253
	internal class PdfTablesOptions
	{
		// Token: 0x06011311 RID: 70417 RVA: 0x003B32D8 File Offset: 0x003B14D8
		public PdfTablesOptions(OptionsRecord options)
		{
			this.options = options;
			Value value;
			if (options.TryGetValue("Implementation", out value) && !value.IsNull)
			{
				string asString = value.AsString;
				if (asString == "1.0")
				{
					this.version = new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1);
				}
				else if (asString == "1.1")
				{
					this.version = new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1_1);
				}
				else if (asString == "1.2")
				{
					this.version = new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1_2);
				}
				else
				{
					if (!(asString == "1.3"))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Implementation"), null, null);
					}
					this.version = new PdfAnalyzerVersion?(PdfAnalyzerVersion.V1_3);
				}
			}
			Value value2;
			if (options.TryGetValue("StartPage", out value2) && !value2.IsNull)
			{
				this.startPage = new int?(value2.AsInteger32);
				int? num = this.startPage;
				int num2 = 0;
				if ((num.GetValueOrDefault() <= num2) & (num != null))
				{
					throw ValueException.NewExpressionError<Message0>(Resources.InvalidPageRange, null, null);
				}
			}
			Value value3;
			if (options.TryGetValue("EndPage", out value3) && !value3.IsNull)
			{
				this.endPage = new int?(value3.AsInteger32);
				int? num = this.endPage;
				int num2 = 0;
				if (!((num.GetValueOrDefault() <= num2) & (num != null)))
				{
					if (this.startPage == null)
					{
						goto IL_01A9;
					}
					num = this.endPage;
					int? num3 = this.startPage;
					if (!((num.GetValueOrDefault() < num3.GetValueOrDefault()) & ((num != null) & (num3 != null))))
					{
						goto IL_01A9;
					}
				}
				throw ValueException.NewExpressionError<Message0>(Resources.InvalidPageRange, null, null);
			}
			IL_01A9:
			Value value4;
			if (options.TryGetValue("EnforceBorderLines", out value4) && !value4.IsNull)
			{
				this.enforceBorderLines = new bool?(value4.AsLogical.AsBoolean);
			}
			Value value5;
			if (options.TryGetValue("MultiPageTables", out value5) && !value5.IsNull)
			{
				this.multiPageTables = new bool?(value5.AsLogical.AsBoolean);
			}
		}

		// Token: 0x17002DCF RID: 11727
		// (get) Token: 0x06011312 RID: 70418 RVA: 0x003B34EA File Offset: 0x003B16EA
		public PdfAnalyzerVersion? Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17002DD0 RID: 11728
		// (get) Token: 0x06011313 RID: 70419 RVA: 0x003B34F2 File Offset: 0x003B16F2
		public int? StartPageIndex
		{
			get
			{
				return PdfTablesOptions.ConvertToIndex(this.startPage);
			}
		}

		// Token: 0x17002DD1 RID: 11729
		// (get) Token: 0x06011314 RID: 70420 RVA: 0x003B34FF File Offset: 0x003B16FF
		public int? EndPageIndex
		{
			get
			{
				return PdfTablesOptions.ConvertToIndex(this.endPage);
			}
		}

		// Token: 0x17002DD2 RID: 11730
		// (get) Token: 0x06011315 RID: 70421 RVA: 0x003B350C File Offset: 0x003B170C
		public bool? EnforceBorderLines
		{
			get
			{
				return this.enforceBorderLines;
			}
		}

		// Token: 0x17002DD3 RID: 11731
		// (get) Token: 0x06011316 RID: 70422 RVA: 0x003B3514 File Offset: 0x003B1714
		public bool? MultiPageTables
		{
			get
			{
				return this.multiPageTables;
			}
		}

		// Token: 0x06011317 RID: 70423 RVA: 0x003B351C File Offset: 0x003B171C
		private static int? ConvertToIndex(int? page)
		{
			if (page != null)
			{
				return new int?(page.Value - 1);
			}
			return null;
		}

		// Token: 0x04006845 RID: 26693
		private const string versionOneString = "1.0";

		// Token: 0x04006846 RID: 26694
		private const string versionOnePointOneString = "1.1";

		// Token: 0x04006847 RID: 26695
		private const string versionOnePointTwoString = "1.2";

		// Token: 0x04006848 RID: 26696
		private const string versionOnePointThreeString = "1.3";

		// Token: 0x04006849 RID: 26697
		private readonly OptionsRecord options;

		// Token: 0x0400684A RID: 26698
		private readonly PdfAnalyzerVersion? version;

		// Token: 0x0400684B RID: 26699
		private readonly int? startPage;

		// Token: 0x0400684C RID: 26700
		private readonly int? endPage;

		// Token: 0x0400684D RID: 26701
		private readonly bool? enforceBorderLines;

		// Token: 0x0400684E RID: 26702
		private readonly bool? multiPageTables;
	}
}
