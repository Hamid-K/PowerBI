using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D8 RID: 4824
	internal class DelegatingValue : Value
	{
		// Token: 0x06007F6E RID: 32622 RVA: 0x001B40C6 File Offset: 0x001B22C6
		public DelegatingValue(Value value)
		{
			this.value = value;
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x06007F6F RID: 32623 RVA: 0x001B40D5 File Offset: 0x001B22D5
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x06007F70 RID: 32624 RVA: 0x001B40DD File Offset: 0x001B22DD
		public override bool IsDefaultType
		{
			get
			{
				return this.value.IsDefaultType;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x06007F71 RID: 32625 RVA: 0x001B40EA File Offset: 0x001B22EA
		public override TypeValue Type
		{
			get
			{
				return this.value.Type;
			}
		}

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x06007F72 RID: 32626 RVA: 0x001B40F7 File Offset: 0x001B22F7
		public override ValueKind Kind
		{
			get
			{
				return this.value.Kind;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x001B4104 File Offset: 0x001B2304
		public override bool IsNull
		{
			get
			{
				return this.value.IsNull;
			}
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x06007F74 RID: 32628 RVA: 0x001B4111 File Offset: 0x001B2311
		public override bool IsLogical
		{
			get
			{
				return this.value.IsLogical;
			}
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x06007F75 RID: 32629 RVA: 0x001B411E File Offset: 0x001B231E
		public override bool IsNumber
		{
			get
			{
				return this.value.IsNumber;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x06007F76 RID: 32630 RVA: 0x001B412B File Offset: 0x001B232B
		public override bool IsDate
		{
			get
			{
				return this.value.IsDate;
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06007F77 RID: 32631 RVA: 0x001B4138 File Offset: 0x001B2338
		public override bool IsDateTime
		{
			get
			{
				return this.value.IsDateTime;
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x001B4145 File Offset: 0x001B2345
		public override bool IsDateTimeZone
		{
			get
			{
				return this.value.IsDateTimeZone;
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x06007F79 RID: 32633 RVA: 0x001B4152 File Offset: 0x001B2352
		public override bool IsTime
		{
			get
			{
				return this.value.IsTime;
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x06007F7A RID: 32634 RVA: 0x001B415F File Offset: 0x001B235F
		public override bool IsDuration
		{
			get
			{
				return this.value.IsDuration;
			}
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x06007F7B RID: 32635 RVA: 0x001B416C File Offset: 0x001B236C
		public override bool IsRecord
		{
			get
			{
				return this.value.IsRecord;
			}
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x06007F7C RID: 32636 RVA: 0x001B4179 File Offset: 0x001B2379
		public override bool IsFunction
		{
			get
			{
				return this.value.IsFunction;
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x06007F7D RID: 32637 RVA: 0x001B4186 File Offset: 0x001B2386
		public override bool IsList
		{
			get
			{
				return this.value.IsList;
			}
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x06007F7E RID: 32638 RVA: 0x001B4193 File Offset: 0x001B2393
		public override bool IsTable
		{
			get
			{
				return this.value.IsTable;
			}
		}

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x06007F7F RID: 32639 RVA: 0x001B41A0 File Offset: 0x001B23A0
		public override bool IsText
		{
			get
			{
				return this.value.IsText;
			}
		}

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x06007F80 RID: 32640 RVA: 0x001B41AD File Offset: 0x001B23AD
		public override bool IsBinary
		{
			get
			{
				return this.value.IsBinary;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x06007F81 RID: 32641 RVA: 0x001B41BA File Offset: 0x001B23BA
		public override bool IsType
		{
			get
			{
				return this.value.IsType;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x06007F82 RID: 32642 RVA: 0x001B41C7 File Offset: 0x001B23C7
		public override bool IsAction
		{
			get
			{
				return this.value.IsAction;
			}
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x001B41D4 File Offset: 0x001B23D4
		public override Value NewType(TypeValue type)
		{
			return this.value.NewType(type);
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x06007F84 RID: 32644 RVA: 0x001B41E2 File Offset: 0x001B23E2
		public override LogicalValue AsLogical
		{
			get
			{
				return this.value.AsLogical;
			}
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x06007F85 RID: 32645 RVA: 0x001B41EF File Offset: 0x001B23EF
		public override NumberValue AsNumber
		{
			get
			{
				return this.value.AsNumber;
			}
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x06007F86 RID: 32646 RVA: 0x001B41FC File Offset: 0x001B23FC
		public override DateValue AsDate
		{
			get
			{
				return this.value.AsDate;
			}
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x06007F87 RID: 32647 RVA: 0x001B4209 File Offset: 0x001B2409
		public override DateTimeValue AsDateTime
		{
			get
			{
				return this.value.AsDateTime;
			}
		}

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x06007F88 RID: 32648 RVA: 0x001B4216 File Offset: 0x001B2416
		public override DateTimeZoneValue AsDateTimeZone
		{
			get
			{
				return this.value.AsDateTimeZone;
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x06007F89 RID: 32649 RVA: 0x001B4223 File Offset: 0x001B2423
		public override TimeValue AsTime
		{
			get
			{
				return this.value.AsTime;
			}
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x06007F8A RID: 32650 RVA: 0x001B4230 File Offset: 0x001B2430
		public override DurationValue AsDuration
		{
			get
			{
				return this.value.AsDuration;
			}
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x06007F8B RID: 32651 RVA: 0x001B423D File Offset: 0x001B243D
		public override RecordValue AsRecord
		{
			get
			{
				return this.value.AsRecord;
			}
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x06007F8C RID: 32652 RVA: 0x001B424A File Offset: 0x001B244A
		public override FunctionValue AsFunction
		{
			get
			{
				return this.value.AsFunction;
			}
		}

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x06007F8D RID: 32653 RVA: 0x001B4257 File Offset: 0x001B2457
		public override ListValue AsList
		{
			get
			{
				return this.value.AsList;
			}
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x06007F8E RID: 32654 RVA: 0x001B4264 File Offset: 0x001B2464
		public override TableValue AsTable
		{
			get
			{
				return this.value.AsTable;
			}
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x06007F8F RID: 32655 RVA: 0x001B4271 File Offset: 0x001B2471
		public override TextValue AsText
		{
			get
			{
				return this.value.AsText;
			}
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x06007F90 RID: 32656 RVA: 0x001B427E File Offset: 0x001B247E
		public override BinaryValue AsBinary
		{
			get
			{
				return this.value.AsBinary;
			}
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x06007F91 RID: 32657 RVA: 0x001B428B File Offset: 0x001B248B
		public override TypeValue AsType
		{
			get
			{
				return this.value.AsType;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x06007F92 RID: 32658 RVA: 0x001B4298 File Offset: 0x001B2498
		public override ActionValue AsAction
		{
			get
			{
				return this.value.AsAction;
			}
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x06007F93 RID: 32659 RVA: 0x001B42A5 File Offset: 0x001B24A5
		public override RecordValue MetaValue
		{
			get
			{
				return this.value.MetaValue;
			}
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x001B42B2 File Offset: 0x001B24B2
		public override Value NewMeta(RecordValue metaValue)
		{
			return this.value.NewMeta(metaValue);
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x001B42C0 File Offset: 0x001B24C0
		public override bool TryGetMetaField(string identifier, out Value value)
		{
			return this.value.TryGetMetaField(identifier, out value);
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x06007F96 RID: 32662 RVA: 0x001B42CF File Offset: 0x001B24CF
		public override IExpression Expression
		{
			get
			{
				return this.value.Expression;
			}
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x001B42DC File Offset: 0x001B24DC
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.value.TryGetProcessor(out processor);
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x001B42EA File Offset: 0x001B24EA
		public override Value NullableGreaterThan(Value value)
		{
			return this.value.NullableGreaterThan(value);
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x001B42F8 File Offset: 0x001B24F8
		public override Value NullableLessThan(Value value)
		{
			return this.value.NullableLessThan(value);
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x001B4306 File Offset: 0x001B2506
		public override Value NullableGreaterThanOrEqual(Value value)
		{
			return this.value.NullableGreaterThanOrEqual(value);
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x001B4314 File Offset: 0x001B2514
		public override Value NullableLessThanOrEqual(Value value)
		{
			return this.value.NullableLessThanOrEqual(value);
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x001B4322 File Offset: 0x001B2522
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return this.value.Equals(value, comparer);
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x001B4331 File Offset: 0x001B2531
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x001B433E File Offset: 0x001B253E
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.value.GetHashCode(comparer);
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x001B434C File Offset: 0x001B254C
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			return this.value.CompareTo(value, comparer);
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x001B435B File Offset: 0x001B255B
		public override Value Identity()
		{
			return this.value.Identity();
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x001B4368 File Offset: 0x001B2568
		public override Value Negate()
		{
			return this.value.Negate();
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x001B4375 File Offset: 0x001B2575
		public override Value Not()
		{
			return this.value.Not();
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x001B4382 File Offset: 0x001B2582
		public override Value BitwiseNot()
		{
			return this.value.BitwiseNot();
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x001B438F File Offset: 0x001B258F
		public override Value Add(Value value)
		{
			return this.value.Add(value);
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x001B439D File Offset: 0x001B259D
		public override Value Subtract(Value value)
		{
			return this.value.Subtract(value);
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x001B43AB File Offset: 0x001B25AB
		public override Value Multiply(Value value)
		{
			return this.value.Multiply(value);
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x001B43B9 File Offset: 0x001B25B9
		public override Value Divide(Value value)
		{
			return this.value.Divide(value);
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x001B43C7 File Offset: 0x001B25C7
		public override Value Mod(Value value)
		{
			return this.value.Mod(value);
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x001B43D5 File Offset: 0x001B25D5
		public override Value IntegerDivide(Value value)
		{
			return this.value.IntegerDivide(value);
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x001B43E3 File Offset: 0x001B25E3
		public override Value Add(Value value, Precision precision)
		{
			return this.value.Add(value, precision);
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x001B43F2 File Offset: 0x001B25F2
		public override Value Subtract(Value value, Precision precision)
		{
			return this.value.Subtract(value, precision);
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x001B4401 File Offset: 0x001B2601
		public override Value Multiply(Value value, Precision precision)
		{
			return this.value.Multiply(value, precision);
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x001B4410 File Offset: 0x001B2610
		public override Value Divide(Value value, Precision precision)
		{
			return this.value.Divide(value, precision);
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x001B441F File Offset: 0x001B261F
		public override Value Mod(Value value, Precision precision)
		{
			return this.value.Mod(value, precision);
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x001B442E File Offset: 0x001B262E
		public override Value IntegerDivide(Value value, Precision precision)
		{
			return this.value.IntegerDivide(value, precision);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x001B443D File Offset: 0x001B263D
		public override Value AddR(Int32NumberValue value, Precision precision)
		{
			return this.value.AddR(value, precision);
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x001B444C File Offset: 0x001B264C
		public override Value AddR(DoubleNumberValue value, Precision precision)
		{
			return this.value.AddR(value, precision);
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x001B445B File Offset: 0x001B265B
		public override Value SubtractR(Int32NumberValue value, Precision precision)
		{
			return this.value.SubtractR(value, precision);
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x001B446A File Offset: 0x001B266A
		public override Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			return this.value.SubtractR(value, precision);
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x001B4479 File Offset: 0x001B2679
		public override Value MultiplyR(Int32NumberValue value, Precision precision)
		{
			return this.value.MultiplyR(value, precision);
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x001B4488 File Offset: 0x001B2688
		public override Value MultiplyR(DoubleNumberValue value, Precision precision)
		{
			return this.value.MultiplyR(value, precision);
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x001B4497 File Offset: 0x001B2697
		public override Value Concatenate(Value value)
		{
			return this.value.Concatenate(value);
		}

		// Token: 0x06007FB7 RID: 32695 RVA: 0x001B44A5 File Offset: 0x001B26A5
		public override Value BitwiseAnd(Value value)
		{
			return this.value.BitwiseAnd(value);
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x001B44B3 File Offset: 0x001B26B3
		public override Value BitwiseOr(Value value)
		{
			return this.value.BitwiseOr(value);
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x001B44C1 File Offset: 0x001B26C1
		public override Value BitwiseXor(Value value)
		{
			return this.value.BitwiseXor(value);
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x001B44CF File Offset: 0x001B26CF
		public override Value ShiftLeft(Value value)
		{
			return this.value.ShiftLeft(value);
		}

		// Token: 0x06007FBB RID: 32699 RVA: 0x001B44DD File Offset: 0x001B26DD
		public override Value ShiftRight(Value value)
		{
			return this.value.ShiftRight(value);
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x001B44EB File Offset: 0x001B26EB
		public override Accumulator GetAccumulator(Precision precision)
		{
			return this.value.GetAccumulator(precision);
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x001B44F9 File Offset: 0x001B26F9
		public override bool TryGetValue(Value index, out Value value)
		{
			return this.value.TryGetValue(index, out value);
		}

		// Token: 0x170022A9 RID: 8873
		public override Value this[int index]
		{
			get
			{
				return this.value[index];
			}
		}

		// Token: 0x170022AA RID: 8874
		public override Value this[Value key]
		{
			get
			{
				return this.value[key];
			}
		}

		// Token: 0x170022AB RID: 8875
		public override Value this[string key]
		{
			get
			{
				return this.value[key];
			}
		}

		// Token: 0x06007FC1 RID: 32705 RVA: 0x001B4532 File Offset: 0x001B2732
		public override ActionValue Replace(Value value)
		{
			return this.Value.Replace(value);
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x001B4540 File Offset: 0x001B2740
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.Value.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x001B4552 File Offset: 0x001B2752
		public override string ToSource()
		{
			return this.value.ToSource();
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x001B455F File Offset: 0x001B275F
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x06007FC5 RID: 32709 RVA: 0x001B456C File Offset: 0x001B276C
		public override object ToOleDb(Type type)
		{
			return this.value.ToOleDb(type);
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x001B457A File Offset: 0x001B277A
		protected override T Cast<T>(TypeValue type)
		{
			return (T)((object)this.value.As(type));
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x001B458D File Offset: 0x001B278D
		public override bool TryGetAs<T>(out T contract)
		{
			return this.value.TryGetAs<T>(out contract);
		}

		// Token: 0x040045AE RID: 17838
		private readonly Value value;
	}
}
