using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x0200175D RID: 5981
	public class OutputTokenFactory
	{
		// Token: 0x0600C656 RID: 50774 RVA: 0x002AA3FC File Offset: 0x002A85FC
		private OutputTokenFactory(Example<IRow, object> example, Recognition recognition, LearnDebugTrace debugTrace, CancellationToken cancellation)
		{
			this._inputRow = example.Input;
			this._output = example.Output as string;
			this._recognition = recognition;
			this._cancellation = cancellation;
			this._debugTrace = debugTrace;
			if (string.IsNullOrEmpty(this._output))
			{
				return;
			}
			this._formattedNumberTokenCache = new Dictionary<int, List<FormattedNumberOutputToken>>();
			this._substringTokenCache = new Dictionary<int, List<SubstringOutputToken>>();
			this._constantStringTokenCache = new Dictionary<int, List<ConstantStringOutputToken>>();
			foreach (int num in Utils.Range(0, this._output.Length - 1).ToArray<int>())
			{
				this._formattedNumberTokenCache[num] = null;
				this._substringTokenCache[num] = null;
				this._constantStringTokenCache[num] = null;
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x0600C657 RID: 50775 RVA: 0x002AA4C3 File Offset: 0x002A86C3
		private int[] FindOffsetRange
		{
			get
			{
				return this._recognition.FindOffsetRange;
			}
		}

		// Token: 0x0600C658 RID: 50776 RVA: 0x002AA4D0 File Offset: 0x002A86D0
		public static OutputTokenFactory Create(Example<IRow, object> example, Recognition recognition, LearnDebugTrace debugTrace, CancellationToken cancellation)
		{
			return new OutputTokenFactory(example, recognition, debugTrace, cancellation);
		}

		// Token: 0x0600C659 RID: 50777 RVA: 0x002AA4DB File Offset: 0x002A86DB
		public IEnumerable<OutputToken> Tokens()
		{
			if (string.IsNullOrEmpty(this._output))
			{
				yield break;
			}
			int totalTokenCount = 0;
			int index = 0;
			do
			{
				IEnumerable<OutputToken> enumerable = this.Tokens(index);
				int num;
				foreach (OutputToken outputToken in enumerable)
				{
					num = totalTokenCount;
					totalTokenCount = num + 1;
					yield return outputToken;
					this._cancellation.ThrowIfCancellationRequested();
				}
				IEnumerator<OutputToken> enumerator = null;
				num = index;
				index = num + 1;
			}
			while (totalTokenCount <= 10000 && index < this._output.Length);
			if (totalTokenCount > 10000)
			{
				throw new Exception(string.Format("{0}.{1}: Too many tokens found. ({2:N0})", "OutputTokenFactory", "Tokens", totalTokenCount));
			}
			yield break;
			yield break;
		}

		// Token: 0x0600C65A RID: 50778 RVA: 0x002AA4EB File Offset: 0x002A86EB
		public IEnumerable<OutputToken> Tokens(int index)
		{
			ReadOnlySpan<char> readOnlySpan = this._output.AsSpan();
			if (readOnlySpan.None<char>())
			{
				yield break;
			}
			if (index >= readOnlySpan.Length)
			{
				yield break;
			}
			this._cancellation.ThrowIfCancellationRequested();
			foreach (DynamicOutputToken dynamicOutputToken in this.DynamicTokens(index))
			{
				yield return dynamicOutputToken;
			}
			IEnumerator<DynamicOutputToken> enumerator = null;
			foreach (ConstantStringOutputToken constantStringOutputToken in this.ResolveConstantString(index))
			{
				yield return constantStringOutputToken;
			}
			IEnumerator<ConstantStringOutputToken> enumerator2 = null;
			this._cancellation.ThrowIfCancellationRequested();
			yield break;
			yield break;
		}

		// Token: 0x0600C65B RID: 50779 RVA: 0x002AA502 File Offset: 0x002A8702
		private IEnumerable<DynamicOutputToken> DynamicTokens(int index)
		{
			foreach (FormattedDateTimeOutputToken formattedDateTimeOutputToken in this.ResolveDateTime(index))
			{
				yield return formattedDateTimeOutputToken;
			}
			IEnumerator<FormattedDateTimeOutputToken> enumerator = null;
			this._cancellation.ThrowIfCancellationRequested();
			foreach (FormattedNumberOutputToken formattedNumberOutputToken in this.ResolveNumber(index))
			{
				yield return formattedNumberOutputToken;
			}
			IEnumerator<FormattedNumberOutputToken> enumerator2 = null;
			this._cancellation.ThrowIfCancellationRequested();
			foreach (SubstringOutputToken substringOutputToken in this.ResolveSubstring(index))
			{
				yield return substringOutputToken;
			}
			IEnumerator<SubstringOutputToken> enumerator3 = null;
			this._cancellation.ThrowIfCancellationRequested();
			yield break;
			yield break;
		}

		// Token: 0x0600C65C RID: 50780 RVA: 0x002AA519 File Offset: 0x002A8719
		private IEnumerable<DynamicOutputToken> FindDynamicTokens(int index)
		{
			int tokenCount = 0;
			if (index >= this._output.Length)
			{
				yield break;
			}
			do
			{
				int num;
				foreach (DynamicOutputToken dynamicOutputToken in this.DynamicTokens(index))
				{
					num = tokenCount;
					tokenCount = num + 1;
					yield return dynamicOutputToken;
					this._cancellation.ThrowIfCancellationRequested();
				}
				IEnumerator<DynamicOutputToken> enumerator = null;
				num = index;
				index = num + 1;
			}
			while (tokenCount <= 10000 && index < this._output.Length);
			if (tokenCount > 10000)
			{
				throw new Exception(string.Format("{0}.{1}: Too many tokens found. ({2:N0})", "OutputTokenFactory", "DynamicTokens", tokenCount));
			}
			yield break;
			yield break;
		}

		// Token: 0x0600C65D RID: 50781 RVA: 0x002AA530 File Offset: 0x002A8730
		private IEnumerable<ConstantStringOutputToken> ResolveConstantString(int index)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<ConstantStringOutputToken> enumerable;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveConstantString", true, true) : null)
			{
				ReadOnlySpan<char> readOnlySpan = this._output.AsSpan();
				ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Skip(index);
				if (readOnlySpan2.None<char>())
				{
					enumerable = OutputTokenFactory._emptyConstantStringTokens;
				}
				else
				{
					List<ConstantStringOutputToken> list = new List<ConstantStringOutputToken>();
					ReadOnlySpan<char> readOnlySpan3 = readOnlySpan2.TakeWhileDelimiter();
					if (readOnlySpan3.Length > 0)
					{
						list.Add(new ConstantStringOutputToken
						{
							FirstIndex = index,
							Output = readOnlySpan3.ToString()
						});
					}
					HashSet<int> hashSet = new HashSet<int>();
					foreach (DynamicOutputToken dynamicOutputToken in this.FindDynamicTokens(index))
					{
						if (dynamicOutputToken.FirstIndex == index)
						{
							break;
						}
						if (!hashSet.Contains(dynamicOutputToken.FirstIndex))
						{
							hashSet.Add(dynamicOutputToken.FirstIndex);
							string text = readOnlySpan.SliceRange(index, dynamicOutputToken.FirstIndex).ToString();
							if (!(text == dynamicOutputToken.Output))
							{
								ConstantStringOutputToken constantStringOutputToken = new ConstantStringOutputToken
								{
									FirstIndex = index,
									Output = text,
									NextToken = dynamicOutputToken
								};
								list.Add(constantStringOutputToken);
							}
						}
					}
					enumerable = list;
				}
			}
			return enumerable;
		}

		// Token: 0x0600C65E RID: 50782 RVA: 0x002AA6C8 File Offset: 0x002A88C8
		private IEnumerable<FormattedDateTimeOutputToken> ResolveDateTime(int index)
		{
			OutputTokenFactory.<ResolveDateTime>d__23 <ResolveDateTime>d__ = new OutputTokenFactory.<ResolveDateTime>d__23(-2);
			<ResolveDateTime>d__.<>4__this = this;
			<ResolveDateTime>d__.<>3__index = index;
			return <ResolveDateTime>d__;
		}

		// Token: 0x0600C65F RID: 50783 RVA: 0x002AA6DF File Offset: 0x002A88DF
		private IEnumerable<FormattedNumberOutputToken> ResolveNumber(int index)
		{
			OutputTokenFactory.<ResolveNumber>d__24 <ResolveNumber>d__ = new OutputTokenFactory.<ResolveNumber>d__24(-2);
			<ResolveNumber>d__.<>4__this = this;
			<ResolveNumber>d__.<>3__index = index;
			return <ResolveNumber>d__;
		}

		// Token: 0x0600C660 RID: 50784 RVA: 0x002AA6F8 File Offset: 0x002A88F8
		private IEnumerable<SubstringOutputToken> ResolveSubstring(int index)
		{
			ReadOnlySpan<char> readOnlySpan = this._output.AsSpan().Take(index);
			IReadOnlyList<SubstringOutputToken> readOnlyList;
			if (this.TrySubstringCache(index, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<SubstringOutputToken> enumerable;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveSubstring", true, true) : null)
			{
				enumerable = this.SetCache(index, (readOnlySpan.Length <= 3) ? this.ResolveSubstringByDescending(index) : this.ResolveSubstringByBisect(index));
			}
			return enumerable;
		}

		// Token: 0x0600C661 RID: 50785 RVA: 0x002AA784 File Offset: 0x002A8984
		private IEnumerable<SubstringOutputToken> ResolveSubstringByBisect(int startIndex)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<SubstringOutputToken> enumerable;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveSubstringByBisect", true, true) : null)
			{
				ReadOnlySpan<char> readOnlySpan = this._output.AsSpan().Skip(startIndex);
				if (readOnlySpan.None<char>() || readOnlySpan.AllDelimiters())
				{
					enumerable = OutputTokenFactory._emptySubstringTokens;
				}
				else
				{
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					ReadOnlySpan<char> readOnlySpan2 = default(ReadOnlySpan<char>);
					try
					{
						int num4 = 0;
						int num5 = readOnlySpan.Length;
						int num6 = (int)Math.Ceiling((double)num5 * 0.5);
						while (num++ < 10000)
						{
							ReadOnlySpan<char> readOnlySpan3 = readOnlySpan.Take(num6);
							bool flag = this._recognition.Contains(this._inputRow, readOnlySpan3, true);
							int num7 = num5 - num6;
							if (flag)
							{
								readOnlySpan2 = readOnlySpan3;
								num4 = num6;
								num6 += (int)Math.Ceiling((double)(num5 - num6) * 0.5);
								if (num7 <= 1)
								{
									num2++;
									break;
								}
							}
							else
							{
								if (num7 <= 1)
								{
									break;
								}
								num5 = num6;
								num6 -= (int)Math.Ceiling((double)(num6 - num4) * 0.5);
								num3++;
							}
						}
						if (num > 10000)
						{
							throw new Exception(string.Format("{0}: Too many substring iterations. ({1:N0})", "OutputTokenFactory", num));
						}
					}
					finally
					{
						LearnDebugTrace debugTrace2 = this._debugTrace;
						if (debugTrace2 != null)
						{
							debugTrace2.HitEvent("OutputTokenFactory", "ResolveSubstringByBisect", num2);
						}
						LearnDebugTrace debugTrace3 = this._debugTrace;
						if (debugTrace3 != null)
						{
							debugTrace3.MissEvent("OutputTokenFactory", "ResolveSubstringByBisect", num3);
						}
					}
					if (readOnlySpan2 == default(ReadOnlySpan<char>))
					{
						enumerable = OutputTokenFactory._emptySubstringTokens;
					}
					else
					{
						List<SubstringOutputToken> list = new List<SubstringOutputToken>();
						bool flag2 = false;
						IReadOnlyList<SubstringDescriptor> readOnlyList;
						while (readOnlySpan2.Length >= 1 && this._recognition.TrySubstring(this._inputRow, readOnlySpan2.ToString(), out readOnlyList, true))
						{
							if (!readOnlySpan2.Last<char>().IsDelimiter())
							{
								list.Add(new SubstringOutputToken
								{
									FirstIndex = startIndex,
									Output = readOnlySpan2.ToString(),
									Descriptors = readOnlyList,
									Partial = flag2
								});
								flag2 = true;
							}
							readOnlySpan2 = readOnlySpan2.Take(readOnlySpan2.Length - 1);
						}
						enumerable = list;
					}
				}
			}
			return enumerable;
		}

		// Token: 0x0600C662 RID: 50786 RVA: 0x002AAA00 File Offset: 0x002A8C00
		private IEnumerable<SubstringOutputToken> ResolveSubstringByDescending(int index)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<SubstringOutputToken> enumerable;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveSubstringByDescending", true, true) : null)
			{
				ReadOnlySpan<char> readOnlySpan = this._output.AsSpan().Skip(index);
				if (readOnlySpan.None<char>() || readOnlySpan.AllDelimiters())
				{
					enumerable = OutputTokenFactory._emptySubstringTokens;
				}
				else
				{
					ReadOnlySpan<char> readOnlySpan2 = readOnlySpan;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					List<SubstringOutputToken> list = new List<SubstringOutputToken>();
					try
					{
						bool flag = false;
						while (readOnlySpan2.Length > 0 && num++ <= 10000)
						{
							IReadOnlyList<SubstringDescriptor> readOnlyList;
							if (!this._recognition.TrySubstring(this._inputRow, readOnlySpan2.ToString(), out readOnlyList, true))
							{
								num3++;
								readOnlySpan2 = readOnlySpan2.Take(readOnlySpan2.Length - 1);
							}
							else
							{
								if (!readOnlySpan2.Last<char>().IsDelimiter())
								{
									list.Add(new SubstringOutputToken
									{
										FirstIndex = index,
										Output = readOnlySpan2.ToString(),
										Descriptors = readOnlyList,
										Partial = flag
									});
									flag = true;
								}
								readOnlySpan2 = readOnlySpan2.Take(readOnlySpan2.Length - 1);
							}
						}
						if (num > 10000)
						{
							throw new Exception(string.Format("{0}: Too many substring iterations. ({1:N0})", "OutputTokenFactory", num));
						}
					}
					finally
					{
						LearnDebugTrace debugTrace2 = this._debugTrace;
						if (debugTrace2 != null)
						{
							debugTrace2.HitEvent("OutputTokenFactory", "ResolveSubstringByDescending", num2);
						}
						LearnDebugTrace debugTrace3 = this._debugTrace;
						if (debugTrace3 != null)
						{
							debugTrace3.MissEvent("OutputTokenFactory", "ResolveSubstringByDescending", num3);
						}
					}
					enumerable = list;
				}
			}
			return enumerable;
		}

		// Token: 0x0600C663 RID: 50787 RVA: 0x002AABC8 File Offset: 0x002A8DC8
		private IEnumerable<SubstringOutputToken> SetCache(int index, IEnumerable<SubstringOutputToken> tokens)
		{
			Dictionary<int, List<SubstringOutputToken>> substringTokenCache = this._substringTokenCache;
			if (substringTokenCache[index] == null)
			{
				substringTokenCache[index] = new List<SubstringOutputToken>();
			}
			IReadOnlyList<SubstringOutputToken> readOnlyList = tokens.ToReadOnlyList<SubstringOutputToken>();
			this._substringTokenCache[index].AddRange(readOnlyList);
			return readOnlyList;
		}

		// Token: 0x0600C664 RID: 50788 RVA: 0x002AAC10 File Offset: 0x002A8E10
		private IEnumerable<FormattedNumberOutputToken> SetCache(int index, IEnumerable<FormattedNumberOutputToken> tokens)
		{
			Dictionary<int, List<FormattedNumberOutputToken>> formattedNumberTokenCache = this._formattedNumberTokenCache;
			if (formattedNumberTokenCache[index] == null)
			{
				formattedNumberTokenCache[index] = new List<FormattedNumberOutputToken>();
			}
			IReadOnlyList<FormattedNumberOutputToken> readOnlyList = tokens.ToReadOnlyList<FormattedNumberOutputToken>();
			this._formattedNumberTokenCache[index].AddRange(readOnlyList);
			return readOnlyList;
		}

		// Token: 0x0600C665 RID: 50789 RVA: 0x002AAC58 File Offset: 0x002A8E58
		private IEnumerable<ConstantStringOutputToken> SetCache(int index, IEnumerable<ConstantStringOutputToken> tokens)
		{
			Dictionary<int, List<ConstantStringOutputToken>> constantStringTokenCache = this._constantStringTokenCache;
			if (constantStringTokenCache[index] == null)
			{
				constantStringTokenCache[index] = new List<ConstantStringOutputToken>();
			}
			IReadOnlyList<ConstantStringOutputToken> readOnlyList = tokens.ToReadOnlyList<ConstantStringOutputToken>();
			this._constantStringTokenCache[index].AddRange(readOnlyList);
			return readOnlyList;
		}

		// Token: 0x0600C666 RID: 50790 RVA: 0x002AAC9F File Offset: 0x002A8E9F
		private bool TryConstantStringCache(int index, out IReadOnlyList<ConstantStringOutputToken> tokens)
		{
			tokens = this._constantStringTokenCache[index];
			return tokens != null;
		}

		// Token: 0x0600C667 RID: 50791 RVA: 0x002AACB4 File Offset: 0x002A8EB4
		private bool TryFormattedNumberCache(int index, out IReadOnlyList<FormattedNumberOutputToken> tokens)
		{
			tokens = this._formattedNumberTokenCache[index];
			return tokens != null;
		}

		// Token: 0x0600C668 RID: 50792 RVA: 0x002AACC9 File Offset: 0x002A8EC9
		private bool TrySubstringCache(int index, out IReadOnlyList<SubstringOutputToken> tokens)
		{
			tokens = this._substringTokenCache[index];
			return tokens != null;
		}

		// Token: 0x04004DD4 RID: 19924
		private const int _substringIterationLimit = 10000;

		// Token: 0x04004DD5 RID: 19925
		private const int _tokenLimit = 10000;

		// Token: 0x04004DD6 RID: 19926
		private readonly CancellationToken _cancellation;

		// Token: 0x04004DD7 RID: 19927
		private readonly Dictionary<int, List<ConstantStringOutputToken>> _constantStringTokenCache;

		// Token: 0x04004DD8 RID: 19928
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x04004DD9 RID: 19929
		private static readonly ConstantStringOutputToken[] _emptyConstantStringTokens = new ConstantStringOutputToken[0];

		// Token: 0x04004DDA RID: 19930
		private static readonly FormattedDateTimeOutputToken[] _emptyFormattedDateTimeTokens = new FormattedDateTimeOutputToken[0];

		// Token: 0x04004DDB RID: 19931
		private static readonly FormattedNumberOutputToken[] _emptyFormattedNumberTokens = new FormattedNumberOutputToken[0];

		// Token: 0x04004DDC RID: 19932
		private static readonly SubstringOutputToken[] _emptySubstringTokens = new SubstringOutputToken[0];

		// Token: 0x04004DDD RID: 19933
		private readonly Dictionary<int, List<FormattedNumberOutputToken>> _formattedNumberTokenCache;

		// Token: 0x04004DDE RID: 19934
		private readonly IRow _inputRow;

		// Token: 0x04004DDF RID: 19935
		private readonly string _output;

		// Token: 0x04004DE0 RID: 19936
		private readonly Recognition _recognition;

		// Token: 0x04004DE1 RID: 19937
		private readonly Dictionary<int, List<SubstringOutputToken>> _substringTokenCache;
	}
}
