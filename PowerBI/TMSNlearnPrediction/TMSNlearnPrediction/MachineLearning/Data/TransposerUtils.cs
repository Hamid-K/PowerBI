using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000173 RID: 371
	public static class TransposerUtils
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00028368 File Offset: 0x00026568
		public static void GetSingleSlotValue<T>(this ITransposeDataView view, int col, ref VBuffer<T> dst)
		{
			Contracts.CheckValue<ITransposeDataView>(view, "view");
			Contracts.CheckParam(0 <= col && col < view.Schema.ColumnCount, "col");
			using (ISlotCursor slotCursor = view.GetSlotCursor(col))
			{
				ValueGetter<VBuffer<T>> getter = slotCursor.GetGetter<T>();
				if (!slotCursor.MoveNext())
				{
					throw Contracts.Except("Could not get single value on column '{0}' because there are no slots", new object[] { view.Schema.GetColumnName(col) });
				}
				getter.Invoke(ref dst);
				if (slotCursor.MoveNext())
				{
					throw Contracts.Except("Could not get single value on column '{0}' because there is more than one slot", new object[] { view.Schema.GetColumnName(col) });
				}
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00028428 File Offset: 0x00026628
		public static ValueGetter<TValue> GetGetterWithVectorType<TValue>(this ISlotCursor cursor, IExceptionContext ctx = null)
		{
			Contracts.CheckValue<ISlotCursor>(ctx, cursor, "cursor");
			Type typeFromHandle = typeof(TValue);
			if (!PlatformUtils.IsGenericEx(typeFromHandle, typeof(VBuffer)))
			{
				throw Contracts.Except(ctx, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
			}
			Type[] genericArguments = typeFromHandle.GetGenericArguments();
			Func<ValueGetter<VBuffer<int>>> func = new Func<ValueGetter<VBuffer<int>>>(cursor.GetGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { genericArguments[0] });
			ValueGetter<TValue> valueGetter = methodInfo.Invoke(cursor, null) as ValueGetter<TValue>;
			if (valueGetter == null)
			{
				throw Contracts.Except(ctx, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
			}
			return valueGetter;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000284F2 File Offset: 0x000266F2
		public static IRowCursor GetRowCursorShim(IChannelProvider provider, ISlotCursor cursor)
		{
			Contracts.CheckValue<IChannelProvider>(provider, "provider");
			Contracts.CheckValue<ISlotCursor>(provider, cursor, "cursor");
			return Utils.MarshalInvoke<IChannelProvider, ISlotCursor, IRowCursor>(new Func<IChannelProvider, ISlotCursor, IRowCursor>(TransposerUtils.GetRowCursorShimCore<int>), cursor.GetSlotType().ItemType.RawType, provider, cursor);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002852E File Offset: 0x0002672E
		private static IRowCursor GetRowCursorShimCore<T>(IChannelProvider provider, ISlotCursor cursor)
		{
			return new TransposerUtils.SlotRowCursorShim<T>(provider, cursor);
		}

		// Token: 0x02000174 RID: 372
		public sealed class SlotDataView : IDataView, ISchematized
		{
			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000770 RID: 1904 RVA: 0x00028537 File Offset: 0x00026737
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000771 RID: 1905 RVA: 0x0002853F File Offset: 0x0002673F
			public bool CanShuffle
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00028544 File Offset: 0x00026744
			public SlotDataView(IHostEnvironment env, ITransposeDataView data, int col)
			{
				Contracts.CheckValue<IHostEnvironment>(env, "env");
				this._host = env.Register("SlotDataView");
				Contracts.CheckValue<ITransposeDataView>(this._host, data, "data");
				Contracts.CheckParam(this._host, 0 <= col && col < data.Schema.ColumnCount, "col");
				this._type = data.TransposeSchema.GetSlotType(col);
				this._data = data;
				this._col = col;
				this._schema = new TransposerUtils.SlotDataView.SchemaImpl(this);
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x000285D8 File Offset: 0x000267D8
			public long? GetRowCount(bool lazy = true)
			{
				ColumnType columnType = this._data.Schema.GetColumnType(this._col);
				int valueCount = columnType.ValueCount;
				return new long?((long)valueCount);
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x0002860A File Offset: 0x0002680A
			public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				return Utils.MarshalInvoke<bool, IRowCursor>(new Func<bool, IRowCursor>(this.GetRowCursor<int>), this._type.ItemType.RawType, predicate(0));
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00028645 File Offset: 0x00026845
			private IRowCursor GetRowCursor<T>(bool active)
			{
				return new TransposerUtils.SlotDataView.Cursor<T>(this, active);
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x00028650 File Offset: 0x00026850
			public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
			{
				Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
				consolidator = null;
				return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
			}

			// Token: 0x040003E7 RID: 999
			private readonly IHost _host;

			// Token: 0x040003E8 RID: 1000
			private readonly ITransposeDataView _data;

			// Token: 0x040003E9 RID: 1001
			private readonly int _col;

			// Token: 0x040003EA RID: 1002
			private readonly ColumnType _type;

			// Token: 0x040003EB RID: 1003
			private readonly TransposerUtils.SlotDataView.SchemaImpl _schema;

			// Token: 0x02000175 RID: 373
			private sealed class SchemaImpl : ISchema
			{
				// Token: 0x170000A1 RID: 161
				// (get) Token: 0x06000777 RID: 1911 RVA: 0x00028685 File Offset: 0x00026885
				private IHost Host
				{
					get
					{
						return this._parent._host;
					}
				}

				// Token: 0x170000A2 RID: 162
				// (get) Token: 0x06000778 RID: 1912 RVA: 0x00028692 File Offset: 0x00026892
				public int ColumnCount
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x06000779 RID: 1913 RVA: 0x00028695 File Offset: 0x00026895
				public SchemaImpl(TransposerUtils.SlotDataView parent)
				{
					this._parent = parent;
				}

				// Token: 0x0600077A RID: 1914 RVA: 0x000286A4 File Offset: 0x000268A4
				public ColumnType GetColumnType(int col)
				{
					Contracts.CheckParam(this.Host, col == 0, "col");
					return this._parent._type;
				}

				// Token: 0x0600077B RID: 1915 RVA: 0x000286C5 File Offset: 0x000268C5
				public string GetColumnName(int col)
				{
					Contracts.CheckParam(this.Host, col == 0, "col");
					return this._parent._data.Schema.GetColumnName(this._parent._col);
				}

				// Token: 0x0600077C RID: 1916 RVA: 0x000286FB File Offset: 0x000268FB
				public bool TryGetColumnIndex(string name, out int col)
				{
					if (name == this.GetColumnName(0))
					{
						col = 0;
						return true;
					}
					col = -1;
					return false;
				}

				// Token: 0x0600077D RID: 1917 RVA: 0x00028715 File Offset: 0x00026915
				public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
				{
					Contracts.CheckParam(this.Host, col == 0, "col");
					return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
				}

				// Token: 0x0600077E RID: 1918 RVA: 0x00028730 File Offset: 0x00026930
				public ColumnType GetMetadataTypeOrNull(string kind, int col)
				{
					Contracts.CheckNonEmpty(this.Host, kind, "kind");
					Contracts.CheckParam(this.Host, col == 0, "col");
					return null;
				}

				// Token: 0x0600077F RID: 1919 RVA: 0x00028759 File Offset: 0x00026959
				public void GetMetadata<TValue>(string kind, int col, ref TValue value)
				{
					Contracts.CheckNonEmpty(this.Host, kind, "kind");
					Contracts.CheckParam(this.Host, col == 0, "col");
					throw MetadataUtils.ExceptGetMetadata();
				}

				// Token: 0x040003EC RID: 1004
				private readonly TransposerUtils.SlotDataView _parent;
			}

			// Token: 0x02000176 RID: 374
			private sealed class Cursor<T> : SynchronizedCursorBase<ISlotCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
			{
				// Token: 0x170000A3 RID: 163
				// (get) Token: 0x06000780 RID: 1920 RVA: 0x00028786 File Offset: 0x00026986
				public ISchema Schema
				{
					get
					{
						return this._parent._schema;
					}
				}

				// Token: 0x06000781 RID: 1921 RVA: 0x00028793 File Offset: 0x00026993
				public Cursor(TransposerUtils.SlotDataView parent, bool active)
					: base(parent._host, parent._data.GetSlotCursor(parent._col))
				{
					this._parent = parent;
					if (active)
					{
						this._getter = base.Input.GetGetter<T>();
					}
				}

				// Token: 0x06000782 RID: 1922 RVA: 0x000287CD File Offset: 0x000269CD
				public bool IsColumnActive(int col)
				{
					Contracts.CheckParam(this._ch, col == 0, "col");
					return this._getter != null;
				}

				// Token: 0x06000783 RID: 1923 RVA: 0x000287F0 File Offset: 0x000269F0
				public ValueGetter<TValue> GetGetter<TValue>(int col)
				{
					Contracts.CheckParam(this._ch, col == 0, "col");
					Contracts.CheckParam(this._ch, this._getter != null, "col", "requested column not active");
					ValueGetter<TValue> valueGetter = this._getter as ValueGetter<TValue>;
					if (valueGetter == null)
					{
						throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
					}
					return valueGetter;
				}

				// Token: 0x040003ED RID: 1005
				private readonly TransposerUtils.SlotDataView _parent;

				// Token: 0x040003EE RID: 1006
				private readonly Delegate _getter;
			}
		}

		// Token: 0x02000177 RID: 375
		private sealed class SlotRowCursorShim<T> : SynchronizedCursorBase<ISlotCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x00028868 File Offset: 0x00026A68
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x00028870 File Offset: 0x00026A70
			public SlotRowCursorShim(IChannelProvider provider, ISlotCursor cursor)
				: base(provider, cursor)
			{
				this._schema = new TransposerUtils.SlotRowCursorShim<T>.SchemaImpl(this, base.Input.GetSlotType());
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x00028891 File Offset: 0x00026A91
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, col == 0, "col");
				return true;
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x000288A8 File Offset: 0x00026AA8
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, col == 0, "col");
				return base.Input.GetGetterWithVectorType(this._ch);
			}

			// Token: 0x040003EF RID: 1007
			private readonly TransposerUtils.SlotRowCursorShim<T>.SchemaImpl _schema;

			// Token: 0x02000178 RID: 376
			private sealed class SchemaImpl : ISchema
			{
				// Token: 0x170000A5 RID: 165
				// (get) Token: 0x06000788 RID: 1928 RVA: 0x000288CF File Offset: 0x00026ACF
				private IChannel Ch
				{
					get
					{
						return this._parent._ch;
					}
				}

				// Token: 0x170000A6 RID: 166
				// (get) Token: 0x06000789 RID: 1929 RVA: 0x000288DC File Offset: 0x00026ADC
				public int ColumnCount
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x0600078A RID: 1930 RVA: 0x000288DF File Offset: 0x00026ADF
				public SchemaImpl(TransposerUtils.SlotRowCursorShim<T> parent, VectorType slotType)
				{
					this._parent = parent;
					this._type = slotType;
				}

				// Token: 0x0600078B RID: 1931 RVA: 0x000288F5 File Offset: 0x00026AF5
				public ColumnType GetColumnType(int col)
				{
					Contracts.CheckParam(this.Ch, col == 0, "col");
					return this._type;
				}

				// Token: 0x0600078C RID: 1932 RVA: 0x00028911 File Offset: 0x00026B11
				public string GetColumnName(int col)
				{
					Contracts.CheckParam(this.Ch, col == 0, "col");
					return "Waffles";
				}

				// Token: 0x0600078D RID: 1933 RVA: 0x0002892C File Offset: 0x00026B2C
				public bool TryGetColumnIndex(string name, out int col)
				{
					if (name == this.GetColumnName(0))
					{
						col = 0;
						return true;
					}
					col = -1;
					return false;
				}

				// Token: 0x0600078E RID: 1934 RVA: 0x00028946 File Offset: 0x00026B46
				public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
				{
					Contracts.CheckParam(this.Ch, col == 0, "col");
					return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
				}

				// Token: 0x0600078F RID: 1935 RVA: 0x00028961 File Offset: 0x00026B61
				public ColumnType GetMetadataTypeOrNull(string kind, int col)
				{
					Contracts.CheckNonEmpty(this.Ch, kind, "kind");
					Contracts.CheckParam(this.Ch, col == 0, "col");
					return null;
				}

				// Token: 0x06000790 RID: 1936 RVA: 0x0002898A File Offset: 0x00026B8A
				public void GetMetadata<TValue>(string kind, int col, ref TValue value)
				{
					Contracts.CheckNonEmpty(this.Ch, kind, "kind");
					Contracts.CheckParam(this.Ch, col == 0, "col");
					throw MetadataUtils.ExceptGetMetadata();
				}

				// Token: 0x040003F0 RID: 1008
				private readonly TransposerUtils.SlotRowCursorShim<T> _parent;

				// Token: 0x040003F1 RID: 1009
				private readonly VectorType _type;
			}
		}

		// Token: 0x02000179 RID: 377
		internal sealed class SimpleTransposeSchema : ITransposeSchema, ISchema
		{
			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x000289B7 File Offset: 0x00026BB7
			public int ColumnCount
			{
				get
				{
					return this._schema.ColumnCount;
				}
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x000289C4 File Offset: 0x00026BC4
			public SimpleTransposeSchema(ISchema schema)
			{
				Contracts.CheckValue<ISchema>(schema, "schema");
				this._schema = schema;
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x000289DE File Offset: 0x00026BDE
			public string GetColumnName(int col)
			{
				return this._schema.GetColumnName(col);
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x000289EC File Offset: 0x00026BEC
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this._schema.TryGetColumnIndex(name, ref col);
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x000289FB File Offset: 0x00026BFB
			public ColumnType GetColumnType(int col)
			{
				return this._schema.GetColumnType(col);
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00028A09 File Offset: 0x00026C09
			public VectorType GetSlotType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return null;
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x00028A26 File Offset: 0x00026C26
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				return this._schema.GetMetadataTypes(col);
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x00028A34 File Offset: 0x00026C34
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				return this._schema.GetMetadataTypeOrNull(kind, col);
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x00028A43 File Offset: 0x00026C43
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				this._schema.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x040003F2 RID: 1010
			private readonly ISchema _schema;
		}
	}
}
