using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000681 RID: 1665
	internal sealed class Filters
	{
		// Token: 0x06005B28 RID: 23336 RVA: 0x00176A15 File Offset: 0x00174C15
		internal Filters(Filters.FilterTypes filterType, ReportProcessing.IFilterOwner owner, FilterList filters, ObjectType objectType, string objectName, ReportProcessing.ProcessingContext processingContext)
		{
			this.m_filterType = filterType;
			this.m_owner = owner;
			this.m_filters = filters;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_processingContext = processingContext;
		}

		// Token: 0x17002051 RID: 8273
		// (set) Token: 0x06005B29 RID: 23337 RVA: 0x00176A51 File Offset: 0x00174C51
		internal bool FailFilters
		{
			set
			{
				this.m_failFilters = value;
			}
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x00176A5C File Offset: 0x00174C5C
		internal bool PassFilters(object dataInstance)
		{
			bool flag;
			return this.PassFilters(dataInstance, out flag);
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x00176A74 File Offset: 0x00174C74
		private void ThrowIfErrorOccurred(string propertyName, bool errorOccurred, DataFieldStatus fieldStatus)
		{
			if (!errorOccurred)
			{
				return;
			}
			if (fieldStatus != DataFieldStatus.None)
			{
				throw new ReportProcessingException(ErrorCode.rsFilterFieldError, new object[]
				{
					this.m_objectType,
					this.m_objectName,
					propertyName,
					ReportRuntime.GetErrorName(fieldStatus, null)
				});
			}
			throw new ReportProcessingException(ErrorCode.rsFilterEvaluationError, new object[] { this.m_objectType, this.m_objectName, propertyName });
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x00176AEC File Offset: 0x00174CEC
		internal bool PassFilters(object dataInstance, out bool specialFilter)
		{
			bool flag = true;
			specialFilter = false;
			if (this.m_failFilters)
			{
				return false;
			}
			if (this.m_filters != null)
			{
				for (int i = this.m_startFilterIndex; i < this.m_filters.Count; i++)
				{
					Filter filter = this.m_filters[i];
					if (Filter.Operators.Like == filter.Operator)
					{
						StringResult stringResult = this.m_processingContext.ReportRuntime.EvaluateFilterStringExpression(filter, this.m_objectType, this.m_objectName);
						this.ThrowIfErrorOccurred("FilterExpression", stringResult.ErrorOccurred, stringResult.FieldStatus);
						Global.Tracer.Assert(filter.Values != null);
						Global.Tracer.Assert(1 <= filter.Values.Count);
						StringResult stringResult2 = this.m_processingContext.ReportRuntime.EvaluateFilterStringValue(filter, 0, this.m_objectType, this.m_objectName);
						this.ThrowIfErrorOccurred("FilterValue", stringResult2.ErrorOccurred, stringResult2.FieldStatus);
						if (stringResult.Value != null && stringResult2.Value != null)
						{
							if (!StringType.StrLikeText(stringResult.Value, stringResult2.Value))
							{
								flag = false;
							}
						}
						else if (stringResult.Value != null || stringResult2.Value != null)
						{
							flag = false;
						}
					}
					else
					{
						VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantExpression(filter, this.m_objectType, this.m_objectName);
						this.ThrowIfErrorOccurred("FilterExpression", variantResult.ErrorOccurred, variantResult.FieldStatus);
						object value = variantResult.Value;
						if (filter.Operator == Filter.Operators.Equal || Filter.Operators.NotEqual == filter.Operator || Filter.Operators.GreaterThan == filter.Operator || Filter.Operators.GreaterThanOrEqual == filter.Operator || Filter.Operators.LessThan == filter.Operator || Filter.Operators.LessThanOrEqual == filter.Operator)
						{
							object obj = this.EvaluateFilterValue(filter);
							int num = 0;
							try
							{
								num = ReportProcessing.CompareTo(value, obj, this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions);
							}
							catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError)
							{
								throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError));
							}
							catch
							{
								throw new ReportProcessingException(this.RegisterComparisonError());
							}
							if (flag)
							{
								Filter.Operators @operator = filter.Operator;
								switch (@operator)
								{
								case Filter.Operators.Equal:
									if (num != 0)
									{
										flag = false;
									}
									break;
								case Filter.Operators.Like:
									break;
								case Filter.Operators.GreaterThan:
									if (0 >= num)
									{
										flag = false;
									}
									break;
								case Filter.Operators.GreaterThanOrEqual:
									if (0 > num)
									{
										flag = false;
									}
									break;
								case Filter.Operators.LessThan:
									if (0 <= num)
									{
										flag = false;
									}
									break;
								case Filter.Operators.LessThanOrEqual:
									if (0 < num)
									{
										flag = false;
									}
									break;
								default:
									if (@operator == Filter.Operators.NotEqual)
									{
										if (num == 0)
										{
											flag = false;
										}
									}
									break;
								}
							}
						}
						else if (Filter.Operators.In == filter.Operator)
						{
							object[] array = this.EvaluateFilterValues(filter);
							flag = false;
							if (array != null)
							{
								for (int j = 0; j < array.Length; j++)
								{
									try
									{
										if (array[j] is ICollection)
										{
											using (IEnumerator enumerator = ((ICollection)array[j]).GetEnumerator())
											{
												while (enumerator.MoveNext())
												{
													object obj2 = enumerator.Current;
													if (ReportProcessing.CompareTo(value, obj2, this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions) == 0)
													{
														flag = true;
														break;
													}
												}
												goto IL_0336;
											}
										}
										if (ReportProcessing.CompareTo(value, array[j], this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions) == 0)
										{
											flag = true;
										}
										IL_0336:
										if (flag)
										{
											break;
										}
									}
									catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError2)
									{
										throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError2));
									}
									catch
									{
										throw new ReportProcessingException(this.RegisterComparisonError());
									}
								}
							}
						}
						else
						{
							if (Filter.Operators.Between == filter.Operator)
							{
								object[] array2 = this.EvaluateFilterValues(filter);
								flag = false;
								Global.Tracer.Assert(array2 != null && 2 == array2.Length);
								try
								{
									if (0 <= ReportProcessing.CompareTo(value, array2[0], this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions) && 0 >= ReportProcessing.CompareTo(value, array2[1], this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions))
									{
										flag = true;
									}
									goto IL_05F2;
								}
								catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError3)
								{
									throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError3));
								}
								catch
								{
									throw new ReportProcessingException(this.RegisterComparisonError());
								}
							}
							if (Filter.Operators.TopN == filter.Operator || Filter.Operators.BottomN == filter.Operator)
							{
								if (this.m_filterInfo == null)
								{
									Global.Tracer.Assert(filter.Values != null && 1 == filter.Values.Count);
									IntegerResult integerResult = this.m_processingContext.ReportRuntime.EvaluateFilterIntegerValue(filter, 0, this.m_objectType, this.m_objectName);
									this.ThrowIfErrorOccurred("FilterValue", integerResult.ErrorOccurred, integerResult.FieldStatus);
									int value2 = integerResult.Value;
									IComparer comparer;
									if (Filter.Operators.TopN == filter.Operator)
									{
										comparer = new Filters.MyTopComparer(this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions);
									}
									else
									{
										comparer = new Filters.MyBottomComparer(this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions);
									}
									this.InitFilterInfos(new Filters.MySortedListWithMaxSize(comparer, value2, this), i);
								}
								this.SortAndSave(value, dataInstance);
								flag = false;
								specialFilter = true;
							}
							else if (Filter.Operators.TopPercent == filter.Operator || Filter.Operators.BottomPercent == filter.Operator)
							{
								if (this.m_filterInfo == null)
								{
									Global.Tracer.Assert(filter.Values != null && 1 == filter.Values.Count);
									FloatResult floatResult = this.m_processingContext.ReportRuntime.EvaluateFilterIntegerOrFloatValue(filter, 0, this.m_objectType, this.m_objectName);
									this.ThrowIfErrorOccurred("FilterValue", floatResult.ErrorOccurred, floatResult.FieldStatus);
									double value3 = floatResult.Value;
									IComparer comparer2;
									if (Filter.Operators.TopPercent == filter.Operator)
									{
										comparer2 = new Filters.MyTopComparer(this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions);
									}
									else
									{
										comparer2 = new Filters.MyBottomComparer(this.m_processingContext.CompareInfo, this.m_processingContext.ClrCompareOptions);
									}
									this.InitFilterInfos(new Filters.MySortedListWithoutMaxSize(comparer2, this), i);
									this.m_filterInfo.Percentage = value3;
								}
								this.SortAndSave(value, dataInstance);
								flag = false;
								specialFilter = true;
							}
						}
					}
					IL_05F2:
					if (!flag)
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x00177160 File Offset: 0x00175360
		internal void FinishReadingRows()
		{
			if (this.m_filterInfo != null)
			{
				Filters.FilterInfo filterInfo = this.m_filterInfo;
				this.m_filterInfo = null;
				this.m_startFilterIndex = this.m_currentSpecialFilterIndex + 1;
				bool flag = this.m_startFilterIndex >= this.m_filters.Count;
				this.TrimInstanceSet(filterInfo);
				for (Filters.DataInstanceInfo dataInstanceInfo = filterInfo.FirstInstance; dataInstanceInfo != null; dataInstanceInfo = dataInstanceInfo.NextInstance)
				{
					object dataInstance = dataInstanceInfo.DataInstance;
					if (Filters.FilterTypes.GroupFilter == this.m_filterType)
					{
						ReportProcessing.RuntimeGroupLeafObj runtimeGroupLeafObj = (ReportProcessing.RuntimeGroupLeafObj)dataInstance;
						runtimeGroupLeafObj.SetupEnvironment();
						if (flag || this.PassFilters(dataInstance))
						{
							runtimeGroupLeafObj.PostFilterNextRow();
						}
						else
						{
							runtimeGroupLeafObj.FailFilter();
						}
					}
					else
					{
						FieldImpl[] array = (FieldImpl[])dataInstance;
						this.m_processingContext.ReportObjectModel.FieldsImpl.SetFields(array);
						if (flag || this.PassFilters(dataInstance))
						{
							this.m_owner.PostFilterNextRow();
						}
					}
				}
				this.FinishReadingRows();
			}
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x00177240 File Offset: 0x00175440
		private ProcessingMessageList RegisterComparisonError()
		{
			return this.RegisterComparisonError(null);
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x0017724C File Offset: 0x0017544C
		private ProcessingMessageList RegisterComparisonError(ReportProcessingException_ComparisonError e)
		{
			if (e == null)
			{
				this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsComparisonError, Severity.Error, this.m_objectType, this.m_objectName, "FilterExpression", Array.Empty<string>());
			}
			else
			{
				this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsComparisonTypeError, Severity.Error, this.m_objectType, this.m_objectName, "FilterExpression", new string[] { e.TypeX, e.TypeY });
			}
			return this.m_processingContext.ErrorContext.Messages;
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x001772DC File Offset: 0x001754DC
		private void TrimInstanceSet(Filters.FilterInfo filterInfo)
		{
			if (-1.0 != filterInfo.Percentage)
			{
				int num = filterInfo.InstanceCount;
				double num2 = (double)num * filterInfo.Percentage / 100.0;
				if (num2 <= 0.0)
				{
					num = 0;
				}
				else
				{
					try
					{
						num = Convert.ToInt32(num2);
					}
					catch
					{
						throw new ReportProcessingException(ErrorCode.rsFilterEvaluationError, new object[] { "FilterValues" });
					}
				}
				filterInfo.TrimInstanceSet(num);
			}
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x00177360 File Offset: 0x00175560
		private object EvaluateFilterValue(Filter filterDef)
		{
			Global.Tracer.Assert(filterDef.Values != null, "(filterDef.Values != null)");
			Global.Tracer.Assert(filterDef.Values.Count > 0, "(filterDef.Values.Count > 0)");
			VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantValue(filterDef, 0, this.m_objectType, this.m_objectName);
			this.ThrowIfErrorOccurred("FilterValue", variantResult.ErrorOccurred, variantResult.FieldStatus);
			return variantResult.Value;
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x001773E0 File Offset: 0x001755E0
		private object[] EvaluateFilterValues(Filter filterDef)
		{
			if (filterDef.Values != null)
			{
				object[] array = new object[filterDef.Values.Count];
				for (int i = filterDef.Values.Count - 1; i >= 0; i--)
				{
					VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantValue(filterDef, i, this.m_objectType, this.m_objectName);
					this.ThrowIfErrorOccurred("FilterValues", variantResult.ErrorOccurred, variantResult.FieldStatus);
					array[i] = variantResult.Value;
				}
				return array;
			}
			return null;
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x00177460 File Offset: 0x00175660
		private void SortAndSave(object key, object dataInstance)
		{
			if (this.m_filterInfo.Add(key, dataInstance) && Filters.FilterTypes.GroupFilter != this.m_filterType)
			{
				this.m_processingContext.ReportObjectModel.FieldsImpl.GetAndSaveFields();
			}
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x00177490 File Offset: 0x00175690
		private void InitFilterInfos(Filters.MySortedList dataInstanceList, int currentFilterIndex)
		{
			this.m_filterInfo = new Filters.FilterInfo(dataInstanceList);
			if (-1 == this.m_currentSpecialFilterIndex && Filters.FilterTypes.DataRegionFilter == this.m_filterType)
			{
				this.m_processingContext.AddSpecialDataRegionFilters(this);
			}
			this.m_currentSpecialFilterIndex = currentFilterIndex;
		}

		// Token: 0x04002F6F RID: 12143
		private Filters.FilterTypes m_filterType;

		// Token: 0x04002F70 RID: 12144
		private ReportProcessing.IFilterOwner m_owner;

		// Token: 0x04002F71 RID: 12145
		private FilterList m_filters;

		// Token: 0x04002F72 RID: 12146
		private ObjectType m_objectType;

		// Token: 0x04002F73 RID: 12147
		private string m_objectName;

		// Token: 0x04002F74 RID: 12148
		private ReportProcessing.ProcessingContext m_processingContext;

		// Token: 0x04002F75 RID: 12149
		private int m_startFilterIndex;

		// Token: 0x04002F76 RID: 12150
		private int m_currentSpecialFilterIndex = -1;

		// Token: 0x04002F77 RID: 12151
		private Filters.FilterInfo m_filterInfo;

		// Token: 0x04002F78 RID: 12152
		private bool m_failFilters;

		// Token: 0x02000C9E RID: 3230
		internal enum FilterTypes
		{
			// Token: 0x04004D4D RID: 19789
			DataSetFilter,
			// Token: 0x04004D4E RID: 19790
			DataRegionFilter,
			// Token: 0x04004D4F RID: 19791
			GroupFilter
		}

		// Token: 0x02000C9F RID: 3231
		private sealed class MyTopComparer : IComparer
		{
			// Token: 0x06008CA2 RID: 36002 RVA: 0x0023C4F4 File Offset: 0x0023A6F4
			internal MyTopComparer(CompareInfo compareInfo, CompareOptions compareOptions)
			{
				this.m_compareInfo = compareInfo;
				this.m_compareOptions = compareOptions;
			}

			// Token: 0x06008CA3 RID: 36003 RVA: 0x0023C50A File Offset: 0x0023A70A
			int IComparer.Compare(object x, object y)
			{
				return ReportProcessing.CompareTo(y, x, this.m_compareInfo, this.m_compareOptions);
			}

			// Token: 0x04004D50 RID: 19792
			private CompareInfo m_compareInfo;

			// Token: 0x04004D51 RID: 19793
			private CompareOptions m_compareOptions;
		}

		// Token: 0x02000CA0 RID: 3232
		private sealed class MyBottomComparer : IComparer
		{
			// Token: 0x06008CA4 RID: 36004 RVA: 0x0023C51F File Offset: 0x0023A71F
			internal MyBottomComparer(CompareInfo compareInfo, CompareOptions compareOptions)
			{
				this.m_compareInfo = compareInfo;
				this.m_compareOptions = compareOptions;
			}

			// Token: 0x06008CA5 RID: 36005 RVA: 0x0023C535 File Offset: 0x0023A735
			int IComparer.Compare(object x, object y)
			{
				return ReportProcessing.CompareTo(x, y, this.m_compareInfo, this.m_compareOptions);
			}

			// Token: 0x04004D52 RID: 19794
			private CompareInfo m_compareInfo;

			// Token: 0x04004D53 RID: 19795
			private CompareOptions m_compareOptions;
		}

		// Token: 0x02000CA1 RID: 3233
		private abstract class MySortedList
		{
			// Token: 0x06008CA6 RID: 36006 RVA: 0x0023C54A File Offset: 0x0023A74A
			internal MySortedList(IComparer comparer, Filters filters)
			{
				this.m_comparer = comparer;
				this.m_filters = filters;
			}

			// Token: 0x17002B3B RID: 11067
			// (get) Token: 0x06008CA7 RID: 36007 RVA: 0x0023C560 File Offset: 0x0023A760
			internal int Count
			{
				get
				{
					if (this.m_keys == null)
					{
						return 0;
					}
					return this.m_keys.Count;
				}
			}

			// Token: 0x06008CA8 RID: 36008
			internal abstract bool Add(object key, object value, Filters.FilterInfo owner);

			// Token: 0x06008CA9 RID: 36009 RVA: 0x0023C577 File Offset: 0x0023A777
			internal virtual void TrimInstanceSet(int maxSize, Filters.FilterInfo owner)
			{
			}

			// Token: 0x06008CAA RID: 36010 RVA: 0x0023C57C File Offset: 0x0023A77C
			protected int Search(object key)
			{
				Global.Tracer.Assert(this.m_keys != null, "(null != m_keys)");
				int num = this.m_keys.BinarySearch(key, this.m_comparer);
				if (num < 0)
				{
					num = ~num;
				}
				else
				{
					num++;
					while (num < this.m_keys.Count && this.m_comparer.Compare(this.m_keys[num - 1], this.m_keys[num]) == 0)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x04004D54 RID: 19796
			protected IComparer m_comparer;

			// Token: 0x04004D55 RID: 19797
			protected ArrayList m_keys;

			// Token: 0x04004D56 RID: 19798
			protected ArrayList m_values;

			// Token: 0x04004D57 RID: 19799
			protected Filters m_filters;
		}

		// Token: 0x02000CA2 RID: 3234
		private sealed class MySortedListWithMaxSize : Filters.MySortedList
		{
			// Token: 0x06008CAB RID: 36011 RVA: 0x0023C5FD File Offset: 0x0023A7FD
			internal MySortedListWithMaxSize(IComparer comparer, int maxSize, Filters filters)
				: base(comparer, filters)
			{
				if (0 > maxSize)
				{
					this.m_maxSize = 0;
					return;
				}
				this.m_maxSize = maxSize;
			}

			// Token: 0x06008CAC RID: 36012 RVA: 0x0023C61C File Offset: 0x0023A81C
			internal override bool Add(object key, object value, Filters.FilterInfo owner)
			{
				if (this.m_keys == null)
				{
					this.m_keys = new ArrayList(Math.Min(1000, this.m_maxSize));
					this.m_values = new ArrayList(Math.Min(1000, this.m_maxSize));
				}
				int num;
				try
				{
					num = base.Search(key);
				}
				catch
				{
					throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
				}
				int count = this.m_keys.Count;
				bool flag = false;
				if (count < this.m_maxSize)
				{
					flag = true;
				}
				else if (num < count)
				{
					flag = true;
					if (num < this.m_maxSize)
					{
						int num2 = this.m_maxSize - 1;
						object obj;
						if (num == this.m_maxSize - 1)
						{
							obj = key;
						}
						else
						{
							obj = this.m_keys[num2 - 1];
						}
						int num3;
						try
						{
							num3 = this.m_comparer.Compare(this.m_keys[num2], obj);
						}
						catch
						{
							throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
						}
						if (num3 != 0)
						{
							for (int i = num2; i < count; i++)
							{
								owner.Remove((Filters.DataInstanceInfo)this.m_values[i]);
							}
							this.m_keys.RemoveRange(num2, count - num2);
							this.m_values.RemoveRange(num2, count - num2);
						}
					}
				}
				else if (count > 0)
				{
					try
					{
						if (this.m_comparer.Compare(this.m_keys[count - 1], key) == 0)
						{
							flag = true;
						}
					}
					catch
					{
						throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
					}
				}
				if (flag)
				{
					this.m_keys.Insert(num, key);
					this.m_values.Insert(num, value);
				}
				return flag;
			}

			// Token: 0x04004D58 RID: 19800
			private int m_maxSize;
		}

		// Token: 0x02000CA3 RID: 3235
		private sealed class MySortedListWithoutMaxSize : Filters.MySortedList
		{
			// Token: 0x06008CAD RID: 36013 RVA: 0x0023C7D8 File Offset: 0x0023A9D8
			internal MySortedListWithoutMaxSize(IComparer comparer, Filters filters)
				: base(comparer, filters)
			{
			}

			// Token: 0x06008CAE RID: 36014 RVA: 0x0023C7E4 File Offset: 0x0023A9E4
			internal override bool Add(object key, object value, Filters.FilterInfo owner)
			{
				if (this.m_keys == null)
				{
					this.m_keys = new ArrayList();
					this.m_values = new ArrayList();
				}
				int num;
				try
				{
					num = base.Search(key);
				}
				catch
				{
					throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
				}
				this.m_keys.Insert(num, key);
				this.m_values.Insert(num, value);
				return true;
			}

			// Token: 0x06008CAF RID: 36015 RVA: 0x0023C858 File Offset: 0x0023AA58
			internal override void TrimInstanceSet(int maxSize, Filters.FilterInfo owner)
			{
				int count = base.Count;
				if (count > maxSize)
				{
					if (0 < maxSize)
					{
						int num = maxSize;
						while (num < count && this.m_comparer.Compare(this.m_keys[num - 1], this.m_keys[num]) == 0)
						{
							num++;
						}
						for (int i = num; i < count; i++)
						{
							owner.Remove((Filters.DataInstanceInfo)this.m_values[i]);
						}
						this.m_keys.RemoveRange(num, count - num);
						this.m_values.RemoveRange(num, count - num);
						return;
					}
					owner.RemoveAll();
					this.m_keys = null;
					this.m_values = null;
				}
			}
		}

		// Token: 0x02000CA4 RID: 3236
		private sealed class FilterInfo
		{
			// Token: 0x06008CB0 RID: 36016 RVA: 0x0023C902 File Offset: 0x0023AB02
			internal FilterInfo(Filters.MySortedList dataInstances)
			{
				Global.Tracer.Assert(dataInstances != null, "(null != dataInstances)");
				this.m_dataInstances = dataInstances;
			}

			// Token: 0x17002B3C RID: 11068
			// (get) Token: 0x06008CB1 RID: 36017 RVA: 0x0023C933 File Offset: 0x0023AB33
			// (set) Token: 0x06008CB2 RID: 36018 RVA: 0x0023C93B File Offset: 0x0023AB3B
			internal double Percentage
			{
				get
				{
					return this.m_percentage;
				}
				set
				{
					this.m_percentage = value;
				}
			}

			// Token: 0x17002B3D RID: 11069
			// (get) Token: 0x06008CB3 RID: 36019 RVA: 0x0023C944 File Offset: 0x0023AB44
			internal int InstanceCount
			{
				get
				{
					return this.m_dataInstances.Count;
				}
			}

			// Token: 0x17002B3E RID: 11070
			// (get) Token: 0x06008CB4 RID: 36020 RVA: 0x0023C951 File Offset: 0x0023AB51
			internal Filters.DataInstanceInfo FirstInstance
			{
				get
				{
					return this.m_firstInstance;
				}
			}

			// Token: 0x06008CB5 RID: 36021 RVA: 0x0023C95C File Offset: 0x0023AB5C
			internal bool Add(object key, object dataInstance)
			{
				Filters.DataInstanceInfo dataInstanceInfo = new Filters.DataInstanceInfo();
				dataInstanceInfo.DataInstance = dataInstance;
				bool flag = this.m_dataInstances.Add(key, dataInstanceInfo, this);
				if (flag)
				{
					if (this.m_firstInstance == null)
					{
						this.m_firstInstance = dataInstanceInfo;
					}
					if (this.m_currentInstance != null)
					{
						this.m_currentInstance.NextInstance = dataInstanceInfo;
					}
					dataInstanceInfo.PrevInstance = this.m_currentInstance;
					dataInstanceInfo.NextInstance = null;
					this.m_currentInstance = dataInstanceInfo;
				}
				return flag;
			}

			// Token: 0x06008CB6 RID: 36022 RVA: 0x0023C9C4 File Offset: 0x0023ABC4
			internal void Remove(Filters.DataInstanceInfo instance)
			{
				if (instance.NextInstance != null)
				{
					instance.NextInstance.PrevInstance = instance.PrevInstance;
				}
				else
				{
					Global.Tracer.Assert(instance == this.m_currentInstance, "(instance == m_currentInstance)");
					this.m_currentInstance = instance.PrevInstance;
				}
				if (instance.PrevInstance != null)
				{
					instance.PrevInstance.NextInstance = instance.NextInstance;
					return;
				}
				Global.Tracer.Assert(instance == this.m_firstInstance, "(instance == m_firstInstance)");
				this.m_firstInstance = instance.NextInstance;
			}

			// Token: 0x06008CB7 RID: 36023 RVA: 0x0023CA4E File Offset: 0x0023AC4E
			internal void RemoveAll()
			{
				this.m_firstInstance = null;
				this.m_currentInstance = null;
			}

			// Token: 0x06008CB8 RID: 36024 RVA: 0x0023CA5E File Offset: 0x0023AC5E
			internal void TrimInstanceSet(int maxSize)
			{
				this.m_dataInstances.TrimInstanceSet(maxSize, this);
			}

			// Token: 0x04004D59 RID: 19801
			private double m_percentage = -1.0;

			// Token: 0x04004D5A RID: 19802
			private Filters.MySortedList m_dataInstances;

			// Token: 0x04004D5B RID: 19803
			private Filters.DataInstanceInfo m_firstInstance;

			// Token: 0x04004D5C RID: 19804
			private Filters.DataInstanceInfo m_currentInstance;
		}

		// Token: 0x02000CA5 RID: 3237
		private sealed class DataInstanceInfo
		{
			// Token: 0x04004D5D RID: 19805
			internal object DataInstance;

			// Token: 0x04004D5E RID: 19806
			internal Filters.DataInstanceInfo PrevInstance;

			// Token: 0x04004D5F RID: 19807
			internal Filters.DataInstanceInfo NextInstance;
		}
	}
}
