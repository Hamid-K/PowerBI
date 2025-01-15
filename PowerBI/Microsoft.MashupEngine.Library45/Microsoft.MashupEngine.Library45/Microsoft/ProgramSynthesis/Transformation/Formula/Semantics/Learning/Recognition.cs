using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001619 RID: 5657
	public class Recognition
	{
		// Token: 0x0600BCCE RID: 48334 RVA: 0x0028884C File Offset: 0x00286A4C
		private IReadOnlyList<decimal[]> InputNumberSets824(IRow inputRow)
		{
			object[] array = new object[] { "InputNumberSets824", inputRow };
			IReadOnlyList<decimal[]> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal[]>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			IReadOnlyList<string> numberColumnNames = (from columnName in inputRow.ColumnNames
				let value = Operators.FromNumber(inputRow, columnName)
				where value != null
				orderby columnName
				select columnName).ToReadOnlyList<string>();
			if (!numberColumnNames.Any<string>())
			{
				return this.CacheSetEmptyList<decimal[]>(array);
			}
			if (numberColumnNames.Count > this._fromNumbersColumnLimit)
			{
				IReadOnlyList<string> readOnlyList2 = numberColumnNames.Where((string columnName) => this._columnNamePriority.Contains(columnName)).Take(this._fromNumbersColumnLimit).ToReadOnlyList<string>();
				IReadOnlyList<string> readOnlyList3 = numberColumnNames.Except(readOnlyList2).Take(this._fromNumbersColumnLimit - readOnlyList2.Count).ToReadOnlyList<string>();
				numberColumnNames = (from i in readOnlyList2.Concat(readOnlyList3)
					orderby i
					select i).ToReadOnlyList<string>();
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			int count = numberColumnNames.Count;
			var readOnlyList4 = (from bitPlace in Enumerable.Range(0, count)
				select new
				{
					Place = bitPlace,
					Value = Convert.ToInt64(Math.Pow(2.0, (double)bitPlace))
				}).ToReadOnlyList();
			List<string[]> list = new List<string[]>();
			long num = Convert.ToInt64(Math.Pow(2.0, (double)count));
			long currentValue = 1L;
			Func<<>f__AnonymousType75<int, long>, bool> <>9__8;
			Func<<>f__AnonymousType75<int, long>, string> <>9__9;
			while (currentValue < num)
			{
				var enumerable = readOnlyList4;
				var func;
				if ((func = <>9__8) == null)
				{
					func = (<>9__8 = bitPlace => (currentValue & bitPlace.Value) == bitPlace.Value);
				}
				var enumerable2 = enumerable.Where(func);
				var func2;
				if ((func2 = <>9__9) == null)
				{
					func2 = (<>9__9 = bitPlace => numberColumnNames[bitPlace.Place]);
				}
				string[] array2 = enumerable2.Select(func2).ToArray<string>();
				if (array2.Length > 2)
				{
					list.Add(array2);
				}
				long currentValue2 = currentValue;
				currentValue = currentValue2 + 1L;
				this._cancellationToken.ThrowIfCancellationRequested();
			}
			readOnlyList = list.Select((string[] columnNameSet) => (from columnName in columnNameSet
				let value = Operators.FromNumber(inputRow, columnName)
				where value != null
				select value.Value).ToArray<decimal>()).ToReadOnlyList<decimal[]>();
			this._cancellationToken.ThrowIfCancellationRequested();
			return this.CacheSet<IReadOnlyList<decimal[]>>(array, readOnlyList);
		}

		// Token: 0x0600BCCF RID: 48335 RVA: 0x00288B14 File Offset: 0x00286D14
		private IReadOnlyList<Record<decimal, decimal>> NumberPairSources824(IRow inputRow)
		{
			object[] array = new object[] { "NumberPairSources824", inputRow };
			IReadOnlyList<Record<decimal, decimal>> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<Record<decimal, decimal>>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			var list = (from columnName in this.NumberColumnNames824(inputRow, null)
				where !this._allZeroColumns.Contains(columnName)
				let value = Operators.FromNumber(inputRow, columnName)
				where value != null && this._numberSourceKinds.HasFlag(NumberSourceKind.Input)
				orderby columnName
				select new
				{
					ColumnName = columnName,
					Value = value.Value
				}).ToList();
			IEnumerable<Record<decimal, decimal>> enumerable = (from pair in list.CartesianProduct(list)
				where pair.Item1.ColumnName != pair.Item2.ColumnName
				select Record.Create<decimal, decimal>(pair.Item1.Value, pair.Item2.Value)).ToList<Record<decimal, decimal>>();
			this._cancellationToken.ThrowIfCancellationRequested();
			IReadOnlyList<decimal> readOnlyList3;
			if (!this._numberSourceKinds.HasFlag(NumberSourceKind.Parsed))
			{
				IReadOnlyList<decimal> readOnlyList2 = new decimal[0];
				readOnlyList3 = readOnlyList2;
			}
			else
			{
				readOnlyList3 = this.NumberSources824(inputRow, true, NumberSourceKind.Parsed);
			}
			IReadOnlyList<decimal> readOnlyList4 = readOnlyList3;
			this._cancellationToken.ThrowIfCancellationRequested();
			IEnumerable<Record<decimal, decimal>> enumerable2 = list.Select(i => i.Value).CartesianProduct(readOnlyList4);
			IEnumerable<Record<decimal, decimal>> enumerable3 = from pair in readOnlyList4.CartesianProduct(readOnlyList4)
				where pair.Item1 != pair.Item2
				select pair;
			this._cancellationToken.ThrowIfCancellationRequested();
			readOnlyList = enumerable.Concat(enumerable2).Concat(enumerable3).Distinct<Record<decimal, decimal>>()
				.ToList<Record<decimal, decimal>>();
			this._cancellationToken.ThrowIfCancellationRequested();
			return this.CacheSet<IReadOnlyList<Record<decimal, decimal>>>(array, readOnlyList);
		}

		// Token: 0x0600BCD0 RID: 48336 RVA: 0x00288D2C File Offset: 0x00286F2C
		public IReadOnlyList<decimal> FindArithmeticNumbers824(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindArithmeticNumbers824", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			IEnumerable<decimal> enumerable = this.FindAddNumbers824(inputRow, result).Union(this.FindSubtractNumbers824(inputRow, result)).Union(this.FindMultiplyNumbers824(inputRow, result))
				.Union(this.FindDivideNumbers824(inputRow, result))
				.Union(this.FindSumNumbers824(inputRow))
				.Union(this.FindProductNumbers824(inputRow))
				.Union(this.FindAverageNumbers824(inputRow));
			return this.CacheSet<List<decimal>>(array, enumerable.ToList<decimal>());
		}

		// Token: 0x0600BCD1 RID: 48337 RVA: 0x00288DD0 File Offset: 0x00286FD0
		public IReadOnlyList<decimal> FindAddNumbers824(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindAddNumbers824", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			IEnumerable<decimal> enumerable = (from inputNumberPair in this.NumberPairSources824(inputRow)
				select Operators.Add(inputNumberPair.Item1, inputNumberPair.Item2)).Collect<decimal>();
			if (result != null && this._enableArithmeticConstants)
			{
				enumerable = enumerable.Union(from inputNumber in this.NumberSources824(inputRow, true, this._numberSourceKinds)
					from formatNumber in this.ReverseFormattedNumber(result)
					let roundInput = Math.Round(inputNumber, formatNumber.Scale)
					let factor = formatNumber.Value - roundInput
					where factor > 0m
					let predictedResult = Operators.Add(inputNumber, factor)
					where predictedResult != null
					select predictedResult.Value);
			}
			return this.CacheSet<List<decimal>>(array, enumerable.Distinct<decimal>().ToList<decimal>());
		}

		// Token: 0x0600BCD2 RID: 48338 RVA: 0x00288FA4 File Offset: 0x002871A4
		public bool TryAdd824(IRow inputRow, decimal result, out IEnumerable<Record<decimal, decimal>> valuePairs)
		{
			object[] array = new object[] { "TryAdd824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<Record<decimal, decimal>>>(array, out valuePairs))
			{
				return valuePairs.Any<Record<decimal, decimal>>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				valuePairs = this.CacheSetEmptyList<Record<decimal, decimal>>(array);
				return false;
			}
			IReadOnlyList<Record<decimal, decimal>> readOnlyList = this.NumberPairSources824(inputRow);
			if (this._enableArithmeticConstants)
			{
				readOnlyList = readOnlyList.Union(from sourceLeft in this.NumberSources824(inputRow, true, this._numberSourceKinds)
					let pairLeft = sourceLeft.Normalize()
					let pairRight = (result - pairLeft).Normalize()
					where pairRight > 0m
					select Record.Create<decimal, decimal>(pairLeft, pairRight)).ToList<Record<decimal, decimal>>();
			}
			valuePairs = readOnlyList.Where(delegate(Record<decimal, decimal> pair)
			{
				decimal? num = Operators.Add(pair.Item1, pair.Item2);
				decimal result2 = result;
				return (num.GetValueOrDefault() == result2) & (num != null);
			});
			return this.CacheSet<List<Record<decimal, decimal>>>(array, valuePairs.ToList<Record<decimal, decimal>>()).Any<Record<decimal, decimal>>();
		}

		// Token: 0x0600BCD3 RID: 48339 RVA: 0x002890D8 File Offset: 0x002872D8
		public IReadOnlyList<decimal> FindSubtractNumbers824(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindSubtractNumbers824", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			IEnumerable<decimal> enumerable = (from inputNumberPair in this.NumberPairSources824(inputRow)
				select Operators.Subtract(inputNumberPair.Item1, inputNumberPair.Item2)).Collect<decimal>();
			if (result != null && this._enableArithmeticConstants)
			{
				enumerable = enumerable.Union(from inputNumber in this.NumberSources824(inputRow, true, this._numberSourceKinds)
					from formatNumber in this.ReverseFormattedNumber(result)
					let roundInput = Math.Round(inputNumber, formatNumber.Scale)
					let factor = roundInput - formatNumber.Value
					where factor > 0m
					let predictedResult = Operators.Subtract(inputNumber, factor)
					where predictedResult != null
					select predictedResult.Value);
			}
			return this.CacheSet<List<decimal>>(array, enumerable.Distinct<decimal>().ToList<decimal>());
		}

		// Token: 0x0600BCD4 RID: 48340 RVA: 0x002892AC File Offset: 0x002874AC
		public bool TrySubtract824(IRow inputRow, decimal result, out IEnumerable<Record<decimal, decimal>> valuePairs)
		{
			object[] array = new object[] { "TrySubtract824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<Record<decimal, decimal>>>(array, out valuePairs))
			{
				return valuePairs.Any<Record<decimal, decimal>>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				valuePairs = this.CacheSetEmptyList<Record<decimal, decimal>>(array);
				return false;
			}
			IReadOnlyList<Record<decimal, decimal>> readOnlyList = this.NumberPairSources824(inputRow);
			if (this._enableArithmeticConstants)
			{
				readOnlyList = readOnlyList.Union(from sourceLeft in this.NumberSources824(inputRow, true, this._numberSourceKinds)
					let pairLeft = sourceLeft.Normalize()
					let pairRight = pairLeft - result
					where pairRight > 0m
					select Record.Create<decimal, decimal>(pairLeft, pairRight.Normalize())).ToList<Record<decimal, decimal>>();
			}
			valuePairs = readOnlyList.Where(delegate(Record<decimal, decimal> pair)
			{
				decimal? num = Operators.Subtract(pair.Item1, pair.Item2);
				decimal result2 = result;
				return (num.GetValueOrDefault() == result2) & (num != null);
			});
			return this.CacheSet<List<Record<decimal, decimal>>>(array, valuePairs.ToList<Record<decimal, decimal>>()).Any<Record<decimal, decimal>>();
		}

		// Token: 0x0600BCD5 RID: 48341 RVA: 0x002893E0 File Offset: 0x002875E0
		public IReadOnlyList<decimal> FindMultiplyNumbers824(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindMultiplyNumbers824", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			IEnumerable<decimal> enumerable = (from inputNumberPair in this.NumberPairSources824(inputRow)
				where inputNumberPair.Item2 != 0m
				select Operators.Multiply(inputNumberPair.Item1, inputNumberPair.Item2)).Collect<decimal>();
			if (result != null && this._enableArithmeticConstants)
			{
				enumerable = enumerable.Union(from <>h__TransparentIdentifier2 in (from inputNumber in this.NumberSources824(inputRow, true, this._numberSourceKinds)
						from formatNumber in this.ReverseFormattedNumber(result)
						let scale = (formatNumber.Scale == 0) ? 0 : (formatNumber.Scale - 1)
						select new
						{
							<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
							factor = ((inputNumber == 0m) ? null : new decimal?(Math.Round(formatNumber.Value / inputNumber, scale)))
						}).Where(delegate(<>h__TransparentIdentifier2)
					{
						decimal? factor = factor;
						if (factor != null)
						{
							decimal valueOrDefault = factor.GetValueOrDefault();
							if (valueOrDefault < -1m || valueOrDefault > 1m)
							{
								return true;
							}
						}
						return false;
					})
					let predictedResult = Operators.Multiply(inputNumber, factor.Value)
					where predictedResult != null
					select predictedResult.Value);
			}
			return this.CacheSet<List<decimal>>(array, enumerable.Distinct<decimal>().ToList<decimal>());
		}

		// Token: 0x0600BCD6 RID: 48342 RVA: 0x002895D8 File Offset: 0x002877D8
		public bool TryMultiply824(IRow inputRow, decimal result, out IEnumerable<Record<decimal, decimal>> valuePairs)
		{
			object[] array = new object[] { "TryMultiply824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<Record<decimal, decimal>>>(array, out valuePairs))
			{
				return valuePairs.Any<Record<decimal, decimal>>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				valuePairs = this.CacheSetEmptyList<Record<decimal, decimal>>(array);
				return false;
			}
			IReadOnlyList<Record<decimal, decimal>> readOnlyList = this.NumberPairSources824(inputRow);
			if (this._enableArithmeticConstants)
			{
				readOnlyList = readOnlyList.Union(from <>h__TransparentIdentifier1 in (from sourceLeft in this.NumberSources824(inputRow, true, this._numberSourceKinds)
						select new
						{
							sourceLeft = sourceLeft,
							pairLeft = sourceLeft.Normalize()
						}).Select(delegate(<>h__TransparentIdentifier0)
					{
						decimal? num;
						decimal? num2;
						if (!(<>h__TransparentIdentifier0.pairLeft == 0m))
						{
							num = new decimal?(result / <>h__TransparentIdentifier0.pairLeft);
						}
						else
						{
							num2 = null;
							num = num2;
						}
						num2 = num;
						return new
						{
							<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
							pairRight = ((num2 != null) ? new decimal?(num2.GetValueOrDefault().Normalize()) : null)
						};
					}).Where(delegate(<>h__TransparentIdentifier1)
					{
						decimal? pairRight = <>h__TransparentIdentifier1.pairRight;
						if (pairRight != null)
						{
							decimal valueOrDefault = pairRight.GetValueOrDefault();
							if (valueOrDefault <= -1m || valueOrDefault > 1m)
							{
								return true;
							}
						}
						return false;
					})
					select Record.Create<decimal, decimal>(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.pairLeft, <>h__TransparentIdentifier1.pairRight.Value)).ToList<Record<decimal, decimal>>();
			}
			valuePairs = readOnlyList.Where(delegate(Record<decimal, decimal> pair)
			{
				decimal? num3 = Operators.Multiply(pair.Item1, pair.Item2);
				decimal result2 = result;
				return (num3.GetValueOrDefault() == result2) & (num3 != null);
			});
			return this.CacheSet<List<Record<decimal, decimal>>>(array, valuePairs.ToList<Record<decimal, decimal>>()).Any<Record<decimal, decimal>>();
		}

		// Token: 0x0600BCD7 RID: 48343 RVA: 0x0028970C File Offset: 0x0028790C
		public IReadOnlyList<decimal> FindDivideNumbers824(IRow inputRow, string result)
		{
			object[] array = new object[] { "FindDivideNumbers824", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			IEnumerable<decimal> enumerable = (from inputNumberPair in this.NumberPairSources824(inputRow)
				where inputNumberPair.Item2 != 0m
				select Operators.Divide(inputNumberPair.Item1, inputNumberPair.Item2)).Collect<decimal>();
			if (result != null && this._enableArithmeticConstants)
			{
				enumerable = enumerable.Union(from <>h__TransparentIdentifier2 in (from inputNumber in this.NumberSources824(inputRow, true, this._numberSourceKinds)
						from formatNumber in this.ReverseFormattedNumber(result)
						let scale = (formatNumber.Scale == 0) ? 0 : (formatNumber.Scale - 1)
						select new
						{
							<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
							factor = ((formatNumber.Value == 0m) ? null : new decimal?(Math.Round(inputNumber / formatNumber.Value, scale)))
						}).Where(delegate(<>h__TransparentIdentifier2)
					{
						decimal? factor = factor;
						if (factor != null)
						{
							decimal valueOrDefault = factor.GetValueOrDefault();
							if (valueOrDefault < -1m || valueOrDefault > 1m)
							{
								return true;
							}
						}
						return false;
					})
					let predictedResult = Operators.Divide(inputNumber, factor.Value)
					where predictedResult != null
					select predictedResult.Value);
			}
			return this.CacheSet<List<decimal>>(array, enumerable.Distinct<decimal>().ToList<decimal>());
		}

		// Token: 0x0600BCD8 RID: 48344 RVA: 0x00289904 File Offset: 0x00287B04
		public bool TryDivide824(IRow inputRow, decimal result, out IEnumerable<Record<decimal, decimal>> valuePairs)
		{
			object[] array = new object[] { "TryDivide824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<Record<decimal, decimal>>>(array, out valuePairs))
			{
				return valuePairs.Any<Record<decimal, decimal>>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				valuePairs = this.CacheSetEmptyList<Record<decimal, decimal>>(array);
				return false;
			}
			IReadOnlyList<Record<decimal, decimal>> readOnlyList = this.NumberPairSources824(inputRow);
			if (this._enableArithmeticConstants)
			{
				readOnlyList = readOnlyList.Union(from <>h__TransparentIdentifier1 in (from sourceLeft in this.NumberSources824(inputRow, true, this._numberSourceKinds)
						select new
						{
							sourceLeft = sourceLeft,
							pairLeft = sourceLeft.Normalize()
						}).Select(delegate(<>h__TransparentIdentifier0)
					{
						decimal? num;
						decimal? num2;
						if (!(result == 0m))
						{
							num = new decimal?(<>h__TransparentIdentifier0.pairLeft / result);
						}
						else
						{
							num2 = null;
							num = num2;
						}
						num2 = num;
						return new
						{
							<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
							pairRight = ((num2 != null) ? new decimal?(num2.GetValueOrDefault().Normalize()) : null)
						};
					}).Where(delegate(<>h__TransparentIdentifier1)
					{
						decimal? pairRight = <>h__TransparentIdentifier1.pairRight;
						if (pairRight != null)
						{
							decimal valueOrDefault = pairRight.GetValueOrDefault();
							if (valueOrDefault < -1m || valueOrDefault > 1m)
							{
								return true;
							}
						}
						return false;
					})
					select Record.Create<decimal, decimal>(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.pairLeft, <>h__TransparentIdentifier1.pairRight.Value)).ToList<Record<decimal, decimal>>();
			}
			int resultScale = result.Scale();
			valuePairs = readOnlyList.Where(delegate(Record<decimal, decimal> pair)
			{
				decimal? num3;
				return Operators.Divide(pair.Item1, pair.Item2) != null && num3.GetValueOrDefault().Truncate(resultScale) == result;
			});
			return this.CacheSet<List<Record<decimal, decimal>>>(array, valuePairs.ToList<Record<decimal, decimal>>()).Any<Record<decimal, decimal>>();
		}

		// Token: 0x0600BCD9 RID: 48345 RVA: 0x00289A48 File Offset: 0x00287C48
		public IReadOnlyList<decimal> FindSumNumbers824(IRow inputRow)
		{
			object[] array = new object[] { "FindSumNumbers824", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			readOnlyList = (from inputNumberSet in this.InputNumberSets824(inputRow)
				let setResult = Operators.Sum(inputNumberSet)
				where setResult != null
				select setResult.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
			return this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
		}

		// Token: 0x0600BCDA RID: 48346 RVA: 0x00289B0C File Offset: 0x00287D0C
		public bool TrySum824(IRow inputRow, decimal result, out IEnumerable<decimal[]> values)
		{
			object[] array = new object[] { "TrySum824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<decimal[]>>(array, out values))
			{
				return values.Any<decimal[]>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				values = this.CacheSetEmptyList<decimal[]>(array);
				return false;
			}
			values = (from <>h__TransparentIdentifier0 in (from inputNumberSet in this.InputNumberSets824(inputRow)
					select new
					{
						inputNumberSet = inputNumberSet,
						setResult = Operators.Sum(inputNumberSet)
					}).Where(delegate(<>h__TransparentIdentifier0)
				{
					decimal result2 = result;
					decimal? setResult = <>h__TransparentIdentifier0.setResult;
					return (result2 == setResult.GetValueOrDefault()) & (setResult != null);
				})
				select <>h__TransparentIdentifier0.inputNumberSet).ToReadOnlyList<decimal[]>();
			return this.CacheSet<IEnumerable<decimal[]>>(array, values).Any<decimal[]>();
		}

		// Token: 0x0600BCDB RID: 48347 RVA: 0x00289BE4 File Offset: 0x00287DE4
		public IReadOnlyList<decimal> FindProductNumbers824(IRow inputRow)
		{
			object[] array = new object[] { "FindProductNumbers824", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			readOnlyList = (from inputNumberSet in this.InputNumberSets824(inputRow)
				let setResult = Operators.Product(inputNumberSet)
				where setResult != null
				select setResult.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
			return this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
		}

		// Token: 0x0600BCDC RID: 48348 RVA: 0x00289CA8 File Offset: 0x00287EA8
		public bool TryProduct824(IRow inputRow, decimal result, out IEnumerable<decimal[]> values)
		{
			object[] array = new object[] { "TryProduct824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<decimal[]>>(array, out values))
			{
				return values.Any<decimal[]>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				values = this.CacheSetEmptyList<decimal[]>(array);
				return false;
			}
			values = (from <>h__TransparentIdentifier0 in (from inputNumberSet in this.InputNumberSets824(inputRow)
					select new
					{
						inputNumberSet = inputNumberSet,
						setResult = Operators.Product(inputNumberSet)
					}).Where(delegate(<>h__TransparentIdentifier0)
				{
					decimal result2 = result;
					decimal? setResult = <>h__TransparentIdentifier0.setResult;
					return (result2 == setResult.GetValueOrDefault()) & (setResult != null);
				})
				select <>h__TransparentIdentifier0.inputNumberSet).Distinct<decimal[]>().ToReadOnlyList<decimal[]>();
			return this.CacheSet<IEnumerable<decimal[]>>(array, values).Any<decimal[]>();
		}

		// Token: 0x0600BCDD RID: 48349 RVA: 0x00289D84 File Offset: 0x00287F84
		public IReadOnlyList<decimal> FindAverageNumbers824(IRow inputRow)
		{
			object[] array = new object[] { "FindAverageNumbers824", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			readOnlyList = (from inputNumberSet in this.InputNumberSets824(inputRow)
				let setResult = Operators.Average(inputNumberSet)
				where setResult != null
				select setResult.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
			return this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
		}

		// Token: 0x0600BCDE RID: 48350 RVA: 0x00289E48 File Offset: 0x00288048
		public bool TryAverage824(IRow inputRow, decimal result, out IEnumerable<decimal[]> values)
		{
			object[] array = new object[] { "TryAverage824", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<decimal[]>>(array, out values))
			{
				return values.Any<decimal[]>();
			}
			if (!this.CanUseArithmetic824(inputRow))
			{
				values = this.CacheSetEmptyList<decimal[]>(array);
				return false;
			}
			values = (from inputNumberSet in this.InputNumberSets824(inputRow)
				let distinctCount = inputNumberSet.Distinct<decimal>().Count<decimal>()
				where distinctCount > 1
				let setResult = Operators.Average(inputNumberSet)
				where setResult != null
				where result.EqualsWithTruncate(setResult.Value)
				select inputNumberSet).ToReadOnlyList<decimal[]>();
			return this.CacheSet<IEnumerable<decimal[]>>(array, values).Any<decimal[]>();
		}

		// Token: 0x0600BCDF RID: 48351 RVA: 0x00289F8A File Offset: 0x0028818A
		public bool CanUseArithmetic824(IRow inputRow)
		{
			return !this._outputAllZeros && this.NumberSources824(inputRow, false, this._numberSourceKinds).Count >= 1;
		}

		// Token: 0x0600BCE0 RID: 48352 RVA: 0x00289FB0 File Offset: 0x002881B0
		public IReadOnlyList<string> NumberColumnNames824(IRow inputRow, IEnumerable<string> columnNames = null)
		{
			object[] array = new object[] { "NumberColumnNames824", inputRow, columnNames };
			IReadOnlyList<string> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<string>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (columnNames == null)
			{
				columnNames = inputRow.ValidColumnNames();
			}
			readOnlyList = columnNames.Where((string columnName) => Operators.FromNumber(inputRow, columnName) != null).ToReadOnlyList<string>();
			return this.CacheSet<IReadOnlyList<string>>(array, readOnlyList);
		}

		// Token: 0x0600BCE1 RID: 48353 RVA: 0x0028A024 File Offset: 0x00288224
		public IReadOnlyList<decimal> NumberInputs824(IRow inputRow, IEnumerable<string> columnNames = null)
		{
			object[] array = new object[] { "NumberInputs824", inputRow, columnNames };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (columnNames == null)
			{
				columnNames = inputRow.ValidColumnNames();
			}
			readOnlyList = (from columnName in columnNames
				let value = Operators.FromNumber(inputRow, columnName)
				where value != null
				select value.Value).ToReadOnlyList<decimal>();
			return this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
		}

		// Token: 0x0600BCE2 RID: 48354 RVA: 0x0028A0E0 File Offset: 0x002882E0
		public IReadOnlyList<decimal> NumberSources824(IRow inputRow, bool distinct = true, NumberSourceKind include = NumberSourceKind.All)
		{
			object[] array = new object[] { "NumberSources824", inputRow, distinct, include };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			IEnumerable<decimal> enumerable = Utils.Empty<decimal>().ToReadOnlyList<decimal>();
			List<string> list = (from c in inputRow.ValidColumnNames()
				where !this._allZeroColumns.Contains(c)
				select c).ToList<string>();
			IEnumerable<decimal> enumerable2;
			if (!include.HasFlag(NumberSourceKind.Input))
			{
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<decimal> enumerable3 = this.NumberInputs824(inputRow, list);
				enumerable2 = enumerable3;
			}
			IEnumerable<decimal> enumerable4;
			if (!include.HasFlag(NumberSourceKind.Parsed))
			{
				enumerable4 = enumerable;
			}
			else
			{
				enumerable4 = from input in this.InputStrings(inputRow, null)
					from findResult in this.FindNumbers(input.Value, false, new int?(10))
					select findResult.Value;
			}
			IEnumerable<decimal> enumerable5 = enumerable4;
			IEnumerable<decimal> enumerable6 = enumerable2.Union(enumerable5);
			if (distinct)
			{
				enumerable6 = enumerable6.Distinct<decimal>();
			}
			return this.CacheSet<List<decimal>>(array, enumerable6.ToList<decimal>());
		}

		// Token: 0x0600BCE3 RID: 48355 RVA: 0x0028A1DC File Offset: 0x002883DC
		public ArithmeticSourcePairList ArithmeticSourcePairs(IRow inputRow)
		{
			return this.CacheGetOrCompute<ArithmeticSourcePairList>(new object[] { "ArithmeticSourcePairs", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				ArithmeticSourcePairList arithmeticSourcePairList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "ArithmeticSourcePairs", false, true) : null))
				{
					NumberSourceList numberSourceList = this.ArithmeticSources(inputRow);
					ArithmeticSourcePairList arithmeticSourcePairList = new ArithmeticSourcePairList((from product in numberSourceList.CartesianProduct(numberSourceList)
						let left = product.Item1
						let right = product.Item2
						where left.Source != NumberSourceKind.Input || right.Source != NumberSourceKind.Input || (left.Source == NumberSourceKind.Input && right.Source == NumberSourceKind.Input && left.ColumnName != right.ColumnName)
						select new ArithmeticSourcePair(left, right)).Distinct<ArithmeticSourcePair>());
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					arithmeticSourcePairList2 = arithmeticSourcePairList;
				}
				return arithmeticSourcePairList2;
			});
		}

		// Token: 0x0600BCE4 RID: 48356 RVA: 0x0028A228 File Offset: 0x00288428
		public NumberSourceList ArithmeticSources(IRow inputRow)
		{
			Func<NumberSource, bool> <>9__1;
			return this.CacheGetOrCompute<NumberSourceList>(new object[] { "ArithmeticSources", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				NumberSourceList numberSourceList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "ArithmeticSources", false, true) : null))
				{
					IEnumerable<NumberSource> enumerable = this.Numbers(inputRow, this._enableFromNumberCoalesceZero, this._numberSourceKinds);
					Func<NumberSource, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (NumberSource source) => !this._allZeroColumns.Contains(source.ColumnName));
					}
					NumberSourceList numberSourceList = new NumberSourceList(enumerable.Where(func));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					numberSourceList2 = numberSourceList;
				}
				return numberSourceList2;
			});
		}

		// Token: 0x0600BCE5 RID: 48357 RVA: 0x0028A274 File Offset: 0x00288474
		public ArithmeticSetList ArithmeticSourceSets(IRow inputRow)
		{
			object[] array = new object[] { "ArithmeticSourceSets", inputRow };
			ArithmeticSetList arithmeticSetList;
			if (this.CacheTryGetValue<ArithmeticSetList>(array, out arithmeticSetList))
			{
				return arithmeticSetList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			ArithmeticSetList arithmeticSetList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "ArithmeticSourceSets", false, true) : null))
			{
				NumberSourceList numberInputs = this.NumberInputs(inputRow, this._enableFromNumberCoalesceZero);
				if (!numberInputs.Any<NumberSource>())
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					arithmeticSetList2 = this.CacheSet<ArithmeticSetList>(array, new ArithmeticSetList());
				}
				else
				{
					if (numberInputs.Count > this._fromNumbersColumnLimit)
					{
						IReadOnlyList<NumberSource> readOnlyList = numberInputs.Where((NumberSource inputDetail) => this._columnNamePriority.Contains(inputDetail.ColumnName)).Take(this._fromNumbersColumnLimit).ToReadOnlyList<NumberSource>();
						IReadOnlyList<NumberSource> readOnlyList2 = numberInputs.Except(readOnlyList).Take(this._fromNumbersColumnLimit - readOnlyList.Count).ToReadOnlyList<NumberSource>();
						numberInputs = new NumberSourceList(readOnlyList.Concat(readOnlyList2));
					}
					if (!numberInputs.Any<NumberSource>())
					{
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						arithmeticSetList2 = this.CacheSet<ArithmeticSetList>(array, new ArithmeticSetList());
					}
					else
					{
						this._cancellationToken.ThrowIfCancellationRequested();
						int count = numberInputs.Count;
						var readOnlyList3 = (from bitPlace in Enumerable.Range(0, count)
							select new
							{
								Place = bitPlace,
								Value = Convert.ToInt64(Math.Pow(2.0, (double)bitPlace))
							}).ToReadOnlyList();
						List<ArithmeticSet> list = new List<ArithmeticSet>();
						long num = Convert.ToInt64(Math.Pow(2.0, (double)count));
						long currentValue;
						Func<<>f__AnonymousType75<int, long>, bool> <>9__2;
						Func<<>f__AnonymousType75<int, long>, <>f__AnonymousType89<<>f__AnonymousType75<int, long>, NumberSource>> <>9__3;
						long currentValue2;
						for (currentValue = 1L; currentValue < num; currentValue = currentValue2 + 1L)
						{
							var enumerable = readOnlyList3;
							var func;
							if ((func = <>9__2) == null)
							{
								func = (<>9__2 = bitPlace => (currentValue & bitPlace.Value) == bitPlace.Value);
							}
							var enumerable2 = enumerable.Where(func);
							var func2;
							if ((func2 = <>9__3) == null)
							{
								func2 = (<>9__3 = bitPlace => new
								{
									bitPlace = bitPlace,
									detail = numberInputs[bitPlace.Place]
								});
							}
							ArithmeticSet arithmeticSet = new ArithmeticSet((from <>h__TransparentIdentifier0 in enumerable2.Select(func2)
								select <>h__TransparentIdentifier0.detail).ToList<NumberSource>());
							if (arithmeticSet.Count > 2)
							{
								list.Add(new ArithmeticSet(arithmeticSet));
							}
							currentValue2 = currentValue;
						}
						this._cancellationToken.ThrowIfCancellationRequested();
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						arithmeticSetList2 = this.CacheSet<ArithmeticSetList>(array, new ArithmeticSetList(list));
					}
				}
			}
			return arithmeticSetList2;
		}

		// Token: 0x0600BCE6 RID: 48358 RVA: 0x0028A52C File Offset: 0x0028872C
		public bool CanUseArithmetic(IRow inputRow)
		{
			return !this._outputAllZeros && this.ArithmeticSources(inputRow).Values.Length >= 1;
		}

		// Token: 0x0600BCE7 RID: 48359 RVA: 0x0028A54C File Offset: 0x0028874C
		public IReadOnlyList<decimal> FindArithmeticNumbers(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindArithmeticNumbers", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindArithmeticNumbers", false, true) : null))
			{
				IEnumerable<decimal> enumerable = this.FindAddNumbers(inputRow, result).Union(this.FindSubtractNumbers(inputRow, result)).Union(this.FindMultiplyNumbers(inputRow, result))
					.Union(this.FindDivideNumbers(inputRow, result))
					.Union(this.FindSumNumbers(inputRow))
					.Union(this.FindProductNumbers(inputRow))
					.Union(this.FindAverageNumbers(inputRow));
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<decimal>>(array, enumerable.ToList<decimal>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCE8 RID: 48360 RVA: 0x0028A638 File Offset: 0x00288838
		public IReadOnlyList<decimal> FindAddNumbers(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindAddNumbers", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindAddNumbers", false, true) : null))
			{
				IReadOnlyList<FormattedNumberCacheItem> resultNumbers = this.ReverseFormattedNumber(result);
				List<decimal> list = (from sourcePair in this.ArithmeticSourcePairs(inputRow)
					where sourcePair.Add != null
					let addValue = sourcePair.Add.Value
					where result == null || resultNumbers.Any((FormattedNumberCacheItem r) => r.Value == Math.Round(addValue, r.Scale))
					select addValue).ToList<decimal>();
				if (result != null && this._enableArithmeticConstants)
				{
					list.AddRange(from left in this.ArithmeticSources(inputRow).DistinctValues
						from resultNumber in resultNumbers
						let right = resultNumber.Value - Math.Round(left, resultNumber.Scale)
						where right > 0m
						let predictedResult = Operators.Add(left, right)
						where predictedResult != null
						select predictedResult.Value);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<decimal>>(array, list.Distinct<decimal>().ToList<decimal>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCE9 RID: 48361 RVA: 0x0028A8A0 File Offset: 0x00288AA0
		public bool TryAdd(IRow inputRow, decimal result, out IReadOnlyList<ArithmeticPair> pairs)
		{
			object[] array = new object[] { "TryAdd", inputRow, result };
			if (this.CacheTryGetValue<IReadOnlyList<ArithmeticPair>>(array, out pairs))
			{
				return pairs.Any<ArithmeticPair>();
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				pairs = this.CacheSetEmptyList<ArithmeticPair>(array);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryAdd", false, true) : null))
			{
				List<ArithmeticPair> list = new List<ArithmeticPair>();
				list.AddRange(from pair in this.ArithmeticSourcePairs(inputRow)
					select new ArithmeticPair(pair.Left.Value, pair.Right.Value));
				if (this._enableArithmeticConstants)
				{
					list.AddRange(from pairLeft in this.ArithmeticSources(inputRow).DistinctValues
						let pairRight = (result - pairLeft).Normalize()
						where pairRight > 0m
						select new ArithmeticPair(pairLeft, pairRight));
				}
				pairs = list.Distinct<ArithmeticPair>().Where(delegate(ArithmeticPair candidatePair)
				{
					decimal? num = Operators.Add(candidatePair.Left, candidatePair.Right);
					decimal result2 = result;
					return (num.GetValueOrDefault() == result2) & (num != null);
				}).ToReadOnlyList<ArithmeticPair>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag = this.CacheSet<IReadOnlyList<ArithmeticPair>>(array, pairs).Any<ArithmeticPair>();
			}
			return flag;
		}

		// Token: 0x0600BCEA RID: 48362 RVA: 0x0028AA20 File Offset: 0x00288C20
		public IReadOnlyList<decimal> FindSubtractNumbers(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindSubtractNumbers", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindSubtractNumbers", false, true) : null))
			{
				IReadOnlyList<FormattedNumberCacheItem> resultNumbers = this.ReverseFormattedNumber(result);
				List<decimal> list = (from sourcePair in this.ArithmeticSourcePairs(inputRow)
					where sourcePair.Subtract != null
					let subtractValue = sourcePair.Subtract.Value
					where result == null || resultNumbers.Any((FormattedNumberCacheItem r) => r.Value == Math.Round(subtractValue, r.Scale))
					select subtractValue).ToList<decimal>();
				if (this._enableArithmeticConstants)
				{
					list.AddRange(from left in this.ArithmeticSources(inputRow).DistinctValues
						from resultNumber in resultNumbers
						let right = Math.Round(left, resultNumber.Scale) - resultNumber.Value
						where right > 0m
						let predictedResult = Operators.Subtract(left, right)
						where predictedResult != null
						select predictedResult.Value);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<decimal>>(array, list.Distinct<decimal>().ToList<decimal>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCEB RID: 48363 RVA: 0x0028AC7C File Offset: 0x00288E7C
		public bool TrySubtract(IRow inputRow, decimal result, out IReadOnlyList<ArithmeticPair> pairs)
		{
			object[] array = new object[] { "TrySubtract", inputRow, result };
			if (this.CacheTryGetValue<IReadOnlyList<ArithmeticPair>>(array, out pairs))
			{
				return pairs.Any<ArithmeticPair>();
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				pairs = this.CacheSetEmptyList<ArithmeticPair>(array);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TrySubtract", false, true) : null))
			{
				List<ArithmeticPair> list = new List<ArithmeticPair>();
				list.AddRange(from pair in this.ArithmeticSourcePairs(inputRow)
					select new ArithmeticPair(pair.Left.Value, pair.Right.Value));
				if (this._enableArithmeticConstants)
				{
					list.AddRange(from pairLeft in this.ArithmeticSources(inputRow).DistinctValues
						let pairRight = (pairLeft - result).Normalize()
						where pairRight > 0m
						select new ArithmeticPair(pairLeft, pairRight));
				}
				pairs = list.Distinct<ArithmeticPair>().Where(delegate(ArithmeticPair pair)
				{
					decimal? num = Operators.Subtract(pair.Left, pair.Right);
					decimal result2 = result;
					return (num.GetValueOrDefault() == result2) & (num != null);
				}).ToReadOnlyList<ArithmeticPair>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag = this.CacheSet<IReadOnlyList<ArithmeticPair>>(array, pairs).Any<ArithmeticPair>();
			}
			return flag;
		}

		// Token: 0x0600BCEC RID: 48364 RVA: 0x0028ADFC File Offset: 0x00288FFC
		public IReadOnlyList<decimal> FindMultiplyNumbers(IRow inputRow, string result = null)
		{
			object[] array = new object[] { "FindMultiplyNumbers", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindMultiplyNumbers", false, true) : null))
			{
				IReadOnlyList<FormattedNumberCacheItem> resultNumbers = this.ReverseFormattedNumber(result);
				List<decimal> list = (from sourcePair in this.ArithmeticSourcePairs(inputRow)
					where sourcePair.Multiply != null
					let multiplyValue = sourcePair.Multiply.Value
					where result == null || resultNumbers.Any((FormattedNumberCacheItem r) => r.Value == Math.Round(multiplyValue, r.Scale))
					select multiplyValue).ToList<decimal>();
				if (this._enableArithmeticConstants)
				{
					list.AddRange(from <>h__TransparentIdentifier2 in (from left in this.ArithmeticSources(inputRow).DistinctValues
							from resultNumber in resultNumbers
							let scale = (resultNumber.Scale <= 0) ? 0 : (resultNumber.Scale - 1)
							select new
							{
								<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
								right = ((left == 0m) ? null : new decimal?(Math.Round(resultNumber.Value / left, scale)))
							}).Where(delegate(<>h__TransparentIdentifier2)
						{
							decimal? right = right;
							if (right != null)
							{
								decimal valueOrDefault = right.GetValueOrDefault();
								if (valueOrDefault <= -1m || valueOrDefault > 1m)
								{
									return true;
								}
							}
							return false;
						})
						let predictedResult = Operators.Multiply(left, right.Value)
						where predictedResult != null
						select predictedResult.Value);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<decimal>>(array, list.Distinct<decimal>().ToList<decimal>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCED RID: 48365 RVA: 0x0028B07C File Offset: 0x0028927C
		public bool TryMultiply(IRow inputRow, decimal result, out IReadOnlyList<ArithmeticPair> pairs, Dictionary<IRow, IReadOnlyList<decimal>> disjunctiveExamples = null)
		{
			object[] array = new object[4];
			array[0] = "TryMultiply";
			array[1] = inputRow;
			array[2] = result;
			int num = 3;
			Dictionary<IRow, IReadOnlyList<decimal>> disjunctiveExamples2 = disjunctiveExamples;
			array[num] = ((disjunctiveExamples2 != null) ? new int?(disjunctiveExamples2.KeyValueHashCode<decimal>()) : null);
			object[] array2 = array;
			if (this.CacheTryGetValue<IReadOnlyList<ArithmeticPair>>(array2, out pairs))
			{
				return pairs.Any<ArithmeticPair>();
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				pairs = this.CacheSetEmptyList<ArithmeticPair>(array2);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryMultiply", false, true) : null))
			{
				List<ArithmeticPair> list = new List<ArithmeticPair>();
				list.AddRange(from pair in this.ArithmeticSourcePairs(inputRow).Where(delegate(ArithmeticSourcePair pair)
					{
						decimal? multiply = pair.Multiply;
						decimal result2 = result;
						return (multiply.GetValueOrDefault() == result2) & (multiply != null);
					})
					select new ArithmeticPair(pair.Left.Value, pair.Right.Value));
				if (this._enableArithmeticConstants)
				{
					decimal[] distinctValues = this.ArithmeticSources(inputRow).DistinctValues;
					int i = 0;
					Func<IRow, bool> <>9__3;
					Func<IRow, IEnumerable<decimal>> <>9__4;
					Func<<>f__AnonymousType97<IRow, decimal>, IEnumerable<decimal>> <>9__6;
					while (i < distinctValues.Length)
					{
						decimal num2 = distinctValues[i];
						decimal? num3 = Recognition.<TryMultiply>g__ComputeConstantRight|31_0(num2, result);
						if (num3 == null)
						{
							goto IL_017B;
						}
						decimal valueOrDefault = num3.GetValueOrDefault();
						if (!(valueOrDefault <= -1m) && !(valueOrDefault > 1m))
						{
							goto IL_017B;
						}
						flag = true;
						IL_017E:
						if (flag)
						{
							list.Add(new ArithmeticPair(num2, num3.Value));
						}
						if (disjunctiveExamples != null && num2 == 0m && result == 0m)
						{
							List<ArithmeticPair> list2 = list;
							IEnumerable<IRow> keys = disjunctiveExamples.Keys;
							Func<IRow, bool> func;
							if ((func = <>9__3) == null)
							{
								func = (<>9__3 = (IRow disjunctiveInputRow) => !inputRow.Equals(disjunctiveInputRow));
							}
							IEnumerable<IRow> enumerable = keys.Where(func);
							Func<IRow, IEnumerable<decimal>> func2;
							if ((func2 = <>9__4) == null)
							{
								func2 = (<>9__4 = (IRow disjunctiveInputRow) => this.ArithmeticSources(disjunctiveInputRow).DistinctValues);
							}
							var enumerable2 = enumerable.SelectMany(func2, (IRow disjunctiveInputRow, decimal disjunctiveLeft) => new { disjunctiveInputRow, disjunctiveLeft });
							var func3;
							if ((func3 = <>9__6) == null)
							{
								func3 = (<>9__6 = <>h__TransparentIdentifier0 => disjunctiveExamples[<>h__TransparentIdentifier0.disjunctiveInputRow]);
							}
							list2.AddRange(from <>h__TransparentIdentifier2 in (from <>h__TransparentIdentifier1 in enumerable2.SelectMany(func3, (<>h__TransparentIdentifier0, decimal disjunctiveResult) => new { <>h__TransparentIdentifier0, disjunctiveResult })
									select new
									{
										<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
										disjunctiveRight = Recognition.<TryMultiply>g__ComputeConstantRight|31_0(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.disjunctiveLeft, <>h__TransparentIdentifier1.disjunctiveResult)
									}).Where(delegate(<>h__TransparentIdentifier2)
								{
									decimal? disjunctiveRight = <>h__TransparentIdentifier2.disjunctiveRight;
									if (disjunctiveRight != null)
									{
										decimal valueOrDefault2 = disjunctiveRight.GetValueOrDefault();
										if (valueOrDefault2 <= -1m || valueOrDefault2 > 1m)
										{
											return true;
										}
									}
									return false;
								})
								select new ArithmeticPair(0m, <>h__TransparentIdentifier2.disjunctiveRight.Value));
						}
						i++;
						continue;
						IL_017B:
						flag = false;
						goto IL_017E;
					}
				}
				pairs = list;
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag = this.CacheSet<IReadOnlyList<ArithmeticPair>>(array2, pairs).Any<ArithmeticPair>();
			}
			return flag;
		}

		// Token: 0x0600BCEE RID: 48366 RVA: 0x0028B3D4 File Offset: 0x002895D4
		public IReadOnlyList<decimal> FindDivideNumbers(IRow inputRow, string result)
		{
			object[] array = new object[] { "FindDivideNumbers", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindDivideNumbers", false, true) : null))
			{
				IReadOnlyList<FormattedNumberCacheItem> resultNumbers = this.ReverseFormattedNumber(result);
				List<decimal> list = (from sourcePair in this.ArithmeticSourcePairs(inputRow)
					where sourcePair.Divide != null
					let divide = sourcePair.Divide.Value
					where result == null || resultNumbers.Any((FormattedNumberCacheItem r) => r.Value == Math.Round(divide, r.Scale))
					select divide).ToList<decimal>();
				if (result != null && this._enableArithmeticConstants)
				{
					list.AddRange(from <>h__TransparentIdentifier2 in (from left in this.ArithmeticSources(inputRow).DistinctValues
							from resultNumber in resultNumbers
							let scale = (resultNumber.Scale <= 0) ? 0 : (resultNumber.Scale - 1)
							select new
							{
								<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
								right = ((resultNumber.Value == 0m) ? null : new decimal?(Math.Round(left / resultNumber.Value, scale)))
							}).Where(delegate(<>h__TransparentIdentifier2)
						{
							decimal? right = right;
							if (right != null)
							{
								decimal valueOrDefault = right.GetValueOrDefault();
								if (valueOrDefault < -1m || valueOrDefault > 1m)
								{
									return true;
								}
							}
							return false;
						})
						let predictedResult = Operators.Divide(left, right.Value)
						where predictedResult != null
						select predictedResult.Value);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<decimal>>(array, list.Distinct<decimal>().ToList<decimal>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCEF RID: 48367 RVA: 0x0028B660 File Offset: 0x00289860
		public bool TryDivide(IRow inputRow, decimal result, out IReadOnlyList<ArithmeticPair> pairs, Dictionary<IRow, IReadOnlyList<decimal>> disjunctiveExamples = null)
		{
			object[] array = new object[4];
			array[0] = "TryDivide";
			array[1] = inputRow;
			array[2] = result;
			int num = 3;
			Dictionary<IRow, IReadOnlyList<decimal>> disjunctiveExamples2 = disjunctiveExamples;
			array[num] = ((disjunctiveExamples2 != null) ? new int?(disjunctiveExamples2.KeyValueHashCode<decimal>()) : null);
			object[] array2 = array;
			if (this.CacheTryGetValue<IReadOnlyList<ArithmeticPair>>(array2, out pairs))
			{
				return pairs.Any<ArithmeticPair>();
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				pairs = this.CacheSetEmptyList<ArithmeticPair>(array2);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryDivide", false, true) : null))
			{
				List<ArithmeticPair> list = new List<ArithmeticPair>();
				list.AddRange(from pair in this.ArithmeticSourcePairs(inputRow)
					where Recognition.<TryDivide>g__ValidateNormalized|33_1(pair.Left.Value, pair.Right.Value, result)
					select new ArithmeticPair(pair.Left.Value, pair.Right.Value));
				if (this._enableArithmeticConstants)
				{
					decimal[] distinctValues = this.ArithmeticSources(inputRow).DistinctValues;
					int i = 0;
					Func<IRow, bool> <>9__4;
					Func<IRow, IEnumerable<decimal>> <>9__5;
					Func<<>f__AnonymousType97<IRow, decimal>, IEnumerable<decimal>> <>9__7;
					while (i < distinctValues.Length)
					{
						decimal num2 = distinctValues[i];
						decimal? num3 = Recognition.<TryDivide>g__ComputeConstantRight|33_0(num2, result);
						if (num3 == null)
						{
							goto IL_017B;
						}
						decimal valueOrDefault = num3.GetValueOrDefault();
						if (!(valueOrDefault < -1m) && !(valueOrDefault > 1m))
						{
							goto IL_017B;
						}
						flag = true;
						IL_017E:
						if (flag)
						{
							list.Add(new ArithmeticPair(num2, num3.Value));
						}
						if (disjunctiveExamples != null && num2 == 0m && result == 0m)
						{
							List<ArithmeticPair> list2 = list;
							IEnumerable<IRow> keys = disjunctiveExamples.Keys;
							Func<IRow, bool> func;
							if ((func = <>9__4) == null)
							{
								func = (<>9__4 = (IRow disjunctiveInputRow) => !inputRow.Equals(disjunctiveInputRow));
							}
							IEnumerable<IRow> enumerable = keys.Where(func);
							Func<IRow, IEnumerable<decimal>> func2;
							if ((func2 = <>9__5) == null)
							{
								func2 = (<>9__5 = (IRow disjunctiveInputRow) => this.ArithmeticSources(disjunctiveInputRow).DistinctValues);
							}
							var enumerable2 = enumerable.SelectMany(func2, (IRow disjunctiveInputRow, decimal disjunctiveLeft) => new { disjunctiveInputRow, disjunctiveLeft });
							var func3;
							if ((func3 = <>9__7) == null)
							{
								func3 = (<>9__7 = <>h__TransparentIdentifier0 => disjunctiveExamples[<>h__TransparentIdentifier0.disjunctiveInputRow]);
							}
							list2.AddRange(from <>h__TransparentIdentifier2 in (from <>h__TransparentIdentifier1 in enumerable2.SelectMany(func3, (<>h__TransparentIdentifier0, decimal disjunctiveResult) => new { <>h__TransparentIdentifier0, disjunctiveResult })
									select new
									{
										<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
										disjunctiveRight = Recognition.<TryDivide>g__ComputeConstantRight|33_0(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.disjunctiveLeft, <>h__TransparentIdentifier1.disjunctiveResult)
									}).Where(delegate(<>h__TransparentIdentifier2)
								{
									decimal? disjunctiveRight = <>h__TransparentIdentifier2.disjunctiveRight;
									if (disjunctiveRight != null)
									{
										decimal valueOrDefault2 = disjunctiveRight.GetValueOrDefault();
										if (valueOrDefault2 < -1m || valueOrDefault2 > 1m)
										{
											return true;
										}
									}
									return false;
								})
								where Recognition.<TryDivide>g__ValidateNormalized|33_1(<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.disjunctiveLeft, <>h__TransparentIdentifier2.disjunctiveRight.Value, <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.disjunctiveResult)
								select new ArithmeticPair(0m, <>h__TransparentIdentifier2.disjunctiveRight.Value));
						}
						i++;
						continue;
						IL_017B:
						flag = false;
						goto IL_017E;
					}
				}
				pairs = list;
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag = this.CacheSet<List<ArithmeticPair>>(array2, list).Any<ArithmeticPair>();
			}
			return flag;
		}

		// Token: 0x0600BCF0 RID: 48368 RVA: 0x0028B9DC File Offset: 0x00289BDC
		public IReadOnlyList<decimal> FindSumNumbers(IRow inputRow)
		{
			object[] array = new object[] { "FindSumNumbers", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindSumNumbers", false, true) : null))
			{
				readOnlyList = (from set in this.ArithmeticSourceSets(inputRow)
					where set.Sum != null
					select set.Sum.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCF1 RID: 48369 RVA: 0x0028BAC4 File Offset: 0x00289CC4
		public bool TrySum(IRow inputRow, decimal result, out ArithmeticSetList sets)
		{
			Func<ArithmeticSet, bool> <>9__1;
			sets = this.CacheGetOrCompute<ArithmeticSetList>(new object[] { "TrySum", inputRow, result }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				ArithmeticSetList arithmeticSetList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryProduct", false, true) : null))
				{
					IEnumerable<ArithmeticSet> enumerable = this.ArithmeticSourceSets(inputRow);
					Func<ArithmeticSet, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(ArithmeticSet set)
						{
							if (this.CanUseArithmetic(inputRow))
							{
								decimal result2 = result;
								decimal? sum = set.Sum;
								return (result2 == sum.GetValueOrDefault()) & (sum != null);
							}
							return false;
						});
					}
					ArithmeticSetList arithmeticSetList = new ArithmeticSetList(enumerable.Where(func));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					arithmeticSetList2 = arithmeticSetList;
				}
				return arithmeticSetList2;
			});
			return sets.Any<ArithmeticSet>();
		}

		// Token: 0x0600BCF2 RID: 48370 RVA: 0x0028BB2C File Offset: 0x00289D2C
		public IReadOnlyList<decimal> FindProductNumbers(IRow inputRow)
		{
			object[] array = new object[] { "FindProductNumbers", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindProductNumbers", false, true) : null))
			{
				readOnlyList = (from set in this.ArithmeticSourceSets(inputRow).Where(delegate(ArithmeticSet set)
					{
						if (set.Product != null)
						{
							return set.DistinctValues.All((decimal i) => i != 0m);
						}
						return false;
					})
					select set.Product.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCF3 RID: 48371 RVA: 0x0028BC14 File Offset: 0x00289E14
		public bool TryProduct(IRow inputRow, decimal result, out ArithmeticSetList sets)
		{
			Func<ArithmeticSet, bool> <>9__1;
			sets = this.CacheGetOrCompute<ArithmeticSetList>(new object[] { "TryProduct", inputRow, result }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				ArithmeticSetList arithmeticSetList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryProduct", false, true) : null))
				{
					IEnumerable<ArithmeticSet> enumerable = this.ArithmeticSourceSets(inputRow);
					Func<ArithmeticSet, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(ArithmeticSet set)
						{
							if (this.CanUseArithmetic(inputRow))
							{
								if (set.DistinctValues.All((decimal i) => i != 0m))
								{
									decimal result2 = result;
									decimal? product = set.Product;
									return (result2 == product.GetValueOrDefault()) & (product != null);
								}
							}
							return false;
						});
					}
					ArithmeticSetList arithmeticSetList = new ArithmeticSetList(enumerable.Where(func));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					arithmeticSetList2 = arithmeticSetList;
				}
				return arithmeticSetList2;
			});
			return sets.Any<ArithmeticSet>();
		}

		// Token: 0x0600BCF4 RID: 48372 RVA: 0x0028BC7C File Offset: 0x00289E7C
		public IReadOnlyList<decimal> FindAverageNumbers(IRow inputRow)
		{
			object[] array = new object[] { "FindAverageNumbers", inputRow };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (!this.CanUseArithmetic(inputRow))
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindAverageNumbers", false, true) : null))
			{
				readOnlyList = (from set in this.ArithmeticSourceSets(inputRow)
					where set.Average != null && set.DistinctValues.Length > 1
					select set.Average.Value).Distinct<decimal>().ToReadOnlyList<decimal>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BCF5 RID: 48373 RVA: 0x0028BD64 File Offset: 0x00289F64
		public bool TryAverage(IRow inputRow, decimal result, out ArithmeticSetList sets)
		{
			Func<ArithmeticSet, bool> <>9__2;
			sets = this.CacheGetOrCompute<ArithmeticSetList>(new object[] { "TryAverage", inputRow, result }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				ArithmeticSetList arithmeticSetList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryAverage", false, true) : null))
				{
					IEnumerable<ArithmeticSet> enumerable = from set in this.ArithmeticSourceSets(inputRow)
						where set.Average != null
						select set;
					Func<ArithmeticSet, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (ArithmeticSet set) => this.CanUseArithmetic(inputRow) && set.DistinctValues.Length > 1 && result.EqualsWithTruncate(set.Average.Value));
					}
					ArithmeticSetList arithmeticSetList = new ArithmeticSetList(enumerable.Where(func));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					arithmeticSetList2 = arithmeticSetList;
				}
				return arithmeticSetList2;
			});
			return sets.Any<ArithmeticSet>();
		}

		// Token: 0x0600BCF6 RID: 48374 RVA: 0x0028BDCC File Offset: 0x00289FCC
		public bool HasAllClassifications(string subject, StringClassification classifications)
		{
			return (this.Classify(subject, classifications) & classifications) == classifications;
		}

		// Token: 0x0600BCF7 RID: 48375 RVA: 0x0028BDDB File Offset: 0x00289FDB
		public bool HasClassification(string subject, StringClassification classifications)
		{
			return (this.Classify(subject, classifications) & classifications) > StringClassification.None;
		}

		// Token: 0x0600BCF8 RID: 48376 RVA: 0x0028BDEC File Offset: 0x00289FEC
		public bool HasOutputClassification(IRow row, StringClassification classifications)
		{
			string text = this.FindOutput(row) as string;
			return text != null && this.HasClassification(text, classifications);
		}

		// Token: 0x0600BCF9 RID: 48377 RVA: 0x0028BE14 File Offset: 0x0028A014
		private StringClassification Classify(string subject, StringClassification checkClassifications)
		{
			object[] array = new object[] { "Classify", subject, checkClassifications };
			StringClassification stringClassification;
			if (this.CacheTryGetValue<StringClassification>(array, out stringClassification))
			{
				return stringClassification;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			StringClassification stringClassification3;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "Classify", false, true) : null))
			{
				StringClassification stringClassification2 = StringClassification.None;
				if (checkClassifications.HasFlag(StringClassification.FormattedNumber) && this.IsFormattedNumber(subject))
				{
					stringClassification2 |= StringClassification.FormattedNumber;
				}
				if (checkClassifications.HasFlag(StringClassification.FormattedDateTime) && this.IsFormattedDateTime(subject, false))
				{
					stringClassification2 |= StringClassification.FormattedDateTime;
				}
				if (checkClassifications.HasFlag(StringClassification.FormattedDateTimeNoNumbers) && this.IsFormattedDateTime(subject, true))
				{
					stringClassification2 |= StringClassification.FormattedDateTimeNoNumbers;
				}
				if (checkClassifications.HasFlag(StringClassification.PhoneNumber) && this.IsPhoneNumber(subject))
				{
					stringClassification2 |= StringClassification.PhoneNumber;
				}
				if (checkClassifications.HasFlag(StringClassification.SocialSecurityNumber) && this.IsSocialSecurityNumber(subject))
				{
					stringClassification2 |= StringClassification.SocialSecurityNumber;
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				stringClassification3 = this.CacheSet<StringClassification>(array, stringClassification2);
			}
			return stringClassification3;
		}

		// Token: 0x0600BCFA RID: 48378 RVA: 0x0028BF4C File Offset: 0x0028A14C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedDateTime(string subject, bool excludeAllNumbers = false)
		{
			IReadOnlyList<DateTimeDescriptor> readOnlyList;
			return this.IsFormattedDateTime(subject, excludeAllNumbers, out readOnlyList);
		}

		// Token: 0x0600BCFB RID: 48379 RVA: 0x0028BF63 File Offset: 0x0028A163
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedDateTime(string subject, out IReadOnlyList<DateTimeDescriptor> descriptors)
		{
			return this.IsFormattedDateTime(subject, false, out descriptors);
		}

		// Token: 0x0600BCFC RID: 48380 RVA: 0x0028BF70 File Offset: 0x0028A170
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedDateTime(string subject, bool excludeAllNumbers, out IReadOnlyList<DateTimeDescriptor> descriptors)
		{
			object[] array = new object[] { "IsFormattedDateTime", subject, excludeAllNumbers };
			if (this.CacheTryGetValue<IReadOnlyList<DateTimeDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<DateTimeDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "IsFormattedDateTime", true, true) : null))
			{
				if (string.IsNullOrEmpty(subject))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<DateTimeDescriptor>(array).Any<DateTimeDescriptor>();
				}
				else
				{
					char c3 = subject[0];
					char c2 = subject[subject.Length - 1];
					if ((!this.DateTimeAllowedDelimiters.Contains(c3) && c3.IsDelimiter()) || (!this.DateTimeAllowedDelimiters.Contains(c2) && c2.IsDelimiter()))
					{
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						flag = this.CacheSetEmptyList<DateTimeDescriptor>(array).Any<DateTimeDescriptor>();
					}
					else
					{
						bool subjectAllNumbers = subject.All((char c) => c.ToUnicodeCategory() == UnicodeCategory.DecimalDigitNumber);
						string subjectDelimters = subject.Where((char c) => c.IsDelimiter() && !this.DateTimeAllowedDelimiters.Contains(c)).ToJoinString();
						char subjectFirstLowerChar = char.ToLower(c3);
						char subjectLastLowerChar = char.ToLower(c2);
						IReadOnlyList<DateTimeDescriptor> readOnlyList = (from descriptor in this.DateTimeFormattedDescriptors
							where !excludeAllNumbers || !descriptor.OutputAllNumbers
							where descriptor.FormattedMinLength <= subject.Length && subject.Length <= descriptor.FormattedMaxLength
							where descriptor.FormattedLowerFirstChars.Contains(subjectFirstLowerChar) && descriptor.FormattedLowerLastChars.Contains(subjectLastLowerChar)
							where (subjectAllNumbers && descriptor.OutputAllNumbers) || (subjectDelimters.None<char>() && descriptor.MaskDelimiters.None<char>()) || (subjectDelimters.Any<char>() && descriptor.MaskDelimiters.Any<char>() && subjectDelimters.Length == descriptor.MaskDelimiters.Length && descriptor.MaskDelimiters == subjectDelimters)
							select descriptor).ToReadOnlyList<DateTimeDescriptor>();
						descriptors = readOnlyList.Where((DateTimeDescriptor descriptor) => descriptor.Regex.IsFullMatch(subject)).ToReadOnlyList<DateTimeDescriptor>();
						LearnDebugTrace debugTrace2 = this._debugTrace;
						if (debugTrace2 != null)
						{
							debugTrace2.HitEvent("Descriptors", "IsFormattedDateTime", descriptors.Count);
						}
						LearnDebugTrace debugTrace3 = this._debugTrace;
						if (debugTrace3 != null)
						{
							debugTrace3.MissEvent("Descriptors", "IsFormattedDateTime", readOnlyList.Count - descriptors.Count);
						}
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						flag = this.CacheSet<IReadOnlyList<DateTimeDescriptor>>(array, descriptors).Any<DateTimeDescriptor>();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600BCFD RID: 48381 RVA: 0x0028C1F0 File Offset: 0x0028A3F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedNumber(string subject)
		{
			return this.IsFormattedNumber(subject.AsSpan());
		}

		// Token: 0x0600BCFE RID: 48382 RVA: 0x0028C1FE File Offset: 0x0028A3FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedNumber(string subject, out IReadOnlyList<FormatNumberDescriptor> descriptors)
		{
			return this.IsFormattedNumber(subject.AsSpan(), out descriptors);
		}

		// Token: 0x0600BCFF RID: 48383 RVA: 0x0028C210 File Offset: 0x0028A410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFormattedNumber(ReadOnlySpan<char> subject)
		{
			IReadOnlyList<FormatNumberDescriptor> readOnlyList;
			return this.IsFormattedNumber(subject, out readOnlyList);
		}

		// Token: 0x0600BD00 RID: 48384 RVA: 0x0028C228 File Offset: 0x0028A428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe bool IsFormattedNumber(ReadOnlySpan<char> subject, out IReadOnlyList<FormatNumberDescriptor> descriptors)
		{
			object[] array = new object[]
			{
				"IsFormattedNumber",
				subject.ToString()
			};
			if (this.CacheTryGetValue<IReadOnlyList<FormatNumberDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<FormatNumberDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "IsFormattedNumber", true, true) : null))
			{
				if (subject.None<char>())
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<FormatNumberDescriptor>(array).Any<FormatNumberDescriptor>();
				}
				else
				{
					char c3 = (char)(*subject[0]);
					char c2 = (char)(*subject[subject.Length - 1]);
					if (subject.Any((char c) => !this.NumberChars.Contains(c)) || this.NumberGroupSeparatorChars.Contains(c3) || this.NumberDecimalSeparatorChars.Contains(c3) || this.NumberGroupSeparatorChars.Contains(c2) || this.NumberDecimalSeparatorChars.Contains(c2))
					{
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						flag = this.CacheSetEmptyList<FormatNumberDescriptor>(array).Any<FormatNumberDescriptor>();
					}
					else
					{
						bool requireGroup = subject.Any((char c) => this.NumberGroupSeparatorChars.Contains(c));
						bool requireDecimal = subject.Any((char c) => this.NumberDecimalSeparatorChars.Contains(c));
						int num = subject.LastIndexOfAny(this.NumberDecimalSeparatorCharsArray);
						bool flag2 = num >= 0;
						int maxLeadingDigits = subject.Take(flag2 ? num : subject.Length).Count((char c) => this.NumberDigitChars.Contains(c));
						maxLeadingDigits = Math.Min(maxLeadingDigits, this._numberFormatMaxLeadingDigits);
						int maxTrailingDigits = subject.Skip(flag2 ? (num + 1) : 0).Count<char>();
						bool allowGroup = requireGroup || maxLeadingDigits <= this.NumberMinGroupSize;
						bool requirePercent = subject.Any((char c) => this.NumberPercentSymbolChars.Contains(c));
						bool flag3 = this.NumberCurrencySymbolChars.Contains(c3);
						bool flag4 = this.NumberCurrencySymbolChars.Contains(c2);
						bool requireCurrency = flag3 || flag4;
						descriptors = (from descriptor in this.NumberFormatDescriptors
							where descriptor.LeadingDigits <= maxLeadingDigits && descriptor.TrailingDigits <= maxTrailingDigits
							where allowGroup || (!requireGroup && !descriptor.IncludeGroupSeparator) || (requireGroup && descriptor.IncludeGroupSeparator)
							where (!requireDecimal && !descriptor.IncludeDecimalSeparator) || (requireDecimal && descriptor.IncludeDecimalSeparator)
							where (!requireCurrency && !descriptor.IncludeCurrencySymbol) || (requireCurrency && descriptor.IncludeCurrencySymbol)
							where (!requirePercent && !descriptor.IncludePercentSymbol) || (requirePercent && descriptor.IncludePercentSymbol)
							select descriptor).ToReadOnlyList<FormatNumberDescriptor>();
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						LearnDebugTrace debugTrace2 = this._debugTrace;
						if (debugTrace2 != null)
						{
							debugTrace2.HitEvent("Descriptors", "IsFormattedNumber", descriptors.Count);
						}
						flag = this.CacheSet<IReadOnlyList<FormatNumberDescriptor>>(array, descriptors).Any<FormatNumberDescriptor>();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600BD01 RID: 48385 RVA: 0x0028C514 File Offset: 0x0028A714
		private bool IsPhoneNumber(string subject)
		{
			object[] array = new object[] { "IsPhoneNumber", subject };
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "IsPhoneNumber", false, true) : null))
			{
				if (!subject.All(delegate(char c)
				{
					bool flag4 = c.ToUnicodeCategory() == UnicodeCategory.DecimalDigitNumber;
					if (!flag4)
					{
						bool flag5;
						switch (c)
						{
						case '(':
						case ')':
						case '-':
						case '.':
							flag5 = true;
							goto IL_003A;
						}
						flag5 = false;
						IL_003A:
						flag4 = flag5;
					}
					return flag4;
				}))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag2 = this.CacheSet<bool>(array, false);
				}
				else
				{
					if (Recognition._classifyPhoneNumberRegex == null)
					{
						Recognition._classifyPhoneNumberRegex = "(?:(?:\\+?\\p{N}{1,3}[\\s.-])?\\(?\\p{N}{3}?\\)?[\\s.-]\\p{N}{3}[\\s.-]\\p{N}{4})|(?:\\+?\\p{N}{2}\\s?\\p{N}{2}\\s?\\p{N}{8})".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
					}
					bool flag3 = this.CacheSet<bool>(array, Recognition._classifyPhoneNumberRegex.IsFullMatch(subject));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag2 = flag3;
				}
			}
			return flag2;
		}

		// Token: 0x0600BD02 RID: 48386 RVA: 0x0028C5F0 File Offset: 0x0028A7F0
		private bool IsSocialSecurityNumber(string subject)
		{
			object[] array = new object[] { "IsSocialSecurityNumber", subject };
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag3;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "IsSocialSecurityNumber", false, true) : null))
			{
				if (subject.Length == 11)
				{
					if (subject.All((char c) => c.ToUnicodeCategory() == UnicodeCategory.DecimalDigitNumber || c == '-'))
					{
						if (Recognition._classifySocialSecurityNumberRegex == null)
						{
							Recognition._classifySocialSecurityNumberRegex = "\\p{N}{3}-\\p{N}{2}-\\p{N}{4}".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
						}
						bool flag2 = this.CacheSet<bool>(array, Recognition._classifySocialSecurityNumberRegex.IsFullMatch(subject));
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						return flag2;
					}
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag3 = this.CacheSet<bool>(array, false);
			}
			return flag3;
		}

		// Token: 0x0600BD03 RID: 48387 RVA: 0x0028C6D8 File Offset: 0x0028A8D8
		public bool CanConcat(string result)
		{
			object[] array = new object[] { "CanConcat", result };
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			int? num = ((result != null) ? new int?(result.Length) : null);
			bool flag2;
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (valueOrDefault > 1 && valueOrDefault <= 300 && !this.Examples.All((Example<IRow, object> e) => this.Contains(e.Input, e.Output as string, false)))
				{
					flag2 = !result.AllDelimiters();
					goto IL_007D;
				}
			}
			flag2 = false;
			IL_007D:
			flag = flag2;
			return this.CacheSet<bool>(array, flag);
		}

		// Token: 0x0600BD04 RID: 48388 RVA: 0x0028C76C File Offset: 0x0028A96C
		public bool CanShiftCase()
		{
			object[] array = new object[] { "CanShiftCase" };
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			IEnumerable<string> enumerable = from output in this.Examples.StringOutputs<IRow, object>()
				where output.Any((char c) => c.IsLetter())
				select output;
			return this.CacheSet<bool>(array, enumerable.Any<string>());
		}

		// Token: 0x0600BD05 RID: 48389 RVA: 0x0028C7D4 File Offset: 0x0028A9D4
		public IReadOnlyList<string> ToShiftCase(IRow inputRow, string result)
		{
			if (!this.CanShiftCase())
			{
				return null;
			}
			object[] array = new object[] { "ToShiftCase", inputRow, result };
			IReadOnlyList<string> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<string>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<string> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ToShiftCase", true, true) : null))
			{
				IEnumerable<string> enumerable = (from input in this.InputStrings(inputRow, null).DistinctValues
					from inputSubIndex in input.AllIndexesOf(result, StringComparison.OrdinalIgnoreCase)
					select input.Substring(inputSubIndex, result.Length)).ToList<string>();
				if (enumerable.Any<string>())
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<List<string>>(array, enumerable.ToList<string>());
				}
				else
				{
					enumerable = from sub in result.AllSubstringsDetail()
						let subIndex = sub.Item1
						let subValue = sub.Item2
						from input in this.InputStrings(inputRow, null).DistinctValues
						from inputIndex in input.AllIndexesOf(subValue, StringComparison.OrdinalIgnoreCase)
						let inputSlice = input.Slice(new int?(inputIndex), new int?(inputIndex + subValue.Length), 1)
						select result.Splice(subIndex, inputSlice);
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<List<string>>(array, enumerable.ToList<string>());
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD06 RID: 48390 RVA: 0x0028C9F0 File Offset: 0x0028ABF0
		public Recognition(IEnumerable<Example<IRow, object>> examples, LearnOptions options, LearnDebugTrace debugTrace, CancellationToken cancellationToken)
			: this(examples, options.DataCultures, options.ColumnNamePriority, options.EnableMatchNames, options.EnableMatchUnicode, options.FromNumbersColumnLimit, options.EnableNegativePosition, options.EnableFromNumberStr, options.EnableRoundNumber, options.EnableRoundDateTime, options.EnableDateTimePart, options.EnableTimePart, options.EnableArithmetic, options.ForwardFillMaxScale, options.NumberSources, options.DateTimeSources, options.EnableArithmeticConstants, options.EnableFromNumberCoalesced, options.NumberFormatMaxLeadingDigits, debugTrace, cancellationToken)
		{
		}

		// Token: 0x0600BD07 RID: 48391 RVA: 0x0028CA74 File Offset: 0x0028AC74
		public Recognition(IEnumerable<Example<IRow, object>> examples, IEnumerable<CultureInfo> dataCultures = null, IEnumerable<string> columnNamePriority = null, MatchName enableMatchNames = MatchName.All, bool enableMatchUnicode = true, int fromNumbersColumnLimit = 16, bool enableNegativePosition = true, bool enableFromNumberStr = false, bool enableRoundNumber = true, bool enableRoundDateTime = true, bool enableDateTimePart = true, bool enableTimePart = true, bool enableArithmetic = true, int forwardFillMaxScale = 1, NumberSourceKind numberSources = NumberSourceKind.All, DateTimeSourceKind dateTimeSources = DateTimeSourceKind.All, bool enableArithmeticConstants = true, bool enableFromNumberCoalesceZero = false, int numberFormatMaxLeadingDigits = 1, LearnDebugTrace debugTrace = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.Examples = examples.ToReadOnlyList<Example<IRow, object>>();
			IReadOnlyList<CultureInfo> readOnlyList;
			if ((readOnlyList = ((dataCultures != null) ? dataCultures.Distinct<CultureInfo>().ToReadOnlyList<CultureInfo>() : null)) == null)
			{
				(readOnlyList = new CultureInfo[1])[0] = new CultureInfo("en-US");
			}
			this.DataCultures = readOnlyList;
			this._columnNamePriority = columnNamePriority.ToReadOnlyList<string>();
			this._enableMatchNames = enableMatchNames;
			this._enableMatchUnicode = enableMatchUnicode;
			this._enableFromNumberStr = enableFromNumberStr;
			this._enableRoundNumber = enableRoundNumber;
			this._enableRoundDateTime = enableRoundDateTime;
			this._enableDateTimePart = enableDateTimePart;
			this._enableTimePart = enableTimePart;
			this._enableArithmetic = enableArithmetic;
			this._cancellationToken = cancellationToken;
			this._fromNumbersColumnLimit = fromNumbersColumnLimit;
			this._enableNegativePosition = enableNegativePosition;
			this._forwardFillMaxScale = forwardFillMaxScale;
			this._enableArithmeticConstants = enableArithmeticConstants;
			this._enableFromNumberCoalesceZero = enableFromNumberCoalesceZero;
			this._numberSourceKinds |= numberSources;
			this._dateTimeSourceKinds |= dateTimeSources;
			this._numberFormatMaxLeadingDigits = numberFormatMaxLeadingDigits;
			this._debugTrace = debugTrace;
			IReadOnlyCollection<ColumnDetail> readOnlyCollection = this.Examples.InputColumnDetails(null).ToReadOnlyList<ColumnDetail>();
			this._allZeroColumns = Recognition.LoadAllZeroColumns(readOnlyCollection, enableFromNumberCoalesceZero);
			if (this.Examples.Any<Example<IRow, object>>())
			{
				ColumnDetail columnDetail = this.Examples.OutputColumnDetail();
				bool flag;
				if (columnDetail.AllNumber)
				{
					flag = columnDetail.NumberValues.All((decimal v) => v == 0m);
				}
				else
				{
					flag = false;
				}
				this._outputAllZeros = flag;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x0600BD08 RID: 48392 RVA: 0x0028CBF6 File Offset: 0x0028ADF6
		public IReadOnlyList<CultureInfo> DataCultures { get; }

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x0600BD09 RID: 48393 RVA: 0x0028CBFE File Offset: 0x0028ADFE
		public IReadOnlyList<Example<IRow, object>> Examples { get; }

		// Token: 0x0600BD0A RID: 48394 RVA: 0x0028CC08 File Offset: 0x0028AE08
		public object FindOutput(IRow inputRow)
		{
			Func<Example<IRow, object>, bool> <>9__1;
			return this.CacheGetOrCompute<object>(new object[] { "FindOutput", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				object obj2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "InputStrings", false, true) : null))
				{
					IEnumerable<Example<IRow, object>> examples = this.Examples;
					Func<Example<IRow, object>, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (Example<IRow, object> e) => e.Input.Equals(inputRow));
					}
					Example<IRow, object> example = examples.FirstOrDefault(func);
					object obj = ((example != null) ? example.Output : null);
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					obj2 = obj;
				}
				return obj2;
			});
		}

		// Token: 0x0600BD0B RID: 48395 RVA: 0x0028CC54 File Offset: 0x0028AE54
		public StringInputList InputStrings(IRow inputRow, IEnumerable<string> columnNames = null)
		{
			Func<string, <>f__AnonymousType74<string, string>> <>9__1;
			return this.CacheGetOrCompute<StringInputList>(new object[] { "InputStrings", inputRow, columnNames }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				StringInputList stringInputList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "InputStrings", false, true) : null))
				{
					IEnumerable<string> enumerable = columnNames ?? inputRow.ColumnNames;
					var func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string columnName) => new
						{
							columnName = columnName,
							value = (Operators.FromStr(inputRow, columnName) ?? (this._enableFromNumberStr ? Operators.FromNumberStr(inputRow, columnName) : null))
						});
					}
					StringInputList stringInputList = new StringInputList(from <>h__TransparentIdentifier0 in enumerable.Select(func)
						where <>h__TransparentIdentifier0.value != null
						select new StringInput(<>h__TransparentIdentifier0.columnName, <>h__TransparentIdentifier0.value));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					stringInputList2 = stringInputList;
				}
				return stringInputList2;
			});
		}

		// Token: 0x0600BD0C RID: 48396 RVA: 0x0028CCAE File Offset: 0x0028AEAE
		public HashSet<string> OutputStrings()
		{
			return this.CacheGetOrCompute<HashSet<string>>(new object[] { "OutputStrings" }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				HashSet<string> hashSet2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "InputStrings", false, true) : null))
				{
					HashSet<string> hashSet = this.Examples.StringOutputs<IRow, object>().ConvertToHashSet<string>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					hashSet2 = hashSet;
				}
				return hashSet2;
			});
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x0600BD0D RID: 48397 RVA: 0x0028CCD0 File Offset: 0x0028AED0
		private ConcurrentLruCache<object[], object> Cache { get; } = new ConcurrentLruCache<object[], object>(131072, new ArrayEquality<object>(), null, null);

		// Token: 0x0600BD0E RID: 48398 RVA: 0x0028CCD8 File Offset: 0x0028AED8
		public TValue CacheGetOrCompute<TValue>(object[] key, Func<TValue> fetch)
		{
			TValue tvalue;
			if (!this.CacheTryGetValue<TValue>(key, out tvalue))
			{
				return this.CacheSet<TValue>(key, fetch());
			}
			return tvalue;
		}

		// Token: 0x0600BD0F RID: 48399 RVA: 0x0028CD00 File Offset: 0x0028AF00
		public TValue CacheSet<TValue>(object[] key, TValue value)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			return (TValue)((object)this.Cache.LookupOrCompute(key, (object[] _) => value));
		}

		// Token: 0x0600BD10 RID: 48400 RVA: 0x0028CD45 File Offset: 0x0028AF45
		public IReadOnlyList<TValue> CacheSetEmptyList<TValue>(object[] key)
		{
			return this.CacheSet<IReadOnlyList<TValue>>(key, new TValue[0]);
		}

		// Token: 0x0600BD11 RID: 48401 RVA: 0x0028CD54 File Offset: 0x0028AF54
		public bool CacheTryGetValue<TValue>(object[] key, out TValue value)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			object obj;
			bool flag = this.Cache.Lookup(key, out obj);
			value = (flag ? ((TValue)((object)obj)) : default(TValue));
			return flag;
		}

		// Token: 0x0600BD12 RID: 48402 RVA: 0x0028CD9C File Offset: 0x0028AF9C
		private static HashSet<string> LoadAllZeroColumns(IEnumerable<ColumnDetail> inputColumnDetails, bool enableFromNumberCoalesceZero)
		{
			return (from detail in inputColumnDetails
				where detail.Values.All(delegate(object v)
				{
					bool flag = v.IsNumeric() && Convert.ToDecimal(v) == 0m;
					if (!flag)
					{
						bool flag2 = enableFromNumberCoalesceZero;
						if (flag2)
						{
							bool flag3;
							if (v != null)
							{
								string text = v as string;
								if (text == null || !(text == ""))
								{
									flag3 = false;
									goto IL_0049;
								}
							}
							flag3 = true;
							IL_0049:
							flag2 = flag3;
						}
						flag = flag2;
					}
					return flag;
				})
				select detail.Name).ConvertToHashSet<string>();
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x0600BD13 RID: 48403 RVA: 0x0028CDF4 File Offset: 0x0028AFF4
		public HashSet<char> DateTimeAllowedDelimiters
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._dateTimeAllowedDelimiters) == null)
				{
					hashSet = (this._dateTimeAllowedDelimiters = this.DataCultures.SelectMany((CultureInfo d) => d.DateTimeFormat.AbbreviatedDayNames).Concat(this.DataCultures.SelectMany((CultureInfo d) => d.DateTimeFormat.AbbreviatedMonthNames)).SelectMany((string c) => c)
						.ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x0600BD14 RID: 48404 RVA: 0x0028CE9C File Offset: 0x0028B09C
		public HashSet<char> DateTimeChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._dateTimeChars) == null)
				{
					hashSet = (this._dateTimeChars = this.LoadDateTimeChars().ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x0600BD15 RID: 48405 RVA: 0x0028CEC8 File Offset: 0x0028B0C8
		public IReadOnlyList<DateTimeDescriptor> DateTimeFormattedDescriptors
		{
			get
			{
				IReadOnlyList<DateTimeDescriptor> readOnlyList;
				if ((readOnlyList = this._dateTimeFormattedDescriptors) == null)
				{
					readOnlyList = (this._dateTimeFormattedDescriptors = this.LoadDateTimeFormattedDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x0600BD16 RID: 48406 RVA: 0x0028CEF0 File Offset: 0x0028B0F0
		public IReadOnlyList<DateTimeDescriptor> DateTimeParseDescriptors
		{
			get
			{
				IReadOnlyList<DateTimeDescriptor> readOnlyList;
				if ((readOnlyList = this._dateTimeParseDescriptors) == null)
				{
					readOnlyList = (this._dateTimeParseDescriptors = this.LoadDateTimeParseDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600BD17 RID: 48407 RVA: 0x0028CF16 File Offset: 0x0028B116
		public DateTimeSourceList DateTimes(IRow inputRow)
		{
			return this.DateTimes(inputRow, this._dateTimeSourceKinds);
		}

		// Token: 0x0600BD18 RID: 48408 RVA: 0x0028CF28 File Offset: 0x0028B128
		public DateTimeSourceList DateTimes(IRow inputRow, DateTimeSourceKind kind)
		{
			return this.CacheGetOrCompute<DateTimeSourceList>(new object[] { "DateTimes", inputRow, kind }, delegate
			{
				IEnumerable<DateTimeSource> enumerable = Utils.Empty<DateTimeSource>();
				LearnDebugTrace debugTrace = this._debugTrace;
				DateTimeSourceList dateTimeSourceList;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "DateTimes", false, true) : null))
				{
					if (kind.HasFlag(DateTimeSourceKind.Input))
					{
						enumerable = enumerable.Union(this.DateTimeInputs(inputRow));
					}
					if (kind.HasFlag(DateTimeSourceKind.Parsed))
					{
						enumerable = enumerable.Union(this.DateTimeParsed(inputRow, kind.HasFlag(DateTimeSourceKind.ParsedPartial)));
					}
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					dateTimeSourceList = new DateTimeSourceList(enumerable);
				}
				return dateTimeSourceList;
			});
		}

		// Token: 0x0600BD19 RID: 48409 RVA: 0x0028CF88 File Offset: 0x0028B188
		public IReadOnlyList<Time> Times(IRow inputRow)
		{
			object[] array = new object[] { "Times", inputRow };
			IReadOnlyList<Time> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<Time>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			IEnumerable<string> columnNames = inputRow.ColumnNames;
			readOnlyList = (((columnNames != null) ? columnNames.Collect((string columnName) => Operators.FromTime(inputRow, columnName)) : null) ?? new Time[0]).ToList<Time>();
			return this.CacheSet<IReadOnlyList<Time>>(array, readOnlyList);
		}

		// Token: 0x0600BD1A RID: 48410 RVA: 0x0028D004 File Offset: 0x0028B204
		private DateTimeSourceList DateTimeInputs(IRow inputRow)
		{
			Func<string, <>f__AnonymousType106<string, DateTime?>> <>9__1;
			return this.CacheGetOrCompute<DateTimeSourceList>(new object[] { "DateTimeInputs", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				DateTimeSourceList dateTimeSourceList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "DateTimeInputs", false, true) : null))
				{
					IEnumerable<string> enumerable = inputRow.ValidColumnNames();
					var func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string columnName) => new
						{
							columnName = columnName,
							date = Operators.FromDateTime(inputRow, columnName)
						});
					}
					DateTimeSourceList dateTimeSourceList = new DateTimeSourceList(from <>h__TransparentIdentifier0 in enumerable.Select(func)
						where <>h__TransparentIdentifier0.date != null
						select new DateTimeSource(<>h__TransparentIdentifier0.columnName, <>h__TransparentIdentifier0.date.Value, DateTimeSourceKind.Input));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					dateTimeSourceList2 = dateTimeSourceList;
				}
				return dateTimeSourceList2;
			});
		}

		// Token: 0x0600BD1B RID: 48411 RVA: 0x0028D050 File Offset: 0x0028B250
		private DateTimeSourceList DateTimeParsed(IRow inputRow, bool allowPartial = true)
		{
			Func<StringInput, IEnumerable<FindDateTimeCacheItem>> <>9__1;
			return this.CacheGetOrCompute<DateTimeSourceList>(new object[] { "DateTimeParsed", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				DateTimeSourceList dateTimeSourceList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "DateTimeParsed", false, true) : null))
				{
					IEnumerable<StringInput> enumerable = this.InputStrings(inputRow, null);
					Func<StringInput, IEnumerable<FindDateTimeCacheItem>> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (StringInput input) => this.FindParseableDateTimes(input.Value, allowPartial));
					}
					DateTimeSourceList dateTimeSourceList = new DateTimeSourceList(from <>h__TransparentIdentifier0 in enumerable.SelectMany(func, (StringInput input, FindDateTimeCacheItem findResult) => new { input, findResult })
						from value in <>h__TransparentIdentifier0.findResult.Values
						select new DateTimeSource(<>h__TransparentIdentifier0.input.ColumnName, value, DateTimeSourceKind.Parsed));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					dateTimeSourceList2 = dateTimeSourceList;
				}
				return dateTimeSourceList2;
			});
		}

		// Token: 0x0600BD1C RID: 48412 RVA: 0x0028D0A1 File Offset: 0x0028B2A1
		public IReadOnlyList<FindDateTimeCacheItem> FindParseableDateTimes(string subject, bool allowPartial = true)
		{
			return this.FindDateTimes(subject, allowPartial, Recognition.FindDateTimeKind.Parseable);
		}

		// Token: 0x0600BD1D RID: 48413 RVA: 0x0028D0AC File Offset: 0x0028B2AC
		public IReadOnlyList<FindDateTimeCacheItem> FindFormattedDateTimes(string subject, bool allowPartial = true)
		{
			return this.FindDateTimes(subject, allowPartial, Recognition.FindDateTimeKind.Formatted);
		}

		// Token: 0x0600BD1E RID: 48414 RVA: 0x0028D0B8 File Offset: 0x0028B2B8
		private IReadOnlyList<FindDateTimeCacheItem> FindDateTimes(string subject, bool allowPartial, Recognition.FindDateTimeKind kind)
		{
			object[] array = new object[] { "FindParseableDateTimes", subject, allowPartial, kind };
			IReadOnlyList<FindDateTimeCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<FindDateTimeCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<FindDateTimeCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", string.Format("{0}/{1}", "FindDateTimes", kind), true, true) : null))
			{
				IReadOnlyList<DateTimeDescriptor> descriptors;
				if (this.HasClassification(subject, StringClassification.FormattedOther))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<FindDateTimeCacheItem>(array);
				}
				else if (this.IsFormattedDateTime(subject, out descriptors))
				{
					List<FindDateTimeCacheItem> list = descriptors.Select(delegate(DateTimeDescriptor descriptor)
					{
						FindDateTimeCacheItem findDateTimeCacheItem = new FindDateTimeCacheItem();
						findDateTimeCacheItem.Substring = subject;
						findDateTimeCacheItem.Values = (from desc in descriptors
							let nativeValue = Operators.ParseDateTime(subject, desc, true)
							where nativeValue != null
							select nativeValue.Value).ToList<DateTime>();
						return findDateTimeCacheItem;
					}).ToList<FindDateTimeCacheItem>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<List<FindDateTimeCacheItem>>(array, list.ToList<FindDateTimeCacheItem>());
				}
				else if (this.IsFormattedNumber(subject))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<FindDateTimeCacheItem>(array);
				}
				else
				{
					string subjectLower = subject.ToLower();
					char[] subjectDelimiters = subject.Where((char c) => c.IsDelimiter()).ToArray<char>();
					IReadOnlyList<DateTimeDescriptor> readOnlyList3;
					if (kind != Recognition.FindDateTimeKind.Formatted)
					{
						if (kind != Recognition.FindDateTimeKind.Parseable)
						{
							throw new Exception(string.Format("Unknown FindDateTimeKind: {0}", kind));
						}
						readOnlyList3 = this.DateTimeParseDescriptors;
					}
					else
					{
						readOnlyList3 = this.DateTimeFormattedDescriptors;
					}
					IReadOnlyList<DateTimeDescriptor> readOnlyList4 = (from descriptor in readOnlyList3
						where allowPartial || !descriptor.AllowParsePartial
						where subject.Length >= descriptor.FormattedMinLength && subjectLower.Any((char c) => descriptor.FormattedLowerFirstChars.Contains(c)) && subjectLower.Any((char c) => descriptor.FormattedLowerLastChars.Contains(c)) && base.<FindDateTimes>g__HasDelimiterSequence|1(descriptor.MaskDelimiters)
						select descriptor).ToReadOnlyList<DateTimeDescriptor>();
					IReadOnlyList<FindDateTimeCacheItem> readOnlyList5 = (from descriptor in readOnlyList4
						let matches = descriptor.Regex.NonCachingMatches(subject)
						from match in matches
						let nativeValue = Operators.ParseDateTime(match.Value, descriptor, true)
						where nativeValue != null
						group nativeValue.Value by new { match.Index, match.Value } into groupMatch
						select new FindDateTimeCacheItem
						{
							StartIndex = groupMatch.Key.Index,
							Substring = groupMatch.Key.Value,
							Values = groupMatch.ToList<DateTime>()
						}).Distinct<FindDateTimeCacheItem>().ToReadOnlyList<FindDateTimeCacheItem>();
					LearnDebugTrace debugTrace2 = this._debugTrace;
					if (debugTrace2 != null)
					{
						debugTrace2.HitEvent("Descriptors", string.Format("{0}/{1}", "FindDateTimes", kind), readOnlyList5.Count);
					}
					LearnDebugTrace debugTrace3 = this._debugTrace;
					if (debugTrace3 != null)
					{
						debugTrace3.MissEvent("Descriptors", string.Format("{0}/{1}", "FindDateTimes", kind), readOnlyList4.Count - readOnlyList5.Count);
					}
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<IReadOnlyList<FindDateTimeCacheItem>>(array, readOnlyList5);
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD1F RID: 48415 RVA: 0x0028D440 File Offset: 0x0028B640
		public IReadOnlyList<FindPartDateTimeCacheItem> FindPartDateTimes(IRow inputRow)
		{
			object[] array = new object[] { "FindPartDateTimes", inputRow };
			IReadOnlyList<FindPartDateTimeCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<FindPartDateTimeCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<FindPartDateTimeCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "FindPartDateTimes", false, true) : null))
			{
				int yearLimit = DateTime.UtcNow.Year + 30;
				readOnlyList = (from columnName in inputRow.ColumnNames
					from kind in new DateTimePartKind[]
					{
						DateTimePartKind.Month,
						DateTimePartKind.Year
					}
					let fromDateTimePart = Operators.FromDateTimePart(inputRow, columnName, kind)
					where fromDateTimePart != null
					let value = fromDateTimePart.Value
					where kind != DateTimePartKind.Year || value.Year.BetweenInclusive(1900, yearLimit)
					select new FindPartDateTimeCacheItem
					{
						ColumnName = columnName,
						Value = value,
						Kind = kind
					}).ToList<FindPartDateTimeCacheItem>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<FindPartDateTimeCacheItem>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD20 RID: 48416 RVA: 0x0028D5D4 File Offset: 0x0028B7D4
		public IReadOnlyList<DateTime> ReverseFormattedDateTime(IRow inputRow, string result, bool allowPartial = true)
		{
			object[] array = new object[] { "ReverseFormattedDateTime", inputRow, allowPartial };
			IReadOnlyList<DateTime> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<DateTime>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<DateTime> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ReverseFormattedDateTime/row", true, true) : null))
			{
				if (!this.IsFormattedDateTime(result, false))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<DateTime>(array);
				}
				else
				{
					DateTime[] array2 = new DateTime[0];
					DateTime[] distinctValues = this.DateTimes(inputRow, this._dateTimeSourceKinds).DistinctValues;
					IEnumerable<DateTime> enumerable;
					if (this._enableRoundDateTime)
					{
						enumerable = from date in distinctValues
							from roundDate in this.ToRoundDateTimes(date)
							select roundDate;
					}
					else
					{
						IEnumerable<DateTime> enumerable2 = array2;
						enumerable = enumerable2;
					}
					IEnumerable<DateTime> enumerable3 = enumerable;
					IEnumerable<DateTime> enumerable4;
					if (this._enableDateTimePart)
					{
						enumerable4 = from item in this.FindPartDateTimes(inputRow)
							select item.Value;
					}
					else
					{
						IEnumerable<DateTime> enumerable2 = array2;
						enumerable4 = enumerable2;
					}
					IEnumerable<DateTime> enumerable5 = enumerable4;
					readOnlyList = distinctValues.Union(enumerable3).Union(enumerable5).Distinct<DateTime>()
						.ToList<DateTime>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<IReadOnlyList<DateTime>>(array, readOnlyList);
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD21 RID: 48417 RVA: 0x0028D730 File Offset: 0x0028B930
		public bool TryFormatDateTime(IRow inputRow, string result)
		{
			IEnumerable<DateTimeDescriptor> enumerable;
			return this.TryFormatDateTime(inputRow, result, out enumerable);
		}

		// Token: 0x0600BD22 RID: 48418 RVA: 0x0028D748 File Offset: 0x0028B948
		public bool TryFormatDateTime(IRow inputRow, string result, out IEnumerable<DateTimeDescriptor> descriptors)
		{
			object[] array = new object[] { "TryFormatDateTime", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<DateTimeDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<DateTimeDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryFormatDateTime/row", true, true) : null))
			{
				if (!this.IsFormattedDateTime(result, false))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<DateTimeDescriptor>(array).Any<DateTimeDescriptor>();
				}
				else
				{
					descriptors = (from <>h__TransparentIdentifier0 in this.ReverseFormattedDateTime(inputRow, result, true).Select(delegate(DateTime dateTime)
						{
							IReadOnlyList<DateTimeDescriptor> readOnlyList;
							return new
							{
								dateTime = dateTime,
								localDescriptors = (this.TryFormatDateTime(dateTime, result, out readOnlyList, false) ? readOnlyList : null)
							};
						})
						where <>h__TransparentIdentifier0.localDescriptors != null
						from localDescriptor in <>h__TransparentIdentifier0.localDescriptors
						select localDescriptor).Distinct<DateTimeDescriptor>().ToReadOnlyList<DateTimeDescriptor>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSet<IEnumerable<DateTimeDescriptor>>(array, descriptors).Any<DateTimeDescriptor>();
				}
			}
			return flag;
		}

		// Token: 0x0600BD23 RID: 48419 RVA: 0x0028D8A8 File Offset: 0x0028BAA8
		public bool TryFormatDateTime(DateTime source, string result, bool ignoreCase = false)
		{
			IReadOnlyList<DateTimeDescriptor> readOnlyList;
			return this.TryFormatDateTime(source, result, out readOnlyList, ignoreCase);
		}

		// Token: 0x0600BD24 RID: 48420 RVA: 0x0028D8C0 File Offset: 0x0028BAC0
		public bool TryFormatDateTime(DateTime subject, string result, out IReadOnlyList<DateTimeDescriptor> descriptors, bool ignoreCase = false)
		{
			object[] array = new object[] { "TryFormatDateTime", subject, result, ignoreCase };
			if (this.CacheTryGetValue<IReadOnlyList<DateTimeDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<DateTimeDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryFormatDateTime", true, true) : null))
			{
				IReadOnlyList<DateTimeDescriptor> readOnlyList;
				if (!this.IsFormattedDateTime(result, out readOnlyList))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<DateTimeDescriptor>(array).Any<DateTimeDescriptor>();
				}
				else
				{
					descriptors = (from descriptor in readOnlyList
						let formatted = Operators.FormatDateTime(subject, descriptor)
						where formatted != null && formatted.Equals(result, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)
						select descriptor).ToList<DateTimeDescriptor>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSet<IReadOnlyList<DateTimeDescriptor>>(array, descriptors).Any<DateTimeDescriptor>();
				}
			}
			return flag;
		}

		// Token: 0x0600BD25 RID: 48421 RVA: 0x0028D9FC File Offset: 0x0028BBFC
		public IEnumerable<DateTime> ToRoundDateTimes(DateTime subject)
		{
			object[] array = new object[] { "ToRoundDateTimes", subject };
			IEnumerable<DateTime> enumerable;
			if (this.CacheTryGetValue<IEnumerable<DateTime>>(array, out enumerable))
			{
				return enumerable;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<DateTime> enumerable2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ToRoundDateTimes", false, true) : null))
			{
				enumerable = from desc in Recognition._roundDateTimeDescriptors
					let roundNumber = Operators.RoundDateTime(subject, desc)
					where roundNumber != null
					select roundNumber.Value;
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable2 = this.CacheSet<List<DateTime>>(array, enumerable.ToList<DateTime>());
			}
			return enumerable2;
		}

		// Token: 0x0600BD26 RID: 48422 RVA: 0x0028DAF4 File Offset: 0x0028BCF4
		public bool TryRoundDateTime(DateTime subject, DateTime result)
		{
			IEnumerable<RoundDateTimeCacheItem> enumerable;
			return this.TryRoundDateTime(subject, result, out enumerable);
		}

		// Token: 0x0600BD27 RID: 48423 RVA: 0x0028DB0C File Offset: 0x0028BD0C
		public bool TryRoundDateTime(DateTime subject, DateTime result, out IEnumerable<RoundDateTimeCacheItem> cacheItems)
		{
			object[] array = new object[] { "TryRoundDateTime", subject, result };
			if (this.CacheTryGetValue<IEnumerable<RoundDateTimeCacheItem>>(array, out cacheItems))
			{
				IEnumerable<RoundDateTimeCacheItem> enumerable = cacheItems;
				return enumerable != null && enumerable.Any<RoundDateTimeCacheItem>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryRoundDateTime", false, true) : null))
			{
				cacheItems = (from descriptor in Recognition._roundDateTimeDescriptors.Where(delegate(RoundDateTimeDescriptor descriptor)
					{
						DateTime? dateTime = Operators.RoundDateTime(subject, descriptor);
						DateTime result2 = result;
						return dateTime != null && (dateTime == null || dateTime.GetValueOrDefault() == result2);
					})
					select new RoundDateTimeCacheItem
					{
						Subject = subject,
						Result = result,
						Descriptor = descriptor
					}).ToList<RoundDateTimeCacheItem>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IEnumerable<RoundDateTimeCacheItem> enumerable2 = this.CacheSet<IEnumerable<RoundDateTimeCacheItem>>(array, cacheItems);
				flag = enumerable2 != null && enumerable2.Any<RoundDateTimeCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD28 RID: 48424 RVA: 0x0028DBFC File Offset: 0x0028BDFC
		public bool TryDateTimePart(DateTime subject, decimal result)
		{
			IEnumerable<DateTimePartCacheItem> enumerable;
			return this.TryDateTimePart(subject, result, out enumerable);
		}

		// Token: 0x0600BD29 RID: 48425 RVA: 0x0028DC14 File Offset: 0x0028BE14
		public bool TryDateTimePart(DateTime subject, decimal result, out IEnumerable<DateTimePartCacheItem> cacheItems)
		{
			object[] array = new object[] { "TryDateTimePart", subject, result };
			if (this.CacheTryGetValue<IEnumerable<DateTimePartCacheItem>>(array, out cacheItems))
			{
				IEnumerable<DateTimePartCacheItem> enumerable = cacheItems;
				return enumerable != null && enumerable.Any<DateTimePartCacheItem>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "DecomposeDateTime", false, true) : null))
			{
				cacheItems = (from kind in Recognition._dateTimePartKinds.Where(delegate(DateTimePartKind kind)
					{
						decimal? num = Operators.DateTimePart(subject, kind);
						decimal result2 = result;
						return (num.GetValueOrDefault() == result2) & (num != null);
					})
					select new DateTimePartCacheItem
					{
						Subject = subject,
						Result = result,
						Kind = kind
					}).ToList<DateTimePartCacheItem>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IEnumerable<DateTimePartCacheItem> enumerable2 = this.CacheSet<IEnumerable<DateTimePartCacheItem>>(array, cacheItems);
				flag = enumerable2 != null && enumerable2.Any<DateTimePartCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD2A RID: 48426 RVA: 0x0028DD04 File Offset: 0x0028BF04
		public bool TryTimePart(Time subject, decimal result)
		{
			IEnumerable<TimePartCacheItem> enumerable;
			return this.TryTimePart(subject, result, out enumerable);
		}

		// Token: 0x0600BD2B RID: 48427 RVA: 0x0028DD1C File Offset: 0x0028BF1C
		public bool TryTimePart(Time subject, decimal result, out IEnumerable<TimePartCacheItem> cacheItems)
		{
			object[] array = new object[] { "TryTimePart", subject, result };
			if (this.CacheTryGetValue<IEnumerable<TimePartCacheItem>>(array, out cacheItems))
			{
				IEnumerable<TimePartCacheItem> enumerable = cacheItems;
				return enumerable != null && enumerable.Any<TimePartCacheItem>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryTimePart", false, true) : null))
			{
				cacheItems = (from kind in Recognition._timePartKinds.Where(delegate(TimePartKind kind)
					{
						decimal? num = Operators.TimePart(subject, kind);
						decimal result2 = result;
						return (num.GetValueOrDefault() == result2) & (num != null);
					})
					select new TimePartCacheItem
					{
						Subject = subject,
						Result = result,
						Kind = kind
					}).ToList<TimePartCacheItem>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IEnumerable<TimePartCacheItem> enumerable2 = this.CacheSet<IEnumerable<TimePartCacheItem>>(array, cacheItems);
				flag = enumerable2 != null && enumerable2.Any<TimePartCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD2C RID: 48428 RVA: 0x0028DE0C File Offset: 0x0028C00C
		public unsafe ReadOnlySpan<char> TakeWhileDateTimeChar(ReadOnlySpan<char> subject)
		{
			int i;
			for (i = 0; i < subject.Length; i++)
			{
				char c = (char)(*subject[i]);
				if (!this.DateTimeChars.Contains(c) || (c == ' ' && (!(i + 1).IsValidIndex(subject) || !this.DateTimeChars.Contains((char)(*subject[i + 1])))))
				{
					break;
				}
			}
			return subject.Take(i);
		}

		// Token: 0x0600BD2D RID: 48429 RVA: 0x0028DE74 File Offset: 0x0028C074
		public IReadOnlyList<decimal> DecomposeDateTime(DateTime subject)
		{
			object[] array = new object[] { "DecomposeDateTime", subject };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "DecomposeDateTime", false, true) : null))
			{
				readOnlyList = (from descriptor in Recognition._dateTimePartKinds
					let part = Operators.DateTimePart(subject, descriptor)
					where part != null
					select part.Value).ToList<decimal>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD2E RID: 48430 RVA: 0x0028DF6C File Offset: 0x0028C16C
		public IReadOnlyList<decimal> DecomposeTime(Time subject)
		{
			object[] array = new object[] { "DecomposeTime", subject };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "DecomposeTime", false, true) : null))
			{
				readOnlyList = (from kind in Recognition._timePartKinds
					let part = Operators.TimePart(subject, kind)
					where part != null
					select part.Value).ToList<decimal>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<IReadOnlyList<decimal>>(array, readOnlyList);
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD2F RID: 48431 RVA: 0x0028E064 File Offset: 0x0028C264
		private IEnumerable<char> LoadDateTimeChars()
		{
			List<DateTimeFormatInfo> list = this.DataCultures.Select((CultureInfo c) => c.DateTimeFormat).ToList<DateTimeFormatInfo>();
			IEnumerable<char> enumerable = from culture in this.DataCultures
				from digit in culture.NumberFormat.NativeDigits
				from c in digit
				select c;
			IEnumerable<char> enumerable2 = from format in list
				from monthName in format.MonthNames
				from c in monthName
				select c;
			IEnumerable<char> enumerable3 = from format in list
				from dayName in format.AbbreviatedMonthNames
				from c in dayName
				select c;
			IEnumerable<char> enumerable4 = from format in list
				from dayName in format.DayNames
				from c in dayName
				select c;
			IEnumerable<char> enumerable5 = from format in list
				from day in format.AbbreviatedDayNames
				from c in day
				select c;
			IEnumerable<char> enumerable6 = from descriptor in this.DateTimeFormattedDescriptors.Concat(this.DateTimeParseDescriptors)
				from maskDelimiter in descriptor.MaskDelimiters
				select maskDelimiter;
			return enumerable.Concat(enumerable2).Concat(enumerable3).Concat(enumerable4)
				.Concat(enumerable5)
				.Concat(enumerable6);
		}

		// Token: 0x0600BD30 RID: 48432 RVA: 0x0028E3C4 File Offset: 0x0028C5C4
		public IReadOnlyList<DateTimeDescriptor> LoadDateTimeDescriptors(CultureInfo sourceCulture)
		{
			IEnumerable<CultureInfo> enumerable = new CultureInfo[] { sourceCulture };
			IEnumerable<DateTimeDescriptor> enumerable2 = Utils.Empty<DateTimeDescriptor>();
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<DateTimeDescriptor> finalDescriptors2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Startup", "LoadDateTimeDescriptors/" + sourceCulture.Name, false, true) : null))
			{
				IEnumerable<string> dateSeparators = new string[] { "/", "-", "." }.Union(enumerable.Select((CultureInfo c) => c.DateTimeFormat.DateSeparator));
				enumerable2 = enumerable2.Union(from culture in enumerable
					let dateFormat = culture.DateTimeFormat
					from mask in new string[]
					{
						dateFormat.FullDateTimePattern,
						dateFormat.SortableDateTimePattern,
						dateFormat.ShortDatePattern,
						dateFormat.ShortDatePattern + " " + dateFormat.ShortTimePattern,
						dateFormat.LongDatePattern,
						dateFormat.LongDatePattern + " " + dateFormat.ShortTimePattern,
						"yyyy'-'MM'-'dd'T'HH':'mm",
						"yyyy'-'MM'-'dd'T'HH':'mm':'ss",
						"yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
						"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff",
						"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff"
					}
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = mask,
						AllowFormat = true,
						AllowParse = true,
						AllowParsePartial = false,
						IsPartial = false
					});
				string text = sourceCulture.DateTimeFormat.ShortDatePattern;
				bool flag = text == "M/d/yyyy" || text == "MM/dd/yyyy";
				if (flag)
				{
					enumerable2 = enumerable2.Union(from culture in enumerable
						from sep in dateSeparators
						from date in new string[]
						{
							string.Concat(new string[] { "M", sep, "d", sep, "yy" }),
							string.Concat(new string[] { "M", sep, "d", sep, "yyyy" }),
							string.Concat(new string[] { "M", sep, "dd", sep, "yy" }),
							string.Concat(new string[] { "M", sep, "dd", sep, "yyyy" }),
							string.Concat(new string[] { "MM", sep, "dd", sep, "yy" }),
							string.Concat(new string[] { "MM", sep, "dd", sep, "yyyy" })
						}
						from time in Recognition.<LoadDateTimeDescriptors>g__ResolveTimeVariants|140_36(culture)
						select new DateTimeDescriptor
						{
							Culture = culture,
							Mask = date + time,
							AllowFormat = true,
							AllowParse = true,
							AllowParsePartial = false,
							IsPartial = false
						});
				}
				text = sourceCulture.DateTimeFormat.ShortDatePattern;
				flag = text == "d/M/yyyy" || text == "dd/MM/yyyy" || text == "yyyy/MM/dd";
				if (flag)
				{
					enumerable2 = enumerable2.Union(from culture in enumerable
						from sep in dateSeparators
						from date in new string[]
						{
							string.Concat(new string[] { "d", sep, "M", sep, "yy" }),
							string.Concat(new string[] { "d", sep, "M", sep, "yyyy" }),
							string.Concat(new string[] { "d", sep, "MM", sep, "yy" }),
							string.Concat(new string[] { "d", sep, "MM", sep, "yyyy" }),
							string.Concat(new string[] { "dd", sep, "MM", sep, "yy" }),
							string.Concat(new string[] { "dd", sep, "MM", sep, "yyyy" })
						}
						from time in Recognition.<LoadDateTimeDescriptors>g__ResolveTimeVariants|140_36(culture)
						select new DateTimeDescriptor
						{
							Culture = culture,
							Mask = date + time,
							AllowFormat = true,
							AllowParse = true,
							AllowParsePartial = false,
							IsPartial = false
						});
				}
				enumerable2 = enumerable2.Union(from culture in enumerable
					from date in new string[]
					{
						"dd-MMM-yy", "d-MMM-yy", "d-MMM-yyyy", "d MMM yyyy", "d MMMM yyyy", "dd-MMM-yyyy", "dd MMM yyyy", "dd MMMM yyyy", "dddd MMMM d yyyy", "dddd MMMM d, yyyy",
						"dddd, MMMM d, yyyy", "dddd, MMMM yyyy", "MMM-d-yyyy", "MMM-dd-yyyy", "MMM d yyyy", "MMMM d yyyy", "MMM d, yyyy", "MMMM d, yyyy", "MMddyyyy", "yyyy-MM-dd",
						"yyyy/M/dd", "yyyy/M/d", "yyyy/MM/dd", "yyyy/MM/d"
					}
					from time in Recognition.<LoadDateTimeDescriptors>g__ResolveTimeVariants|140_36(culture)
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = date + time,
						AllowFormat = true,
						AllowParse = true,
						AllowParsePartial = false,
						IsPartial = false
					});
				enumerable2 = enumerable2.Union(from culture in enumerable
					from mask in new string[] { "%M", "MM", "MMM", "MMMM", "MMM yyyy", "MMMM yyyy" }
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = mask,
						AllowFormat = true,
						AllowParse = true,
						AllowParsePartial = true,
						IsPartial = true
					});
				enumerable2 = enumerable2.Union(from culture in enumerable
					let cultureDateTimeFormat = culture.DateTimeFormat
					from mask in new string[] { cultureDateTimeFormat.ShortTimePattern, cultureDateTimeFormat.LongTimePattern }
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = mask,
						AllowFormat = true,
						AllowParse = false,
						AllowParsePartial = false,
						IsPartial = true
					});
				enumerable2 = enumerable2.Union(from culture in enumerable
					from mask in new string[] { "%d", "dd", "yy", "yyyy", "tt" }
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = mask,
						AllowFormat = true,
						AllowParse = (mask == "yyyy"),
						AllowParsePartial = (mask == "yyyy"),
						IsPartial = true
					});
				enumerable2 = enumerable2.Union(from culture in enumerable
					from date in new string[] { "d MMM", "d MMMM", "ddd", "dddd", "MMM d", "MMMM d", "" }
					from time in Recognition.<LoadDateTimeDescriptors>g__ResolveTimeVariants|140_36(culture)
					let mask = (date + time).Trim()
					where !string.IsNullOrEmpty(mask)
					select new DateTimeDescriptor
					{
						Culture = culture,
						Mask = mask,
						AllowFormat = true,
						AllowParse = false,
						AllowParsePartial = false,
						IsPartial = true
					});
				IReadOnlyList<DateTimeDescriptor> finalDescriptors = enumerable2.Distinct<DateTimeDescriptor>().ToReadOnlyList<DateTimeDescriptor>();
				Recognition._dateTimeDescriptorCache.AddOrUpdate(sourceCulture.Name, (string _) => finalDescriptors, (string _, IReadOnlyList<DateTimeDescriptor> _) => finalDescriptors);
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				finalDescriptors2 = finalDescriptors;
			}
			return finalDescriptors2;
		}

		// Token: 0x0600BD31 RID: 48433 RVA: 0x0028E9CC File Offset: 0x0028CBCC
		public IReadOnlyList<DateTimeDescriptor> LoadDateTimeFormattedDescriptors()
		{
			return (from <>h__TransparentIdentifier0 in this.DataCultures.Select(delegate(CultureInfo culture)
				{
					IReadOnlyList<DateTimeDescriptor> readOnlyList;
					return new
					{
						culture = culture,
						descriptors = (Recognition._dateTimeDescriptorCache.Lookup(culture.Name, out readOnlyList) ? readOnlyList : this.LoadDateTimeDescriptors(culture))
					};
				})
				from descriptor in descriptors
				where descriptor.AllowFormat
				select descriptor).ToList<DateTimeDescriptor>();
		}

		// Token: 0x0600BD32 RID: 48434 RVA: 0x0028EA80 File Offset: 0x0028CC80
		public IReadOnlyList<DateTimeDescriptor> LoadDateTimeParseDescriptors()
		{
			return (from <>h__TransparentIdentifier0 in this.DataCultures.Select(delegate(CultureInfo culture)
				{
					IReadOnlyList<DateTimeDescriptor> readOnlyList;
					return new
					{
						culture = culture,
						descriptors = (Recognition._dateTimeDescriptorCache.Lookup(culture.Name, out readOnlyList) ? readOnlyList : this.LoadDateTimeDescriptors(culture))
					};
				})
				from descriptor in descriptors
				where descriptor.AllowParse
				select descriptor).ToList<DateTimeDescriptor>();
		}

		// Token: 0x0600BD33 RID: 48435 RVA: 0x0028EB34 File Offset: 0x0028CD34
		private static IReadOnlyList<RoundDateTimeDescriptor> LoadRoundDateTimeDescriptors()
		{
			return (from mode in new RoundingMode[]
				{
					RoundingMode.Nearest,
					RoundingMode.Up,
					RoundingMode.Down
				}
				from period in EnumUtils.GetValues<RoundDateTimePeriod>()
				from ceiling in from roundPeriodCeiling in EnumUtils.GetValues<RoundDatePeriodCeiling>()
					where roundPeriodCeiling == RoundDatePeriodCeiling.Ceiling || (period > RoundDateTimePeriod.Day && mode == RoundingMode.Up)
					select roundPeriodCeiling
				select new RoundDateTimeDescriptor
				{
					Mode = mode,
					Period = period,
					Ceiling = ceiling
				}).ToList<RoundDateTimeDescriptor>();
		}

		// Token: 0x0600BD34 RID: 48436 RVA: 0x0028EBDC File Offset: 0x0028CDDC
		public IEnumerable<decimal> FindRowNumberTransforms(int rowNumber, string result, IDictionary<int, IEnumerable<string>> disjunctiveExamples)
		{
			object[] array = new object[]
			{
				"FindRowNumberTransforms",
				rowNumber,
				result,
				disjunctiveExamples.KeyValueHashCode<string>()
			};
			IEnumerable<decimal> enumerable;
			if (this.CacheTryGetValue<IEnumerable<decimal>>(array, out enumerable))
			{
				return enumerable;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<decimal> enumerable2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindRowNumberTransforms/string", false, true) : null))
			{
				FindNumberCacheItem findNumberCacheItem = this.FindNumbers(result, true, new int?(10)).FirstOrDefault<FindNumberCacheItem>();
				decimal? num = ((findNumberCacheItem != null) ? new decimal?(findNumberCacheItem.Value) : null);
				if (num == null)
				{
					enumerable2 = this.CacheSetEmptyList<decimal>(array);
				}
				else
				{
					Dictionary<int, IEnumerable<decimal>> dictionary = disjunctiveExamples.ToDictionary((KeyValuePair<int, IEnumerable<string>> e) => e.Key, (KeyValuePair<int, IEnumerable<string>> e) => from output in e.Value
						from number in this.FindNumbers(output, true, new int?(10))
						select number.Value);
					enumerable = this.FindRowNumberTransforms(rowNumber, num.Value, dictionary).ToList<decimal>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					enumerable2 = this.CacheSet<IEnumerable<decimal>>(array, enumerable);
				}
			}
			return enumerable2;
		}

		// Token: 0x0600BD35 RID: 48437 RVA: 0x0028ECFC File Offset: 0x0028CEFC
		public IEnumerable<decimal> FindRowNumberTransforms(int rowNumber, decimal result, IDictionary<int, IEnumerable<decimal>> disjunctiveExamples)
		{
			object[] array = new object[]
			{
				"FindRowNumberTransforms",
				rowNumber,
				result,
				disjunctiveExamples.KeyValueHashCode<decimal>()
			};
			IEnumerable<decimal> enumerable;
			if (this.CacheTryGetValue<IEnumerable<decimal>>(array, out enumerable))
			{
				return enumerable;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<decimal> enumerable2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "FindRowNumberTransforms/decimal", false, true) : null))
			{
				List<decimal> list = new List<decimal>();
				IReadOnlyList<RowNumberLinearTransformDescriptor> readOnlyList;
				if (this.TryRowNumberLinearTransform(rowNumber, result, disjunctiveExamples, out readOnlyList))
				{
					list.AddRange(readOnlyList.Select((RowNumberLinearTransformDescriptor descriptor) => Operators.RowNumberLinearTransform(rowNumber, descriptor)));
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable2 = this.CacheSet<List<decimal>>(array, list);
			}
			return enumerable2;
		}

		// Token: 0x0600BD36 RID: 48438 RVA: 0x0028EDDC File Offset: 0x0028CFDC
		public bool TryRowNumberLinearTransform(int rowNumber, decimal result, IDictionary<int, IEnumerable<decimal>> disjunctiveExamples, out IReadOnlyList<RowNumberLinearTransformDescriptor> descriptors)
		{
			object[] array = new object[]
			{
				"TryRowNumberLinearTransform",
				rowNumber,
				result,
				disjunctiveExamples.KeyValueHashCode<decimal>()
			};
			if (this.CacheTryGetValue<IReadOnlyList<RowNumberLinearTransformDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<RowNumberLinearTransformDescriptor>();
			}
			if (result.Scale() > this._forwardFillMaxScale)
			{
				this.CacheSetEmptyList<RowNumberLinearTransformDescriptor>(array);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryRowNumberLinearTransform", false, true) : null))
			{
				descriptors = (from key in disjunctiveExamples.Keys
					where key != rowNumber
					select key into otherRowNumber
					from otherResult in disjunctiveExamples[otherRowNumber]
					let gradient = (otherResult - result) / (otherRowNumber - rowNumber)
					let intercept = result - gradient * rowNumber
					where gradient != 0m && gradient.Scale() <= this._forwardFillMaxScale && intercept.Scale() <= this._forwardFillMaxScale
					let descriptor = new RowNumberLinearTransformDescriptor
					{
						Gradient = gradient,
						Intercept = intercept
					}
					where Operators.RowNumberLinearTransform(rowNumber, descriptor) == result
					select descriptor).Distinct<RowNumberLinearTransformDescriptor>().ToList<RowNumberLinearTransformDescriptor>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				flag = this.CacheSet<IReadOnlyList<RowNumberLinearTransformDescriptor>>(array, descriptors).Any<RowNumberLinearTransformDescriptor>();
			}
			return flag;
		}

		// Token: 0x0600BD37 RID: 48439 RVA: 0x0028EFC0 File Offset: 0x0028D1C0
		public IReadOnlyList<MatchCacheItem> Matches(string subject)
		{
			object[] array = new object[] { "Matches", subject };
			IReadOnlyList<MatchCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<MatchCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<MatchCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "Matches", false, true) : null))
			{
				IEnumerable<MatchCacheItem> enumerable = from descriptor in this._enableMatchUnicode ? MatchDescriptor.Descriptors : MatchDescriptor.DescriptorsAscii
					where this._enableMatchNames.HasFlag(descriptor.Name)
					from match in descriptor.Regex.NonCachingMatches(subject)
					where match.Success
					select new MatchCacheItem
					{
						Descriptor = descriptor,
						StartIndex = match.Index,
						EndIndex = match.Index + match.Value.Length,
						Value = match.Value
					};
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<MatchCacheItem>>(array, enumerable.ToList<MatchCacheItem>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD38 RID: 48440 RVA: 0x0028F0FC File Offset: 0x0028D2FC
		public bool TryMatch(string subject, int index, out IReadOnlyList<MatchInstanceCacheItem> matches)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryMatch", false, true) : null))
			{
				matches = this.ParseMatchDescriptors(subject, index, false);
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IReadOnlyList<MatchInstanceCacheItem> readOnlyList = matches;
				flag = readOnlyList != null && readOnlyList.Any<MatchInstanceCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD39 RID: 48441 RVA: 0x0028F168 File Offset: 0x0028D368
		public bool TryMatchEnd(string subject, int index, out IReadOnlyList<MatchInstanceCacheItem> matches)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryMatchEnd", false, true) : null))
			{
				matches = this.ParseMatchDescriptors(subject, index, true);
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IReadOnlyList<MatchInstanceCacheItem> readOnlyList = matches;
				flag = readOnlyList != null && readOnlyList.Any<MatchInstanceCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD3A RID: 48442 RVA: 0x0028F1D4 File Offset: 0x0028D3D4
		public bool TryMatchFull(string subject, string result, out IReadOnlyList<MatchInstanceCacheItem> matches)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryMatchFull", false, true) : null))
			{
				matches = this.ParseMatchFullDescriptors(subject, result);
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IReadOnlyList<MatchInstanceCacheItem> readOnlyList = matches;
				flag = readOnlyList != null && readOnlyList.Any<MatchInstanceCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD3B RID: 48443 RVA: 0x0028F240 File Offset: 0x0028D440
		public IReadOnlyList<MatchInstanceCacheItem> ParseMatchDescriptors(string subject, int index, bool useEnd)
		{
			object[] array = new object[] { "ParseMatchDescriptors", subject, index, useEnd };
			IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<MatchInstanceCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<MatchInstanceCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "ParseMatchDescriptors", false, true) : null))
			{
				IEnumerable<MatchInstanceCacheItem> enumerable;
				if (useEnd)
				{
					enumerable = from m in this.Matches(subject)
						group m by m.Descriptor into gms
						from im in gms
						let instance = gms.TakeWhile((MatchCacheItem j) => j.EndIndex <= index).Count<MatchCacheItem>()
						let instanceFromEnd = -1 * (gms.Count<MatchCacheItem>() - instance + 1)
						where im.EndIndex == index
						select new MatchInstanceCacheItem
						{
							Descriptor = im.Descriptor,
							StartIndex = im.StartIndex,
							EndIndex = im.EndIndex,
							Value = im.Value,
							Instance = instance,
							InstanceFromEnd = instanceFromEnd
						};
				}
				else
				{
					enumerable = from m in this.Matches(subject)
						group m by m.Descriptor into gms
						from im in gms
						let instance = gms.TakeWhile((MatchCacheItem j) => j.StartIndex <= index).Count<MatchCacheItem>()
						let instanceFromEnd = -1 * (gms.Count<MatchCacheItem>() - instance + 1)
						where im.StartIndex == index
						select new MatchInstanceCacheItem
						{
							Descriptor = im.Descriptor,
							StartIndex = im.StartIndex,
							EndIndex = im.EndIndex,
							Value = im.Value,
							Instance = instance,
							InstanceFromEnd = instanceFromEnd
						};
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = this.CacheSet<List<MatchInstanceCacheItem>>(array, enumerable.ToList<MatchInstanceCacheItem>());
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD3C RID: 48444 RVA: 0x0028F4B4 File Offset: 0x0028D6B4
		public IReadOnlyList<MatchInstanceCacheItem> ParseMatchFullDescriptors(string subject, string result)
		{
			object[] array = new object[] { "ParseMatchFullDescriptors", subject, result };
			IReadOnlyList<MatchInstanceCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<MatchInstanceCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			IEnumerable<MatchInstanceCacheItem> enumerable = from <>h__TransparentIdentifier0 in subject.AllIndexesOf(result, StringComparison.Ordinal).SelectMany(delegate(int index)
				{
					IEnumerable<MatchInstanceCacheItem> enumerable2 = this.ParseMatchDescriptors(subject, index, false);
					return enumerable2 ?? Utils.Empty<MatchInstanceCacheItem>();
				}, (int index, MatchInstanceCacheItem descriptor) => new { index, descriptor })
				where <>h__TransparentIdentifier0.descriptor != null && <>h__TransparentIdentifier0.descriptor.Value == result
				select <>h__TransparentIdentifier0.descriptor;
			return this.CacheSet<List<MatchInstanceCacheItem>>(array, enumerable.ToList<MatchInstanceCacheItem>());
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x0600BD3D RID: 48445 RVA: 0x0028F590 File Offset: 0x0028D790
		public HashSet<char> NumberChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberChars) == null)
				{
					hashSet = (this._numberChars = this.NumberDigitChars.Concat(this.NumberSignChars).Concat(this.NumberGroupSeparatorChars).Concat(this.NumberDecimalSeparatorChars)
						.Concat(this.NumberPercentSymbolChars)
						.Concat(this.NumberCurrencySymbolChars)
						.Concat(' '.Yield<char>())
						.ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x0600BD3E RID: 48446 RVA: 0x0028F600 File Offset: 0x0028D800
		public HashSet<char> NumberCurrencySymbolChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberCurrencySymbolChars) == null)
				{
					hashSet = (this._numberCurrencySymbolChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.CurrencySymbol).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x0600BD3F RID: 48447 RVA: 0x0028F650 File Offset: 0x0028D850
		public HashSet<char> NumberDecimalSeparatorChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberDecimalSeparatorChars) == null)
				{
					hashSet = (this._numberDecimalSeparatorChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberDecimalSeparator).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x0600BD40 RID: 48448 RVA: 0x0028F6A0 File Offset: 0x0028D8A0
		public char[] NumberDecimalSeparatorCharsArray
		{
			get
			{
				char[] array;
				if ((array = this._numberDecimalSeparatorCharsArray) == null)
				{
					array = (this._numberDecimalSeparatorCharsArray = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberDecimalSeparator).ToArray<char>());
				}
				return array;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x0600BD41 RID: 48449 RVA: 0x0028F6F0 File Offset: 0x0028D8F0
		public HashSet<char> NumberDigitChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberDigitChars) == null)
				{
					hashSet = (this._numberDigitChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NativeDigits).SelectMany((string c) => c.ToCharArray()).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x0600BD42 RID: 48450 RVA: 0x0028F764 File Offset: 0x0028D964
		public Regex NumberFormattedRegex
		{
			get
			{
				Regex regex;
				if ((regex = this._numberFormattedRegex) == null)
				{
					regex = (this._numberFormattedRegex = this.LoadFormattedNumberRegex(true));
				}
				return regex;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x0600BD43 RID: 48451 RVA: 0x0028F78C File Offset: 0x0028D98C
		public HashSet<char> NumberGroupSeparatorChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberGroupSeparatorChars) == null)
				{
					hashSet = (this._numberGroupSeparatorChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberGroupSeparator).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x0600BD44 RID: 48452 RVA: 0x0028F7DC File Offset: 0x0028D9DC
		public int NumberMinGroupSize
		{
			get
			{
				int num = this._numberMinGroupSize.GetValueOrDefault();
				if (this._numberMinGroupSize == null)
				{
					num = this.DataCultures.SelectMany((CultureInfo c) => c.NumberFormat.NumberGroupSizes).Min();
					this._numberMinGroupSize = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x0600BD45 RID: 48453 RVA: 0x0028F844 File Offset: 0x0028DA44
		public Regex NumberParseRegex
		{
			get
			{
				Regex regex;
				if ((regex = this._numberParseRegex) == null)
				{
					regex = (this._numberParseRegex = this.LoadNumberParseRegex(false));
				}
				return regex;
			}
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x0600BD46 RID: 48454 RVA: 0x0028F86C File Offset: 0x0028DA6C
		public HashSet<char> NumberPercentSymbolChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberPercentSymbolChars) == null)
				{
					hashSet = (this._numberPercentSymbolChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.PercentSymbol).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x0600BD47 RID: 48455 RVA: 0x0028F8BC File Offset: 0x0028DABC
		public HashSet<char> NumberSignChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._numberSignChars) == null)
				{
					hashSet = (this._numberSignChars = this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NegativeSign).Concat(this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.PositiveSign)).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x0600BD48 RID: 48456 RVA: 0x0028F93C File Offset: 0x0028DB3C
		internal IReadOnlyList<FormatNumberDescriptor> NumberFormatDescriptors
		{
			get
			{
				IReadOnlyList<FormatNumberDescriptor> readOnlyList;
				if ((readOnlyList = this._numberFormatDescriptors) == null)
				{
					readOnlyList = (this._numberFormatDescriptors = this.LoadNumberFormatDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x0600BD49 RID: 48457 RVA: 0x0028F962 File Offset: 0x0028DB62
		private static IReadOnlyList<RoundNumberDescriptor> RoundNumberDescriptors
		{
			get
			{
				IReadOnlyList<RoundNumberDescriptor> readOnlyList;
				if ((readOnlyList = Recognition._roundNumberDescriptors) == null)
				{
					readOnlyList = (Recognition._roundNumberDescriptors = Recognition.LoadRoundNumberDescriptors());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600BD4A RID: 48458 RVA: 0x0028F978 File Offset: 0x0028DB78
		public NumberSourceList Numbers(IRow inputRow, bool coalesceZero)
		{
			return this.Numbers(inputRow, coalesceZero, this._numberSourceKinds);
		}

		// Token: 0x0600BD4B RID: 48459 RVA: 0x0028F988 File Offset: 0x0028DB88
		public NumberSourceList Numbers(IRow inputRow, bool coalesceZero, NumberSourceKind kind)
		{
			return this.CacheGetOrCompute<NumberSourceList>(new object[] { "Numbers", inputRow, coalesceZero, kind }, delegate
			{
				IEnumerable<NumberSource> enumerable = Utils.Empty<NumberSource>();
				LearnDebugTrace debugTrace = this._debugTrace;
				NumberSourceList numberSourceList;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "Numbers", false, true) : null))
				{
					if (kind.HasFlag(NumberSourceKind.Input))
					{
						enumerable = enumerable.Union(this.NumberInputs(inputRow, coalesceZero));
					}
					if (kind.HasFlag(NumberSourceKind.Parsed))
					{
						enumerable = enumerable.Union(this.NumberParsed(inputRow));
					}
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					numberSourceList = new NumberSourceList(enumerable);
				}
				return numberSourceList;
			});
		}

		// Token: 0x0600BD4C RID: 48460 RVA: 0x0028F9FC File Offset: 0x0028DBFC
		private NumberSourceList NumberInputs(IRow inputRow, bool coalesceZero)
		{
			Func<string, <>f__AnonymousType143<string, decimal?>> <>9__1;
			return this.CacheGetOrCompute<NumberSourceList>(new object[] { "NumberInputs", inputRow, coalesceZero }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				NumberSourceList numberSourceList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "NumberInputs", false, true) : null))
				{
					IEnumerable<string> enumerable = inputRow.ValidColumnNames();
					var func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string columnName) => new
						{
							columnName = columnName,
							number = Operators.FromNumber(inputRow, columnName, coalesceZero)
						});
					}
					NumberSourceList numberSourceList = new NumberSourceList(from <>h__TransparentIdentifier0 in enumerable.Select(func)
						where <>h__TransparentIdentifier0.number != null
						select new NumberSource(<>h__TransparentIdentifier0.columnName, <>h__TransparentIdentifier0.number.Value, NumberSourceKind.Input));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					numberSourceList2 = numberSourceList;
				}
				return numberSourceList2;
			});
		}

		// Token: 0x0600BD4D RID: 48461 RVA: 0x0028FA5C File Offset: 0x0028DC5C
		private NumberSourceList NumberParsed(IRow inputRow)
		{
			Func<StringInput, IEnumerable<FindNumberCacheItem>> <>9__1;
			return this.CacheGetOrCompute<NumberSourceList>(new object[] { "NumberParsed", inputRow }, delegate
			{
				LearnDebugTrace debugTrace = this._debugTrace;
				NumberSourceList numberSourceList2;
				using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "NumberParsed", false, true) : null))
				{
					IEnumerable<StringInput> enumerable = this.InputStrings(inputRow, null);
					Func<StringInput, IEnumerable<FindNumberCacheItem>> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (StringInput input) => this.FindNumbers(input.Value, false, new int?(10)));
					}
					NumberSourceList numberSourceList = new NumberSourceList(enumerable.SelectMany(func, (StringInput input, FindNumberCacheItem findResult) => new NumberSource(input.ColumnName, findResult.Value, NumberSourceKind.Parsed)));
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					numberSourceList2 = numberSourceList;
				}
				return numberSourceList2;
			});
		}

		// Token: 0x0600BD4E RID: 48462 RVA: 0x0028FAA8 File Offset: 0x0028DCA8
		public IReadOnlyList<FindNumberCacheItem> FindNumbers(string subject, bool fullMatch = false, int? take = 10)
		{
			object[] array = new object[] { "FindNumbers", subject, fullMatch, take };
			IReadOnlyList<FindNumberCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<FindNumberCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<FindNumberCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "FindNumbers", true, true) : null))
			{
				if (subject.ToUnicodeCategory().All((UnicodeCategory c) => c != UnicodeCategory.DecimalDigitNumber))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<FindNumberCacheItem>(array);
				}
				else if (this.HasClassification(subject, StringClassification.FormattedOther))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<FindNumberCacheItem>(array);
				}
				else
				{
					IEnumerable<FindNumberCacheItem> enumerable = from match in this.NumberParseRegex.NonCachingMatches(subject)
						where match.Success && (!fullMatch || match.IsFullMatch(subject))
						from culture in this.DataCultures
						let value = Operators.ParseNumber(match.Value, culture.Name)
						where value != null
						select new FindNumberCacheItem
						{
							StartIndex = match.Index,
							Substring = match.Value,
							Value = value.Value,
							Locale = culture.Name
						};
					if (take != null)
					{
						enumerable = enumerable.Take(take.Value);
					}
					readOnlyList = enumerable.ToList<FindNumberCacheItem>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<IReadOnlyList<FindNumberCacheItem>>(array, readOnlyList);
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD4F RID: 48463 RVA: 0x0028FCBC File Offset: 0x0028DEBC
		public IReadOnlyList<FormattedNumberCacheItem> ReverseFormattedNumber(string result)
		{
			object[] array = new object[] { "ReverseFormattedNumber", result };
			IReadOnlyList<FormattedNumberCacheItem> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<FormattedNumberCacheItem>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			if (string.IsNullOrEmpty(result))
			{
				return this.CacheSetEmptyList<FormattedNumberCacheItem>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<FormattedNumberCacheItem> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ReverseFormattedNumber", true, true) : null))
			{
				Match match = this.NumberFormattedRegex.Match(result);
				if (!match.IsFullMatch(result))
				{
					readOnlyList2 = this.CacheSetEmptyList<FormattedNumberCacheItem>(array);
				}
				else
				{
					string resultValue = match.Groups["value"].Value;
					readOnlyList = (from <>h__TransparentIdentifier0 in this.DataCultures.Select(delegate(CultureInfo culture)
						{
							decimal num;
							return new
							{
								culture = culture,
								value = (decimal.TryParse(resultValue, NumberStyles.Any, culture.NumberFormat, out num) ? new decimal?(num) : null)
							};
						})
						where value != null
						let rvalue = result.Any((char c) => this.NumberPercentSymbolChars.Contains(c)) ? (value.Value / 100m) : value.Value
						select new FormattedNumberCacheItem
						{
							Scale = rvalue.Scale(),
							Value = rvalue
						}).Distinct<FormattedNumberCacheItem>().ToList<FormattedNumberCacheItem>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<IReadOnlyList<FormattedNumberCacheItem>>(array, readOnlyList);
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD50 RID: 48464 RVA: 0x0028FE30 File Offset: 0x0028E030
		public IReadOnlyList<decimal> ReverseFormattedNumber(IRow inputRow, string result)
		{
			object[] array = new object[] { "ReverseFormattedNumber", inputRow, result };
			IReadOnlyList<decimal> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<decimal>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<decimal> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ReverseFormattedNumber/row", true, true) : null))
			{
				if (!this.IsFormattedNumber(result))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSetEmptyList<decimal>(array);
				}
				else
				{
					decimal[] array2 = new decimal[0];
					IReadOnlyList<FormattedNumberCacheItem> formatNumbers = this.ReverseFormattedNumber(result);
					IReadOnlyList<decimal> distinctValues = this.Numbers(inputRow, false).DistinctValues;
					IEnumerable<decimal> enumerable = (from inputNumber in distinctValues
						from formatNumber in formatNumbers
						where Math.Round(inputNumber, formatNumber.Scale) == formatNumber.Value
						select inputNumber).ToList<decimal>();
					IEnumerable<decimal> enumerable2;
					if (this._enableRoundNumber)
					{
						enumerable2 = from number in distinctValues
							from roundNumber in this.ToRoundNumbers(number)
							select roundNumber;
					}
					else
					{
						IEnumerable<decimal> enumerable3 = array2;
						enumerable2 = enumerable3;
					}
					IEnumerable<decimal> enumerable4 = enumerable2;
					IEnumerable<decimal> enumerable5;
					if (this._enableDateTimePart)
					{
						enumerable5 = from date in this.DateTimes(inputRow).DistinctValues
							from part in this.DecomposeDateTime(date)
							select part;
					}
					else
					{
						IEnumerable<decimal> enumerable3 = array2;
						enumerable5 = enumerable3;
					}
					IEnumerable<decimal> enumerable6 = enumerable5;
					IEnumerable<decimal> enumerable7;
					if (this._enableTimePart)
					{
						enumerable7 = from time in this.Times(inputRow)
							from part in this.DecomposeTime(time)
							select part;
					}
					else
					{
						IEnumerable<decimal> enumerable3 = array2;
						enumerable7 = enumerable3;
					}
					IEnumerable<decimal> enumerable8 = enumerable7;
					IEnumerable<decimal> enumerable9;
					if (this._enableArithmetic)
					{
						enumerable9 = this.FindArithmeticNumbers(inputRow, result);
					}
					else
					{
						IReadOnlyList<decimal> readOnlyList3 = array2;
						enumerable9 = readOnlyList3;
					}
					IEnumerable<decimal> enumerable10 = enumerable9;
					IEnumerable<decimal> enumerable11 = distinctValues.Union(enumerable).Union(enumerable4).Union(enumerable6)
						.Union(enumerable8)
						.Union(enumerable10);
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList2 = this.CacheSet<List<decimal>>(array, enumerable11.ToList<decimal>());
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD51 RID: 48465 RVA: 0x002900B4 File Offset: 0x0028E2B4
		public IEnumerable<decimal> ToRoundNumbers(decimal subject)
		{
			object[] array = new object[] { "ToRoundNumbers", subject };
			IEnumerable<decimal> enumerable;
			if (this.CacheTryGetValue<IEnumerable<decimal>>(array, out enumerable))
			{
				return enumerable;
			}
			if (this._outputAllZeros)
			{
				return this.CacheSetEmptyList<decimal>(array);
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<decimal> enumerable2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "ToRoundNumbers", false, true) : null))
			{
				enumerable = from desc in Recognition.RoundNumberDescriptors
					let roundNumber = Operators.RoundNumber(subject, desc)
					where roundNumber != null
					select roundNumber.Value;
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable2 = this.CacheSet<List<decimal>>(array, enumerable.ToList<decimal>());
			}
			return enumerable2;
		}

		// Token: 0x0600BD52 RID: 48466 RVA: 0x002901BC File Offset: 0x0028E3BC
		public bool TryFormatNumber(IRow inputRow, string result)
		{
			IEnumerable<FormatNumberDescriptor> enumerable;
			return this.TryFormatNumber(inputRow, result, out enumerable);
		}

		// Token: 0x0600BD53 RID: 48467 RVA: 0x002901D4 File Offset: 0x0028E3D4
		public bool TryFormatNumber(IRow inputRow, string result, out IEnumerable<FormatNumberDescriptor> descriptors)
		{
			object[] array = new object[] { "TryFormatNumber", inputRow, result };
			if (this.CacheTryGetValue<IEnumerable<FormatNumberDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<FormatNumberDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryFormatNumber/row", true, true) : null))
			{
				if (!this.IsFormattedNumber(result))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<FormatNumberDescriptor>(array).Any<FormatNumberDescriptor>();
				}
				else
				{
					descriptors = (from <>h__TransparentIdentifier0 in this.ReverseFormattedNumber(inputRow, result).Select(delegate(decimal number)
						{
							IEnumerable<FormatNumberDescriptor> enumerable;
							return new
							{
								number = number,
								formatDescriptors = (this.TryFormatNumber(number, result, out enumerable) ? enumerable : null)
							};
						})
						where <>h__TransparentIdentifier0.formatDescriptors != null
						from formatDescriptor in <>h__TransparentIdentifier0.formatDescriptors
						select formatDescriptor).Distinct<FormatNumberDescriptor>().ToReadOnlyList<FormatNumberDescriptor>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSet<IEnumerable<FormatNumberDescriptor>>(array, descriptors).Any<FormatNumberDescriptor>();
				}
			}
			return flag;
		}

		// Token: 0x0600BD54 RID: 48468 RVA: 0x00290334 File Offset: 0x0028E534
		public bool TryFormatNumber(decimal subject, string result)
		{
			IEnumerable<FormatNumberDescriptor> enumerable;
			return this.TryFormatNumber(subject, result, out enumerable);
		}

		// Token: 0x0600BD55 RID: 48469 RVA: 0x0029034C File Offset: 0x0028E54C
		public bool TryFormatNumber(decimal subject, string result, out IEnumerable<FormatNumberDescriptor> descriptors)
		{
			object[] array = new object[] { "TryFormatNumber", subject, result };
			if (this.CacheTryGetValue<IEnumerable<FormatNumberDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<FormatNumberDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryFormatNumber", true, true) : null))
			{
				IReadOnlyList<FormatNumberDescriptor> readOnlyList;
				if (!this.IsFormattedNumber(result.AsSpan(), out readOnlyList))
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<FormatNumberDescriptor>(array).Any<FormatNumberDescriptor>();
				}
				else
				{
					descriptors = readOnlyList.Where((FormatNumberDescriptor descriptor) => Operators.FormatNumber(subject, descriptor) == result).ToList<FormatNumberDescriptor>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSet<IEnumerable<FormatNumberDescriptor>>(array, descriptors).Any<FormatNumberDescriptor>();
				}
			}
			return flag;
		}

		// Token: 0x0600BD56 RID: 48470 RVA: 0x00290444 File Offset: 0x0028E644
		public bool TryRoundNumber(decimal source, decimal result)
		{
			IEnumerable<RoundNumberCacheItem> enumerable;
			return this.TryRoundNumber(source, result, out enumerable);
		}

		// Token: 0x0600BD57 RID: 48471 RVA: 0x0029045C File Offset: 0x0028E65C
		public bool TryRoundNumber(decimal source, decimal result, out IEnumerable<RoundNumberCacheItem> cacheItems)
		{
			object[] array = new object[] { "TryRoundNumber", source, result };
			if (this.CacheTryGetValue<IEnumerable<RoundNumberCacheItem>>(array, out cacheItems))
			{
				IEnumerable<RoundNumberCacheItem> enumerable = cacheItems;
				return enumerable != null && enumerable.Any<RoundNumberCacheItem>();
			}
			if (this._outputAllZeros)
			{
				cacheItems = this.CacheSetEmptyList<RoundNumberCacheItem>(array);
				return false;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "TryRoundNumber", false, true) : null))
			{
				cacheItems = (from descriptor in Recognition.RoundNumberDescriptors.Where(delegate(RoundNumberDescriptor descriptor)
					{
						decimal? num = Operators.RoundNumber(source, descriptor);
						decimal result2 = result;
						return (num.GetValueOrDefault() == result2) & (num != null);
					})
					select new RoundNumberCacheItem
					{
						Subject = source,
						Result = result,
						Descriptor = descriptor
					}).ToList<RoundNumberCacheItem>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				IEnumerable<RoundNumberCacheItem> enumerable2 = this.CacheSet<IEnumerable<RoundNumberCacheItem>>(array, cacheItems);
				flag = enumerable2 != null && enumerable2.Any<RoundNumberCacheItem>();
			}
			return flag;
		}

		// Token: 0x0600BD58 RID: 48472 RVA: 0x0029055C File Offset: 0x0028E75C
		public unsafe ReadOnlySpan<char> TakeWhileNumberChar(ReadOnlySpan<char> subject)
		{
			int i;
			for (i = 0; i < subject.Length; i++)
			{
				char c = (char)(*subject[i]);
				if (!this.NumberChars.Contains(c) || (c == ' ' && (!(i + 1).IsValidIndex(subject) || (!this.NumberCurrencySymbolChars.Contains((char)(*subject[i + 1])) && !this.NumberPercentSymbolChars.Contains((char)(*subject[i + 1]))))))
				{
					break;
				}
			}
			return subject.Take(i);
		}

		// Token: 0x0600BD59 RID: 48473 RVA: 0x002905DC File Offset: 0x0028E7DC
		public IReadOnlyList<FormatNumberDescriptor> LoadFormatNumberDescriptors()
		{
			return (from <>h__TransparentIdentifier0 in this.DataCultures.Select(delegate(CultureInfo culture)
				{
					IReadOnlyList<FormatNumberDescriptor> readOnlyList;
					return new
					{
						culture = culture,
						descriptors = (Recognition._numberFormatDescriptorCache.Lookup(culture.Name, out readOnlyList) ? readOnlyList : this.LoadNumberDescriptors(culture))
					};
				})
				from descriptor in <>h__TransparentIdentifier0.descriptors
				select descriptor).ToList<FormatNumberDescriptor>();
		}

		// Token: 0x0600BD5A RID: 48474 RVA: 0x00290648 File Offset: 0x0028E848
		public IReadOnlyList<FormatNumberDescriptor> LoadNumberDescriptors(CultureInfo sourceCulture)
		{
			IEnumerable<CultureInfo> enumerable = new CultureInfo[] { sourceCulture };
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<FormatNumberDescriptor> readOnlyList2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Startup", "LoadNumberDescriptors/" + sourceCulture.Name, false, true) : null))
			{
				IReadOnlyList<FormatNumberDescriptor> readOnlyList = (from culture in enumerable
					let isGlobalCulture = culture.Name != "en-US"
					let currencySymbol = culture.NumberFormat.CurrencySymbol
					let currencyDecimalDigits = culture.NumberFormat.CurrencyDecimalDigits
					let percentDecimalDigits = culture.NumberFormat.PercentDecimalDigits
					let percentSymbol = culture.NumberFormat.PercentSymbol
					from symbolInfo in new <>f__AnonymousType155<string, bool, bool, bool>[]
					{
						new
						{
							Symbol = null,
							Prefix = true,
							IsCurrency = false,
							IsPercent = false
						},
						isGlobalCulture ? new
						{
							Symbol = " " + currencySymbol,
							Prefix = false,
							IsCurrency = true,
							IsPercent = false
						} : null,
						new
						{
							Symbol = currencySymbol,
							Prefix = true,
							IsCurrency = true,
							IsPercent = false
						},
						new
						{
							Symbol = " " + percentSymbol,
							Prefix = false,
							IsCurrency = false,
							IsPercent = true
						},
						new
						{
							Symbol = percentSymbol,
							Prefix = false,
							IsCurrency = false,
							IsPercent = true
						}
					}.Collect()
					select new { <>h__TransparentIdentifier4, symbolInfo }).SelectMany(delegate(<>h__TransparentIdentifier5)
				{
					bool[] array = new bool[2];
					array[0] = true;
					return array;
				}, (<>h__TransparentIdentifier5, bool includeGroup) => new { <>h__TransparentIdentifier5, includeGroup }).SelectMany(delegate(<>h__TransparentIdentifier6)
				{
					if (!<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.IsCurrency)
					{
						return Enumerable.Range(1, this._numberFormatMaxLeadingDigits);
					}
					return new int[] { 1 };
				}, (<>h__TransparentIdentifier6, int leadingDigit) => new { <>h__TransparentIdentifier6, leadingDigit }).SelectMany(delegate(<>h__TransparentIdentifier7)
				{
					if (!<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.IsCurrency)
					{
						return Enumerable.Range(0, 8);
					}
					return new int[]
					{
						0,
						2,
						<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.currencyDecimalDigits
					}.Distinct<int>();
				}, (<>h__TransparentIdentifier7, int trailingDigit) => new FormatNumberDescriptor
				{
					Locale = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.culture.Name,
					Symbol = ((<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.Symbol == null) ? null : new FormatNumberSymbolDescriptor(<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.Symbol, <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.Prefix, <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.IsCurrency, <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.symbolInfo.IsPercent)),
					IncludeGroupSeparator = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.includeGroup,
					LeadingDigits = <>h__TransparentIdentifier7.leadingDigit,
					TrailingDigits = trailingDigit
				})
					.ToReadOnlyList<FormatNumberDescriptor>();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				readOnlyList2 = readOnlyList;
			}
			return readOnlyList2;
		}

		// Token: 0x0600BD5B RID: 48475 RVA: 0x00290870 File Offset: 0x0028EA70
		public IReadOnlyList<FormatNumberDescriptor> LoadNumberFormatDescriptors()
		{
			return (from <>h__TransparentIdentifier0 in this.DataCultures.Select(delegate(CultureInfo culture)
				{
					IReadOnlyList<FormatNumberDescriptor> readOnlyList;
					return new
					{
						culture = culture,
						descriptors = (Recognition._numberFormatDescriptorCache.Lookup(culture.Name, out readOnlyList) ? readOnlyList : this.LoadNumberDescriptors(culture))
					};
				})
				from descriptor in <>h__TransparentIdentifier0.descriptors
				select descriptor).ToList<FormatNumberDescriptor>();
		}

		// Token: 0x0600BD5C RID: 48476 RVA: 0x002908DC File Offset: 0x0028EADC
		private Regex LoadFormattedNumberRegex(bool capture)
		{
			string text = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NegativeSign).Concat(this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.PositiveSign))
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text2 = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberGroupSeparator)
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text3 = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberDecimalSeparator)
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text4 = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.PercentSymbol)
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text5 = (capture ? "?<preSymbol>" : ":?");
			string text6 = (capture ? "?<postSymbol>" : ":?");
			string text7 = (capture ? "?<value>" : ":?");
			return string.Concat(new string[]
			{
				"(?<![\\p{N}])(", text5, "[\\p{Sc}]\\p{Zs}?)?(", text7, "[", text, "]?\\p{N}[", text2, "\\p{N}]*(?:[", text3,
				"]\\p{N}+)?)(", text6, "\\p{Zs}?[\\p{Sc}", text4, "])?(?!\\p{N})"
			}).ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600BD5D RID: 48477 RVA: 0x00290B2C File Offset: 0x0028ED2C
		private Regex LoadNumberParseRegex(bool capture)
		{
			string text = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NegativeSign).Concat(this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.PositiveSign))
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text2 = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberGroupSeparator)
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text3 = (from d in this.DataCultures.SelectMany((CultureInfo d) => d.NumberFormat.NumberDecimalSeparator)
				select Regex.Escape(d.ToString())).Distinct<string>().ToJoinString();
			string text4 = (capture ? "?<value>" : ":?");
			return string.Concat(new string[] { "(?<![\\p{N}])(", text4, "[", text, "]?\\p{N}[", text2, "\\p{N}]*(?:[", text3, "]\\p{N}+)?)(?!\\p{N})" }).ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600BD5E RID: 48478 RVA: 0x00290CD4 File Offset: 0x0028EED4
		private static IReadOnlyList<RoundNumberDescriptor> LoadRoundNumberDescriptors()
		{
			return (from mode in new RoundingMode[]
				{
					RoundingMode.Nearest,
					RoundingMode.Up,
					RoundingMode.Down
				}
				from delta in new double[]
				{
					100000.0, 10000.0, 5000.0, 2000.0, 1500.0, 1000.0, 100.0, 50.0, 20.0, 10.0,
					5.0, 2.0, 1.0, 0.5, 0.1, 0.05, 0.01
				}
				select new RoundNumberDescriptor
				{
					Mode = mode,
					Delta = delta
				}).ToList<RoundNumberDescriptor>();
		}

		// Token: 0x0600BD5F RID: 48479 RVA: 0x00290D38 File Offset: 0x0028EF38
		public IEnumerable<string> UniqueSubstrings(string left, string right)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IEnumerable<string> enumerable;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition", "UniqueSubstrings/Util", false, true) : null))
			{
				HashSet<Substring> hashSet = new HashSet<Substring>(Substring.ValueEquality);
				Substring substring = new Substring(left);
				Substring substring2 = new Substring(right);
				int num = 0;
				while (substring.Length > 0U && substring2.Length > 0U)
				{
					if (num++ > 500)
					{
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						return Utils.Empty<string>();
					}
					Substring substring3;
					Substring substring4;
					if (!Recognition.TryGetMaxCommonSubstring(substring, substring2, out substring3, out substring4))
					{
						break;
					}
					if (substring3.Start != substring.Start)
					{
						hashSet.Add(new Substring(left, substring.Start, substring3.Start));
					}
					substring = new Substring(left, substring3.End, substring.End);
					substring2 = new Substring(right, substring4.End, substring2.End);
				}
				if (substring.Length > 0U)
				{
					hashSet.Add(substring);
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				enumerable = hashSet.Select((Substring ss) => ss.Value);
			}
			return enumerable;
		}

		// Token: 0x0600BD60 RID: 48480 RVA: 0x00290E7C File Offset: 0x0028F07C
		private static bool TryGetMaxCommonSubstring(Substring left, Substring right, out Substring leftCommon, out Substring rightCommon)
		{
			uint[,] array = new uint[(int)left.Length, (int)right.Length];
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			for (uint num4 = 0U; num4 < left.Length; num4 += 1U)
			{
				for (uint num5 = 0U; num5 < right.Length; num5 += 1U)
				{
					if (left[num4] == right[num5])
					{
						array[(int)num4, (int)num5] = ((num4 == 0U || num5 == 0U) ? 1U : (array[(int)(num4 - 1U), (int)(num5 - 1U)] + 1U));
						if (array[(int)num4, (int)num5] > num)
						{
							num = array[(int)num4, (int)num5];
							num2 = num4;
							num3 = num5;
						}
					}
					else
					{
						array[(int)num4, (int)num5] = 0U;
					}
				}
			}
			if (num > 0U)
			{
				uint num6 = left.Start + num2 - num + 1U;
				leftCommon = new Substring(left.Source, num6, num6 + num);
				uint num7 = right.Start + num3 - num + 1U;
				rightCommon = new Substring(right.Source, num7, num7 + num);
				return true;
			}
			Substring substring;
			rightCommon = (substring = null);
			leftCommon = substring;
			return false;
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x0600BD61 RID: 48481 RVA: 0x00290F84 File Offset: 0x0028F184
		public int[] FindOffsetRange
		{
			get
			{
				int[] array;
				if ((array = this._delimiterIndexRange) == null)
				{
					array = (this._delimiterIndexRange = Utils.Range(-3, 5).ToArray<int>());
				}
				return array;
			}
		}

		// Token: 0x0600BD62 RID: 48482 RVA: 0x00290FB4 File Offset: 0x0028F1B4
		public bool TryFind(string subject, int index)
		{
			IReadOnlyList<FindDescriptor> readOnlyList;
			return this.TryFind(subject, index, out readOnlyList);
		}

		// Token: 0x0600BD63 RID: 48483 RVA: 0x00290FCB File Offset: 0x0028F1CB
		public bool TryFind(string subject, int index, out IReadOnlyList<FindDescriptor> spliceDescriptors)
		{
			spliceDescriptors = this.ParseFindDescriptors(subject, index);
			IReadOnlyList<FindDescriptor> readOnlyList = spliceDescriptors;
			return readOnlyList != null && readOnlyList.Any<FindDescriptor>();
		}

		// Token: 0x0600BD64 RID: 48484 RVA: 0x00290FE4 File Offset: 0x0028F1E4
		public IReadOnlyList<FindDescriptor> ParseFindDescriptors(string subject, int resultIndex)
		{
			object[] array = new object[] { "ParseFindDescriptors", subject, resultIndex };
			IReadOnlyList<FindDescriptor> readOnlyList;
			if (this.CacheTryGetValue<IReadOnlyList<FindDescriptor>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			IEnumerable<FindDescriptor> enumerable = from offset in Enumerable.Range(-3, 7)
				let i = resultIndex + offset
				where i.IsValidIndex(subject) && subject[i].IsDelimiter()
				select new FindDescriptor
				{
					Delimiter = subject[i].ToString(),
					DelimiterIndex = i
				};
			return this.CacheSet<List<FindDescriptor>>(array, enumerable.ToList<FindDescriptor>());
		}

		// Token: 0x0600BD65 RID: 48485 RVA: 0x00291084 File Offset: 0x0028F284
		public IEnumerable<int> ExpandInstance(string subject, string delimiter, int index)
		{
			IReadOnlyList<int> readOnlyList = subject.AllIndexesOf(delimiter, StringComparison.Ordinal).ToArray<int>();
			if (!readOnlyList.Any<int>())
			{
				return new int[0];
			}
			int rightEdgeIndex = index + delimiter.Length;
			int num = readOnlyList.Count((int i) => i < rightEdgeIndex);
			if (!this._enableNegativePosition)
			{
				return new int[] { num };
			}
			int num2 = -(readOnlyList.Count - num + 1);
			return new int[] { num, num2 };
		}

		// Token: 0x0600BD66 RID: 48486 RVA: 0x00291104 File Offset: 0x0028F304
		public IEnumerable<int> ExpandPosition(string subject, int position)
		{
			if (position <= 0)
			{
				return new int[0];
			}
			if (position > subject.Length)
			{
				return new int[] { position };
			}
			if (!this._enableNegativePosition)
			{
				return new int[] { position };
			}
			return new int[]
			{
				position,
				-(subject.Length - position + 1)
			};
		}

		// Token: 0x0600BD67 RID: 48487 RVA: 0x0029115B File Offset: 0x0028F35B
		public string ResolveSliceBetweenEndText(string subject, int referenceIndex)
		{
			return Recognition.ResolveSliceBetweenText(subject, referenceIndex, false);
		}

		// Token: 0x0600BD68 RID: 48488 RVA: 0x00291165 File Offset: 0x0028F365
		public string ResolveSliceBetweenStartText(string subject, int referenceIndex)
		{
			return Recognition.ResolveSliceBetweenText(subject, referenceIndex, true);
		}

		// Token: 0x0600BD69 RID: 48489 RVA: 0x00291170 File Offset: 0x0028F370
		private static string ResolveSliceBetweenText(string subject, int referenceIndex, bool reverse)
		{
			Recognition.<>c__DisplayClass227_0 CS$<>8__locals1;
			CS$<>8__locals1.subject = subject;
			CS$<>8__locals1.increment = (reverse ? (-1) : 1);
			CS$<>8__locals1.index = (reverse ? (referenceIndex - 1) : referenceIndex);
			char? c = null;
			while (0 <= CS$<>8__locals1.index && CS$<>8__locals1.index < CS$<>8__locals1.subject.Length)
			{
				char c2 = CS$<>8__locals1.subject[CS$<>8__locals1.index];
				char? c3 = Recognition.<ResolveSliceBetweenText>g__Peek|227_0(ref CS$<>8__locals1);
				bool flag = c2 == ' ' && c != null && c3 != null && c.IsDelimiter() && c3.IsDelimiter() && !Recognition._sliceBetweenTextStopChars.Contains(c3.Value);
				if (c3 == null || (!flag && Recognition._sliceBetweenTextStopChars.Contains(c2)))
				{
					break;
				}
				c = new char?(c2);
				CS$<>8__locals1.index += CS$<>8__locals1.increment;
			}
			if (CS$<>8__locals1.index == referenceIndex)
			{
				return null;
			}
			if (!reverse)
			{
				return CS$<>8__locals1.subject.Slice(new int?(referenceIndex), new int?(CS$<>8__locals1.index), 1);
			}
			return CS$<>8__locals1.subject.Slice(new int?(CS$<>8__locals1.index + 1), new int?(referenceIndex), 1);
		}

		// Token: 0x0600BD6A RID: 48490 RVA: 0x002912B0 File Offset: 0x0028F4B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TrySubstring(IRow inputRow, string substring, out IReadOnlyList<SubstringDescriptor> descriptors, bool ignoreCase = false)
		{
			object[] array = new object[] { "TrySubstring", inputRow, substring, ignoreCase };
			if (this.CacheTryGetValue<IReadOnlyList<SubstringDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<SubstringDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "TrySubstring/row", true, true) : null))
			{
				if (substring.Length == 0)
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<SubstringDescriptor>(array).Any<SubstringDescriptor>();
				}
				else
				{
					StringInputList stringInputList = this.InputStrings(inputRow, null);
					if (stringInputList.None<StringInput>())
					{
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						flag = this.CacheSetEmptyList<SubstringDescriptor>(array).Any<SubstringDescriptor>();
					}
					else
					{
						List<SubstringDescriptor> list = new List<SubstringDescriptor>();
						foreach (StringInput stringInput in stringInputList)
						{
							IReadOnlyList<SubstringDescriptor> readOnlyList;
							if (this.TrySubstring(stringInput.Value, substring, out readOnlyList, ignoreCase))
							{
								foreach (SubstringDescriptor substringDescriptor in readOnlyList)
								{
									substringDescriptor.InputColumn = stringInput;
									list.Add(substringDescriptor);
								}
							}
						}
						descriptors = list;
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						flag = this.CacheSet<IReadOnlyList<SubstringDescriptor>>(array, descriptors).Any<SubstringDescriptor>();
					}
				}
			}
			return flag;
		}

		// Token: 0x0600BD6B RID: 48491 RVA: 0x00291434 File Offset: 0x0028F634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TrySubstring(string input, string output, out IReadOnlyList<SubstringDescriptor> descriptors, bool ignoreCase = false)
		{
			object[] array = new object[] { "TrySubstring", input, output, ignoreCase };
			if (this.CacheTryGetValue<IReadOnlyList<SubstringDescriptor>>(array, out descriptors))
			{
				return descriptors.Any<SubstringDescriptor>();
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "TrySubstring", true, true) : null))
			{
				if (input.None<char>() || output.None<char>() || output.Length > input.Length)
				{
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSetEmptyList<SubstringDescriptor>(array).Any<SubstringDescriptor>();
				}
				else
				{
					string text = input;
					string text2 = output;
					if (ignoreCase)
					{
						input = input.ToLowerInvariant();
						output = output.ToLowerInvariant();
					}
					ReadOnlySpan<char> readOnlySpan = input.AsSpan();
					ReadOnlySpan<char> readOnlySpan2 = output.AsSpan();
					int i = 0;
					int num = 0;
					List<SubstringDescriptor> list = new List<SubstringDescriptor>();
					while (i < input.Length)
					{
						if (num++ > 1000)
						{
							throw new Exception(string.Format("Exceeded maximum substrings ({0:N0}).", 1000));
						}
						int num2 = readOnlySpan.Slice(i).IndexOf(readOnlySpan2);
						if (num2 == -1)
						{
							break;
						}
						list.Add(new SubstringDescriptor
						{
							Input = text,
							Output = text2,
							StartIndex = i + num2
						});
						i += num2 + output.Length + 1;
					}
					descriptors = list;
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					flag = this.CacheSet<IReadOnlyList<SubstringDescriptor>>(array, descriptors).Any<SubstringDescriptor>();
				}
			}
			return flag;
		}

		// Token: 0x0600BD6C RID: 48492 RVA: 0x002915CC File Offset: 0x0028F7CC
		public bool Contains(IRow inputRow, ReadOnlySpan<char> substring, bool ignoreCase = false)
		{
			object[] array = new object[]
			{
				"Contains",
				inputRow,
				substring.ToString(),
				ignoreCase
			};
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag2;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "Contains", true, true) : null)
			{
				StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
				if (substring.Length == 0)
				{
					flag2 = this.CacheSet<bool>(array, false);
				}
				else
				{
					flag = false;
					foreach (StringInput stringInput in this.InputStrings(inputRow, null))
					{
						string value = stringInput.Value;
						flag = value != null && value.AsSpan().IndexOf(substring, stringComparison) >= 0;
						if (flag)
						{
							break;
						}
					}
					flag2 = this.CacheSet<bool>(array, flag);
				}
			}
			return flag2;
		}

		// Token: 0x0600BD6D RID: 48493 RVA: 0x002916D8 File Offset: 0x0028F8D8
		public bool Contains(IRow inputRow, string substring, bool ignoreCase = false)
		{
			object[] array = new object[] { "Contains", inputRow, substring, ignoreCase };
			bool flag;
			if (this.CacheTryGetValue<bool>(array, out flag))
			{
				return flag;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			bool flag2;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("Recognition/Util", "Contains", true, true) : null)
			{
				StringComparison stringComparison = (ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
				if (string.IsNullOrEmpty(substring))
				{
					flag2 = this.CacheSet<bool>(array, false);
				}
				else
				{
					flag = false;
					foreach (StringInput stringInput in this.InputStrings(inputRow, null))
					{
						string value = stringInput.Value;
						flag = value != null && value.IndexOf(substring, stringComparison) >= 0;
						if (flag)
						{
							break;
						}
					}
					flag2 = this.CacheSet<bool>(array, flag);
				}
			}
			return flag2;
		}

		// Token: 0x0600BD6E RID: 48494 RVA: 0x002917D4 File Offset: 0x0028F9D4
		public bool Contains(IRow inputRow, IEnumerable<string> substrings, bool ignoreCase = false)
		{
			return substrings.Any((string s) => this.Contains(inputRow, s, ignoreCase));
		}

		// Token: 0x0600BD72 RID: 48498 RVA: 0x002918AC File Offset: 0x0028FAAC
		[CompilerGenerated]
		internal static decimal? <TryMultiply>g__ComputeConstantRight|31_0(decimal localLeft, decimal localResult)
		{
			if (!(localLeft == 0m))
			{
				return new decimal?((localResult.Normalize() / localLeft.Normalize()).Normalize());
			}
			return null;
		}

		// Token: 0x0600BD73 RID: 48499 RVA: 0x002918EC File Offset: 0x0028FAEC
		[CompilerGenerated]
		internal static decimal? <TryDivide>g__ComputeConstantRight|33_0(decimal localLeft, decimal localResult)
		{
			if (!(localResult == 0m))
			{
				return new decimal?((localLeft.Normalize() / localResult.Normalize()).Normalize());
			}
			return null;
		}

		// Token: 0x0600BD74 RID: 48500 RVA: 0x0029192C File Offset: 0x0028FB2C
		[CompilerGenerated]
		internal static bool <TryDivide>g__ValidateNormalized|33_1(decimal localLeft, decimal localRight, decimal localResult)
		{
			decimal? num = Operators.Divide(localLeft.Normalize(), localRight.Normalize());
			return num != null && ((num.Value.Scale() > 8) ? num.Value.Truncate(localResult.Scale()) : num.Value) == localResult;
		}

		// Token: 0x0600BD78 RID: 48504 RVA: 0x00291A10 File Offset: 0x0028FC10
		[CompilerGenerated]
		internal static IEnumerable<string> <LoadDateTimeDescriptors>g__ResolveTimeVariants|140_36(CultureInfo culture)
		{
			if (string.IsNullOrEmpty(culture.DateTimeFormat.AMDesignator) || string.IsNullOrEmpty(culture.DateTimeFormat.PMDesignator))
			{
				return new string[] { "", " HH:mm", " HH:mm:ss" };
			}
			return new string[] { "", " h:mm tt", " h:mm:ss tt", " h:mm", " H:mm", " HH:mm", " HH:mm:ss" };
		}

		// Token: 0x0600BD80 RID: 48512 RVA: 0x00291BF0 File Offset: 0x0028FDF0
		[CompilerGenerated]
		internal static char? <ResolveSliceBetweenText>g__Peek|227_0(ref Recognition.<>c__DisplayClass227_0 A_0)
		{
			int num = A_0.index + A_0.increment;
			if (0 <= num && num < A_0.subject.Length)
			{
				return new char?(A_0.subject[num]);
			}
			return null;
		}

		// Token: 0x040047AB RID: 18347
		private static Regex _classifyPhoneNumberRegex;

		// Token: 0x040047AC RID: 18348
		private static Regex _classifySocialSecurityNumberRegex;

		// Token: 0x040047AD RID: 18349
		private readonly HashSet<string> _allZeroColumns;

		// Token: 0x040047AE RID: 18350
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040047AF RID: 18351
		private readonly IReadOnlyList<string> _columnNamePriority;

		// Token: 0x040047B0 RID: 18352
		private readonly DateTimeSourceKind _dateTimeSourceKinds;

		// Token: 0x040047B1 RID: 18353
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x040047B2 RID: 18354
		private readonly bool _enableArithmetic;

		// Token: 0x040047B3 RID: 18355
		private readonly bool _enableArithmeticConstants;

		// Token: 0x040047B4 RID: 18356
		private readonly bool _enableDateTimePart;

		// Token: 0x040047B5 RID: 18357
		private readonly bool _enableFromNumberCoalesceZero;

		// Token: 0x040047B6 RID: 18358
		private readonly bool _enableFromNumberStr;

		// Token: 0x040047B7 RID: 18359
		private readonly MatchName _enableMatchNames;

		// Token: 0x040047B8 RID: 18360
		private readonly bool _enableMatchUnicode;

		// Token: 0x040047B9 RID: 18361
		private readonly bool _enableNegativePosition;

		// Token: 0x040047BA RID: 18362
		private readonly bool _enableRoundDateTime;

		// Token: 0x040047BB RID: 18363
		private readonly bool _enableRoundNumber;

		// Token: 0x040047BC RID: 18364
		private readonly bool _enableTimePart;

		// Token: 0x040047BD RID: 18365
		private readonly int _forwardFillMaxScale;

		// Token: 0x040047BE RID: 18366
		private readonly int _fromNumbersColumnLimit;

		// Token: 0x040047BF RID: 18367
		private readonly int _numberFormatMaxLeadingDigits;

		// Token: 0x040047C0 RID: 18368
		private readonly NumberSourceKind _numberSourceKinds;

		// Token: 0x040047C1 RID: 18369
		private readonly bool _outputAllZeros;

		// Token: 0x040047C5 RID: 18373
		private HashSet<char> _dateTimeAllowedDelimiters;

		// Token: 0x040047C6 RID: 18374
		private HashSet<char> _dateTimeChars;

		// Token: 0x040047C7 RID: 18375
		private static readonly ConcurrentLruCache<string, IReadOnlyList<DateTimeDescriptor>> _dateTimeDescriptorCache = new ConcurrentLruCache<string, IReadOnlyList<DateTimeDescriptor>>(4096, null, null, null);

		// Token: 0x040047C8 RID: 18376
		private IReadOnlyList<DateTimeDescriptor> _dateTimeFormattedDescriptors;

		// Token: 0x040047C9 RID: 18377
		private IReadOnlyList<DateTimeDescriptor> _dateTimeParseDescriptors;

		// Token: 0x040047CA RID: 18378
		private static readonly IReadOnlyList<DateTimePartKind> _dateTimePartKinds = EnumUtils.GetValues<DateTimePartKind>().ToList<DateTimePartKind>();

		// Token: 0x040047CB RID: 18379
		private static readonly IReadOnlyList<RoundDateTimeDescriptor> _roundDateTimeDescriptors = Recognition.LoadRoundDateTimeDescriptors();

		// Token: 0x040047CC RID: 18380
		private static readonly IReadOnlyList<TimePartKind> _timePartKinds = EnumUtils.GetValues<TimePartKind>().ToList<TimePartKind>();

		// Token: 0x040047CD RID: 18381
		private HashSet<char> _numberChars;

		// Token: 0x040047CE RID: 18382
		private HashSet<char> _numberCurrencySymbolChars;

		// Token: 0x040047CF RID: 18383
		private HashSet<char> _numberDecimalSeparatorChars;

		// Token: 0x040047D0 RID: 18384
		private char[] _numberDecimalSeparatorCharsArray;

		// Token: 0x040047D1 RID: 18385
		private HashSet<char> _numberDigitChars;

		// Token: 0x040047D2 RID: 18386
		private static readonly ConcurrentLruCache<string, IReadOnlyList<FormatNumberDescriptor>> _numberFormatDescriptorCache = new ConcurrentLruCache<string, IReadOnlyList<FormatNumberDescriptor>>(4096, null, null, null);

		// Token: 0x040047D3 RID: 18387
		private IReadOnlyList<FormatNumberDescriptor> _numberFormatDescriptors;

		// Token: 0x040047D4 RID: 18388
		private Regex _numberFormattedRegex;

		// Token: 0x040047D5 RID: 18389
		private HashSet<char> _numberGroupSeparatorChars;

		// Token: 0x040047D6 RID: 18390
		private int? _numberMinGroupSize;

		// Token: 0x040047D7 RID: 18391
		private Regex _numberParseRegex;

		// Token: 0x040047D8 RID: 18392
		private HashSet<char> _numberPercentSymbolChars;

		// Token: 0x040047D9 RID: 18393
		private HashSet<char> _numberSignChars;

		// Token: 0x040047DA RID: 18394
		private static IReadOnlyList<RoundNumberDescriptor> _roundNumberDescriptors;

		// Token: 0x040047DB RID: 18395
		private int[] _delimiterIndexRange;

		// Token: 0x040047DC RID: 18396
		private static readonly HashSet<char> _sliceBetweenTextStopChars = new char[]
		{
			' ', ',', '|', '{', '}', '<', '>', '[', ']', '(',
			')'
		}.ConvertToHashSet<char>();

		// Token: 0x0200161A RID: 5658
		private enum FindDateTimeKind
		{
			// Token: 0x040047DE RID: 18398
			Formatted,
			// Token: 0x040047DF RID: 18399
			Parseable
		}
	}
}
