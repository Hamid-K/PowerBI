using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using ParquetSharp;
using ParquetSharp.IO;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F64 RID: 8036
	internal sealed class ParquetScalableTableWriter : IDisposable
	{
		// Token: 0x06010DF6 RID: 69110 RVA: 0x003A1D38 File Offset: 0x0039FF38
		public ParquetScalableTableWriter(IEngineHost host, OptionsRecord options)
		{
			this.host = host;
			this.memoryPool = MemoryPool.GetDefaultMemoryPool();
			this.process = Process.GetCurrentProcess();
			this.memoryPressure = new ParquetScalableTableWriter.MemoryPressure(0L);
			this.InitMemoryLimits(out this.maxWorkingSet, out this.processCommitLimit, out this.jobCommitLimit);
			this.memoryTarget = this.CalculateMemoryTarget();
			this.GetAvailableMemory(true, false);
			this.shuffled = !options.GetBool("PreserveOrder", true);
			this.buffered = true;
		}

		// Token: 0x17002CBC RID: 11452
		// (get) Token: 0x06010DF7 RID: 69111 RVA: 0x003A1DD0 File Offset: 0x0039FFD0
		private static string EngineName
		{
			get
			{
				if (ParquetScalableTableWriter.engineName == null)
				{
					string text;
					try
					{
						text = new Version(FileVersionInfo.GetVersionInfo(typeof(IEngine).Assembly.Location).FileVersion).ToString();
					}
					catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						text = "(unknown)";
					}
					ParquetScalableTableWriter.engineName = "Mashup Engine/" + text;
				}
				return ParquetScalableTableWriter.engineName;
			}
		}

		// Token: 0x06010DF8 RID: 69112 RVA: 0x003A1E54 File Offset: 0x003A0054
		public void Dispose()
		{
			this.process.Dispose();
			this.memoryPool.ReleaseUnused();
			this.memoryPressure.Dispose();
		}

		// Token: 0x06010DF9 RID: 69113 RVA: 0x003A1E78 File Offset: 0x003A0078
		public long Write(BinaryValue destination, TableValue table, SchemaElement schema, WriterProperties properties, IAccumulator accumulator)
		{
			GroupSchemaElement groupSchemaElement = (GroupSchemaElement)schema;
			bool flag;
			if (accumulator == null)
			{
				flag = groupSchemaElement.Fields.All((SchemaElement c) => c.ElementType == NodeType.Primitive && c.Repetition != Repetition.Repeated && ((PrimitiveSchemaElement)c).TypeMap.IsOleDbCompatible);
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				using (IPageReader reader = table.GetReader())
				{
					IPageReader pageReader;
					if (SchemaConformingPageReader.TryCreate(reader, schema, out pageReader))
					{
						using (IPageReader pageReader2 = pageReader)
						{
							using (IPage page = pageReader2.CreatePage())
							{
								return this.Write(destination, this.GetAddRows(page), this.GetMoveNext(pageReader2, page), schema, properties, null);
							}
						}
					}
				}
			}
			long num;
			using (IEnumerator<IValueReference> enumerator = table.GetEnumerator())
			{
				num = this.Write(destination, this.GetAddRows(enumerator), this.GetMoveNext(enumerator), schema, properties, this.GetAccumulate(enumerator, accumulator));
			}
			return num;
		}

		// Token: 0x06010DFA RID: 69114 RVA: 0x003A1F90 File Offset: 0x003A0190
		private long Write(BinaryValue destination, Func<ParquetRecordWriter, long> addRows, Func<bool> moveNext, SchemaElement schema, WriterProperties properties, Action accumulate)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"EngineName",
				ParquetScalableTableWriter.EngineName
			} };
			long num;
			using (ParquetRecordWriter parquetRecordWriter = new ParquetRecordWriter(schema))
			{
				using (GroupNode groupNode = (GroupNode)schema.CreateNode())
				{
					using (Stream stream = destination.OpenForWrite())
					{
						using (OutputStream outputStream = new ManagedOutputStream(stream))
						{
							using (ParquetFileWriter parquetFileWriter = new ParquetFileWriter(outputStream, groupNode, properties, dictionary))
							{
								if (!moveNext())
								{
									parquetFileWriter.Close();
									num = 0L;
								}
								else
								{
									long num2 = 0L;
									while (this.WriteRowGroup(parquetFileWriter, parquetRecordWriter, addRows, moveNext, accumulate, ref num2))
									{
									}
									parquetFileWriter.Close();
									num = num2;
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06010DFB RID: 69115 RVA: 0x003A2098 File Offset: 0x003A0298
		private bool WriteRowGroup(ParquetFileWriter writer, ParquetRecordWriter recordWriter, Func<ParquetRecordWriter, long> addRows, Func<bool> moveNext, Action accumulate, ref long count)
		{
			this.committedNativeMemory = 0L;
			this.endRowGroup = false;
			if (!this.IncreaseCommittedNativeMemory(16777216L, false) && !this.IncreaseCommittedNativeMemory(16777216L, true))
			{
				throw this.InsufficientMemory("WriteRowGroup", null);
			}
			bool flag3;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Parquet/Write/RowGroup", TraceEventType.Information, null))
			{
				try
				{
					using (this.memoryPressure)
					{
						using (RowGroupWriter rowGroupWriter = (this.buffered ? writer.AppendBufferedRowGroup() : writer.AppendRowGroup()))
						{
							hostTrace.Add("StartingCommittedNativeMemory", this.committedNativeMemory, false);
							bool flag = true;
							bool flag2 = true;
							long num = 0L;
							long num2 = 0L;
							do
							{
								try
								{
									long num3 = addRows(recordWriter);
									long num4 = recordWriter.EstimateSize();
									if (!flag2 && num4 > this.recordWriterSizeLimit)
									{
										recordWriter.Rollback();
										this.recordWriterSizeLimit = Math.Min(this.recordWriterSizeLimit, recordWriter.EstimateSize());
										break;
									}
									if (!flag2 && this.memoryPool.BytesAllocated + num4 + 16777216L > this.committedNativeMemory && !this.IncreaseCommittedNativeMemory(num4 + 16777216L, false))
									{
										recordWriter.Rollback();
										break;
									}
									recordWriter.Commit();
									if (accumulate != null)
									{
										accumulate();
									}
									count += num3;
									flag2 = false;
									long num5 = num;
									num += num3;
									if (num4 >= this.memoryTarget)
									{
										flag = moveNext();
										break;
									}
									if (num / 4096L > num5 / 4096L)
									{
										if (this.buffered)
										{
											recordWriter.Write(rowGroupWriter, this.buffered);
											recordWriter.Clear();
											num2 += num;
											num = 0L;
											if (this.shuffled && this.memoryPool.BytesAllocated > this.committedNativeMemory)
											{
												hostTrace.Add("VOrderAllocationExeededTracking", this.memoryPool.BytesAllocated, false);
												hostTrace.Add("TrackingMemoryLimit", this.committedNativeMemory, false);
												flag = moveNext();
												break;
											}
											if (this.shuffled && this.memoryPool.BytesAllocated >= 250000000L && this.memoryPool.BytesAllocated * 2L > this.committedNativeMemory && !this.IncreaseCommittedNativeMemory(this.memoryPool.BytesAllocated + 16777216L, false))
											{
												hostTrace.Add("VOrderAllocationNearLimit", this.memoryPool.BytesAllocated, false);
												flag = moveNext();
												break;
											}
											if (this.memoryPool.BytesAllocated >= this.memoryTarget && (!this.shuffled || rowGroupWriter.EstimatedMemoryUsage >= this.memoryTarget))
											{
												flag = moveNext();
												break;
											}
										}
										this.CheckCommittedNativeMemory();
									}
								}
								catch (InsufficientMemoryException)
								{
									if (flag2)
									{
										throw this.InsufficientMemory("StartOfRowGroup", new long?(rowGroupWriter.EstimatedMemoryUsage));
									}
									recordWriter.Rollback();
									this.recordWriterSizeLimit = Math.Min(this.recordWriterSizeLimit, recordWriter.EstimateSize());
									break;
								}
							}
							while ((flag = moveNext()) && num2 + num < 16000000L);
							if (num > 0L)
							{
								recordWriter.Write(rowGroupWriter, this.buffered);
								recordWriter.Clear();
							}
							hostTrace.Add("NumRows", rowGroupWriter.NumRows, false);
							hostTrace.Add("EstimatedSize", rowGroupWriter.EstimatedSize, false);
							hostTrace.Add("MoreRows", flag, false);
							hostTrace.Add("EndingCommittedNativeMemory", this.committedNativeMemory, false);
							hostTrace.Add("EndingBytesAllocated", this.memoryPool.BytesAllocated, false);
							rowGroupWriter.Close();
							flag3 = flag;
						}
					}
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return flag3;
		}

		// Token: 0x06010DFC RID: 69116 RVA: 0x003A24E4 File Offset: 0x003A06E4
		private Func<ParquetRecordWriter, long> GetAddRows(IEnumerator<IValueReference> enumerator)
		{
			return delegate(ParquetRecordWriter recordWriter)
			{
				RecordValue asRecord = enumerator.Current.Value.AsRecord;
				recordWriter.Add(asRecord);
				return 1L;
			};
		}

		// Token: 0x06010DFD RID: 69117 RVA: 0x003A24FD File Offset: 0x003A06FD
		private Func<bool> GetMoveNext(IEnumerator<IValueReference> enumerator)
		{
			return new Func<bool>(enumerator.MoveNext);
		}

		// Token: 0x06010DFE RID: 69118 RVA: 0x003A250C File Offset: 0x003A070C
		private Action GetAccumulate(IEnumerator<IValueReference> enumerator, IAccumulator accumulator)
		{
			if (accumulator == null)
			{
				return null;
			}
			return delegate
			{
				accumulator.AccumulateNext(enumerator.Current.Value.AsRecord);
			};
		}

		// Token: 0x06010DFF RID: 69119 RVA: 0x003A2543 File Offset: 0x003A0743
		private Func<ParquetRecordWriter, long> GetAddRows(IPage page)
		{
			this.exceptionRow = null;
			return delegate(ParquetRecordWriter recordWriter)
			{
				KeyValuePair<int, IExceptionRow> keyValuePair = page.ExceptionRows.OrderBy((KeyValuePair<int, IExceptionRow> kvp) => kvp.Key).FirstOrDefault((KeyValuePair<int, IExceptionRow> kvp) => kvp.Value.Exceptions.Values.Any<ISerializedException>());
				int num = page.RowCount;
				if (keyValuePair.Value != null)
				{
					this.exceptionRow = keyValuePair.Value;
					num = keyValuePair.Key;
				}
				recordWriter.Add(page, num);
				return (long)num;
			};
		}

		// Token: 0x06010E00 RID: 69120 RVA: 0x003A256A File Offset: 0x003A076A
		private Func<bool> GetMoveNext(IPageReader pageReader, IPage page)
		{
			return delegate
			{
				if (this.exceptionRow != null)
				{
					throw PageExceptionSerializer.GetExceptionFromProperties(this.exceptionRow.Exceptions.Values.First<ISerializedException>());
				}
				pageReader.Read(page);
				if (page.PageException != null)
				{
					throw PageExceptionSerializer.GetExceptionFromProperties(page.PageException);
				}
				return page.RowCount > 0;
			};
		}

		// Token: 0x06010E01 RID: 69121 RVA: 0x003A2594 File Offset: 0x003A0794
		private ValueException InsufficientMemory(string context, long? estimatedUsage)
		{
			ValueException ex;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Parquet/Write/OOM", TraceEventType.Information, null))
			{
				hostTrace.Add("BytesAllocated", this.memoryPool.BytesAllocated, false);
				hostTrace.Add("CommitedNativeMemory", this.committedNativeMemory, false);
				hostTrace.Add("Target", this.memoryTarget, false);
				hostTrace.Add("EstimatedUsage", estimatedUsage, false);
				long num;
				long num2;
				this.GetAvailableMemory(false, true, out num, out num2);
				hostTrace.Add("MaxWorkingSet", this.maxWorkingSet, false);
				hostTrace.Add("GCMemory", num, false);
				hostTrace.Add("PrivateMemorySize", num2, false);
				hostTrace.Add("ProcessCommitLimit", this.processCommitLimit, false);
				hostTrace.Add("JobCommitLimit", this.jobCommitLimit, false);
				ex = ValueException.NewDataFormatError<Message0>(Resources.WriteInsufficientMemory, Value.Null, null);
			}
			return ex;
		}

		// Token: 0x06010E02 RID: 69122 RVA: 0x003A26B4 File Offset: 0x003A08B4
		private bool IncreaseCommittedNativeMemory(long moreThan, bool lastResort = false)
		{
			bool flag;
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Parquet/Write/RowGroup/IncreaseCommitNativeMemory", TraceEventType.Information, null))
			{
				long bytesAllocated = this.memoryPool.BytesAllocated;
				hostTrace.Add("CurrentCommittedNativeMemory", this.committedNativeMemory, false);
				hostTrace.Add("BytesAllocated", bytesAllocated, false);
				hostTrace.Add("RequestedMinimumIncrease", moreThan, false);
				hostTrace.Add("Target", this.memoryTarget, false);
				if (this.endRowGroup)
				{
					hostTrace.Add("AfterEndRowGroup", this.endRowGroup, false);
					flag = false;
				}
				else
				{
					long num = this.committedNativeMemory;
					long num2 = Math.Max(this.committedNativeMemory - bytesAllocated + 1073741824L, moreThan);
					if (num2 > 0L)
					{
						long availableMemory = this.GetAvailableMemory(lastResort, lastResort);
						hostTrace.Add("AvailableMemory", availableMemory, false);
						hostTrace.Add("LastResort", lastResort, false);
						num2 = Math.Min(availableMemory, num2);
						if (num2 <= moreThan)
						{
							hostTrace.Add("InsufficientMemory", true, false);
							return false;
						}
						this.committedNativeMemory = bytesAllocated + num2;
						this.CheckCommittedNativeMemory();
						num2 = this.committedNativeMemory - bytesAllocated;
					}
					bool flag2 = num2 > moreThan;
					if (flag2)
					{
						this.memoryPressure.Add(this.committedNativeMemory - num);
					}
					if (flag2 && this.committedNativeMemory - num < 1073741824L)
					{
						this.endRowGroup = true;
						hostTrace.Add("EndRowGroup", this.endRowGroup, false);
					}
					hostTrace.Add("IncreasedCommittedNativeMemory", this.committedNativeMemory, false);
					hostTrace.Add("Success", flag2, false);
					flag = flag2;
				}
			}
			return flag;
		}

		// Token: 0x06010E03 RID: 69123 RVA: 0x003A2894 File Offset: 0x003A0A94
		private void CheckCommittedNativeMemory()
		{
			long bytesAllocated = this.memoryPool.BytesAllocated;
			if (this.committedNativeMemory > bytesAllocated)
			{
				long num = this.committedNativeMemory - bytesAllocated;
				num = this.CommitToNativeMemory(num);
				this.committedNativeMemory = num + bytesAllocated;
			}
		}

		// Token: 0x06010E04 RID: 69124 RVA: 0x003A28D0 File Offset: 0x003A0AD0
		private long GetAvailableMemory(bool forceFullCollection = false, bool lastResort = false)
		{
			long num;
			long num2;
			return this.GetAvailableMemory(forceFullCollection, lastResort, out num, out num2);
		}

		// Token: 0x06010E05 RID: 69125 RVA: 0x003A28EC File Offset: 0x003A0AEC
		private long GetAvailableMemory(bool forceFullCollection, bool lastResort, out long gcMemory, out long privateMemorySize)
		{
			privateMemorySize = -1L;
			gcMemory = -1L;
			long num = long.MaxValue;
			if (this.jobCommitLimit >= 0L)
			{
				num = this.jobCommitLimit / 2L;
			}
			if (this.processCommitLimit >= 0L)
			{
				num = Math.Min(num, this.processCommitLimit);
			}
			this.process.Refresh();
			privateMemorySize = this.process.PrivateMemorySize64;
			gcMemory = GC.GetTotalMemory(forceFullCollection);
			long num2 = Math.Min(gcMemory + this.memoryPool.BytesAllocated, privateMemorySize);
			long num3 = long.MaxValue;
			if (num < 9223372036854775807L)
			{
				num3 = num - num2;
				if (lastResort)
				{
					num3 = (long)((double)num3 * 0.8);
				}
				else
				{
					num3 /= 2L;
				}
			}
			return num3;
		}

		// Token: 0x06010E06 RID: 69126 RVA: 0x003A29A4 File Offset: 0x003A0BA4
		private long CalculateMemoryTarget()
		{
			if (this.maxWorkingSet < 0L)
			{
				return long.MaxValue;
			}
			if (this.maxWorkingSet > 8589934592L)
			{
				return 8589934592L;
			}
			if (this.maxWorkingSet >= (long)((ulong)(-2147483648)))
			{
				return this.maxWorkingSet;
			}
			if (this.maxWorkingSet >= 1073741824L)
			{
				return (long)((ulong)int.MinValue);
			}
			return this.maxWorkingSet + 1073741824L;
		}

		// Token: 0x06010E07 RID: 69127 RVA: 0x003A2A18 File Offset: 0x003A0C18
		private void InitMemoryLimits(out long maxWorkingSet, out long processCommitLimit, out long jobCommitLimit)
		{
			maxWorkingSet = -1L;
			processCommitLimit = -1L;
			jobCommitLimit = -1L;
			UIntPtr uintPtr;
			UIntPtr uintPtr2;
			uint num;
			if (ParquetScalableTableWriter.Interop.GetProcessWorkingSetSizeEx(this.process.Handle, out uintPtr, out uintPtr2, out num) && (num & 4U) != 0U)
			{
				maxWorkingSet = (long)(ulong)uintPtr2;
			}
			uint num2 = (uint)Marshal.SizeOf(typeof(ParquetScalableTableWriter.Interop.JobObjectExtendedLimitInformation));
			ParquetScalableTableWriter.Interop.JobObjectExtendedLimitInformation jobObjectExtendedLimitInformation;
			if (ParquetScalableTableWriter.Interop.QueryInformationJobObject(IntPtr.Zero, ParquetScalableTableWriter.Interop.JobObjectInfoClass.ExtendedLimitInformation, out jobObjectExtendedLimitInformation, num2, IntPtr.Zero))
			{
				if ((jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags & ParquetScalableTableWriter.Interop.JobObjectLimitFlags.PROCESS_MEMORY) != (ParquetScalableTableWriter.Interop.JobObjectLimitFlags)0U)
				{
					processCommitLimit = (long)(ulong)jobObjectExtendedLimitInformation.ProcessMemoryLimit;
				}
				if ((jobObjectExtendedLimitInformation.BasicLimitInformation.LimitFlags & ParquetScalableTableWriter.Interop.JobObjectLimitFlags.JOB_MEMORY) != (ParquetScalableTableWriter.Interop.JobObjectLimitFlags)0U)
				{
					jobCommitLimit = (long)(ulong)jobObjectExtendedLimitInformation.JobMemoryLimit;
				}
			}
		}

		// Token: 0x06010E08 RID: 69128 RVA: 0x003A2AC0 File Offset: 0x003A0CC0
		private long CommitToNativeMemory(long bytesToBeAvailable)
		{
			while (bytesToBeAvailable > 16777216L)
			{
				if (this.memoryPool.TestAvailableMemory(bytesToBeAvailable))
				{
					return bytesToBeAvailable;
				}
				long num = bytesToBeAvailable / 4L;
				long num2 = bytesToBeAvailable / 2L;
				bytesToBeAvailable = num2 + num;
				if (this.memoryPool.TestAvailableMemory(bytesToBeAvailable))
				{
					return bytesToBeAvailable;
				}
				bytesToBeAvailable = num2;
			}
			return 0L;
		}

		// Token: 0x04006563 RID: 25955
		public const string DataSourceName = "Parquet";

		// Token: 0x04006564 RID: 25956
		private const long MaxRowGroupRowCount = 16000000L;

		// Token: 0x04006565 RID: 25957
		private const long MaxTargetCommitMemory = 8589934592L;

		// Token: 0x04006566 RID: 25958
		private const long MinTargetCommitMemory = 1073741824L;

		// Token: 0x04006567 RID: 25959
		private const long IdealTargetCommitMemory = 2147483648L;

		// Token: 0x04006568 RID: 25960
		private const long NativeMemoryCommitmentIncrement = 1073741824L;

		// Token: 0x04006569 RID: 25961
		private const long VOrderAllocationDoublingTransitionPoint = 250000000L;

		// Token: 0x0400656A RID: 25962
		private const long NoDefinedLimit = 9223372036854775807L;

		// Token: 0x0400656B RID: 25963
		private static string engineName;

		// Token: 0x0400656C RID: 25964
		private readonly IEngineHost host;

		// Token: 0x0400656D RID: 25965
		private readonly bool buffered;

		// Token: 0x0400656E RID: 25966
		private readonly bool shuffled;

		// Token: 0x0400656F RID: 25967
		private readonly MemoryPool memoryPool;

		// Token: 0x04006570 RID: 25968
		private readonly ParquetScalableTableWriter.MemoryPressure memoryPressure;

		// Token: 0x04006571 RID: 25969
		private readonly Process process;

		// Token: 0x04006572 RID: 25970
		private readonly long maxWorkingSet;

		// Token: 0x04006573 RID: 25971
		private readonly long processCommitLimit;

		// Token: 0x04006574 RID: 25972
		private readonly long jobCommitLimit;

		// Token: 0x04006575 RID: 25973
		private readonly long memoryTarget;

		// Token: 0x04006576 RID: 25974
		private long recordWriterSizeLimit = long.MaxValue;

		// Token: 0x04006577 RID: 25975
		private long committedNativeMemory;

		// Token: 0x04006578 RID: 25976
		private bool endRowGroup;

		// Token: 0x04006579 RID: 25977
		private IExceptionRow exceptionRow;

		// Token: 0x02001F65 RID: 8037
		private sealed class MemoryPressure : IDisposable
		{
			// Token: 0x06010E09 RID: 69129 RVA: 0x003A2B0C File Offset: 0x003A0D0C
			public MemoryPressure(long memoryPressure = 0L)
			{
				this.Add(memoryPressure);
			}

			// Token: 0x06010E0A RID: 69130 RVA: 0x003A2B1B File Offset: 0x003A0D1B
			public void Add(long memoryPressure)
			{
				if (memoryPressure > 0L)
				{
					this.memoryPressure += memoryPressure;
					GC.AddMemoryPressure(memoryPressure);
				}
			}

			// Token: 0x06010E0B RID: 69131 RVA: 0x003A2B36 File Offset: 0x003A0D36
			public void Dispose()
			{
				if (this.memoryPressure > 0L)
				{
					GC.RemoveMemoryPressure(this.memoryPressure);
					this.memoryPressure = 0L;
				}
			}

			// Token: 0x0400657A RID: 25978
			private long memoryPressure;
		}

		// Token: 0x02001F66 RID: 8038
		private static class Interop
		{
			// Token: 0x06010E0C RID: 69132
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool GetProcessWorkingSetSizeEx(IntPtr hProcess, out UIntPtr lpMinimumWorkingSetSize, out UIntPtr lpMaximumWorkingSetSize, out uint flags);

			// Token: 0x06010E0D RID: 69133
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern bool QueryInformationJobObject(IntPtr hJob, ParquetScalableTableWriter.Interop.JobObjectInfoClass jobObectInfoClass, out ParquetScalableTableWriter.Interop.JobObjectExtendedLimitInformation lpJobObjectInfo, uint cbJobObjectInfoLength, IntPtr lpReturnLength);

			// Token: 0x0400657B RID: 25979
			public const uint QUOTA_LIMITS_HARDWS_MAX_ENABLE = 4U;

			// Token: 0x02001F67 RID: 8039
			public enum JobObjectInfoClass
			{
				// Token: 0x0400657D RID: 25981
				AssociateCompletionPortInformation = 7,
				// Token: 0x0400657E RID: 25982
				BasicLimitInformation = 2,
				// Token: 0x0400657F RID: 25983
				BasicUIRestrictions = 4,
				// Token: 0x04006580 RID: 25984
				CpuRateControlInformation = 15,
				// Token: 0x04006581 RID: 25985
				EndOfJobTimeInformation = 6,
				// Token: 0x04006582 RID: 25986
				ExtendedLimitInformation = 9,
				// Token: 0x04006583 RID: 25987
				GroupInformation = 11,
				// Token: 0x04006584 RID: 25988
				GroupInformationEx = 14,
				// Token: 0x04006585 RID: 25989
				LimitViolationInformation2 = 35,
				// Token: 0x04006586 RID: 25990
				NetRateControlInformation = 32,
				// Token: 0x04006587 RID: 25991
				NotificationLimitInformation,
				// Token: 0x04006588 RID: 25992
				NotificationLimitInformation2,
				// Token: 0x04006589 RID: 25993
				SecurityLimitInformation = 5
			}

			// Token: 0x02001F68 RID: 8040
			[Flags]
			public enum JobObjectLimitFlags : uint
			{
				// Token: 0x0400658B RID: 25995
				ACTIVE_PROCESS = 8U,
				// Token: 0x0400658C RID: 25996
				AFFINITY = 16U,
				// Token: 0x0400658D RID: 25997
				BREAKAWAY_OK = 2048U,
				// Token: 0x0400658E RID: 25998
				DIE_ON_UNHANDLED_EXCEPTION = 1024U,
				// Token: 0x0400658F RID: 25999
				JOB_MEMORY = 512U,
				// Token: 0x04006590 RID: 26000
				JOB_TIME = 4U,
				// Token: 0x04006591 RID: 26001
				KILL_ON_JOB_CLOSE = 8192U,
				// Token: 0x04006592 RID: 26002
				PRESERVE_JOB_TIME = 64U,
				// Token: 0x04006593 RID: 26003
				PRIORITY_CLASS = 32U,
				// Token: 0x04006594 RID: 26004
				PROCESS_MEMORY = 256U,
				// Token: 0x04006595 RID: 26005
				PROCESS_TIME = 2U,
				// Token: 0x04006596 RID: 26006
				SCHEDULING_CLASS = 128U,
				// Token: 0x04006597 RID: 26007
				SILENT_BREAKAWAY_OK = 4096U,
				// Token: 0x04006598 RID: 26008
				SUBSET_AFFINITY = 16384U,
				// Token: 0x04006599 RID: 26009
				WORKINGSET = 1U
			}

			// Token: 0x02001F69 RID: 8041
			public struct JobObjectBasicLimitInformation
			{
				// Token: 0x0400659A RID: 26010
				public long PerProcessUserTimeLimit;

				// Token: 0x0400659B RID: 26011
				public long PerJobUserTimeLimit;

				// Token: 0x0400659C RID: 26012
				public ParquetScalableTableWriter.Interop.JobObjectLimitFlags LimitFlags;

				// Token: 0x0400659D RID: 26013
				public UIntPtr MinimumWorkingSetSize;

				// Token: 0x0400659E RID: 26014
				public UIntPtr MaximumWorkingSetSize;

				// Token: 0x0400659F RID: 26015
				public uint ActiveProcessLimit;

				// Token: 0x040065A0 RID: 26016
				public UIntPtr Affinity;

				// Token: 0x040065A1 RID: 26017
				public uint PriorityClass;

				// Token: 0x040065A2 RID: 26018
				public uint SchedulingClass;
			}

			// Token: 0x02001F6A RID: 8042
			public struct JobObjectExtendedLimitInformation
			{
				// Token: 0x040065A3 RID: 26019
				public ParquetScalableTableWriter.Interop.JobObjectBasicLimitInformation BasicLimitInformation;

				// Token: 0x040065A4 RID: 26020
				private ParquetScalableTableWriter.Interop.IoCounters IoInfo;

				// Token: 0x040065A5 RID: 26021
				public UIntPtr ProcessMemoryLimit;

				// Token: 0x040065A6 RID: 26022
				public UIntPtr JobMemoryLimit;

				// Token: 0x040065A7 RID: 26023
				public UIntPtr PeakProcessMemoryUsed;

				// Token: 0x040065A8 RID: 26024
				public UIntPtr PeakJobMemoryUsed;
			}

			// Token: 0x02001F6B RID: 8043
			public struct IoCounters
			{
				// Token: 0x040065A9 RID: 26025
				public ulong ReadOperationCount;

				// Token: 0x040065AA RID: 26026
				public ulong WriteOperationCount;

				// Token: 0x040065AB RID: 26027
				public ulong OtherOperationCount;

				// Token: 0x040065AC RID: 26028
				public ulong ReadTransferCount;

				// Token: 0x040065AD RID: 26029
				public ulong WriteTransferCount;

				// Token: 0x040065AE RID: 26030
				public ulong OtherTransferCount;
			}
		}
	}
}
