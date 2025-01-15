using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001771 RID: 6001
	internal class TokenConcatStrategy : IConcatStrategy
	{
		// Token: 0x0600C6F7 RID: 50935 RVA: 0x002AC358 File Offset: 0x002AA558
		public TokenConcatStrategy(Recognition recognition, CancellationToken cancellation, LearnDebugTrace debugTrace)
		{
			this._recognition = recognition;
			this._cancellation = cancellation;
			IReadOnlyCollection<Example<IRow, object>> examples = this._recognition.Examples;
			this._outputValid = examples.All((Example<IRow, object> e) => e.Output is string) && !examples.All((Example<IRow, object> e) => this._recognition.Contains(e.Input, e.Output as string, false));
			if (!this._outputValid)
			{
				return;
			}
			this._outputLookup = new Dictionary<IRow, string>();
			this._tokenizerLookup = new Dictionary<IRow, OutputTokenFactory>();
			foreach (Example<IRow, object> example in examples)
			{
				this._outputLookup[example.Input] = (string)example.Output;
				this._tokenizerLookup[example.Input] = OutputTokenFactory.Create(example, this._recognition, debugTrace, this._cancellation);
			}
		}

		// Token: 0x0600C6F8 RID: 50936 RVA: 0x002AC460 File Offset: 0x002AA660
		public bool CanConcat(string outputSuffix)
		{
			object[] array = new object[] { "CanConcat", outputSuffix };
			bool flag;
			if (this._recognition.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			bool flag2;
			if (this._outputValid)
			{
				int? num = ((outputSuffix != null) ? new int?(outputSuffix.Length) : null);
				if (num != null)
				{
					int valueOrDefault = num.GetValueOrDefault();
					if (valueOrDefault > 1 && valueOrDefault <= 100)
					{
						flag2 = !outputSuffix.AllDelimiters();
						goto IL_006E;
					}
				}
			}
			flag2 = false;
			IL_006E:
			flag = flag2;
			return this._recognition.CacheSet<bool>(array, flag);
		}

		// Token: 0x0600C6F9 RID: 50937 RVA: 0x002AC4EC File Offset: 0x002AA6EC
		public IEnumerable<string> Prefixes(WitnessContext<string> context)
		{
			string outputSuffix = context.OperatorOutput;
			if (!this.CanConcat(outputSuffix))
			{
				return null;
			}
			this._cancellation.ThrowIfCancellationRequested();
			List<OutputToken> list = (from t in this.Tokens(context.InputRow, outputSuffix)
				where t.Output != outputSuffix
				select t).ToList<OutputToken>();
			List<OutputToken> list2 = new List<OutputToken>();
			List<OutputToken> otherTokens = (from otherContext in context.OtherDisjunctiveContexts
				from otherSuffix in otherContext.OperatorOutputs
				from token1 in this.Tokens(otherContext.InputRow, otherSuffix)
				select token1).ToList<OutputToken>();
			List<ConstantStringOutputToken> list3 = (from token in list
				where token is ConstantStringOutputToken
				where otherTokens.None<OutputToken>() || otherTokens.Any(new Func<OutputToken, bool>(token.IsCompatible))
				orderby token.Output.Length descending
				select token).Cast<ConstantStringOutputToken>().ToList<ConstantStringOutputToken>();
			List<DynamicOutputToken> list4 = (from token in list
				where token is DynamicOutputToken
				where otherTokens.None<OutputToken>() || otherTokens.Any(new Func<OutputToken, bool>(token.IsCompatible))
				orderby token.Output.Length descending
				select token).Cast<DynamicOutputToken>().ToList<DynamicOutputToken>();
			List<ConstantStringOutputToken> list5 = list3.Where(delegate(ConstantStringOutputToken token)
			{
				if (!token.AllDelimiters)
				{
					DynamicOutputToken nextToken = token.NextToken;
					return nextToken != null && !nextToken.Partial && token.NextToken.Output.Length > 1;
				}
				return true;
			}).OrderByDescending(delegate(ConstantStringOutputToken token)
			{
				if (!token.AllDelimiters)
				{
					return token.Output.Length;
				}
				return 100000;
			}).ToList<ConstantStringOutputToken>();
			List<DynamicOutputToken> list6 = list4.Where((DynamicOutputToken token) => !token.Partial).ToList<DynamicOutputToken>();
			this._cancellation.ThrowIfCancellationRequested();
			if (context.OtherDisjunctiveContexts.None<WitnessDisjunctiveContext<string>>())
			{
				list2.AddRange(list5);
				list2.AddRange(list6);
			}
			else
			{
				list2.AddRange(list5.Concat(list3.Except(list5)));
				list2.AddRange(list6.Concat(list4.Except(list6)));
			}
			return list2.Select((OutputToken t) => t.Output).Distinct<string>().ToList<string>();
		}

		// Token: 0x0600C6FA RID: 50938 RVA: 0x002AC7B0 File Offset: 0x002AA9B0
		private IEnumerable<OutputToken> Tokens(IRow inputRow, string outputSuffix)
		{
			string text = this._outputLookup[inputRow];
			int num = text.Length - outputSuffix.Length;
			if (num.IsValidIndex(text))
			{
				return this._tokenizerLookup[inputRow].Tokens(num);
			}
			return new OutputToken[0];
		}

		// Token: 0x04004E30 RID: 20016
		private readonly CancellationToken _cancellation;

		// Token: 0x04004E31 RID: 20017
		private readonly Dictionary<IRow, string> _outputLookup;

		// Token: 0x04004E32 RID: 20018
		private readonly bool _outputValid;

		// Token: 0x04004E33 RID: 20019
		private readonly Recognition _recognition;

		// Token: 0x04004E34 RID: 20020
		private readonly Dictionary<IRow, OutputTokenFactory> _tokenizerLookup;
	}
}
