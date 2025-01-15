using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BC8 RID: 7112
	internal class OutputProvenanceCalculator
	{
		// Token: 0x0600E8DE RID: 59614 RVA: 0x0032C167 File Offset: 0x0032A367
		public OutputProvenanceCalculator(Grammar grammar, IRow input, ValueSubstring output, DescriptionLookup descriptionLookup)
		{
			this._grammar = grammar;
			this._build = GrammarBuilders.Instance(grammar);
			this._input = input;
			this._output = output;
			this._descriptionLookup = descriptionLookup;
		}

		// Token: 0x0600E8DF RID: 59615 RVA: 0x0032C198 File Offset: 0x0032A398
		private State BuildState(string columnName)
		{
			return State.CreateForExecution(new KeyValuePair<Symbol, object>[]
			{
				new KeyValuePair<Symbol, object>(this._grammar.InputSymbol, this._input),
				new KeyValuePair<Symbol, object>(this._build.Symbol.columnName, columnName),
				new KeyValuePair<Symbol, object>(this._build.Symbol.x, Semantics.ChooseInput(this._input, columnName)),
				new KeyValuePair<Symbol, object>(this._build.Symbol.cell, Semantics.LookupInput(this._input, columnName))
			});
		}

		// Token: 0x0600E8E0 RID: 59616 RVA: 0x0032C23C File Offset: 0x0032A43C
		private IEnumerable<TransformationDescription> GetTransformations(conv conv, string columnName, State state)
		{
			return conv.Switch<IEnumerable<TransformationDescription>>(this._build, (SubString substring) => this.GetTransformations(substring.SS, columnName), (ToLowercase toLowercase) => this.GetTransformations(toLowercase.SS, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (ToUppercase toUppercase) => this.GetTransformations(toUppercase.SS, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (ToSimpleTitleCase toSimpleTitleCase) => this.GetTransformations(toSimpleTitleCase.SS, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (FormatPartialDateTime formatPartialDateTime) => this.GetTransformations(formatPartialDateTime, columnName, state), (Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber formatNumber) => this.GetTransformations(formatNumber.number, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.Lookup lookup) => new TransformationDescription[]
			{
				this._descriptionLookup.Lookup(lookup.x.Node, columnName),
				this._descriptionLookup.Lookup(conv.Node)
			}, (Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumericRange formatNumericRange) => this.GetTransformations(formatNumericRange.inputNumber, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange formatDateTimeRange) => this.GetTransformations(formatDateTimeRange.inputDateTime, columnName, state).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(conv.Node) })), (LetSharedParsedNumber letSharedParsedNumber) => this.GetTransformations(letSharedParsedNumber.inputNumber, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(letSharedParsedNumber._LetB0.Node) })), (LetSharedParsedDateTime letSharedParsedDateTime) => this.GetTransformations(letSharedParsedDateTime.inputDateTime, columnName, state).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(letSharedParsedDateTime._LetB1.Node) })));
		}

		// Token: 0x0600E8E1 RID: 59617 RVA: 0x0032C300 File Offset: 0x0032A500
		private IEnumerable<TransformationDescription> GetTransformations(SS ss, string columnName)
		{
			return new TransformationDescription[] { this._descriptionLookup.Lookup(ss.Node, columnName) };
		}

		// Token: 0x0600E8E2 RID: 59618 RVA: 0x0032C320 File Offset: 0x0032A520
		private IEnumerable<TransformationDescription> GetTransformations(number number, string columnName)
		{
			return number.Switch<IEnumerable<TransformationDescription>>(this._build, (number_inputNumber conversion) => this.GetTransformations(conversion.inputNumber, columnName), (Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.RoundNumber roundNumber) => this.GetTransformations(roundNumber.inputNumber, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(number.Node) })));
		}

		// Token: 0x0600E8E3 RID: 59619 RVA: 0x0032C374 File Offset: 0x0032A574
		private IEnumerable<TransformationDescription> GetTransformations(inputNumber inputNumber, string columnName)
		{
			return inputNumber.Switch<IEnumerable<TransformationDescription>>(this._build, (inputNumber_castToNumber castToNumber) => this.GetTransformations(castToNumber.castToNumber, columnName), (inputNumber_parsedNumber parsedNumber) => this.GetTransformations(parsedNumber.parsedNumber, columnName));
		}

		// Token: 0x0600E8E4 RID: 59620 RVA: 0x0032C3BA File Offset: 0x0032A5BA
		private IEnumerable<TransformationDescription> GetTransformations(castToNumber castToNumber, string columnName)
		{
			return new TransformationDescription[] { this._descriptionLookup.Lookup(castToNumber.Node, columnName) };
		}

		// Token: 0x0600E8E5 RID: 59621 RVA: 0x0032C3D8 File Offset: 0x0032A5D8
		private IEnumerable<TransformationDescription> GetTransformations(parsedNumber parsedNumber, string columnName)
		{
			return this.GetTransformations(parsedNumber.Cast_ParseNumber().SS, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(parsedNumber.Node) }));
		}

		// Token: 0x0600E8E6 RID: 59622 RVA: 0x0032C420 File Offset: 0x0032A620
		private IEnumerable<TransformationDescription> GetTransformations(datetime datetime, string columnName, State state)
		{
			return datetime.Switch<IEnumerable<TransformationDescription>>(this._build, (datetime_inputDateTime inputDateTime) => this.GetTransformations(inputDateTime.inputDateTime, columnName, state), (RoundPartialDateTime roundPartialDateTime) => this.GetTransformations(roundPartialDateTime.inputDateTime, columnName, state).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(datetime.Node) })));
		}

		// Token: 0x0600E8E7 RID: 59623 RVA: 0x0032C478 File Offset: 0x0032A678
		private IEnumerable<TransformationDescription> GetTransformations(inputDateTime inputDateTime, string columnName, State state)
		{
			return inputDateTime.Switch<IEnumerable<TransformationDescription>>(this._build, (AsPartialDateTime asPartialDateTime) => new TransformationDescription[] { this._descriptionLookup.Lookup(inputDateTime.Node, columnName) }, (inputDateTime_parsedDateTime parsedDateTime) => this.GetTransformations(parsedDateTime.parsedDateTime, columnName, state));
		}

		// Token: 0x0600E8E8 RID: 59624 RVA: 0x0032C4D0 File Offset: 0x0032A6D0
		private IEnumerable<TransformationDescription> GetTransformations(parsedDateTime parsedDateTime, string columnName, State state)
		{
			ParsePartialDateTime parsePartialDateTime = parsedDateTime.Cast_ParsePartialDateTime();
			return this.GetTransformations(parsePartialDateTime.SS, columnName).Concat(Seq.Of<TransformationDescription>(new TransformationDescription[] { this._descriptionLookup.Lookup(parsedDateTime.Node, this.GetParseDateTimeFormatIndex(parsePartialDateTime, state), null) }));
		}

		// Token: 0x0600E8E9 RID: 59625 RVA: 0x0032C52C File Offset: 0x0032A72C
		private IEnumerable<TransformationDescription> GetTransformations(FormatPartialDateTime format, string columnName, State state)
		{
			DateTimeFormat value = format.outputDtFormat.Value;
			return this.GetTransformations(format.datetime, columnName, state).Concat(from idx in Enumerable.Range(0, value.FormatParts.Count)
				select this._descriptionLookup.Lookup(format.Node, idx));
		}

		// Token: 0x0600E8EA RID: 59626 RVA: 0x0032C59C File Offset: 0x0032A79C
		private int? GetParseDateTimeFormatIndex(ParsePartialDateTime? parseDateNode, State state, out DateTimeFormatMatch inputMatch)
		{
			if (parseDateNode == null)
			{
				inputMatch = null;
				return null;
			}
			ValueSubstring dateSubString = (ValueSubstring)parseDateNode.Value.SS.Node.Invoke(state);
			var <>f__AnonymousType = ((DateTimeFormat[])parseDateNode.Value.inputDtFormats.Node.Invoke(state)).Select((DateTimeFormat format, int idx) => new
			{
				idx = idx,
				match = format.Parse(dateSubString)
			}).First(o => o.match.HasValue);
			inputMatch = <>f__AnonymousType.match.Value;
			return new int?(<>f__AnonymousType.idx);
		}

		// Token: 0x0600E8EB RID: 59627 RVA: 0x0032C664 File Offset: 0x0032A864
		private int GetParseDateTimeFormatIndex(ParsePartialDateTime dateNode, State state)
		{
			DateTimeFormatMatch dateTimeFormatMatch;
			return this.GetParseDateTimeFormatIndex(new ParsePartialDateTime?(dateNode), state, out dateTimeFormatMatch).Value;
		}

		// Token: 0x0600E8EC RID: 59628 RVA: 0x0032C688 File Offset: 0x0032A888
		private IEnumerable<OutputProvenance> GetProvenanceNotRoundedDateTime(FormatPartialDateTime formatPartialDateTime, string columnName, State state)
		{
			ProgramNode node = formatPartialDateTime.Node;
			ValueSubstring valueSubstring = (ValueSubstring)node.Invoke(state);
			DateTimeFormat dateTimeFormat = (DateTimeFormat)formatPartialDateTime.outputDtFormat.Node.Invoke(state);
			datetime datetime = formatPartialDateTime.datetime;
			inputDateTime_parsedDateTime? inputDateTime_parsedDateTime;
			ParsePartialDateTime? parsePartialDateTime = ((datetime.Cast_datetime_inputDateTime(this._build).inputDateTime.As_inputDateTime_parsedDateTime(this._build) != null) ? new ParsePartialDateTime?(inputDateTime_parsedDateTime.GetValueOrDefault().parsedDateTime.Cast_ParsePartialDateTime()) : null);
			DateTimeFormatMatch dateTimeFormatMatch;
			int? parseDateTimeFormatIndex = this.GetParseDateTimeFormatIndex(parsePartialDateTime, state, out dateTimeFormatMatch);
			PartialDateTime partialDateTime;
			MultiValueDictionary<DateTimePart, Record<ValueSubstring, int>> multiValueDictionary;
			if (parseDateTimeFormatIndex == null)
			{
				dateTimeFormatMatch = null;
				partialDateTime = formatPartialDateTime.datetime.Node.Invoke(state) as PartialDateTime;
				multiValueDictionary = null;
			}
			else
			{
				partialDateTime = dateTimeFormatMatch.PartialDateTime;
				multiValueDictionary = new MultiValueDictionary<DateTimePart, Record<ValueSubstring, int>>();
				uint num = 0U;
				int num2 = 0;
				foreach (DateTimeFormatPart dateTimeFormatPart in dateTimeFormatMatch.DateTimeFormat.FormatParts)
				{
					uint length = (uint)dateTimeFormatPart.ToString(dateTimeFormatMatch.PartialDateTime).Length;
					if (dateTimeFormatPart.MatchedPart.HasValue)
					{
						ValueSubstring valueSubstring2 = ValueSubstring.Create(dateTimeFormatMatch.Region.Source, new uint?(dateTimeFormatMatch.Region.Start + num), new uint?(dateTimeFormatMatch.Region.Start + num + length), null, null);
						multiValueDictionary.Add(dateTimeFormatPart.MatchedPart.Value, Record.Create<ValueSubstring, int>(valueSubstring2, num2));
					}
					num += length;
					num2++;
				}
			}
			List<OutputProvenance> list = new List<OutputProvenance>(dateTimeFormat.FormatParts.Count);
			uint num3 = 0U;
			int num4 = 0;
			foreach (DateTimeFormatPart dateTimeFormatPart2 in dateTimeFormat.FormatParts)
			{
				string text = dateTimeFormatPart2.ToString(partialDateTime);
				ValueSubstring valueSubstring3 = valueSubstring.Slice(num3, new uint?((uint)((ulong)num3 + (ulong)((long)text.Length))));
				num3 += (uint)text.Length;
				if (dateTimeFormatPart2.MatchedPart.HasValue)
				{
					ValueSubstring valueSubstring4;
					int? num5;
					IReadOnlyCollection<Record<ValueSubstring, int>> readOnlyCollection;
					if (multiValueDictionary == null)
					{
						valueSubstring4 = null;
						num5 = null;
					}
					else if (multiValueDictionary.TryGetValue(dateTimeFormatPart2.MatchedPart.Value, out readOnlyCollection) && readOnlyCollection.Any<Record<ValueSubstring, int>>())
					{
						Record<ValueSubstring, int> record = readOnlyCollection.First<Record<ValueSubstring, int>>();
						valueSubstring4 = record.Item1;
						num5 = new int?(record.Item2);
					}
					else
					{
						valueSubstring4 = ValueSubstring.Create(dateTimeFormatMatch.Region.Source, new uint?(dateTimeFormatMatch.Region.Start), new uint?(dateTimeFormatMatch.Region.End), null, null);
						num5 = null;
					}
					list.Add(new OutputProvenance(valueSubstring3, ProvenanceKind.DateTimeTransformation, valueSubstring4, columnName, ((parsePartialDateTime != null) ? this.GetTransformations(parsePartialDateTime.Value.SS, columnName).AppendItem(this._descriptionLookup.Lookup(parsePartialDateTime.Value.Node, parseDateTimeFormatIndex.Value, num5)) : this.GetTransformations(datetime, columnName, state)).AppendItem(this._descriptionLookup.Lookup(node, num4))));
				}
				else
				{
					list.Add(new OutputProvenance(valueSubstring3, ProvenanceKind.Constant, null, null, new TransformationDescription[] { this._descriptionLookup.Lookup(node, num4) }));
				}
				num4++;
			}
			return list;
		}

		// Token: 0x0600E8ED RID: 59629 RVA: 0x0032CA5C File Offset: 0x0032AC5C
		private IEnumerable<OutputProvenance> GetProvenance(conv conv, string columnName, State state)
		{
			FormatPartialDateTime formatPartialDateTime;
			if (conv.Is_FormatPartialDateTime(this._build, out formatPartialDateTime) && !formatPartialDateTime.datetime.Is_RoundPartialDateTime(this._build))
			{
				return this.GetProvenanceNotRoundedDateTime(formatPartialDateTime, columnName, state);
			}
			List<TransformationDescription> list = this.GetTransformations(conv, columnName, state).ToList<TransformationDescription>();
			if (list.Count == 1 && list.Single<TransformationDescription>().Category == TransformationCategory.Constant)
			{
				return Seq.Of<OutputProvenance>(new OutputProvenance[]
				{
					new OutputProvenance(ValueSubstring.Create(list.OfType<Constant>().Single<Constant>().ConstantString, null, null, null, null), ProvenanceKind.Constant, null, null, list)
				});
			}
			TransformationDescription transformationDescription = list.FirstOrDefault((TransformationDescription t) => t.Category == TransformationCategory.Substring);
			ValueSubstring valueSubstring = (ValueSubstring)conv.Node.Invoke(state);
			ValueSubstring valueSubstring2 = ValueSubstring.Create((valueSubstring != null) ? valueSubstring.Value : null, null, null, null, null);
			TransformationKind transformationKind;
			if (transformationDescription == null)
			{
				TransformationDescription transformationDescription2 = list.FirstOrDefault((TransformationDescription t) => t.Category == TransformationCategory.Input);
				transformationKind = transformationDescription2.Kind;
				ProvenanceKind provenanceKind;
				if (transformationKind != TransformationKind.InputNumber)
				{
					if (transformationKind != TransformationKind.InputDate)
					{
						throw new NotImplementedException("Unknown input type: " + transformationDescription2.Kind.ToString());
					}
					provenanceKind = ProvenanceKind.DateTimeTransformation;
				}
				else
				{
					provenanceKind = ProvenanceKind.NumberTransformation;
				}
				return Seq.Of<OutputProvenance>(new OutputProvenance[]
				{
					new OutputProvenance(valueSubstring2, provenanceKind, null, columnName, list)
				});
			}
			ValueSubstring valueSubstring3 = (ValueSubstring)transformationDescription.ProgramNode.Invoke(state);
			transformationKind = list.Last<TransformationDescription>().Kind;
			ProvenanceKind provenanceKind2;
			if (transformationKind <= TransformationKind.RoundDateTime)
			{
				if (transformationKind <= TransformationKind.Substring)
				{
					if (transformationKind == TransformationKind.Constant)
					{
						provenanceKind2 = ProvenanceKind.Constant;
						goto IL_0262;
					}
					if (transformationKind != TransformationKind.Substring)
					{
						goto IL_0238;
					}
				}
				else if (transformationKind != TransformationKind.WholeColumn)
				{
					if (transformationKind == TransformationKind.Lookup)
					{
						provenanceKind2 = ProvenanceKind.Lookup;
						goto IL_0262;
					}
					if (transformationKind != TransformationKind.RoundDateTime)
					{
						goto IL_0238;
					}
					goto IL_0229;
				}
				provenanceKind2 = ProvenanceKind.Substring;
				goto IL_0262;
			}
			if (transformationKind <= TransformationKind.FormatNumber)
			{
				if (transformationKind == TransformationKind.CaseTransformation)
				{
					provenanceKind2 = ProvenanceKind.CaseTransformation;
					goto IL_0262;
				}
				if (transformationKind != TransformationKind.FormatNumber)
				{
					goto IL_0238;
				}
			}
			else if (transformationKind != TransformationKind.FormatNumericRange)
			{
				if (transformationKind != TransformationKind.FormatDateTime && transformationKind != TransformationKind.FormatDateTimeRange)
				{
					goto IL_0238;
				}
				goto IL_0229;
			}
			provenanceKind2 = ProvenanceKind.NumberTransformation;
			goto IL_0262;
			IL_0229:
			provenanceKind2 = ProvenanceKind.DateTimeTransformation;
			goto IL_0262;
			IL_0238:
			throw new NotImplementedException("Unknown TransformationKind: " + list.Last<TransformationDescription>().Kind.ToString());
			IL_0262:
			return Seq.Of<OutputProvenance>(new OutputProvenance[]
			{
				new OutputProvenance(valueSubstring2, provenanceKind2, valueSubstring3, columnName, list)
			});
		}

		// Token: 0x0600E8EE RID: 59630 RVA: 0x0032CCE8 File Offset: 0x0032AEE8
		public IEnumerable<OutputProvenance> GetProvenance(st st)
		{
			return this.GetProvenance(st.Cast_Transformation().e);
		}

		// Token: 0x0600E8EF RID: 59631 RVA: 0x0032CD0A File Offset: 0x0032AF0A
		private IEnumerable<OutputProvenance> GetProvenance(e e)
		{
			return e.Switch<IEnumerable<OutputProvenance>>(this._build, new Func<Atom, IEnumerable<OutputProvenance>>(this.GetProvenance), (Concat concat) => this.GetProvenance(concat, 0U));
		}

		// Token: 0x0600E8F0 RID: 59632 RVA: 0x0032CD34 File Offset: 0x0032AF34
		private IEnumerable<OutputProvenance> GetProvenance(Atom atom)
		{
			IEnumerable<OutputProvenance> provenance = this.GetProvenance(atom.f);
			IList<OutputProvenance> list = (provenance as IList<OutputProvenance>) ?? provenance.ToList<OutputProvenance>();
			if (list.Count != 1)
			{
				return list;
			}
			OutputProvenance outputProvenance = list.Single<OutputProvenance>();
			if (this._output.Value == outputProvenance.OutputSubstring.Value)
			{
				return new OutputProvenance[] { outputProvenance.WithOutput(this._output, 0U) };
			}
			return list;
		}

		// Token: 0x0600E8F1 RID: 59633 RVA: 0x0032CDA7 File Offset: 0x0032AFA7
		private IEnumerable<OutputProvenance> GetProvenance(f f)
		{
			return f.Switch<IEnumerable<OutputProvenance>>(this._build, new Func<ConstStr, IEnumerable<OutputProvenance>>(this.GetProvenance), new Func<LetColumnName, IEnumerable<OutputProvenance>>(this.GetProvenance));
		}

		// Token: 0x0600E8F2 RID: 59634 RVA: 0x0032CDD0 File Offset: 0x0032AFD0
		private IEnumerable<OutputProvenance> GetProvenance(Concat concat, uint offset)
		{
			IEnumerable<OutputProvenance> enumerable = (from p in this.GetProvenance(concat.f)
				select p.WithOutput(this._output, offset)).ToList<OutputProvenance>();
			uint prefixEndOffset = enumerable.Last<OutputProvenance>().OutputSubstring.End;
			Func<OutputProvenance, OutputProvenance> <>9__3;
			IEnumerable<OutputProvenance> enumerable2 = concat.e.Switch<IEnumerable<OutputProvenance>>(this._build, delegate(Atom atom)
			{
				IEnumerable<OutputProvenance> provenance = this.GetProvenance(atom);
				Func<OutputProvenance, OutputProvenance> func;
				if ((func = <>9__3) == null)
				{
					func = (<>9__3 = (OutputProvenance p) => p.WithOutput(this._output, prefixEndOffset));
				}
				return provenance.Select(func);
			}, (Concat suffixConcat) => this.GetProvenance(suffixConcat, prefixEndOffset));
			return enumerable.Concat(enumerable2);
		}

		// Token: 0x0600E8F3 RID: 59635 RVA: 0x0032CE60 File Offset: 0x0032B060
		private IEnumerable<OutputProvenance> GetProvenance(ConstStr constStr)
		{
			string value = constStr.s.Value;
			return new OutputProvenance[]
			{
				new OutputProvenance(ValueSubstring.Create(value, null, null, null, null), ProvenanceKind.Constant, null, null, new TransformationDescription[] { this._descriptionLookup.Lookup(constStr.Node) })
			};
		}

		// Token: 0x0600E8F4 RID: 59636 RVA: 0x0032CEC4 File Offset: 0x0032B0C4
		private IEnumerable<OutputProvenance> GetProvenance(LetColumnName let)
		{
			string text = (string)let.idx.Node.Invoke(null);
			conv conv = let.letOptions.Switch<conv>(this._build, (LetCell letCell) => letCell.conv, (LetX letX) => letX.conv);
			return this.GetProvenance(conv, text, this.BuildState(text));
		}

		// Token: 0x040058DE RID: 22750
		private readonly Grammar _grammar;

		// Token: 0x040058DF RID: 22751
		private readonly GrammarBuilders _build;

		// Token: 0x040058E0 RID: 22752
		private readonly IRow _input;

		// Token: 0x040058E1 RID: 22753
		private readonly ValueSubstring _output;

		// Token: 0x040058E2 RID: 22754
		private readonly DescriptionLookup _descriptionLookup;
	}
}
