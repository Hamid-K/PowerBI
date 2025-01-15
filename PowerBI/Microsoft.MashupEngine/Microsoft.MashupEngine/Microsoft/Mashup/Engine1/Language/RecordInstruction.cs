using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001743 RID: 5955
	internal sealed class RecordInstruction : Instruction
	{
		// Token: 0x06009773 RID: 38771 RVA: 0x001F5ABF File Offset: 0x001F3CBF
		public RecordInstruction(Keys keys, Instruction[] fields, BitArray delayed)
		{
			this.keys = keys;
			this.fields = fields;
			this.delayed = delayed;
		}

		// Token: 0x06009774 RID: 38772 RVA: 0x001F5ADC File Offset: 0x001F3CDC
		public override Value Execute(Value frame)
		{
			return this.CreateRecord(Instruction.Execute(frame, this.fields));
		}

		// Token: 0x06009775 RID: 38773 RVA: 0x001F5AF0 File Offset: 0x001F3CF0
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.CreateRecord(Instruction.Execute(ref frame, this.fields));
		}

		// Token: 0x06009776 RID: 38774 RVA: 0x001F5B04 File Offset: 0x001F3D04
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.CreateRecord(Instruction.Execute(ref frame, this.fields));
		}

		// Token: 0x06009777 RID: 38775 RVA: 0x001F5B18 File Offset: 0x001F3D18
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.CreateRecord(Instruction.Execute(ref frame, this.fields));
		}

		// Token: 0x06009778 RID: 38776 RVA: 0x001F5B2C File Offset: 0x001F3D2C
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.CreateRecord(Instruction.Execute(ref frame, this.fields));
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x001F5B40 File Offset: 0x001F3D40
		private Value CreateRecord(Value[] fields)
		{
			if (this.delayed.Empty)
			{
				return RecordValue.New(this.keys, fields);
			}
			return new RecordInstruction.RuntimeRecordValue(this.keys, fields, this.delayed);
		}

		// Token: 0x0400504D RID: 20557
		private readonly Keys keys;

		// Token: 0x0400504E RID: 20558
		private readonly Instruction[] fields;

		// Token: 0x0400504F RID: 20559
		private readonly BitArray delayed;

		// Token: 0x02001744 RID: 5956
		private sealed class RuntimeRecordValue : RecordValue
		{
			// Token: 0x0600977A RID: 38778 RVA: 0x001F5B7C File Offset: 0x001F3D7C
			public RuntimeRecordValue(Keys keys, Value[] values, BitArray delayed)
			{
				this.keys = keys;
				this.values = values;
				this.delayed = delayed.Clone();
			}

			// Token: 0x17002757 RID: 10071
			// (get) Token: 0x0600977B RID: 38779 RVA: 0x001F5B9F File Offset: 0x001F3D9F
			public override Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x17002758 RID: 10072
			// (get) Token: 0x0600977C RID: 38780 RVA: 0x001F5BA7 File Offset: 0x001F3DA7
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = RecordTypeValue.New(this.keys);
					}
					return this.type;
				}
			}

			// Token: 0x0600977D RID: 38781 RVA: 0x001F5BC8 File Offset: 0x001F3DC8
			public override IValueReference GetReference(int index)
			{
				if (index >= this.values.Length)
				{
					throw ValueException.RecordIndexOutOfRange(index, this);
				}
				if (this.delayed[index])
				{
					return base.GetReference(index);
				}
				return this.values[index];
			}

			// Token: 0x17002759 RID: 10073
			public override Value this[int index]
			{
				get
				{
					if (index < this.values.Length)
					{
						if (this.delayed[index])
						{
							this.Force(index);
							this.delayed[index] = false;
						}
						return this.values[index];
					}
					return base[index];
				}
			}

			// Token: 0x0600977F RID: 38783 RVA: 0x001F5C3C File Offset: 0x001F3E3C
			private void Force(int index)
			{
				Value value = this.values[index];
				try
				{
					if (value == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ValueException_CyclicReference, null, null);
					}
					this.values[index] = null;
					this.values[index] = value.AsFunction.Invoke(this);
				}
				catch (ValueException ex)
				{
					if (!(this.values[index] is RecordInstruction.RuntimeRecordValue.ExceptionFunctionValue))
					{
						this.values[index] = new RecordInstruction.RuntimeRecordValue.ExceptionFunctionValue(ex);
					}
					throw;
				}
				catch
				{
					this.values[index] = value;
					throw;
				}
			}

			// Token: 0x04005050 RID: 20560
			private TypeValue type;

			// Token: 0x04005051 RID: 20561
			private readonly Keys keys;

			// Token: 0x04005052 RID: 20562
			private readonly Value[] values;

			// Token: 0x04005053 RID: 20563
			private BitArray delayed;

			// Token: 0x02001745 RID: 5957
			private sealed class ExceptionFunctionValue : NativeFunctionValue1
			{
				// Token: 0x06009780 RID: 38784 RVA: 0x001F5CCC File Offset: 0x001F3ECC
				public ExceptionFunctionValue(ValueException exception)
				{
					this.exception = exception;
				}

				// Token: 0x06009781 RID: 38785 RVA: 0x001F5CDB File Offset: 0x001F3EDB
				public override Value Invoke(Value record)
				{
					throw this.exception;
				}

				// Token: 0x04005054 RID: 20564
				private readonly ValueException exception;
			}
		}
	}
}
