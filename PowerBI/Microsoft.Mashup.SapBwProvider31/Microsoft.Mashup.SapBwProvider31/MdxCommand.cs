using System;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000014 RID: 20
	internal sealed class MdxCommand : IDisposable
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000045BC File Offset: 0x000027BC
		public MdxCommand(SapBwCommand command)
		{
			this.command = command;
			this.connection = (SapBwConnection)command.Connection;
			this.mdx = command.CommandText;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000045E8 File Offset: 0x000027E8
		public string DataSetId
		{
			get
			{
				return this.datasetId;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000045F0 File Offset: 0x000027F0
		public void ExecuteMdx()
		{
			if (this.datasetId == null)
			{
				IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_CREATE_OBJECT", true);
				IRfcTable table = function.GetTable("COMMAND_TEXT");
				for (int i = 0; i < this.mdx.Length; i += 75)
				{
					table.Append();
					table.SetValue(0, (this.mdx.Length > i + 75) ? this.mdx.Substring(i, 75) : this.mdx.Substring(i));
				}
				this.connection.InvokeFunction(function, true, this.command, true);
				this.datasetId = function.GetString("DATASETID");
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000469C File Offset: 0x0000289C
		public void SelectData()
		{
			this.ExecuteMdx();
			IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_SELECT_DATA", true);
			function.SetValue("DATASETID", this.datasetId);
			this.connection.InvokeFunction(function, true, this.command, true);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000046E8 File Offset: 0x000028E8
		public MdxColumnProvider GetColumnProvider()
		{
			this.ExecuteMdx();
			ColumnInfos columnInfos = null;
			object[][] columnInfosParameterValue = null;
			object obj;
			if (this.command.TryGetParameterValue("COLUMNINFOS", out obj))
			{
				columnInfosParameterValue = obj as object[][];
				columnInfos = ColumnInfos.NewOrNull(this.connection, columnInfosParameterValue);
			}
			object cubeName;
			this.command.TryGetParameterValue("CUBENAME", out cubeName);
			MdxColumnProvider columnProvider = this.CreateColumnProvider(cubeName as string);
			if (columnProvider.TryRetrieveColumns(this.datasetId, columnInfos))
			{
				columnProvider.ApplyColumnInfos(columnInfos);
				this.connection.FileTrace(delegate(IFileTracer fileTracer)
				{
					fileTracer.TraceMdxInfo(this.command.CommandText, columnProvider.Columns, this.command.TraceHash, columnInfosParameterValue, cubeName as string);
				});
				return columnProvider;
			}
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000047B0 File Offset: 0x000029B0
		private MdxColumnProvider CreateColumnProvider(string cubeName)
		{
			switch (this.command.SapBwCommandType)
			{
			case SapBwCommandType.MdxBasXml:
			case SapBwCommandType.MdxBasXmlGzip:
				return new MdxBasXmlColumnProvider(this.command, this, cubeName);
			case SapBwCommandType.MdxDataStream:
				return new MdxStreamingColumnProvider(this.command, this, cubeName);
			case SapBwCommandType.MdxFlattening:
				return new MdxFlatteningColumnProvider(this.command, this, cubeName);
			case SapBwCommandType.MdxMultidimensional:
				return new MdxMultidimensionalColumnProvider(this.command, this, cubeName);
			default:
				throw this.connection.Helper.NewDataSourceError(Resources.InvalidCommandType);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000483A File Offset: 0x00002A3A
		public void Dispose()
		{
			if (!string.IsNullOrEmpty(this.datasetId))
			{
				Utils.SwallowSafeExceptions(delegate
				{
					try
					{
						IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_DELETE_OBJECT", true);
						function.SetValue("DATASETID", this.datasetId);
						this.connection.InvokeFunction(function, true, this.command, false);
					}
					finally
					{
						this.connection.EndContext();
					}
				});
				this.datasetId = null;
			}
		}

		// Token: 0x04000035 RID: 53
		private const int CommandTextLineLength = 75;

		// Token: 0x04000036 RID: 54
		private readonly SapBwCommand command;

		// Token: 0x04000037 RID: 55
		private readonly SapBwConnection connection;

		// Token: 0x04000038 RID: 56
		private readonly string mdx;

		// Token: 0x04000039 RID: 57
		private string datasetId;
	}
}
