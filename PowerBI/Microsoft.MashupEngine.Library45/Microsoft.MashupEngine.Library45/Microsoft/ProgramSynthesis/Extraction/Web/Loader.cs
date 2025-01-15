using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB8 RID: 4024
	public class Loader : ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>
	{
		// Token: 0x06006F21 RID: 28449 RVA: 0x0016B618 File Offset: 0x00169818
		private Loader()
		{
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06006F22 RID: 28450 RVA: 0x0016B620 File Offset: 0x00169820
		public static Loader Instance { get; } = new Loader();

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06006F23 RID: 28451 RVA: 0x0016AF35 File Offset: 0x00169135
		public override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06006F24 RID: 28452 RVA: 0x0016B628 File Offset: 0x00169828
		protected override SortedDictionary<Version, ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser> VersionParsers
		{
			get
			{
				return new SortedDictionary<Version, ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser>(Comparer<Version>.Create((Version x, Version y) => y.CompareTo(x)))
				{
					{
						new Version(0, 1),
						new ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser(base.LoadProgramVersion01)
					},
					{
						new Version(0, 0),
						new ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser(base.LoadProgramVersion00)
					}
				};
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06006F25 RID: 28453 RVA: 0x0016B690 File Offset: 0x00169890
		protected override ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser DefaultVersionParser
		{
			get
			{
				return new ExtractionLoader<SequenceProgram, RegionProgram, WebRegion>.VersionParser(base.LoadProgramVersion00);
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06006F26 RID: 28454 RVA: 0x0016B69E File Offset: 0x0016989E
		protected override Dictionary<Record<Version, Version>, Func<XElement, Version, string>> VersionConverters
		{
			get
			{
				return new Dictionary<Record<Version, Version>, Func<XElement, Version, string>> { 
				{
					Record.Create<Version, Version>(new Version(0, 0), new Version(0, 1)),
					new Func<XElement, Version, string>(base.ConvertProgramFrom00To01)
				} };
			}
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x0016B6CC File Offset: 0x001698CC
		public override IExtractionProgram<WebRegion> CreateProgram(ProgramNode programNode, ExtractionKind extractionKind, ReferenceKind refKind, double? score = null)
		{
			if (refKind == ReferenceKind.Invalid)
			{
				refKind = ReferenceKind.Parent;
			}
			else if (refKind != ReferenceKind.Parent)
			{
				throw new ArgumentException("Invalid ReferenceKind: " + refKind.ToString(), "refKind");
			}
			if (extractionKind == ExtractionKind.Region)
			{
				return new RegionProgram(programNode, refKind, score);
			}
			if (extractionKind == ExtractionKind.Sequence)
			{
				return new SequenceProgram(programNode, refKind, score);
			}
			throw new ArgumentException("Unknown ExtractionKind: " + extractionKind.ToString());
		}
	}
}
