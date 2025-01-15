using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000093 RID: 147
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WriterPropertiesBuilder : IDisposable
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000EE08 File Offset: 0x0000D008
		public WriterPropertiesBuilder()
		{
			IntPtr intPtr;
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Create(out intPtr));
			this._handle = new ParquetHandle(intPtr, new Action<IntPtr>(WriterPropertiesBuilder.WriterPropertiesBuilder_Free));
			this.ApplyDefaults();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public WriterProperties Build()
		{
			return new WriterProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(WriterPropertiesBuilder.WriterPropertiesBuilder_Build)));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public WriterPropertiesBuilder DisableDictionary()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Dictionary(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public WriterPropertiesBuilder DisableDictionary(string path)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Dictionary_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000EF04 File Offset: 0x0000D104
		public WriterPropertiesBuilder DisableDictionary(ColumnPath path)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Dictionary_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000EF38 File Offset: 0x0000D138
		public WriterPropertiesBuilder EnableDictionary()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Dictionary(this._handle.IntPtr));
			return this;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000EF50 File Offset: 0x0000D150
		public WriterPropertiesBuilder EnableDictionary(string path)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Dictionary_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public WriterPropertiesBuilder EnableDictionary(ColumnPath path)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Dictionary_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public WriterPropertiesBuilder DisableStatistics()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Statistics(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F00C File Offset: 0x0000D20C
		public WriterPropertiesBuilder DisableStatistics(string path)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Statistics_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F070 File Offset: 0x0000D270
		public WriterPropertiesBuilder DisableStatistics(ColumnPath path)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Statistics_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public WriterPropertiesBuilder EnableStatistics()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Statistics(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		public WriterPropertiesBuilder EnableStatistics(string path)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Statistics_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F12C File Offset: 0x0000D32C
		public WriterPropertiesBuilder EnableStatistics(ColumnPath path)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Statistics_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F160 File Offset: 0x0000D360
		public WriterPropertiesBuilder Compression(Compression codec)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression(this._handle.IntPtr, codec));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F184 File Offset: 0x0000D384
		public WriterPropertiesBuilder Compression(string path, Compression codec)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer), codec));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public WriterPropertiesBuilder Compression(ColumnPath path, Compression codec)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr, codec));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F220 File Offset: 0x0000D420
		public WriterPropertiesBuilder CompressionLevel(int compressionLevel)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression_Level(this._handle.IntPtr, compressionLevel));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F244 File Offset: 0x0000D444
		public WriterPropertiesBuilder CompressionLevel(string path, int compressionLevel)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression_Level_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer), compressionLevel));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		public WriterPropertiesBuilder CompressionLevel(ColumnPath path, int compressionLevel)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Compression_Level_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr, compressionLevel));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public WriterPropertiesBuilder CreatedBy(string createdBy)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Created_By(this._handle.IntPtr, StringUtil.ToCStringUtf8(createdBy, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000F344 File Offset: 0x0000D544
		public WriterPropertiesBuilder DataPagesize(long pageSize)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Data_Pagesize(this._handle.IntPtr, pageSize));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F368 File Offset: 0x0000D568
		public WriterPropertiesBuilder DictionaryPagesizeLimit(long dictionaryPagesizeLimit)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Dictionary_Pagesize_Limit(this._handle.IntPtr, dictionaryPagesizeLimit));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F38C File Offset: 0x0000D58C
		public WriterPropertiesBuilder Encoding(Encoding encoding)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Encoding(this._handle.IntPtr, encoding));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public WriterPropertiesBuilder Encoding(string path, Encoding encoding)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Encoding_By_Path(this._handle.IntPtr, StringUtil.ToCStringUtf8(path, byteBuffer), encoding));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F414 File Offset: 0x0000D614
		public WriterPropertiesBuilder Encoding(ColumnPath path, Encoding encoding)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Encoding_By_ColumnPath(this._handle.IntPtr, path.Handle.IntPtr, encoding));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(path);
			return this;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F44C File Offset: 0x0000D64C
		public WriterPropertiesBuilder Encryption([Nullable(2)] FileEncryptionProperties fileEncryptionProperties)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Encryption(this._handle.IntPtr, (fileEncryptionProperties != null) ? fileEncryptionProperties.Handle.IntPtr : IntPtr.Zero));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(fileEncryptionProperties);
			return this;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public WriterPropertiesBuilder MaxRowGroupLength(long maxRowGroupLength)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Max_Row_Group_Length(this._handle.IntPtr, maxRowGroupLength));
			return this;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public WriterPropertiesBuilder Version(ParquetVersion version)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Version(this._handle.IntPtr, version));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		public WriterPropertiesBuilder WriteBatchSize(long writeBatchSize)
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Write_Batch_Size(this._handle.IntPtr, writeBatchSize));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F504 File Offset: 0x0000D704
		public WriterPropertiesBuilder EnableWritePageIndex()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Write_Page_Index(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F528 File Offset: 0x0000D728
		public WriterPropertiesBuilder DisableWritePageIndex()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Write_Page_Index(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F54C File Offset: 0x0000D74C
		public WriterPropertiesBuilder DisableShuffle()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Disable_Shuffle(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F570 File Offset: 0x0000D770
		public WriterPropertiesBuilder EnableShuffle()
		{
			ExceptionInfo.Check(WriterPropertiesBuilder.WriterPropertiesBuilder_Enable_Shuffle(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F594 File Offset: 0x0000D794
		private void ApplyDefaults()
		{
			WriterPropertiesBuilder.OnDefaultProperty<bool>(DefaultWriterProperties.EnableDictionary, delegate(bool enabled)
			{
				if (enabled)
				{
					this.EnableDictionary();
					return;
				}
				this.DisableDictionary();
			});
			WriterPropertiesBuilder.OnDefaultProperty<bool>(DefaultWriterProperties.EnableStatistics, delegate(bool enabled)
			{
				if (enabled)
				{
					this.EnableStatistics();
					return;
				}
				this.DisableStatistics();
			});
			WriterPropertiesBuilder.OnDefaultProperty<Compression>(DefaultWriterProperties.Compression, delegate(Compression compression)
			{
				this.Compression(compression);
			});
			WriterPropertiesBuilder.OnDefaultProperty<int>(DefaultWriterProperties.CompressionLevel, delegate(int compressionLevel)
			{
				this.CompressionLevel(compressionLevel);
			});
			WriterPropertiesBuilder.OnDefaultRefProperty<string>(DefaultWriterProperties.CreatedBy, delegate(string createdBy)
			{
				this.CreatedBy(createdBy);
			});
			WriterPropertiesBuilder.OnDefaultProperty<long>(DefaultWriterProperties.DataPagesize, delegate(long dataPagesize)
			{
				this.DataPagesize(dataPagesize);
			});
			WriterPropertiesBuilder.OnDefaultProperty<long>(DefaultWriterProperties.DictionaryPagesizeLimit, delegate(long dictionaryPagesizeLimit)
			{
				this.DictionaryPagesizeLimit(dictionaryPagesizeLimit);
			});
			WriterPropertiesBuilder.OnDefaultProperty<Encoding>(DefaultWriterProperties.Encoding, delegate(Encoding encoding)
			{
				this.Encoding(encoding);
			});
			WriterPropertiesBuilder.OnDefaultProperty<long>(DefaultWriterProperties.MaxRowGroupLength, delegate(long maxRowGroupLength)
			{
				this.MaxRowGroupLength(maxRowGroupLength);
			});
			WriterPropertiesBuilder.OnDefaultProperty<ParquetVersion>(DefaultWriterProperties.Version, delegate(ParquetVersion version)
			{
				this.Version(version);
			});
			WriterPropertiesBuilder.OnDefaultProperty<long>(DefaultWriterProperties.WriteBatchSize, delegate(long writeBatchSize)
			{
				this.WriteBatchSize(writeBatchSize);
			});
			WriterPropertiesBuilder.OnDefaultProperty<bool>(DefaultWriterProperties.WritePageIndex, delegate(bool writePageIndex)
			{
				if (writePageIndex)
				{
					this.EnableWritePageIndex();
					return;
				}
				this.DisableWritePageIndex();
			});
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void OnDefaultProperty<T>(T? defaultPropertyValue, [Nullable(new byte[] { 1, 0 })] Action<T> setProperty) where T : struct
		{
			if (defaultPropertyValue != null)
			{
				setProperty(defaultPropertyValue.Value);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void OnDefaultRefProperty<T>(T defaultPropertyValue, [Nullable(1)] Action<T> setProperty)
		{
			if (defaultPropertyValue != null)
			{
				setProperty(defaultPropertyValue);
			}
		}

		// Token: 0x06000441 RID: 1089
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Create(out IntPtr builder);

		// Token: 0x06000442 RID: 1090
		[DllImport("ParquetSharpNative")]
		private static extern void WriterPropertiesBuilder_Free(IntPtr builder);

		// Token: 0x06000443 RID: 1091
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Build(IntPtr builder, out IntPtr writerProperties);

		// Token: 0x06000444 RID: 1092
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Dictionary(IntPtr builder);

		// Token: 0x06000445 RID: 1093
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Dictionary_By_Path(IntPtr builder, IntPtr path);

		// Token: 0x06000446 RID: 1094
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Dictionary_By_ColumnPath(IntPtr builder, IntPtr path);

		// Token: 0x06000447 RID: 1095
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Dictionary(IntPtr builder);

		// Token: 0x06000448 RID: 1096
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Dictionary_By_Path(IntPtr builder, IntPtr path);

		// Token: 0x06000449 RID: 1097
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Dictionary_By_ColumnPath(IntPtr builder, IntPtr path);

		// Token: 0x0600044A RID: 1098
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Statistics(IntPtr builder);

		// Token: 0x0600044B RID: 1099
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Statistics_By_Path(IntPtr builder, IntPtr path);

		// Token: 0x0600044C RID: 1100
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Statistics_By_ColumnPath(IntPtr builder, IntPtr path);

		// Token: 0x0600044D RID: 1101
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Statistics(IntPtr builder);

		// Token: 0x0600044E RID: 1102
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Statistics_By_Path(IntPtr builder, IntPtr path);

		// Token: 0x0600044F RID: 1103
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Statistics_By_ColumnPath(IntPtr builder, IntPtr path);

		// Token: 0x06000450 RID: 1104
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression(IntPtr builder, Compression codec);

		// Token: 0x06000451 RID: 1105
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression_By_Path(IntPtr builder, IntPtr path, Compression codec);

		// Token: 0x06000452 RID: 1106
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression_By_ColumnPath(IntPtr builder, IntPtr path, Compression codec);

		// Token: 0x06000453 RID: 1107
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression_Level(IntPtr builder, int compressionLevel);

		// Token: 0x06000454 RID: 1108
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression_Level_By_Path(IntPtr builder, IntPtr path, int compressionLevel);

		// Token: 0x06000455 RID: 1109
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Compression_Level_By_ColumnPath(IntPtr builder, IntPtr path, int compressionLevel);

		// Token: 0x06000456 RID: 1110
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Created_By(IntPtr builder, IntPtr createdBy);

		// Token: 0x06000457 RID: 1111
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Data_Pagesize(IntPtr builder, long pgSize);

		// Token: 0x06000458 RID: 1112
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Dictionary_Pagesize_Limit(IntPtr builder, long dictionaryPsizeLimit);

		// Token: 0x06000459 RID: 1113
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Encoding(IntPtr builder, Encoding encodingType);

		// Token: 0x0600045A RID: 1114
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Encoding_By_Path(IntPtr builder, IntPtr path, Encoding encodingType);

		// Token: 0x0600045B RID: 1115
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Encoding_By_ColumnPath(IntPtr builder, IntPtr path, Encoding encodingType);

		// Token: 0x0600045C RID: 1116
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Encryption(IntPtr builder, IntPtr fileEncryptionProperties);

		// Token: 0x0600045D RID: 1117
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Max_Row_Group_Length(IntPtr builder, long maxRowGroupLength);

		// Token: 0x0600045E RID: 1118
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Version(IntPtr builder, ParquetVersion version);

		// Token: 0x0600045F RID: 1119
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Write_Batch_Size(IntPtr builder, long writeBatchSize);

		// Token: 0x06000460 RID: 1120
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Write_Page_Index(IntPtr builder);

		// Token: 0x06000461 RID: 1121
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Write_Page_Index(IntPtr builder);

		// Token: 0x06000462 RID: 1122
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Disable_Shuffle(IntPtr builder);

		// Token: 0x06000463 RID: 1123
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr WriterPropertiesBuilder_Enable_Shuffle(IntPtr builder);

		// Token: 0x0400013B RID: 315
		private readonly ParquetHandle _handle;
	}
}
