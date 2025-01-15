using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BC6 RID: 7110
	public class OutputProvenance
	{
		// Token: 0x0600E8D1 RID: 59601 RVA: 0x0032BF48 File Offset: 0x0032A148
		internal OutputProvenance(ValueSubstring outputSubstring, ProvenanceKind kind, ValueSubstring inputSubstring, string inputColumnName, IEnumerable<TransformationDescription> transformations)
		{
			this.OutputSubstring = outputSubstring;
			this.Kind = kind;
			this.InputSubstring = inputSubstring;
			this.InputColumnName = inputColumnName;
			this.Transformations = (transformations as IReadOnlyList<TransformationDescription>) ?? transformations.ToList<TransformationDescription>();
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x0600E8D2 RID: 59602 RVA: 0x0032BF85 File Offset: 0x0032A185
		public string InputColumnName { get; }

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x0600E8D3 RID: 59603 RVA: 0x0032BF8D File Offset: 0x0032A18D
		[JsonIgnore]
		public IReadOnlyList<TransformationDescription> Transformations { get; }

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x0600E8D4 RID: 59604 RVA: 0x0032BF95 File Offset: 0x0032A195
		public IEnumerable<int> TransformationIndexes
		{
			get
			{
				return this.Transformations.Select((TransformationDescription t) => t.Index.Value);
			}
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x0600E8D5 RID: 59605 RVA: 0x0032BFC1 File Offset: 0x0032A1C1
		public ValueSubstring InputSubstring { get; }

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x0600E8D6 RID: 59606 RVA: 0x0032BFC9 File Offset: 0x0032A1C9
		public ValueSubstring OutputSubstring { get; }

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x0600E8D7 RID: 59607 RVA: 0x0032BFD1 File Offset: 0x0032A1D1
		public ProvenanceKind Kind { get; }

		// Token: 0x0600E8D8 RID: 59608 RVA: 0x0032BFDC File Offset: 0x0032A1DC
		internal static IEnumerable<OutputProvenance> Compute(Grammar grammar, st program, DescriptionLookup descriptionLookup, IRow input, ValueSubstring output)
		{
			if (output == null)
			{
				return null;
			}
			if ((ulong)output.Length != (ulong)((long)output.Source.Length))
			{
				string value = output.Value;
				IType type = output.Type;
				output = ValueSubstring.Create(value, null, null, type, null);
			}
			return new OutputProvenanceCalculator(grammar, input, output, descriptionLookup).GetProvenance(program).ToList<OutputProvenance>();
		}

		// Token: 0x0600E8D9 RID: 59609 RVA: 0x0032C04C File Offset: 0x0032A24C
		internal OutputProvenance WithOutput(ValueSubstring output, uint offset)
		{
			offset += this.OutputSubstring.Start;
			return new OutputProvenance(output.Slice(offset, new uint?(offset + this.OutputSubstring.Length)), this.Kind, this.InputSubstring, this.InputColumnName, this.Transformations);
		}

		// Token: 0x0600E8DA RID: 59610 RVA: 0x0032C0A0 File Offset: 0x0032A2A0
		public override string ToString()
		{
			if (this.Kind == ProvenanceKind.Constant)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("[{0}: \"{1}\"]", new object[] { this.Kind, this.OutputSubstring }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0}: [{1}-{2}]\"{3}\"->\"{4}\"]", new object[]
			{
				this.Kind,
				this.InputSubstring.Start,
				this.InputSubstring.End,
				this.InputSubstring,
				this.OutputSubstring
			}));
		}
	}
}
