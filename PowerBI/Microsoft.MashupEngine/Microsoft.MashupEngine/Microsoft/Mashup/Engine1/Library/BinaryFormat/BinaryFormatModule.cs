using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.BinaryFormat
{
	// Token: 0x02000E6F RID: 3695
	internal class BinaryFormatModule : Module
	{
		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06006316 RID: 25366 RVA: 0x00154126 File Offset: 0x00152326
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(32, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return ByteOrderValue.Type.GetName();
						case 1:
							return ByteOrderValue.LittleEndian.GetName();
						case 2:
							return ByteOrderValue.BigEndian.GetName();
						case 3:
							return BinaryOccurrenceValue.AlternateOptional.GetName();
						case 4:
							return BinaryOccurrenceValue.AlternateRequired.GetName();
						case 5:
							return BinaryOccurrenceValue.AlternateRepeating.GetName();
						case 6:
							return BinaryOccurrenceValue.Type.GetName();
						case 7:
							return BinaryOccurrenceValue.Optional.GetName();
						case 8:
							return BinaryOccurrenceValue.Required.GetName();
						case 9:
							return BinaryOccurrenceValue.Repeating.GetName();
						case 10:
							return "BinaryFormat.SignedInteger16";
						case 11:
							return "BinaryFormat.SignedInteger32";
						case 12:
							return "BinaryFormat.SignedInteger64";
						case 13:
							return "BinaryFormat.UnsignedInteger16";
						case 14:
							return "BinaryFormat.UnsignedInteger32";
						case 15:
							return "BinaryFormat.UnsignedInteger64";
						case 16:
							return "BinaryFormat.Single";
						case 17:
							return "BinaryFormat.Double";
						case 18:
							return "BinaryFormat.Decimal";
						case 19:
							return "BinaryFormat.7BitEncodedUnsignedInteger";
						case 20:
							return "BinaryFormat.7BitEncodedSignedInteger";
						case 21:
							return "BinaryFormat.Byte";
						case 22:
							return "BinaryFormat.Binary";
						case 23:
							return "BinaryFormat.Record";
						case 24:
							return "BinaryFormat.List";
						case 25:
							return "BinaryFormat.Text";
						case 26:
							return "BinaryFormat.Transform";
						case 27:
							return "BinaryFormat.Length";
						case 28:
							return "BinaryFormat.Choice";
						case 29:
							return "BinaryFormat.ByteOrder";
						case 30:
							return "BinaryFormat.Group";
						case 31:
							return "BinaryFormat.Null";
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00154162 File Offset: 0x00152362
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return ByteOrderValue.Type;
				case 1:
					return ByteOrderValue.LittleEndian;
				case 2:
					return ByteOrderValue.BigEndian;
				case 3:
					return BinaryOccurrenceValue.AlternateOptional;
				case 4:
					return BinaryOccurrenceValue.AlternateRequired;
				case 5:
					return BinaryOccurrenceValue.AlternateRepeating;
				case 6:
					return BinaryOccurrenceValue.Type;
				case 7:
					return BinaryOccurrenceValue.Optional;
				case 8:
					return BinaryOccurrenceValue.Required;
				case 9:
					return BinaryOccurrenceValue.Repeating;
				case 10:
					return new BinaryFormatModule.SignedInteger16BinaryFormatValue();
				case 11:
					return new BinaryFormatModule.SignedInteger32BinaryFormatValue();
				case 12:
					return new BinaryFormatModule.SignedInteger64BinaryFormatValue();
				case 13:
					return new BinaryFormatModule.UnsignedInteger16BinaryFormatValue();
				case 14:
					return new BinaryFormatModule.UnsignedInteger32BinaryFormatValue();
				case 15:
					return new BinaryFormatModule.UnsignedInteger64BinaryFormatValue();
				case 16:
					return new BinaryFormatModule.SingleBinaryFormatValue();
				case 17:
					return new BinaryFormatModule.DoubleBinaryFormatValue();
				case 18:
					return new BinaryFormatModule.DecimalBinaryFormatValue();
				case 19:
					return new BinaryFormatModule.SevenBitEncodedUnsignedIntegerBinaryFormatValue();
				case 20:
					return new BinaryFormatModule.SevenBitEncodedSignedIntegerBinaryFormatValue();
				case 21:
					return new BinaryFormatModule.ByteBinaryFormatValue();
				case 22:
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue();
				case 23:
					return new BinaryFormatModule.BinaryFormatRecordFunctionValue();
				case 24:
					return new BinaryFormatModule.BinaryFormatListFunctionValue();
				case 25:
					return new BinaryFormatModule.BinaryFormatTextFunctionValue();
				case 26:
					return new BinaryFormatModule.BinaryFormatTransformFunctionValue();
				case 27:
					return new BinaryFormatModule.BinaryFormatLengthFunctionValue();
				case 28:
					return new BinaryFormatModule.BinaryFormatChoiceFunctionValue();
				case 29:
					return new BinaryFormatModule.BinaryFormatByteOrderFunctionValue();
				case 30:
					return new BinaryFormatModule.BinaryFormatGroupFunctionValue();
				case 31:
					return new BinaryFormatModule.NullBinaryFormatValue();
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x040035EF RID: 13807
		public const string BinaryFormatSignedInteger16 = "BinaryFormat.SignedInteger16";

		// Token: 0x040035F0 RID: 13808
		public const string BinaryFormatSignedInteger32 = "BinaryFormat.SignedInteger32";

		// Token: 0x040035F1 RID: 13809
		public const string BinaryFormatSignedInteger64 = "BinaryFormat.SignedInteger64";

		// Token: 0x040035F2 RID: 13810
		public const string BinaryFormatUnsignedInteger16 = "BinaryFormat.UnsignedInteger16";

		// Token: 0x040035F3 RID: 13811
		public const string BinaryFormatUnsignedInteger32 = "BinaryFormat.UnsignedInteger32";

		// Token: 0x040035F4 RID: 13812
		public const string BinaryFormatUnsignedInteger64 = "BinaryFormat.UnsignedInteger64";

		// Token: 0x040035F5 RID: 13813
		public const string BinaryFormatSingle = "BinaryFormat.Single";

		// Token: 0x040035F6 RID: 13814
		public const string BinaryFormatDouble = "BinaryFormat.Double";

		// Token: 0x040035F7 RID: 13815
		public const string BinaryFormatDecimal = "BinaryFormat.Decimal";

		// Token: 0x040035F8 RID: 13816
		public const string BinaryFormat7BitEncodedUnsignedInteger = "BinaryFormat.7BitEncodedUnsignedInteger";

		// Token: 0x040035F9 RID: 13817
		public const string BinaryFormat7BitEncodedSignedInteger = "BinaryFormat.7BitEncodedSignedInteger";

		// Token: 0x040035FA RID: 13818
		public const string BinaryFormatByte = "BinaryFormat.Byte";

		// Token: 0x040035FB RID: 13819
		public const string BinaryFormatBinary = "BinaryFormat.Binary";

		// Token: 0x040035FC RID: 13820
		public const string BinaryFormatRecord = "BinaryFormat.Record";

		// Token: 0x040035FD RID: 13821
		public const string BinaryFormatList = "BinaryFormat.List";

		// Token: 0x040035FE RID: 13822
		public const string BinaryFormatText = "BinaryFormat.Text";

		// Token: 0x040035FF RID: 13823
		public const string BinaryFormatTransform = "BinaryFormat.Transform";

		// Token: 0x04003600 RID: 13824
		public const string BinaryFormatLength = "BinaryFormat.Length";

		// Token: 0x04003601 RID: 13825
		public const string BinaryFormatChoice = "BinaryFormat.Choice";

		// Token: 0x04003602 RID: 13826
		public const string BinaryFormatByteOrder = "BinaryFormat.ByteOrder";

		// Token: 0x04003603 RID: 13827
		public const string BinaryFormatGroup = "BinaryFormat.Group";

		// Token: 0x04003604 RID: 13828
		public const string BinaryFormatNull = "BinaryFormat.Null";

		// Token: 0x04003605 RID: 13829
		private Keys exportKeys;

		// Token: 0x02000E70 RID: 3696
		private enum Exports
		{
			// Token: 0x04003607 RID: 13831
			ByteOrder_Type,
			// Token: 0x04003608 RID: 13832
			ByteOrder_LittleEndian,
			// Token: 0x04003609 RID: 13833
			ByteOrder_BigEndian,
			// Token: 0x0400360A RID: 13834
			Occurrence_Optional,
			// Token: 0x0400360B RID: 13835
			Occurrence_Required,
			// Token: 0x0400360C RID: 13836
			Occurrence_Repeating,
			// Token: 0x0400360D RID: 13837
			BinaryOccurrence_Type,
			// Token: 0x0400360E RID: 13838
			BinaryOccurrence_Optional,
			// Token: 0x0400360F RID: 13839
			BinaryOccurrence_Required,
			// Token: 0x04003610 RID: 13840
			BinaryOccurrence_Repeating,
			// Token: 0x04003611 RID: 13841
			BinaryFormatSignedInteger16,
			// Token: 0x04003612 RID: 13842
			BinaryFormatSignedInteger32,
			// Token: 0x04003613 RID: 13843
			BinaryFormatSignedInteger64,
			// Token: 0x04003614 RID: 13844
			BinaryFormatUnsignedInteger16,
			// Token: 0x04003615 RID: 13845
			BinaryFormatUnsignedInteger32,
			// Token: 0x04003616 RID: 13846
			BinaryFormatUnsignedInteger64,
			// Token: 0x04003617 RID: 13847
			BinaryFormatSingle,
			// Token: 0x04003618 RID: 13848
			BinaryFormatDouble,
			// Token: 0x04003619 RID: 13849
			BinaryFormatDecimal,
			// Token: 0x0400361A RID: 13850
			BinaryFormat7BitEncodedUnsignedInteger,
			// Token: 0x0400361B RID: 13851
			BinaryFormat7BitEncodedSignedInteger,
			// Token: 0x0400361C RID: 13852
			BinaryFormatByte,
			// Token: 0x0400361D RID: 13853
			BinaryFormatBinary,
			// Token: 0x0400361E RID: 13854
			BinaryFormatRecord,
			// Token: 0x0400361F RID: 13855
			BinaryFormatList,
			// Token: 0x04003620 RID: 13856
			BinaryFormatText,
			// Token: 0x04003621 RID: 13857
			BinaryFormatTransform,
			// Token: 0x04003622 RID: 13858
			BinaryFormatLength,
			// Token: 0x04003623 RID: 13859
			BinaryFormatChoice,
			// Token: 0x04003624 RID: 13860
			BinaryFormatByteOrder,
			// Token: 0x04003625 RID: 13861
			BinaryFormatGroup,
			// Token: 0x04003626 RID: 13862
			BinaryFormatNull,
			// Token: 0x04003627 RID: 13863
			Count
		}

		// Token: 0x02000E71 RID: 3697
		private class SignedInteger16BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x06006318 RID: 25368 RVA: 0x00154190 File Offset: 0x00152390
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				short num;
				if (reader.TryReadInt16(out num))
				{
					value = NumberValue.New((int)num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E72 RID: 3698
		private class SignedInteger32BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x0600631A RID: 25370 RVA: 0x001541C0 File Offset: 0x001523C0
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				int num;
				if (reader.TryReadInt32(out num))
				{
					value = NumberValue.New(num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E73 RID: 3699
		private class SignedInteger64BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x0600631C RID: 25372 RVA: 0x001541E8 File Offset: 0x001523E8
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				long num;
				if (reader.TryReadInt64(out num))
				{
					value = NumberValue.New(num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E74 RID: 3700
		private class UnsignedInteger16BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x17001CE6 RID: 7398
			// (get) Token: 0x0600631E RID: 25374 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanReadUInt64
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600631F RID: 25375 RVA: 0x00154210 File Offset: 0x00152410
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				short num;
				if (reader.TryReadInt16(out num))
				{
					value = NumberValue.New((int)((ushort)num));
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06006320 RID: 25376 RVA: 0x00154238 File Offset: 0x00152438
			public override bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
			{
				short num;
				if (reader.TryReadInt16(out num))
				{
					value = (ulong)((ushort)num);
					return true;
				}
				value = 0UL;
				return false;
			}
		}

		// Token: 0x02000E75 RID: 3701
		private class UnsignedInteger32BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x17001CE7 RID: 7399
			// (get) Token: 0x06006322 RID: 25378 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanReadUInt64
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006323 RID: 25379 RVA: 0x0015425C File Offset: 0x0015245C
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				int num;
				if (reader.TryReadInt32(out num))
				{
					value = NumberValue.New((long)((ulong)num));
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06006324 RID: 25380 RVA: 0x00154284 File Offset: 0x00152484
			public override bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
			{
				int num;
				if (reader.TryReadInt32(out num))
				{
					value = (ulong)num;
					return true;
				}
				value = 0UL;
				return false;
			}
		}

		// Token: 0x02000E76 RID: 3702
		private class UnsignedInteger64BinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x17001CE8 RID: 7400
			// (get) Token: 0x06006326 RID: 25382 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanReadUInt64
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006327 RID: 25383 RVA: 0x001542A8 File Offset: 0x001524A8
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				long num;
				if (reader.TryReadInt64(out num))
				{
					value = NumberValue.New((ulong)num);
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06006328 RID: 25384 RVA: 0x001542D0 File Offset: 0x001524D0
			public override bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
			{
				long num;
				if (reader.TryReadInt64(out num))
				{
					value = (ulong)num;
					return true;
				}
				value = 0UL;
				return false;
			}
		}

		// Token: 0x02000E77 RID: 3703
		private class SevenBitEncodedUnsignedIntegerBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x17001CE9 RID: 7401
			// (get) Token: 0x0600632A RID: 25386 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanReadUInt64
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600632B RID: 25387 RVA: 0x001542F4 File Offset: 0x001524F4
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				ulong num;
				if (reader.TryRead7BitEncodedUnsignedInteger(out num))
				{
					value = NumberValue.New(num);
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x0600632C RID: 25388 RVA: 0x00154319 File Offset: 0x00152519
			public override bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
			{
				if (reader.TryRead7BitEncodedUnsignedInteger(out value))
				{
					return true;
				}
				value = 0UL;
				return false;
			}
		}

		// Token: 0x02000E78 RID: 3704
		private class SevenBitEncodedSignedIntegerBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x0600632E RID: 25390 RVA: 0x0015432C File Offset: 0x0015252C
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				ulong num;
				if (reader.TryRead7BitEncodedUnsignedInteger(out num))
				{
					long num2;
					if ((num & 1UL) != 0UL)
					{
						num2 = (long)(-(num >> 1) - 1UL);
					}
					else
					{
						num2 = (long)(num >> 1);
					}
					value = NumberValue.New(num2);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E79 RID: 3705
		private class SingleBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x06006330 RID: 25392 RVA: 0x00154368 File Offset: 0x00152568
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				float num;
				if (reader.TryReadSingle(out num))
				{
					value = NumberValue.New((double)num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E7A RID: 3706
		private class DoubleBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x06006332 RID: 25394 RVA: 0x00154390 File Offset: 0x00152590
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				double num;
				if (reader.TryReadDouble(out num))
				{
					value = NumberValue.New(num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E7B RID: 3707
		private class DecimalBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x06006334 RID: 25396 RVA: 0x001543B8 File Offset: 0x001525B8
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				decimal num;
				if (reader.TryReadDecimal(out num))
				{
					value = NumberValue.New(num);
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000E7C RID: 3708
		private class ByteBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x17001CEA RID: 7402
			// (get) Token: 0x06006336 RID: 25398 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanReadUInt64
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006337 RID: 25399 RVA: 0x001543E0 File Offset: 0x001525E0
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				int num = reader.ReadOptionalByte();
				if (num >= 0)
				{
					value = NumberValue.New((int)((byte)num));
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06006338 RID: 25400 RVA: 0x00154408 File Offset: 0x00152608
			public override bool TryReadUInt64(BinaryFormatReader reader, out ulong value)
			{
				int num = reader.ReadOptionalByte();
				if (num >= 0)
				{
					value = (ulong)((byte)num);
					return true;
				}
				value = 0UL;
				return false;
			}
		}

		// Token: 0x02000E7D RID: 3709
		private class NullBinaryFormatValue : BinaryFormatValue
		{
			// Token: 0x0600633A RID: 25402 RVA: 0x0015442C File Offset: 0x0015262C
			public override bool TryReadValue(BinaryFormatReader reader, out Value value)
			{
				value = Value.Null;
				return true;
			}
		}

		// Token: 0x02000E7E RID: 3710
		private class BinaryFormatBinaryFunctionValue : NativeFunctionValue1<FunctionValue, Value>
		{
			// Token: 0x0600633C RID: 25404 RVA: 0x00154436 File Offset: 0x00152636
			public BinaryFormatBinaryFunctionValue()
				: base(TypeValue.Function, 0, "length", TypeValue.Any)
			{
			}

			// Token: 0x0600633D RID: 25405 RVA: 0x00154450 File Offset: 0x00152650
			public override FunctionValue TypedInvoke(Value length)
			{
				if (length.IsNull)
				{
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryUntilEndBinaryFormatValue();
				}
				IBinaryFormat binaryFormat;
				if (BinaryFormatValue.TryGetLengthBinaryFormat(length, out binaryFormat))
				{
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryDynamicBinaryFormatValue(binaryFormat);
				}
				int asInteger = length.AsInteger32;
				if (asInteger < 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_LengthMayNotBeNegative, length, null);
				}
				return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryFixedBinaryFormatValue(asInteger);
			}

			// Token: 0x02000E7F RID: 3711
			private class BinaryUntilEndBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x17001CEB RID: 7403
				// (get) Token: 0x0600633E RID: 25406 RVA: 0x000023C4 File Offset: 0x000005C4
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Stream;
					}
				}

				// Token: 0x17001CEC RID: 7404
				// (get) Token: 0x0600633F RID: 25407 RVA: 0x00154498 File Offset: 0x00152698
				public override IExpression Expression
				{
					get
					{
						return new InvocationExpressionSyntaxNode1(new LibraryIdentifierExpression("BinaryFormat.Binary"), ConstantExpressionSyntaxNode.Null);
					}
				}

				// Token: 0x06006340 RID: 25408 RVA: 0x001544B0 File Offset: 0x001526B0
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					List<byte> list = new List<byte>();
					for (;;)
					{
						int num = reader.ReadOptionalByte();
						if (num < 0)
						{
							break;
						}
						list.Add((byte)num);
					}
					value = new BytesBufferedBinaryValue(list.ToArray());
					return true;
				}

				// Token: 0x06006341 RID: 25409 RVA: 0x001544E6 File Offset: 0x001526E6
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryUntilEndBinaryFormatValue.BinaryUntilEndFormatBinaryStream(reader);
				}

				// Token: 0x02000E80 RID: 3712
				private class BinaryUntilEndFormatBinaryStream : ReadableStream
				{
					// Token: 0x06006343 RID: 25411 RVA: 0x001544EE File Offset: 0x001526EE
					public BinaryUntilEndFormatBinaryStream(BinaryFormatReader reader)
					{
						this.reader = reader;
					}

					// Token: 0x06006344 RID: 25412 RVA: 0x00154500 File Offset: 0x00152700
					public override int Read(byte[] buffer, int offset, int count)
					{
						int num = 0;
						while (count > 0)
						{
							int num2 = this.reader.ReadOptionalBytes(buffer, offset, count);
							if (num2 == 0)
							{
								break;
							}
							offset += num2;
							count -= num2;
							num += num2;
						}
						return num;
					}

					// Token: 0x06006345 RID: 25413 RVA: 0x00154536 File Offset: 0x00152736
					public override int ReadByte()
					{
						return this.reader.ReadOptionalByte();
					}

					// Token: 0x06006346 RID: 25414 RVA: 0x00154543 File Offset: 0x00152743
					protected override void Dispose(bool disposing)
					{
						this.reader.Dispose();
					}

					// Token: 0x04003628 RID: 13864
					private BinaryFormatReader reader;
				}
			}

			// Token: 0x02000E81 RID: 3713
			private class BinaryDynamicBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x06006347 RID: 25415 RVA: 0x00154550 File Offset: 0x00152750
				public BinaryDynamicBinaryFormatValue(IBinaryFormat binaryFormat)
				{
					this.binaryFormat = binaryFormat;
				}

				// Token: 0x17001CED RID: 7405
				// (get) Token: 0x06006348 RID: 25416 RVA: 0x000023C4 File Offset: 0x000005C4
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Stream;
					}
				}

				// Token: 0x17001CEE RID: 7406
				// (get) Token: 0x06006349 RID: 25417 RVA: 0x0015455F File Offset: 0x0015275F
				public override IExpression Expression
				{
					get
					{
						return new InvocationExpressionSyntaxNode1(new LibraryIdentifierExpression("BinaryFormat.Binary"), new ConstantExpressionSyntaxNode((Value)this.binaryFormat));
					}
				}

				// Token: 0x0600634A RID: 25418 RVA: 0x00154580 File Offset: 0x00152780
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					ulong num;
					if (!this.binaryFormat.TryReadUInt64(reader, out num))
					{
						value = null;
						return false;
					}
					if (num > 2147483647UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					byte[] array = reader.ReadBytes((int)num);
					value = new BytesBufferedBinaryValue(array);
					return true;
				}

				// Token: 0x0600634B RID: 25419 RVA: 0x001545D0 File Offset: 0x001527D0
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					ulong num = this.binaryFormat.ReadUInt64(reader);
					if (num > 9223372036854775807UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryLengthFormatBinaryStream(reader, (long)num);
				}

				// Token: 0x04003629 RID: 13865
				private readonly IBinaryFormat binaryFormat;
			}

			// Token: 0x02000E82 RID: 3714
			private class BinaryFixedBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x0600634C RID: 25420 RVA: 0x0015460F File Offset: 0x0015280F
				public BinaryFixedBinaryFormatValue(int length)
				{
					this.length = length;
				}

				// Token: 0x17001CEF RID: 7407
				// (get) Token: 0x0600634D RID: 25421 RVA: 0x000023C4 File Offset: 0x000005C4
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Stream;
					}
				}

				// Token: 0x17001CF0 RID: 7408
				// (get) Token: 0x0600634E RID: 25422 RVA: 0x0015461E File Offset: 0x0015281E
				public override IExpression Expression
				{
					get
					{
						return new InvocationExpressionSyntaxNode1(new LibraryIdentifierExpression("BinaryFormat.Binary"), new ConstantExpressionSyntaxNode(NumberValue.New(this.length)));
					}
				}

				// Token: 0x0600634F RID: 25423 RVA: 0x00154640 File Offset: 0x00152840
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					byte[] array;
					if (reader.TryReadBytes(this.length, out array))
					{
						value = new BytesBufferedBinaryValue(array);
						return true;
					}
					value = null;
					return false;
				}

				// Token: 0x06006350 RID: 25424 RVA: 0x0015466B File Offset: 0x0015286B
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					return new BinaryFormatModule.BinaryFormatBinaryFunctionValue.BinaryLengthFormatBinaryStream(reader, (long)this.length);
				}

				// Token: 0x0400362A RID: 13866
				private readonly int length;
			}

			// Token: 0x02000E83 RID: 3715
			private class BinaryLengthFormatBinaryStream : ReadableStream
			{
				// Token: 0x06006351 RID: 25425 RVA: 0x0015467A File Offset: 0x0015287A
				public BinaryLengthFormatBinaryStream(BinaryFormatReader reader, long length)
				{
					this.reader = reader;
					this.length = length;
				}

				// Token: 0x06006352 RID: 25426 RVA: 0x00154690 File Offset: 0x00152890
				public override int Read(byte[] buffer, int offset, int count)
				{
					int num = (int)Math.Min((long)count, this.length);
					if (num > 0)
					{
						this.reader.ReadBytes(buffer, offset, num);
						this.length -= (long)num;
					}
					return num;
				}

				// Token: 0x06006353 RID: 25427 RVA: 0x001546CE File Offset: 0x001528CE
				public override int ReadByte()
				{
					if (this.length == 0L)
					{
						return -1;
					}
					int num = (int)this.reader.ReadByte();
					this.length -= 1L;
					return num;
				}

				// Token: 0x06006354 RID: 25428 RVA: 0x001546F4 File Offset: 0x001528F4
				protected override void Dispose(bool disposing)
				{
					this.reader.Dispose();
				}

				// Token: 0x0400362B RID: 13867
				private BinaryFormatReader reader;

				// Token: 0x0400362C RID: 13868
				private long length;
			}
		}

		// Token: 0x02000E84 RID: 3716
		private class BinaryFormatTextFunctionValue : NativeFunctionValue2<FunctionValue, Value, Value>
		{
			// Token: 0x06006355 RID: 25429 RVA: 0x00154701 File Offset: 0x00152901
			public BinaryFormatTextFunctionValue()
				: base(TypeValue.Function, 1, "length", TypeValue.Any, "encoding", TextEncoding.Type.Nullable)
			{
			}

			// Token: 0x06006356 RID: 25430 RVA: 0x00154728 File Offset: 0x00152928
			public override FunctionValue TypedInvoke(Value length, Value encoding)
			{
				TextDecoder textDecoder = TextEncoding.GetTextDecoder(encoding);
				IBinaryFormat binaryFormat;
				if (BinaryFormatValue.TryGetLengthBinaryFormat(length, out binaryFormat))
				{
					return new BinaryFormatModule.BinaryFormatTextFunctionValue.DynamicTextBinaryFormatValue(binaryFormat, textDecoder);
				}
				int asInteger = length.AsNumber.AsInteger32;
				if (asInteger < 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_LengthMayNotBeNegative, length, null);
				}
				return new BinaryFormatModule.BinaryFormatTextFunctionValue.FixedTextBinaryFormatValue(asInteger, textDecoder);
			}

			// Token: 0x02000E85 RID: 3717
			private abstract class TextBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x06006357 RID: 25431 RVA: 0x00154770 File Offset: 0x00152970
				protected TextBinaryFormatValue(TextDecoder textDecoder)
				{
					this.textDecoder = textDecoder;
				}

				// Token: 0x06006358 RID: 25432 RVA: 0x00154780 File Offset: 0x00152980
				protected Value DecodeText(byte[] bytes, int index, int count)
				{
					string text;
					try
					{
						text = this.textDecoder.Decode(bytes, index, count);
					}
					catch (ArgumentException)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.Text_InvalidUnicodeCodePoints, Value.Null, null);
					}
					return TextValue.New(text);
				}

				// Token: 0x0400362D RID: 13869
				private readonly TextDecoder textDecoder;
			}

			// Token: 0x02000E86 RID: 3718
			private class DynamicTextBinaryFormatValue : BinaryFormatModule.BinaryFormatTextFunctionValue.TextBinaryFormatValue
			{
				// Token: 0x06006359 RID: 25433 RVA: 0x001547C8 File Offset: 0x001529C8
				public DynamicTextBinaryFormatValue(IBinaryFormat binaryFormat, TextDecoder textDecoder)
					: base(textDecoder)
				{
					this.binaryFormat = binaryFormat;
				}

				// Token: 0x0600635A RID: 25434 RVA: 0x001547D8 File Offset: 0x001529D8
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					ulong num;
					if (!this.binaryFormat.TryReadUInt64(reader, out num))
					{
						value = null;
						return false;
					}
					if (num > 2147483647UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					int num2 = (int)num;
					BinaryFormatBufferPool bufferPool = reader.BufferPool;
					byte[] array = bufferPool.TakeBuffer(num2);
					reader.ReadBytes(array, 0, num2);
					value = base.DecodeText(array, 0, num2);
					bufferPool.ReturnBuffer(array);
					return true;
				}

				// Token: 0x0400362E RID: 13870
				private readonly IBinaryFormat binaryFormat;
			}

			// Token: 0x02000E87 RID: 3719
			private class FixedTextBinaryFormatValue : BinaryFormatModule.BinaryFormatTextFunctionValue.TextBinaryFormatValue
			{
				// Token: 0x0600635B RID: 25435 RVA: 0x00154841 File Offset: 0x00152A41
				public FixedTextBinaryFormatValue(int length, TextDecoder textDecoder)
					: base(textDecoder)
				{
					this.length = length;
				}

				// Token: 0x0600635C RID: 25436 RVA: 0x00154854 File Offset: 0x00152A54
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					BinaryFormatBufferPool bufferPool = reader.BufferPool;
					byte[] array = bufferPool.TakeBuffer(this.length);
					if (!reader.TryReadBytes(array, 0, this.length))
					{
						bufferPool.ReturnBuffer(array);
						value = null;
						return false;
					}
					value = base.DecodeText(array, 0, this.length);
					bufferPool.ReturnBuffer(array);
					return true;
				}

				// Token: 0x0400362F RID: 13871
				private readonly int length;
			}
		}

		// Token: 0x02000E88 RID: 3720
		private class BinaryFormatGroupFunctionValue : NativeFunctionValue4<FunctionValue, FunctionValue, ListValue, Value, Value>
		{
			// Token: 0x0600635D RID: 25437 RVA: 0x001548AC File Offset: 0x00152AAC
			public BinaryFormatGroupFunctionValue()
				: base(TypeValue.Function, 2, "binaryFormat", TypeValue.Function, "group", TypeValue.List, "extra", TypeValue.Function.Nullable, "lastKey", TypeValue.Any)
			{
			}

			// Token: 0x0600635E RID: 25438 RVA: 0x001548F4 File Offset: 0x00152AF4
			public override FunctionValue TypedInvoke(FunctionValue binaryFormatFunction, ListValue group, Value extra, Value lastKey)
			{
				IBinaryFormat binaryFormat = BinaryFormatValue.GetBinaryFormat(binaryFormatFunction);
				FunctionValue functionValue;
				if (extra.IsNull)
				{
					functionValue = null;
				}
				else
				{
					functionValue = extra.AsFunction;
				}
				return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue(binaryFormat, group, functionValue, lastKey);
			}

			// Token: 0x02000E89 RID: 3721
			private class GroupBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x0600635F RID: 25439 RVA: 0x00154923 File Offset: 0x00152B23
				public GroupBinaryFormatValue(IBinaryFormat binaryFormat, ListValue group, FunctionValue extra, Value lastKey)
				{
					this.group = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group(binaryFormat, group, extra, lastKey);
				}

				// Token: 0x17001CF1 RID: 7409
				// (get) Token: 0x06006360 RID: 25440 RVA: 0x00002105 File Offset: 0x00000305
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Value;
					}
				}

				// Token: 0x17001CF2 RID: 7410
				// (get) Token: 0x06006361 RID: 25441 RVA: 0x0015493B File Offset: 0x00152B3B
				public override IExpression Expression
				{
					get
					{
						return this.group.GetExpression();
					}
				}

				// Token: 0x06006362 RID: 25442 RVA: 0x00154948 File Offset: 0x00152B48
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader groupReader = this.group.TakeReader();
					bool flag = groupReader.TryReadGroup(reader, out value);
					this.group.ReturnReader(groupReader);
					return flag;
				}

				// Token: 0x04003630 RID: 13872
				private readonly BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group group;

				// Token: 0x02000E8A RID: 3722
				private abstract class GroupReader
				{
					// Token: 0x06006363 RID: 25443
					public abstract bool TryReadGroup(BinaryFormatReader reader, out Value group);
				}

				// Token: 0x02000E8B RID: 3723
				private class Group
				{
					// Token: 0x06006365 RID: 25445 RVA: 0x00154978 File Offset: 0x00152B78
					public Group(IBinaryFormat binaryFormat, ListValue group, FunctionValue extra, Value lastKey)
					{
						this.binaryFormat = binaryFormat;
						this.group = group;
						this.extra = extra;
						this.lastKey = lastKey;
						this.itemsByValue = new Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem>();
						this.itemsByIndex = new List<BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem>();
						foreach (IValueReference valueReference in group)
						{
							this.AddItem(valueReference.Value.AsList);
						}
						this.itemMapping = BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.Create(this.itemsByValue, lastKey);
					}

					// Token: 0x06006366 RID: 25446 RVA: 0x00154A18 File Offset: 0x00152C18
					public IExpression GetExpression()
					{
						return new InvocationExpressionSyntaxNodeN(new LibraryIdentifierExpression("BinaryFormat.Group"), new IExpression[]
						{
							new ConstantExpressionSyntaxNode((Value)this.binaryFormat),
							new ConstantExpressionSyntaxNode(this.group),
							new ConstantExpressionSyntaxNode(this.extra),
							new ConstantExpressionSyntaxNode(this.lastKey)
						});
					}

					// Token: 0x06006367 RID: 25447 RVA: 0x00154A78 File Offset: 0x00152C78
					private void AddItem(Value item)
					{
						ListValue asList = Library.List.Buffer.Invoke(item).AsList;
						if (asList.Count < 3 || asList.Count > 5)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_GroupItemInvalid, asList, null);
						}
						Value value = asList[0];
						Value value2 = asList[1];
						Value value3 = asList[2];
						Value value4;
						if (asList.Count > 3)
						{
							value4 = asList[3];
						}
						else
						{
							value4 = Value.Null;
						}
						Value value5;
						if (asList.Count > 4)
						{
							value5 = asList[4];
						}
						else
						{
							value5 = Value.Null;
						}
						FunctionValue functionValue;
						if (value5.IsNull)
						{
							functionValue = null;
						}
						else
						{
							functionValue = value5.AsFunction;
						}
						IBinaryFormat binaryFormat = BinaryFormatValue.GetBinaryFormat(value2);
						if (this.itemsByValue.ContainsKey(value))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_GroupItemInvalid, asList, null);
						}
						BinaryOccurrence value6 = BinaryOccurrenceValue.Type.GetValue(value3.AsNumber);
						this.AddItem(value, value6, binaryFormat, value4, functionValue);
					}

					// Token: 0x06006368 RID: 25448 RVA: 0x00154B5C File Offset: 0x00152D5C
					private void AddItem(Value key, BinaryOccurrence occurrence, IBinaryFormat binaryFormat, Value defaultValue, FunctionValue itemTransformFunction)
					{
						int num;
						if (occurrence == BinaryOccurrence.Repeating)
						{
							num = this.repeatingCount;
							this.repeatingCount++;
						}
						else
						{
							num = -1;
						}
						int count = this.itemsByIndex.Count;
						BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem groupItem = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem(key, occurrence, binaryFormat, count, defaultValue, itemTransformFunction, num);
						this.itemsByIndex.Add(groupItem);
						this.itemsByValue.Add(key, groupItem);
					}

					// Token: 0x06006369 RID: 25449 RVA: 0x00154BBA File Offset: 0x00152DBA
					public BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader TakeReader()
					{
						if (this.cachedGroupReader != null)
						{
							BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader groupReader = this.cachedGroupReader;
							this.cachedGroupReader = null;
							return groupReader;
						}
						if (this.binaryFormat.CanReadUInt64)
						{
							return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.ULongGroupReader(this);
						}
						return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.ValueGroupReader(this);
					}

					// Token: 0x0600636A RID: 25450 RVA: 0x00154BEC File Offset: 0x00152DEC
					public void ReturnReader(BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader groupReader)
					{
						this.cachedGroupReader = groupReader;
					}

					// Token: 0x04003631 RID: 13873
					private readonly Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> itemsByValue;

					// Token: 0x04003632 RID: 13874
					private readonly BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping itemMapping;

					// Token: 0x04003633 RID: 13875
					private readonly List<BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> itemsByIndex;

					// Token: 0x04003634 RID: 13876
					private readonly IBinaryFormat binaryFormat;

					// Token: 0x04003635 RID: 13877
					private readonly ListValue group;

					// Token: 0x04003636 RID: 13878
					private readonly FunctionValue extra;

					// Token: 0x04003637 RID: 13879
					private readonly Value lastKey;

					// Token: 0x04003638 RID: 13880
					private int repeatingCount;

					// Token: 0x04003639 RID: 13881
					private BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader cachedGroupReader;

					// Token: 0x02000E8C RID: 3724
					private class GroupItem
					{
						// Token: 0x0600636B RID: 25451 RVA: 0x00154BF5 File Offset: 0x00152DF5
						public GroupItem(Value key, BinaryOccurrence occurrence, IBinaryFormat binaryFormat, int index, Value defaultValue, FunctionValue transformFunction, int repeatingIndex)
						{
							this.key = key;
							this.occurrence = occurrence;
							this.binaryFormat = binaryFormat;
							this.index = index;
							this.defaultValue = defaultValue;
							this.transformFunction = transformFunction;
							this.repeatingIndex = repeatingIndex;
						}

						// Token: 0x17001CF3 RID: 7411
						// (get) Token: 0x0600636C RID: 25452 RVA: 0x00154C32 File Offset: 0x00152E32
						public Value Key
						{
							get
							{
								return this.key;
							}
						}

						// Token: 0x17001CF4 RID: 7412
						// (get) Token: 0x0600636D RID: 25453 RVA: 0x00154C3A File Offset: 0x00152E3A
						public BinaryOccurrence Occurrence
						{
							get
							{
								return this.occurrence;
							}
						}

						// Token: 0x17001CF5 RID: 7413
						// (get) Token: 0x0600636E RID: 25454 RVA: 0x00154C42 File Offset: 0x00152E42
						public IBinaryFormat BinaryFormat
						{
							get
							{
								return this.binaryFormat;
							}
						}

						// Token: 0x17001CF6 RID: 7414
						// (get) Token: 0x0600636F RID: 25455 RVA: 0x00154C4A File Offset: 0x00152E4A
						public Value DefaultValue
						{
							get
							{
								return this.defaultValue;
							}
						}

						// Token: 0x17001CF7 RID: 7415
						// (get) Token: 0x06006370 RID: 25456 RVA: 0x00154C52 File Offset: 0x00152E52
						public FunctionValue TransformFunction
						{
							get
							{
								return this.transformFunction;
							}
						}

						// Token: 0x17001CF8 RID: 7416
						// (get) Token: 0x06006371 RID: 25457 RVA: 0x00154C5A File Offset: 0x00152E5A
						public int Index
						{
							get
							{
								return this.index;
							}
						}

						// Token: 0x17001CF9 RID: 7417
						// (get) Token: 0x06006372 RID: 25458 RVA: 0x00154C62 File Offset: 0x00152E62
						public int RepeatingIndex
						{
							get
							{
								return this.repeatingIndex;
							}
						}

						// Token: 0x0400363A RID: 13882
						private readonly Value key;

						// Token: 0x0400363B RID: 13883
						private readonly BinaryOccurrence occurrence;

						// Token: 0x0400363C RID: 13884
						private readonly IBinaryFormat binaryFormat;

						// Token: 0x0400363D RID: 13885
						private readonly int index;

						// Token: 0x0400363E RID: 13886
						private readonly Value defaultValue;

						// Token: 0x0400363F RID: 13887
						private readonly FunctionValue transformFunction;

						// Token: 0x04003640 RID: 13888
						private readonly int repeatingIndex;
					}

					// Token: 0x02000E8D RID: 3725
					private abstract class GroupItemMapping
					{
						// Token: 0x06006373 RID: 25459
						public abstract bool TryGetItem(ulong key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item);

						// Token: 0x06006374 RID: 25460
						public abstract bool TryGetItem(Value key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item);

						// Token: 0x06006375 RID: 25461
						public abstract bool IsLastItem(ulong key);

						// Token: 0x06006376 RID: 25462
						public abstract bool IsLastItem(Value key);

						// Token: 0x06006377 RID: 25463 RVA: 0x00154C6C File Offset: 0x00152E6C
						public static BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping Create(Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, Value lastKey)
						{
							BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping groupItemMapping;
							if (BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryCreateULongMapping(items, lastKey, out groupItemMapping))
							{
								return groupItemMapping;
							}
							return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.ValueGroupItemMapping(items, lastKey);
						}

						// Token: 0x06006378 RID: 25464 RVA: 0x00154C90 File Offset: 0x00152E90
						private static bool TryCreateULongMapping(Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, Value lastKey, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping itemMapping)
						{
							ulong? num;
							if (!BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryGetULong(lastKey, out num))
							{
								itemMapping = null;
								return false;
							}
							Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> dictionary = new Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem>();
							foreach (KeyValuePair<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> keyValuePair in items)
							{
								ulong num2;
								if (!BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryGetULong(keyValuePair.Key, out num2))
								{
									itemMapping = null;
									return false;
								}
								dictionary.Add(num2, keyValuePair.Value);
							}
							itemMapping = BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.CreateULongMapping(dictionary, num);
							return true;
						}

						// Token: 0x06006379 RID: 25465 RVA: 0x00154D20 File Offset: 0x00152F20
						private static BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping CreateULongMapping(Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, ulong? lastKey)
						{
							if (items.Count > 0 && items.Count <= 1024)
							{
								ulong num = ulong.MaxValue;
								ulong num2 = 0UL;
								foreach (ulong num3 in items.Keys)
								{
									num = Math.Min(num3, num);
									num2 = Math.Max(num3, num2);
								}
								ulong num4 = num2 - num;
								if (num4 < 1024UL)
								{
									return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.RangeGroupItemMapping(items, num, num4 + 1UL, lastKey);
								}
							}
							return new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.DictionaryGroupItemMapping(items, lastKey);
						}

						// Token: 0x0600637A RID: 25466 RVA: 0x00154DB8 File Offset: 0x00152FB8
						private static bool TryGetULong(Value value, out ulong? ulongValue)
						{
							if (value.IsNull)
							{
								ulongValue = null;
								return true;
							}
							ulong num;
							if (!BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryGetULong(value, out num))
							{
								ulongValue = null;
								return false;
							}
							ulongValue = new ulong?(num);
							return true;
						}

						// Token: 0x0600637B RID: 25467 RVA: 0x00154DF8 File Offset: 0x00152FF8
						private static bool TryGetULong(Value value, out ulong ulongValue)
						{
							if (!value.IsNumber)
							{
								ulongValue = 0UL;
								return false;
							}
							long num;
							if (!value.AsNumber.TryGetInt64(out num))
							{
								ulongValue = 0UL;
								return false;
							}
							if (num < 0L)
							{
								ulongValue = 0UL;
								return false;
							}
							ulongValue = (ulong)num;
							return true;
						}

						// Token: 0x02000E8E RID: 3726
						private abstract class BaseULongGroupItemMapping : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping
						{
							// Token: 0x0600637D RID: 25469 RVA: 0x00154E38 File Offset: 0x00153038
							public override bool TryGetItem(Value key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
							{
								ulong num;
								if (BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryGetULong(key, out num))
								{
									return this.TryGetItem(num, out item);
								}
								item = null;
								return false;
							}

							// Token: 0x0600637E RID: 25470 RVA: 0x00154E5C File Offset: 0x0015305C
							public override bool IsLastItem(Value key)
							{
								ulong num;
								return BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.TryGetULong(key, out num) && this.IsLastItem(num);
							}
						}

						// Token: 0x02000E8F RID: 3727
						private class ValueGroupItemMapping : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping
						{
							// Token: 0x06006380 RID: 25472 RVA: 0x00154E84 File Offset: 0x00153084
							public ValueGroupItemMapping(Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, Value lastKey)
							{
								this.items = items;
								this.lastKey = lastKey;
							}

							// Token: 0x06006381 RID: 25473 RVA: 0x00154E9C File Offset: 0x0015309C
							public override bool TryGetItem(ulong key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
							{
								Value value = NumberValue.New(key);
								return this.TryGetItem(value, out item);
							}

							// Token: 0x06006382 RID: 25474 RVA: 0x00154EB8 File Offset: 0x001530B8
							public override bool IsLastItem(ulong key)
							{
								Value value = NumberValue.New(key);
								return this.lastKey.Equals(value);
							}

							// Token: 0x06006383 RID: 25475 RVA: 0x00154ED8 File Offset: 0x001530D8
							public override bool TryGetItem(Value key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
							{
								return this.items.TryGetValue(key, out item);
							}

							// Token: 0x06006384 RID: 25476 RVA: 0x00154EE7 File Offset: 0x001530E7
							public override bool IsLastItem(Value key)
							{
								return key.Equals(this.lastKey);
							}

							// Token: 0x04003641 RID: 13889
							private readonly Dictionary<Value, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items;

							// Token: 0x04003642 RID: 13890
							private readonly Value lastKey;
						}

						// Token: 0x02000E90 RID: 3728
						private class DictionaryGroupItemMapping : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.BaseULongGroupItemMapping
						{
							// Token: 0x06006385 RID: 25477 RVA: 0x00154EF5 File Offset: 0x001530F5
							public DictionaryGroupItemMapping(Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, ulong? lastKey)
							{
								this.items = items;
								this.lastKey = lastKey;
							}

							// Token: 0x06006386 RID: 25478 RVA: 0x00154F0B File Offset: 0x0015310B
							public override bool TryGetItem(ulong key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
							{
								return this.items.TryGetValue(key, out item);
							}

							// Token: 0x06006387 RID: 25479 RVA: 0x00154F1A File Offset: 0x0015311A
							public override bool IsLastItem(ulong key)
							{
								return this.lastKey != null && key == this.lastKey.Value;
							}

							// Token: 0x04003643 RID: 13891
							private readonly Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items;

							// Token: 0x04003644 RID: 13892
							private readonly ulong? lastKey;
						}

						// Token: 0x02000E91 RID: 3729
						private class RangeGroupItemMapping : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItemMapping.BaseULongGroupItemMapping
						{
							// Token: 0x06006388 RID: 25480 RVA: 0x00154F3C File Offset: 0x0015313C
							public RangeGroupItemMapping(Dictionary<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> items, ulong start, ulong count, ulong? lastKey)
							{
								this.start = start;
								this.items = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem[count];
								checked
								{
									foreach (KeyValuePair<ulong, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> keyValuePair in items)
									{
										this.items[(int)((IntPtr)(unchecked(keyValuePair.Key - start)))] = keyValuePair.Value;
									}
									this.lastKey = lastKey;
								}
							}

							// Token: 0x06006389 RID: 25481 RVA: 0x00154FC0 File Offset: 0x001531C0
							public override bool TryGetItem(ulong key, out BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem value)
							{
								ulong num = key - this.start;
								if (num < (ulong)((long)this.items.Length))
								{
									value = this.items[(int)(checked((IntPtr)num))];
									return value != null;
								}
								value = null;
								return false;
							}

							// Token: 0x0600638A RID: 25482 RVA: 0x00154FF7 File Offset: 0x001531F7
							public override bool IsLastItem(ulong key)
							{
								return this.lastKey != null && key == this.lastKey.Value;
							}

							// Token: 0x04003645 RID: 13893
							private readonly BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem[] items;

							// Token: 0x04003646 RID: 13894
							private readonly ulong? lastKey;

							// Token: 0x04003647 RID: 13895
							private readonly ulong start;
						}
					}

					// Token: 0x02000E92 RID: 3730
					private class RepeatingValueBuilder
					{
						// Token: 0x0600638B RID: 25483 RVA: 0x00155018 File Offset: 0x00153218
						public RepeatingValueBuilder(int repeatingCount)
						{
							this.repeatingValues = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder.ValueListBuilder[repeatingCount];
							for (int i = 0; i < repeatingCount; i++)
							{
								this.repeatingValues[i] = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder.ValueListBuilder();
							}
						}

						// Token: 0x0600638C RID: 25484 RVA: 0x00155050 File Offset: 0x00153250
						public void AddValue(int index, Value value)
						{
							this.repeatingValues[index].Add(value);
						}

						// Token: 0x0600638D RID: 25485 RVA: 0x00155060 File Offset: 0x00153260
						public bool TryGetValues(int index, out Value[] values)
						{
							BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder.ValueListBuilder valueListBuilder = this.repeatingValues[index];
							if (valueListBuilder.Count > 0)
							{
								values = valueListBuilder.GetValues();
								return true;
							}
							values = null;
							return false;
						}

						// Token: 0x04003648 RID: 13896
						private BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder.ValueListBuilder[] repeatingValues;

						// Token: 0x02000E93 RID: 3731
						private class ValueListBuilder
						{
							// Token: 0x0600638E RID: 25486 RVA: 0x0015508D File Offset: 0x0015328D
							public ValueListBuilder()
							{
								this.values = new Value[4];
							}

							// Token: 0x0600638F RID: 25487 RVA: 0x001550A4 File Offset: 0x001532A4
							public void Add(Value value)
							{
								if (this.count == this.values.Length)
								{
									Value[] array = new Value[checked(this.values.Length * 2)];
									Array.Copy(this.values, array, this.count);
									this.values = array;
								}
								this.values[this.count] = value;
								this.count++;
							}

							// Token: 0x17001CFA RID: 7418
							// (get) Token: 0x06006390 RID: 25488 RVA: 0x00155106 File Offset: 0x00153306
							public int Count
							{
								get
								{
									return this.count;
								}
							}

							// Token: 0x06006391 RID: 25489 RVA: 0x00155110 File Offset: 0x00153310
							public Value[] GetValues()
							{
								int num = this.count;
								Value[] array = this.values;
								Value[] array2 = new Value[num];
								for (int i = 0; i < num; i++)
								{
									array2[i] = array[i];
									array[i] = null;
								}
								if (num > 64)
								{
									this.values = new Value[64];
								}
								this.count = 0;
								return array2;
							}

							// Token: 0x04003649 RID: 13897
							private Value[] values;

							// Token: 0x0400364A RID: 13898
							private int count;

							// Token: 0x0400364B RID: 13899
							private const int maxCount = 64;
						}
					}

					// Token: 0x02000E94 RID: 3732
					private abstract class BaseGroupReader : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.GroupReader
					{
						// Token: 0x06006392 RID: 25490 RVA: 0x00155162 File Offset: 0x00153362
						protected BaseGroupReader(BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group group)
						{
							this.group = group;
							this.repeatingValueBuilder = new BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder(group.repeatingCount);
						}

						// Token: 0x06006393 RID: 25491 RVA: 0x00155182 File Offset: 0x00153382
						protected void InitGroupValue()
						{
							this.values = new Value[this.group.itemsByIndex.Count];
						}

						// Token: 0x06006394 RID: 25492 RVA: 0x0015519F File Offset: 0x0015339F
						protected void SkipItem(BinaryFormatReader reader, Value key)
						{
							if (this.group.extra == null)
							{
								throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_GroupItemUnknown, key, null);
							}
							BinaryFormatValue.GetBinaryFormat(this.group.extra.Invoke(key)).ReadValue(reader);
						}

						// Token: 0x06006395 RID: 25493 RVA: 0x001551D8 File Offset: 0x001533D8
						protected Value GetGroupValue()
						{
							List<BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem> itemsByIndex = this.group.itemsByIndex;
							Value[] array = this.values;
							for (int i = 0; i < array.Length; i++)
							{
								Value value = array[i];
								BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem groupItem = itemsByIndex[i];
								if (value == null)
								{
									switch (groupItem.Occurrence)
									{
									case BinaryOccurrence.Optional:
										value = groupItem.DefaultValue;
										break;
									case BinaryOccurrence.Required:
										throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_GroupItemRequired, groupItem.Key, null);
									case BinaryOccurrence.Repeating:
										value = this.GetRepeatingValue(groupItem);
										break;
									default:
										throw new InvalidOperationException();
									}
									array[i] = value;
								}
								else
								{
									FunctionValue transformFunction = groupItem.TransformFunction;
									if (transformFunction != null)
									{
										value = transformFunction.Invoke(value);
										array[i] = value;
									}
								}
							}
							Value value2 = ListValue.New(array);
							this.values = null;
							return value2;
						}

						// Token: 0x06006396 RID: 25494 RVA: 0x00155298 File Offset: 0x00153498
						protected Value GetRepeatingValue(BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
						{
							Value[] array;
							if (this.repeatingValueBuilder.TryGetValues(item.RepeatingIndex, out array))
							{
								Value value = ListValue.New(array);
								FunctionValue transformFunction = item.TransformFunction;
								if (transformFunction != null)
								{
									value = transformFunction.Invoke(value);
								}
								return value;
							}
							Value defaultValue = item.DefaultValue;
							if (defaultValue.IsNull)
							{
								return ListValue.Empty;
							}
							return defaultValue;
						}

						// Token: 0x06006397 RID: 25495 RVA: 0x001552EC File Offset: 0x001534EC
						protected void ReadItemValue(BinaryFormatReader reader, BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem item)
						{
							if (item.Occurrence == BinaryOccurrence.Repeating)
							{
								Value value = item.BinaryFormat.ReadValue(reader);
								this.repeatingValueBuilder.AddValue(item.RepeatingIndex, value);
								return;
							}
							if (this.values[item.Index] != null)
							{
								this.SkipItem(reader, item.Key);
								return;
							}
							Value value2 = item.BinaryFormat.ReadValue(reader);
							this.values[item.Index] = value2;
						}

						// Token: 0x0400364C RID: 13900
						protected readonly BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group group;

						// Token: 0x0400364D RID: 13901
						private readonly BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.RepeatingValueBuilder repeatingValueBuilder;

						// Token: 0x0400364E RID: 13902
						private Value[] values;
					}

					// Token: 0x02000E95 RID: 3733
					private class ValueGroupReader : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.BaseGroupReader
					{
						// Token: 0x06006398 RID: 25496 RVA: 0x0015535A File Offset: 0x0015355A
						public ValueGroupReader(BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group group)
							: base(group)
						{
						}

						// Token: 0x06006399 RID: 25497 RVA: 0x00155364 File Offset: 0x00153564
						public override bool TryReadGroup(BinaryFormatReader reader, out Value value)
						{
							base.InitGroupValue();
							Value value2;
							while (this.group.binaryFormat.TryReadValue(reader, out value2))
							{
								if (!this.TryReadItem(reader, value2))
								{
									if (this.group.itemMapping.IsLastItem(value2))
									{
										break;
									}
									base.SkipItem(reader, value2);
								}
							}
							value = base.GetGroupValue();
							return true;
						}

						// Token: 0x0600639A RID: 25498 RVA: 0x001553BC File Offset: 0x001535BC
						private bool TryReadItem(BinaryFormatReader reader, Value key)
						{
							BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem groupItem;
							if (!this.group.itemMapping.TryGetItem(key, out groupItem))
							{
								return false;
							}
							base.ReadItemValue(reader, groupItem);
							return true;
						}
					}

					// Token: 0x02000E96 RID: 3734
					private class ULongGroupReader : BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.BaseGroupReader
					{
						// Token: 0x0600639B RID: 25499 RVA: 0x0015535A File Offset: 0x0015355A
						public ULongGroupReader(BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group group)
							: base(group)
						{
						}

						// Token: 0x0600639C RID: 25500 RVA: 0x001553EC File Offset: 0x001535EC
						public override bool TryReadGroup(BinaryFormatReader reader, out Value value)
						{
							base.InitGroupValue();
							ulong num;
							while (this.group.binaryFormat.TryReadUInt64(reader, out num))
							{
								if (!this.TryReadItem(reader, num))
								{
									if (this.IsLastItem(num))
									{
										break;
									}
									base.SkipItem(reader, NumberValue.New(num));
								}
							}
							value = base.GetGroupValue();
							return true;
						}

						// Token: 0x0600639D RID: 25501 RVA: 0x0015543F File Offset: 0x0015363F
						private bool IsLastItem(ulong key)
						{
							return this.group.itemMapping.IsLastItem(key);
						}

						// Token: 0x0600639E RID: 25502 RVA: 0x00155454 File Offset: 0x00153654
						private bool TryReadItem(BinaryFormatReader reader, ulong key)
						{
							BinaryFormatModule.BinaryFormatGroupFunctionValue.GroupBinaryFormatValue.Group.GroupItem groupItem;
							if (!this.group.itemMapping.TryGetItem(key, out groupItem))
							{
								return false;
							}
							base.ReadItemValue(reader, groupItem);
							return true;
						}
					}
				}
			}
		}

		// Token: 0x02000E97 RID: 3735
		private class BinaryFormatByteOrderFunctionValue : NativeFunctionValue2<FunctionValue, FunctionValue, NumberValue>
		{
			// Token: 0x0600639F RID: 25503 RVA: 0x00155481 File Offset: 0x00153681
			public BinaryFormatByteOrderFunctionValue()
				: base(TypeValue.Function, 2, "binaryFormat", TypeValue.Function, "byteOrder", ByteOrderValue.Type)
			{
			}

			// Token: 0x060063A0 RID: 25504 RVA: 0x001554A3 File Offset: 0x001536A3
			public override FunctionValue TypedInvoke(FunctionValue binaryFormatFunction, NumberValue byteOrder)
			{
				return new BinaryFormatModule.BinaryFormatByteOrderFunctionValue.ByteOrderBinaryFormatValue(BinaryFormatValue.GetBinaryFormat(binaryFormatFunction), ByteOrderValue.Type.GetValue(byteOrder));
			}

			// Token: 0x02000E98 RID: 3736
			private class ByteOrderBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063A1 RID: 25505 RVA: 0x001554BB File Offset: 0x001536BB
				public ByteOrderBinaryFormatValue(IBinaryFormat binaryFormat, ByteOrder byteOrder)
				{
					this.binaryFormat = binaryFormat;
					this.byteOrder = byteOrder;
				}

				// Token: 0x17001CFB RID: 7419
				// (get) Token: 0x060063A2 RID: 25506 RVA: 0x001554D1 File Offset: 0x001536D1
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return this.binaryFormat.BinaryFormatType;
					}
				}

				// Token: 0x060063A3 RID: 25507 RVA: 0x001554DE File Offset: 0x001536DE
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					reader.PushByteOrder(this.byteOrder);
					return this.binaryFormat.ReadItems(reader);
				}

				// Token: 0x060063A4 RID: 25508 RVA: 0x001554F8 File Offset: 0x001536F8
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					reader.PushByteOrder(this.byteOrder);
					return this.binaryFormat.ReadStream(reader);
				}

				// Token: 0x060063A5 RID: 25509 RVA: 0x00155512 File Offset: 0x00153712
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					reader.PushByteOrder(this.byteOrder);
					bool flag = this.binaryFormat.TryReadValue(reader, out value);
					reader.Pop();
					return flag;
				}

				// Token: 0x0400364F RID: 13903
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x04003650 RID: 13904
				private readonly ByteOrder byteOrder;
			}
		}

		// Token: 0x02000E99 RID: 3737
		private class BinaryFormatTransformFunctionValue : NativeFunctionValue2<FunctionValue, FunctionValue, FunctionValue>
		{
			// Token: 0x060063A6 RID: 25510 RVA: 0x00155533 File Offset: 0x00153733
			public BinaryFormatTransformFunctionValue()
				: base(TypeValue.Function, 2, "binaryFormat", TypeValue.Function, "function", TypeValue.Function)
			{
			}

			// Token: 0x060063A7 RID: 25511 RVA: 0x00155555 File Offset: 0x00153755
			public override FunctionValue TypedInvoke(FunctionValue binaryFormatFunction, FunctionValue transformFunction)
			{
				return new BinaryFormatModule.BinaryFormatTransformFunctionValue.TransformBinaryFormatValue(BinaryFormatValue.GetBinaryFormat(binaryFormatFunction), transformFunction);
			}

			// Token: 0x02000E9A RID: 3738
			private class TransformBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063A8 RID: 25512 RVA: 0x00155563 File Offset: 0x00153763
				public TransformBinaryFormatValue(IBinaryFormat binaryFormat, FunctionValue function)
				{
					this.binaryFormat = binaryFormat;
					this.function = function;
				}

				// Token: 0x060063A9 RID: 25513 RVA: 0x00155579 File Offset: 0x00153779
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					if (this.binaryFormat.TryReadValue(reader, out value))
					{
						value = this.function.Invoke(value);
						return true;
					}
					value = null;
					return false;
				}

				// Token: 0x04003651 RID: 13905
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x04003652 RID: 13906
				private readonly FunctionValue function;
			}
		}

		// Token: 0x02000E9B RID: 3739
		private class BinaryFormatLengthFunctionValue : NativeFunctionValue2<FunctionValue, FunctionValue, Value>
		{
			// Token: 0x060063AA RID: 25514 RVA: 0x0015559F File Offset: 0x0015379F
			public BinaryFormatLengthFunctionValue()
				: base(TypeValue.Function, 2, "binaryFormat", TypeValue.Function, "length", TypeValue.Any)
			{
			}

			// Token: 0x060063AB RID: 25515 RVA: 0x001555C4 File Offset: 0x001537C4
			public override FunctionValue TypedInvoke(FunctionValue binaryFormatFunction, Value lengthValue)
			{
				IBinaryFormat binaryFormat = BinaryFormatValue.GetBinaryFormat(binaryFormatFunction);
				IBinaryFormat binaryFormat2;
				if (BinaryFormatValue.TryGetLengthBinaryFormat(lengthValue, out binaryFormat2))
				{
					return new BinaryFormatModule.BinaryFormatLengthFunctionValue.DynamicLengthBinaryFormatValue(binaryFormat, binaryFormat2);
				}
				long asInteger = lengthValue.AsNumber.AsInteger64;
				if (asInteger < 0L)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_LengthMayNotBeNegative, lengthValue, null);
				}
				return new BinaryFormatModule.BinaryFormatLengthFunctionValue.FixedLengthBinaryFormatValue(binaryFormat, asInteger);
			}

			// Token: 0x02000E9C RID: 3740
			private class DynamicLengthBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063AC RID: 25516 RVA: 0x0015560F File Offset: 0x0015380F
				public DynamicLengthBinaryFormatValue(IBinaryFormat binaryFormat, IBinaryFormat lengthBinaryFormat)
				{
					this.binaryFormat = binaryFormat;
					this.lengthBinaryFormat = lengthBinaryFormat;
				}

				// Token: 0x17001CFC RID: 7420
				// (get) Token: 0x060063AD RID: 25517 RVA: 0x00155625 File Offset: 0x00153825
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return this.binaryFormat.BinaryFormatType;
					}
				}

				// Token: 0x060063AE RID: 25518 RVA: 0x00155634 File Offset: 0x00153834
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					ulong num = this.lengthBinaryFormat.ReadUInt64(reader);
					if (num > 9223372036854775807UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					reader.PushLimit((long)num);
					return this.binaryFormat.ReadItems(reader);
				}

				// Token: 0x060063AF RID: 25519 RVA: 0x00155680 File Offset: 0x00153880
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					ulong num = this.lengthBinaryFormat.ReadUInt64(reader);
					if (num > 9223372036854775807UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					reader.PushLimit((long)num);
					return this.binaryFormat.ReadStream(reader);
				}

				// Token: 0x060063B0 RID: 25520 RVA: 0x001556CC File Offset: 0x001538CC
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					ulong num;
					if (!this.lengthBinaryFormat.TryReadUInt64(reader, out num))
					{
						value = null;
						return false;
					}
					if (num > 9223372036854775807UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_LengthReadTooLarge, NumberValue.New(num), null);
					}
					reader.PushLimit((long)num);
					value = this.binaryFormat.ReadValue(reader);
					reader.Pop();
					return true;
				}

				// Token: 0x04003653 RID: 13907
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x04003654 RID: 13908
				private readonly IBinaryFormat lengthBinaryFormat;
			}

			// Token: 0x02000E9D RID: 3741
			private class FixedLengthBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063B1 RID: 25521 RVA: 0x00155728 File Offset: 0x00153928
				public FixedLengthBinaryFormatValue(IBinaryFormat binaryFormat, long length)
				{
					this.binaryFormat = binaryFormat;
					this.length = length;
				}

				// Token: 0x17001CFD RID: 7421
				// (get) Token: 0x060063B2 RID: 25522 RVA: 0x0015573E File Offset: 0x0015393E
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return this.binaryFormat.BinaryFormatType;
					}
				}

				// Token: 0x060063B3 RID: 25523 RVA: 0x0015574B File Offset: 0x0015394B
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					reader.PushLimit(this.length);
					return this.binaryFormat.ReadItems(reader);
				}

				// Token: 0x060063B4 RID: 25524 RVA: 0x00155765 File Offset: 0x00153965
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					reader.PushLimit(this.length);
					return this.binaryFormat.ReadStream(reader);
				}

				// Token: 0x060063B5 RID: 25525 RVA: 0x0015577F File Offset: 0x0015397F
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					reader.PushLimit(this.length);
					bool flag = this.binaryFormat.TryReadValue(reader, out value);
					reader.Pop();
					return flag;
				}

				// Token: 0x04003655 RID: 13909
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x04003656 RID: 13910
				private readonly long length;
			}
		}

		// Token: 0x02000E9E RID: 3742
		private class BinaryFormatChoiceFunctionValue : NativeFunctionValue4<FunctionValue, FunctionValue, FunctionValue, Value, Value>
		{
			// Token: 0x060063B6 RID: 25526 RVA: 0x001557A0 File Offset: 0x001539A0
			public BinaryFormatChoiceFunctionValue()
				: base(TypeValue.Function, 2, "binaryFormat", TypeValue.Function, "chooseFunction", TypeValue.Function, "type", TypeValue._Type.Nullable, "combineFunction", TypeValue.Function.Nullable)
			{
			}

			// Token: 0x060063B7 RID: 25527 RVA: 0x001557EC File Offset: 0x001539EC
			public override FunctionValue TypedInvoke(FunctionValue binaryFormatFunction, FunctionValue chooseFunction, Value type, Value combine)
			{
				IBinaryFormat binaryFormat = BinaryFormatValue.GetBinaryFormat(binaryFormatFunction);
				TypeValue typeValue = (type.IsNull ? TypeValue.Any : type.AsType);
				FunctionValue functionValue = (combine.IsNull ? null : combine.AsFunction);
				return new BinaryFormatModule.BinaryFormatChoiceFunctionValue.ChoiceBinaryFormatValue(binaryFormat, chooseFunction, typeValue, functionValue);
			}

			// Token: 0x02000E9F RID: 3743
			private class ChoiceBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063B8 RID: 25528 RVA: 0x00155834 File Offset: 0x00153A34
				public ChoiceBinaryFormatValue(IBinaryFormat binaryFormat, FunctionValue chooseFunction, TypeValue typeValue, FunctionValue combineFunction)
				{
					this.binaryFormat = binaryFormat;
					this.chooseFunction = chooseFunction;
					this.typeValue = typeValue;
					this.combineFunction = combineFunction;
					if (typeValue.TypeKind == ValueKind.List)
					{
						this.binaryFormatType = BinaryFormatType.Items;
					}
					else if (typeValue.TypeKind == ValueKind.Binary)
					{
						this.binaryFormatType = BinaryFormatType.Stream;
					}
					else
					{
						if (typeValue.TypeKind != ValueKind.Any)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_InvalidType, typeValue, null);
						}
						this.binaryFormatType = BinaryFormatType.Value;
					}
					if (this.combineFunction != null && this.binaryFormatType != BinaryFormatType.Value)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_InvalidTypeWhenCombining, typeValue, null);
					}
				}

				// Token: 0x17001CFE RID: 7422
				// (get) Token: 0x060063B9 RID: 25529 RVA: 0x001558C6 File Offset: 0x00153AC6
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return this.binaryFormatType;
					}
				}

				// Token: 0x17001CFF RID: 7423
				// (get) Token: 0x060063BA RID: 25530 RVA: 0x001558D0 File Offset: 0x00153AD0
				public override IExpression Expression
				{
					get
					{
						return new InvocationExpressionSyntaxNodeN(new LibraryIdentifierExpression("BinaryFormat.Choice"), new IExpression[]
						{
							new ConstantExpressionSyntaxNode((Value)this.binaryFormat),
							new ConstantExpressionSyntaxNode(this.chooseFunction),
							new ConstantExpressionSyntaxNode(this.typeValue),
							new ConstantExpressionSyntaxNode(this.combineFunction)
						});
					}
				}

				// Token: 0x060063BB RID: 25531 RVA: 0x00155930 File Offset: 0x00153B30
				private IBinaryFormat GetChoice(BinaryFormatReader reader)
				{
					Value value = this.binaryFormat.ReadValue(reader);
					return this.GetChoice(value);
				}

				// Token: 0x060063BC RID: 25532 RVA: 0x00155951 File Offset: 0x00153B51
				private IBinaryFormat GetChoice(Value choiceValue)
				{
					return BinaryFormatValue.GetBinaryFormat(this.chooseFunction.Invoke(choiceValue));
				}

				// Token: 0x060063BD RID: 25533 RVA: 0x00155964 File Offset: 0x00153B64
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					return this.GetChoice(reader).ReadItems(reader);
				}

				// Token: 0x060063BE RID: 25534 RVA: 0x00155973 File Offset: 0x00153B73
				public override Stream ReadStream(BinaryFormatReader reader)
				{
					return this.GetChoice(reader).ReadStream(reader);
				}

				// Token: 0x060063BF RID: 25535 RVA: 0x00155984 File Offset: 0x00153B84
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					Value value2;
					if (!this.binaryFormat.TryReadValue(reader, out value2))
					{
						value = null;
						return false;
					}
					Value value3 = this.GetChoice(value2).ReadValue(reader);
					if (this.combineFunction != null)
					{
						value = this.combineFunction.Invoke(value2, value3);
					}
					else
					{
						value = value3;
					}
					return true;
				}

				// Token: 0x04003657 RID: 13911
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x04003658 RID: 13912
				private readonly FunctionValue chooseFunction;

				// Token: 0x04003659 RID: 13913
				private readonly TypeValue typeValue;

				// Token: 0x0400365A RID: 13914
				private readonly BinaryFormatType binaryFormatType;

				// Token: 0x0400365B RID: 13915
				private readonly FunctionValue combineFunction;
			}
		}

		// Token: 0x02000EA0 RID: 3744
		private class BinaryFormatListFunctionValue : NativeFunctionValue2<FunctionValue, FunctionValue, Value>
		{
			// Token: 0x060063C0 RID: 25536 RVA: 0x001559D1 File Offset: 0x00153BD1
			public BinaryFormatListFunctionValue()
				: base(TypeValue.Function, 1, "binaryFormat", TypeValue.Function, "countOrCondition", TypeValue.Any)
			{
			}

			// Token: 0x060063C1 RID: 25537 RVA: 0x001559F4 File Offset: 0x00153BF4
			public override FunctionValue TypedInvoke(FunctionValue function, Value countOrCondition)
			{
				IBinaryFormat binaryFormat = BinaryFormatValue.GetBinaryFormat(function);
				if (countOrCondition.IsNull)
				{
					return new BinaryFormatModule.BinaryFormatListFunctionValue.RepeatUntilEndBinaryFormatValue(binaryFormat);
				}
				if (countOrCondition.IsNumber)
				{
					long asInteger = countOrCondition.AsNumber.AsInteger64;
					if (asInteger < 0L)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_CountMayNotBeNegative, countOrCondition, null);
					}
					return new BinaryFormatModule.BinaryFormatListFunctionValue.RepeatForCountBinaryFormatValue(binaryFormat, asInteger);
				}
				else
				{
					if (!countOrCondition.IsFunction)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_CountOrConditionInvalid, countOrCondition, null);
					}
					IBinaryFormat binaryFormat2;
					if (!BinaryFormatValue.TryGetBinaryFormat(countOrCondition, out binaryFormat2))
					{
						return new BinaryFormatModule.BinaryFormatListFunctionValue.RepeatUntilConditionBinaryFormatValue(binaryFormat, countOrCondition.AsFunction);
					}
					if (!binaryFormat2.CanReadUInt64)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.BinaryFormat_NotSupportedForCount, countOrCondition, null);
					}
					return new BinaryFormatModule.BinaryFormatListFunctionValue.DynamicRepeatBinaryFormatValue(binaryFormat, binaryFormat2);
				}
			}

			// Token: 0x02000EA1 RID: 3745
			private class RepeatUntilConditionBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063C2 RID: 25538 RVA: 0x00155A8D File Offset: 0x00153C8D
				public RepeatUntilConditionBinaryFormatValue(IBinaryFormat binaryFormat, FunctionValue condition)
				{
					this.binaryFormat = binaryFormat;
					this.condition = condition;
				}

				// Token: 0x17001D00 RID: 7424
				// (get) Token: 0x060063C3 RID: 25539 RVA: 0x00002139 File Offset: 0x00000339
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Items;
					}
				}

				// Token: 0x060063C4 RID: 25540 RVA: 0x00155AA4 File Offset: 0x00153CA4
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					Value value2;
					if (this.binaryFormat.TryReadValue(reader, out value2))
					{
						List<Value> list = new List<Value>();
						list.Add(value2);
						while (this.condition.Invoke(value2).AsBoolean)
						{
							value2 = this.binaryFormat.ReadValue(reader);
							list.Add(value2);
						}
						value = ListValue.New(list.ToArray());
						return true;
					}
					value = null;
					return false;
				}

				// Token: 0x060063C5 RID: 25541 RVA: 0x00155B0A File Offset: 0x00153D0A
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					for (;;)
					{
						Value item = this.binaryFormat.ReadValue(reader);
						yield return item;
						if (!this.condition.Invoke(item).AsBoolean)
						{
							break;
						}
						item = null;
					}
					yield break;
				}

				// Token: 0x0400365C RID: 13916
				private readonly IBinaryFormat binaryFormat;

				// Token: 0x0400365D RID: 13917
				private readonly FunctionValue condition;
			}

			// Token: 0x02000EA3 RID: 3747
			private class RepeatUntilEndBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063CC RID: 25548 RVA: 0x00155BB8 File Offset: 0x00153DB8
				public RepeatUntilEndBinaryFormatValue(IBinaryFormat binaryFormat)
				{
					this.binaryFormat = binaryFormat;
				}

				// Token: 0x060063CD RID: 25549 RVA: 0x00155BC8 File Offset: 0x00153DC8
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					List<Value> list = new List<Value>();
					Value value2;
					while (this.binaryFormat.TryReadValue(reader, out value2))
					{
						list.Add(value2);
					}
					value = ListValue.New(list.ToArray());
					return true;
				}

				// Token: 0x17001D03 RID: 7427
				// (get) Token: 0x060063CE RID: 25550 RVA: 0x00002139 File Offset: 0x00000339
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Items;
					}
				}

				// Token: 0x060063CF RID: 25551 RVA: 0x00155C02 File Offset: 0x00153E02
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					Value value;
					while (this.binaryFormat.TryReadValue(reader, out value))
					{
						yield return value;
					}
					yield break;
				}

				// Token: 0x04003663 RID: 13923
				private readonly IBinaryFormat binaryFormat;
			}

			// Token: 0x02000EA5 RID: 3749
			private class RepeatForCountBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063D6 RID: 25558 RVA: 0x00155C8A File Offset: 0x00153E8A
				public RepeatForCountBinaryFormatValue(IBinaryFormat binaryFormat, long count)
				{
					this.binaryFormat = binaryFormat;
					this.count = count;
				}

				// Token: 0x17001D06 RID: 7430
				// (get) Token: 0x060063D7 RID: 25559 RVA: 0x00002139 File Offset: 0x00000339
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Items;
					}
				}

				// Token: 0x060063D8 RID: 25560 RVA: 0x00155CA0 File Offset: 0x00153EA0
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					if (this.count > 2147483647L)
					{
						throw ValueException.ListCountTooLarge(this.count);
					}
					if (this.count == 0L)
					{
						value = ListValue.Empty;
						return true;
					}
					Value value2;
					if (!this.binaryFormat.TryReadValue(reader, out value2))
					{
						value = null;
						return false;
					}
					Value[] array = new Value[(int)this.count];
					array[0] = value2;
					int num = 1;
					while ((long)num < this.count)
					{
						array[num] = this.binaryFormat.ReadValue(reader);
						num++;
					}
					value = ListValue.New(array);
					return true;
				}

				// Token: 0x060063D9 RID: 25561 RVA: 0x00155D28 File Offset: 0x00153F28
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					long num;
					for (long i = 0L; i < this.count; i = num + 1L)
					{
						Value value = this.binaryFormat.ReadValue(reader);
						yield return value;
						num = i;
					}
					yield break;
				}

				// Token: 0x04003668 RID: 13928
				private readonly long count;

				// Token: 0x04003669 RID: 13929
				private readonly IBinaryFormat binaryFormat;
			}

			// Token: 0x02000EA7 RID: 3751
			private class DynamicRepeatBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063E0 RID: 25568 RVA: 0x00155DD6 File Offset: 0x00153FD6
				public DynamicRepeatBinaryFormatValue(IBinaryFormat binaryFormat, IBinaryFormat countBinaryFormat)
				{
					this.binaryFormat = binaryFormat;
					this.countBinaryFormat = countBinaryFormat;
				}

				// Token: 0x17001D09 RID: 7433
				// (get) Token: 0x060063E1 RID: 25569 RVA: 0x00002139 File Offset: 0x00000339
				public override BinaryFormatType BinaryFormatType
				{
					get
					{
						return BinaryFormatType.Items;
					}
				}

				// Token: 0x060063E2 RID: 25570 RVA: 0x00155DEC File Offset: 0x00153FEC
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					ulong num;
					if (!this.countBinaryFormat.TryReadUInt64(reader, out num))
					{
						value = null;
						return false;
					}
					if (num > 2147483647UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_CountReadTooLarge, NumberValue.New(num), null);
					}
					int num2 = (int)num;
					if (num2 == 0)
					{
						value = ListValue.Empty;
						return true;
					}
					Value[] array = new Value[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = this.binaryFormat.ReadValue(reader);
					}
					value = ListValue.New(array);
					return true;
				}

				// Token: 0x060063E3 RID: 25571 RVA: 0x00155E63 File Offset: 0x00154063
				public override IEnumerator<IValueReference> ReadItems(BinaryFormatReader reader)
				{
					ulong num = this.countBinaryFormat.ReadUInt64(reader);
					if (num > 9223372036854775807UL)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_CountReadTooLarge, NumberValue.New(num), null);
					}
					long count = (long)num;
					long num2;
					for (long i = 0L; i < count; i = num2 + 1L)
					{
						Value value = this.binaryFormat.ReadValue(reader);
						yield return value;
						num2 = i;
					}
					yield break;
				}

				// Token: 0x0400366F RID: 13935
				private readonly IBinaryFormat countBinaryFormat;

				// Token: 0x04003670 RID: 13936
				private readonly IBinaryFormat binaryFormat;
			}
		}

		// Token: 0x02000EA9 RID: 3753
		private class BinaryFormatRecordFunctionValue : NativeFunctionValue1<FunctionValue, RecordValue>
		{
			// Token: 0x060063EA RID: 25578 RVA: 0x00155F47 File Offset: 0x00154147
			public BinaryFormatRecordFunctionValue()
				: base(TypeValue.Function, 1, "record", TypeValue.Record)
			{
			}

			// Token: 0x060063EB RID: 25579 RVA: 0x00155F5F File Offset: 0x0015415F
			public override FunctionValue TypedInvoke(RecordValue record)
			{
				return new BinaryFormatModule.BinaryFormatRecordFunctionValue.RecordBinaryFormatValue(record);
			}

			// Token: 0x02000EAA RID: 3754
			private class RecordBinaryFormatValue : BinaryFormatValue
			{
				// Token: 0x060063EC RID: 25580 RVA: 0x00155F68 File Offset: 0x00154168
				public RecordBinaryFormatValue(RecordValue record)
				{
					this.record = record;
					this.fields = new BinaryFormatModule.BinaryFormatRecordFunctionValue.RecordBinaryFormatValue.FieldInfo[record.Keys.Length];
					for (int i = 0; i < record.Keys.Length; i++)
					{
						this.fields[i] = new BinaryFormatModule.BinaryFormatRecordFunctionValue.RecordBinaryFormatValue.FieldInfo(record[i]);
					}
				}

				// Token: 0x17001D0C RID: 7436
				// (get) Token: 0x060063ED RID: 25581 RVA: 0x00155FC2 File Offset: 0x001541C2
				public override IExpression Expression
				{
					get
					{
						return new InvocationExpressionSyntaxNode1(new LibraryIdentifierExpression("BinaryFormat.Record"), new ConstantExpressionSyntaxNode(this.record));
					}
				}

				// Token: 0x060063EE RID: 25582 RVA: 0x00155FE0 File Offset: 0x001541E0
				public override bool TryReadValue(BinaryFormatReader reader, out Value value)
				{
					bool flag = true;
					Value[] array = new Value[this.fields.Length];
					for (int i = 0; i < array.Length; i++)
					{
						BinaryFormatModule.BinaryFormatRecordFunctionValue.RecordBinaryFormatValue.FieldInfo fieldInfo = this.fields[i];
						IBinaryFormat binaryFormat = fieldInfo.BinaryFormat;
						Value value2;
						if (binaryFormat != null)
						{
							if (flag)
							{
								if (!binaryFormat.TryReadValue(reader, out value2))
								{
									value = null;
									return false;
								}
								flag = false;
							}
							else
							{
								value2 = binaryFormat.ReadValue(reader);
							}
						}
						else
						{
							value2 = fieldInfo.Value;
						}
						array[i] = value2;
					}
					value = RecordValue.New(this.record.Keys, array);
					return true;
				}

				// Token: 0x04003677 RID: 13943
				private readonly RecordValue record;

				// Token: 0x04003678 RID: 13944
				private readonly BinaryFormatModule.BinaryFormatRecordFunctionValue.RecordBinaryFormatValue.FieldInfo[] fields;

				// Token: 0x02000EAB RID: 3755
				private class FieldInfo
				{
					// Token: 0x060063EF RID: 25583 RVA: 0x00156065 File Offset: 0x00154265
					public FieldInfo(Value value)
					{
						if (!BinaryFormatValue.TryGetBinaryFormat(value, out this.binaryFormat))
						{
							this.value = value;
						}
					}

					// Token: 0x17001D0D RID: 7437
					// (get) Token: 0x060063F0 RID: 25584 RVA: 0x00156082 File Offset: 0x00154282
					public IBinaryFormat BinaryFormat
					{
						get
						{
							return this.binaryFormat;
						}
					}

					// Token: 0x17001D0E RID: 7438
					// (get) Token: 0x060063F1 RID: 25585 RVA: 0x0015608A File Offset: 0x0015428A
					public Value Value
					{
						get
						{
							return this.value;
						}
					}

					// Token: 0x04003679 RID: 13945
					private readonly IBinaryFormat binaryFormat;

					// Token: 0x0400367A RID: 13946
					private readonly Value value;
				}
			}
		}
	}
}
