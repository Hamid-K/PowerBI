using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002E6 RID: 742
	internal sealed class CodecFactory
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0005DDF0 File Offset: 0x0005BFF0
		private bool GetVBufferCodec(Stream definitionStream, out IValueCodec codec)
		{
			IValueCodec valueCodec;
			if (!this.TryReadCodec(definitionStream, out valueCodec))
			{
				codec = null;
				return false;
			}
			PrimitiveType primitiveType = valueCodec.Type as PrimitiveType;
			Contracts.CheckDecode(primitiveType != null);
			VectorType vectorType;
			using (BinaryReader binaryReader = this.OpenBinaryReader(definitionStream))
			{
				int[] array = Utils.ReadIntArray(binaryReader);
				if (Utils.Size<int>(array) > 0)
				{
					foreach (int num in array)
					{
						Contracts.CheckDecode(num >= 0);
					}
					vectorType = new VectorType(primitiveType, array);
				}
				else
				{
					vectorType = new VectorType(primitiveType, 0);
				}
			}
			Type type = typeof(CodecFactory.VBufferCodec<>).MakeGenericType(new Type[] { primitiveType.RawType });
			codec = (IValueCodec)Activator.CreateInstance(type, new object[] { this, vectorType, valueCodec });
			return true;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
		private bool GetVBufferCodec(ColumnType type, out IValueCodec codec)
		{
			if (!type.IsVector)
			{
				throw Contracts.ExceptParam("type", "type must be a vector type");
			}
			ColumnType itemType = type.ItemType;
			IValueCodec valueCodec;
			if (!this.TryGetCodec(itemType, out valueCodec))
			{
				codec = null;
				return false;
			}
			Type type2 = typeof(CodecFactory.VBufferCodec<>).MakeGenericType(new Type[] { itemType.RawType });
			codec = (IValueCodec)Activator.CreateInstance(type2, new object[] { this, type, valueCodec });
			return true;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0005DF6C File Offset: 0x0005C16C
		private bool GetKeyCodec(Stream definitionStream, out IValueCodec codec)
		{
			IValueCodec valueCodec;
			if (!this.TryReadCodec(definitionStream, out valueCodec))
			{
				codec = null;
				return false;
			}
			PrimitiveType primitiveType = valueCodec.Type as PrimitiveType;
			Contracts.CheckDecode(primitiveType != null);
			Contracts.CheckDecode(KeyType.IsValidDataKind(primitiveType.RawKind));
			KeyType keyType;
			using (BinaryReader binaryReader = this.OpenBinaryReader(definitionStream))
			{
				bool flag = Utils.ReadBoolByte(binaryReader);
				ulong num = binaryReader.ReadUInt64();
				int num2 = binaryReader.ReadInt32();
				Contracts.CheckDecode(num >= 0UL);
				Contracts.CheckDecode(0 <= num2);
				Contracts.CheckDecode((long)num2 <= (long)(ulong.MaxValue - num));
				Contracts.CheckDecode((long)num2 <= (long)DataKindExtensions.ToMaxInt(primitiveType.RawKind));
				Contracts.CheckDecode(flag || num2 == 0);
				keyType = new KeyType(primitiveType.RawKind, num, num2, flag);
			}
			Type type = typeof(CodecFactory.KeyCodec<>).MakeGenericType(new Type[] { primitiveType.RawType });
			codec = (IValueCodec)Activator.CreateInstance(type, new object[] { this, keyType, valueCodec });
			return true;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0005E0A4 File Offset: 0x0005C2A4
		private bool GetKeyCodec(ColumnType type, out IValueCodec codec)
		{
			if (!type.IsKey)
			{
				throw Contracts.ExceptParam("type", "type must be a key type");
			}
			IValueCodec valueCodec;
			if (!this.TryGetCodec(NumberType.FromKind(type.RawKind), out valueCodec))
			{
				codec = null;
				return false;
			}
			Type type2 = typeof(CodecFactory.KeyCodec<>).MakeGenericType(new Type[] { type.RawType });
			codec = (IValueCodec)Activator.CreateInstance(type2, new object[] { this, type, valueCodec });
			return true;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0005E128 File Offset: 0x0005C328
		public CodecFactory(IHostEnvironment env, MemoryStreamPool memPool = null)
		{
			this._host = env.Register("CodecFactory");
			this._memPool = memPool ?? new MemoryStreamPool();
			this._encoding = Encoding.UTF8;
			this._loadNameToCodecCreator = new Dictionary<string, CodecFactory.GetCodecFromStreamDelegate>();
			this._simpleCodecTypeMap = new Dictionary<DataKind, IValueCodec>();
			this.RegisterSimpleCodec<DvInt1>(new CodecFactory.UnsafeTypeCodec<DvInt1>(this));
			this.RegisterSimpleCodec<byte>(new CodecFactory.UnsafeTypeCodec<byte>(this));
			this.RegisterSimpleCodec<DvInt2>(new CodecFactory.UnsafeTypeCodec<DvInt2>(this));
			this.RegisterSimpleCodec<ushort>(new CodecFactory.UnsafeTypeCodec<ushort>(this));
			this.RegisterSimpleCodec<DvInt4>(new CodecFactory.UnsafeTypeCodec<DvInt4>(this));
			this.RegisterSimpleCodec<uint>(new CodecFactory.UnsafeTypeCodec<uint>(this));
			this.RegisterSimpleCodec<DvInt8>(new CodecFactory.UnsafeTypeCodec<DvInt8>(this));
			this.RegisterSimpleCodec<ulong>(new CodecFactory.UnsafeTypeCodec<ulong>(this));
			this.RegisterSimpleCodec<float>(new CodecFactory.UnsafeTypeCodec<float>(this));
			this.RegisterSimpleCodec<double>(new CodecFactory.UnsafeTypeCodec<double>(this));
			this.RegisterSimpleCodec<DvTimeSpan>(new CodecFactory.UnsafeTypeCodec<DvTimeSpan>(this));
			this.RegisterSimpleCodec<DvText>(new CodecFactory.DvTextCodec(this));
			this.RegisterSimpleCodec<DvBool>(new CodecFactory.BoolCodec(this));
			this.RegisterSimpleCodec<DvDateTime>(new CodecFactory.DateTimeCodec(this));
			this.RegisterSimpleCodec<DvDateTimeZone>(new CodecFactory.DateTimeZoneCodec(this));
			this.RegisterSimpleCodec<UInt128>(new CodecFactory.UnsafeTypeCodec<UInt128>(this));
			CodecFactory.OldBoolCodec oldBoolCodec = new CodecFactory.OldBoolCodec(this);
			this.RegisterOtherCodec(oldBoolCodec.LoadName, new CodecFactory.GetCodecFromStreamDelegate(oldBoolCodec.GetCodec));
			this.RegisterOtherCodec("VBuffer", new CodecFactory.GetCodecFromStreamDelegate(this.GetVBufferCodec));
			this.RegisterOtherCodec("Key", new CodecFactory.GetCodecFromStreamDelegate(this.GetKeyCodec));
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0005E28A File Offset: 0x0005C48A
		private BinaryWriter OpenBinaryWriter(Stream stream)
		{
			return new BinaryWriter(stream, this._encoding, true);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0005E299 File Offset: 0x0005C499
		private BinaryReader OpenBinaryReader(Stream stream)
		{
			return new BinaryReader(stream, this._encoding, true);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0005E2A8 File Offset: 0x0005C4A8
		private void RegisterSimpleCodec<T>(CodecFactory.SimpleCodec<T> codec)
		{
			this._loadNameToCodecCreator.Add(codec.LoadName, new CodecFactory.GetCodecFromStreamDelegate(codec.GetCodec));
			this._simpleCodecTypeMap.Add(codec.Type.RawKind, codec);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0005E2DE File Offset: 0x0005C4DE
		private void RegisterOtherCodec(string name, CodecFactory.GetCodecFromStreamDelegate fn)
		{
			this._loadNameToCodecCreator.Add(name, fn);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005E2ED File Offset: 0x0005C4ED
		public bool TryGetCodec(ColumnType type, out IValueCodec codec)
		{
			if (type.IsKey)
			{
				return this.GetKeyCodec(type, out codec);
			}
			if (type.IsVector)
			{
				return this.GetVBufferCodec(type, out codec);
			}
			return this._simpleCodecTypeMap.TryGetValue(type.RawKind, out codec);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0005E324 File Offset: 0x0005C524
		public int WriteCodec(Stream definitionStream, IValueCodec codec)
		{
			checked
			{
				int num3;
				using (BinaryWriter binaryWriter = this.OpenBinaryWriter(definitionStream))
				{
					string loadName = codec.LoadName;
					binaryWriter.Write(loadName);
					int num = this._encoding.GetByteCount(loadName);
					num += Utils.LEB128IntLength(unchecked((ulong)(checked((uint)num))));
					MemoryStream memoryStream = this._memPool.Get();
					int num2 = codec.WriteParameterization(memoryStream);
					Contracts.Check(memoryStream.Length == unchecked((long)num2), "codec description length did not match stream length");
					Contracts.Check(memoryStream.Length <= 2147483647L);
					Utils.WriteLEB128Int(binaryWriter, (ulong)memoryStream.Length);
					num = num + Utils.LEB128IntLength(unchecked((ulong)(checked((uint)memoryStream.Length)))) + num2;
					memoryStream.Position = 0L;
					memoryStream.CopyTo(definitionStream);
					this._memPool.Return(ref memoryStream);
					num3 = num;
				}
				return num3;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0005E3FC File Offset: 0x0005C5FC
		public bool TryReadCodec(Stream definitionStream, out IValueCodec codec)
		{
			bool flag;
			using (IChannel channel = this._host.Start("TryGetCodec"))
			{
				using (BinaryReader binaryReader = new BinaryReader(definitionStream, Encoding.UTF8, true))
				{
					string text = binaryReader.ReadString();
					Contracts.CheckDecode(!string.IsNullOrEmpty(text), "Non-empty signature string expected");
					ulong num = Utils.ReadLEB128Int(binaryReader);
					Contracts.CheckDecode(num <= 9223372036854775807UL, "Codec type definition read from stream too large");
					long num2 = (long)num;
					CodecFactory.GetCodecFromStreamDelegate getCodecFromStreamDelegate;
					if (!this._loadNameToCodecCreator.TryGetValue(text, out getCodecFromStreamDelegate))
					{
						codec = null;
						if (num2 == 0L)
						{
							flag = false;
						}
						else
						{
							if (definitionStream.CanSeek)
							{
								long num3 = definitionStream.Length - definitionStream.Position;
								if (num3 < num2)
								{
									throw Contracts.ExceptDecode(channel, "Codec type definition supposedly has {0} bytes, but end-of-stream reached after {1} bytes", new object[] { num2, num3 });
								}
								definitionStream.Seek(num2, SeekOrigin.Current);
							}
							else
							{
								for (long num4 = 0L; num4 < num2; num4 += 1L)
								{
									if (definitionStream.ReadByte() == -1)
									{
										throw Contracts.ExceptDecode(channel, "Codec type definition supposedly has {0} bytes, but end-of-stream reached after {1} bytes", new object[] { num2, num4 });
									}
								}
							}
							channel.Warning("Did not recognize value codec signature '{0}'", new object[] { text });
							channel.Done();
							flag = false;
						}
					}
					else
					{
						long num5 = (definitionStream.CanSeek ? definitionStream.Position : (-1L));
						bool flag2 = getCodecFromStreamDelegate(definitionStream, out codec);
						if (definitionStream.CanSeek && definitionStream.Position - num5 != num2)
						{
							throw Contracts.ExceptDecode(channel, "Codec type definition supposedly has {0} bytes, but the handler consumed {1}", new object[]
							{
								num2,
								definitionStream.Position - num5
							});
						}
						channel.Done();
						flag = flag2;
					}
				}
			}
			return flag;
		}

		// Token: 0x04000985 RID: 2437
		private readonly Dictionary<string, CodecFactory.GetCodecFromStreamDelegate> _loadNameToCodecCreator;

		// Token: 0x04000986 RID: 2438
		private readonly Dictionary<DataKind, IValueCodec> _simpleCodecTypeMap;

		// Token: 0x04000987 RID: 2439
		private readonly MemoryStreamPool _memPool;

		// Token: 0x04000988 RID: 2440
		private readonly Encoding _encoding;

		// Token: 0x04000989 RID: 2441
		private readonly IHost _host;

		// Token: 0x020002E9 RID: 745
		private abstract class ValueWriterBase<T> : IValueWriter<T>, IValueWriter, IDisposable
		{
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0005E60C File Offset: 0x0005C80C
			protected bool Disposed
			{
				get
				{
					return this._writer == null;
				}
			}

			// Token: 0x060010DA RID: 4314 RVA: 0x0005E617 File Offset: 0x0005C817
			public ValueWriterBase(CodecFactory factory, Stream stream)
			{
				this._factory = factory;
				this._stream = stream;
				this._writer = this._factory.OpenBinaryWriter(this._stream);
			}

			// Token: 0x060010DB RID: 4315 RVA: 0x0005E644 File Offset: 0x0005C844
			public virtual void Dispose()
			{
				if (!this.Disposed)
				{
					this._writer.Dispose();
					this._writer = null;
					this._stream = null;
				}
			}

			// Token: 0x060010DC RID: 4316
			public abstract void Write(ref T value);

			// Token: 0x060010DD RID: 4317 RVA: 0x0005E668 File Offset: 0x0005C868
			public virtual void Write(T[] values, int index, int count)
			{
				for (int i = 0; i < count; i++)
				{
					this.Write(ref values[i + index]);
				}
			}

			// Token: 0x060010DE RID: 4318
			public abstract void Commit();

			// Token: 0x060010DF RID: 4319
			public abstract long GetCommitLengthEstimate();

			// Token: 0x0400098A RID: 2442
			protected readonly CodecFactory _factory;

			// Token: 0x0400098B RID: 2443
			protected Stream _stream;

			// Token: 0x0400098C RID: 2444
			protected BinaryWriter _writer;
		}

		// Token: 0x020002EB RID: 747
		private abstract class ValueReaderBase<T> : IValueReader<T>, IDisposable
		{
			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0005E690 File Offset: 0x0005C890
			protected bool Disposed
			{
				get
				{
					return this._reader == null;
				}
			}

			// Token: 0x060010E4 RID: 4324 RVA: 0x0005E69B File Offset: 0x0005C89B
			public ValueReaderBase(CodecFactory factory, Stream stream)
			{
				this._factory = factory;
				this._stream = stream;
				this._reader = this._factory.OpenBinaryReader(this._stream);
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x0005E6C8 File Offset: 0x0005C8C8
			public virtual void Dispose()
			{
				if (!this.Disposed)
				{
					this._reader.Dispose();
					this._reader = null;
					this._stream = null;
				}
			}

			// Token: 0x060010E6 RID: 4326
			public abstract void MoveNext();

			// Token: 0x060010E7 RID: 4327
			public abstract void Get(ref T value);

			// Token: 0x060010E8 RID: 4328 RVA: 0x0005E6EC File Offset: 0x0005C8EC
			public virtual void Read(T[] values, int index, int count)
			{
				for (int i = 0; i < count; i++)
				{
					this.MoveNext();
					this.Get(ref values[i + index]);
				}
			}

			// Token: 0x0400098D RID: 2445
			protected readonly CodecFactory _factory;

			// Token: 0x0400098E RID: 2446
			protected Stream _stream;

			// Token: 0x0400098F RID: 2447
			protected BinaryReader _reader;
		}

		// Token: 0x020002EE RID: 750
		private abstract class SimpleCodec<T> : IValueCodec<T>, IValueCodec
		{
			// Token: 0x170001AA RID: 426
			// (get) Token: 0x060010EE RID: 4334 RVA: 0x0005E71A File Offset: 0x0005C91A
			public ColumnType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060010EF RID: 4335 RVA: 0x0005E722 File Offset: 0x0005C922
			public virtual string LoadName
			{
				get
				{
					return typeof(T).Name;
				}
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x0005E733 File Offset: 0x0005C933
			public SimpleCodec(CodecFactory factory, ColumnType type)
			{
				this._factory = factory;
				this._type = type;
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x0005E749 File Offset: 0x0005C949
			public bool GetCodec(Stream definitionStream, out IValueCodec codec)
			{
				codec = this;
				return true;
			}

			// Token: 0x060010F2 RID: 4338 RVA: 0x0005E74F File Offset: 0x0005C94F
			public int WriteParameterization(Stream stream)
			{
				return 0;
			}

			// Token: 0x060010F3 RID: 4339
			public abstract IValueWriter<T> OpenWriter(Stream stream);

			// Token: 0x060010F4 RID: 4340
			public abstract IValueReader<T> OpenReader(Stream stream, int items);

			// Token: 0x04000990 RID: 2448
			protected readonly ColumnType _type;

			// Token: 0x04000991 RID: 2449
			protected readonly CodecFactory _factory;
		}

		// Token: 0x020002EF RID: 751
		private sealed class UnsafeTypeCodec<T> : CodecFactory.SimpleCodec<T> where T : struct
		{
			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0005E754 File Offset: 0x0005C954
			public override string LoadName
			{
				get
				{
					DataKind rawKind = base.Type.RawKind;
					switch (rawKind)
					{
					case 1:
						return typeof(sbyte).Name;
					case 2:
					case 4:
					case 6:
						break;
					case 3:
						return typeof(short).Name;
					case 5:
						return typeof(int).Name;
					case 7:
						return typeof(long).Name;
					default:
						if (rawKind == 13)
						{
							return typeof(TimeSpan).Name;
						}
						break;
					}
					return base.LoadName;
				}
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x0005E7EE File Offset: 0x0005C9EE
			private static ColumnType UnsafeColumnType(Type type)
			{
				if (!(type == typeof(DvTimeSpan)))
				{
					return NumberType.FromType(type);
				}
				return TimeSpanType.Instance;
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x0005E80E File Offset: 0x0005CA0E
			public UnsafeTypeCodec(CodecFactory factory)
				: base(factory, CodecFactory.UnsafeTypeCodec<T>.UnsafeColumnType(typeof(T)))
			{
				this._ops = UnsafeTypeOpsFactory.Get<T>();
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x0005E831 File Offset: 0x0005CA31
			public override IValueWriter<T> OpenWriter(Stream stream)
			{
				return new CodecFactory.UnsafeTypeCodec<T>.Writer(this, stream);
			}

			// Token: 0x060010F9 RID: 4345 RVA: 0x0005E83A File Offset: 0x0005CA3A
			public override IValueReader<T> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.UnsafeTypeCodec<T>.Reader(this, stream, items);
			}

			// Token: 0x04000992 RID: 2450
			private readonly UnsafeTypeOps<T> _ops;

			// Token: 0x020002F0 RID: 752
			private sealed class Writer : CodecFactory.ValueWriterBase<T>
			{
				// Token: 0x060010FA RID: 4346 RVA: 0x0005E844 File Offset: 0x0005CA44
				public Writer(CodecFactory.UnsafeTypeCodec<T> codec, Stream stream)
					: base(codec._factory, stream)
				{
					this._buffer = new byte[32768];
					this._ops = codec._ops;
				}

				// Token: 0x060010FB RID: 4347 RVA: 0x0005E86F File Offset: 0x0005CA6F
				public override void Write(ref T value)
				{
					this._ops.Write(value, this._writer);
					this._numWritten += 1L;
				}

				// Token: 0x060010FC RID: 4348 RVA: 0x0005E940 File Offset: 0x0005CB40
				public override void Write(T[] values, int index, int count)
				{
					this._ops.Apply(values, delegate(IntPtr ptr)
					{
						int num = index * this._ops.Size;
						int i = count * this._ops.Size;
						ptr += num;
						while (i > 0)
						{
							int num2 = Math.Min(i, this._buffer.Length);
							Marshal.Copy(ptr, this._buffer, 0, num2);
							this._stream.Write(this._buffer, 0, num2);
							ptr += num2;
							i -= num2;
						}
					});
					this._numWritten += (long)count;
				}

				// Token: 0x060010FD RID: 4349 RVA: 0x0005E994 File Offset: 0x0005CB94
				public override void Commit()
				{
				}

				// Token: 0x060010FE RID: 4350 RVA: 0x0005E996 File Offset: 0x0005CB96
				public override long GetCommitLengthEstimate()
				{
					return (long)this._ops.Size * this._numWritten;
				}

				// Token: 0x04000993 RID: 2451
				private readonly byte[] _buffer;

				// Token: 0x04000994 RID: 2452
				private readonly UnsafeTypeOps<T> _ops;

				// Token: 0x04000995 RID: 2453
				private long _numWritten;
			}

			// Token: 0x020002F1 RID: 753
			private sealed class Reader : CodecFactory.ValueReaderBase<T>
			{
				// Token: 0x060010FF RID: 4351 RVA: 0x0005E9AB File Offset: 0x0005CBAB
				public Reader(CodecFactory.UnsafeTypeCodec<T> codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._buffer = new byte[32768];
					this._ops = codec._ops;
					this._remaining = items;
				}

				// Token: 0x06001100 RID: 4352 RVA: 0x0005E9DD File Offset: 0x0005CBDD
				public override void MoveNext()
				{
					this._value = this._ops.Read(this._reader);
					this._remaining--;
				}

				// Token: 0x06001101 RID: 4353 RVA: 0x0005EA04 File Offset: 0x0005CC04
				public override void Get(ref T value)
				{
					value = this._value;
				}

				// Token: 0x06001102 RID: 4354 RVA: 0x0005EABC File Offset: 0x0005CCBC
				public override void Read(T[] values, int index, int count)
				{
					this._ops.Apply(values, delegate(IntPtr ptr)
					{
						int num = index * this._ops.Size;
						int i = count * this._ops.Size;
						ptr += num;
						while (i > 0)
						{
							int num2 = Math.Min(i, this._buffer.Length);
							Utils.ReadBlock(this._stream, this._buffer, 0, num2);
							Marshal.Copy(this._buffer, 0, ptr, num2);
							ptr += num2;
							i -= num2;
						}
					});
					this._remaining -= count;
				}

				// Token: 0x04000996 RID: 2454
				private readonly byte[] _buffer;

				// Token: 0x04000997 RID: 2455
				private readonly UnsafeTypeOps<T> _ops;

				// Token: 0x04000998 RID: 2456
				private int _remaining;

				// Token: 0x04000999 RID: 2457
				private T _value;
			}
		}

		// Token: 0x020002F2 RID: 754
		private sealed class DvTextCodec : CodecFactory.SimpleCodec<DvText>
		{
			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06001103 RID: 4355 RVA: 0x0005EB0F File Offset: 0x0005CD0F
			public override string LoadName
			{
				get
				{
					return "TextSpan";
				}
			}

			// Token: 0x06001104 RID: 4356 RVA: 0x0005EB16 File Offset: 0x0005CD16
			public DvTextCodec(CodecFactory factory)
				: base(factory, TextType.Instance)
			{
			}

			// Token: 0x06001105 RID: 4357 RVA: 0x0005EB24 File Offset: 0x0005CD24
			public override IValueWriter<DvText> OpenWriter(Stream stream)
			{
				return new CodecFactory.DvTextCodec.Writer(this, stream);
			}

			// Token: 0x06001106 RID: 4358 RVA: 0x0005EB2D File Offset: 0x0005CD2D
			public override IValueReader<DvText> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.DvTextCodec.Reader(this, stream, items);
			}

			// Token: 0x0400099A RID: 2458
			private const int MissingBit = -2147483648;

			// Token: 0x0400099B RID: 2459
			private const int LengthMask = 2147483647;

			// Token: 0x020002F3 RID: 755
			private sealed class Writer : CodecFactory.ValueWriterBase<DvText>
			{
				// Token: 0x06001107 RID: 4359 RVA: 0x0005EB37 File Offset: 0x0005CD37
				public Writer(CodecFactory.DvTextCodec codec, Stream stream)
					: base(codec._factory, stream)
				{
					this._builder = new StringBuilder();
					this._boundaries = new List<int>();
				}

				// Token: 0x06001108 RID: 4360 RVA: 0x0005EB5C File Offset: 0x0005CD5C
				public override void Write(ref DvText value)
				{
					Contracts.Check(this._builder != null, "writer was already committed");
					if (value.IsNA)
					{
						this._boundaries.Add(this._builder.Length | int.MinValue);
						return;
					}
					value.AddToStringBuilder(this._builder);
					this._boundaries.Add(this._builder.Length);
				}

				// Token: 0x06001109 RID: 4361 RVA: 0x0005EBC8 File Offset: 0x0005CDC8
				public override void Commit()
				{
					Contracts.Check(this._builder != null, "writer already committed");
					this._writer.Write(this._boundaries.Count);
					Utils.WriteIntStream(this._writer, this._boundaries);
					this._writer.Write(this._builder.ToString());
					this._builder.Clear();
					this._builder = null;
				}

				// Token: 0x0600110A RID: 4362 RVA: 0x0005EC3C File Offset: 0x0005CE3C
				public override long GetCommitLengthEstimate()
				{
					return 4L * (1L + (long)this._boundaries.Count) + (long)this._builder.Length;
				}

				// Token: 0x0400099C RID: 2460
				private StringBuilder _builder;

				// Token: 0x0400099D RID: 2461
				private List<int> _boundaries;
			}

			// Token: 0x020002F4 RID: 756
			private sealed class Reader : CodecFactory.ValueReaderBase<DvText>
			{
				// Token: 0x0600110B RID: 4363 RVA: 0x0005EC60 File Offset: 0x0005CE60
				public Reader(CodecFactory.DvTextCodec codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._entries = this._reader.ReadInt32();
					Contracts.CheckDecode(this._entries == items);
					this._index = -1;
					this._boundaries = new int[this._entries + 1];
					int num = 0;
					for (int i = 1; i < this._boundaries.Length; i++)
					{
						int num2 = (this._boundaries[i] = this._reader.ReadInt32());
						Contracts.CheckDecode(num2 >= (num & int.MaxValue) || (num2 & int.MaxValue) == (num & int.MaxValue));
						num = num2;
					}
					this._text = this._reader.ReadString();
					Contracts.CheckDecode(this._text.Length == (this._boundaries[this._entries] & int.MaxValue));
				}

				// Token: 0x0600110C RID: 4364 RVA: 0x0005ED3C File Offset: 0x0005CF3C
				public override void MoveNext()
				{
					Contracts.Check(++this._index < this._entries, "reader already read all values");
				}

				// Token: 0x0600110D RID: 4365 RVA: 0x0005ED6C File Offset: 0x0005CF6C
				public override void Get(ref DvText value)
				{
					int num = this._boundaries[this._index + 1];
					if (num < 0)
					{
						value = DvText.NA;
						return;
					}
					value = new DvText(this._text, this._boundaries[this._index] & int.MaxValue, num & int.MaxValue);
				}

				// Token: 0x0400099E RID: 2462
				private readonly int _entries;

				// Token: 0x0400099F RID: 2463
				private readonly int[] _boundaries;

				// Token: 0x040009A0 RID: 2464
				private int _index;

				// Token: 0x040009A1 RID: 2465
				private string _text;
			}
		}

		// Token: 0x020002F5 RID: 757
		private sealed class OldBoolCodec : CodecFactory.SimpleCodec<DvBool>
		{
			// Token: 0x0600110E RID: 4366 RVA: 0x0005EDC4 File Offset: 0x0005CFC4
			public OldBoolCodec(CodecFactory factory)
				: base(factory, BoolType.Instance)
			{
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x0600110F RID: 4367 RVA: 0x0005EDD2 File Offset: 0x0005CFD2
			public override string LoadName
			{
				get
				{
					return typeof(bool).Name;
				}
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x0005EDE3 File Offset: 0x0005CFE3
			public override IValueWriter<DvBool> OpenWriter(Stream stream)
			{
				throw Contracts.ExceptNotSupp("Writing single bit booleans no longer supported");
			}

			// Token: 0x06001111 RID: 4369 RVA: 0x0005EDEF File Offset: 0x0005CFEF
			public override IValueReader<DvBool> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.OldBoolCodec.Reader(this, stream, items);
			}

			// Token: 0x020002F6 RID: 758
			private sealed class Reader : CodecFactory.ValueReaderBase<DvBool>
			{
				// Token: 0x06001112 RID: 4370 RVA: 0x0005EDF9 File Offset: 0x0005CFF9
				public Reader(CodecFactory.OldBoolCodec codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._remaining = items;
					this._currentIndex = -1;
				}

				// Token: 0x06001113 RID: 4371 RVA: 0x0005EE18 File Offset: 0x0005D018
				public override void MoveNext()
				{
					this._remaining--;
					if ((this._currentIndex = (this._currentIndex + 1) & 7) == 0)
					{
						this._currentBits = this._reader.ReadByte();
						return;
					}
					this._currentBits = (byte)(this._currentBits >> 1);
				}

				// Token: 0x06001114 RID: 4372 RVA: 0x0005EE69 File Offset: 0x0005D069
				public override void Get(ref DvBool value)
				{
					value = (this._currentBits & 1) != 0;
				}

				// Token: 0x040009A2 RID: 2466
				private byte _currentBits;

				// Token: 0x040009A3 RID: 2467
				private int _currentIndex;

				// Token: 0x040009A4 RID: 2468
				private int _remaining;
			}
		}

		// Token: 0x020002F7 RID: 759
		private sealed class BoolCodec : CodecFactory.SimpleCodec<DvBool>
		{
			// Token: 0x06001115 RID: 4373 RVA: 0x0005EE84 File Offset: 0x0005D084
			public BoolCodec(CodecFactory factory)
				: base(factory, BoolType.Instance)
			{
			}

			// Token: 0x06001116 RID: 4374 RVA: 0x0005EE92 File Offset: 0x0005D092
			public override IValueWriter<DvBool> OpenWriter(Stream stream)
			{
				return new CodecFactory.BoolCodec.Writer(this, stream);
			}

			// Token: 0x06001117 RID: 4375 RVA: 0x0005EE9B File Offset: 0x0005D09B
			public override IValueReader<DvBool> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.BoolCodec.Reader(this, stream, items);
			}

			// Token: 0x020002F8 RID: 760
			private sealed class Writer : CodecFactory.ValueWriterBase<DvBool>
			{
				// Token: 0x06001118 RID: 4376 RVA: 0x0005EEA5 File Offset: 0x0005D0A5
				public Writer(CodecFactory.BoolCodec codec, Stream stream)
					: base(codec._factory, stream)
				{
				}

				// Token: 0x06001119 RID: 4377 RVA: 0x0005EEB4 File Offset: 0x0005D0B4
				public override void Write(ref DvBool value)
				{
					this._numWritten += 1L;
					if (value.IsTrue)
					{
						this._currentBits |= 1 << this._currentIndex;
					}
					else if (!value.IsFalse)
					{
						this._currentBits |= 2 << this._currentIndex;
					}
					this._currentIndex += 2;
					if (this._currentIndex == 32)
					{
						this._writer.Write(this._currentBits);
						this._currentBits = 0;
						this._currentIndex = 0;
					}
				}

				// Token: 0x0600111A RID: 4378 RVA: 0x0005EF49 File Offset: 0x0005D149
				public override long GetCommitLengthEstimate()
				{
					return 4L * ((this._numWritten - 1L >> 4) + 1L);
				}

				// Token: 0x0600111B RID: 4379 RVA: 0x0005EF5C File Offset: 0x0005D15C
				public override void Commit()
				{
					if (this._currentIndex > 0)
					{
						this._writer.Write(this._currentBits);
						this._currentBits = 0;
						this._currentIndex = 0;
					}
				}

				// Token: 0x040009A5 RID: 2469
				private int _currentBits;

				// Token: 0x040009A6 RID: 2470
				private long _numWritten;

				// Token: 0x040009A7 RID: 2471
				private int _currentIndex;
			}

			// Token: 0x020002F9 RID: 761
			private sealed class Reader : CodecFactory.ValueReaderBase<DvBool>
			{
				// Token: 0x0600111C RID: 4380 RVA: 0x0005EF86 File Offset: 0x0005D186
				public Reader(CodecFactory.BoolCodec codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._remaining = items;
					this._currentSlot = -1;
				}

				// Token: 0x0600111D RID: 4381 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
				public override void MoveNext()
				{
					this._remaining--;
					if ((this._currentSlot = (this._currentSlot + 1) & 15) == 0)
					{
						this._currentBits = this._reader.ReadInt32();
						return;
					}
					this._currentBits = (int)((uint)this._currentBits >> 2);
				}

				// Token: 0x0600111E RID: 4382 RVA: 0x0005EFF8 File Offset: 0x0005D1F8
				public override void Get(ref DvBool value)
				{
					switch (this._currentBits & 3)
					{
					case 0:
						value = DvBool.False;
						return;
					case 1:
						value = DvBool.True;
						return;
					case 2:
						value = DvBool.NA;
						return;
					default:
						throw Contracts.ExceptDecode("Invalid bit pattern in BoolCodec");
					}
				}

				// Token: 0x040009A8 RID: 2472
				private int _currentBits;

				// Token: 0x040009A9 RID: 2473
				private int _currentSlot;

				// Token: 0x040009AA RID: 2474
				private int _remaining;
			}
		}

		// Token: 0x020002FA RID: 762
		private sealed class DateTimeCodec : CodecFactory.SimpleCodec<DvDateTime>
		{
			// Token: 0x0600111F RID: 4383 RVA: 0x0005F050 File Offset: 0x0005D250
			public DateTimeCodec(CodecFactory factory)
				: base(factory, DateTimeType.Instance)
			{
			}

			// Token: 0x06001120 RID: 4384 RVA: 0x0005F05E File Offset: 0x0005D25E
			public override IValueWriter<DvDateTime> OpenWriter(Stream stream)
			{
				return new CodecFactory.DateTimeCodec.Writer(this, stream);
			}

			// Token: 0x06001121 RID: 4385 RVA: 0x0005F067 File Offset: 0x0005D267
			public override IValueReader<DvDateTime> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.DateTimeCodec.Reader(this, stream, items);
			}

			// Token: 0x020002FB RID: 763
			private sealed class Writer : CodecFactory.ValueWriterBase<DvDateTime>
			{
				// Token: 0x06001122 RID: 4386 RVA: 0x0005F071 File Offset: 0x0005D271
				public Writer(CodecFactory.DateTimeCodec codec, Stream stream)
					: base(codec._factory, stream)
				{
				}

				// Token: 0x06001123 RID: 4387 RVA: 0x0005F080 File Offset: 0x0005D280
				public override void Write(ref DvDateTime value)
				{
					long rawValue = value.Ticks.RawValue;
					this._writer.Write(rawValue);
					this._numWritten += 1L;
				}

				// Token: 0x06001124 RID: 4388 RVA: 0x0005F0B7 File Offset: 0x0005D2B7
				public override void Commit()
				{
				}

				// Token: 0x06001125 RID: 4389 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
				public override long GetCommitLengthEstimate()
				{
					return this._numWritten * 8L;
				}

				// Token: 0x040009AB RID: 2475
				private long _numWritten;
			}

			// Token: 0x020002FC RID: 764
			private sealed class Reader : CodecFactory.ValueReaderBase<DvDateTime>
			{
				// Token: 0x06001126 RID: 4390 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
				public Reader(CodecFactory.DateTimeCodec codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._remaining = items;
				}

				// Token: 0x06001127 RID: 4391 RVA: 0x0005F0DC File Offset: 0x0005D2DC
				public override void MoveNext()
				{
					long num = this._reader.ReadInt64();
					Contracts.CheckDecode(num == long.MinValue || num <= 3155378975999999999L);
					this._value = new DvDateTime(num);
					this._remaining--;
				}

				// Token: 0x06001128 RID: 4392 RVA: 0x0005F137 File Offset: 0x0005D337
				public override void Get(ref DvDateTime value)
				{
					value = this._value;
				}

				// Token: 0x040009AC RID: 2476
				private int _remaining;

				// Token: 0x040009AD RID: 2477
				private DvDateTime _value;
			}
		}

		// Token: 0x020002FD RID: 765
		private sealed class DateTimeZoneCodec : CodecFactory.SimpleCodec<DvDateTimeZone>
		{
			// Token: 0x06001129 RID: 4393 RVA: 0x0005F14C File Offset: 0x0005D34C
			public DateTimeZoneCodec(CodecFactory factory)
				: base(factory, DateTimeZoneType.Instance)
			{
				this._shortBufferPool = new MadeObjectPool<short[]>(() => null);
				this._longBufferPool = new MadeObjectPool<long[]>(() => null);
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x0005F1B5 File Offset: 0x0005D3B5
			public override IValueWriter<DvDateTimeZone> OpenWriter(Stream stream)
			{
				return new CodecFactory.DateTimeZoneCodec.Writer(this, stream);
			}

			// Token: 0x0600112B RID: 4395 RVA: 0x0005F1BE File Offset: 0x0005D3BE
			public override IValueReader<DvDateTimeZone> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.DateTimeZoneCodec.Reader(this, stream, items);
			}

			// Token: 0x040009AE RID: 2478
			private readonly MadeObjectPool<short[]> _shortBufferPool;

			// Token: 0x040009AF RID: 2479
			private readonly MadeObjectPool<long[]> _longBufferPool;

			// Token: 0x020002FE RID: 766
			private sealed class Writer : CodecFactory.ValueWriterBase<DvDateTimeZone>
			{
				// Token: 0x0600112E RID: 4398 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
				public Writer(CodecFactory.DateTimeZoneCodec codec, Stream stream)
					: base(codec._factory, stream)
				{
					this._offsets = new List<short>();
					this._ticks = new List<long>();
				}

				// Token: 0x0600112F RID: 4399 RVA: 0x0005F1F0 File Offset: 0x0005D3F0
				public override void Write(ref DvDateTimeZone value)
				{
					DvInt8 ticks = value.ClockDateTime.Ticks;
					DvInt2 offsetMinutes = value.OffsetMinutes;
					this._ticks.Add(ticks.RawValue);
					if (ticks.IsNA)
					{
						this._offsets.Add(0);
						return;
					}
					this._offsets.Add(offsetMinutes.RawValue);
				}

				// Token: 0x06001130 RID: 4400 RVA: 0x0005F24D File Offset: 0x0005D44D
				public override void Commit()
				{
					Utils.WriteShortStream(this._writer, this._offsets);
					Utils.WriteLongStream(this._writer, this._ticks);
					this._offsets = null;
					this._ticks = null;
				}

				// Token: 0x06001131 RID: 4401 RVA: 0x0005F281 File Offset: 0x0005D481
				public override long GetCommitLengthEstimate()
				{
					return (long)this._offsets.Count * 10L;
				}

				// Token: 0x040009B2 RID: 2482
				private List<short> _offsets;

				// Token: 0x040009B3 RID: 2483
				private List<long> _ticks;
			}

			// Token: 0x020002FF RID: 767
			private sealed class Reader : CodecFactory.ValueReaderBase<DvDateTimeZone>
			{
				// Token: 0x06001132 RID: 4402 RVA: 0x0005F294 File Offset: 0x0005D494
				public Reader(CodecFactory.DateTimeZoneCodec codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._codec = codec;
					this._entries = items;
					this._index = -1;
					this._offsets = this._codec._shortBufferPool.Get();
					Utils.EnsureSize<short>(ref this._offsets, this._entries, false);
					for (int i = 0; i < this._entries; i++)
					{
						this._offsets[i] = this._reader.ReadInt16();
						Contracts.CheckDecode(-840L <= (long)this._offsets[i] && (long)this._offsets[i] <= 840L);
					}
					this._ticks = this._codec._longBufferPool.Get();
					Utils.EnsureSize<long>(ref this._ticks, this._entries, false);
					for (int j = 0; j < this._entries; j++)
					{
						this._ticks[j] = this._reader.ReadInt64();
						Contracts.CheckDecode(this._ticks[j] == long.MinValue || this._ticks[j] <= 3155378975999999999L);
					}
				}

				// Token: 0x06001133 RID: 4403 RVA: 0x0005F3BC File Offset: 0x0005D5BC
				public override void MoveNext()
				{
					Contracts.Check(++this._index < this._entries, "reader already read all values");
				}

				// Token: 0x06001134 RID: 4404 RVA: 0x0005F3EC File Offset: 0x0005D5EC
				public override void Get(ref DvDateTimeZone value)
				{
					value = new DvDateTimeZone(this._ticks[this._index], this._offsets[this._index]);
				}

				// Token: 0x06001135 RID: 4405 RVA: 0x0005F420 File Offset: 0x0005D620
				public override void Dispose()
				{
					if (!this._disposed)
					{
						this._codec._shortBufferPool.Return(this._offsets);
						this._codec._longBufferPool.Return(this._ticks);
						this._offsets = null;
						this._ticks = null;
						this._disposed = true;
					}
					base.Dispose();
				}

				// Token: 0x040009B4 RID: 2484
				private readonly CodecFactory.DateTimeZoneCodec _codec;

				// Token: 0x040009B5 RID: 2485
				private readonly int _entries;

				// Token: 0x040009B6 RID: 2486
				private short[] _offsets;

				// Token: 0x040009B7 RID: 2487
				private long[] _ticks;

				// Token: 0x040009B8 RID: 2488
				private int _index;

				// Token: 0x040009B9 RID: 2489
				private bool _disposed;
			}
		}

		// Token: 0x02000300 RID: 768
		private sealed class VBufferCodec<T> : IValueCodec<VBuffer<T>>, IValueCodec
		{
			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005F47C File Offset: 0x0005D67C
			public string LoadName
			{
				get
				{
					return "VBuffer";
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06001137 RID: 4407 RVA: 0x0005F483 File Offset: 0x0005D683
			public ColumnType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06001138 RID: 4408 RVA: 0x0005F494 File Offset: 0x0005D694
			public VBufferCodec(CodecFactory factory, VectorType type, IValueCodec<T> innerCodec)
			{
				this._factory = factory;
				this._type = type;
				this._innerCodec = innerCodec;
				this._bufferPool = new MadeObjectPool<T[]>(() => null);
				if (typeof(T) == typeof(int))
				{
					this._intBufferPool = this._bufferPool as MadeObjectPool<int[]>;
					return;
				}
				this._intBufferPool = new MadeObjectPool<int[]>(() => null);
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x0005F53C File Offset: 0x0005D73C
			public int WriteParameterization(Stream stream)
			{
				int num = this._factory.WriteCodec(stream, this._innerCodec);
				int dimCount = this._type.DimCount;
				num += 4 * (1 + dimCount);
				using (BinaryWriter binaryWriter = this._factory.OpenBinaryWriter(stream))
				{
					binaryWriter.Write(dimCount);
					for (int i = 0; i < dimCount; i++)
					{
						binaryWriter.Write(this._type.GetDim(i));
					}
				}
				return num;
			}

			// Token: 0x0600113A RID: 4410 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
			public IValueWriter<VBuffer<T>> OpenWriter(Stream stream)
			{
				return new CodecFactory.VBufferCodec<T>.Writer(this, stream);
			}

			// Token: 0x0600113B RID: 4411 RVA: 0x0005F5C9 File Offset: 0x0005D7C9
			public IValueReader<VBuffer<T>> OpenReader(Stream stream, int items)
			{
				return new CodecFactory.VBufferCodec<T>.Reader(this, stream, items);
			}

			// Token: 0x040009BA RID: 2490
			private readonly CodecFactory _factory;

			// Token: 0x040009BB RID: 2491
			private readonly VectorType _type;

			// Token: 0x040009BC RID: 2492
			private readonly IValueCodec<T> _innerCodec;

			// Token: 0x040009BD RID: 2493
			private readonly MadeObjectPool<T[]> _bufferPool;

			// Token: 0x040009BE RID: 2494
			private readonly MadeObjectPool<int[]> _intBufferPool;

			// Token: 0x02000301 RID: 769
			private sealed class Writer : CodecFactory.ValueWriterBase<VBuffer<T>>
			{
				// Token: 0x170001B1 RID: 433
				// (get) Token: 0x0600113E RID: 4414 RVA: 0x0005F5D3 File Offset: 0x0005D7D3
				private bool FixedLength
				{
					get
					{
						return this._size > 0;
					}
				}

				// Token: 0x0600113F RID: 4415 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
				public Writer(CodecFactory.VBufferCodec<T> codec, Stream stream)
					: base(codec._factory, stream)
				{
					this._size = codec._type.VectorSize;
					this._lengths = (this.FixedLength ? null : new List<int>());
					this._counts = new List<int>();
					this._indices = new List<int>();
					this._valuesStream = this._factory._memPool.Get();
					this._valueWriter = codec._innerCodec.OpenWriter(this._valuesStream);
				}

				// Token: 0x06001140 RID: 4416 RVA: 0x0005F664 File Offset: 0x0005D864
				public override void Commit()
				{
					Contracts.Check(this._valuesStream != null, "writer already committed");
					this._valueWriter.Commit();
					this._valueWriter.Dispose();
					this._valueWriter = null;
					this._writer.Write(this._counts.Count);
					if (this.FixedLength)
					{
						this._writer.Write(this._size);
					}
					else
					{
						int num = ((this._lengths.Count == 0) ? 0 : this._lengths[0]);
						for (int i = 1; i < this._lengths.Count; i++)
						{
							if (num != this._lengths[i])
							{
								num = 0;
								break;
							}
						}
						this._writer.Write(num);
						if (num == 0)
						{
							Utils.WriteIntStream(this._writer, this._lengths);
						}
					}
					Utils.WriteIntStream(this._writer, this._counts);
					this._writer.Write(this._indices.Count);
					Utils.WriteIntStream(this._writer, this._indices);
					ArraySegment<byte> arraySegment;
					Utils.TryGetBuffer(this._valuesStream, ref arraySegment);
					this._stream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					this._factory._memPool.Return(ref this._valuesStream);
				}

				// Token: 0x06001141 RID: 4417 RVA: 0x0005F7BC File Offset: 0x0005D9BC
				public override long GetCommitLengthEstimate()
				{
					long num = 4L * (2L + (long)Utils.Size<int>(this._lengths) + (long)this._counts.Count + 1L + (long)this._indices.Count);
					return num + this._valueWriter.GetCommitLengthEstimate();
				}

				// Token: 0x06001142 RID: 4418 RVA: 0x0005F808 File Offset: 0x0005DA08
				public override void Write(ref VBuffer<T> value)
				{
					Contracts.Check(this._valuesStream != null, "writer already committed");
					if (this.FixedLength)
					{
						if (value.Length != this._size)
						{
							throw Contracts.Except("Length mismatch: expected {0} slots but got {1}", new object[] { this._size, value.Length });
						}
					}
					else
					{
						this._lengths.Add(value.Length);
					}
					if (value.IsDense)
					{
						this._counts.Add(-1);
						this._valueWriter.Write(value.Values, 0, value.Length);
						return;
					}
					this._counts.Add(value.Count);
					if (value.Count > 0)
					{
						this._indices.AddRange(value.Indices.Take(value.Count));
						this._valueWriter.Write(value.Values, 0, value.Count);
					}
				}

				// Token: 0x040009C1 RID: 2497
				private readonly int _size;

				// Token: 0x040009C2 RID: 2498
				private readonly List<int> _lengths;

				// Token: 0x040009C3 RID: 2499
				private readonly List<int> _counts;

				// Token: 0x040009C4 RID: 2500
				private readonly List<int> _indices;

				// Token: 0x040009C5 RID: 2501
				private MemoryStream _valuesStream;

				// Token: 0x040009C6 RID: 2502
				private IValueWriter<T> _valueWriter;
			}

			// Token: 0x02000302 RID: 770
			private sealed class Reader : CodecFactory.ValueReaderBase<VBuffer<T>>
			{
				// Token: 0x170001B2 RID: 434
				// (get) Token: 0x06001143 RID: 4419 RVA: 0x0005F8FC File Offset: 0x0005DAFC
				private bool FixedLength
				{
					get
					{
						return this._size > 0;
					}
				}

				// Token: 0x06001144 RID: 4420 RVA: 0x0005F908 File Offset: 0x0005DB08
				public Reader(CodecFactory.VBufferCodec<T> codec, Stream stream, int items)
					: base(codec._factory, stream)
				{
					this._codec = codec;
					this._numVectors = this._reader.ReadInt32();
					Contracts.CheckDecode(this._numVectors == items);
					this._size = this._reader.ReadInt32();
					if (codec._type.IsKnownSizeVector)
					{
						Contracts.CheckDecode(codec._type.VectorSize == this._size);
					}
					else
					{
						Contracts.CheckDecode(this._size >= 0);
					}
					if (!this.FixedLength)
					{
						this._lengths = this.ReadIntArray(this._numVectors);
					}
					this._counts = this.ReadIntArray(this._numVectors);
					int num = this._reader.ReadInt32();
					Contracts.CheckDecode(num >= 0);
					this._indices = this.ReadIntArray(num);
					int num2 = 0;
					int i = 0;
					int num3 = 0;
					while (i < this._numVectors)
					{
						int num4 = this._counts[i];
						int num5 = (this.FixedLength ? this._size : this._lengths[i]);
						Contracts.CheckDecode(num5 >= 0);
						if (num4 < 0)
						{
							Contracts.CheckDecode(num4 == -1);
							num4 = num5;
							num2 += num4;
						}
						else
						{
							Contracts.CheckDecode(num4 < num5);
							num += num4;
							num2 += num4;
							int num6 = -1;
							num4 += num3;
							for (int j = num3; j < num4; j++)
							{
								Contracts.CheckDecode(num6 < this._indices[j]);
								num6 = this._indices[j];
							}
							num3 = num4;
							Contracts.CheckDecode(num6 < num5);
						}
						i++;
					}
					T[] array = codec._bufferPool.Get();
					Utils.EnsureSize<T>(ref array, num2, false);
					if (num2 > 0)
					{
						using (IValueReader<T> valueReader = codec._innerCodec.OpenReader(stream, num2))
						{
							valueReader.Read(array, 0, num2);
						}
					}
					this._values = array;
					this._vectorIndex = -1;
				}

				// Token: 0x06001145 RID: 4421 RVA: 0x0005FB04 File Offset: 0x0005DD04
				public override void Dispose()
				{
					if (!this._disposed)
					{
						this._codec._bufferPool.Return(this._values);
						this._codec._intBufferPool.Return(this._counts);
						this._codec._intBufferPool.Return(this._indices);
						if (this._lengths != null)
						{
							this._codec._intBufferPool.Return(this._lengths);
						}
						this._disposed = true;
					}
					base.Dispose();
				}

				// Token: 0x06001146 RID: 4422 RVA: 0x0005FB88 File Offset: 0x0005DD88
				private int[] ReadIntArray(int count)
				{
					int[] array = this._codec._intBufferPool.Get();
					Utils.EnsureSize<int>(ref array, count, false);
					for (int i = 0; i < count; i++)
					{
						array[i] = this._reader.ReadInt32();
					}
					return array;
				}

				// Token: 0x06001147 RID: 4423 RVA: 0x0005FBCC File Offset: 0x0005DDCC
				public override void MoveNext()
				{
					if (this._vectorIndex >= 0)
					{
						int num = this._counts[this._vectorIndex];
						if (num < 0)
						{
							this._valuesOffset += (this.FixedLength ? this._size : this._lengths[this._vectorIndex]);
						}
						else
						{
							this._indicesOffset += num;
							this._valuesOffset += num;
						}
					}
					this._vectorIndex++;
				}

				// Token: 0x06001148 RID: 4424 RVA: 0x0005FC4C File Offset: 0x0005DE4C
				public override void Get(ref VBuffer<T> value)
				{
					int num = (this.FixedLength ? this._size : this._lengths[this._vectorIndex]);
					int num2 = this._counts[this._vectorIndex];
					int[] indices = value.Indices;
					T[] values = value.Values;
					if (num2 < 0)
					{
						if (num > 0)
						{
							Utils.EnsureSize<T>(ref values, num, true);
							Array.Copy(this._values, this._valuesOffset, values, 0, num);
						}
						value = new VBuffer<T>(num, values, indices);
						return;
					}
					if (num2 > 0)
					{
						Utils.EnsureSize<T>(ref values, num2, true);
						Utils.EnsureSize<int>(ref indices, num2, true);
						Array.Copy(this._values, this._valuesOffset, values, 0, num2);
						Array.Copy(this._indices, this._indicesOffset, indices, 0, num2);
					}
					value = new VBuffer<T>(num, num2, values, indices);
				}

				// Token: 0x040009C7 RID: 2503
				private readonly CodecFactory.VBufferCodec<T> _codec;

				// Token: 0x040009C8 RID: 2504
				private readonly int _size;

				// Token: 0x040009C9 RID: 2505
				private readonly int _numVectors;

				// Token: 0x040009CA RID: 2506
				private readonly int[] _lengths;

				// Token: 0x040009CB RID: 2507
				private readonly int[] _counts;

				// Token: 0x040009CC RID: 2508
				private readonly int[] _indices;

				// Token: 0x040009CD RID: 2509
				private readonly T[] _values;

				// Token: 0x040009CE RID: 2510
				private bool _disposed;

				// Token: 0x040009CF RID: 2511
				private int _vectorIndex;

				// Token: 0x040009D0 RID: 2512
				private int _indicesOffset;

				// Token: 0x040009D1 RID: 2513
				private int _valuesOffset;
			}
		}

		// Token: 0x02000303 RID: 771
		private sealed class KeyCodec<T> : IValueCodec<T>, IValueCodec
		{
			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06001149 RID: 4425 RVA: 0x0005FD17 File Offset: 0x0005DF17
			public string LoadName
			{
				get
				{
					return "Key";
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005FD1E File Offset: 0x0005DF1E
			public ColumnType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x0600114B RID: 4427 RVA: 0x0005FD26 File Offset: 0x0005DF26
			public KeyCodec(CodecFactory factory, KeyType type, IValueCodec<T> innerCodec)
			{
				this._factory = factory;
				this._type = type;
				this._innerCodec = innerCodec;
			}

			// Token: 0x0600114C RID: 4428 RVA: 0x0005FD44 File Offset: 0x0005DF44
			public int WriteParameterization(Stream stream)
			{
				int num = this._factory.WriteCodec(stream, this._innerCodec);
				using (BinaryWriter binaryWriter = this._factory.OpenBinaryWriter(stream))
				{
					Utils.WriteBoolByte(binaryWriter, this._type.Contiguous);
					num++;
					binaryWriter.Write(this._type.Min);
					num += 8;
					binaryWriter.Write(this._type.Count);
					num += 4;
				}
				return num;
			}

			// Token: 0x0600114D RID: 4429 RVA: 0x0005FDD0 File Offset: 0x0005DFD0
			public IValueWriter<T> OpenWriter(Stream stream)
			{
				return this._innerCodec.OpenWriter(stream);
			}

			// Token: 0x0600114E RID: 4430 RVA: 0x0005FDDE File Offset: 0x0005DFDE
			public IValueReader<T> OpenReader(Stream stream, int items)
			{
				return this._innerCodec.OpenReader(stream, items);
			}

			// Token: 0x040009D2 RID: 2514
			private readonly CodecFactory _factory;

			// Token: 0x040009D3 RID: 2515
			private readonly KeyType _type;

			// Token: 0x040009D4 RID: 2516
			private readonly IValueCodec<T> _innerCodec;
		}

		// Token: 0x02000304 RID: 772
		// (Invoke) Token: 0x06001150 RID: 4432
		private delegate bool GetCodecFromStreamDelegate(Stream definitionStream, out IValueCodec codec);

		// Token: 0x02000305 RID: 773
		// (Invoke) Token: 0x06001154 RID: 4436
		private delegate bool GetCodecFromTypeDelegate(ColumnType type, out IValueCodec codec);
	}
}
