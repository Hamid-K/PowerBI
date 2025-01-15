using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D9 RID: 4825
	public sealed class DisabledModule : DelegatingModule
	{
		// Token: 0x06007FC8 RID: 32712 RVA: 0x001B459B File Offset: 0x001B279B
		public DisabledModule(Module module)
			: base(module)
		{
			this.moduleName = module.Name ?? "<unknown>";
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x001B45BC File Offset: 0x001B27BC
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			RecordValue recordValue = base.Link(environment, hostEnvironment);
			List<Value> list = new List<Value>(3);
			list.Add(recordValue);
			Value value;
			if (recordValue.TryGetValue("Shared", out value) && value.IsRecord && value.AsRecord.Count > 0)
			{
				list.Add(RecordValue.New(DelegatingModule.SharedKeys, new Value[] { this.DisableFunctions(value.AsRecord) }));
			}
			if (recordValue.TryGetValue("Section", out value) && value.IsRecord && value.AsRecord.Count > 0)
			{
				list.Add(RecordValue.New(DelegatingModule.Section_Keys, new Value[] { this.DisableFunctions(value.AsRecord) }));
			}
			return RecordValue.Combine(ListValue.New(list.ToArray()));
		}

		// Token: 0x06007FCA RID: 32714 RVA: 0x001B4684 File Offset: 0x001B2884
		private RecordValue DisableFunctions(RecordValue members)
		{
			return RecordValue.New(members.Keys, (int i) => this.DisableFunction(members[i]));
		}

		// Token: 0x06007FCB RID: 32715 RVA: 0x001B46C1 File Offset: 0x001B28C1
		private Value DisableFunction(Value value)
		{
			if (value.IsFunction)
			{
				return new DisabledModule.DisabledFunctionValue(this.moduleName, value.AsFunction);
			}
			return value;
		}

		// Token: 0x040045AF RID: 17839
		private readonly string moduleName;

		// Token: 0x020012DA RID: 4826
		private sealed class DisabledFunctionValue : NativeFunctionValue
		{
			// Token: 0x06007FCC RID: 32716 RVA: 0x001B46DE File Offset: 0x001B28DE
			public DisabledFunctionValue(string moduleName, FunctionValue function)
			{
				this.moduleName = moduleName;
				this.function = function;
			}

			// Token: 0x170022AC RID: 8876
			// (get) Token: 0x06007FCD RID: 32717 RVA: 0x001B46F4 File Offset: 0x001B28F4
			public override TypeValue Type
			{
				get
				{
					if (this.functionType == null)
					{
						this.functionType = DisabledModule.DisabledFunctionValue.ApplyDocumentation(this.function.Type.AsFunctionType, this.moduleName);
					}
					return this.functionType;
				}
			}

			// Token: 0x06007FCE RID: 32718 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke()
			{
				return this.Fail();
			}

			// Token: 0x06007FCF RID: 32719 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(Value arg0)
			{
				return this.Fail();
			}

			// Token: 0x06007FD0 RID: 32720 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(Value arg0, Value arg1)
			{
				return this.Fail();
			}

			// Token: 0x06007FD1 RID: 32721 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(Value arg0, Value arg1, Value arg2)
			{
				return this.Fail();
			}

			// Token: 0x06007FD2 RID: 32722 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
			{
				return this.Fail();
			}

			// Token: 0x06007FD3 RID: 32723 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
			{
				return this.Fail();
			}

			// Token: 0x06007FD4 RID: 32724 RVA: 0x001B4725 File Offset: 0x001B2925
			public override Value Invoke(params Value[] args)
			{
				return this.Fail();
			}

			// Token: 0x06007FD5 RID: 32725 RVA: 0x001B472D File Offset: 0x001B292D
			private Value Fail()
			{
				throw ValueException.NewExpressionError<Message1>(Strings.ModuleDisabled(this.moduleName), null, null);
			}

			// Token: 0x170022AD RID: 8877
			// (get) Token: 0x06007FD6 RID: 32726 RVA: 0x001B4741 File Offset: 0x001B2941
			public override string PrimaryResourceKind
			{
				get
				{
					return this.function.PrimaryResourceKind;
				}
			}

			// Token: 0x06007FD7 RID: 32727 RVA: 0x001B474E File Offset: 0x001B294E
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return this.function.TryGetLocation(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x06007FD8 RID: 32728 RVA: 0x001B4760 File Offset: 0x001B2960
			private static FunctionTypeValue ApplyDocumentation(FunctionTypeValue functionType, string moduleName)
			{
				TextValue disabledText = TextValue.New(string.Format(CultureInfo.InvariantCulture, "<b>{0}</b><br>", Strings.ModuleDisabled(moduleName)));
				Keys keys = functionType.MetaValue.Keys;
				Keys keys2 = keys;
				int descIndex = keys.IndexOfKey("Documentation.LongDescription");
				if (descIndex < 0)
				{
					descIndex = keys.Length;
					keys2 = Keys.New(keys.Length + 1, delegate(int i)
					{
						if (i >= descIndex)
						{
							return "Documentation.LongDescription";
						}
						return keys[i];
					});
				}
				RecordValue recordValue = RecordValue.New(keys2, delegate(int i)
				{
					if (i != descIndex)
					{
						return functionType.MetaValue[i];
					}
					if (descIndex == keys.Length)
					{
						return disabledText;
					}
					return disabledText.Concatenate(functionType.MetaValue[i]);
				});
				return functionType.NewMeta(recordValue).AsType.AsFunctionType;
			}

			// Token: 0x040045B0 RID: 17840
			private readonly string moduleName;

			// Token: 0x040045B1 RID: 17841
			private readonly FunctionValue function;

			// Token: 0x040045B2 RID: 17842
			private FunctionTypeValue functionType;
		}
	}
}
