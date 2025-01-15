using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D31 RID: 7473
	internal static class RemotePageReader
	{
		// Token: 0x0600BA2F RID: 47663 RVA: 0x0025B5BC File Offset: 0x002597BC
		public static void RunStub(IEngineHost engineHost, IMessageChannel channel, Func<IPageReader> getPageReader)
		{
			RemotePageReader.<>c__DisplayClass1_0 CS$<>8__locals1 = new RemotePageReader.<>c__DisplayClass1_0();
			CS$<>8__locals1.getPageReader = getPageReader;
			CS$<>8__locals1.engineHost = engineHost;
			using (Stream targetStream = new MessageBasedOutputStream(channel))
			{
				EvaluationHost.ReportExceptions("RemotePageReader/RunStub", CS$<>8__locals1.engineHost, channel, delegate
				{
					IPageReader pageReader = CS$<>8__locals1.getPageReader();
					bool flag = true;
					while (pageReader != null)
					{
						using (IPageReader pageReader2 = pageReader)
						{
							IPageReaderWithTableSource pageReaderWithTableSource = pageReader2 as IPageReaderWithTableSource;
							ITableSource tableSource = ((pageReaderWithTableSource != null) ? pageReaderWithTableSource.TableSource : null);
							new BinaryWriter(targetStream).WriteITableSource(tableSource);
							IProgressService2 progressService = CS$<>8__locals1.engineHost.QueryService<IProgressService>() as IProgressService2;
							OleDbPageWriter oleDbPageWriter = new OleDbPageWriter(targetStream, pageReader2.Schema, pageReader2.MaxPageRowCount);
							using (flag ? new ProgressMonitor(progressService, pageReader2.Progress) : null)
							{
								using (IPage page = pageReader2.CreatePage())
								{
									using (IPage page2 = pageReader2.CreatePage())
									{
										oleDbPageWriter.Flush();
										Action<Exception> action;
										if ((action = CS$<>8__locals1.<>9__1) == null)
										{
											action = (CS$<>8__locals1.<>9__1 = delegate(Exception exception)
											{
												IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemotePageReader/RunStub/onWriterException", CS$<>8__locals1.engineHost, TraceEventType.Information, null);
												try
												{
													if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, exception))
													{
														throw exception;
													}
													throw exception.ToCallbackException();
												}
												finally
												{
													if (hostTrace != null)
													{
														hostTrace.Dispose();
														goto IL_002E;
													}
													goto IL_002E;
													IL_002E:;
												}
											});
										}
										Action<Exception> action2 = action;
										using (OverlappingPageWriter overlappingPageWriter = new OverlappingPageWriter(new Action<ThreadStart>(EvaluatorThreadPool.Start), oleDbPageWriter, page, page2, action2, null))
										{
											bool flag2 = false;
											IPage page3;
											do
											{
												page3 = overlappingPageWriter.Page;
												pageReader2.Read(page3);
												overlappingPageWriter.Write();
												if (!flag2 && pageReader2.Progress.Rows >= 100L)
												{
													overlappingPageWriter.Flush();
													flag2 = true;
												}
											}
											while (page3.RowCount > 0);
											overlappingPageWriter.Flush();
											pageReader = pageReader2.NextResult();
											new BinaryWriter(targetStream).WriteBool(pageReader != null);
										}
									}
								}
							}
						}
						flag = false;
					}
				});
			}
		}

		// Token: 0x0600BA30 RID: 47664 RVA: 0x0025B640 File Offset: 0x00259840
		public static IPageReader CreateProxy(IEngineHost engineHost, IMessageChannel channel, ExceptionHandler exceptionHandler)
		{
			return RemotePageReader.CreatePageReader(new RemotePageReader.OwnedStream(new MessageBasedInputStream(channel, exceptionHandler)));
		}

		// Token: 0x0600BA31 RID: 47665 RVA: 0x0025B654 File Offset: 0x00259854
		private static RemotePageReader.PageReader CreatePageReader(RemotePageReader.OwnedStream stream)
		{
			RemotePageReader.PageReader pageReader2;
			try
			{
				ITableSource tableSource = new BinaryReader(stream.Stream).ReadITableSource();
				IPageReader pageReader = new OleDbPageReader(stream.Stream);
				pageReader2 = new RemotePageReader.PageReader(stream, pageReader, tableSource);
			}
			catch
			{
				stream.DisposeIfOwned();
				throw;
			}
			return pageReader2;
		}

		// Token: 0x04005EC2 RID: 24258
		private const int previewRowCount = 100;

		// Token: 0x02001D32 RID: 7474
		private sealed class PageReader : DelegatingPageReader, IPageReaderWithTableSource, IPageReader, IDisposable
		{
			// Token: 0x0600BA32 RID: 47666 RVA: 0x0025B6A4 File Offset: 0x002598A4
			public PageReader(RemotePageReader.OwnedStream stream, IPageReader pageReader, ITableSource tableSource)
				: base(pageReader.AfterDispose(new Action(stream.DisposeIfOwned)))
			{
				this.stream = stream;
				this.tableSource = tableSource;
			}

			// Token: 0x17002E07 RID: 11783
			// (get) Token: 0x0600BA33 RID: 47667 RVA: 0x0025B6CC File Offset: 0x002598CC
			public ITableSource TableSource
			{
				get
				{
					return this.tableSource;
				}
			}

			// Token: 0x0600BA34 RID: 47668 RVA: 0x0025B6D4 File Offset: 0x002598D4
			public override IPageReader NextResult()
			{
				if (new BinaryReader(this.stream.Stream).ReadBool())
				{
					return RemotePageReader.CreatePageReader(this.stream.TakeOwnership());
				}
				this.stream.DisposeIfOwned();
				return null;
			}

			// Token: 0x04005EC3 RID: 24259
			private readonly ITableSource tableSource;

			// Token: 0x04005EC4 RID: 24260
			private readonly RemotePageReader.OwnedStream stream;
		}

		// Token: 0x02001D33 RID: 7475
		private sealed class OwnedStream
		{
			// Token: 0x0600BA35 RID: 47669 RVA: 0x0025B70A File Offset: 0x0025990A
			public OwnedStream(Stream stream)
				: this(stream, stream.NonDisposable())
			{
			}

			// Token: 0x0600BA36 RID: 47670 RVA: 0x0025B719 File Offset: 0x00259919
			private OwnedStream(Stream disposableStream, Stream nonDisposableStream)
			{
				this.disposableStream = disposableStream;
				this.nonDisposableStream = nonDisposableStream;
			}

			// Token: 0x17002E08 RID: 11784
			// (get) Token: 0x0600BA37 RID: 47671 RVA: 0x0025B72F File Offset: 0x0025992F
			public Stream Stream
			{
				get
				{
					return this.nonDisposableStream;
				}
			}

			// Token: 0x0600BA38 RID: 47672 RVA: 0x0025B737 File Offset: 0x00259937
			public RemotePageReader.OwnedStream TakeOwnership()
			{
				RemotePageReader.OwnedStream ownedStream = new RemotePageReader.OwnedStream(this.disposableStream, this.nonDisposableStream);
				this.disposableStream = null;
				this.nonDisposableStream = null;
				return ownedStream;
			}

			// Token: 0x0600BA39 RID: 47673 RVA: 0x0025B758 File Offset: 0x00259958
			public void DisposeIfOwned()
			{
				if (this.disposableStream != null)
				{
					this.disposableStream.Dispose();
					this.disposableStream = null;
				}
			}

			// Token: 0x04005EC5 RID: 24261
			private Stream disposableStream;

			// Token: 0x04005EC6 RID: 24262
			private Stream nonDisposableStream;
		}
	}
}
