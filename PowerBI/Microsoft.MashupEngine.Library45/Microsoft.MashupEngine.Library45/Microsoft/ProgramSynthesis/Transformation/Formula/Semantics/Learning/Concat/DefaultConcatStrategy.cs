using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001758 RID: 5976
	internal class DefaultConcatStrategy : IConcatStrategy
	{
		// Token: 0x0600C644 RID: 50756 RVA: 0x002A9DC2 File Offset: 0x002A7FC2
		public DefaultConcatStrategy(Recognition recognition, CancellationToken cancellation)
		{
			this._recognition = recognition;
			this._cancellation = cancellation;
			this._examples = this._recognition.Examples;
		}

		// Token: 0x0600C645 RID: 50757 RVA: 0x002A9DEC File Offset: 0x002A7FEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool CanConcat(string result)
		{
			object[] array = new object[] { "CanConcat", result };
			bool flag;
			if (this._recognition.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			int? num = ((result != null) ? new int?(result.Length) : null);
			bool flag2;
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (valueOrDefault > 1 && valueOrDefault <= 300 && !this._examples.All((Example<IRow, object> e) => this._recognition.Contains(e.Input, e.Output as string, false)))
				{
					flag2 = !result.AllDelimiters();
					goto IL_0082;
				}
			}
			flag2 = false;
			IL_0082:
			flag = flag2;
			return this._recognition.CacheSet<bool>(array, flag);
		}

		// Token: 0x0600C646 RID: 50758 RVA: 0x002A9E8C File Offset: 0x002A808C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerable<string> Prefixes(WitnessContext<string> context)
		{
			DefaultConcatStrategy.<>c__DisplayClass5_0 CS$<>8__locals1 = new DefaultConcatStrategy.<>c__DisplayClass5_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.result = context.OperatorOutput;
			if (!this.CanConcat(CS$<>8__locals1.result))
			{
				return null;
			}
			IRow inputRow = context.InputRow;
			CS$<>8__locals1.cacheKey = new object[] { "Prefixes", inputRow, CS$<>8__locals1.result };
			IReadOnlyList<string> readOnlyList;
			if (this._recognition.CacheTryGetValue<IReadOnlyList<string>>(CS$<>8__locals1.cacheKey, out readOnlyList))
			{
				return readOnlyList;
			}
			List<string> list = new List<string>();
			ReadOnlySpan<char> readOnlySpan = CS$<>8__locals1.result.AsSpan();
			List<string> list2 = (from input in this._recognition.InputStrings(inputRow, null).DistinctValues
				where !string.IsNullOrEmpty(input) && !string.Equals(input, CS$<>8__locals1.result, StringComparison.InvariantCultureIgnoreCase) && CS$<>8__locals1.result.StartsWith(input, StringComparison.InvariantCultureIgnoreCase)
				select CS$<>8__locals1.result.Substring(0, input.Length)).ToList<string>();
			list.AddRange(list2);
			this._cancellation.ThrowIfCancellationRequested();
			string text = this._recognition.TakeWhileNumberChar(readOnlySpan).ToString();
			if (text.Any<char>() && this._recognition.TryFormatNumber(inputRow, text))
			{
				list.Add(text);
			}
			string text2 = this._recognition.TakeWhileDateTimeChar(readOnlySpan).ToString();
			if (text2.Any<char>() && this._recognition.IsFormattedDateTime(text2, false))
			{
				list.Add(text2);
			}
			bool flag = this._recognition.OutputStrings().Contains(CS$<>8__locals1.result);
			bool flag2 = this._recognition.OutputStrings().Count > 1;
			if (flag && flag2)
			{
				int i;
				IEnumerable<string> enumerable = this._recognition.CacheGetOrCompute<List<string>>(new object[]
				{
					"commonConstPrefixes",
					this._examples.First<Example<IRow, object>>().Output
				}, delegate
				{
					string text3 = CS$<>8__locals1.<>4__this._examples.StringOutputs<IRow, object>().FirstOrDefault<string>();
					IEnumerable<string> enumerable3 = ((text3 != null) ? text3.AllPrefixes(null) : null).OrderByDescending((string p) => p.Length);
					Func<string, bool> func;
					if ((func = CS$<>8__locals1.<>9__6) == null)
					{
						func = (CS$<>8__locals1.<>9__6 = (string p) => CS$<>8__locals1.<>4__this._recognition.OutputStrings().All((string i) => i.StartsWith(p)));
					}
					return enumerable3.Where(func).Take(1).ToList<string>();
				});
				list.AddRange(enumerable);
			}
			this._cancellation.ThrowIfCancellationRequested();
			List<char> list3 = CS$<>8__locals1.result.TakeWhileDelimiter().ToList<char>();
			if (list3.Any<char>())
			{
				list.Add(string.Join<char>(string.Empty, list3));
			}
			this._cancellation.ThrowIfCancellationRequested();
			bool flag3 = !list2.Any<string>();
			bool flag4 = true;
			bool flag5 = true;
			ReadOnlySpan<char> readOnlySpan2 = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> readOnlySpan3 = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> readOnlySpan4 = default(ReadOnlySpan<char>);
			ReadOnlySpan<char> readOnlySpan5 = default(ReadOnlySpan<char>);
			for (int i = 1; i <= CS$<>8__locals1.result.Length; i++)
			{
				ReadOnlySpan<char> readOnlySpan6 = readOnlySpan.Take(i);
				bool flag6 = this._recognition.Contains(inputRow, readOnlySpan6, false);
				flag3 = flag3 && flag6;
				bool flag7;
				if (flag5)
				{
					flag7 = !this._recognition.Contains(inputRow, from sub in readOnlySpan6.ToString().AllSubstrings()
						where !sub.AllDelimiters()
						select sub, false);
				}
				else
				{
					flag7 = false;
				}
				flag5 = flag7;
				flag4 = flag4 && (!flag6 || readOnlySpan6.AnyDelimiter());
				if (!flag3 && !flag5 && !flag4)
				{
					break;
				}
				if (readOnlySpan6.Length != 1 && readOnlySpan6.Last<char>().IsDelimiter())
				{
					list.Add(readOnlySpan6.ToString());
					list.Add(readOnlySpan5.ToString());
				}
				if (flag3)
				{
					readOnlySpan2 = readOnlySpan6;
				}
				if (flag5)
				{
					list.Add(readOnlySpan6.ToString());
					readOnlySpan3 = readOnlySpan6;
				}
				if (flag4)
				{
					readOnlySpan4 = readOnlySpan6;
				}
				readOnlySpan5 = readOnlySpan6;
				this._cancellation.ThrowIfCancellationRequested();
			}
			if (readOnlySpan2.Any<char>())
			{
				list.Add(readOnlySpan2.ToString());
			}
			if (readOnlySpan4.Any<char>())
			{
				list.Add(readOnlySpan4.ToString());
			}
			if (readOnlySpan3.Any<char>())
			{
				list.Add(readOnlySpan3.ToString());
			}
			IEnumerable<string> enumerable2 = from prefix1 in list.Distinct<string>()
				where !string.IsNullOrEmpty(prefix1) && prefix1 != CS$<>8__locals1.result
				select prefix1;
			return CS$<>8__locals1.<Prefixes>g__SetCache|0(enumerable2.ToList<string>());
		}

		// Token: 0x04004DC9 RID: 19913
		private readonly CancellationToken _cancellation;

		// Token: 0x04004DCA RID: 19914
		private readonly IReadOnlyCollection<Example<IRow, object>> _examples;

		// Token: 0x04004DCB RID: 19915
		private readonly Recognition _recognition;
	}
}
