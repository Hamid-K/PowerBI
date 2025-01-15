using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000036 RID: 54
	public sealed class FileTracer : IFileTracer
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000B794 File Offset: 0x00009994
		static FileTracer()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				FileTracer.processId = currentProcess.Id.ToString("X8", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B7F4 File Offset: 0x000099F4
		public FileTracer(string tracePath)
		{
			this.tracePath = tracePath;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B803 File Offset: 0x00009A03
		private static int NextSequenceNumber
		{
			get
			{
				Interlocked.Increment(ref FileTracer.sequence);
				if (FileTracer.sequence == 1000)
				{
					FileTracer.sequence = 0;
				}
				return FileTracer.sequence;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B828 File Offset: 0x00009A28
		public void TraceMdxInfo(string mdx, List<MdxColumn> columns, string traceHash, object[][] columnInfos, string cubeName)
		{
			Utils.SwallowSafeExceptions(delegate
			{
				using (FileStream fileStream = File.Create(this.GetFilename("json", traceHash, 0, "MdxTraceInfo", "OK")))
				{
					FileTracer.jsonSerializer.WriteObject(fileStream, new MdxTraceInfo
					{
						MdxStatement = mdx,
						Columns = columns,
						ColumnInfos = columnInfos,
						CubeName = cubeName
					});
				}
			});
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B876 File Offset: 0x00009A76
		public void TraceRfcFunction(IRfcFunction function, List<string> skipElements, string filename)
		{
			Utils.SwallowSafeExceptions(delegate
			{
				using (BasXmlFunctionWriter basXmlFunctionWriter = new BasXmlFunctionWriter(filename, skipElements))
				{
					basXmlFunctionWriter.WriteFunction(function);
				}
			});
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public void TraceStatelessFunction(IRfcFunction function, string traceHash, string traceIdentifier, string status)
		{
			this.TraceRfcFunction(function, null, this.GetFilename("bxml", traceHash, 0, traceIdentifier, status));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B8C9 File Offset: 0x00009AC9
		public void TraceString(Func<string> getString, string filename)
		{
			Utils.SwallowSafeExceptions(delegate
			{
				File.WriteAllText(filename, getString());
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B8EE File Offset: 0x00009AEE
		public void TraceData(Func<byte[]> getData, string filename)
		{
			Utils.SwallowSafeExceptions(delegate
			{
				File.WriteAllBytes(filename, getData());
			});
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B914 File Offset: 0x00009B14
		public void TraceFunctionInvoke(IRfcFunction function, SapBwCommand command, bool? producedRfcException)
		{
			string name = function.Metadata.Name;
			string text = "OK";
			if (producedRfcException != null)
			{
				text = (producedRfcException.Value ? "EXC" : "MSG");
			}
			List<string> list = new List<string>();
			if (!(name == "BAPI_MDDATASET_GET_STREAMDATA"))
			{
				if (!(name == "RSR_MDX_BXML_GET_DATA"))
				{
					if (!(name == "RSR_MDX_BXML_GET_GZIP_DATA"))
					{
						if (name == "RSR_MDX_CREATE_OBJECT")
						{
							this.TraceString(() => command.CommandText, this.GetFilename("mdx", command.TraceHash, command.Step, name + "=MDX", text));
						}
					}
					else
					{
						list.Add("XML");
						this.TraceData(() => function.GetValue("XML") as byte[], this.GetFilename("bxml", command.TraceHash, command.Step, name + "=XML", text));
					}
				}
				else
				{
					list.Add("XML");
					this.TraceData(() => function.GetValue("XML") as byte[], this.GetFilename("bxml", command.TraceHash, command.Step, name + "=XML", text));
				}
			}
			else
			{
				list.Add("STREAM");
				this.TraceString(() => function.GetString("STREAM"), this.GetFilename("txt", command.TraceHash, command.Step, name + "=STREAM", text));
			}
			this.TraceRfcFunction(function, list, this.GetFilename("bxml", command.TraceHash, command.Step, name, text));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BB09 File Offset: 0x00009D09
		public static MdxTraceInfo ReadTrace(string filename)
		{
			return Utils.SwallowSafeExceptions<MdxTraceInfo>(delegate
			{
				if (File.Exists(filename))
				{
					using (FileStream fileStream = File.OpenRead(filename))
					{
						return (MdxTraceInfo)FileTracer.jsonSerializer.ReadObject(fileStream);
					}
				}
				return null;
			});
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BB27 File Offset: 0x00009D27
		public static MdxTraceInfo ReadTrace(Stream stream)
		{
			return Utils.SwallowSafeExceptions<MdxTraceInfo>(delegate
			{
				if (stream != null)
				{
					using (stream)
					{
						return (MdxTraceInfo)FileTracer.jsonSerializer.ReadObject(stream);
					}
				}
				return null;
			});
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BB48 File Offset: 0x00009D48
		public string GetFilename(string extension, string traceHash, int step, string functionName, string status)
		{
			return Path.Combine(this.tracePath, string.Format(CultureInfo.InvariantCulture, "{0}-{1:D3}-{2}-{3}-{4}-{5}-{6}.{7}", new object[]
			{
				DateTime.Now.ToString("yyyyMMdd'T'HHmmss.fff", CultureInfo.InvariantCulture),
				FileTracer.NextSequenceNumber,
				FileTracer.processId,
				traceHash,
				step.ToString("D2", CultureInfo.InvariantCulture),
				functionName,
				status,
				extension
			}));
		}

		// Token: 0x040001D2 RID: 466
		public const string StatusOk = "OK";

		// Token: 0x040001D3 RID: 467
		public const string StatusException = "EXC";

		// Token: 0x040001D4 RID: 468
		public const string StatusMessage = "MSG";

		// Token: 0x040001D5 RID: 469
		public const string TimestampFormat = "yyyyMMdd'T'HHmmss.fff";

		// Token: 0x040001D6 RID: 470
		private const string BxmlExtension = "bxml";

		// Token: 0x040001D7 RID: 471
		private const string TextExtension = "txt";

		// Token: 0x040001D8 RID: 472
		private const string JsonExtension = "json";

		// Token: 0x040001D9 RID: 473
		private const string MdxExtension = "mdx";

		// Token: 0x040001DA RID: 474
		private static readonly string processId;

		// Token: 0x040001DB RID: 475
		private static readonly DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(MdxTraceInfo));

		// Token: 0x040001DC RID: 476
		private static int sequence;

		// Token: 0x040001DD RID: 477
		private readonly string tracePath;
	}
}
