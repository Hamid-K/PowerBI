using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D4 RID: 212
	public abstract class SequenceExtractionProgram<TProgram, TRegion> : Program<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>, IExtractionProgram<TRegion>, IProgram where TProgram : SequenceExtractionProgram<TProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x000104F3 File Offset: 0x0000E6F3
		protected SequenceExtractionProgram(ProgramNode programNode, ReferenceKind referenceKind, double score, Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
			: base(programNode, score, programNormalizingFunc)
		{
			this.ReferenceKind = referenceKind;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000A5FD File Offset: 0x000087FD
		public ExtractionKind ExtractionKind
		{
			get
			{
				return ExtractionKind.Sequence;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00010506 File Offset: 0x0000E706
		public ReferenceKind ReferenceKind { get; }

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001050E File Offset: 0x0000E70E
		public IEnumerable<IEnumerable<TRegion>> RunExtraction(IEnumerable<TRegion> references)
		{
			return this.Run(references);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00010518 File Offset: 0x0000E718
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
