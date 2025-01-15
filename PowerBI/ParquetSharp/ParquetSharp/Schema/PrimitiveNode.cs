using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.Schema
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PrimitiveNode : Node
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000FE60 File Offset: 0x0000E060
		public PrimitiveNode(string name, Repetition repetition, LogicalType logicalType, PhysicalType physicalType, int primitiveLength = -1)
			: this(PrimitiveNode.Make(name, repetition, logicalType, physicalType, primitiveLength))
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000FE74 File Offset: 0x0000E074
		internal PrimitiveNode(IntPtr handle)
			: base(handle)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000FE80 File Offset: 0x0000E080
		public ColumnOrder ColumnOrder
		{
			get
			{
				return ExceptionInfo.Return<ColumnOrder>(this.Handle, new ExceptionInfo.GetFunction<ColumnOrder>(PrimitiveNode.PrimitiveNode_Column_Order));
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000FE9C File Offset: 0x0000E09C
		public PhysicalType PhysicalType
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this.Handle, new ExceptionInfo.GetFunction<PhysicalType>(PrimitiveNode.PrimitiveNode_Physical_Type));
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		public int TypeLength
		{
			get
			{
				return ExceptionInfo.Return<int>(this.Handle, new ExceptionInfo.GetFunction<int>(PrimitiveNode.PrimitiveNode_Type_Length));
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public override Node DeepClone()
		{
			Node node;
			using (LogicalType logicalType = base.LogicalType)
			{
				node = new PrimitiveNode(base.Name, base.Repetition, logicalType, this.PhysicalType, this.TypeLength);
			}
			return node;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000FF2C File Offset: 0x0000E12C
		private static IntPtr Make(string name, Repetition repetition, LogicalType logicalType, PhysicalType physicalType, int primitiveLength)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (logicalType == null)
			{
				throw new ArgumentNullException("logicalType");
			}
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(PrimitiveNode.PrimitiveNode_Make(StringUtil.ToCStringUtf8(name, byteBuffer), repetition, logicalType.Handle.IntPtr, physicalType, primitiveLength, out intPtr));
				GC.KeepAlive(logicalType.Handle);
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x060004B1 RID: 1201
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr PrimitiveNode_Make(IntPtr name, Repetition repetition, IntPtr logicalType, PhysicalType type, int primitiveLength, out IntPtr primitiveNode);

		// Token: 0x060004B2 RID: 1202
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr PrimitiveNode_Column_Order(IntPtr node, out ColumnOrder columnOrder);

		// Token: 0x060004B3 RID: 1203
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr PrimitiveNode_Physical_Type(IntPtr node, out PhysicalType physicalType);

		// Token: 0x060004B4 RID: 1204
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr PrimitiveNode_Type_Length(IntPtr node, out int typeLength);
	}
}
