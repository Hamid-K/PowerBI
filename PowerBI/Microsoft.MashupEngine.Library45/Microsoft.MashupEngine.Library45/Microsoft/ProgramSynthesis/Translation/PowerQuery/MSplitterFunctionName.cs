using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032A RID: 810
	public readonly struct MSplitterFunctionName
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x00034DC3 File Offset: 0x00032FC3
		public MSplitterFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00034DCC File Offset: 0x00032FCC
		public string Name { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00034DD4 File Offset: 0x00032FD4
		public string QualifiedName
		{
			get
			{
				return "Splitter." + this.Name;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00034DE6 File Offset: 0x00032FE6
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00034DEE File Offset: 0x00032FEE
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00034DFC File Offset: 0x00032FFC
		public override bool Equals(object obj)
		{
			return obj is MSplitterFunctionName && ((MSplitterFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008CF RID: 2255
		public static readonly MSplitterFunctionName SplitTextByAnyDelimiter = new MSplitterFunctionName("SplitTextByAnyDelimiter");

		// Token: 0x040008D0 RID: 2256
		public static readonly MSplitterFunctionName SplitTextByDelimiter = new MSplitterFunctionName("SplitTextByDelimiter");

		// Token: 0x040008D1 RID: 2257
		public static readonly MSplitterFunctionName SplitTextByEachDelimiter = new MSplitterFunctionName("SplitTextByEachDelimiter");

		// Token: 0x040008D2 RID: 2258
		public static readonly MSplitterFunctionName SplitTextByPositions = new MSplitterFunctionName("SplitTextByPositions");

		// Token: 0x040008D3 RID: 2259
		public static readonly MSplitterFunctionName SplitTextByRanges = new MSplitterFunctionName("SplitTextByRanges");
	}
}
