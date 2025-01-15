using System;
using System.Runtime.CompilerServices;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001256 RID: 4694
	public abstract class ActionValue : PrimitiveValue, IActionValue, IValue
	{
		// Token: 0x06007BAE RID: 31662 RVA: 0x001AA89A File Offset: 0x001A8A9A
		public static ActionValue New(Func<Value> action)
		{
			return new ActionValue.ActionActionValue(action);
		}

		// Token: 0x06007BAF RID: 31663 RVA: 0x001AA8A2 File Offset: 0x001A8AA2
		public static ActionValue New(ListValue actions)
		{
			return ActionModule.Action.Sequence.Invoke(actions).AsAction;
		}

		// Token: 0x06007BB0 RID: 31664 RVA: 0x001AA8B4 File Offset: 0x001A8AB4
		private static ActionValue New(ActionValue value, RecordValue meta, TypeValue type)
		{
			if (meta.IsEmpty && value.Type == type)
			{
				return value;
			}
			return new ActionValue.MetaTypeActionValue(value, meta, type);
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06007BB2 RID: 31666 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Action;
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public sealed override ActionValue AsAction
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06007BB5 RID: 31669 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public virtual object ActionIdentity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06007BB6 RID: 31670 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool CanBind
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007BB7 RID: 31671 RVA: 0x001AA8DD File Offset: 0x001A8ADD
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsAction && value.AsAction.ActionIdentity == this.ActionIdentity;
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x001AA8FC File Offset: 0x001A8AFC
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			return RuntimeHelpers.GetHashCode(this.ActionIdentity);
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06007BB9 RID: 31673 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x001AA909 File Offset: 0x001A8B09
		public override Value NewMeta(RecordValue metaValue)
		{
			return ActionValue.New(this, metaValue, this.Type);
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06007BBB RID: 31675 RVA: 0x001AA918 File Offset: 0x001A8B18
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Action;
			}
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x001AA91F File Offset: 0x001A8B1F
		public override Value NewType(TypeValue type)
		{
			return ActionValue.New(this, this.MetaValue, type);
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x001AA930 File Offset: 0x001A8B30
		public ActionValue Bind(Value binding)
		{
			FunctionValue bindingFunction = ActionValue.GetBindingFunction(binding);
			ActionValue actionValue;
			if (!this.TryBind(bindingFunction, out actionValue))
			{
				actionValue = new ActionValue.BindActionValue(this, bindingFunction);
			}
			return actionValue;
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x001AA958 File Offset: 0x001A8B58
		public bool TryBind(Value binding, out ActionValue action)
		{
			FunctionValue bindingFunction = ActionValue.GetBindingFunction(binding);
			return this.TryBind(bindingFunction, out action);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryBind(FunctionValue binding, out ActionValue action)
		{
			action = null;
			return false;
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public virtual ActionValue ExecuteBindings()
		{
			return this;
		}

		// Token: 0x06007BC1 RID: 31681
		public abstract Value Execute();

		// Token: 0x06007BC2 RID: 31682 RVA: 0x001AA974 File Offset: 0x001A8B74
		public virtual ActionValue Optimize()
		{
			return new ActionValue.OptimizedActionValue(this);
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x001AA97C File Offset: 0x001A8B7C
		public sealed override string ToSource()
		{
			return TextValue.New("Action.Return(...)").ToSource();
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x001AA98D File Offset: 0x001A8B8D
		public sealed override string ToString()
		{
			return "action";
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x001AA994 File Offset: 0x001A8B94
		public sealed override object ToOleDb(Type type)
		{
			return "[Action]";
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x001AA99B File Offset: 0x001A8B9B
		IValue IActionValue.Execute()
		{
			return this.Execute();
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x001AA9A4 File Offset: 0x001A8BA4
		protected static FunctionValue GetBindingFunction(Value actionOrFunction)
		{
			FunctionValue functionValue;
			if (actionOrFunction.IsAction)
			{
				functionValue = new ActionValue.ActionFunctionValue(actionOrFunction.AsAction);
			}
			else
			{
				functionValue = actionOrFunction.AsFunction;
				if (functionValue.Type.AsFunctionType.Parameters.Count == 0)
				{
					functionValue = new ActionValue.OneToZeroFunctionValue(functionValue);
				}
			}
			return functionValue;
		}

		// Token: 0x0400448C RID: 17548
		private const string placeholder = "[Action]";

		// Token: 0x0400448D RID: 17549
		public static readonly TextValue Placeholder = TextValue.New("[Action]");

		// Token: 0x02001257 RID: 4695
		private sealed class ActionFunctionValue : NativeFunctionValue1<ActionValue, Value>
		{
			// Token: 0x06007BC9 RID: 31689 RVA: 0x001AA9FE File Offset: 0x001A8BFE
			public ActionFunctionValue(ActionValue action)
				: base(TypeValue.Action, "value", TypeValue.Any)
			{
				this.action = action;
			}

			// Token: 0x170021C4 RID: 8644
			// (get) Token: 0x06007BCA RID: 31690 RVA: 0x001AAA1C File Offset: 0x001A8C1C
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(this.Type.AsFunctionType.ToFunctionTypeExpression(), new ConstantExpressionSyntaxNode(this.action));
					}
					return this.expression;
				}
			}

			// Token: 0x06007BCB RID: 31691 RVA: 0x001AAA52 File Offset: 0x001A8C52
			public override ActionValue TypedInvoke(Value value)
			{
				return this.action;
			}

			// Token: 0x0400448E RID: 17550
			private readonly ActionValue action;

			// Token: 0x0400448F RID: 17551
			private IExpression expression;
		}

		// Token: 0x02001258 RID: 4696
		private sealed class OneToZeroFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x06007BCC RID: 31692 RVA: 0x001AAA5A File Offset: 0x001A8C5A
			public OneToZeroFunctionValue(FunctionValue function)
				: base(TypeValue.Any, "value", TypeValue.Any)
			{
				this.function = function;
			}

			// Token: 0x170021C5 RID: 8645
			// (get) Token: 0x06007BCD RID: 31693 RVA: 0x001AAA78 File Offset: 0x001A8C78
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(this.Type.AsFunctionType.ToFunctionTypeExpression(), new InvocationExpressionSyntaxNode0(new ConstantExpressionSyntaxNode(this.function)));
					}
					return this.expression;
				}
			}

			// Token: 0x06007BCE RID: 31694 RVA: 0x001AAAB3 File Offset: 0x001A8CB3
			public override Value TypedInvoke(Value value)
			{
				return this.function.Invoke();
			}

			// Token: 0x04004490 RID: 17552
			private readonly FunctionValue function;

			// Token: 0x04004491 RID: 17553
			private IExpression expression;
		}

		// Token: 0x02001259 RID: 4697
		private sealed class ActionActionValue : ActionValue
		{
			// Token: 0x06007BCF RID: 31695 RVA: 0x001AAAC0 File Offset: 0x001A8CC0
			public ActionActionValue(Func<Value> action)
			{
				this.action = action;
			}

			// Token: 0x06007BD0 RID: 31696 RVA: 0x001AAACF File Offset: 0x001A8CCF
			public override Value Execute()
			{
				return this.action();
			}

			// Token: 0x04004492 RID: 17554
			private readonly Func<Value> action;
		}

		// Token: 0x0200125A RID: 4698
		private abstract class DelegatingActionValue : ActionValue
		{
			// Token: 0x06007BD1 RID: 31697 RVA: 0x001AAADC File Offset: 0x001A8CDC
			protected DelegatingActionValue(ActionValue action)
			{
				this.action = action;
			}

			// Token: 0x170021C6 RID: 8646
			// (get) Token: 0x06007BD2 RID: 31698 RVA: 0x001AAAEB File Offset: 0x001A8CEB
			protected ActionValue Action
			{
				get
				{
					return this.action;
				}
			}

			// Token: 0x170021C7 RID: 8647
			// (get) Token: 0x06007BD3 RID: 31699 RVA: 0x001AAAF3 File Offset: 0x001A8CF3
			public override TypeValue Type
			{
				get
				{
					return this.action.Type;
				}
			}

			// Token: 0x170021C8 RID: 8648
			// (get) Token: 0x06007BD4 RID: 31700 RVA: 0x001AAB00 File Offset: 0x001A8D00
			public override RecordValue MetaValue
			{
				get
				{
					return this.action.MetaValue;
				}
			}

			// Token: 0x170021C9 RID: 8649
			// (get) Token: 0x06007BD5 RID: 31701 RVA: 0x001AAB0D File Offset: 0x001A8D0D
			public override IExpression Expression
			{
				get
				{
					return this.action.Expression;
				}
			}

			// Token: 0x170021CA RID: 8650
			// (get) Token: 0x06007BD6 RID: 31702 RVA: 0x001AAB1A File Offset: 0x001A8D1A
			public override object ActionIdentity
			{
				get
				{
					return this.action.ActionIdentity;
				}
			}

			// Token: 0x170021CB RID: 8651
			// (get) Token: 0x06007BD7 RID: 31703 RVA: 0x001AAB27 File Offset: 0x001A8D27
			public override bool CanBind
			{
				get
				{
					return this.action.CanBind;
				}
			}

			// Token: 0x06007BD8 RID: 31704 RVA: 0x001AAB34 File Offset: 0x001A8D34
			public override bool TryBind(FunctionValue binding, out ActionValue action)
			{
				return this.action.TryBind(binding, out action);
			}

			// Token: 0x06007BD9 RID: 31705 RVA: 0x001AAB43 File Offset: 0x001A8D43
			public override ActionValue ExecuteBindings()
			{
				return this.action.ExecuteBindings();
			}

			// Token: 0x06007BDA RID: 31706 RVA: 0x001AAB50 File Offset: 0x001A8D50
			public override Value Execute()
			{
				return this.action.Execute();
			}

			// Token: 0x06007BDB RID: 31707 RVA: 0x001AAB5D File Offset: 0x001A8D5D
			public override ActionValue Optimize()
			{
				return this.action.Optimize();
			}

			// Token: 0x04004493 RID: 17555
			private readonly ActionValue action;
		}

		// Token: 0x0200125B RID: 4699
		private class MetaTypeActionValue : ActionValue.DelegatingActionValue
		{
			// Token: 0x06007BDC RID: 31708 RVA: 0x001AAB6A File Offset: 0x001A8D6A
			public MetaTypeActionValue(ActionValue action, RecordValue meta, TypeValue type)
				: base(action)
			{
				this.meta = meta;
				this.type = type;
			}

			// Token: 0x170021CC RID: 8652
			// (get) Token: 0x06007BDD RID: 31709 RVA: 0x001AAB81 File Offset: 0x001A8D81
			public sealed override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x06007BDE RID: 31710 RVA: 0x001AAB89 File Offset: 0x001A8D89
			public sealed override Value NewMeta(RecordValue metaValue)
			{
				return ActionValue.New(base.Action, metaValue, this.Type);
			}

			// Token: 0x170021CD RID: 8653
			// (get) Token: 0x06007BDF RID: 31711 RVA: 0x001AAB9D File Offset: 0x001A8D9D
			public sealed override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x06007BE0 RID: 31712 RVA: 0x001AABA5 File Offset: 0x001A8DA5
			public sealed override Value NewType(TypeValue type)
			{
				return base.Action.NewType(type).NewMeta(this.meta);
			}

			// Token: 0x04004494 RID: 17556
			private RecordValue meta;

			// Token: 0x04004495 RID: 17557
			private TypeValue type;
		}

		// Token: 0x0200125C RID: 4700
		private class BindActionValue : ActionValue
		{
			// Token: 0x06007BE1 RID: 31713 RVA: 0x001AABBE File Offset: 0x001A8DBE
			public BindActionValue(ActionValue action, FunctionValue binding)
			{
				this.action = action;
				this.binding = binding;
			}

			// Token: 0x170021CE RID: 8654
			// (get) Token: 0x06007BE2 RID: 31714 RVA: 0x001AABD4 File Offset: 0x001A8DD4
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.Action.Bind), new ConstantExpressionSyntaxNode(this.action), new ConstantExpressionSyntaxNode(this.binding));
					}
					return this.expression;
				}
			}

			// Token: 0x06007BE3 RID: 31715 RVA: 0x001AAC10 File Offset: 0x001A8E10
			public sealed override ActionValue ExecuteBindings()
			{
				ActionValue actionValue = this.action.ExecuteBindings();
				ActionValue asAction;
				if (!actionValue.TryBind(this.binding, out asAction))
				{
					asAction = this.binding.Invoke(actionValue.Execute()).AsAction;
				}
				return asAction.ExecuteBindings();
			}

			// Token: 0x06007BE4 RID: 31716 RVA: 0x00172420 File Offset: 0x00170620
			public sealed override Value Execute()
			{
				return this.ExecuteBindings().Execute();
			}

			// Token: 0x04004496 RID: 17558
			protected readonly ActionValue action;

			// Token: 0x04004497 RID: 17559
			protected readonly FunctionValue binding;

			// Token: 0x04004498 RID: 17560
			private IExpression expression;
		}

		// Token: 0x0200125D RID: 4701
		private sealed class OptimizedActionValue : ActionValue.DelegatingActionValue, IOptimizedValue
		{
			// Token: 0x06007BE5 RID: 31717 RVA: 0x001AAC56 File Offset: 0x001A8E56
			public OptimizedActionValue(ActionValue action)
				: base(action)
			{
			}

			// Token: 0x06007BE6 RID: 31718 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override ActionValue Optimize()
			{
				return this;
			}
		}
	}
}
