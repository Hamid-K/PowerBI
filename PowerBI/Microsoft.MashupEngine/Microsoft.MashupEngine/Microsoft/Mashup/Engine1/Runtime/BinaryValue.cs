using System;
using System.IO;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001280 RID: 4736
	public abstract class BinaryValue : PrimitiveValue, IBinaryValue, IValue
	{
		// Token: 0x06007C65 RID: 31845 RVA: 0x001ABB0E File Offset: 0x001A9D0E
		public static BinaryValue New(byte[] value)
		{
			return new BytesBufferedBinaryValue(value);
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x001ABB16 File Offset: 0x001A9D16
		public sealed override Value NewMeta(RecordValue meta)
		{
			if (meta.IsEmpty && this.MetaValue.IsEmpty)
			{
				return this;
			}
			return new BinaryValue.MetaTypeBinaryValue(this, this.Type, meta);
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x001ABB3C File Offset: 0x001A9D3C
		public sealed override Value NewType(TypeValue type)
		{
			if (type == this.Type)
			{
				return this;
			}
			return new BinaryValue.MetaTypeBinaryValue(this, type, this.MetaValue);
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06007C68 RID: 31848 RVA: 0x00142610 File Offset: 0x00140810
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Binary;
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x06007C69 RID: 31849 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsBinary
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06007C6A RID: 31850 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public sealed override BinaryValue AsBinary
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x001ABB56 File Offset: 0x001A9D56
		public sealed override string ToSource()
		{
			return "Binary.FromText(...)";
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x001ABB5D File Offset: 0x001A9D5D
		public sealed override string ToString()
		{
			return "binary";
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x001ABB64 File Offset: 0x001A9D64
		public sealed override object ToOleDb(Type type)
		{
			if (type == typeof(byte[]) || type == typeof(object))
			{
				return this.AsBytes;
			}
			if (type == typeof(Stream))
			{
				return this.Open();
			}
			throw ValueMarshaller.CreateTypeError(this, type);
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x001ABBBC File Offset: 0x001A9DBC
		public override void TestConnection()
		{
			using (Stream stream = this.Open())
			{
				stream.ReadByte();
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06007C6F RID: 31855
		public abstract byte[] AsBytes { get; }

		// Token: 0x06007C70 RID: 31856
		public abstract Stream Open();

		// Token: 0x06007C71 RID: 31857 RVA: 0x001ABBF4 File Offset: 0x001A9DF4
		public virtual Stream Open(bool preferCanSeek)
		{
			return this.Open();
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x001ABBFC File Offset: 0x001A9DFC
		public StreamReader OpenText(Value encoding)
		{
			return this.OpenText(encoding.IsNull ? null : TextEncoding.GetEncoding(encoding, LogicalValue.False, TextEncoding.CodePage.Utf8));
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x001ABC1F File Offset: 0x001A9E1F
		public StreamReader OpenText(Encoding encoding)
		{
			if (encoding == null)
			{
				return new BinaryValue.EngineContextAwareStreamReader(this.Open());
			}
			return new BinaryValue.EngineContextAwareStreamReader(this.Open(), encoding);
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x001ABC3C File Offset: 0x001A9E3C
		public virtual Stream OpenForWrite()
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, this, null);
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06007C75 RID: 31861 RVA: 0x001ABC4A File Offset: 0x001A9E4A
		public virtual BinaryValue End
		{
			get
			{
				return BinaryValue.New(new byte[0]);
			}
		}

		// Token: 0x06007C76 RID: 31862
		public abstract bool TryGetLength(out long Length);

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06007C77 RID: 31863
		public abstract long Length { get; }

		// Token: 0x06007C78 RID: 31864
		public abstract ListValue ToList();

		// Token: 0x06007C79 RID: 31865
		public abstract BinaryValue Range(RowCount offset, RowCount count);

		// Token: 0x06007C7A RID: 31866
		public abstract ListValue Split(RowCount pageSize);

		// Token: 0x06007C7B RID: 31867 RVA: 0x001ABC57 File Offset: 0x001A9E57
		public static BinaryValue Combine(ListValue binaries)
		{
			return new CombinedBinaryValue(binaries);
		}

		// Token: 0x06007C7C RID: 31868
		public abstract bool Equals(BufferedBinaryValue value);

		// Token: 0x06007C7D RID: 31869
		public abstract bool Equals(StreamedBinaryValue value);

		// Token: 0x06007C7E RID: 31870 RVA: 0x001ABC60 File Offset: 0x001A9E60
		protected static bool Equals(BufferedBinaryValue value1, BufferedBinaryValue value2)
		{
			byte[] asBytes = value1.AsBytes;
			byte[] asBytes2 = value2.AsBytes;
			if (asBytes.Length != asBytes2.Length)
			{
				return false;
			}
			for (int i = 0; i < asBytes.Length; i++)
			{
				if (asBytes[i] != asBytes2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x001ABCA0 File Offset: 0x001A9EA0
		protected static bool Equals(BufferedBinaryValue value1, StreamedBinaryValue value2)
		{
			byte[] asBytes = value1.AsBytes;
			bool flag;
			using (Stream stream = value2.Open())
			{
				for (int i = 0; i < asBytes.Length; i++)
				{
					if ((int)asBytes[i] != stream.ReadByte())
					{
						return false;
					}
				}
				flag = stream.ReadByte() == -1;
			}
			return flag;
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x001ABD00 File Offset: 0x001A9F00
		protected static bool Equals(StreamedBinaryValue value1, StreamedBinaryValue value2)
		{
			bool flag;
			using (Stream stream = value1.Open())
			{
				using (Stream stream2 = value2.Open())
				{
					for (;;)
					{
						int num = stream.ReadByte();
						int num2 = stream2.ReadByte();
						if (num != num2)
						{
							break;
						}
						if (num == -1)
						{
							goto Block_5;
						}
					}
					return false;
					Block_5:
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x001ABD70 File Offset: 0x001A9F70
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.Kind == ValueKind.Binary)
			{
				using (Stream stream = this.Open())
				{
					using (Stream stream2 = value.AsBinary.Open())
					{
						int num;
						int num2;
						for (;;)
						{
							num = stream.ReadByte();
							num2 = stream2.ReadByte();
							if (num != num2)
							{
								break;
							}
							if (num == -1)
							{
								goto Block_7;
							}
						}
						return (num < num2) ? (-1) : 1;
						Block_7:
						return 0;
					}
				}
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x001ABDFC File Offset: 0x001A9FFC
		public virtual BinaryValue Optimize()
		{
			return new OptimizedBinaryValue(this);
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x001ABE04 File Offset: 0x001AA004
		public override ActionValue Replace(Value value)
		{
			BinaryValue sourceValue = value.AsBinary;
			return new SimpleBindingActionValue(delegate(FunctionValue binding)
			{
				if (binding != SimpleActionBinding.ReturnNull && binding != SimpleActionBinding.ReturnBinaryLength)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
				}
				return ActionValue.New(delegate
				{
					long num;
					using (Stream stream = new ReadFullBufferStream(sourceValue.Open()))
					{
						using (Stream stream2 = this.OpenForWrite())
						{
							using (EngineContext.Leave())
							{
								using (Stream stream3 = stream.LeaveEngineContext<Stream>())
								{
									using (Stream stream4 = stream2.LeaveEngineContext<Stream>())
									{
										num = stream3.CopyTo(stream4);
									}
								}
							}
						}
					}
					if (binding != SimpleActionBinding.ReturnBinaryLength)
					{
						return Value.Null;
					}
					return NumberValue.New(num);
				});
			});
		}

		// Token: 0x040044C4 RID: 17604
		public static readonly TextValue Placeholder = TextValue.New("[Binary]");

		// Token: 0x040044C5 RID: 17605
		public static readonly BinaryValue Empty = BinaryValue.New(new byte[0]);

		// Token: 0x02001281 RID: 4737
		private class MetaTypeBinaryValue : DelegatingBinaryValue
		{
			// Token: 0x06007C86 RID: 31878 RVA: 0x001ABE4F File Offset: 0x001AA04F
			public MetaTypeBinaryValue(BinaryValue binary, TypeValue type, RecordValue meta)
				: base(binary)
			{
				this.type = type;
				this.meta = meta;
			}

			// Token: 0x170021DF RID: 8671
			// (get) Token: 0x06007C87 RID: 31879 RVA: 0x001ABE66 File Offset: 0x001AA066
			public sealed override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x170021E0 RID: 8672
			// (get) Token: 0x06007C88 RID: 31880 RVA: 0x001ABE6E File Offset: 0x001AA06E
			public sealed override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x040044C6 RID: 17606
			private TypeValue type;

			// Token: 0x040044C7 RID: 17607
			private RecordValue meta;
		}

		// Token: 0x02001282 RID: 4738
		private sealed class EngineContextAwareStreamReader : StreamReader, ILeaveEngineContext<TextReader>
		{
			// Token: 0x06007C89 RID: 31881 RVA: 0x001ABE76 File Offset: 0x001AA076
			public EngineContextAwareStreamReader(Stream stream)
				: base(stream)
			{
			}

			// Token: 0x06007C8A RID: 31882 RVA: 0x001ABE7F File Offset: 0x001AA07F
			public EngineContextAwareStreamReader(Stream stream, Encoding encoding)
				: base(stream, encoding)
			{
			}

			// Token: 0x06007C8B RID: 31883 RVA: 0x001ABE89 File Offset: 0x001AA089
			TextReader ILeaveEngineContext<TextReader>.Leave()
			{
				return new StreamReader(this.BaseStream.LeaveEngineContext<Stream>(), this.CurrentEncoding);
			}
		}
	}
}
