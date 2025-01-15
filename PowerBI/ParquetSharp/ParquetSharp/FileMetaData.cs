using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FileMetaData : IEquatable<FileMetaData>, IDisposable
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000062F4 File Offset: 0x000044F4
		internal FileMetaData(IntPtr handle)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(FileMetaData.FileMetaData_Free));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006314 File Offset: 0x00004514
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006324 File Offset: 0x00004524
		public string CreatedBy
		{
			get
			{
				return ExceptionInfo.ReturnString(this._handle, new ExceptionInfo.GetFunction<IntPtr>(FileMetaData.FileMetaData_Created_By), null);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006340 File Offset: 0x00004540
		public IReadOnlyDictionary<string, string> KeyValueMetadata
		{
			get
			{
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(FileMetaData.FileMetaData_Key_Value_Metadata));
				if (intPtr == IntPtr.Zero)
				{
					return new Dictionary<string, string>();
				}
				IReadOnlyDictionary<string, string> readOnlyDictionary;
				using (KeyValueMetadata keyValueMetadata = new KeyValueMetadata(intPtr))
				{
					readOnlyDictionary = keyValueMetadata.ToDictionary();
				}
				return readOnlyDictionary;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000063AC File Offset: 0x000045AC
		public int NumColumns
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(FileMetaData.FileMetaData_Num_Columns));
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000063C8 File Offset: 0x000045C8
		public long NumRows
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(FileMetaData.FileMetaData_Num_Rows));
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000063E4 File Offset: 0x000045E4
		public int NumRowGroups
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(FileMetaData.FileMetaData_Num_Row_Groups));
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006400 File Offset: 0x00004600
		public int NumSchemaElements
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(FileMetaData.FileMetaData_Num_Schema_Elements));
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000641C File Offset: 0x0000461C
		public SchemaDescriptor Schema
		{
			get
			{
				SchemaDescriptor schemaDescriptor;
				if ((schemaDescriptor = this._schema) == null)
				{
					schemaDescriptor = (this._schema = new SchemaDescriptor(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(FileMetaData.FileMetaData_Schema))));
				}
				return schemaDescriptor;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006460 File Offset: 0x00004660
		public int Size
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(FileMetaData.FileMetaData_Size));
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000647C File Offset: 0x0000467C
		public ParquetVersion Version
		{
			get
			{
				return ExceptionInfo.Return<ParquetVersion>(this._handle, new ExceptionInfo.GetFunction<ParquetVersion>(FileMetaData.FileMetaData_Version));
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006498 File Offset: 0x00004698
		public ApplicationVersion WriterVersion
		{
			get
			{
				return new ApplicationVersion(ExceptionInfo.Return<ApplicationVersion.CStruct>(this._handle, new ExceptionInfo.GetFunction<ApplicationVersion.CStruct>(FileMetaData.FileMetaData_Writer_Version)));
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000064B8 File Offset: 0x000046B8
		[NullableContext(2)]
		public bool Equals(FileMetaData other)
		{
			return other != null && ExceptionInfo.Return<bool>(this._handle, other._handle, new ExceptionInfo.GetFunction<IntPtr, bool>(FileMetaData.FileMetaData_Equals));
		}

		// Token: 0x060001DA RID: 474
		[DllImport("ParquetSharpNative")]
		private static extern void FileMetaData_Free(IntPtr fileMetaData);

		// Token: 0x060001DB RID: 475
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Created_By(IntPtr fileMetaData, out IntPtr createdBy);

		// Token: 0x060001DC RID: 476
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Equals(IntPtr fileMetaData, IntPtr other, [MarshalAs(UnmanagedType.I1)] out bool equals);

		// Token: 0x060001DD RID: 477
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Key_Value_Metadata(IntPtr fileMetaData, out IntPtr keyValueMetadata);

		// Token: 0x060001DE RID: 478
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Num_Columns(IntPtr fileMetaData, out int numColumns);

		// Token: 0x060001DF RID: 479
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Num_Rows(IntPtr fileMetaData, out long numRows);

		// Token: 0x060001E0 RID: 480
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Num_Row_Groups(IntPtr fileMetaData, out int numRowGroups);

		// Token: 0x060001E1 RID: 481
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Num_Schema_Elements(IntPtr fileMetaData, out int numSchemaElements);

		// Token: 0x060001E2 RID: 482
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Schema(IntPtr fileMetaData, out IntPtr schema);

		// Token: 0x060001E3 RID: 483
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Size(IntPtr fileMetaData, out int size);

		// Token: 0x060001E4 RID: 484
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Version(IntPtr fileMetaData, out ParquetVersion version);

		// Token: 0x060001E5 RID: 485
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileMetaData_Writer_Version(IntPtr fileMetaData, out ApplicationVersion.CStruct applicationVersion);

		// Token: 0x04000072 RID: 114
		private readonly ParquetHandle _handle;

		// Token: 0x04000073 RID: 115
		[Nullable(2)]
		private SchemaDescriptor _schema;
	}
}
