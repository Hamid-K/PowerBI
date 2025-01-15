using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.SchemaParser;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D1 RID: 209
	public abstract class SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> : SchemaVisitor<string, TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
		public override string VisitBot(BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			throw new InvalidOperationException("BotSchemaElement does not have a string representation");
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00010000 File Offset: 0x0000E200
		public override string VisitField(FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion> field)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<field name=\"{0}\"{1}>{2}  <{3} symbol=\"{4}\">{5}</{6}>{7}</field>{8}", new object[]
			{
				field.Name,
				field.IsNullable ? " nullable=\"true\"" : string.Empty,
				Environment.NewLine,
				this.GetProgramTag(field),
				this.GetSymbol(field),
				this.GetProgram(field),
				this.GetProgramTag(field),
				Environment.NewLine,
				Environment.NewLine
			}));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00010084 File Offset: 0x0000E284
		public override string VisitStruct(StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<struct name=\"{0}\"{1}>{2}  <{3} symbol=\"{4}\">{5}</{6}>    {7}</struct>{8}", new object[]
			{
				node.Name,
				node.IsNullable ? " nullable=\"true\"" : string.Empty,
				Environment.NewLine,
				this.GetProgramTag(node),
				this.GetSymbol(node),
				this.GetProgram(node),
				this.GetProgramTag(node),
				string.Join("  ", node.Members.Select((SchemaElement<TSequenceProgram, TRegionProgram, TRegion> e) => e.Serialize(this))),
				Environment.NewLine
			}));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010123 File Offset: 0x0000E323
		private string GetProgramTag(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			if (node.ExtractionKind != ExtractionKind.Sequence)
			{
				return "SubstringProgram";
			}
			return "SequenceProgram";
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001013C File Offset: 0x0000E33C
		private string GetSymbol(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			IReadOnlyCollection<IExtractionProgram<TRegion>> programs = node.Programs;
			IExtractionProgram<TRegion> extractionProgram = ((programs != null) ? programs.FirstOrDefault<IExtractionProgram<TRegion>>() : null);
			if (extractionProgram == null)
			{
				return "";
			}
			return extractionProgram.ProgramNode.Symbol.Name ?? "";
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00010180 File Offset: 0x0000E380
		public override string VisitSequence(SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<sequence name=\"{0}\"{1}>{2}  <program>{3}</program>{4}  {5}</sequence>{6}", new object[]
			{
				node.Name,
				node.IsNullable ? " nullable=\"true\"" : string.Empty,
				Environment.NewLine,
				this.GetProgram(node),
				Environment.NewLine,
				node.Element.Serialize(this),
				Environment.NewLine
			}));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000101F4 File Offset: 0x0000E3F4
		public override string VisitUnion(UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion> node)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<union name=\"{0}\"{1}>{2}  <program>{3}</program>{4}  {5}</union>{6}", new object[]
			{
				node.Name,
				node.IsNullable ? " nullable=\"true\"" : string.Empty,
				Environment.NewLine,
				this.GetProgram(node),
				Environment.NewLine,
				string.Join("  ", node.Members.Select((SchemaElement<TSequenceProgram, TRegionProgram, TRegion> e) => e.Serialize(this))),
				Environment.NewLine
			}));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001027C File Offset: 0x0000E47C
		private string GetProgram(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement)
		{
			if (schemaElement.Programs != null && schemaElement.Programs.Count > 0)
			{
				return this.GetProgramRepresentation(schemaElement.Programs.First<IExtractionProgram<TRegion>>().ProgramNode);
			}
			return string.Empty;
		}

		// Token: 0x060004A2 RID: 1186
		protected abstract string GetProgramRepresentation(ProgramNode programNode);

		// Token: 0x060004A3 RID: 1187 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public override string VisitConvert<TSequenceProgramChild, TRegionProgramChild, TRegionChild>(ConvertSchemaElement<TSequenceProgram, TRegionProgram, TRegion, TSequenceProgramChild, TRegionProgramChild, TRegionChild> convertSchemaElement)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("<convert name=\"{0}\" converterName=\"{1}\">{2}{3}</convert>{4}", new object[]
			{
				convertSchemaElement.Name,
				convertSchemaElement.Converter.ConverterName,
				Environment.NewLine,
				(convertSchemaElement.ChildElement != null) ? convertSchemaElement.ChildElement.Serialize(this.GetConvertChildVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>()) : "",
				Environment.NewLine
			}));
		}

		// Token: 0x060004A4 RID: 1188
		protected abstract SchemaPrintVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild> GetConvertChildVisitor<TSequenceProgramChild, TRegionProgramChild, TRegionChild>() where TSequenceProgramChild : SequenceExtractionProgram<TSequenceProgramChild, TRegionChild> where TRegionProgramChild : RegionExtractionProgram<TRegionProgramChild, TRegionChild> where TRegionChild : IRegion<TRegionChild>;
	}
}
