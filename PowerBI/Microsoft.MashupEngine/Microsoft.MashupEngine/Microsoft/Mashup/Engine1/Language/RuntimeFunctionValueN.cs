using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175A RID: 5978
	internal sealed class RuntimeFunctionValueN : MembersFunctionValue
	{
		// Token: 0x060097E3 RID: 38883 RVA: 0x001F672C File Offset: 0x001F492C
		public RuntimeFunctionValueN(Instruction instruction, Value[] members, FunctionTypeValue type, IExpression ast, Identifier[] memberNames)
			: base(instruction, type, ast, members, memberNames)
		{
		}

		// Token: 0x060097E4 RID: 38884 RVA: 0x00189545 File Offset: 0x00187745
		public override Value Invoke()
		{
			return this.Invoke(new Value[0]);
		}

		// Token: 0x060097E5 RID: 38885 RVA: 0x00189553 File Offset: 0x00187753
		public override Value Invoke(Value arg0)
		{
			return this.Invoke(new Value[] { arg0 });
		}

		// Token: 0x060097E6 RID: 38886 RVA: 0x00189565 File Offset: 0x00187765
		public override Value Invoke(Value arg0, Value arg1)
		{
			return this.Invoke(new Value[] { arg0, arg1 });
		}

		// Token: 0x060097E7 RID: 38887 RVA: 0x0018957B File Offset: 0x0018777B
		public override Value Invoke(Value arg0, Value arg1, Value arg2)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2 });
		}

		// Token: 0x060097E8 RID: 38888 RVA: 0x00189595 File Offset: 0x00187795
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x060097E9 RID: 38889 RVA: 0x001895B4 File Offset: 0x001877B4
		public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
		{
			return this.Invoke(new Value[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x060097EA RID: 38890 RVA: 0x001F673C File Offset: 0x001F493C
		public override Value Invoke(params Value[] args)
		{
			if (args.Length < base.Min || args.Length > base.Max)
			{
				throw ValueException.InvalidArguments(this, args);
			}
			if (args.Length != base.Max)
			{
				Value[] array = new Value[base.Max];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i];
				}
				for (int j = args.Length; j < array.Length; j++)
				{
					array[j] = Value.Null;
				}
				args = array;
			}
			MembersFrameN membersFrameN = new MembersFrameN(this, args);
			return this.instruction.Execute(ref membersFrameN);
		}
	}
}
