using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.Schema
{
	// Token: 0x02000096 RID: 150
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GroupNode : Node
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		public GroupNode(string name, Repetition repetition, IReadOnlyList<Node> fields, [Nullable(2)] LogicalType logicalType = null)
			: this(GroupNode.Make(name, repetition, fields, logicalType))
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		internal GroupNode(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000FA08 File Offset: 0x0000DC08
		public int FieldCount
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(GroupNode.GroupNode_Field_Count));
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public Node[] Fields
		{
			get
			{
				return Enumerable.Range(0, this.FieldCount).Select(new Func<int, Node>(this.Field)).ToArray<Node>();
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public Node Field(int i)
		{
			Node node = Node.Create(ExceptionInfo.Return<int, IntPtr>(this.Handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(GroupNode.GroupNode_Field)));
			if (node == null)
			{
				throw new InvalidOperationException();
			}
			return node;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000FA74 File Offset: 0x0000DC74
		public int FieldIndex(string name)
		{
			int num;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				num = ExceptionInfo.Return<IntPtr, int>(this.Handle, StringUtil.ToCStringUtf8(name, byteBuffer), new ExceptionInfo.GetFunction<IntPtr, int>(GroupNode.GroupNode_Field_Index_By_Name));
			}
			return num;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000FACC File Offset: 0x0000DCCC
		public int FieldIndex(Node node)
		{
			return ExceptionInfo.Return<int>(this.Handle, node.Handle, new ExceptionInfo.GetFunction<IntPtr, int>(GroupNode.GroupNode_Field_Index_By_Node));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		public override Node DeepClone()
		{
			Node node;
			using (LogicalType logicalType = base.LogicalType)
			{
				Node[] fields = this.Fields;
				Node[] array = fields.Select((Node f) => f.DeepClone()).ToArray<Node>();
				try
				{
					node = new GroupNode(base.Name, base.Repetition, array, (logicalType is NoneLogicalType) ? null : logicalType);
				}
				finally
				{
					foreach (Node node2 in fields.Concat(array))
					{
						node2.Dispose();
					}
				}
			}
			return node;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		private unsafe static IntPtr Make(string name, Repetition repetition, IReadOnlyList<Node> fields, [Nullable(2)] LogicalType logicalType)
		{
			IntPtr[] array = fields.Select((Node f) => f.Handle.IntPtr).ToArray<IntPtr>();
			IntPtr[] array2;
			IntPtr* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(GroupNode.GroupNode_Make(StringUtil.ToCStringUtf8(name, byteBuffer), repetition, (IntPtr)((void*)ptr), array.Length, (logicalType != null) ? logicalType.Handle.IntPtr : IntPtr.Zero, out intPtr));
				GC.KeepAlive(fields);
				GC.KeepAlive(logicalType);
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x0600048E RID: 1166
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr GroupNode_Make(IntPtr name, Repetition repetition, IntPtr fields, int numFields, IntPtr logicalType, out IntPtr groupNode);

		// Token: 0x0600048F RID: 1167
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr GroupNode_Field(IntPtr groupNode, int i, out IntPtr field);

		// Token: 0x06000490 RID: 1168
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr GroupNode_Field_Count(IntPtr groupNode, out int fieldCount);

		// Token: 0x06000491 RID: 1169
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr GroupNode_Field_Index_By_Name(IntPtr groupNode, IntPtr name, out int index);

		// Token: 0x06000492 RID: 1170
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr GroupNode_Field_Index_By_Node(IntPtr groupNode, IntPtr node, out int index);
	}
}
