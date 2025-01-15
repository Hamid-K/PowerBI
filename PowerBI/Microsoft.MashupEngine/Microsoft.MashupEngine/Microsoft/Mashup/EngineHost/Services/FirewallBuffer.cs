using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019E2 RID: 6626
	internal class FirewallBuffer : IDataReaderSource, IDisposable, ITableSource
	{
		// Token: 0x0600A7B2 RID: 42930 RVA: 0x0022B0CB File Offset: 0x002292CB
		public FirewallBuffer(IEngineHost engineHost, IEngine engine, TempFileManager tempFileManager, string key)
		{
			this.engineHost = engineHost;
			this.engine = engine;
			this.tempFileManager = tempFileManager;
			this.fileName = tempFileManager.GenerateKey();
			this.key = key;
		}

		// Token: 0x0600A7B3 RID: 42931 RVA: 0x0022B0FC File Offset: 0x002292FC
		public void SetDataReaderSource(IDataReaderSource dataReaderSource, Action<int> writeCallback)
		{
			try
			{
				this.exception = null;
				this.valueShape = dataReaderSource.TableSource.ValueShape;
				this.columnCount = dataReaderSource.TableSource.ColumnCount;
				this.keyColumnNames = new List<string>(dataReaderSource.TableSource.KeyColumnNames);
				this.relationships = new List<IRelationship>();
				foreach (IRelationship relationship in dataReaderSource.TableSource.Relationships)
				{
					this.relationships.Add(FirewallBuffer.BufferedRelationship.New(relationship));
				}
				this.columnIdentities = new IColumnIdentity[this.columnCount];
				for (int i = 0; i < this.columnCount; i++)
				{
					this.columnIdentities[i] = FirewallBuffer.BufferedColumnIdentity.New(dataReaderSource.TableSource.ColumnIdentity(i));
				}
				using (Stream stream = this.CreateStream(writeCallback))
				{
					using (SinglePageReader singlePageReader = new SinglePageReader(dataReaderSource.PageReader))
					{
						using (OleDbPageWriter oleDbPageWriter = new OleDbPageWriter(stream, singlePageReader.Schema, singlePageReader.MaxPageRowCount))
						{
							do
							{
								singlePageReader.Read();
								oleDbPageWriter.Write(singlePageReader.Page);
							}
							while (singlePageReader.Page.RowCount > 0);
							oleDbPageWriter.Flush();
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw new HostingException(ex.Message, "BufferingError");
			}
		}

		// Token: 0x0600A7B4 RID: 42932 RVA: 0x0022B2DC File Offset: 0x002294DC
		public void SetException(ValueException2 exception, Action<int> writeCallback)
		{
			try
			{
				this.exception = null;
				IGetStackFrameExtendedInfo getStackFrameExtendedInfo = this.engineHost.QueryService<IGetStackFrameExtendedInfo>();
				string text = ValueSerializer.SerializePreviewException(this.engine, exception, getStackFrameExtendedInfo);
				try
				{
					ValueDeserializer.Deserialize(this.engine, text);
				}
				catch (ValueException2 valueException)
				{
					this.exception = valueException;
				}
				if (this.exception == null)
				{
					throw new ArgumentException(Strings.Firewall_CouldNotSerializeException, "exception");
				}
			}
			catch (IOException ex)
			{
				throw new HostingException(ex.Message, "BufferingError");
			}
			catch (Exception ex2)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("FirewallBuffer/SetException", null, TraceEventType.Information, null))
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, ex2))
					{
						throw;
					}
				}
				throw this.NewBufferingException(ex2);
			}
		}

		// Token: 0x0600A7B5 RID: 42933 RVA: 0x0022B3B4 File Offset: 0x002295B4
		private Exception NewBufferingException(Exception e)
		{
			return new ValueBufferingException(Strings.Firewall_Buffering_Failed(this.key, e.Message), e);
		}

		// Token: 0x0600A7B6 RID: 42934 RVA: 0x0022B3CD File Offset: 0x002295CD
		private Stream CreateStream(Action<int> writeCallback)
		{
			return new FirewallBuffer.WriteMonitorStream(this.tempFileManager.Create(this.fileName), writeCallback);
		}

		// Token: 0x17002AAB RID: 10923
		// (get) Token: 0x0600A7B7 RID: 42935 RVA: 0x0022B3E6 File Offset: 0x002295E6
		ITableSource IDataReaderSource.TableSource
		{
			get
			{
				this.CheckException();
				return this;
			}
		}

		// Token: 0x17002AAC RID: 10924
		// (get) Token: 0x0600A7B8 RID: 42936 RVA: 0x0022B3EF File Offset: 0x002295EF
		ValueShape ITableSource.ValueShape
		{
			get
			{
				this.CheckException();
				return this.valueShape;
			}
		}

		// Token: 0x17002AAD RID: 10925
		// (get) Token: 0x0600A7B9 RID: 42937 RVA: 0x0022B3FD File Offset: 0x002295FD
		int ITableSource.ColumnCount
		{
			get
			{
				this.CheckException();
				return this.columnCount;
			}
		}

		// Token: 0x17002AAE RID: 10926
		// (get) Token: 0x0600A7BA RID: 42938 RVA: 0x0022B40B File Offset: 0x0022960B
		IEnumerable<string> ITableSource.KeyColumnNames
		{
			get
			{
				this.CheckException();
				return this.keyColumnNames;
			}
		}

		// Token: 0x17002AAF RID: 10927
		// (get) Token: 0x0600A7BB RID: 42939 RVA: 0x0022B419 File Offset: 0x00229619
		IEnumerable<IRelationship> ITableSource.Relationships
		{
			get
			{
				this.CheckException();
				return this.relationships;
			}
		}

		// Token: 0x0600A7BC RID: 42940 RVA: 0x0022B427 File Offset: 0x00229627
		IColumnIdentity ITableSource.ColumnIdentity(int index)
		{
			this.CheckException();
			return this.columnIdentities[index];
		}

		// Token: 0x17002AB0 RID: 10928
		// (get) Token: 0x0600A7BD RID: 42941 RVA: 0x0022B437 File Offset: 0x00229637
		IPageReader IDataReaderSource.PageReader
		{
			get
			{
				this.CheckException();
				return new OleDbPageReader(this.tempFileManager.Open(this.fileName));
			}
		}

		// Token: 0x0600A7BE RID: 42942 RVA: 0x0000336E File Offset: 0x0000156E
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600A7BF RID: 42943 RVA: 0x0022B455 File Offset: 0x00229655
		private void CheckException()
		{
			if (this.exception != null)
			{
				throw this.exception;
			}
		}

		// Token: 0x04005756 RID: 22358
		private readonly IEngineHost engineHost;

		// Token: 0x04005757 RID: 22359
		private readonly IEngine engine;

		// Token: 0x04005758 RID: 22360
		private readonly TempFileManager tempFileManager;

		// Token: 0x04005759 RID: 22361
		private readonly string fileName;

		// Token: 0x0400575A RID: 22362
		private readonly string key;

		// Token: 0x0400575B RID: 22363
		private ValueException2 exception;

		// Token: 0x0400575C RID: 22364
		private ValueShape valueShape;

		// Token: 0x0400575D RID: 22365
		private int columnCount;

		// Token: 0x0400575E RID: 22366
		private List<string> keyColumnNames;

		// Token: 0x0400575F RID: 22367
		private List<IRelationship> relationships;

		// Token: 0x04005760 RID: 22368
		private IColumnIdentity[] columnIdentities;

		// Token: 0x020019E3 RID: 6627
		private class BufferedRelationship : IRelationship
		{
			// Token: 0x0600A7C0 RID: 42944 RVA: 0x0022B468 File Offset: 0x00229668
			public static FirewallBuffer.BufferedRelationship New(IRelationship relationship)
			{
				int[] array = new int[relationship.KeyColumnCount];
				FirewallBuffer.BufferedColumnIdentity[] array2 = new FirewallBuffer.BufferedColumnIdentity[relationship.KeyColumnCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = relationship.KeyColumn(i);
					array2[i] = FirewallBuffer.BufferedColumnIdentity.New(relationship.OtherKeyColumn(i));
				}
				return new FirewallBuffer.BufferedRelationship(array, array2);
			}

			// Token: 0x0600A7C1 RID: 42945 RVA: 0x0022B4BB File Offset: 0x002296BB
			private BufferedRelationship(int[] keyColumns, FirewallBuffer.BufferedColumnIdentity[] otherKeyColumns)
			{
				this.keyColumns = keyColumns;
				this.otherKeyColumns = otherKeyColumns;
			}

			// Token: 0x17002AB1 RID: 10929
			// (get) Token: 0x0600A7C2 RID: 42946 RVA: 0x0022B4D1 File Offset: 0x002296D1
			public int KeyColumnCount
			{
				get
				{
					return this.keyColumns.Length;
				}
			}

			// Token: 0x0600A7C3 RID: 42947 RVA: 0x0022B4DB File Offset: 0x002296DB
			public int KeyColumn(int index)
			{
				return this.keyColumns[index];
			}

			// Token: 0x0600A7C4 RID: 42948 RVA: 0x0022B4E5 File Offset: 0x002296E5
			public IColumnIdentity OtherKeyColumn(int index)
			{
				return this.otherKeyColumns[index];
			}

			// Token: 0x04005761 RID: 22369
			private readonly int[] keyColumns;

			// Token: 0x04005762 RID: 22370
			private readonly FirewallBuffer.BufferedColumnIdentity[] otherKeyColumns;
		}

		// Token: 0x020019E4 RID: 6628
		private class BufferedColumnIdentity : IColumnIdentity
		{
			// Token: 0x0600A7C5 RID: 42949 RVA: 0x0022B4EF File Offset: 0x002296EF
			public static FirewallBuffer.BufferedColumnIdentity New(IColumnIdentity columnIdentity)
			{
				if (columnIdentity != null)
				{
					return new FirewallBuffer.BufferedColumnIdentity(columnIdentity.Identity);
				}
				return null;
			}

			// Token: 0x0600A7C6 RID: 42950 RVA: 0x0022B501 File Offset: 0x00229701
			private BufferedColumnIdentity(string identity)
			{
				this.identity = identity;
			}

			// Token: 0x17002AB2 RID: 10930
			// (get) Token: 0x0600A7C7 RID: 42951 RVA: 0x0022B510 File Offset: 0x00229710
			public string Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x04005763 RID: 22371
			private readonly string identity;
		}

		// Token: 0x020019E5 RID: 6629
		private class WriteMonitorStream : Stream, IDisposable
		{
			// Token: 0x0600A7C8 RID: 42952 RVA: 0x0022B518 File Offset: 0x00229718
			public WriteMonitorStream(Stream stream, Action<int> writeCallback)
			{
				this.stream = stream;
				this.writeCallback = writeCallback;
			}

			// Token: 0x17002AB3 RID: 10931
			// (get) Token: 0x0600A7C9 RID: 42953 RVA: 0x0022B52E File Offset: 0x0022972E
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17002AB4 RID: 10932
			// (get) Token: 0x0600A7CA RID: 42954 RVA: 0x0022B53B File Offset: 0x0022973B
			public override bool CanSeek
			{
				get
				{
					return this.stream.CanSeek;
				}
			}

			// Token: 0x17002AB5 RID: 10933
			// (get) Token: 0x0600A7CB RID: 42955 RVA: 0x0022B548 File Offset: 0x00229748
			public override bool CanWrite
			{
				get
				{
					return this.stream.CanWrite;
				}
			}

			// Token: 0x0600A7CC RID: 42956 RVA: 0x0022B555 File Offset: 0x00229755
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x17002AB6 RID: 10934
			// (get) Token: 0x0600A7CD RID: 42957 RVA: 0x0022B562 File Offset: 0x00229762
			public override long Length
			{
				get
				{
					return this.stream.Length;
				}
			}

			// Token: 0x17002AB7 RID: 10935
			// (get) Token: 0x0600A7CE RID: 42958 RVA: 0x0022B56F File Offset: 0x0022976F
			// (set) Token: 0x0600A7CF RID: 42959 RVA: 0x0022B57C File Offset: 0x0022977C
			public override long Position
			{
				get
				{
					return this.stream.Position;
				}
				set
				{
					this.stream.Position = value;
				}
			}

			// Token: 0x0600A7D0 RID: 42960 RVA: 0x0022B58A File Offset: 0x0022978A
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x0600A7D1 RID: 42961 RVA: 0x0022B597 File Offset: 0x00229797
			public new void Dispose()
			{
				this.stream.Dispose();
			}

			// Token: 0x0600A7D2 RID: 42962 RVA: 0x0022B5A4 File Offset: 0x002297A4
			public override int ReadByte()
			{
				return this.stream.ReadByte();
			}

			// Token: 0x0600A7D3 RID: 42963 RVA: 0x0022B5B1 File Offset: 0x002297B1
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.stream.Read(buffer, offset, count);
			}

			// Token: 0x0600A7D4 RID: 42964 RVA: 0x0022B5C1 File Offset: 0x002297C1
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.stream.Seek(offset, origin);
			}

			// Token: 0x0600A7D5 RID: 42965 RVA: 0x0022B5D0 File Offset: 0x002297D0
			public override void SetLength(long value)
			{
				this.stream.SetLength(value);
			}

			// Token: 0x0600A7D6 RID: 42966 RVA: 0x0022B5DE File Offset: 0x002297DE
			public override void WriteByte(byte value)
			{
				this.stream.WriteByte(value);
				this.writeCallback(1);
			}

			// Token: 0x0600A7D7 RID: 42967 RVA: 0x0022B5F8 File Offset: 0x002297F8
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.stream.Write(buffer, offset, count);
				this.writeCallback(count);
			}

			// Token: 0x04005764 RID: 22372
			private readonly Stream stream;

			// Token: 0x04005765 RID: 22373
			private readonly Action<int> writeCallback;
		}
	}
}
