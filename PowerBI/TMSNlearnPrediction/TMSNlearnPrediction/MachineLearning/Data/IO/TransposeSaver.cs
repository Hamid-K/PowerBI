using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020000DA RID: 218
	public sealed class TransposeSaver : IDataSaver
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00019680 File Offset: 0x00017880
		public TransposeSaver(TransposeSaver.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<TransposeSaver.Arguments>(env, args, "args");
			this._host = env.Register("TransposeSaver");
			this._internalSaver = new BinarySaver(new BinarySaver.Arguments
			{
				silent = true
			}, this._host);
			this._writeRowData = args.writeRowData;
			this._silent = args.silent;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000196F4 File Offset: 0x000178F4
		public bool IsColumnSavable(ColumnType type)
		{
			Contracts.CheckValue<ColumnType>(this._host, type, "type");
			if (type.IsVector && !type.IsKnownSizeVector)
			{
				return false;
			}
			ColumnType itemType = type.ItemType;
			PrimitiveType asPrimitive = itemType.AsPrimitive;
			if (asPrimitive == null)
			{
				return false;
			}
			VectorType vectorType = new VectorType(asPrimitive, 2);
			return this._internalSaver.IsColumnSavable(vectorType);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001974C File Offset: 0x0001794C
		public void SaveData(Stream stream, IDataView data, params int[] cols)
		{
			Contracts.CheckValue<Stream>(this._host, stream, "stream");
			Contracts.CheckValue<IDataView>(this._host, data, "data");
			Contracts.CheckParam(this._host, stream.CanSeek, "stream", "Must be seekable but is not");
			Contracts.CheckParam(this._host, stream.Position == 0L, "stream", "Stream must be at beginning but appears to not be");
			Contracts.CheckNonEmpty<int>(this._host, cols, "cols");
			Transposer transposer = Transposer.Create(this._host, data, false, cols);
			using (IChannel channel = this._host.Start("Saving"))
			{
				this.SaveTransposedData(channel, stream, transposer, cols);
				channel.Done();
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000198C8 File Offset: 0x00017AC8
		private unsafe void SaveTransposedData(IChannel ch, Stream stream, ITransposeDataView data, int[] cols)
		{
			TransposeLoader.Header header = default(TransposeLoader.Header);
			header.Signature = 6216168450219266136UL;
			header.Version = 281479271743489UL;
			header.CompatibleVersion = 281479271743489UL;
			VectorType slotType = data.TransposeSchema.GetSlotType(cols[0]);
			header.RowCount = (long)slotType.ValueCount;
			header.ColumnCount = cols.Length;
			List<long> offsets = new List<long>();
			byte[] array = new byte[256];
			stream.Write(array, 0, array.Length);
			offsets.Add(stream.Position);
			Action<string, IDataView> action = delegate(string name, IDataView view)
			{
				using (SubsetStream subsetStream = new SubsetStream(stream, null))
				{
					this._internalSaver.SaveData(subsetStream, view, Utils.GetIdentityPermutation(view.Schema.ColumnCount));
					subsetStream.Seek(0L, SeekOrigin.End);
					ch.Info("Wrote {0} data view in {1} bytes", new object[] { name, subsetStream.Length });
				}
				offsets.Add(stream.Position);
			};
			IDataView dataView = new ChooseColumnsByIndexTransform(new ChooseColumnsByIndexTransform.Arguments
			{
				index = cols
			}, this._host, data);
			if (!this._writeRowData)
			{
				dataView = SkipTakeFilter.Create(new SkipTakeFilter.TakeArguments
				{
					count = 0L
				}, this._host, dataView);
			}
			string text = (this._writeRowData ? "row-wise data, schema, and metadata" : "schema and metadata");
			action(text, dataView);
			foreach (int num in cols)
			{
				action(data.Schema.GetColumnName(num), new TransposerUtils.SlotDataView(this._host, data, num));
			}
			using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
			{
				header.SubIdvTableOffset = stream.Position;
				for (int j = 1; j < offsets.Count; j++)
				{
					binaryWriter.Write(offsets[j - 1]);
					binaryWriter.Write(offsets[j] - offsets[j - 1]);
				}
				header.TailOffset = stream.Position;
				binaryWriter.Write(6363673492537492566UL);
				Marshal.Copy(new IntPtr((void*)(&header)), array, 0, Marshal.SizeOf(typeof(Header)));
				binaryWriter.Seek(0, SeekOrigin.Begin);
				binaryWriter.Write(array);
			}
		}

		// Token: 0x040001F5 RID: 501
		internal const string Summary = "Writes data into a transposed binary TDV file.";

		// Token: 0x040001F6 RID: 502
		internal const string LoadName = "TransposeSaver";

		// Token: 0x040001F7 RID: 503
		private const ulong WriterVersion = 281479271743489UL;

		// Token: 0x040001F8 RID: 504
		private readonly IHost _host;

		// Token: 0x040001F9 RID: 505
		private readonly IDataSaver _internalSaver;

		// Token: 0x040001FA RID: 506
		private readonly bool _writeRowData;

		// Token: 0x040001FB RID: 507
		private readonly bool _silent;

		// Token: 0x020000DB RID: 219
		public sealed class Arguments
		{
			// Token: 0x040001FC RID: 508
			[Argument(4, HelpText = "Write a copy of the data in row-wise format, in addition to the transposed data. This will increase performance for mixed applications while taking significantly more space.", ShortName = "row")]
			public bool writeRowData = true;

			// Token: 0x040001FD RID: 509
			[Argument(4, HelpText = "Suppress any info output (not warnings or errors)", Hide = true)]
			public bool silent;
		}
	}
}
