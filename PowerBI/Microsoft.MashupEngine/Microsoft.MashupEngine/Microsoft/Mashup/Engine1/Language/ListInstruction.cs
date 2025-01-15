using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001740 RID: 5952
	internal sealed class ListInstruction : Instruction
	{
		// Token: 0x06009765 RID: 38757 RVA: 0x001F58D7 File Offset: 0x001F3AD7
		public ListInstruction(Instruction[] elements, BitArray delayed)
		{
			this.elements = elements;
			this.delayed = delayed;
		}

		// Token: 0x06009766 RID: 38758 RVA: 0x001F58ED File Offset: 0x001F3AED
		public override Value Execute(Value frame)
		{
			return this.CreateList(Instruction.Execute(frame, this.elements));
		}

		// Token: 0x06009767 RID: 38759 RVA: 0x001F5901 File Offset: 0x001F3B01
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.CreateList(Instruction.Execute(ref frame, this.elements));
		}

		// Token: 0x06009768 RID: 38760 RVA: 0x001F5915 File Offset: 0x001F3B15
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.CreateList(Instruction.Execute(ref frame, this.elements));
		}

		// Token: 0x06009769 RID: 38761 RVA: 0x001F5929 File Offset: 0x001F3B29
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.CreateList(Instruction.Execute(ref frame, this.elements));
		}

		// Token: 0x0600976A RID: 38762 RVA: 0x001F593D File Offset: 0x001F3B3D
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.CreateList(Instruction.Execute(ref frame, this.elements));
		}

		// Token: 0x0600976B RID: 38763 RVA: 0x001F5954 File Offset: 0x001F3B54
		private Value CreateList(Value[] elements)
		{
			if (this.delayed.Empty)
			{
				return ListValue.New(elements);
			}
			return new ListInstruction.RuntimeListValue(elements, this.delayed);
		}

		// Token: 0x04005048 RID: 20552
		private readonly Instruction[] elements;

		// Token: 0x04005049 RID: 20553
		private readonly BitArray delayed;

		// Token: 0x02001741 RID: 5953
		private sealed class RuntimeListValue : ArrayListValue
		{
			// Token: 0x0600976C RID: 38764 RVA: 0x001F5984 File Offset: 0x001F3B84
			public RuntimeListValue(Value[] values, BitArray delayed)
			{
				this.values = values;
				this.delayed = delayed.Clone();
			}

			// Token: 0x17002755 RID: 10069
			// (get) Token: 0x0600976D RID: 38765 RVA: 0x001F59A0 File Offset: 0x001F3BA0
			public override int Count
			{
				get
				{
					return this.values.Length;
				}
			}

			// Token: 0x0600976E RID: 38766 RVA: 0x001F59AA File Offset: 0x001F3BAA
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

			// Token: 0x17002756 RID: 10070
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

			// Token: 0x06009770 RID: 38768 RVA: 0x001F5A1C File Offset: 0x001F3C1C
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
					this.values[index] = value.AsFunction.Invoke();
				}
				catch (ValueException ex)
				{
					if (!(this.values[index] is ListInstruction.RuntimeListValue.ExceptionFunctionValue))
					{
						this.values[index] = new ListInstruction.RuntimeListValue.ExceptionFunctionValue(ex);
					}
					throw;
				}
				catch
				{
					this.values[index] = value;
					throw;
				}
			}

			// Token: 0x0400504A RID: 20554
			private readonly Value[] values;

			// Token: 0x0400504B RID: 20555
			private BitArray delayed;

			// Token: 0x02001742 RID: 5954
			private sealed class ExceptionFunctionValue : NativeFunctionValue0
			{
				// Token: 0x06009771 RID: 38769 RVA: 0x001F5AA8 File Offset: 0x001F3CA8
				public ExceptionFunctionValue(ValueException exception)
				{
					this.exception = exception;
				}

				// Token: 0x06009772 RID: 38770 RVA: 0x001F5AB7 File Offset: 0x001F3CB7
				public override Value Invoke()
				{
					throw this.exception;
				}

				// Token: 0x0400504C RID: 20556
				private readonly ValueException exception;
			}
		}
	}
}
