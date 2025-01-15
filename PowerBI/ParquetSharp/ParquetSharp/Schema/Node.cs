using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.Schema
{
	// Token: 0x02000097 RID: 151
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{NodeType}Node: ({FieldId}), {Name}, LogicalType: {LogicalType}")]
	public abstract class Node : IDisposable, IEquatable<Node>
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		protected Node(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(Node.Node_Free));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public int FieldId
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(Node.Node_Field_Id));
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public LogicalType LogicalType
		{
			get
			{
				return LogicalType.Create(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(Node.Node_Logical_Type)));
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public ConvertedType ConvertedType
		{
			get
			{
				return ExceptionInfo.Return<ConvertedType>(this.Handle, new ExceptionInfo.GetFunction<ConvertedType>(Node.Node_Converted_Type));
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		public string Name
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(Node.Node_Name), null);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000FD48 File Offset: 0x0000DF48
		public NodeType NodeType
		{
			get
			{
				return ExceptionInfo.Return<NodeType>(this.Handle, new ExceptionInfo.GetFunction<NodeType>(Node.Node_Node_Type));
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000FD64 File Offset: 0x0000DF64
		[Nullable(2)]
		public Node Parent
		{
			[NullableContext(2)]
			get
			{
				return Node.Create(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(Node.Node_Parent)));
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public ColumnPath Path
		{
			get
			{
				return new ColumnPath(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(Node.Node_Path)));
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public Repetition Repetition
		{
			get
			{
				return ExceptionInfo.Return<Repetition>(this.Handle, new ExceptionInfo.GetFunction<Repetition>(Node.Node_Repetition));
			}
		}

		// Token: 0x0600049D RID: 1181
		public abstract Node DeepClone();

		// Token: 0x0600049E RID: 1182 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		[NullableContext(2)]
		public bool Equals(Node other)
		{
			return other != null && ExceptionInfo.Return<bool>(this.Handle, other.Handle, new ExceptionInfo.GetFunction<IntPtr, bool>(Node.Node_Equals));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		[NullableContext(2)]
		internal static Node Create(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			NodeType nodeType = ExceptionInfo.Return<NodeType>(handle, new ExceptionInfo.GetFunction<NodeType>(Node.Node_Node_Type));
			Node node;
			if (nodeType != NodeType.Primitive)
			{
				if (nodeType != NodeType.Group)
				{
					throw new ArgumentOutOfRangeException(string.Format("unknown node type {0}", nodeType));
				}
				node = new GroupNode(handle);
			}
			else
			{
				node = new PrimitiveNode(handle);
			}
			return node;
		}

		// Token: 0x060004A0 RID: 1184
		[DllImport("ParquetSharpNative")]
		private static extern void Node_Free(IntPtr node);

		// Token: 0x060004A1 RID: 1185
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Equals(IntPtr node, IntPtr other, [MarshalAs(UnmanagedType.I1)] out bool equals);

		// Token: 0x060004A2 RID: 1186
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Field_Id(IntPtr node, out int id);

		// Token: 0x060004A3 RID: 1187
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Logical_Type(IntPtr node, out IntPtr logicalType);

		// Token: 0x060004A4 RID: 1188
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Name(IntPtr node, out IntPtr name);

		// Token: 0x060004A5 RID: 1189
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Node_Type(IntPtr node, out NodeType nodeType);

		// Token: 0x060004A6 RID: 1190
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Converted_Type(IntPtr node, out ConvertedType convertedType);

		// Token: 0x060004A7 RID: 1191
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Parent(IntPtr node, out IntPtr parent);

		// Token: 0x060004A8 RID: 1192
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Path(IntPtr node, out IntPtr parent);

		// Token: 0x060004A9 RID: 1193
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr Node_Repetition(IntPtr node, out Repetition repetition);

		// Token: 0x04000157 RID: 343
		internal readonly ParquetHandle Handle;
	}
}
