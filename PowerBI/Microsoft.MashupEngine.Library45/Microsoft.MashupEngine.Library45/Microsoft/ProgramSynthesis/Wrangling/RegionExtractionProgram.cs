using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000C4 RID: 196
	public abstract class RegionExtractionProgram<TProgram, TRegion> : Program<IEnumerable<TRegion>, IEnumerable<TRegion>>, IExtractionProgram<TRegion>, IProgram where TProgram : RegionExtractionProgram<TProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0000F9FE File Offset: 0x0000DBFE
		protected RegionExtractionProgram(ProgramNode programNode, ReferenceKind referenceKind, double score, Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
			: base(programNode, score, programNormalizingFunc)
		{
			this.ReferenceKind = referenceKind;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public ExtractionKind ExtractionKind
		{
			get
			{
				return ExtractionKind.Region;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public ReferenceKind ReferenceKind { get; }

		// Token: 0x0600046E RID: 1134 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		public IEnumerable<IEnumerable<TRegion>> RunExtraction(IEnumerable<TRegion> references)
		{
			return from output in this.Run(references)
				select new TRegion[] { output };
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public override string Serialize(ASTSerializationSettings serializationSettings)
		{
			string text = base.ProgramNode.PrintAST(serializationSettings);
			if (serializationSettings.HasHumanReadable)
			{
				text = new XCData(text).ToString();
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("<{0} version=\"{1}\" symbol=\"{2}\" refkind=\"{3}\" score=\"{4}\" >{5}</{6}>", new object[]
			{
				this.ExtractionKind,
				base.Version,
				base.ProgramNode.Symbol.Name,
				this.ReferenceKind,
				base.Score.ToString(CultureInfo.InvariantCulture),
				text,
				this.ExtractionKind
			}));
		}
	}
}
