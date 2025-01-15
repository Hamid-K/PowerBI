using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000825 RID: 2085
	public sealed class Filters : IStaticReferenceable
	{
		// Token: 0x060074DE RID: 29918 RVA: 0x001E3C9C File Offset: 0x001E1E9C
		internal Filters(Filters.FilterTypes filterType, IReference<ReportProcessing.IFilterOwner> owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.Filter> filters, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, OnDemandProcessingContext processingContext, int scalabilityPriority)
			: this(filterType, filters, objectType, objectName, processingContext, scalabilityPriority)
		{
			this.m_owner = owner;
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x001E3CB5 File Offset: 0x001E1EB5
		internal Filters(Filters.FilterTypes filterType, RuntimeParameterDataSet owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.Filter> filters, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, OnDemandProcessingContext processingContext, int scalabilityPriority)
			: this(filterType, filters, objectType, objectName, processingContext, scalabilityPriority)
		{
			this.m_ownerObj = owner;
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x001E3CD0 File Offset: 0x001E1ED0
		private Filters(Filters.FilterTypes filterType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Filter> filters, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, OnDemandProcessingContext processingContext, int scalabilityPriority)
		{
			this.m_filterType = filterType;
			this.m_filters = filters;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_processingContext = processingContext;
			this.m_scalabilityPriority = scalabilityPriority;
		}

		// Token: 0x1700277C RID: 10108
		// (set) Token: 0x060074E1 RID: 29921 RVA: 0x001E3D22 File Offset: 0x001E1F22
		internal bool FailFilters
		{
			set
			{
				this.m_failFilters = value;
			}
		}

		// Token: 0x060074E2 RID: 29922 RVA: 0x001E3D2C File Offset: 0x001E1F2C
		internal bool PassFilters(object dataInstance)
		{
			bool flag;
			return this.PassFilters(dataInstance, out flag);
		}

		// Token: 0x060074E3 RID: 29923 RVA: 0x001E3D44 File Offset: 0x001E1F44
		private void ThrowIfErrorOccurred(string propertyName, bool errorOccurred, DataFieldStatus fieldStatus)
		{
			if (!errorOccurred)
			{
				return;
			}
			if (fieldStatus == DataFieldStatus.UnSupportedDataType)
			{
				throw new ReportProcessingException(string.Format(CultureInfo.CurrentCulture, RPRes.rsInvalidExpressionDataType, this.m_objectType, this.m_objectName, propertyName), ErrorCode.rsFilterEvaluationError);
			}
			if (fieldStatus != DataFieldStatus.None)
			{
				throw new ReportProcessingException(ErrorCode.rsFilterFieldError, new object[]
				{
					this.m_objectType,
					this.m_objectName,
					propertyName,
					Microsoft.ReportingServices.RdlExpressions.ReportRuntime.GetErrorName(fieldStatus, null)
				});
			}
			throw new ReportProcessingException(ErrorCode.rsFilterEvaluationError, new object[] { this.m_objectType, this.m_objectName, propertyName });
		}

		// Token: 0x060074E4 RID: 29924 RVA: 0x001E3DEC File Offset: 0x001E1FEC
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
					Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter = this.m_filters[i];
					if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.Like == filter.Operator)
					{
						Microsoft.ReportingServices.RdlExpressions.StringResult stringResult = this.m_processingContext.ReportRuntime.EvaluateFilterStringExpression(filter, this.m_objectType, this.m_objectName);
						this.ThrowIfErrorOccurred("FilterExpression", stringResult.ErrorOccurred, stringResult.FieldStatus);
						Global.Tracer.Assert(filter.Values != null, "(null != filter.Values)");
						Global.Tracer.Assert(1 <= filter.Values.Count, "(1 <= filter.Values.Count)");
						Microsoft.ReportingServices.RdlExpressions.StringResult stringResult2 = this.m_processingContext.ReportRuntime.EvaluateFilterStringValue(filter, 0, this.m_objectType, this.m_objectName);
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
						Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantExpression(filter, this.m_objectType, this.m_objectName);
						this.ThrowIfErrorOccurred("FilterExpression", variantResult.ErrorOccurred, variantResult.FieldStatus);
						object value = variantResult.Value;
						if (filter.Operator == Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.Equal || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.NotEqual == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.GreaterThan == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.GreaterThanOrEqual == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.LessThan == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.LessThanOrEqual == filter.Operator)
						{
							object obj = this.EvaluateFilterValue(filter);
							int num = 0;
							try
							{
								num = this.Compare(value, obj);
							}
							catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError)
							{
								throw new ReportProcessingException(this.RegisterSpatialTypeComparisonError(reportProcessingException_SpatialTypeComparisonError.Type));
							}
							catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError)
							{
								throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError));
							}
							catch (Exception ex)
							{
								if (AsynchronousExceptionDetection.IsStoppingException(ex))
								{
									throw;
								}
								throw new ReportProcessingException(this.RegisterComparisonError());
							}
							if (flag)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators @operator = filter.Operator;
								switch (@operator)
								{
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.Equal:
									if (num != 0)
									{
										flag = false;
									}
									break;
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.Like:
									break;
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.GreaterThan:
									if (0 >= num)
									{
										flag = false;
									}
									break;
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.GreaterThanOrEqual:
									if (0 > num)
									{
										flag = false;
									}
									break;
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.LessThan:
									if (0 <= num)
									{
										flag = false;
									}
									break;
								case Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.LessThanOrEqual:
									if (0 < num)
									{
										flag = false;
									}
									break;
								default:
									if (@operator == Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.NotEqual)
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
						else if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.In == filter.Operator)
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
													if (this.Compare(value, obj2) == 0)
													{
														flag = true;
														break;
													}
												}
												goto IL_031E;
											}
										}
										if (this.Compare(value, array[j]) == 0)
										{
											flag = true;
										}
										IL_031E:
										if (flag)
										{
											break;
										}
									}
									catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError2)
									{
										throw new ReportProcessingException(this.RegisterSpatialTypeComparisonError(reportProcessingException_SpatialTypeComparisonError2.Type));
									}
									catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError2)
									{
										throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError2));
									}
									catch (Exception ex2)
									{
										if (AsynchronousExceptionDetection.IsStoppingException(ex2))
										{
											throw;
										}
										throw new ReportProcessingException(this.RegisterComparisonError());
									}
								}
							}
						}
						else
						{
							if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.Between == filter.Operator)
							{
								object[] array2 = this.EvaluateFilterValues(filter);
								flag = false;
								Global.Tracer.Assert(array2 != null && 2 == array2.Length, "(null != values && 2 == values.Length)");
								try
								{
									if (0 <= this.Compare(value, array2[0]) && 0 >= this.Compare(value, array2[1]))
									{
										flag = true;
									}
									goto IL_05E1;
								}
								catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError3)
								{
									throw new ReportProcessingException(this.RegisterSpatialTypeComparisonError(reportProcessingException_SpatialTypeComparisonError3.Type));
								}
								catch (ReportProcessingException_ComparisonError reportProcessingException_ComparisonError3)
								{
									throw new ReportProcessingException(this.RegisterComparisonError(reportProcessingException_ComparisonError3));
								}
								catch (RSException)
								{
									throw;
								}
								catch (Exception ex3)
								{
									if (AsynchronousExceptionDetection.IsStoppingException(ex3))
									{
										throw;
									}
									throw new ReportProcessingException(this.RegisterComparisonError());
								}
							}
							if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.TopN == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.BottomN == filter.Operator)
							{
								if (this.m_filterInfo == null)
								{
									Global.Tracer.Assert(filter.Values != null && 1 == filter.Values.Count, "(null != filter.Values && 1 == filter.Values.Count)");
									Microsoft.ReportingServices.RdlExpressions.IntegerResult integerResult = this.m_processingContext.ReportRuntime.EvaluateFilterIntegerValue(filter, 0, this.m_objectType, this.m_objectName);
									this.ThrowIfErrorOccurred("FilterValue", integerResult.ErrorOccurred, integerResult.FieldStatus);
									int value2 = integerResult.Value;
									IComparer comparer;
									if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.TopN == filter.Operator)
									{
										comparer = new Filters.MyTopComparer(this.m_processingContext.ProcessingComparer);
									}
									else
									{
										comparer = new Filters.MyBottomComparer(this.m_processingContext.ProcessingComparer);
									}
									this.InitFilterInfos(new Filters.MySortedListWithMaxSize(comparer, value2, this), i);
								}
								this.SortAndSave(value, dataInstance);
								flag = false;
								specialFilter = true;
							}
							else if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.TopPercent == filter.Operator || Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.BottomPercent == filter.Operator)
							{
								if (this.m_filterInfo == null)
								{
									Global.Tracer.Assert(filter.Values != null && 1 == filter.Values.Count, "(null != filter.Values && 1 == filter.Values.Count)");
									Microsoft.ReportingServices.RdlExpressions.FloatResult floatResult = this.m_processingContext.ReportRuntime.EvaluateFilterIntegerOrFloatValue(filter, 0, this.m_objectType, this.m_objectName);
									this.ThrowIfErrorOccurred("FilterValue", floatResult.ErrorOccurred, floatResult.FieldStatus);
									double value3 = floatResult.Value;
									IComparer comparer2;
									if (Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.TopPercent == filter.Operator)
									{
										comparer2 = new Filters.MyTopComparer(this.m_processingContext.ProcessingComparer);
									}
									else
									{
										comparer2 = new Filters.MyBottomComparer(this.m_processingContext.ProcessingComparer);
									}
									this.InitFilterInfos(new Filters.MySortedListWithoutMaxSize(comparer2, this), i);
									this.m_filterInfo.Percentage = value3;
									this.m_filterInfo.Operator = filter.Operator;
								}
								this.SortAndSave(value, dataInstance);
								flag = false;
								specialFilter = true;
							}
						}
					}
					IL_05E1:
					if (!flag)
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060074E5 RID: 29925 RVA: 0x001E4480 File Offset: 0x001E2680
		private int Compare(object value1, object value2)
		{
			return this.m_processingContext.ProcessingComparer.Compare(value1, value2);
		}

		// Token: 0x060074E6 RID: 29926 RVA: 0x001E4494 File Offset: 0x001E2694
		internal void FinishReadingGroups(AggregateUpdateContext context)
		{
			this.FinishFilters(context);
		}

		// Token: 0x060074E7 RID: 29927 RVA: 0x001E449D File Offset: 0x001E269D
		internal void FinishReadingRows()
		{
			this.FinishFilters(null);
		}

		// Token: 0x060074E8 RID: 29928 RVA: 0x001E44A8 File Offset: 0x001E26A8
		private void FinishFilters(AggregateUpdateContext context)
		{
			if (this.m_filterInfo != null)
			{
				Filters.FilterInfo filterInfo = this.m_filterInfo;
				this.m_filterInfo = null;
				this.m_startFilterIndex = this.m_currentSpecialFilterIndex + 1;
				bool flag = this.m_startFilterIndex >= this.m_filters.Count;
				this.TrimInstanceSet(filterInfo);
				IEnumerator<object> instances = filterInfo.Instances;
				if (instances != null)
				{
					try
					{
						ReportProcessing.IFilterOwner filterOwner;
						if (this.m_owner != null)
						{
							this.m_owner.PinValue();
							filterOwner = this.m_owner.Value();
						}
						else
						{
							filterOwner = this.m_ownerObj;
						}
						while (instances.MoveNext())
						{
							object obj = instances.Current;
							if (Filters.FilterTypes.GroupFilter == this.m_filterType)
							{
								IReference<RuntimeGroupLeafObj> reference = (IReference<RuntimeGroupLeafObj>)obj;
								using (reference.PinValue())
								{
									RuntimeGroupLeafObj runtimeGroupLeafObj = reference.Value();
									runtimeGroupLeafObj.SetupEnvironment();
									if (flag || this.PassFilters(obj))
									{
										runtimeGroupLeafObj.PostFilterNextRow(context);
										continue;
									}
									runtimeGroupLeafObj.FailFilter();
									continue;
								}
							}
							((DataFieldRow)obj).SetFields(this.m_processingContext.ReportObjectModel.FieldsImpl);
							if (flag || this.PassFilters(obj))
							{
								filterOwner.PostFilterNextRow();
							}
						}
					}
					finally
					{
						if (this.m_owner != null)
						{
							this.m_owner.UnPinValue();
						}
					}
				}
				filterInfo.RemoveAll();
				filterInfo = null;
				this.FinishFilters(context);
			}
		}

		// Token: 0x060074E9 RID: 29929 RVA: 0x001E4614 File Offset: 0x001E2814
		private ProcessingMessageList RegisterComparisonError()
		{
			return this.RegisterComparisonError(null);
		}

		// Token: 0x060074EA RID: 29930 RVA: 0x001E4620 File Offset: 0x001E2820
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

		// Token: 0x060074EB RID: 29931 RVA: 0x001E46AF File Offset: 0x001E28AF
		private ProcessingMessageList RegisterSpatialTypeComparisonError(string type)
		{
			this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCannotCompareSpatialType, Severity.Error, this.m_objectType, this.m_objectName, type, Array.Empty<string>());
			return this.m_processingContext.ErrorContext.Messages;
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x001E46EC File Offset: 0x001E28EC
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
						if (filterInfo.Operator == Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.BottomPercent)
						{
							num = (int)Math.Floor(num2);
						}
						else
						{
							num = (int)Math.Ceiling(num2);
						}
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						throw new ReportProcessingException(ErrorCode.rsFilterEvaluationError, new object[] { "FilterValues" });
					}
				}
				filterInfo.TrimInstanceSet(num);
			}
		}

		// Token: 0x060074ED RID: 29933 RVA: 0x001E4790 File Offset: 0x001E2990
		private object EvaluateFilterValue(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filterDef)
		{
			Global.Tracer.Assert(filterDef.Values != null, "(filterDef.Values != null)");
			Global.Tracer.Assert(filterDef.Values.Count > 0, "(filterDef.Values.Count > 0)");
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantValue(filterDef, 0, this.m_objectType, this.m_objectName);
			this.ThrowIfErrorOccurred("FilterValue", variantResult.ErrorOccurred, variantResult.FieldStatus);
			return variantResult.Value;
		}

		// Token: 0x060074EE RID: 29934 RVA: 0x001E4810 File Offset: 0x001E2A10
		private object[] EvaluateFilterValues(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filterDef)
		{
			if (filterDef.Values != null)
			{
				object[] array = new object[filterDef.Values.Count];
				for (int i = filterDef.Values.Count - 1; i >= 0; i--)
				{
					Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.m_processingContext.ReportRuntime.EvaluateFilterVariantValue(filterDef, i, this.m_objectType, this.m_objectName);
					this.ThrowIfErrorOccurred("FilterValues", variantResult.ErrorOccurred, variantResult.FieldStatus);
					array[i] = variantResult.Value;
				}
				return array;
			}
			return null;
		}

		// Token: 0x060074EF RID: 29935 RVA: 0x001E4890 File Offset: 0x001E2A90
		private void SortAndSave(object key, object dataInstance)
		{
			if (this.m_filterInfo.Add(key, dataInstance) && Filters.FilterTypes.GroupFilter != this.m_filterType)
			{
				this.m_processingContext.ReportObjectModel.FieldsImpl.GetAndSaveFields();
			}
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x001E48C0 File Offset: 0x001E2AC0
		private void InitFilterInfos(Filters.MySortedList dataInstanceList, int currentFilterIndex)
		{
			this.m_filterInfo = new Filters.FilterInfo(dataInstanceList);
			if (-1 == this.m_currentSpecialFilterIndex && Filters.FilterTypes.DataRegionFilter == this.m_filterType)
			{
				this.m_processingContext.AddSpecialDataRegionFilters(this);
			}
			this.m_currentSpecialFilterIndex = currentFilterIndex;
		}

		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x060074F1 RID: 29937 RVA: 0x001E48F3 File Offset: 0x001E2AF3
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x060074F2 RID: 29938 RVA: 0x001E48FB File Offset: 0x001E2AFB
		void IStaticReferenceable.SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x060074F3 RID: 29939 RVA: 0x001E4904 File Offset: 0x001E2B04
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filters;
		}

		// Token: 0x04003B5D RID: 15197
		private Filters.FilterTypes m_filterType;

		// Token: 0x04003B5E RID: 15198
		private IReference<ReportProcessing.IFilterOwner> m_owner;

		// Token: 0x04003B5F RID: 15199
		private ReportProcessing.IFilterOwner m_ownerObj;

		// Token: 0x04003B60 RID: 15200
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.Filter> m_filters;

		// Token: 0x04003B61 RID: 15201
		private Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04003B62 RID: 15202
		private string m_objectName;

		// Token: 0x04003B63 RID: 15203
		private OnDemandProcessingContext m_processingContext;

		// Token: 0x04003B64 RID: 15204
		private int m_startFilterIndex;

		// Token: 0x04003B65 RID: 15205
		private int m_currentSpecialFilterIndex = -1;

		// Token: 0x04003B66 RID: 15206
		private Filters.FilterInfo m_filterInfo;

		// Token: 0x04003B67 RID: 15207
		private bool m_failFilters;

		// Token: 0x04003B68 RID: 15208
		private int m_scalabilityPriority;

		// Token: 0x04003B69 RID: 15209
		private int m_id = int.MinValue;

		// Token: 0x02000CFC RID: 3324
		internal enum FilterTypes
		{
			// Token: 0x04005009 RID: 20489
			DataSetFilter,
			// Token: 0x0400500A RID: 20490
			DataRegionFilter,
			// Token: 0x0400500B RID: 20491
			GroupFilter
		}

		// Token: 0x02000CFD RID: 3325
		private sealed class MyTopComparer : IComparer
		{
			// Token: 0x06008E56 RID: 36438 RVA: 0x002448EF File Offset: 0x00242AEF
			internal MyTopComparer(IDataComparer comparer)
			{
				this.m_comparer = comparer;
			}

			// Token: 0x06008E57 RID: 36439 RVA: 0x00244900 File Offset: 0x00242B00
			int IComparer.Compare(object x, object y)
			{
				object key = ((Filters.FilterKey)x).Key;
				object key2 = ((Filters.FilterKey)y).Key;
				return this.m_comparer.Compare(key2, key);
			}

			// Token: 0x0400500C RID: 20492
			private readonly IDataComparer m_comparer;
		}

		// Token: 0x02000CFE RID: 3326
		private sealed class MyBottomComparer : IComparer
		{
			// Token: 0x06008E58 RID: 36440 RVA: 0x00244932 File Offset: 0x00242B32
			internal MyBottomComparer(IDataComparer comparer)
			{
				this.m_comparer = comparer;
			}

			// Token: 0x06008E59 RID: 36441 RVA: 0x00244944 File Offset: 0x00242B44
			int IComparer.Compare(object x, object y)
			{
				object key = ((Filters.FilterKey)x).Key;
				object key2 = ((Filters.FilterKey)y).Key;
				return this.m_comparer.Compare(key, key2);
			}

			// Token: 0x0400500D RID: 20493
			private readonly IDataComparer m_comparer;
		}

		// Token: 0x02000CFF RID: 3327
		private abstract class MySortedList
		{
			// Token: 0x06008E5A RID: 36442 RVA: 0x00244976 File Offset: 0x00242B76
			internal MySortedList(IComparer comparer, Filters filters)
			{
				this.m_comparer = comparer;
				this.m_filters = filters;
			}

			// Token: 0x17002BAE RID: 11182
			// (get) Token: 0x06008E5B RID: 36443 RVA: 0x0024498C File Offset: 0x00242B8C
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

			// Token: 0x17002BAF RID: 11183
			// (get) Token: 0x06008E5C RID: 36444 RVA: 0x002449A3 File Offset: 0x00242BA3
			internal IEnumerator<object> Instances
			{
				get
				{
					if (this.m_keys == null)
					{
						return null;
					}
					return this.m_values.GetEnumerator();
				}
			}

			// Token: 0x06008E5D RID: 36445
			internal abstract bool Add(Filters.FilterKey key, object value, Filters.FilterInfo owner);

			// Token: 0x06008E5E RID: 36446 RVA: 0x002449BA File Offset: 0x00242BBA
			internal virtual void TrimInstanceSet(int maxSize, Filters.FilterInfo owner)
			{
			}

			// Token: 0x06008E5F RID: 36447 RVA: 0x002449BC File Offset: 0x00242BBC
			protected int Search(Filters.FilterKey key)
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

			// Token: 0x06008E60 RID: 36448 RVA: 0x00244A3D File Offset: 0x00242C3D
			internal void Clear()
			{
				if (this.m_values != null)
				{
					this.m_values.Clear();
				}
				if (this.m_keys != null)
				{
					this.m_keys.Clear();
				}
			}

			// Token: 0x06008E61 RID: 36449 RVA: 0x00244A68 File Offset: 0x00242C68
			protected void InitLists(int bucketSize, int initialCapacity)
			{
				OnDemandProcessingContext processingContext = this.m_filters.m_processingContext;
				processingContext.EnsureScalabilitySetup();
				this.m_keys = new ScalableList<Filters.FilterKey>(this.m_filters.m_scalabilityPriority, processingContext.TablixProcessingScalabilityCache, bucketSize, initialCapacity);
				this.m_values = new ScalableHybridList<object>(this.m_filters.m_scalabilityPriority, processingContext.TablixProcessingScalabilityCache, bucketSize, initialCapacity);
			}

			// Token: 0x0400500E RID: 20494
			protected IComparer m_comparer;

			// Token: 0x0400500F RID: 20495
			protected ScalableList<Filters.FilterKey> m_keys;

			// Token: 0x04005010 RID: 20496
			protected ScalableHybridList<object> m_values;

			// Token: 0x04005011 RID: 20497
			protected Filters m_filters;
		}

		// Token: 0x02000D00 RID: 3328
		internal sealed class FilterKey : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x17002BB0 RID: 11184
			// (get) Token: 0x06008E62 RID: 36450 RVA: 0x00244AC3 File Offset: 0x00242CC3
			public int Size
			{
				get
				{
					return ItemSizes.SizeOf(this.Key) + 4;
				}
			}

			// Token: 0x06008E63 RID: 36451 RVA: 0x00244AD4 File Offset: 0x00242CD4
			public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(Filters.FilterKey.m_declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.Value)
					{
						if (memberName == MemberName.Key)
						{
							writer.WriteVariantOrPersistable(this.Key);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						writer.Write(this.ValueIndex);
					}
				}
			}

			// Token: 0x06008E64 RID: 36452 RVA: 0x00244B38 File Offset: 0x00242D38
			public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(Filters.FilterKey.m_declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Value)
					{
						if (memberName == MemberName.Key)
						{
							this.Key = reader.ReadVariant();
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						this.ValueIndex = reader.ReadInt32();
					}
				}
			}

			// Token: 0x06008E65 RID: 36453 RVA: 0x00244B9C File Offset: 0x00242D9C
			public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
				Global.Tracer.Assert(false, "No references to resolve");
			}

			// Token: 0x06008E66 RID: 36454 RVA: 0x00244BAE File Offset: 0x00242DAE
			public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FilterKey;
			}

			// Token: 0x06008E67 RID: 36455 RVA: 0x00244BB4 File Offset: 0x00242DB4
			public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				if (Filters.FilterKey.m_declaration == null)
				{
					return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FilterKey, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Key, Token.Object),
						new MemberInfo(MemberName.Value, Token.Int32)
					});
				}
				return Filters.FilterKey.m_declaration;
			}

			// Token: 0x04005012 RID: 20498
			internal object Key;

			// Token: 0x04005013 RID: 20499
			internal int ValueIndex;

			// Token: 0x04005014 RID: 20500
			private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Filters.FilterKey.GetDeclaration();
		}

		// Token: 0x02000D01 RID: 3329
		private sealed class MySortedListWithMaxSize : Filters.MySortedList
		{
			// Token: 0x06008E6A RID: 36458 RVA: 0x00244C11 File Offset: 0x00242E11
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

			// Token: 0x06008E6B RID: 36459 RVA: 0x00244C30 File Offset: 0x00242E30
			internal override bool Add(Filters.FilterKey key, object value, Filters.FilterInfo owner)
			{
				if (this.m_keys == null)
				{
					int num = Math.Min(500, this.m_maxSize);
					base.InitLists(num, num);
				}
				int num2;
				try
				{
					num2 = base.Search(key);
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
				}
				int count = this.m_keys.Count;
				bool flag = false;
				if (count < this.m_maxSize)
				{
					flag = true;
				}
				else if (num2 < count)
				{
					flag = true;
					if (num2 < this.m_maxSize)
					{
						int num3 = this.m_maxSize - 1;
						object obj;
						if (num2 == this.m_maxSize - 1)
						{
							obj = key;
						}
						else
						{
							obj = this.m_keys[num3 - 1];
						}
						int num4;
						try
						{
							num4 = this.m_comparer.Compare(this.m_keys[num3], obj);
						}
						catch (Exception ex2)
						{
							if (AsynchronousExceptionDetection.IsStoppingException(ex2))
							{
								throw;
							}
							throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
						}
						if (num4 != 0)
						{
							for (int i = num3; i < count; i++)
							{
								Filters.FilterKey filterKey = this.m_keys[i];
								this.m_values.Remove(filterKey.ValueIndex);
							}
							this.m_keys.RemoveRange(num3, count - num3);
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
					catch (Exception ex3)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex3))
						{
							throw;
						}
						throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
					}
				}
				if (flag)
				{
					key.ValueIndex = this.m_values.Add(value);
					this.m_keys.Insert(num2, key);
				}
				return flag;
			}

			// Token: 0x04005015 RID: 20501
			private int m_maxSize;
		}

		// Token: 0x02000D02 RID: 3330
		private sealed class MySortedListWithoutMaxSize : Filters.MySortedList
		{
			// Token: 0x06008E6C RID: 36460 RVA: 0x00244DF0 File Offset: 0x00242FF0
			internal MySortedListWithoutMaxSize(IComparer comparer, Filters filters)
				: base(comparer, filters)
			{
			}

			// Token: 0x06008E6D RID: 36461 RVA: 0x00244DFC File Offset: 0x00242FFC
			internal override bool Add(Filters.FilterKey key, object value, Filters.FilterInfo owner)
			{
				if (this.m_keys == null)
				{
					base.InitLists(500, 500);
				}
				int num;
				try
				{
					num = base.Search(key);
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException(this.m_filters.RegisterComparisonError());
				}
				key.ValueIndex = this.m_values.Add(value);
				this.m_keys.Insert(num, key);
				return true;
			}

			// Token: 0x06008E6E RID: 36462 RVA: 0x00244E78 File Offset: 0x00243078
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
							Filters.FilterKey filterKey = this.m_keys[i];
							this.m_values.Remove(filterKey.ValueIndex);
						}
						this.m_keys.RemoveRange(num, count - num);
						return;
					}
					if (this.m_keys != null)
					{
						this.m_keys.Dispose();
					}
					if (this.m_values != null)
					{
						this.m_values.Dispose();
					}
					this.m_keys = null;
					this.m_values = null;
				}
			}
		}

		// Token: 0x02000D03 RID: 3331
		private sealed class FilterInfo
		{
			// Token: 0x06008E6F RID: 36463 RVA: 0x00244F3A File Offset: 0x0024313A
			internal FilterInfo(Filters.MySortedList dataInstances)
			{
				Global.Tracer.Assert(dataInstances != null, "(null != dataInstances)");
				this.m_dataInstances = dataInstances;
			}

			// Token: 0x17002BB1 RID: 11185
			// (get) Token: 0x06008E70 RID: 36464 RVA: 0x00244F72 File Offset: 0x00243172
			// (set) Token: 0x06008E71 RID: 36465 RVA: 0x00244F7A File Offset: 0x0024317A
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

			// Token: 0x17002BB2 RID: 11186
			// (get) Token: 0x06008E72 RID: 36466 RVA: 0x00244F83 File Offset: 0x00243183
			// (set) Token: 0x06008E73 RID: 36467 RVA: 0x00244F8B File Offset: 0x0024318B
			internal Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators Operator
			{
				get
				{
					return this.m_operator;
				}
				set
				{
					this.m_operator = value;
				}
			}

			// Token: 0x17002BB3 RID: 11187
			// (get) Token: 0x06008E74 RID: 36468 RVA: 0x00244F94 File Offset: 0x00243194
			internal int InstanceCount
			{
				get
				{
					return this.m_dataInstances.Count;
				}
			}

			// Token: 0x17002BB4 RID: 11188
			// (get) Token: 0x06008E75 RID: 36469 RVA: 0x00244FA1 File Offset: 0x002431A1
			internal IEnumerator<object> Instances
			{
				get
				{
					return this.m_dataInstances.Instances;
				}
			}

			// Token: 0x06008E76 RID: 36470 RVA: 0x00244FB0 File Offset: 0x002431B0
			internal bool Add(object key, object dataInstance)
			{
				Filters.FilterKey filterKey = new Filters.FilterKey();
				filterKey.Key = key;
				return this.m_dataInstances.Add(filterKey, dataInstance, this);
			}

			// Token: 0x06008E77 RID: 36471 RVA: 0x00244FD8 File Offset: 0x002431D8
			internal void RemoveAll()
			{
				this.m_dataInstances.Clear();
			}

			// Token: 0x06008E78 RID: 36472 RVA: 0x00244FE5 File Offset: 0x002431E5
			internal void TrimInstanceSet(int maxSize)
			{
				this.m_dataInstances.TrimInstanceSet(maxSize, this);
			}

			// Token: 0x04005016 RID: 20502
			private double m_percentage = -1.0;

			// Token: 0x04005017 RID: 20503
			private Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators m_operator = Microsoft.ReportingServices.ReportIntermediateFormat.Filter.Operators.TopPercent;

			// Token: 0x04005018 RID: 20504
			private Filters.MySortedList m_dataInstances;
		}
	}
}
