using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnDescriptor
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003588 File Offset: 0x00001788
		internal ColumnDescriptor(IntPtr handle)
		{
			this._handle = handle;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003598 File Offset: 0x00001798
		public ColumnOrder ColumnOrder
		{
			get
			{
				return ExceptionInfo.Return<ColumnOrder>(this._handle, new ExceptionInfo.GetFunction<ColumnOrder>(ColumnDescriptor.ColumnDescriptor_ColumnOrder));
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000035B4 File Offset: 0x000017B4
		public LogicalType LogicalType
		{
			get
			{
				return LogicalType.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDescriptor.ColumnDescriptor_Logical_Type)));
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000035D4 File Offset: 0x000017D4
		public short MaxDefinitionLevel
		{
			get
			{
				return ExceptionInfo.Return<short>(this._handle, new ExceptionInfo.GetFunction<short>(ColumnDescriptor.ColumnDescriptor_Max_Definition_Level));
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000035F0 File Offset: 0x000017F0
		public short MaxRepetitionLevel
		{
			get
			{
				return ExceptionInfo.Return<short>(this._handle, new ExceptionInfo.GetFunction<short>(ColumnDescriptor.ColumnDescriptor_Max_Repetition_Level));
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000360C File Offset: 0x0000180C
		public string Name
		{
			get
			{
				return ExceptionInfo.ReturnString(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDescriptor.ColumnDescriptor_Name), null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003628 File Offset: 0x00001828
		public ColumnPath Path
		{
			get
			{
				return new ColumnPath(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDescriptor.ColumnDescriptor_Path)));
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003648 File Offset: 0x00001848
		public Node SchemaNode
		{
			get
			{
				Node node = Node.Create(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDescriptor.ColumnDescriptor_Schema_Node)));
				if (node == null)
				{
					throw new InvalidOperationException();
				}
				return node;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003674 File Offset: 0x00001874
		public PhysicalType PhysicalType
		{
			get
			{
				return ExceptionInfo.Return<PhysicalType>(this._handle, new ExceptionInfo.GetFunction<PhysicalType>(ColumnDescriptor.ColumnDescriptor_Physical_Type));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003690 File Offset: 0x00001890
		public SortOrder SortOrder
		{
			get
			{
				return ExceptionInfo.Return<SortOrder>(this._handle, new ExceptionInfo.GetFunction<SortOrder>(ColumnDescriptor.ColumnDescriptor_SortOrder));
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000036AC File Offset: 0x000018AC
		public int TypeLength
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(ColumnDescriptor.ColumnDescriptor_Type_Length));
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000036C8 File Offset: 0x000018C8
		public int TypePrecision
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(ColumnDescriptor.ColumnDescriptor_Type_Precision));
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000036E4 File Offset: 0x000018E4
		public int TypeScale
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(ColumnDescriptor.ColumnDescriptor_Type_Scale));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003700 File Offset: 0x00001900
		public TReturn Apply<[Nullable(2)] TReturn>(LogicalTypeFactory typeFactory, IColumnDescriptorVisitor<TReturn> visitor)
		{
			return this.Apply<TReturn>(typeFactory, null, false, visitor);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000370C File Offset: 0x0000190C
		public TReturn Apply<[Nullable(2)] TReturn>(LogicalTypeFactory typeFactory, [Nullable(2)] Type columnLogicalTypeOverride, IColumnDescriptorVisitor<TReturn> visitor)
		{
			return this.Apply<TReturn>(typeFactory, columnLogicalTypeOverride, false, visitor);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003718 File Offset: 0x00001918
		public TReturn Apply<[Nullable(2)] TReturn>(LogicalTypeFactory typeFactory, [Nullable(2)] Type columnLogicalTypeOverride, bool useNesting, IColumnDescriptorVisitor<TReturn> visitor)
		{
			global::System.ValueTuple<Type, Type, Type> systemTypes = this.GetSystemTypes(typeFactory, columnLogicalTypeOverride, useNesting);
			return ((Func<IColumnDescriptorVisitor<TReturn>, TReturn>)ColumnDescriptor.VisitorCache.GetOrAdd(new global::System.ValueTuple<Type, Type, Type, Type>(systemTypes.Item1, systemTypes.Item2, systemTypes.Item3, typeof(TReturn)), delegate([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType", "elementType", "returnType" })] [Nullable(new byte[] { 0, 1, 1, 1, 1 })] global::System.ValueTuple<Type, Type, Type, Type> t)
			{
				MethodInfo method = typeof(IColumnDescriptorVisitor<TReturn>).GetMethod("OnColumnDescriptor");
				if (method == null)
				{
					throw new Exception("failed to reflect 'OnColumnDescriptor' method");
				}
				MethodInfo methodInfo = method.MakeGenericMethod(new Type[] { t.Item1, t.Item2, t.Item3 });
				ParameterExpression parameterExpression;
				return Expression.Lambda<Func<IColumnDescriptorVisitor<TReturn>, TReturn>>(Expression.Call(parameterExpression, methodInfo), new ParameterExpression[] { parameterExpression }).Compile();
			}))(visitor);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000378C File Offset: 0x0000198C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType", "elementType" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1 })]
		public global::System.ValueTuple<Type, Type, Type> GetSystemTypes(LogicalTypeFactory typeFactory, [Nullable(2)] Type columnLogicalTypeOverride)
		{
			return this.GetSystemTypes(typeFactory, columnLogicalTypeOverride, false);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003798 File Offset: 0x00001998
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType", "elementType" })]
		[return: Nullable(new byte[] { 0, 1, 1, 1 })]
		public global::System.ValueTuple<Type, Type, Type> GetSystemTypes(LogicalTypeFactory typeFactory, [Nullable(2)] Type columnLogicalTypeOverride, bool useNesting)
		{
			global::System.ValueTuple<Type, Type> systemTypes = typeFactory.GetSystemTypes(this, columnLogicalTypeOverride);
			Type item = systemTypes.Item1;
			Type item2 = systemTypes.Item2;
			Type type = ColumnDescriptor.NonNullable(item2);
			Node node = this.SchemaNode;
			while (node != null)
			{
				using (node.LogicalType)
				{
					Node node2 = node.Parent;
					using (LogicalType logicalType2 = ((node2 != null) ? node2.LogicalType : null))
					{
						if (node.Repetition == Repetition.Repeated)
						{
							bool flag = node2 != null;
							if (flag)
							{
								LogicalTypeEnum type2 = logicalType2.Type;
								bool flag2 = type2 - LogicalTypeEnum.Map <= 1;
								flag = flag2;
							}
							bool flag3 = flag;
							if (flag3)
							{
								Repetition repetition = node2.Repetition;
								bool flag2 = repetition <= Repetition.Optional;
								flag3 = flag2;
							}
							if (flag3)
							{
								type = type.MakeArrayType();
								node.Dispose();
								node = node2;
								node2 = node.Parent;
								goto IL_01AA;
							}
							using (ColumnPath path = node.Path)
							{
								throw new Exception("Invalid Parquet schema, found a repeated node '" + path.ToDotString() + "' that is not the child of a valid list or map annotated group.");
							}
						}
						if (node is GroupNode && node2 != null && useNesting)
						{
							type = typeof(Nested<>).MakeGenericType(new Type[] { type });
						}
						Type type3;
						if (node.Repetition == Repetition.Optional && type.BaseType != typeof(object) && type.BaseType != typeof(Array) && !TypeUtils.IsNullable(type, out type3))
						{
							type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
						}
						IL_01AA:
						node.Dispose();
						node = node2;
					}
				}
			}
			return new global::System.ValueTuple<Type, Type, Type>(item, item2, type);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000039D8 File Offset: 0x00001BD8
		private static Type NonNullable(Type type)
		{
			if (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))
			{
				return type;
			}
			return type.GetGenericArguments().Single<Type>();
		}

		// Token: 0x06000082 RID: 130
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Max_Definition_Level(IntPtr columnDescriptor, out short maxDefinitionLevel);

		// Token: 0x06000083 RID: 131
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Max_Repetition_Level(IntPtr columnDescriptor, out short maxRepetitionLevel);

		// Token: 0x06000084 RID: 132
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Physical_Type(IntPtr columnDescriptor, out PhysicalType physicalType);

		// Token: 0x06000085 RID: 133
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Logical_Type(IntPtr columnDescriptor, out IntPtr logicalType);

		// Token: 0x06000086 RID: 134
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_ColumnOrder(IntPtr columnDescriptor, out ColumnOrder columnOrder);

		// Token: 0x06000087 RID: 135
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_SortOrder(IntPtr columnDescriptor, out SortOrder sortOrder);

		// Token: 0x06000088 RID: 136
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Name(IntPtr columnDescriptor, out IntPtr name);

		// Token: 0x06000089 RID: 137
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Path(IntPtr columnDescriptor, out IntPtr path);

		// Token: 0x0600008A RID: 138
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Schema_Node(IntPtr columnDescriptor, out IntPtr schemaNode);

		// Token: 0x0600008B RID: 139
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Type_Length(IntPtr columnDescriptor, out int typeLength);

		// Token: 0x0600008C RID: 140
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Type_Precision(IntPtr columnDescriptor, out int typePrecision);

		// Token: 0x0600008D RID: 141
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDescriptor_Type_Scale(IntPtr columnDescriptor, out int typeScale);

		// Token: 0x0400002F RID: 47
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType", "elementType", "returnType" })]
		[Nullable(new byte[] { 1, 0, 1, 1, 1, 1, 1 })]
		private static readonly ConcurrentDictionary<global::System.ValueTuple<Type, Type, Type, Type>, Delegate> VisitorCache = new ConcurrentDictionary<global::System.ValueTuple<Type, Type, Type, Type>, Delegate>();

		// Token: 0x04000030 RID: 48
		private readonly IntPtr _handle;
	}
}
