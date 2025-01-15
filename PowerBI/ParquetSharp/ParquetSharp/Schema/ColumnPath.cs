using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.Schema
{
	// Token: 0x02000094 RID: 148
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnPath : IDisposable
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000F794 File Offset: 0x0000D994
		public ColumnPath(string[] dotVector)
			: this(ColumnPath.Make(dotVector))
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public ColumnPath(string dotString)
			: this(ColumnPath.Make(dotString))
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public ColumnPath(Node node)
			: this(ColumnPath.Make(node))
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		internal ColumnPath(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnPath.ColumnPath_Free));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		public ColumnPath Extend(string nodeName)
		{
			ColumnPath columnPath;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				columnPath = new ColumnPath(ExceptionInfo.Return<IntPtr, IntPtr>(this.Handle, StringUtil.ToCStringUtf8(nodeName, byteBuffer), new ExceptionInfo.GetFunction<IntPtr, IntPtr>(ColumnPath.ColumnPath_Extend)));
			}
			return columnPath;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000F850 File Offset: 0x0000DA50
		public string ToDotString()
		{
			return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnPath.ColumnPath_ToDotString), new Action<IntPtr>(ColumnPath.ColumnPath_ToDotString_Free));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000F878 File Offset: 0x0000DA78
		public unsafe string[] ToDotVector()
		{
			IntPtr intPtr;
			int num;
			ExceptionInfo.Check(ColumnPath.ColumnPath_ToDotVector(this.Handle.IntPtr, out intPtr, out num));
			string[] array = new string[num];
			IntPtr* ptr = (IntPtr*)intPtr.ToPointer();
			for (int num2 = 0; num2 != num; num2++)
			{
				array[num2] = StringUtil.PtrToStringUtf8(ptr[(IntPtr)num2 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
			}
			ColumnPath.ColumnPath_ToDotVector_Free(intPtr, num);
			GC.KeepAlive(this.Handle);
			return array;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public override string ToString()
		{
			return this.ToDotString();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000F900 File Offset: 0x0000DB00
		private static IntPtr Make(string[] dotVector)
		{
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(1024, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ColumnPath.ColumnPath_Make(dotVector.Select((string s) => StringUtil.ToCStringUtf8(s, byteBuffer)).ToArray<IntPtr>(), dotVector.Length, out intPtr));
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000F97C File Offset: 0x0000DB7C
		private static IntPtr Make(string dotString)
		{
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ColumnPath.ColumnPath_MakeFromDotString(StringUtil.ToCStringUtf8(dotString, byteBuffer), out intPtr));
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		private static IntPtr Make(Node node)
		{
			return ExceptionInfo.Return<IntPtr>(node.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnPath.ColumnPath_MakeFromNode));
		}

		// Token: 0x0600047C RID: 1148
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnPath_Free(IntPtr columnPath);

		// Token: 0x0600047D RID: 1149
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_Make(IntPtr[] path, int length, out IntPtr columnPath);

		// Token: 0x0600047E RID: 1150
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_MakeFromDotString(IntPtr dotString, out IntPtr columnPath);

		// Token: 0x0600047F RID: 1151
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_MakeFromNode(IntPtr node, out IntPtr columnPath);

		// Token: 0x06000480 RID: 1152
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_Extend(IntPtr columnPath, IntPtr nodeName, out IntPtr newColumnPath);

		// Token: 0x06000481 RID: 1153
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_ToDotString(IntPtr columnPath, out IntPtr dotString);

		// Token: 0x06000482 RID: 1154
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnPath_ToDotString_Free(IntPtr dotString);

		// Token: 0x06000483 RID: 1155
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnPath_ToDotVector(IntPtr columnPath, out IntPtr dotVector, out int length);

		// Token: 0x06000484 RID: 1156
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnPath_ToDotVector_Free(IntPtr dotVector, int length);

		// Token: 0x0400013C RID: 316
		internal readonly ParquetHandle Handle;
	}
}
