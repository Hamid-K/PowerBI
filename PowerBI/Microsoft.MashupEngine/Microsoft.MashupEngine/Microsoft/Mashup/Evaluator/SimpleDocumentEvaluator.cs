using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D59 RID: 7513
	internal sealed class SimpleDocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>, IDocumentEvaluator<IValue>
	{
		// Token: 0x0600BAE4 RID: 47844 RVA: 0x0025D704 File Offset: 0x0025B904
		public SimpleDocumentEvaluator(IEngineHost engineHost, IEngine engine)
		{
			this.engineHost = new CompositeEngineHost(new IEngineHost[]
			{
				new SimpleEngineHost<IResourcePermissionService>(new ReportingResourcePermissionService(engineHost)),
				new SimpleEngineHost<ICultureService>(new ReportingCultureService(engineHost)),
				engineHost
			});
			this.engine = engine;
		}

		// Token: 0x0600BAE5 RID: 47845 RVA: 0x0025D744 File Offset: 0x0025B944
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			IHostTrace trace = EvaluatorTracing.CreatePerformanceTrace("SimpleDocumentEvaluator/GetResult<IPreviewValueSource>", this.engineHost, TraceEventType.Information, null);
			IEvaluation evaluation;
			try
			{
				evaluation = this.BeginGetResult(parameters, delegate(EvaluationResult2<IValue> result)
				{
					EvaluationResult2<IPreviewValueSource> evaluationResult;
					try
					{
						IPreviewValueSource previewValueSource = SimpleDocumentEvaluator.ValuePreviewValueSource.Create(this.engineHost, this.engine, result.Result, parameters.reportRelationships, new Action(this.ReportStalenessAndSampling));
						previewValueSource = previewValueSource.TraceTo(trace).AfterDispose(new Action(trace.Dispose));
						evaluationResult = new EvaluationResult2<IPreviewValueSource>(previewValueSource);
						trace.Suspend();
					}
					catch (Exception ex)
					{
						using (trace)
						{
							if (!SafeExceptions.TraceIsSafeException(trace, ex))
							{
								throw;
							}
						}
						evaluationResult = new EvaluationResult2<IPreviewValueSource>(ex.ToCallbackException());
					}
					callback(evaluationResult);
				});
			}
			catch
			{
				trace.Dispose();
				throw;
			}
			return evaluation;
		}

		// Token: 0x0600BAE6 RID: 47846 RVA: 0x0025D7C0 File Offset: 0x0025B9C0
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			IHostTrace trace = EvaluatorTracing.CreatePerformanceTrace("SimpleDocumentEvaluator/GetResult<IDataReaderSource>", this.engineHost, TraceEventType.Information, null);
			IEvaluation evaluation;
			try
			{
				evaluation = this.BeginGetResult(parameters, delegate(EvaluationResult2<IValue> result)
				{
					EvaluationResult2<IDataReaderSource> evaluationResult;
					try
					{
						IDataReaderSource dataReaderSource = ValueDataReaderSource.Create(this.engine, result.Result, parameters.reportRelationships);
						dataReaderSource = dataReaderSource.TraceTo(trace).AfterDispose(new Action(trace.Dispose));
						evaluationResult = new EvaluationResult2<IDataReaderSource>(dataReaderSource);
						trace.Suspend();
					}
					catch (Exception ex)
					{
						using (trace)
						{
							if (!SafeExceptions.TraceIsSafeException(trace, ex))
							{
								throw;
							}
						}
						evaluationResult = new EvaluationResult2<IDataReaderSource>(ex.ToCallbackException());
					}
					callback.InvokeThenOnDispose(evaluationResult, new Action(this.ReportStalenessAndSampling));
				});
			}
			catch
			{
				trace.Dispose();
				throw;
			}
			return evaluation;
		}

		// Token: 0x0600BAE7 RID: 47847 RVA: 0x0025D83C File Offset: 0x0025BA3C
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			IHostTrace trace = EvaluatorTracing.CreatePerformanceTrace("SimpleDocumentEvaluator/GetResult<IStreamSource>", this.engineHost, TraceEventType.Information, null);
			IEvaluation evaluation;
			try
			{
				evaluation = this.BeginGetResult(parameters, delegate(EvaluationResult2<IValue> result)
				{
					EvaluationResult2<IStreamSource> evaluationResult;
					try
					{
						IStreamSource streamSource = new SimpleDocumentEvaluator.ValueStreamSource(result.Result.AsBinary);
						streamSource = streamSource.TraceTo(trace).AfterDispose(new Action(trace.Dispose));
						evaluationResult = new EvaluationResult2<IStreamSource>(streamSource);
						trace.Suspend();
					}
					catch (Exception ex)
					{
						using (trace)
						{
							if (!SafeExceptions.TraceIsSafeException(trace, ex))
							{
								throw;
							}
						}
						evaluationResult = new EvaluationResult2<IStreamSource>(ex.ToEvaluationException());
					}
					callback.InvokeThenOnDispose(evaluationResult, new Action(this.ReportStalenessAndSampling));
				});
			}
			catch
			{
				trace.Dispose();
				throw;
			}
			return evaluation;
		}

		// Token: 0x0600BAE8 RID: 47848 RVA: 0x0025D8AC File Offset: 0x0025BAAC
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IValue>> callback)
		{
			EvaluationResult2<IValue> evaluationResult;
			try
			{
				parameters.SetUiCulture();
				IAssembly assembly;
				using (IHostTrace hostTrace = EvaluatorTracing.CreatePerformanceTrace("SimpleDocumentEvaluator/GetResult/Compile", this.engineHost, TraceEventType.Information, null))
				{
					hostTrace.Add("Expression", parameters.expression, true);
					string text = parameters.document.GetPartitionSection(parameters.partitionKey) ?? parameters.document.Package.SectionNames.First<string>();
					IPackageSectionConfig config = parameters.document.Package.GetSection(text).Config;
					DependencyCompiler dependencyCompiler = new DependencyCompiler(this.engine, this.engineHost, config, parameters.config.requiredModules);
					this.engineHost.QueryService<ILifetimeService>().Register(dependencyCompiler);
					dependencyCompiler.RemoveChangedDependenciesFromCaches();
					CompileOptions compileOptions = (parameters.config.debug ? CompileOptions.Debug : CompileOptions.None);
					List<IModule> list = new List<IModule>();
					List<IError> errors = new List<IError>();
					foreach (string text2 in parameters.document.Package.SectionNames)
					{
						IPackageSection section = parameters.document.Package.GetSection(text2);
						IModule module2;
						IModule module = dependencyCompiler.Compile(section, section.Text, section.Config, compileOptions, errors, out module2);
						list.Add(module);
						if (text2 == text)
						{
							list.Add(module2);
						}
					}
					IModule module3 = dependencyCompiler.Compile(new TextDocumentHost(parameters.expression), parameters.expression, config, compileOptions, errors);
					list.Add(module3);
					assembly = this.engine.Assemble(list, this.engine.EmptyRecord, dependencyCompiler.EngineHost, delegate(IError error)
					{
						errors.Add(error);
					});
					((CurrentEnvironmentService)dependencyCompiler.EngineHost.QueryService<ICurrentEnvironmentService>()).Environment = assembly.Exports;
					ISourceErrorExceptionService sourceErrorExceptionService = this.engineHost.QueryService<ISourceErrorExceptionService>();
					foreach (IError error2 in errors)
					{
						ValueException2 valueException;
						if (sourceErrorExceptionService.TryGetSourceErrorException(parameters.partitionKey, error2, out valueException))
						{
							throw valueException;
						}
					}
				}
				using (EvaluatorTracing.CreatePerformanceTrace("SimpleDocumentEvaluator/GetResult/Evaluate", this.engineHost, TraceEventType.Information, null))
				{
					IValue value = this.engine.Invoke(assembly.Function, Array.Empty<IValue>());
					if (value.IsAction && parameters.executeAction)
					{
						value = value.AsAction.Execute();
					}
					evaluationResult = new EvaluationResult2<IValue>(value);
				}
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace3 = EvaluatorTracing.CreateTrace("SimpleDocumentEvaluator/GetResult", this.engineHost, TraceEventType.Information, null))
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace3, ex))
					{
						throw;
					}
				}
				evaluationResult = new EvaluationResult2<IValue>(ex.ToCallbackException());
			}
			if (callback != null)
			{
				callback(evaluationResult);
			}
			return new EmptyEvaluation();
		}

		// Token: 0x0600BAE9 RID: 47849 RVA: 0x0025DC38 File Offset: 0x0025BE38
		private void ReportStalenessAndSampling()
		{
			IReportStaleness reportStaleness = this.engineHost.QueryService<IReportStaleness>();
			IReportSampling reportSampling = this.engineHost.QueryService<IReportSampling>();
			if (reportStaleness != null)
			{
				IPersistentCache persistentCache = this.engineHost.QueryService<ICacheSets>().Data.PersistentCache;
				reportStaleness.StaleSince(persistentCache.Staleness);
			}
			if (reportSampling != null && this.engineHost.QueryService<ISamplingService>().Sampled)
			{
				reportSampling.SamplingUsed();
			}
		}

		// Token: 0x04005F1B RID: 24347
		private readonly IEngineHost engineHost;

		// Token: 0x04005F1C RID: 24348
		private readonly IEngine engine;

		// Token: 0x02001D5A RID: 7514
		private static class ValuePreviewValueSource
		{
			// Token: 0x0600BAEA RID: 47850 RVA: 0x0025DC9D File Offset: 0x0025BE9D
			public static IPreviewValueSource Create(IEngineHost engineHost, IEngine engine, IValue value, bool reportRelationships, Action reportStalenessAndSampling)
			{
				if (value.IsTable)
				{
					return new SimpleDocumentEvaluator.ValuePreviewValueSource.TableValuePreviewValueSource(engineHost, engine, value.AsTable, reportRelationships, reportStalenessAndSampling);
				}
				return new SimpleDocumentEvaluator.ValuePreviewValueSource.NonTableValuePreviewValueSource(engineHost, engine, value, reportStalenessAndSampling);
			}

			// Token: 0x02001D5B RID: 7515
			private sealed class NonTableValuePreviewValueSource : IPreviewValueSource, IDisposable
			{
				// Token: 0x0600BAEB RID: 47851 RVA: 0x0025DCC2 File Offset: 0x0025BEC2
				public NonTableValuePreviewValueSource(IEngineHost engineHost, IEngine engine, IValue value, Action reportStalenessAndSampling)
				{
					this.engineHost = engineHost;
					this.engine = engine;
					this.value = value;
					this.reportStalenessAndSampling = reportStalenessAndSampling;
				}

				// Token: 0x17002E22 RID: 11810
				// (get) Token: 0x0600BAEC RID: 47852 RVA: 0x0025DCE7 File Offset: 0x0025BEE7
				public bool IsComplete
				{
					get
					{
						return this.serializedValue != null;
					}
				}

				// Token: 0x17002E23 RID: 11811
				// (get) Token: 0x0600BAED RID: 47853 RVA: 0x000020FA File Offset: 0x000002FA
				public ITableSource TableSource
				{
					get
					{
						return null;
					}
				}

				// Token: 0x17002E24 RID: 11812
				// (get) Token: 0x0600BAEE RID: 47854 RVA: 0x0025DCF2 File Offset: 0x0025BEF2
				public string SmallValue
				{
					get
					{
						return this.Value;
					}
				}

				// Token: 0x17002E25 RID: 11813
				// (get) Token: 0x0600BAEF RID: 47855 RVA: 0x0025DCFC File Offset: 0x0025BEFC
				public string Value
				{
					get
					{
						if (this.serializedValue == null)
						{
							try
							{
								Action<int, int> action = null;
								IProgressService2 progressService = this.engineHost.QueryService<IProgressService2>();
								if (progressService != null)
								{
									action = delegate(int rowCount, int errorRowCount)
									{
										progressService.RecordRowCount((long)rowCount, (long)errorRowCount);
									};
								}
								this.serializedValue = ValueSerializer.SerializePreviewValue(this.engine, this.value, action, new ValueSerializerOptions?(this.GetSerializerOptions()));
							}
							catch (ValueException2 valueException)
							{
								IGetStackFrameExtendedInfo getStackFrameExtendedInfo = this.engineHost.QueryService<IGetStackFrameExtendedInfo>();
								this.serializedValue = ValueSerializer.SerializePreviewException(this.engine, valueException, getStackFrameExtendedInfo);
							}
							this.reportStalenessAndSampling();
						}
						return this.serializedValue;
					}
				}

				// Token: 0x0600BAF0 RID: 47856 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x0600BAF1 RID: 47857 RVA: 0x0025DDAC File Offset: 0x0025BFAC
				private ValueSerializerOptions GetSerializerOptions()
				{
					return ValueSerializer.DefaultOptions;
				}

				// Token: 0x04005F1D RID: 24349
				private readonly IEngineHost engineHost;

				// Token: 0x04005F1E RID: 24350
				private readonly IEngine engine;

				// Token: 0x04005F1F RID: 24351
				private readonly IValue value;

				// Token: 0x04005F20 RID: 24352
				private readonly Action reportStalenessAndSampling;

				// Token: 0x04005F21 RID: 24353
				private string serializedValue;
			}

			// Token: 0x02001D5D RID: 7517
			private class TableValuePreviewValueSource : IPreviewValueSource, IDisposable, ITableSource
			{
				// Token: 0x0600BAF4 RID: 47860 RVA: 0x0025DDC4 File Offset: 0x0025BFC4
				public TableValuePreviewValueSource(IEngineHost engineHost, IEngine engine, ITableValue table, bool reportRelationships, Action reportStalenessAndSampling)
				{
					this.engineHost = engineHost;
					this.engine = engine;
					this.table = table;
					this.reportRelationships = reportRelationships;
					this.reportStalenessAndSampling = reportStalenessAndSampling;
				}

				// Token: 0x17002E26 RID: 11814
				// (get) Token: 0x0600BAF5 RID: 47861 RVA: 0x0025DDF1 File Offset: 0x0025BFF1
				public bool IsComplete
				{
					get
					{
						return this.serializedValue != null;
					}
				}

				// Token: 0x17002E27 RID: 11815
				// (get) Token: 0x0600BAF6 RID: 47862 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
				public ITableSource TableSource
				{
					get
					{
						return this;
					}
				}

				// Token: 0x17002E28 RID: 11816
				// (get) Token: 0x0600BAF7 RID: 47863 RVA: 0x00002105 File Offset: 0x00000305
				public ValueShape ValueShape
				{
					get
					{
						return ValueShape.Table;
					}
				}

				// Token: 0x17002E29 RID: 11817
				// (get) Token: 0x0600BAF8 RID: 47864 RVA: 0x0025DDFC File Offset: 0x0025BFFC
				public int ColumnCount
				{
					get
					{
						return this.table.Type.AsTableType.ItemType.Fields.Keys.Length;
					}
				}

				// Token: 0x17002E2A RID: 11818
				// (get) Token: 0x0600BAF9 RID: 47865 RVA: 0x0025DE22 File Offset: 0x0025C022
				public IEnumerable<string> KeyColumnNames
				{
					get
					{
						return this.table.Type.KeyColumnNames;
					}
				}

				// Token: 0x17002E2B RID: 11819
				// (get) Token: 0x0600BAFA RID: 47866 RVA: 0x0025DE34 File Offset: 0x0025C034
				public IEnumerable<IRelationship> Relationships
				{
					get
					{
						if (!this.reportRelationships)
						{
							return EmptyArray<IRelationship>.Instance;
						}
						return this.table.Relationships;
					}
				}

				// Token: 0x0600BAFB RID: 47867 RVA: 0x0025DE5C File Offset: 0x0025C05C
				public IColumnIdentity ColumnIdentity(int index)
				{
					return this.table.ColumnIdentity(index);
				}

				// Token: 0x17002E2C RID: 11820
				// (get) Token: 0x0600BAFC RID: 47868 RVA: 0x0025DE6A File Offset: 0x0025C06A
				public string SmallValue
				{
					get
					{
						if (this.serializedSmallValue == null)
						{
							this.serializedSmallValue = this.SerializeRows(200);
							if (this.inputComplete && this.serializedValue == null)
							{
								this.serializedValue = this.serializedSmallValue;
							}
						}
						return this.serializedSmallValue;
					}
				}

				// Token: 0x17002E2D RID: 11821
				// (get) Token: 0x0600BAFD RID: 47869 RVA: 0x0025DEA7 File Offset: 0x0025C0A7
				public string Value
				{
					get
					{
						if (this.serializedValue == null)
						{
							this.serializedValue = this.SerializeRows(int.MaxValue);
							if (this.serializedSmallValue == null)
							{
								this.serializedSmallValue = this.serializedValue;
							}
						}
						return this.serializedValue;
					}
				}

				// Token: 0x0600BAFE RID: 47870 RVA: 0x0025DEDC File Offset: 0x0025C0DC
				public void Dispose()
				{
					if (this.enumerator != null)
					{
						this.enumerator.Dispose();
						this.enumerator = null;
					}
				}

				// Token: 0x0600BAFF RID: 47871 RVA: 0x0025DEF8 File Offset: 0x0025C0F8
				private string SerializeRows(int count)
				{
					string text;
					try
					{
						Action<int, int> action = null;
						IProgressService2 progressService = this.engineHost.QueryService<IProgressService2>();
						if (progressService != null)
						{
							action = delegate(int rowCount, int errorRowCount)
							{
								progressService.RecordRowCount((long)rowCount, (long)errorRowCount);
							};
						}
						if (this.enumerator == null)
						{
							this.enumerator = this.table.GetEnumerator();
							this.items = new List<IValueReference2>();
							this.inputComplete = false;
						}
						try
						{
							while (!this.inputComplete && this.items.Count < count)
							{
								if (this.enumerator.MoveNext())
								{
									this.items.Add(this.enumerator.Current);
								}
								else
								{
									this.inputComplete = true;
								}
							}
						}
						catch (ValueException2 valueException)
						{
							if (this.items.Count == 0)
							{
								if (!(this.enumerator is SimpleDocumentEvaluator.ValuePreviewValueSource.TableValuePreviewValueSource.ExceptionEnumerator))
								{
									this.enumerator.Dispose();
									this.enumerator = new SimpleDocumentEvaluator.ValuePreviewValueSource.TableValuePreviewValueSource.ExceptionEnumerator(valueException);
								}
								throw;
							}
							this.items.Add(new SimpleDocumentEvaluator.ValuePreviewValueSource.TableValuePreviewValueSource.ExceptionValueReference(valueException));
							this.inputComplete = true;
						}
						ITableValue tableValue = this.engine.Table(this.table.Type, this.table.MetaValue, this.Relationships, ArrayHelpers.NewArray<IColumnIdentity>(this.ColumnCount, new Func<int, IColumnIdentity>(this.ColumnIdentity)), this.items.ToArray());
						text = ValueSerializer.SerializePreviewValue(this.engine, tableValue, action, null);
					}
					catch (ValueException2 valueException2)
					{
						IGetStackFrameExtendedInfo getStackFrameExtendedInfo = this.engineHost.QueryService<IGetStackFrameExtendedInfo>();
						text = ValueSerializer.SerializePreviewException(this.engine, valueException2, getStackFrameExtendedInfo);
					}
					this.reportStalenessAndSampling();
					return text;
				}

				// Token: 0x04005F23 RID: 24355
				private const int smallValueRowCount = 200;

				// Token: 0x04005F24 RID: 24356
				private readonly IEngineHost engineHost;

				// Token: 0x04005F25 RID: 24357
				private readonly IEngine engine;

				// Token: 0x04005F26 RID: 24358
				private readonly ITableValue table;

				// Token: 0x04005F27 RID: 24359
				private readonly bool reportRelationships;

				// Token: 0x04005F28 RID: 24360
				private readonly Action reportStalenessAndSampling;

				// Token: 0x04005F29 RID: 24361
				private IEnumerator<IValueReference2> enumerator;

				// Token: 0x04005F2A RID: 24362
				private List<IValueReference2> items;

				// Token: 0x04005F2B RID: 24363
				private bool inputComplete;

				// Token: 0x04005F2C RID: 24364
				private string serializedSmallValue;

				// Token: 0x04005F2D RID: 24365
				private string serializedValue;

				// Token: 0x02001D5E RID: 7518
				private sealed class ExceptionValueReference : IValueReference2
				{
					// Token: 0x0600BB00 RID: 47872 RVA: 0x0025E0BC File Offset: 0x0025C2BC
					public ExceptionValueReference(ValueException2 exception)
					{
						this.exception = exception;
					}

					// Token: 0x17002E2E RID: 11822
					// (get) Token: 0x0600BB01 RID: 47873 RVA: 0x00002105 File Offset: 0x00000305
					public bool Evaluated
					{
						get
						{
							return false;
						}
					}

					// Token: 0x17002E2F RID: 11823
					// (get) Token: 0x0600BB02 RID: 47874 RVA: 0x0025E0CB File Offset: 0x0025C2CB
					public IValue Value
					{
						get
						{
							throw this.exception;
						}
					}

					// Token: 0x04005F2E RID: 24366
					private ValueException2 exception;
				}

				// Token: 0x02001D5F RID: 7519
				private class ExceptionEnumerator : IEnumerator<IValueReference2>, IDisposable, IEnumerator
				{
					// Token: 0x0600BB03 RID: 47875 RVA: 0x0025E0D3 File Offset: 0x0025C2D3
					public ExceptionEnumerator(ValueException2 exception)
					{
						this.exception = exception;
					}

					// Token: 0x17002E30 RID: 11824
					// (get) Token: 0x0600BB04 RID: 47876 RVA: 0x0025E0E2 File Offset: 0x0025C2E2
					object IEnumerator.Current
					{
						get
						{
							throw this.exception;
						}
					}

					// Token: 0x17002E31 RID: 11825
					// (get) Token: 0x0600BB05 RID: 47877 RVA: 0x0025E0E2 File Offset: 0x0025C2E2
					public IValueReference2 Current
					{
						get
						{
							throw this.exception;
						}
					}

					// Token: 0x0600BB06 RID: 47878 RVA: 0x0025E0E2 File Offset: 0x0025C2E2
					public bool MoveNext()
					{
						throw this.exception;
					}

					// Token: 0x0600BB07 RID: 47879 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x0600BB08 RID: 47880 RVA: 0x0000EE09 File Offset: 0x0000D009
					public void Reset()
					{
						throw new InvalidOperationException();
					}

					// Token: 0x04005F2F RID: 24367
					private readonly ValueException2 exception;
				}
			}
		}

		// Token: 0x02001D61 RID: 7521
		private class ValueStreamSource : IStreamSource, IDisposable
		{
			// Token: 0x0600BB0B RID: 47883 RVA: 0x0025E0FB File Offset: 0x0025C2FB
			public ValueStreamSource(IBinaryValue value)
			{
				this.value = value;
			}

			// Token: 0x17002E32 RID: 11826
			// (get) Token: 0x0600BB0C RID: 47884 RVA: 0x0025E10A File Offset: 0x0025C30A
			public Stream Stream
			{
				get
				{
					if (this.value == null)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					if (this.stream == null)
					{
						this.stream = this.value.Open();
					}
					return this.stream;
				}
			}

			// Token: 0x0600BB0D RID: 47885 RVA: 0x0025E144 File Offset: 0x0025C344
			public void Dispose()
			{
				this.value = null;
				this.stream = null;
			}

			// Token: 0x04005F31 RID: 24369
			private IBinaryValue value;

			// Token: 0x04005F32 RID: 24370
			private Stream stream;
		}
	}
}
