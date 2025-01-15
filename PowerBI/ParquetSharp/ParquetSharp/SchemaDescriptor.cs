using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000088 RID: 136
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SchemaDescriptor
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000E29C File Offset: 0x0000C49C
		internal SchemaDescriptor(IntPtr schemaDescriptorHandle)
		{
			this._handle = schemaDescriptorHandle;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public GroupNode GroupNode
		{
			get
			{
				Node node = Node.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(SchemaDescriptor.SchemaDescriptor_Group_Node)));
				if (node == null)
				{
					throw new InvalidOperationException();
				}
				return (GroupNode)node;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public string Name
		{
			get
			{
				return ExceptionInfo.ReturnString(this._handle, new ExceptionInfo.GetFunction<IntPtr>(SchemaDescriptor.SchemaDescriptor_Name), null);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		public int NumColumns
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(SchemaDescriptor.SchemaDescriptor_Num_Columns));
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E314 File Offset: 0x0000C514
		public Node SchemaRoot
		{
			get
			{
				Node node = Node.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(SchemaDescriptor.SchemaDescriptor_Schema_Root)));
				if (node == null)
				{
					throw new InvalidOperationException();
				}
				return node;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E340 File Offset: 0x0000C540
		public ColumnDescriptor Column(int i)
		{
			return new ColumnDescriptor(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(SchemaDescriptor.SchemaDescriptor_Column)));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E360 File Offset: 0x0000C560
		public int ColumnIndex(Node node)
		{
			int num = ExceptionInfo.Return<IntPtr, int>(this._handle, node.Handle.IntPtr, new ExceptionInfo.GetFunction<IntPtr, int>(SchemaDescriptor.SchemaDescriptor_ColumnIndex_ByNode));
			GC.KeepAlive(node);
			return num;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E38C File Offset: 0x0000C58C
		public int ColumnIndex(string path)
		{
			int num;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				num = ExceptionInfo.Return<IntPtr, int>(this._handle, StringUtil.ToCStringUtf8(path, byteBuffer), new ExceptionInfo.GetFunction<IntPtr, int>(SchemaDescriptor.SchemaDescriptor_ColumnIndex_ByPath));
			}
			return num;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public Node ColumnRoot(int i)
		{
			Node node = Node.Create(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(SchemaDescriptor.SchemaDescriptor_Get_Column_Root)));
			if (node == null)
			{
				throw new InvalidOperationException();
			}
			return node;
		}

		// Token: 0x060003B6 RID: 950
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Column(IntPtr descriptor, int i, out IntPtr columnDescriptor);

		// Token: 0x060003B7 RID: 951
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_ColumnIndex_ByNode(IntPtr descriptor, IntPtr node, out int columnIndex);

		// Token: 0x060003B8 RID: 952
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_ColumnIndex_ByPath(IntPtr descriptor, IntPtr path, out int columnIndex);

		// Token: 0x060003B9 RID: 953
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Get_Column_Root(IntPtr descriptor, int i, out IntPtr columnRoot);

		// Token: 0x060003BA RID: 954
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Group_Node(IntPtr descriptor, out IntPtr groupNode);

		// Token: 0x060003BB RID: 955
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Name(IntPtr descriptor, out IntPtr name);

		// Token: 0x060003BC RID: 956
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Num_Columns(IntPtr descriptor, out int numColumns);

		// Token: 0x060003BD RID: 957
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr SchemaDescriptor_Schema_Root(IntPtr descriptor, out IntPtr schemaRoot);

		// Token: 0x04000117 RID: 279
		private readonly IntPtr _handle;
	}
}
