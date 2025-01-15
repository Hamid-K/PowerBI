using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Action
{
	// Token: 0x02001000 RID: 4096
	public sealed class ActionModule : Module
	{
		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06006B6A RID: 27498 RVA: 0x001721D0 File Offset: 0x001703D0
		public override string Name
		{
			get
			{
				return "Action";
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06006B6B RID: 27499 RVA: 0x001721D7 File Offset: 0x001703D7
		public override Keys ExportKeys
		{
			get
			{
				if (ActionModule.exportKeys == null)
				{
					ActionModule.exportKeys = Keys.New(12, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Binary.End";
						case 1:
							return "Action.Type";
						case 2:
							return "Action.Sequence";
						case 3:
							return "Action.Return";
						case 4:
							return "Action.Try";
						case 5:
							return "Action.DoNothing";
						case 6:
							return "ValueAction.Replace";
						case 7:
							return "ValueAction.NativeStatement";
						case 8:
							return "TableAction.InsertRows";
						case 9:
							return "TableAction.UpdateRows";
						case 10:
							return "TableAction.DeleteRows";
						case 11:
							return "TableAction.Tee";
						default:
							throw new InvalidOperationException(Microsoft.Mashup.Engine1.Strings.UnreachableCodePath);
						}
					});
				}
				return ActionModule.exportKeys;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x06006B6C RID: 27500 RVA: 0x00172210 File Offset: 0x00170410
		public override Keys SectionKeys
		{
			get
			{
				if (ActionModule.sectionKeys == null)
				{
					ActionModule.sectionKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Value.Expression";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Microsoft.Mashup.Engine1.Strings.UnreachableCodePath);
						}
						return "Action.FromHandlers";
					});
				}
				return ActionModule.sectionKeys;
			}
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x00172248 File Offset: 0x00170448
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return Library.Binary.End;
				case 1:
					return TypeValue.Action;
				case 2:
					return ActionModule.Action.Sequence;
				case 3:
					return ActionModule.Action.Return;
				case 4:
					return ActionModule.Action.Try;
				case 5:
					return ActionModule.Action.DoNothing;
				case 6:
					return ActionModule.ValueAction.Replace;
				case 7:
					return ActionModule.ValueAction.NativeStatement;
				case 8:
					return ActionModule.TableAction.InsertRows;
				case 9:
					return ActionModule.TableAction.UpdateRows;
				case 10:
					return ActionModule.TableAction.DeleteRows;
				case 11:
					return new ActionModule.TableAction.TeeFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Microsoft.Mashup.Engine1.Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x0017227C File Offset: 0x0017047C
		protected override RecordValue GetSectionExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.SectionKeys, delegate(int index)
			{
				if (index == 0)
				{
					return Library._Value.Expression;
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Microsoft.Mashup.Engine1.Strings.UnreachableCodePath);
				}
				return new ActionModule.Action.FromHandlersFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x04003BC4 RID: 15300
		private static Keys exportKeys;

		// Token: 0x04003BC5 RID: 15301
		private static Keys sectionKeys;

		// Token: 0x02001001 RID: 4097
		private enum Exports
		{
			// Token: 0x04003BC7 RID: 15303
			Binary_End,
			// Token: 0x04003BC8 RID: 15304
			Action_Type,
			// Token: 0x04003BC9 RID: 15305
			Action_Sequence,
			// Token: 0x04003BCA RID: 15306
			Action_Return,
			// Token: 0x04003BCB RID: 15307
			Action_Try,
			// Token: 0x04003BCC RID: 15308
			Action_DoNothing,
			// Token: 0x04003BCD RID: 15309
			ValueAction_Replace,
			// Token: 0x04003BCE RID: 15310
			ValueAction_NativeStatement,
			// Token: 0x04003BCF RID: 15311
			TableAction_InsertRows,
			// Token: 0x04003BD0 RID: 15312
			TableAction_UpdateRows,
			// Token: 0x04003BD1 RID: 15313
			TableAction_DeleteRows,
			// Token: 0x04003BD2 RID: 15314
			TableAction_Tee,
			// Token: 0x04003BD3 RID: 15315
			Count
		}

		// Token: 0x02001002 RID: 4098
		private enum SectionExports
		{
			// Token: 0x04003BD5 RID: 15317
			Value_Expression,
			// Token: 0x04003BD6 RID: 15318
			Action_FromHandlers,
			// Token: 0x04003BD7 RID: 15319
			Count
		}

		// Token: 0x02001003 RID: 4099
		public static class Action
		{
			// Token: 0x04003BD8 RID: 15320
			public static readonly FunctionValue Bind = new ActionModule.Action.BindFunctionValue();

			// Token: 0x04003BD9 RID: 15321
			public static readonly FunctionValue Sequence = new ActionModule.Action.SequenceFunctionValue();

			// Token: 0x04003BDA RID: 15322
			public static readonly FunctionValue Return = new ActionModule.Action.ReturnFunctionValue();

			// Token: 0x04003BDB RID: 15323
			public static readonly FunctionValue Try = new ActionModule.Action.TryFunctionValue();

			// Token: 0x04003BDC RID: 15324
			public static readonly ActionValue DoNothing = ActionModule.Action.Return.Invoke(Value.Null).AsAction;

			// Token: 0x02001004 RID: 4100
			private sealed class BindFunctionValue : NativeFunctionValue2<ActionValue, ActionValue, Value>
			{
				// Token: 0x06006B71 RID: 27505 RVA: 0x001722FE File Offset: 0x001704FE
				public BindFunctionValue()
					: base(TypeValue.Action, "action", TypeValue.Action, "binding", TypeValue.Any)
				{
				}

				// Token: 0x06006B72 RID: 27506 RVA: 0x0017231F File Offset: 0x0017051F
				public override ActionValue TypedInvoke(ActionValue action, Value binding)
				{
					return action.Bind(binding);
				}
			}

			// Token: 0x02001005 RID: 4101
			private sealed class SequenceFunctionValue : NativeFunctionValue1<ActionValue, ListValue>
			{
				// Token: 0x06006B73 RID: 27507 RVA: 0x00172328 File Offset: 0x00170528
				public SequenceFunctionValue()
					: base(TypeValue.Action, "actions", ListTypeValue.Any)
				{
				}

				// Token: 0x06006B74 RID: 27508 RVA: 0x0017233F File Offset: 0x0017053F
				public override ActionValue TypedInvoke(ListValue actions)
				{
					return new ActionModule.Action.SequenceFunctionValue.SequenceActionValue(actions);
				}

				// Token: 0x02001006 RID: 4102
				private sealed class SequenceActionValue : ActionValue
				{
					// Token: 0x06006B75 RID: 27509 RVA: 0x00172347 File Offset: 0x00170547
					public SequenceActionValue(ListValue actions)
					{
						this.actions = actions;
					}

					// Token: 0x06006B76 RID: 27510 RVA: 0x00172358 File Offset: 0x00170558
					public override ActionValue ExecuteBindings()
					{
						ListValue listValue = this.actions;
						ActionValue actionValue;
						for (;;)
						{
							actionValue = ActionModule.Action.DoNothing;
							foreach (IValueReference valueReference in listValue)
							{
								actionValue = actionValue.ExecuteBindings();
								if (!actionValue.CanBind)
								{
									Value value = actionValue.Execute();
									actionValue = ActionModule.Action.Return.Invoke(value).AsAction;
								}
								FunctionValue bindingFunction = ActionValue.GetBindingFunction(valueReference.Value);
								ActionValue actionValue2;
								if (actionValue.TryBind(bindingFunction, out actionValue2))
								{
									actionValue = actionValue2;
								}
								else
								{
									Value value2 = actionValue.Execute();
									actionValue = bindingFunction.Invoke(value2).AsAction;
								}
							}
							ActionModule.Action.SequenceFunctionValue.SequenceActionValue sequenceActionValue = actionValue as ActionModule.Action.SequenceFunctionValue.SequenceActionValue;
							if (sequenceActionValue == null)
							{
								break;
							}
							listValue = sequenceActionValue.actions;
						}
						return actionValue.ExecuteBindings();
					}

					// Token: 0x06006B77 RID: 27511 RVA: 0x00172420 File Offset: 0x00170620
					public override Value Execute()
					{
						return this.ExecuteBindings().Execute();
					}

					// Token: 0x04003BDD RID: 15325
					private readonly ListValue actions;
				}
			}

			// Token: 0x02001007 RID: 4103
			private sealed class ReturnFunctionValue : NativeFunctionValue1<ActionValue, Value>
			{
				// Token: 0x06006B78 RID: 27512 RVA: 0x0017242D File Offset: 0x0017062D
				public ReturnFunctionValue()
					: base(TypeValue.Action, "value", TypeValue.Any)
				{
				}

				// Token: 0x06006B79 RID: 27513 RVA: 0x00172444 File Offset: 0x00170644
				public override ActionValue TypedInvoke(Value value)
				{
					return new ActionModule.Action.ReturnFunctionValue.ValueActionValue(value);
				}

				// Token: 0x02001008 RID: 4104
				private sealed class ValueActionValue : ActionValue
				{
					// Token: 0x06006B7A RID: 27514 RVA: 0x0017244C File Offset: 0x0017064C
					public ValueActionValue(Value value)
					{
						this.value = value;
					}

					// Token: 0x17001EB5 RID: 7861
					// (get) Token: 0x06006B7B RID: 27515 RVA: 0x0017245B File Offset: 0x0017065B
					public override IExpression Expression
					{
						get
						{
							if (this.expression == null)
							{
								this.expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new ConstantExpressionSyntaxNode(this.value));
							}
							return this.expression;
						}
					}

					// Token: 0x06006B7C RID: 27516 RVA: 0x0017248B File Offset: 0x0017068B
					public override Value Execute()
					{
						return this.value;
					}

					// Token: 0x04003BDE RID: 15326
					private readonly Value value;

					// Token: 0x04003BDF RID: 15327
					private IExpression expression;
				}
			}

			// Token: 0x02001009 RID: 4105
			private sealed class TryFunctionValue : NativeFunctionValue1<ActionValue, ActionValue>
			{
				// Token: 0x06006B7D RID: 27517 RVA: 0x00172493 File Offset: 0x00170693
				public TryFunctionValue()
					: base(TypeValue.Action, "action", TypeValue.Action)
				{
				}

				// Token: 0x06006B7E RID: 27518 RVA: 0x001724AA File Offset: 0x001706AA
				public override ActionValue TypedInvoke(ActionValue action)
				{
					return new ActionModule.Action.TryFunctionValue.TryActionValue(action);
				}

				// Token: 0x0200100A RID: 4106
				private sealed class TryActionValue : ActionValue
				{
					// Token: 0x06006B7F RID: 27519 RVA: 0x001724B2 File Offset: 0x001706B2
					public TryActionValue(ActionValue action)
					{
						this.action = action;
					}

					// Token: 0x06006B80 RID: 27520 RVA: 0x001724C4 File Offset: 0x001706C4
					public override Value Execute()
					{
						Value value;
						try
						{
							value = RecordValue.New(ActionModule.Action.TryFunctionValue.TryActionValue.valueKeys, new Value[]
							{
								LogicalValue.False,
								this.action.Execute()
							});
						}
						catch (ValueException ex)
						{
							value = RecordValue.New(ActionModule.Action.TryFunctionValue.TryActionValue.errorKeys, new Value[]
							{
								LogicalValue.True,
								ex.Value
							});
						}
						return value;
					}

					// Token: 0x04003BE0 RID: 15328
					private static readonly Keys valueKeys = Keys.New("HasError", "Value");

					// Token: 0x04003BE1 RID: 15329
					private static readonly Keys errorKeys = Keys.New("HasError", "Error");

					// Token: 0x04003BE2 RID: 15330
					private readonly ActionValue action;
				}
			}

			// Token: 0x0200100B RID: 4107
			public class FromHandlersFunctionValue : NativeFunctionValue1<ActionValue, RecordValue>
			{
				// Token: 0x06006B82 RID: 27522 RVA: 0x0017255A File Offset: 0x0017075A
				public FromHandlersFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Action, "handlers", TypeValue.Record)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06006B83 RID: 27523 RVA: 0x00172578 File Offset: 0x00170778
				public override ActionValue TypedInvoke(RecordValue handlers)
				{
					return new ActionModule.Action.FromHandlersFunctionValue.HandlersActionValue(this.engineHost, handlers);
				}

				// Token: 0x04003BE3 RID: 15331
				private readonly IEngineHost engineHost;

				// Token: 0x0200100C RID: 4108
				private class HandlersActionValue : ActionValue
				{
					// Token: 0x06006B84 RID: 27524 RVA: 0x00172586 File Offset: 0x00170786
					public HandlersActionValue(IEngineHost engineHost, RecordValue handlers)
					{
						this.engineHost = engineHost;
						this.handlers = handlers;
					}

					// Token: 0x17001EB6 RID: 7862
					// (get) Token: 0x06006B85 RID: 27525 RVA: 0x0017259C File Offset: 0x0017079C
					public override IExpression Expression
					{
						get
						{
							try
							{
								FunctionValue functionValue;
								if (this.TryGetHandler("GetExpression", out functionValue))
								{
									Value value = functionValue.Invoke();
									if (value.IsNull)
									{
										return null;
									}
									return MAstToExpressionVisitor.ToExpression(value.AsRecord);
								}
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
							return null;
						}
					}

					// Token: 0x17001EB7 RID: 7863
					// (get) Token: 0x06006B86 RID: 27526 RVA: 0x0017260C File Offset: 0x0017080C
					public override bool CanBind
					{
						get
						{
							bool flag = this.HasHandler("OnBind");
							if (!flag)
							{
								this.ReportFoldingFailure();
							}
							return flag;
						}
					}

					// Token: 0x06006B87 RID: 27527 RVA: 0x00172624 File Offset: 0x00170824
					public override bool TryBind(FunctionValue function, out ActionValue action)
					{
						FunctionValue functionValue;
						if (this.TryGetHandler("OnBind", out functionValue))
						{
							try
							{
								action = functionValue.Invoke(function).AsAction;
								return true;
							}
							catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
							{
							}
						}
						this.ReportFoldingFailure();
						action = null;
						return false;
					}

					// Token: 0x06006B88 RID: 27528 RVA: 0x0017268C File Offset: 0x0017088C
					public override Value Execute()
					{
						return this.GetHandler("OnExecute").Invoke().AsAction.Execute();
					}

					// Token: 0x06006B89 RID: 27529 RVA: 0x001726A8 File Offset: 0x001708A8
					public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
					{
						try
						{
							FunctionValue functionValue;
							if (this.TryGetHandler("OnInvoke", out functionValue))
							{
								result = functionValue.Invoke(function, ListValue.New(arguments), NumberValue.New(index));
								return true;
							}
						}
						catch (ValueException ex) when (!ViewExceptions.IsMarked(ex))
						{
						}
						this.ReportFoldingFailure();
						result = null;
						return false;
					}

					// Token: 0x06006B8A RID: 27530 RVA: 0x0017271C File Offset: 0x0017091C
					private bool HasHandler(string key)
					{
						FunctionValue functionValue;
						return this.TryGetHandler(key, out functionValue);
					}

					// Token: 0x06006B8B RID: 27531 RVA: 0x00172734 File Offset: 0x00170934
					private bool TryGetHandler(string key, out FunctionValue handler)
					{
						Value value;
						if (this.handlers.TryGetValue(key, out value))
						{
							handler = value.AsFunction;
							return true;
						}
						handler = null;
						return false;
					}

					// Token: 0x06006B8C RID: 27532 RVA: 0x0017275F File Offset: 0x0017095F
					private FunctionValue GetHandler(string key)
					{
						return this.handlers[key].AsFunction;
					}

					// Token: 0x06006B8D RID: 27533 RVA: 0x00172774 File Offset: 0x00170974
					private void ReportFoldingFailure()
					{
						using (TracingService.CreatePerformanceTrace(this.engineHost, "HandlersActionValue/ReportFoldingFailure", TraceEventType.Information, null))
						{
							if (this.engineHost.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure)
							{
								throw ValueException.NewExpressionError<Message0>(Microsoft.Mashup.Engine1.Strings.FoldingFailure, null, null);
							}
						}
					}

					// Token: 0x04003BE4 RID: 15332
					private const string GetExpressionKey = "GetExpression";

					// Token: 0x04003BE5 RID: 15333
					private const string OnBindKey = "OnBind";

					// Token: 0x04003BE6 RID: 15334
					private const string OnExecuteKey = "OnExecute";

					// Token: 0x04003BE7 RID: 15335
					private const string OnInvokeKey = "OnInvoke";

					// Token: 0x04003BE8 RID: 15336
					private readonly IEngineHost engineHost;

					// Token: 0x04003BE9 RID: 15337
					private readonly RecordValue handlers;
				}
			}
		}

		// Token: 0x0200100D RID: 4109
		public static class ValueAction
		{
			// Token: 0x04003BEA RID: 15338
			public static readonly FunctionValue Replace = new ActionModule.ValueAction.ReplaceFunctionValue();

			// Token: 0x04003BEB RID: 15339
			public static readonly FunctionValue NativeStatement = new ActionModule.ValueAction.NativeStatementFunctionValue();

			// Token: 0x0200100E RID: 4110
			private sealed class ReplaceFunctionValue : NativeFunctionValue2<ActionValue, Value, Value>
			{
				// Token: 0x06006B8F RID: 27535 RVA: 0x001727E6 File Offset: 0x001709E6
				public ReplaceFunctionValue()
					: base(TypeValue.Action, "target", TypeValue.Any, "source", TypeValue.Any)
				{
				}

				// Token: 0x06006B90 RID: 27536 RVA: 0x00172807 File Offset: 0x00170A07
				public override ActionValue TypedInvoke(Value target, Value source)
				{
					return target.Replace(source);
				}
			}

			// Token: 0x0200100F RID: 4111
			private sealed class NativeStatementFunctionValue : NativeFunctionValue4<ActionValue, Value, TextValue, Value, Value>
			{
				// Token: 0x06006B91 RID: 27537 RVA: 0x00172810 File Offset: 0x00170A10
				public NativeStatementFunctionValue()
					: base(TypeValue.Action, 2, "target", TypeValue.Any, "statement", TypeValue.Text, "parameters", TypeValue.Any, "options", TypeValue.Record.Nullable)
				{
				}

				// Token: 0x06006B92 RID: 27538 RVA: 0x00172856 File Offset: 0x00170A56
				public override ActionValue TypedInvoke(Value target, TextValue statement, Value parameters, Value options)
				{
					return target.NativeStatement(statement, parameters, options);
				}
			}
		}

		// Token: 0x02001010 RID: 4112
		public static class TableAction
		{
			// Token: 0x04003BEC RID: 15340
			public static readonly FunctionValue InsertRows = new ActionModule.TableAction.InsertRowsFunctionValue();

			// Token: 0x04003BED RID: 15341
			public static readonly FunctionValue UpdateRows = new ActionModule.TableAction.UpdateRowsFunctionValue();

			// Token: 0x04003BEE RID: 15342
			public static readonly FunctionValue DeleteRows = new ActionModule.TableAction.DeleteRowsFunctionValue();

			// Token: 0x02001011 RID: 4113
			private sealed class InsertRowsFunctionValue : NativeFunctionValue2<ActionValue, TableValue, TableValue>
			{
				// Token: 0x06006B94 RID: 27540 RVA: 0x00172882 File Offset: 0x00170A82
				public InsertRowsFunctionValue()
					: base(TypeValue.Action, "table", TypeValue.Table, "rowsToInsert", TypeValue.Table)
				{
				}

				// Token: 0x06006B95 RID: 27541 RVA: 0x001728A3 File Offset: 0x00170AA3
				public override ActionValue TypedInvoke(TableValue table, TableValue rowsToInsert)
				{
					return table.InsertRows(rowsToInsert);
				}
			}

			// Token: 0x02001012 RID: 4114
			private sealed class UpdateRowsFunctionValue : NativeFunctionValue2<ActionValue, TableValue, ListValue>
			{
				// Token: 0x06006B96 RID: 27542 RVA: 0x001728AC File Offset: 0x00170AAC
				public UpdateRowsFunctionValue()
					: base(TypeValue.Action, "table", TypeValue.Table, "columnUpdates", ListTypeValue.Any)
				{
				}

				// Token: 0x06006B97 RID: 27543 RVA: 0x001728CD File Offset: 0x00170ACD
				public override ActionValue TypedInvoke(TableValue table, ListValue columnUpdates)
				{
					return table.UpdateRows(columnUpdates);
				}
			}

			// Token: 0x02001013 RID: 4115
			private sealed class DeleteRowsFunctionValue : NativeFunctionValue1<ActionValue, TableValue>
			{
				// Token: 0x06006B98 RID: 27544 RVA: 0x001728D6 File Offset: 0x00170AD6
				public DeleteRowsFunctionValue()
					: base(TypeValue.Action, "table", TypeValue.Table)
				{
				}

				// Token: 0x06006B99 RID: 27545 RVA: 0x001728ED File Offset: 0x00170AED
				public override ActionValue TypedInvoke(TableValue table)
				{
					return table.DeleteRows();
				}
			}

			// Token: 0x02001014 RID: 4116
			public sealed class TeeFunctionValue : NativeFunctionValue2<ActionValue, TableValue, ListValue>
			{
				// Token: 0x06006B9A RID: 27546 RVA: 0x001728F5 File Offset: 0x00170AF5
				public TeeFunctionValue(IEngineHost host)
					: base(TypeValue.Action, "table", TypeValue.Table, "actions", ActionModule.TableAction.TeeFunctionValue.listOfFunctionsType)
				{
					this.host = host;
				}

				// Token: 0x06006B9B RID: 27547 RVA: 0x00172920 File Offset: 0x00170B20
				public override ActionValue TypedInvoke(TableValue table, ListValue actions)
				{
					FunctionValue[] functions = actions.Select((IValueReference a) => a.Value.AsFunction).ToArray<FunctionValue>();
					return ActionValue.New(delegate
					{
						EngineContext.Enable();
						Value value;
						try
						{
							ActionModule.TableAction.TeeFunctionValue.Result[] results = new ActionModule.TableAction.TeeFunctionValue.Result[functions.Length];
							using (IPageReader reader = table.GetReader())
							{
								using (ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage> singleWriterMultipleReaderQueueSource = new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>(1))
								{
									IThreadPoolService threadPoolService = this.host.QueryService<IThreadPoolService>();
									ILifetimeService lifetimeService = this.host.QueryService<ILifetimeService>();
									ActionModule.TableAction.TeeFunctionValue.VerifyPrimitiveColumnTypes(table.Type.AsTableType);
									Semaphore actionsDone = new Semaphore(0, functions.Length);
									TableValue[] tables = new TableValue[functions.Length];
									IPageReader[] readers = new IPageReader[functions.Length];
									for (int i = 0; i < functions.Length; i++)
									{
										ActionModule.TableAction.TeeFunctionValue.<>c__DisplayClass4_2 CS$<>8__locals3 = new ActionModule.TableAction.TeeFunctionValue.<>c__DisplayClass4_2();
										CS$<>8__locals3.queueReader = new ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader(reader.LeaveEngineContext<IPageReader>(), singleWriterMultipleReaderQueueSource.GetReader());
										readers[i] = CS$<>8__locals3.queueReader;
										tables[i] = new GetPageReaderTableValue(lifetimeService, new Func<IPageReader>(new SingleInvocation<IPageReader>(new Func<IPageReader>(CS$<>8__locals3, ldftn(<TypedInvoke>b__3))).Invoke));
										results[i] = new ActionModule.TableAction.TeeFunctionValue.Result
										{
											Exception = new InvalidOperationException("No result was set.")
										};
									}
									long readerExceptionCount = 0L;
									ParameterizedThreadStart parameterizedThreadStart = delegate(object o)
									{
										int num = (int)o;
										try
										{
											using (EngineContext.Enter())
											{
												ActionValue asAction = functions[num].Invoke(tables[num]).AsAction;
												results[num].Value = asAction.Execute();
												results[num].Exception = null;
											}
										}
										catch (Exception ex2)
										{
											Interlocked.Increment(ref readerExceptionCount);
											results[num].Exception = ex2;
										}
										finally
										{
											readers[num].Dispose();
											actionsDone.Release();
										}
									};
									for (int j = 0; j < functions.Length; j++)
									{
										threadPoolService.StartThread(parameterizedThreadStart, j);
									}
									ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.Writer writer = singleWriterMultipleReaderQueueSource.GetWriter();
									for (;;)
									{
										IPage page;
										if (Interlocked.Read(ref readerExceptionCount) > 0L)
										{
											ISerializedException ex;
											if (!PageExceptionSerializer.TryGetPropertiesFromException(ValueException.NewExpressionError<Message0>(Microsoft.Mashup.Engine1.Strings.TableAction_Tee_ErrorInAction, null, null), out ex))
											{
												break;
											}
											page = new ActionModule.TableAction.TeeFunctionValue.ExceptionPage(reader.Schema, ex);
										}
										else
										{
											page = reader.CreatePage();
											reader.Read(page);
										}
										using (EngineContext.Leave())
										{
											IConcurrentPage concurrentPage = page as IConcurrentPage;
											if (concurrentPage != null)
											{
												concurrentPage.BufferForRead();
											}
											try
											{
												writer.Enqueue(page.LeaveEngineContext<IPage>());
											}
											catch (ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.NoReadersException)
											{
												goto IL_021E;
											}
										}
										if (page.RowCount == 0)
										{
											goto IL_021E;
										}
									}
									throw new InvalidOperationException("Can't serialize exception.");
									IL_021E:
									using (EngineContext.Leave())
									{
										for (int k = 0; k < functions.Length; k++)
										{
											actionsDone.WaitOne();
										}
									}
									IValueReference[] results2 = results;
									value = ListValue.New(results2);
								}
							}
						}
						finally
						{
							EngineContext.Disable();
						}
						return value;
					});
				}

				// Token: 0x06006B9C RID: 27548 RVA: 0x00172980 File Offset: 0x00170B80
				private static void VerifyPrimitiveColumnTypes(TableTypeValue tableType)
				{
					for (int i = 0; i < tableType.ItemType.Fields.Count; i++)
					{
						ValueKind typeKind = tableType.ItemType.Fields[i].AsRecord["Type"].AsType.TypeKind;
						if (typeKind > ValueKind.Binary)
						{
							throw ValueException.NewExpressionError<Message0>(Microsoft.Mashup.Engine1.Strings.TableAction_Tee_InputTableMustHavePrimitiveValues, null, null);
						}
					}
				}

				// Token: 0x04003BEF RID: 15343
				private const int bufferPageCount = 1;

				// Token: 0x04003BF0 RID: 15344
				private static readonly TypeValue listOfFunctionsType = ListTypeValue.New(TypeValue.Function);

				// Token: 0x04003BF1 RID: 15345
				private readonly IEngineHost host;

				// Token: 0x02001015 RID: 4117
				private class Result : IValueReference
				{
					// Token: 0x17001EB8 RID: 7864
					// (get) Token: 0x06006B9E RID: 27550 RVA: 0x001729F6 File Offset: 0x00170BF6
					public bool Evaluated
					{
						get
						{
							return this.value != null || this.exception != null;
						}
					}

					// Token: 0x17001EB9 RID: 7865
					// (get) Token: 0x06006B9F RID: 27551 RVA: 0x00172A0B File Offset: 0x00170C0B
					// (set) Token: 0x06006BA0 RID: 27552 RVA: 0x00172A22 File Offset: 0x00170C22
					public Value Value
					{
						get
						{
							if (this.exception != null)
							{
								throw this.exception;
							}
							return this.value;
						}
						set
						{
							this.value = value;
						}
					}

					// Token: 0x17001EBA RID: 7866
					// (get) Token: 0x06006BA1 RID: 27553 RVA: 0x00172A2B File Offset: 0x00170C2B
					// (set) Token: 0x06006BA2 RID: 27554 RVA: 0x00172A33 File Offset: 0x00170C33
					public Exception Exception
					{
						get
						{
							return this.exception;
						}
						set
						{
							this.exception = value;
						}
					}

					// Token: 0x04003BF2 RID: 15346
					private Value value;

					// Token: 0x04003BF3 RID: 15347
					private Exception exception;
				}

				// Token: 0x02001016 RID: 4118
				private class ExceptionPage : IPage, IDisposable
				{
					// Token: 0x06006BA4 RID: 27556 RVA: 0x00172A3C File Offset: 0x00170C3C
					public ExceptionPage(TableSchema schema, ISerializedException exception)
					{
						this.columns = Column.CreateColumns(schema, 0);
						this.exception = exception;
						this.exceptionRows = new Dictionary<int, IExceptionRow>();
					}

					// Token: 0x17001EBB RID: 7867
					// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x00172A63 File Offset: 0x00170C63
					public int ColumnCount
					{
						get
						{
							return this.columns.Length;
						}
					}

					// Token: 0x17001EBC RID: 7868
					// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x00002105 File Offset: 0x00000305
					public int RowCount
					{
						get
						{
							return 0;
						}
					}

					// Token: 0x06006BA7 RID: 27559 RVA: 0x00172A6D File Offset: 0x00170C6D
					public IColumn GetColumn(int ordinal)
					{
						return this.columns[ordinal];
					}

					// Token: 0x17001EBD RID: 7869
					// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x00172A77 File Offset: 0x00170C77
					public IDictionary<int, IExceptionRow> ExceptionRows
					{
						get
						{
							return this.exceptionRows;
						}
					}

					// Token: 0x17001EBE RID: 7870
					// (get) Token: 0x06006BA9 RID: 27561 RVA: 0x00172A7F File Offset: 0x00170C7F
					public ISerializedException PageException
					{
						get
						{
							return this.exception;
						}
					}

					// Token: 0x06006BAA RID: 27562 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x04003BF4 RID: 15348
					private readonly Column[] columns;

					// Token: 0x04003BF5 RID: 15349
					private readonly ISerializedException exception;

					// Token: 0x04003BF6 RID: 15350
					private readonly Dictionary<int, IExceptionRow> exceptionRows;
				}

				// Token: 0x02001017 RID: 4119
				private class QueueSourcePageReader : IPageReader, IDisposable
				{
					// Token: 0x06006BAB RID: 27563 RVA: 0x00172A87 File Offset: 0x00170C87
					public QueueSourcePageReader(IPageReader source, ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.Reader reader)
					{
						this.source = source;
						this.reader = reader;
					}

					// Token: 0x17001EBF RID: 7871
					// (get) Token: 0x06006BAC RID: 27564 RVA: 0x00172A9D File Offset: 0x00170C9D
					public TableSchema Schema
					{
						get
						{
							return this.source.Schema;
						}
					}

					// Token: 0x17001EC0 RID: 7872
					// (get) Token: 0x06006BAD RID: 27565 RVA: 0x00172AAA File Offset: 0x00170CAA
					public IProgress Progress
					{
						get
						{
							return this.source.Progress;
						}
					}

					// Token: 0x17001EC1 RID: 7873
					// (get) Token: 0x06006BAE RID: 27566 RVA: 0x00172AB7 File Offset: 0x00170CB7
					public int MaxPageRowCount
					{
						get
						{
							return this.source.MaxPageRowCount;
						}
					}

					// Token: 0x06006BAF RID: 27567 RVA: 0x00172AC4 File Offset: 0x00170CC4
					public IPage CreatePage()
					{
						return new ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage(this.reader);
					}

					// Token: 0x06006BB0 RID: 27568 RVA: 0x00172AD4 File Offset: 0x00170CD4
					public void Read(IPage page)
					{
						int num;
						((ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.IQueuePage)page).SetPage(this.reader.Dequeue(out num), num);
					}

					// Token: 0x06006BB1 RID: 27569 RVA: 0x000020FA File Offset: 0x000002FA
					public IPageReader NextResult()
					{
						return null;
					}

					// Token: 0x06006BB2 RID: 27570 RVA: 0x00172AFA File Offset: 0x00170CFA
					public void Dispose()
					{
						this.reader.Dispose();
					}

					// Token: 0x04003BF7 RID: 15351
					private readonly IPageReader source;

					// Token: 0x04003BF8 RID: 15352
					private readonly ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.Reader reader;

					// Token: 0x02001018 RID: 4120
					private interface IQueuePage : IPage, IDisposable
					{
						// Token: 0x06006BB3 RID: 27571
						void SetPage(IPage page, int index);
					}

					// Token: 0x02001019 RID: 4121
					private class QueuePage : ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.IQueuePage, IPage, IDisposable, ILeaveEngineContext<IPage>, IEnterEngineContext<IPage>
					{
						// Token: 0x06006BB4 RID: 27572 RVA: 0x00172B07 File Offset: 0x00170D07
						public QueuePage(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.Reader reader)
						{
							this.index = -1;
							this.reader = reader;
							this.enterOnUsePage = new ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage.EnterOnUseQueuePage(this);
							this.leaveOnUsePage = new ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage.LeaveOnUseQueuePage(this);
						}

						// Token: 0x06006BB5 RID: 27573 RVA: 0x00172B35 File Offset: 0x00170D35
						public void SetPage(IPage page, int index)
						{
							if (this.index != -1)
							{
								this.reader.DisposeAt(this.index);
							}
							this.page = page;
							this.index = index;
							this.enterOnUsePage.Clear();
							this.leaveOnUsePage.Clear();
						}

						// Token: 0x17001EC2 RID: 7874
						// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x00172B75 File Offset: 0x00170D75
						public int ColumnCount
						{
							get
							{
								return this.page.ColumnCount;
							}
						}

						// Token: 0x17001EC3 RID: 7875
						// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x00172B82 File Offset: 0x00170D82
						public int RowCount
						{
							get
							{
								IPage page = this.page;
								if (page == null)
								{
									return 0;
								}
								return page.RowCount;
							}
						}

						// Token: 0x06006BB8 RID: 27576 RVA: 0x00172B95 File Offset: 0x00170D95
						public IColumn GetColumn(int ordinal)
						{
							return this.page.GetColumn(ordinal);
						}

						// Token: 0x17001EC4 RID: 7876
						// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x00172BA3 File Offset: 0x00170DA3
						public IDictionary<int, IExceptionRow> ExceptionRows
						{
							get
							{
								return this.page.ExceptionRows;
							}
						}

						// Token: 0x17001EC5 RID: 7877
						// (get) Token: 0x06006BBA RID: 27578 RVA: 0x00172BB0 File Offset: 0x00170DB0
						public ISerializedException PageException
						{
							get
							{
								IPage page = this.page;
								if (page == null)
								{
									return null;
								}
								return page.PageException;
							}
						}

						// Token: 0x06006BBB RID: 27579 RVA: 0x00172BC3 File Offset: 0x00170DC3
						public void Dispose()
						{
							this.reader.DisposeAt(this.index);
						}

						// Token: 0x06006BBC RID: 27580 RVA: 0x00172BD6 File Offset: 0x00170DD6
						public IPage Leave()
						{
							return this.enterOnUsePage;
						}

						// Token: 0x06006BBD RID: 27581 RVA: 0x00172BDE File Offset: 0x00170DDE
						public IPage Enter()
						{
							return this.leaveOnUsePage;
						}

						// Token: 0x04003BF9 RID: 15353
						private const int emptyIndex = -1;

						// Token: 0x04003BFA RID: 15354
						private readonly ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<IPage>.Reader reader;

						// Token: 0x04003BFB RID: 15355
						private readonly ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage.EnterOnUseQueuePage enterOnUsePage;

						// Token: 0x04003BFC RID: 15356
						private readonly ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage.LeaveOnUseQueuePage leaveOnUsePage;

						// Token: 0x04003BFD RID: 15357
						private IPage page;

						// Token: 0x04003BFE RID: 15358
						private int index;

						// Token: 0x0200101A RID: 4122
						private class EnterOnUseQueuePage : VirtualPage, ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.IQueuePage, IPage, IDisposable, IEnterEngineContext<IPage>
						{
							// Token: 0x06006BBE RID: 27582 RVA: 0x00172BE6 File Offset: 0x00170DE6
							public EnterOnUseQueuePage(ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage queuePage)
							{
								this.queuePage = queuePage;
							}

							// Token: 0x17001EC6 RID: 7878
							// (get) Token: 0x06006BBF RID: 27583 RVA: 0x00172BF5 File Offset: 0x00170DF5
							protected override IPage Page
							{
								get
								{
									if (this.enterOnUsePage == null)
									{
										this.enterOnUsePage = this.queuePage.page.LeaveEngineContext<IPage>();
									}
									return this.enterOnUsePage;
								}
							}

							// Token: 0x06006BC0 RID: 27584 RVA: 0x00172C1B File Offset: 0x00170E1B
							public void SetPage(IPage page, int index)
							{
								this.queuePage.SetPage(page.EnterEngineContext<IPage>(), index);
							}

							// Token: 0x06006BC1 RID: 27585 RVA: 0x00172C30 File Offset: 0x00170E30
							public override void Dispose()
							{
								using (EngineContext.Enter())
								{
									this.queuePage.Dispose();
								}
							}

							// Token: 0x06006BC2 RID: 27586 RVA: 0x00172C70 File Offset: 0x00170E70
							public void Clear()
							{
								this.enterOnUsePage = null;
							}

							// Token: 0x06006BC3 RID: 27587 RVA: 0x00172C79 File Offset: 0x00170E79
							public IPage Enter()
							{
								return this.queuePage;
							}

							// Token: 0x04003BFF RID: 15359
							private readonly ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage queuePage;

							// Token: 0x04003C00 RID: 15360
							private IPage enterOnUsePage;
						}

						// Token: 0x0200101B RID: 4123
						private class LeaveOnUseQueuePage : VirtualPage, ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.IQueuePage, IPage, IDisposable, ILeaveEngineContext<IPage>
						{
							// Token: 0x06006BC4 RID: 27588 RVA: 0x00172C81 File Offset: 0x00170E81
							public LeaveOnUseQueuePage(ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage queuePage)
							{
								this.queuePage = queuePage;
							}

							// Token: 0x17001EC7 RID: 7879
							// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x00172C90 File Offset: 0x00170E90
							protected override IPage Page
							{
								get
								{
									if (this.leaveOnUsePage == null)
									{
										this.leaveOnUsePage = this.queuePage.page.EnterEngineContext<IPage>();
									}
									return this.leaveOnUsePage;
								}
							}

							// Token: 0x06006BC6 RID: 27590 RVA: 0x00172CB6 File Offset: 0x00170EB6
							public void SetPage(IPage page, int index)
							{
								this.queuePage.SetPage(page.LeaveEngineContext<IPage>(), index);
							}

							// Token: 0x06006BC7 RID: 27591 RVA: 0x00172CCC File Offset: 0x00170ECC
							public override void Dispose()
							{
								using (EngineContext.Leave())
								{
									this.queuePage.Dispose();
								}
							}

							// Token: 0x06006BC8 RID: 27592 RVA: 0x00172D0C File Offset: 0x00170F0C
							public void Clear()
							{
								this.leaveOnUsePage = null;
							}

							// Token: 0x06006BC9 RID: 27593 RVA: 0x00172D15 File Offset: 0x00170F15
							public IPage Leave()
							{
								return this.queuePage;
							}

							// Token: 0x04003C01 RID: 15361
							private readonly ActionModule.TableAction.TeeFunctionValue.QueueSourcePageReader.QueuePage queuePage;

							// Token: 0x04003C02 RID: 15362
							private IPage leaveOnUsePage;
						}
					}
				}

				// Token: 0x0200101C RID: 4124
				private class SingleWriterMultipleReaderQueueSource<T> : IDisposable where T : IDisposable
				{
					// Token: 0x06006BCA RID: 27594 RVA: 0x00172D20 File Offset: 0x00170F20
					public SingleWriterMultipleReaderQueueSource(int maxItems)
					{
						this.maxItems = maxItems;
						this.items = new List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item>(this.maxItems);
						this.itemsByIndex = new Dictionary<int, ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item>();
						this.readers = new HashSet<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader>();
						this.notEmpty = new ManualResetEvent(false);
						this.notFull = new ManualResetEvent(true);
						this.minIndex = 0;
						this.maxIndex = -1;
					}

					// Token: 0x06006BCB RID: 27595 RVA: 0x00172D88 File Offset: 0x00170F88
					public ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader GetReader()
					{
						ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader reader = new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader(this);
						this.readers.Add(reader);
						return reader;
					}

					// Token: 0x06006BCC RID: 27596 RVA: 0x00172DAA File Offset: 0x00170FAA
					public ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Writer GetWriter()
					{
						if (this.writer != null)
						{
							throw new InvalidOperationException("Only a single writer is supported.");
						}
						this.writer = new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Writer(this);
						return this.writer;
					}

					// Token: 0x06006BCD RID: 27597 RVA: 0x00172DD4 File Offset: 0x00170FD4
					private T Dequeue(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader reader, int index)
					{
						T value;
						for (;;)
						{
							List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> list = this.items;
							lock (list)
							{
								if (index < this.minIndex)
								{
									throw new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.ReaderAddedAfterEnumerationException();
								}
								ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item item = null;
								if (index <= this.maxIndex)
								{
									item = this.items[index - this.minIndex];
									item.dequeuedBy.Add(reader);
								}
								if (this.items.Count > 0 && this.readers.IsSubsetOf(this.items[0].dequeuedBy))
								{
									this.items.RemoveAt(0);
									this.minIndex++;
									this.notFull.Set();
								}
								if (item != null)
								{
									value = item.value;
									break;
								}
								this.notEmpty.Reset();
							}
							this.notEmpty.WaitOne();
						}
						return value;
					}

					// Token: 0x06006BCE RID: 27598 RVA: 0x00172EC4 File Offset: 0x001710C4
					private void Enqueue(T item)
					{
						List<IDisposable> list = null;
						try
						{
							for (;;)
							{
								List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> list2 = this.items;
								lock (list2)
								{
									if (this.readers.Count == 0)
									{
										while (this.items.Count > 0)
										{
											ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item item2;
											if (this.itemsByIndex.TryGetValue(this.minIndex, out item2))
											{
												if (list == null)
												{
													list = new List<IDisposable>();
												}
												list.Add(item2.value);
											}
											this.items.RemoveAt(0);
											this.minIndex++;
										}
										throw new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.NoReadersException();
									}
									if (this.maxIndex - this.minIndex + 1 != this.maxItems)
									{
										this.maxIndex++;
										ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item item3 = new ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item(item);
										this.items.Add(item3);
										this.itemsByIndex.Add(this.maxIndex, item3);
										this.notEmpty.Set();
										break;
									}
									this.notFull.Reset();
								}
								this.notFull.WaitOne();
							}
						}
						catch (ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.NoReadersException)
						{
							if (list != null)
							{
								foreach (IDisposable disposable in list)
								{
									disposable.Dispose();
								}
							}
						}
					}

					// Token: 0x06006BCF RID: 27599 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x06006BD0 RID: 27600 RVA: 0x00173058 File Offset: 0x00171258
					private void Dispose(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader reader)
					{
						List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> list = this.items;
						lock (list)
						{
							this.readers.Remove(reader);
							this.notEmpty.Set();
							this.notFull.Set();
						}
					}

					// Token: 0x06006BD1 RID: 27601 RVA: 0x001730B8 File Offset: 0x001712B8
					private void DisposeItem(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader reader, int index)
					{
						IDisposable disposable = null;
						List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> list = this.items;
						lock (list)
						{
							ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item item;
							if (this.itemsByIndex.TryGetValue(index, out item))
							{
								item.disposedBy.Add(reader);
								if (this.readers.IsSubsetOf(item.disposedBy))
								{
									this.itemsByIndex.Remove(index);
									disposable = item.value;
								}
							}
						}
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}

					// Token: 0x04003C03 RID: 15363
					private readonly int maxItems;

					// Token: 0x04003C04 RID: 15364
					private readonly List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> items;

					// Token: 0x04003C05 RID: 15365
					private readonly Dictionary<int, ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Item> itemsByIndex;

					// Token: 0x04003C06 RID: 15366
					private readonly HashSet<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader> readers;

					// Token: 0x04003C07 RID: 15367
					private readonly ManualResetEvent notEmpty;

					// Token: 0x04003C08 RID: 15368
					private readonly ManualResetEvent notFull;

					// Token: 0x04003C09 RID: 15369
					private int minIndex;

					// Token: 0x04003C0A RID: 15370
					private int maxIndex;

					// Token: 0x04003C0B RID: 15371
					private ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Writer writer;

					// Token: 0x0200101D RID: 4125
					private class Item
					{
						// Token: 0x06006BD2 RID: 27602 RVA: 0x00173148 File Offset: 0x00171348
						public Item(T value)
						{
							this.value = value;
							this.dequeuedBy = new List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader>();
							this.disposedBy = new List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader>();
						}

						// Token: 0x04003C0C RID: 15372
						public readonly T value;

						// Token: 0x04003C0D RID: 15373
						public readonly List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader> dequeuedBy;

						// Token: 0x04003C0E RID: 15374
						public readonly List<ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T>.Reader> disposedBy;
					}

					// Token: 0x0200101E RID: 4126
					public sealed class Writer
					{
						// Token: 0x06006BD3 RID: 27603 RVA: 0x0017316D File Offset: 0x0017136D
						public Writer(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T> source)
						{
							this.source = source;
						}

						// Token: 0x06006BD4 RID: 27604 RVA: 0x0017317C File Offset: 0x0017137C
						public void Enqueue(T item)
						{
							this.source.Enqueue(item);
						}

						// Token: 0x04003C0F RID: 15375
						private ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T> source;
					}

					// Token: 0x0200101F RID: 4127
					public sealed class Reader : IDisposable
					{
						// Token: 0x06006BD5 RID: 27605 RVA: 0x0017318A File Offset: 0x0017138A
						public Reader(ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T> source)
						{
							this.source = source;
							this.index = -1;
						}

						// Token: 0x06006BD6 RID: 27606 RVA: 0x001731A0 File Offset: 0x001713A0
						public T Dequeue(out int index)
						{
							this.index++;
							index = this.index;
							return this.source.Dequeue(this, index);
						}

						// Token: 0x06006BD7 RID: 27607 RVA: 0x001731C6 File Offset: 0x001713C6
						public void DisposeAt(int index)
						{
							this.source.DisposeItem(this, index);
						}

						// Token: 0x06006BD8 RID: 27608 RVA: 0x001731D5 File Offset: 0x001713D5
						public void Dispose()
						{
							if (this.index != -2)
							{
								this.index = -2;
								this.source.Dispose(this);
							}
						}

						// Token: 0x04003C10 RID: 15376
						private const int disposed = -2;

						// Token: 0x04003C11 RID: 15377
						private readonly ActionModule.TableAction.TeeFunctionValue.SingleWriterMultipleReaderQueueSource<T> source;

						// Token: 0x04003C12 RID: 15378
						private int index;
					}

					// Token: 0x02001020 RID: 4128
					public class ReaderAddedAfterEnumerationException : Exception
					{
						// Token: 0x06006BD9 RID: 27609 RVA: 0x001731F5 File Offset: 0x001713F5
						public ReaderAddedAfterEnumerationException()
							: base("A reader was added after enumeration had begun.")
						{
						}
					}

					// Token: 0x02001021 RID: 4129
					public class NoReadersException : Exception
					{
						// Token: 0x06006BDA RID: 27610 RVA: 0x00173202 File Offset: 0x00171402
						public NoReadersException()
							: base("An item was enqueued with no active readers.")
						{
						}
					}
				}
			}
		}
	}
}
