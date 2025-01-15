using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.BinaryFormat
{
	// Token: 0x02000EAD RID: 3757
	internal abstract class BinaryFormatValue : NativeFunctionValue1<Value, BinaryValue>, IBinaryFormat
	{
		// Token: 0x060063F6 RID: 25590 RVA: 0x00156393 File Offset: 0x00154593
		public BinaryFormatValue()
			: base(TypeValue.Any, 1, "binary", TypeValue.Binary)
		{
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool CanReadUInt64
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x00002105 File Offset: 0x00000305
		public virtual BinaryFormatType BinaryFormatType
		{
			get
			{
				return BinaryFormatType.Value;
			}
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x00049610 File Offset: 0x00047810
		public override bool TryGetBinaryFormat(out IBinaryFormat binaryFormat)
		{
			binaryFormat = this;
			return true;
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001563AC File Offset: 0x001545AC
		public override Value TypedInvoke(BinaryValue binary)
		{
			switch (this.BinaryFormatType)
			{
			case BinaryFormatType.Value:
				return this.ReadValue(binary);
			case BinaryFormatType.Items:
				return new BinaryFormatValue.BinaryFormatListValue(this, binary);
			case BinaryFormatType.Stream:
				return new BinaryFormatValue.BinaryFormatBinaryValue(this, binary);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001563F4 File Offset: 0x001545F4
		public Value ReadValue(BinaryValue binary)
		{
			Value value;
			using (Stream stream = binary.Open())
			{
				BinaryFormatReader binaryFormatReader = new BinaryFormatReader(stream);
				value = this.ReadValue(binaryFormatReader);
			}
			return value;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x00156434 File Offset: 0x00154634
		public Value ReadValue(BinaryFormatReader reader)
		{
			Value value;
			if (!this.TryReadValue(reader, out value))
			{
				throw reader.NewUnexpectedEndOfInputException();
			}
			return value;
		}

		// Token: 0x060063FD RID: 25597
		public abstract bool TryReadValue(BinaryFormatReader reader, out Value value);

		// Token: 0x060063FE RID: 25598 RVA: 0x00156454 File Offset: 0x00154654
		public virtual ulong ReadUInt64(BinaryFormatReader reader)
		{
			ulong num;
			if (!this.TryReadUInt64(reader, out num))
			{
				throw reader.NewUnexpectedEndOfInputException();
			}
			return num;
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x00156474 File Offset: 0x00154674
		public virtual bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_NotSupportedForLength, this, null);
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x00156482 File Offset: 0x00154682
		public virtual IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_DoesNotProduceList, Value.Null, null);
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x00156494 File Offset: 0x00154694
		public virtual Stream ReadStream(BinaryFormatReader reader)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_DoesNotProduceBinary, Value.Null, null);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001564A6 File Offset: 0x001546A6
		public static bool TryGetLengthBinaryFormat(Value value, out IBinaryFormat binaryFormat)
		{
			if (!BinaryFormatValue.TryGetBinaryFormat(value, out binaryFormat))
			{
				binaryFormat = null;
				return false;
			}
			if (!binaryFormat.CanReadUInt64)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_NotSupportedForLength, value, null);
			}
			return true;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x001564CD File Offset: 0x001546CD
		public static bool TryGetBinaryFormat(Value value, out IBinaryFormat binaryFormat)
		{
			if (value.IsFunction && value.AsFunction.TryGetBinaryFormat(out binaryFormat))
			{
				return true;
			}
			binaryFormat = null;
			return false;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x001564EC File Offset: 0x001546EC
		public static IBinaryFormat GetBinaryFormat(Value value)
		{
			IBinaryFormat binaryFormat;
			if (BinaryFormatValue.TryGetBinaryFormat(value, out binaryFormat))
			{
				return binaryFormat;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_ExpectBinaryFormat, value, null);
		}

		// Token: 0x02000EAE RID: 3758
		private class BinaryFormatBinaryValue : StreamedBinaryValue
		{
			// Token: 0x06006405 RID: 25605 RVA: 0x00156511 File Offset: 0x00154711
			public BinaryFormatBinaryValue(IBinaryFormat binaryFormat, BinaryValue binaryValue)
			{
				this.binaryFormat = binaryFormat;
				this.binaryValue = binaryValue;
			}

			// Token: 0x06006406 RID: 25606 RVA: 0x00156528 File Offset: 0x00154728
			public override Stream Open()
			{
				Stream stream = this.binaryValue.Open();
				Stream stream2;
				try
				{
					BinaryFormatReader binaryFormatReader = new BinaryFormatReader(stream);
					stream2 = this.binaryFormat.ReadStream(binaryFormatReader);
				}
				catch
				{
					stream.Close();
					throw;
				}
				return stream2;
			}

			// Token: 0x0400367E RID: 13950
			private readonly IBinaryFormat binaryFormat;

			// Token: 0x0400367F RID: 13951
			private readonly BinaryValue binaryValue;
		}

		// Token: 0x02000EAF RID: 3759
		private class BinaryFormatListValue : StreamedListValue
		{
			// Token: 0x06006407 RID: 25607 RVA: 0x00156574 File Offset: 0x00154774
			public BinaryFormatListValue(IBinaryFormat binaryFormat, BinaryValue binaryValue)
			{
				this.binaryFormat = binaryFormat;
				this.binaryValue = binaryValue;
			}

			// Token: 0x06006408 RID: 25608 RVA: 0x0015658C File Offset: 0x0015478C
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				Stream stream = this.binaryValue.Open();
				IEnumerator<IValueReference> enumerator;
				try
				{
					BinaryFormatReader binaryFormatReader = new BinaryFormatReader(stream);
					enumerator = new BinaryFormatValue.BinaryFormatListValue.DisposingEnumerator(this.binaryFormat.ReadItems(binaryFormatReader), binaryFormatReader);
				}
				catch
				{
					stream.Close();
					throw;
				}
				return enumerator;
			}

			// Token: 0x04003680 RID: 13952
			private readonly IBinaryFormat binaryFormat;

			// Token: 0x04003681 RID: 13953
			private readonly BinaryValue binaryValue;

			// Token: 0x02000EB0 RID: 3760
			private class DisposingEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06006409 RID: 25609 RVA: 0x001565DC File Offset: 0x001547DC
				public DisposingEnumerator(IEnumerator<IValueReference> enumerator, BinaryFormatReader reader)
				{
					this.reader = reader;
					this.enumerator = enumerator;
				}

				// Token: 0x17001D11 RID: 7441
				// (get) Token: 0x0600640A RID: 25610 RVA: 0x001565F2 File Offset: 0x001547F2
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x0600640B RID: 25611 RVA: 0x001565FF File Offset: 0x001547FF
				public void Dispose()
				{
					this.reader.Dispose();
				}

				// Token: 0x17001D12 RID: 7442
				// (get) Token: 0x0600640C RID: 25612 RVA: 0x0015660C File Offset: 0x0015480C
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600640D RID: 25613 RVA: 0x00156614 File Offset: 0x00154814
				public bool MoveNext()
				{
					return this.enumerator.MoveNext();
				}

				// Token: 0x0600640E RID: 25614 RVA: 0x00156621 File Offset: 0x00154821
				public void Reset()
				{
					this.enumerator.Reset();
				}

				// Token: 0x04003682 RID: 13954
				private readonly IEnumerator<IValueReference> enumerator;

				// Token: 0x04003683 RID: 13955
				private readonly BinaryFormatReader reader;
			}
		}
	}
}
