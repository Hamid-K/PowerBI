using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175F RID: 5983
	public static class LanguageLibrary
	{
		// Token: 0x060097FA RID: 38906 RVA: 0x001F687C File Offset: 0x001F4A7C
		public static IModule LibraryCachingModule(IModule module)
		{
			Module module2 = module as Module;
			if (module2 == null || module2.Imports.Length != 0)
			{
				return module;
			}
			return new LanguageLibrary.CachingLibraryModule(module2);
		}

		// Token: 0x060097FB RID: 38907 RVA: 0x001F68A4 File Offset: 0x001F4AA4
		public static RecordValue LinkLibrary(IEngineHost host, IList<IModule> modules)
		{
			return LanguageLibrary.LinkLibrary<LanguageLibrary.CacheKey>(host, modules, LanguageLibrary.cache, new LanguageLibrary.CacheKey(host, modules));
		}

		// Token: 0x060097FC RID: 38908 RVA: 0x001F68BC File Offset: 0x001F4ABC
		private static RecordValue LinkLibrary<K>(IEngineHost host, IList<IModule> modules, LruCache<K, RecordValue> cache, K key) where K : IEquatable<K>
		{
			LruCache<K, RecordValue> lruCache = cache;
			RecordValue recordValue;
			lock (lruCache)
			{
				cache.TryGetValue(key, out recordValue);
			}
			if (recordValue == null)
			{
				Module[] array = new Module[modules.Count + 1];
				array[0] = new LanguageLibrary.SharedModule();
				for (int i = 0; i < modules.Count; i++)
				{
					array[i + 1] = (Module)modules[i];
				}
				recordValue = Linker.Assemble(array, host, delegate(IError entry)
				{
					throw new InvalidOperationException(entry.Message);
				}).Function.Invoke().AsRecord;
				lruCache = cache;
				lock (lruCache)
				{
					RecordValue recordValue2;
					if (cache.TryGetValue(key, out recordValue2))
					{
						recordValue = recordValue2;
					}
					else
					{
						cache.Add(key, recordValue);
					}
				}
			}
			return recordValue;
		}

		// Token: 0x060097FD RID: 38909 RVA: 0x001F69B4 File Offset: 0x001F4BB4
		public static Value Evaluate(IExpression expression)
		{
			return LanguageLibrary.Evaluate(expression, Value.Null);
		}

		// Token: 0x060097FE RID: 38910 RVA: 0x001F69C1 File Offset: 0x001F4BC1
		private static Value Evaluate(IExpression expression, Value environment)
		{
			return LanguageLibrary.Evaluate(expression, environment, EmptyArray<Module>.Instance);
		}

		// Token: 0x060097FF RID: 38911 RVA: 0x001F69CF File Offset: 0x001F4BCF
		public static Value Evaluate(IExpression expression, Value environment, params Module[] modules)
		{
			return LanguageLibrary.Evaluate(new ExpressionDocumentSyntaxNode(expression), environment, modules, EngineHost.Empty);
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x001F69E3 File Offset: 0x001F4BE3
		public static Value Evaluate(string text, Value environment)
		{
			return LanguageLibrary.Evaluate(text, environment, EmptyArray<Module>.Instance);
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x001F69F1 File Offset: 0x001F4BF1
		public static Value Evaluate(string text, Value environment, params Module[] modules)
		{
			return LanguageLibrary.Evaluate(text, environment, EngineHost.Empty, modules);
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x001F6A00 File Offset: 0x001F4C00
		public static Value Evaluate(string text, Value environment, IEngineHost engineHost, params Module[] modules)
		{
			List<IError> errors = new List<IError>();
			Action<IError> action = delegate(IError entry)
			{
				errors.Add(entry);
			};
			IDocument document = Engine.Instance.Parse(text, action);
			if (errors.Count > 0)
			{
				throw LanguageLibrary.SourceErrorException(ValueException.ExpressionError.String, errors);
			}
			return LanguageLibrary.Evaluate(document, environment, modules, engineHost);
		}

		// Token: 0x06009803 RID: 38915 RVA: 0x001F6A63 File Offset: 0x001F4C63
		private static Value Evaluate(IDocument document, Value environment, Module[] modules, IEngineHost engineHost)
		{
			return LanguageLibrary.GetAssembly(document, environment, modules, engineHost).Function.Invoke();
		}

		// Token: 0x06009804 RID: 38916 RVA: 0x001F6A78 File Offset: 0x001F4C78
		private static Assembly GetAssembly(IDocument document, Value withRecord, Module[] modules, IEngineHost engineHost)
		{
			List<IError> errors = new List<IError>();
			Action<IError> action = delegate(IError entry)
			{
				errors.Add(entry);
			};
			if (document.Kind != DocumentKind.Expression)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Evaluate_EvaluatedTextCannotContainSections, Value.Null, null);
			}
			Module module = Engine.Instance.Compile(document, withRecord.IsNull ? RecordValue.Empty : withRecord.AsRecord, CompileOptions.Debug, action);
			if (errors.Count > 0)
			{
				throw LanguageLibrary.SourceErrorException(ValueException.ExpressionError.String, errors);
			}
			List<Module> list = new List<Module>(modules);
			if (!withRecord.IsNull && withRecord.AsRecord.Keys.Length > 0)
			{
				list.Add(new EnvironmentModule(withRecord.AsRecord));
			}
			list.Add(module);
			Assembly assembly = Linker.Assemble(list.ToArray(), engineHost, action);
			if (errors.Count > 0)
			{
				throw LanguageLibrary.SourceErrorException(ValueException.ExpressionError.String, errors);
			}
			return assembly;
		}

		// Token: 0x06009805 RID: 38917 RVA: 0x001F6B6F File Offset: 0x001F4D6F
		public static RecordValue GetSourceErrorAsValue(IError error)
		{
			return RecordValue.New(LanguageLibrary.sourceErrorValueKeys, new Value[]
			{
				LanguageLibrary.GetSourceLocationAsValue(error.Location),
				TextValue.New(error.Message)
			});
		}

		// Token: 0x06009806 RID: 38918 RVA: 0x001F6B9D File Offset: 0x001F4D9D
		public static RecordValue GetTextPositionAsValue(TextPosition position)
		{
			return RecordValue.New(LanguageLibrary.textPositionValueKeys, new Value[]
			{
				NumberValue.New(position.Row),
				NumberValue.New(position.Column)
			});
		}

		// Token: 0x06009807 RID: 38919 RVA: 0x001F6BCD File Offset: 0x001F4DCD
		public static RecordValue GetTextRangeAsValue(TextRange range)
		{
			return RecordValue.New(LanguageLibrary.textRangeValueKeys, new Value[]
			{
				LanguageLibrary.GetTextPositionAsValue(range.Start),
				LanguageLibrary.GetTextPositionAsValue(range.End)
			});
		}

		// Token: 0x06009808 RID: 38920 RVA: 0x001F6BFD File Offset: 0x001F4DFD
		public static RecordValue GetSourceLocationAsValue(SourceLocation location)
		{
			return RecordValue.New(LanguageLibrary.textSourceLocationValueKeys, new Value[]
			{
				LanguageLibrary.textSourceLocationKind,
				TextValue.Empty,
				LanguageLibrary.GetTextRangeAsValue(location.Range)
			});
		}

		// Token: 0x06009809 RID: 38921 RVA: 0x001F6C2D File Offset: 0x001F4E2D
		public static ValueException SourceErrorException(string reason, IList<IError> errors)
		{
			return ValueException.New(LanguageLibrary.SourceErrorExceptionRecord(reason, errors.FirstOrDefault<IError>(), errors), null);
		}

		// Token: 0x0600980A RID: 38922 RVA: 0x001F6C44 File Offset: 0x001F4E44
		private static RecordValue SourceErrorExceptionRecord(string reason, IError error, IList<IError> errorsToIncludeInDetail)
		{
			Value value;
			if (errorsToIncludeInDetail != null)
			{
				RecordValue[] array = new RecordValue[errorsToIncludeInDetail.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = LanguageLibrary.GetSourceErrorAsValue(errorsToIncludeInDetail[i]);
				}
				Value[] array2 = array;
				value = ListValue.New(array2);
			}
			else
			{
				value = ListValue.New(new Value[] { LanguageLibrary.GetSourceErrorAsValue(error) });
			}
			string text = reason;
			if (error != null)
			{
				TextRange range = error.Location.Range;
				text = string.Format(CultureInfo.CurrentCulture, "[{0},{1}-{2},{3}] {4}", new object[]
				{
					range.Start.Row + 1,
					range.Start.Column + 1,
					range.End.Row + 1,
					range.End.Column + 1,
					error.Message
				});
			}
			return ErrorRecord.New(TextValue.New(reason), TextValue.New(text), value);
		}

		// Token: 0x04005079 RID: 20601
		private const string textSourceLocationKindString = "Language.TextSourceLocation";

		// Token: 0x0400507A RID: 20602
		private static readonly Keys sourceErrorValueKeys = Keys.New("Location", "Text");

		// Token: 0x0400507B RID: 20603
		private static readonly Keys textPositionValueKeys = Keys.New("Line", "Column");

		// Token: 0x0400507C RID: 20604
		private static readonly Keys textRangeValueKeys = Keys.New("Start", "End");

		// Token: 0x0400507D RID: 20605
		private static readonly Keys textSourceLocationValueKeys = Keys.New("Kind", "Text", "Range");

		// Token: 0x0400507E RID: 20606
		private static readonly TextValue textSourceLocationKind = TextValue.New("Language.TextSourceLocation");

		// Token: 0x0400507F RID: 20607
		private static readonly LruCache<LanguageLibrary.CacheKey, RecordValue> cache = new LruCache<LanguageLibrary.CacheKey, RecordValue>(16, null);

		// Token: 0x02001760 RID: 5984
		private class SharedModule : Module
		{
			// Token: 0x17002767 RID: 10087
			// (get) Token: 0x0600980C RID: 38924 RVA: 0x0003389B File Offset: 0x00031A9B
			public override Keys ExportKeys
			{
				get
				{
					return Keys.Empty;
				}
			}

			// Token: 0x0600980D RID: 38925 RVA: 0x001F6DCE File Offset: 0x001F4FCE
			public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
			{
				return RecordValue.New(Linker.LinkedKeys, new Value[]
				{
					new ConstantFunctionValue(environment["Shared"]),
					RecordValue.Empty,
					RecordValue.Empty
				});
			}
		}

		// Token: 0x02001761 RID: 5985
		public static class List
		{
			// Token: 0x04005080 RID: 20608
			public static readonly FunctionValue Count = new LanguageLibrary.List.CountFunctionValue();

			// Token: 0x04005081 RID: 20609
			public static readonly FunctionValue Distinct = new LanguageLibrary.List.DistinctFunctionValue();

			// Token: 0x04005082 RID: 20610
			public static readonly FunctionValue FirstN = new LanguageLibrary.List.FirstNFunctionValue();

			// Token: 0x04005083 RID: 20611
			public static readonly FunctionValue IsEmpty = new LanguageLibrary.List.IsEmptyFunctionValue();

			// Token: 0x04005084 RID: 20612
			public static readonly FunctionValue LastN = new LanguageLibrary.List.LastNFunctionValue();

			// Token: 0x04005085 RID: 20613
			public static readonly FunctionValue Select = new LanguageLibrary.List.SelectFunctionValue();

			// Token: 0x04005086 RID: 20614
			public static readonly FunctionValue Skip = new LanguageLibrary.List.SkipFunctionValue();

			// Token: 0x04005087 RID: 20615
			public static readonly FunctionValue Sort = new LanguageLibrary.List.SortFunctionValue();

			// Token: 0x04005088 RID: 20616
			public static readonly FunctionValue Transform = new LanguageLibrary.List.TransformFunctionValue();

			// Token: 0x04005089 RID: 20617
			public static readonly FunctionValue TransformMany = new LanguageLibrary.List.TransformManyFunctionValue();

			// Token: 0x0400508A RID: 20618
			public static readonly FunctionValue Take = new LanguageLibrary.List.TakeFunctionValue();

			// Token: 0x02001762 RID: 5986
			private sealed class FirstNFunctionValue : NativeFunctionValue2<Value, ListValue, Value>, IAccumulableFunction, IAccumulableChainingFunction
			{
				// Token: 0x06009810 RID: 38928 RVA: 0x001F6E7F File Offset: 0x001F507F
				public FirstNFunctionValue()
					: base(TypeValue.Any, "list", TypeValue.List, "countOrCondition", TypeValue.Any)
				{
				}

				// Token: 0x17002768 RID: 10088
				// (get) Token: 0x06009811 RID: 38929 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x17002769 RID: 10089
				// (get) Token: 0x06009812 RID: 38930 RVA: 0x001F6EA0 File Offset: 0x001F50A0
				private IAccumulableFunction TakeAccumulableFunction
				{
					get
					{
						if (this.takeAccumulableFunction == null)
						{
							LanguageLibrary.List.Take.TryGetAccumulableFunction(out this.takeAccumulableFunction);
						}
						return this.takeAccumulableFunction;
					}
				}

				// Token: 0x1700276A RID: 10090
				// (get) Token: 0x06009813 RID: 38931 RVA: 0x001F6EC1 File Offset: 0x001F50C1
				private IAccumulableChainingFunction TakeAccumulableChainingFunction
				{
					get
					{
						if (this.takeAccumulableChainingFunction == null)
						{
							LanguageLibrary.List.Take.TryGetAccumulableChainingFunction(out this.takeAccumulableChainingFunction);
						}
						return this.takeAccumulableChainingFunction;
					}
				}

				// Token: 0x06009814 RID: 38932 RVA: 0x001F6EE4 File Offset: 0x001F50E4
				public override Value TypedInvoke(ListValue list, Value countOrCondition)
				{
					if (countOrCondition.IsNumber)
					{
						return LanguageLibrary.List.Take.Invoke(list, countOrCondition).AsList;
					}
					if (countOrCondition.IsFunction)
					{
						return Library.List.TakeWhile.Invoke(list, countOrCondition).AsList;
					}
					throw ValueException.NewExpressionError<Message0>(Strings.CountOrCondition_CountOrConditionValueExpectedError, list, null);
				}

				// Token: 0x06009815 RID: 38933 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x06009816 RID: 38934 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06009817 RID: 38935 RVA: 0x001F6F34 File Offset: 0x001F5134
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					Value value = arguments["countOrCondition"];
					return this.TakeAccumulableFunction.CreateAccumulable(RecordValue.New(LanguageLibrary.List.FirstNFunctionValue.takeNonEnumeratingParameters, new Value[] { value.AsNumber }));
				}

				// Token: 0x06009818 RID: 38936 RVA: 0x001F6F74 File Offset: 0x001F5174
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					Value value = arguments["countOrCondition"];
					return this.TakeAccumulableChainingFunction.CreateAccumulable(RecordValue.New(LanguageLibrary.List.FirstNFunctionValue.takeNonEnumeratingParameters, new Value[] { value.AsNumber }), accumulable);
				}

				// Token: 0x0400508B RID: 20619
				private const string enumerableParameter = "list";

				// Token: 0x0400508C RID: 20620
				private static readonly Keys takeNonEnumeratingParameters = Keys.New("count");

				// Token: 0x0400508D RID: 20621
				private IAccumulableFunction takeAccumulableFunction;

				// Token: 0x0400508E RID: 20622
				private IAccumulableChainingFunction takeAccumulableChainingFunction;
			}

			// Token: 0x02001763 RID: 5987
			private sealed class LastNFunctionValue : NativeFunctionValue2<Value, ListValue, Value>
			{
				// Token: 0x0600981A RID: 38938 RVA: 0x001F6FC3 File Offset: 0x001F51C3
				public LastNFunctionValue()
					: base(TypeValue.Any, 1, "list", TypeValue.List, "countOrCondition", TypeValue.Any)
				{
				}

				// Token: 0x0600981B RID: 38939 RVA: 0x001F6FE8 File Offset: 0x001F51E8
				public override Value TypedInvoke(ListValue list, Value countOrCondition)
				{
					if (countOrCondition.IsNumber)
					{
						long asInteger = countOrCondition.AsNumber.AsInteger64;
						if (asInteger < 0L || asInteger > 2147483647L)
						{
							throw ValueException.ArgumentOutOfRange("count", countOrCondition);
						}
						if (asInteger == 0L)
						{
							return ListValue.Empty;
						}
						IValueReference[] array = new IValueReference[(int)asInteger];
						int num = 0;
						bool flag = false;
						foreach (IValueReference valueReference in list)
						{
							array[num++] = valueReference;
							if (num == array.Length)
							{
								flag = true;
								num = 0;
							}
						}
						IValueReference[] array2;
						if (!flag)
						{
							array2 = new IValueReference[num];
							Array.Copy(array, array2, num);
						}
						else
						{
							array2 = new IValueReference[array.Length];
							Array.Copy(array, num, array2, 0, array.Length - num);
							Array.Copy(array, 0, array2, array.Length - num, num);
						}
						return ListValue.New(array2);
					}
					else
					{
						if (countOrCondition.IsFunction)
						{
							if (!list.IsBuffered)
							{
								list = Library.List.Buffer.Invoke(list).AsList;
							}
							IValueReference[] array3 = new IValueReference[list.Count];
							int i = list.Count;
							while (i > 0)
							{
								i--;
								Value value = list[i];
								if (!countOrCondition.AsFunction.Invoke(value).AsBoolean)
								{
									i++;
									break;
								}
								array3[i] = value;
							}
							if (i > 0)
							{
								IValueReference[] array4 = new IValueReference[list.Count - i];
								Array.Copy(array3, i, array4, 0, list.Count - i);
								array3 = array4;
							}
							return ListValue.New(array3);
						}
						throw ValueException.NewExpressionError<Message0>(Strings.CountOrCondition_CountOrConditionValueExpectedError, list, null);
					}
				}

				// Token: 0x0600981C RID: 38940 RVA: 0x001F7184 File Offset: 0x001F5384
				private Value Last(ListValue list)
				{
					IValueReference valueReference = null;
					using (IEnumerator<IValueReference> enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							valueReference = enumerator.Current;
						}
					}
					if (valueReference == null)
					{
						throw ValueException.InsufficientElements(list);
					}
					return valueReference.Value;
				}
			}

			// Token: 0x02001764 RID: 5988
			private class SkipFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>
			{
				// Token: 0x0600981D RID: 38941 RVA: 0x001F71D8 File Offset: 0x001F53D8
				public SkipFunctionValue()
					: base(TypeValue.List, 1, "list", TypeValue.List, "countOrCondition", TypeValue.Any)
				{
				}

				// Token: 0x0600981E RID: 38942 RVA: 0x001F71FC File Offset: 0x001F53FC
				public override ListValue TypedInvoke(ListValue list, Value countOrCondition)
				{
					if (countOrCondition.IsNull)
					{
						countOrCondition = NumberValue.One;
					}
					if (countOrCondition.IsNumber)
					{
						NumberValue asNumber = countOrCondition.AsNumber;
						if (asNumber.LessThan(NumberValue.Zero))
						{
							throw ValueException.ArgumentOutOfRange("countOrCondition", asNumber);
						}
						FoldableListValue foldableListValue = list as FoldableListValue;
						if (foldableListValue != null)
						{
							return foldableListValue.Skip(new RowCount(asNumber.AsInteger64));
						}
						return new LanguageLibrary.List.SkipFunctionValue.SkipListValue(list, asNumber.AsInteger32);
					}
					else
					{
						if (countOrCondition.IsFunction)
						{
							return Library.List.SkipWhile.Invoke(list, countOrCondition).AsList;
						}
						throw ValueException.NewExpressionError<Message0>(Strings.CountOrCondition_CountOrConditionValueExpectedError, list, null);
					}
				}

				// Token: 0x02001765 RID: 5989
				private class SkipListValue : StreamedListValue
				{
					// Token: 0x0600981F RID: 38943 RVA: 0x001F728F File Offset: 0x001F548F
					public SkipListValue(ListValue list, int count)
					{
						this.list = list;
						this.count = ((count < 0) ? 0 : count);
					}

					// Token: 0x1700276B RID: 10091
					// (get) Token: 0x06009820 RID: 38944 RVA: 0x001F72AC File Offset: 0x001F54AC
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x06009821 RID: 38945 RVA: 0x001F72B9 File Offset: 0x001F54B9
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.SkipFunctionValue.SkipListValue.SkipEnumerator(this.list.GetEnumerator(), this.count);
					}

					// Token: 0x0400508F RID: 20623
					private ListValue list;

					// Token: 0x04005090 RID: 20624
					private int count;

					// Token: 0x02001766 RID: 5990
					private class SkipEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06009822 RID: 38946 RVA: 0x001F72D1 File Offset: 0x001F54D1
						public SkipEnumerator(IEnumerator<IValueReference> enumerator, int count)
						{
							this.enumerator = enumerator;
							this.count = count;
						}

						// Token: 0x1700276C RID: 10092
						// (get) Token: 0x06009823 RID: 38947 RVA: 0x001F72E7 File Offset: 0x001F54E7
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x1700276D RID: 10093
						// (get) Token: 0x06009824 RID: 38948 RVA: 0x001F72F4 File Offset: 0x001F54F4
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06009825 RID: 38949 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06009826 RID: 38950 RVA: 0x001F72FC File Offset: 0x001F54FC
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06009827 RID: 38951 RVA: 0x001F7309 File Offset: 0x001F5509
						public bool MoveNext()
						{
							while (this.count > 0)
							{
								if (!this.enumerator.MoveNext())
								{
									return false;
								}
								this.count--;
							}
							return this.enumerator.MoveNext();
						}

						// Token: 0x04005091 RID: 20625
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04005092 RID: 20626
						private int count;
					}
				}
			}

			// Token: 0x02001767 RID: 5991
			private class CountFunctionValue : NativeFunctionValue1<NumberValue, ListValue>, IAccumulableFunction
			{
				// Token: 0x06009828 RID: 38952 RVA: 0x001CC3C5 File Offset: 0x001CA5C5
				public CountFunctionValue()
					: base(TypeValue.Number, "list", TypeValue.List)
				{
				}

				// Token: 0x1700276E RID: 10094
				// (get) Token: 0x06009829 RID: 38953 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x0600982A RID: 38954 RVA: 0x001F733E File Offset: 0x001F553E
				public override NumberValue TypedInvoke(ListValue list)
				{
					return NumberValue.New(list.LargeCount);
				}

				// Token: 0x0600982B RID: 38955 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x0600982C RID: 38956 RVA: 0x001F734B File Offset: 0x001F554B
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new LanguageLibrary.List.CountFunctionValue.CountAccumulable();
				}

				// Token: 0x04005093 RID: 20627
				private const string enumerableParameter = "list";

				// Token: 0x02001768 RID: 5992
				private sealed class CountAccumulable : IAccumulable
				{
					// Token: 0x0600982D RID: 38957 RVA: 0x001F7352 File Offset: 0x001F5552
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.CountFunctionValue.CountAccumulable.CountAccumulator();
					}

					// Token: 0x02001769 RID: 5993
					private sealed class CountAccumulator : IAccumulator
					{
						// Token: 0x1700276F RID: 10095
						// (get) Token: 0x0600982F RID: 38959 RVA: 0x001F7359 File Offset: 0x001F5559
						public IValueReference Current
						{
							get
							{
								return NumberValue.New(this.count);
							}
						}

						// Token: 0x06009830 RID: 38960 RVA: 0x001F7366 File Offset: 0x001F5566
						public void AccumulateNext(IValueReference next)
						{
							this.count += 1L;
						}

						// Token: 0x04005094 RID: 20628
						private long count;
					}
				}
			}

			// Token: 0x0200176A RID: 5994
			private class DistinctFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>, IAccumulableChainingFunction
			{
				// Token: 0x06009832 RID: 38962 RVA: 0x001CAADD File Offset: 0x001C8CDD
				public DistinctFunctionValue()
					: base(TypeValue.List, 1, "list", TypeValue.List, "equationCriteria", TypeValue.Any)
				{
				}

				// Token: 0x17002770 RID: 10096
				// (get) Token: 0x06009833 RID: 38963 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06009834 RID: 38964 RVA: 0x001F7378 File Offset: 0x001F5578
				public override ListValue TypedInvoke(ListValue list, Value equationCriteria)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null && equationCriteria.IsNull)
					{
						return foldableListValue.Distinct();
					}
					IEqualityComparer<Value> equalityComparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					return new LanguageLibrary.List.DistinctFunctionValue.DistinctListValue(list, equalityComparer);
				}

				// Token: 0x06009835 RID: 38965 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06009836 RID: 38966 RVA: 0x001F73AD File Offset: 0x001F55AD
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new LanguageLibrary.List.DistinctFunctionValue.DistinctAccumulable(accumulable, arguments["equationCriteria"]);
				}

				// Token: 0x04005095 RID: 20629
				private const string enumerableParameter = "list";

				// Token: 0x0200176B RID: 5995
				private class DistinctListValue : StreamedListValue
				{
					// Token: 0x06009837 RID: 38967 RVA: 0x001F73C0 File Offset: 0x001F55C0
					public DistinctListValue(ListValue list, IEqualityComparer<Value> comparer)
					{
						this.list = list;
						this.comparer = comparer;
					}

					// Token: 0x17002771 RID: 10097
					// (get) Token: 0x06009838 RID: 38968 RVA: 0x001F73D6 File Offset: 0x001F55D6
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x06009839 RID: 38969 RVA: 0x001F73E3 File Offset: 0x001F55E3
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.DistinctFunctionValue.DistinctListValue.DistinctEnumerator(this.list.GetEnumerator(), this.comparer);
					}

					// Token: 0x04005096 RID: 20630
					private ListValue list;

					// Token: 0x04005097 RID: 20631
					private IEqualityComparer<Value> comparer;

					// Token: 0x0200176C RID: 5996
					private class DistinctEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600983A RID: 38970 RVA: 0x001F73FB File Offset: 0x001F55FB
						public DistinctEnumerator(IEnumerator<IValueReference> enumerator, IEqualityComparer<Value> comparer)
						{
							this.enumerator = enumerator;
							this.hashSet = new HashSet<Value>(comparer);
						}

						// Token: 0x17002772 RID: 10098
						// (get) Token: 0x0600983B RID: 38971 RVA: 0x001F7416 File Offset: 0x001F5616
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002773 RID: 10099
						// (get) Token: 0x0600983C RID: 38972 RVA: 0x001F7423 File Offset: 0x001F5623
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600983D RID: 38973 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x0600983E RID: 38974 RVA: 0x001F742B File Offset: 0x001F562B
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x0600983F RID: 38975 RVA: 0x001F7438 File Offset: 0x001F5638
						public bool MoveNext()
						{
							while (this.enumerator.MoveNext())
							{
								if (this.hashSet.Add(this.enumerator.Current.Value))
								{
									return true;
								}
							}
							return false;
						}

						// Token: 0x04005098 RID: 20632
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x04005099 RID: 20633
						private HashSet<Value> hashSet;
					}
				}

				// Token: 0x0200176D RID: 5997
				private sealed class DistinctAccumulable : IAccumulable
				{
					// Token: 0x06009840 RID: 38976 RVA: 0x001F7469 File Offset: 0x001F5669
					public DistinctAccumulable(IAccumulable accumulable, Value equationCriteria)
					{
						this.accumulable = accumulable;
						this.comparer = Library.ListEquationCriteria.CreateEqualityComparer(equationCriteria, true);
					}

					// Token: 0x06009841 RID: 38977 RVA: 0x001F7485 File Offset: 0x001F5685
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.DistinctFunctionValue.DistinctAccumulable.DistinctAccumulator(this);
					}

					// Token: 0x0400509A RID: 20634
					private readonly IAccumulable accumulable;

					// Token: 0x0400509B RID: 20635
					private readonly IEqualityComparer<Value> comparer;

					// Token: 0x0200176E RID: 5998
					private sealed class DistinctAccumulator : SelectingAccumulator
					{
						// Token: 0x06009842 RID: 38978 RVA: 0x001F748D File Offset: 0x001F568D
						public DistinctAccumulator(LanguageLibrary.List.DistinctFunctionValue.DistinctAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.hashSet = new HashSet<Value>(accumulable.comparer);
						}

						// Token: 0x06009843 RID: 38979 RVA: 0x001F74B1 File Offset: 0x001F56B1
						protected override bool Select(IValueReference valueReference)
						{
							return this.hashSet.Add(valueReference.Value);
						}

						// Token: 0x0400509C RID: 20636
						private readonly HashSet<Value> hashSet;
					}
				}
			}

			// Token: 0x0200176F RID: 5999
			private class IsEmptyFunctionValue : NativeFunctionValue1<LogicalValue, ListValue>
			{
				// Token: 0x06009844 RID: 38980 RVA: 0x001C82D7 File Offset: 0x001C64D7
				public IsEmptyFunctionValue()
					: base(TypeValue.Logical, "list", TypeValue.List)
				{
				}

				// Token: 0x06009845 RID: 38981 RVA: 0x001F74C4 File Offset: 0x001F56C4
				public override LogicalValue TypedInvoke(ListValue list)
				{
					return LogicalValue.New(list.IsEmpty);
				}
			}

			// Token: 0x02001770 RID: 6000
			private class TransformFunctionValue : NativeFunctionValue2<ListValue, ListValue, FunctionValue>, IAccumulableChainingFunction
			{
				// Token: 0x06009846 RID: 38982 RVA: 0x001C7B8C File Offset: 0x001C5D8C
				public TransformFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "transform", TypeValue.Function)
				{
				}

				// Token: 0x17002774 RID: 10100
				// (get) Token: 0x06009847 RID: 38983 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06009848 RID: 38984 RVA: 0x001F74D4 File Offset: 0x001F56D4
				public override ListValue TypedInvoke(ListValue list, FunctionValue transform)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null)
					{
						return foldableListValue.Transform(transform);
					}
					return new LanguageLibrary.List.TransformFunctionValue.TransformListValue(list, transform);
				}

				// Token: 0x06009849 RID: 38985 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x0600984A RID: 38986 RVA: 0x001F74FA File Offset: 0x001F56FA
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new LanguageLibrary.List.TransformFunctionValue.TransformAccumulable(accumulable, arguments["transform"].AsFunction);
				}

				// Token: 0x0400509D RID: 20637
				private const string enumerableParameter = "list";

				// Token: 0x02001771 RID: 6001
				private class TransformListValue : StreamedListValue
				{
					// Token: 0x0600984B RID: 38987 RVA: 0x001F7512 File Offset: 0x001F5712
					public TransformListValue(ListValue list, FunctionValue transform)
					{
						this.list = list;
						this.transform = transform;
					}

					// Token: 0x0600984C RID: 38988 RVA: 0x001F7528 File Offset: 0x001F5728
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.TransformFunctionValue.TransformListValue.TransformEnumerator(this.list.GetEnumerator(), this.transform);
					}

					// Token: 0x0400509E RID: 20638
					private ListValue list;

					// Token: 0x0400509F RID: 20639
					private FunctionValue transform;

					// Token: 0x02001772 RID: 6002
					private class TransformEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600984D RID: 38989 RVA: 0x001F7540 File Offset: 0x001F5740
						public TransformEnumerator(IEnumerator<IValueReference> source, FunctionValue transform)
						{
							this.source = source;
							this.transform = transform;
						}

						// Token: 0x17002775 RID: 10101
						// (get) Token: 0x0600984E RID: 38990 RVA: 0x001F7556 File Offset: 0x001F5756
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									this.current = new TransformValueReference(this.source.Current, this.transform);
								}
								return this.current;
							}
						}

						// Token: 0x17002776 RID: 10102
						// (get) Token: 0x0600984F RID: 38991 RVA: 0x001F7582 File Offset: 0x001F5782
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06009850 RID: 38992 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06009851 RID: 38993 RVA: 0x001F758A File Offset: 0x001F578A
						public void Dispose()
						{
							this.source.Dispose();
						}

						// Token: 0x06009852 RID: 38994 RVA: 0x001F7597 File Offset: 0x001F5797
						public bool MoveNext()
						{
							this.current = null;
							return this.source.MoveNext();
						}

						// Token: 0x040050A0 RID: 20640
						private IEnumerator<IValueReference> source;

						// Token: 0x040050A1 RID: 20641
						private FunctionValue transform;

						// Token: 0x040050A2 RID: 20642
						private IValueReference current;
					}
				}

				// Token: 0x02001773 RID: 6003
				private sealed class TransformAccumulable : IAccumulable
				{
					// Token: 0x06009853 RID: 38995 RVA: 0x001F75AB File Offset: 0x001F57AB
					public TransformAccumulable(IAccumulable accumulable, FunctionValue transform)
					{
						this.accumulable = accumulable;
						this.transform = transform;
					}

					// Token: 0x06009854 RID: 38996 RVA: 0x001F75C1 File Offset: 0x001F57C1
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.TransformFunctionValue.TransformAccumulable.TransformAccumulator(this);
					}

					// Token: 0x040050A3 RID: 20643
					private readonly IAccumulable accumulable;

					// Token: 0x040050A4 RID: 20644
					private readonly FunctionValue transform;

					// Token: 0x02001774 RID: 6004
					private sealed class TransformAccumulator : TransformingAccumulator
					{
						// Token: 0x06009855 RID: 38997 RVA: 0x001F75C9 File Offset: 0x001F57C9
						public TransformAccumulator(LanguageLibrary.List.TransformFunctionValue.TransformAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.transform = accumulable.transform;
						}

						// Token: 0x06009856 RID: 38998 RVA: 0x001F75E8 File Offset: 0x001F57E8
						protected override IValueReference Transform(IValueReference valueReference)
						{
							return new TransformValueReference(valueReference, this.transform);
						}

						// Token: 0x040050A5 RID: 20645
						private readonly FunctionValue transform;
					}
				}
			}

			// Token: 0x02001775 RID: 6005
			private class TransformManyFunctionValue : NativeFunctionValue3<ListValue, ListValue, FunctionValue, FunctionValue>, IAccumulableChainingFunction
			{
				// Token: 0x06009857 RID: 38999 RVA: 0x001F75F6 File Offset: 0x001F57F6
				public TransformManyFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "collectionTransform", TypeValue.Function, "resultTransform", TypeValue.Function)
				{
				}

				// Token: 0x17002777 RID: 10103
				// (get) Token: 0x06009858 RID: 39000 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06009859 RID: 39001 RVA: 0x001F7621 File Offset: 0x001F5821
				public override ListValue TypedInvoke(ListValue list, FunctionValue collectionTransform, FunctionValue resultTransform)
				{
					return new LanguageLibrary.List.TransformManyFunctionValue.TransformManyListValue(list, collectionTransform, resultTransform);
				}

				// Token: 0x0600985A RID: 39002 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x0600985B RID: 39003 RVA: 0x001F762B File Offset: 0x001F582B
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new LanguageLibrary.List.TransformManyFunctionValue.TransformManyAccumulable(accumulable, arguments["collectionTransform"].AsFunction, arguments["resultTransform"].AsFunction);
				}

				// Token: 0x040050A6 RID: 20646
				private const string enumerableParameter = "list";

				// Token: 0x02001776 RID: 6006
				private class TransformManyListValue : StreamedListValue
				{
					// Token: 0x0600985C RID: 39004 RVA: 0x001F7653 File Offset: 0x001F5853
					public TransformManyListValue(ListValue list, FunctionValue collectionTransform, FunctionValue resultTransform)
					{
						this.list = list;
						this.collectionTransform = collectionTransform;
						this.resultTransform = resultTransform;
					}

					// Token: 0x0600985D RID: 39005 RVA: 0x001F7670 File Offset: 0x001F5870
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.TransformManyFunctionValue.TransformManyListValue.TransformManyEnumerator(this.list.GetEnumerator(), this.collectionTransform, this.resultTransform);
					}

					// Token: 0x040050A7 RID: 20647
					private ListValue list;

					// Token: 0x040050A8 RID: 20648
					private FunctionValue collectionTransform;

					// Token: 0x040050A9 RID: 20649
					private FunctionValue resultTransform;

					// Token: 0x02001777 RID: 6007
					private class TransformManyEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600985E RID: 39006 RVA: 0x001F768E File Offset: 0x001F588E
						public TransformManyEnumerator(IEnumerator<IValueReference> outer, FunctionValue collectionTransform, FunctionValue resultTransform)
						{
							this.outer = outer;
							this.inner = ListValue.Empty.GetEnumerator();
							this.collectionTransform = collectionTransform;
							this.resultTransform = resultTransform;
						}

						// Token: 0x17002778 RID: 10104
						// (get) Token: 0x0600985F RID: 39007 RVA: 0x001F76BB File Offset: 0x001F58BB
						public IValueReference Current
						{
							get
							{
								if (this.current == null)
								{
									this.current = new LanguageLibrary.List.TransformManyFunctionValue.TransformManyValueReference(this.outer.Current, this.inner.Current, this.resultTransform);
								}
								return this.current;
							}
						}

						// Token: 0x17002779 RID: 10105
						// (get) Token: 0x06009860 RID: 39008 RVA: 0x001F76F2 File Offset: 0x001F58F2
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06009861 RID: 39009 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06009862 RID: 39010 RVA: 0x001F76FA File Offset: 0x001F58FA
						public void Dispose()
						{
							this.outer.Dispose();
							this.inner.Dispose();
						}

						// Token: 0x06009863 RID: 39011 RVA: 0x001F7714 File Offset: 0x001F5914
						public bool MoveNext()
						{
							this.current = null;
							while (!this.inner.MoveNext())
							{
								if (!this.outer.MoveNext())
								{
									return false;
								}
								this.inner.Dispose();
								this.inner = this.collectionTransform.Invoke(this.outer.Current.Value).AsList.GetEnumerator();
							}
							return true;
						}

						// Token: 0x040050AA RID: 20650
						private IEnumerator<IValueReference> outer;

						// Token: 0x040050AB RID: 20651
						private IEnumerator<IValueReference> inner;

						// Token: 0x040050AC RID: 20652
						private FunctionValue collectionTransform;

						// Token: 0x040050AD RID: 20653
						private FunctionValue resultTransform;

						// Token: 0x040050AE RID: 20654
						private IValueReference current;
					}
				}

				// Token: 0x02001778 RID: 6008
				private sealed class TransformManyAccumulable : IAccumulable
				{
					// Token: 0x06009864 RID: 39012 RVA: 0x001F777D File Offset: 0x001F597D
					public TransformManyAccumulable(IAccumulable accumulable, FunctionValue collectionTransform, FunctionValue resultTransform)
					{
						this.accumulable = accumulable;
						this.collectionTransform = collectionTransform;
						this.resultTransform = resultTransform;
					}

					// Token: 0x06009865 RID: 39013 RVA: 0x001F779A File Offset: 0x001F599A
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.TransformManyFunctionValue.TransformManyAccumulable.TransformManyAccumulator(this);
					}

					// Token: 0x040050AF RID: 20655
					private readonly IAccumulable accumulable;

					// Token: 0x040050B0 RID: 20656
					private readonly FunctionValue collectionTransform;

					// Token: 0x040050B1 RID: 20657
					private readonly FunctionValue resultTransform;

					// Token: 0x02001779 RID: 6009
					private sealed class TransformManyAccumulator : ChainedAccumulator
					{
						// Token: 0x06009866 RID: 39014 RVA: 0x001F77A2 File Offset: 0x001F59A2
						public TransformManyAccumulator(LanguageLibrary.List.TransformManyFunctionValue.TransformManyAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.collectionTransform = accumulable.collectionTransform;
							this.resultTransform = accumulable.resultTransform;
						}

						// Token: 0x06009867 RID: 39015 RVA: 0x001F77D0 File Offset: 0x001F59D0
						public override void AccumulateNext(IValueReference next)
						{
							foreach (IValueReference valueReference in this.collectionTransform.Invoke(next.Value).AsList)
							{
								this.accumulator.AccumulateNext(new LanguageLibrary.List.TransformManyFunctionValue.TransformManyValueReference(next, valueReference, this.resultTransform));
							}
						}

						// Token: 0x040050B2 RID: 20658
						private readonly FunctionValue collectionTransform;

						// Token: 0x040050B3 RID: 20659
						private readonly FunctionValue resultTransform;
					}
				}

				// Token: 0x0200177A RID: 6010
				private class TransformManyValueReference : IValueReference
				{
					// Token: 0x06009868 RID: 39016 RVA: 0x001F7840 File Offset: 0x001F5A40
					public TransformManyValueReference(IValueReference outer, IValueReference inner, FunctionValue transform)
					{
						this.outer = outer;
						this.inner = inner;
						this.transform = transform;
					}

					// Token: 0x1700277A RID: 10106
					// (get) Token: 0x06009869 RID: 39017 RVA: 0x001F785D File Offset: 0x001F5A5D
					public bool Evaluated
					{
						get
						{
							return this.value != null;
						}
					}

					// Token: 0x1700277B RID: 10107
					// (get) Token: 0x0600986A RID: 39018 RVA: 0x001F7868 File Offset: 0x001F5A68
					public Value Value
					{
						get
						{
							if (this.value == null)
							{
								this.value = this.transform.Invoke(this.outer.Value, this.inner.Value);
								this.transform = null;
								this.outer = null;
								this.inner = null;
							}
							return this.value;
						}
					}

					// Token: 0x040050B4 RID: 20660
					private IValueReference outer;

					// Token: 0x040050B5 RID: 20661
					private IValueReference inner;

					// Token: 0x040050B6 RID: 20662
					private FunctionValue transform;

					// Token: 0x040050B7 RID: 20663
					private Value value;
				}
			}

			// Token: 0x0200177B RID: 6011
			private class TakeFunctionValue : NativeFunctionValue2<ListValue, ListValue, NumberValue>, IAccumulableFunction, IAccumulableChainingFunction
			{
				// Token: 0x0600986B RID: 39019 RVA: 0x001C83B8 File Offset: 0x001C65B8
				public TakeFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "count", TypeValue.Number)
				{
				}

				// Token: 0x1700277C RID: 10108
				// (get) Token: 0x0600986C RID: 39020 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x0600986D RID: 39021 RVA: 0x001F78C0 File Offset: 0x001F5AC0
				public override ListValue TypedInvoke(ListValue list, NumberValue count)
				{
					if (count.LessThan(NumberValue.Zero))
					{
						throw ValueException.ArgumentOutOfRange("count", count);
					}
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null)
					{
						return foldableListValue.Take(new RowCount(count.AsInteger64));
					}
					return new LanguageLibrary.List.TakeFunctionValue.TakeListValue(list, count.AsNumber.AsInteger32);
				}

				// Token: 0x0600986E RID: 39022 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableFunction(out IAccumulableFunction accumulableFunction)
				{
					accumulableFunction = this;
					return true;
				}

				// Token: 0x0600986F RID: 39023 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06009870 RID: 39024 RVA: 0x001F7913 File Offset: 0x001F5B13
				public IAccumulable CreateAccumulable(RecordValue arguments)
				{
					return new LanguageLibrary.List.TakeFunctionValue.TakeAccumulable(arguments["count"].AsInteger32);
				}

				// Token: 0x06009871 RID: 39025 RVA: 0x001F792A File Offset: 0x001F5B2A
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new LanguageLibrary.List.TakeFunctionValue.ChainingTakeAccumulable(accumulable, arguments["count"].AsInteger32);
				}

				// Token: 0x040050B8 RID: 20664
				private const string enumerableParameter = "list";

				// Token: 0x0200177C RID: 6012
				private class TakeListValue : StreamedListValue
				{
					// Token: 0x06009872 RID: 39026 RVA: 0x001F7942 File Offset: 0x001F5B42
					public TakeListValue(ListValue list, int count)
					{
						this.list = list;
						this.count = count;
					}

					// Token: 0x1700277D RID: 10109
					// (get) Token: 0x06009873 RID: 39027 RVA: 0x001F7958 File Offset: 0x001F5B58
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x06009874 RID: 39028 RVA: 0x001F7965 File Offset: 0x001F5B65
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.TakeFunctionValue.TakeListValue.TakeEnumerator(this.list.GetEnumerator(), this.count);
					}

					// Token: 0x040050B9 RID: 20665
					private ListValue list;

					// Token: 0x040050BA RID: 20666
					private int count;

					// Token: 0x0200177D RID: 6013
					private class TakeEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x06009875 RID: 39029 RVA: 0x001F797D File Offset: 0x001F5B7D
						public TakeEnumerator(IEnumerator<IValueReference> enumerator, int count)
						{
							this.enumerator = enumerator;
							this.count = count;
						}

						// Token: 0x1700277E RID: 10110
						// (get) Token: 0x06009876 RID: 39030 RVA: 0x001F7993 File Offset: 0x001F5B93
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x1700277F RID: 10111
						// (get) Token: 0x06009877 RID: 39031 RVA: 0x001F79A0 File Offset: 0x001F5BA0
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x06009878 RID: 39032 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x06009879 RID: 39033 RVA: 0x001F79A8 File Offset: 0x001F5BA8
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x0600987A RID: 39034 RVA: 0x001F79B5 File Offset: 0x001F5BB5
						public bool MoveNext()
						{
							if (this.count > 0)
							{
								this.count--;
								return this.enumerator.MoveNext();
							}
							return false;
						}

						// Token: 0x040050BB RID: 20667
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x040050BC RID: 20668
						private int count;
					}
				}

				// Token: 0x0200177E RID: 6014
				private sealed class TakeAccumulable : IAccumulable
				{
					// Token: 0x0600987B RID: 39035 RVA: 0x001F79DB File Offset: 0x001F5BDB
					public TakeAccumulable(int count)
					{
						this.count = count;
					}

					// Token: 0x0600987C RID: 39036 RVA: 0x001F79EA File Offset: 0x001F5BEA
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.TakeFunctionValue.TakeAccumulable.TakeAccumulator(this);
					}

					// Token: 0x040050BD RID: 20669
					private readonly int count;

					// Token: 0x0200177F RID: 6015
					private sealed class TakeAccumulator : IAccumulator
					{
						// Token: 0x0600987D RID: 39037 RVA: 0x001F79F2 File Offset: 0x001F5BF2
						public TakeAccumulator(LanguageLibrary.List.TakeFunctionValue.TakeAccumulable accumulable)
						{
							this.count = accumulable.count;
							this.buffer = new List<IValueReference>(this.count);
						}

						// Token: 0x17002780 RID: 10112
						// (get) Token: 0x0600987E RID: 39038 RVA: 0x001F7A17 File Offset: 0x001F5C17
						public IValueReference Current
						{
							get
							{
								if (this.buffer.Count < this.count)
								{
									this.copyOnAccumulateNext = true;
								}
								return ListValue.New(this.buffer);
							}
						}

						// Token: 0x0600987F RID: 39039 RVA: 0x001F7A40 File Offset: 0x001F5C40
						public void AccumulateNext(IValueReference next)
						{
							if (this.buffer.Count < this.count)
							{
								if (this.copyOnAccumulateNext)
								{
									this.buffer = new List<IValueReference>(this.buffer);
									this.copyOnAccumulateNext = false;
								}
								this.buffer.Add(next);
							}
						}

						// Token: 0x040050BE RID: 20670
						private readonly int count;

						// Token: 0x040050BF RID: 20671
						private List<IValueReference> buffer;

						// Token: 0x040050C0 RID: 20672
						private bool copyOnAccumulateNext;
					}
				}

				// Token: 0x02001780 RID: 6016
				private sealed class ChainingTakeAccumulable : IAccumulable
				{
					// Token: 0x06009880 RID: 39040 RVA: 0x001F7A8C File Offset: 0x001F5C8C
					public ChainingTakeAccumulable(IAccumulable accumulable, int max)
					{
						this.accumulable = accumulable;
						this.max = max;
					}

					// Token: 0x06009881 RID: 39041 RVA: 0x001F7AA2 File Offset: 0x001F5CA2
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.TakeFunctionValue.ChainingTakeAccumulable.TakeAccumulator(this);
					}

					// Token: 0x040050C1 RID: 20673
					private readonly IAccumulable accumulable;

					// Token: 0x040050C2 RID: 20674
					private readonly int max;

					// Token: 0x02001781 RID: 6017
					private sealed class TakeAccumulator : SelectingAccumulator
					{
						// Token: 0x06009882 RID: 39042 RVA: 0x001F7AAA File Offset: 0x001F5CAA
						public TakeAccumulator(LanguageLibrary.List.TakeFunctionValue.ChainingTakeAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.count = accumulable.max;
						}

						// Token: 0x06009883 RID: 39043 RVA: 0x001F7AC9 File Offset: 0x001F5CC9
						protected override bool Select(IValueReference valueReference)
						{
							if (this.count > 0)
							{
								this.count--;
								return true;
							}
							return false;
						}

						// Token: 0x040050C3 RID: 20675
						private int count;
					}
				}
			}

			// Token: 0x02001782 RID: 6018
			private class SelectFunctionValue : NativeFunctionValue2<ListValue, ListValue, FunctionValue>, IAccumulableChainingFunction
			{
				// Token: 0x06009884 RID: 39044 RVA: 0x001F7AE5 File Offset: 0x001F5CE5
				public SelectFunctionValue()
					: base(TypeValue.List, "list", TypeValue.List, "selection", TypeValue.Function)
				{
				}

				// Token: 0x17002781 RID: 10113
				// (get) Token: 0x06009885 RID: 39045 RVA: 0x001C884D File Offset: 0x001C6A4D
				public string EnumerableParameter
				{
					get
					{
						return "list";
					}
				}

				// Token: 0x06009886 RID: 39046 RVA: 0x001F7B08 File Offset: 0x001F5D08
				public override ListValue TypedInvoke(ListValue list, FunctionValue selection)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					if (foldableListValue != null)
					{
						return foldableListValue.Select(selection);
					}
					return new LanguageLibrary.List.SelectFunctionValue.SelectListValue(list, selection);
				}

				// Token: 0x06009887 RID: 39047 RVA: 0x00049610 File Offset: 0x00047810
				public override bool TryGetAccumulableChainingFunction(out IAccumulableChainingFunction accumulableChainingFunction)
				{
					accumulableChainingFunction = this;
					return true;
				}

				// Token: 0x06009888 RID: 39048 RVA: 0x001F7B2E File Offset: 0x001F5D2E
				public IAccumulable CreateAccumulable(RecordValue arguments, IAccumulable accumulable)
				{
					return new LanguageLibrary.List.SelectFunctionValue.SelectAccumulable(accumulable, arguments["selection"].AsFunction);
				}

				// Token: 0x040050C4 RID: 20676
				private const string enumerableParameter = "list";

				// Token: 0x02001783 RID: 6019
				private class SelectListValue : StreamedListValue
				{
					// Token: 0x06009889 RID: 39049 RVA: 0x001F7B46 File Offset: 0x001F5D46
					public SelectListValue(ListValue list, FunctionValue selection)
					{
						this.list = list;
						this.selection = selection;
					}

					// Token: 0x0600988A RID: 39050 RVA: 0x001F7B5C File Offset: 0x001F5D5C
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						return new LanguageLibrary.List.SelectFunctionValue.SelectListValue.SelectEnumerator(this.list.GetEnumerator(), this.selection);
					}

					// Token: 0x040050C5 RID: 20677
					private ListValue list;

					// Token: 0x040050C6 RID: 20678
					private FunctionValue selection;

					// Token: 0x02001784 RID: 6020
					private class SelectEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x0600988B RID: 39051 RVA: 0x001F7B74 File Offset: 0x001F5D74
						public SelectEnumerator(IEnumerator<IValueReference> enumerator, FunctionValue selection)
						{
							this.enumerator = enumerator;
							this.selection = selection;
						}

						// Token: 0x17002782 RID: 10114
						// (get) Token: 0x0600988C RID: 39052 RVA: 0x001F7B8A File Offset: 0x001F5D8A
						public IValueReference Current
						{
							get
							{
								return this.enumerator.Current;
							}
						}

						// Token: 0x17002783 RID: 10115
						// (get) Token: 0x0600988D RID: 39053 RVA: 0x001F7B97 File Offset: 0x001F5D97
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x0600988E RID: 39054 RVA: 0x0000EE09 File Offset: 0x0000D009
						public void Reset()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x0600988F RID: 39055 RVA: 0x001F7B9F File Offset: 0x001F5D9F
						public void Dispose()
						{
							this.enumerator.Dispose();
						}

						// Token: 0x06009890 RID: 39056 RVA: 0x001F7BAC File Offset: 0x001F5DAC
						public bool MoveNext()
						{
							while (this.enumerator.MoveNext())
							{
								Value value = this.selection.Invoke(this.enumerator.Current.Value);
								if (!value.IsNull && value.AsBoolean)
								{
									return true;
								}
							}
							return false;
						}

						// Token: 0x040050C7 RID: 20679
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x040050C8 RID: 20680
						private FunctionValue selection;
					}
				}

				// Token: 0x02001785 RID: 6021
				private sealed class SelectAccumulable : IAccumulable
				{
					// Token: 0x06009891 RID: 39057 RVA: 0x001F7BF5 File Offset: 0x001F5DF5
					public SelectAccumulable(IAccumulable accumulable, FunctionValue selection)
					{
						this.accumulable = accumulable;
						this.selection = selection;
					}

					// Token: 0x06009892 RID: 39058 RVA: 0x001F7C0B File Offset: 0x001F5E0B
					public IAccumulator CreateAccumulator()
					{
						return new LanguageLibrary.List.SelectFunctionValue.SelectAccumulable.SelectAccumulator(this);
					}

					// Token: 0x040050C9 RID: 20681
					private readonly IAccumulable accumulable;

					// Token: 0x040050CA RID: 20682
					private readonly FunctionValue selection;

					// Token: 0x02001786 RID: 6022
					private sealed class SelectAccumulator : SelectingAccumulator
					{
						// Token: 0x06009893 RID: 39059 RVA: 0x001F7C13 File Offset: 0x001F5E13
						public SelectAccumulator(LanguageLibrary.List.SelectFunctionValue.SelectAccumulable accumulable)
							: base(accumulable.accumulable.CreateAccumulator())
						{
							this.selection = accumulable.selection;
						}

						// Token: 0x06009894 RID: 39060 RVA: 0x001F7C32 File Offset: 0x001F5E32
						protected override bool Select(IValueReference valueReference)
						{
							return this.selection.Invoke(valueReference.Value).AsBoolean;
						}

						// Token: 0x040050CB RID: 20683
						private readonly FunctionValue selection;
					}
				}
			}

			// Token: 0x02001787 RID: 6023
			private class SortFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>
			{
				// Token: 0x06009895 RID: 39061 RVA: 0x001F7C4A File Offset: 0x001F5E4A
				public SortFunctionValue()
					: base(TypeValue.List, 1, "list", TypeValue.List, "comparisonCriteria", TypeValue.Any)
				{
				}

				// Token: 0x06009896 RID: 39062 RVA: 0x001F7C6C File Offset: 0x001F5E6C
				public override ListValue TypedInvoke(ListValue list, Value comparisonCriteria)
				{
					FoldableListValue foldableListValue = list as FoldableListValue;
					bool flag;
					if (foldableListValue != null && this.TryGetSortOrder(comparisonCriteria, out flag))
					{
						return foldableListValue.Sort(flag);
					}
					IComparer<Value> comparer = Library.ListComparisonCriteria.CreateComparer(comparisonCriteria);
					return new LanguageLibrary.List.SortFunctionValue.SortListValue(list, comparer);
				}

				// Token: 0x06009897 RID: 39063 RVA: 0x001F7CA4 File Offset: 0x001F5EA4
				private bool TryGetSortOrder(Value comparisonCriteria, out bool ascending)
				{
					SortOrder[] array = Library.ListComparisonCriteria.CreateSortCriteria(comparisonCriteria);
					if (array.Length == 1 && array[0].Comparer == null && array[0].Selector == null)
					{
						ascending = array[0].Ascending;
						return true;
					}
					ascending = false;
					return false;
				}

				// Token: 0x02001788 RID: 6024
				private class SortListValue : StreamedListValue
				{
					// Token: 0x06009898 RID: 39064 RVA: 0x001F7CEE File Offset: 0x001F5EEE
					public SortListValue(ListValue list, IComparer<Value> comparer)
					{
						this.list = list;
						this.comparer = comparer;
					}

					// Token: 0x17002784 RID: 10116
					// (get) Token: 0x06009899 RID: 39065 RVA: 0x001F7D04 File Offset: 0x001F5F04
					public override long LargeCount
					{
						get
						{
							return this.list.LargeCount;
						}
					}

					// Token: 0x17002785 RID: 10117
					// (get) Token: 0x0600989A RID: 39066 RVA: 0x001F7D11 File Offset: 0x001F5F11
					public override TypeValue Type
					{
						get
						{
							return this.list.Type;
						}
					}

					// Token: 0x0600989B RID: 39067 RVA: 0x001F7D20 File Offset: 0x001F5F20
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						Value[] array = this.list.ToArray();
						Value[] array2 = new Value[array.Length];
						Array.Copy(array, array2, array.Length);
						try
						{
							Array.Sort<Value>(array2, this.comparer);
						}
						catch (InvalidOperationException ex)
						{
							if (ex.InnerException != null)
							{
								throw ex.InnerException;
							}
						}
						return ListValue.New(array2).GetEnumerator();
					}

					// Token: 0x040050CC RID: 20684
					private readonly ListValue list;

					// Token: 0x040050CD RID: 20685
					private readonly IComparer<Value> comparer;
				}
			}
		}

		// Token: 0x02001789 RID: 6025
		public static class Query
		{
			// Token: 0x0600989C RID: 39068 RVA: 0x001F7D88 File Offset: 0x001F5F88
			public static IExpression CreateInvokeQuery(Value function, params Value[] args)
			{
				IExpression[] array = new IExpression[args.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new ConstantExpressionSyntaxNode(args[i]);
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(function), array);
			}
		}

		// Token: 0x0200178A RID: 6026
		private sealed class CacheKey : IEquatable<LanguageLibrary.CacheKey>
		{
			// Token: 0x0600989D RID: 39069 RVA: 0x001F7DC3 File Offset: 0x001F5FC3
			public CacheKey(IEngineHost host, IList<IModule> modules)
			{
				this.host = host;
				this.modules = modules;
			}

			// Token: 0x0600989E RID: 39070 RVA: 0x001F7DD9 File Offset: 0x001F5FD9
			public override int GetHashCode()
			{
				return this.host.GetHashCode();
			}

			// Token: 0x0600989F RID: 39071 RVA: 0x001F7DE6 File Offset: 0x001F5FE6
			public override bool Equals(object other)
			{
				return this.Equals(other as LanguageLibrary.CacheKey);
			}

			// Token: 0x060098A0 RID: 39072 RVA: 0x001F7DF4 File Offset: 0x001F5FF4
			public bool Equals(LanguageLibrary.CacheKey other)
			{
				return other != null && this.host == other.host && this.modules.SequenceEqual(other.modules);
			}

			// Token: 0x040050CE RID: 20686
			private readonly IEngineHost host;

			// Token: 0x040050CF RID: 20687
			private readonly IList<IModule> modules;
		}

		// Token: 0x0200178B RID: 6027
		private sealed class CachingLibraryModule : DelegatingModule
		{
			// Token: 0x060098A1 RID: 39073 RVA: 0x001F7E1A File Offset: 0x001F601A
			public CachingLibraryModule(Module module)
				: base(module)
			{
				this.cache = new LruCache<LanguageLibrary.CachingLibraryModule.EngineHostKey, RecordValue>(10, null);
			}

			// Token: 0x060098A2 RID: 39074 RVA: 0x001F7E34 File Offset: 0x001F6034
			public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
			{
				LanguageLibrary.CachingLibraryModule.EngineHostKey engineHostKey = new LanguageLibrary.CachingLibraryModule.EngineHostKey(hostEnvironment);
				ILifetimeService lifetimeService = hostEnvironment.QueryService<ILifetimeService>();
				if (lifetimeService != null)
				{
					lifetimeService.Register(new LanguageLibrary.CachingLibraryModule.RemoveCacheItem(this.cache, engineHostKey));
				}
				RecordValue recordValue = LanguageLibrary.LinkLibrary<LanguageLibrary.CachingLibraryModule.EngineHostKey>(hostEnvironment, new Module[] { base.BaseModule }, this.cache, engineHostKey);
				return RecordValue.New(Linker.LinkedKeys, new Value[]
				{
					Value.Null,
					recordValue,
					RecordValue.Empty
				});
			}

			// Token: 0x040050D0 RID: 20688
			private readonly LruCache<LanguageLibrary.CachingLibraryModule.EngineHostKey, RecordValue> cache;

			// Token: 0x0200178C RID: 6028
			private struct EngineHostKey : IEquatable<LanguageLibrary.CachingLibraryModule.EngineHostKey>
			{
				// Token: 0x060098A3 RID: 39075 RVA: 0x001F7EA6 File Offset: 0x001F60A6
				public EngineHostKey(IEngineHost engineHost)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x060098A4 RID: 39076 RVA: 0x001F7EAF File Offset: 0x001F60AF
				public override int GetHashCode()
				{
					return this.engineHost.GetHashCode();
				}

				// Token: 0x060098A5 RID: 39077 RVA: 0x001F7EBC File Offset: 0x001F60BC
				public override bool Equals(object obj)
				{
					LanguageLibrary.CachingLibraryModule.EngineHostKey? engineHostKey = obj as LanguageLibrary.CachingLibraryModule.EngineHostKey?;
					return engineHostKey != null && this.Equals(engineHostKey.Value);
				}

				// Token: 0x060098A6 RID: 39078 RVA: 0x001F7EED File Offset: 0x001F60ED
				public bool Equals(LanguageLibrary.CachingLibraryModule.EngineHostKey other)
				{
					return this.engineHost == other.engineHost;
				}

				// Token: 0x040050D1 RID: 20689
				private readonly IEngineHost engineHost;
			}

			// Token: 0x0200178D RID: 6029
			private class RemoveCacheItem : IDisposable
			{
				// Token: 0x060098A7 RID: 39079 RVA: 0x001F7EFD File Offset: 0x001F60FD
				public RemoveCacheItem(LruCache<LanguageLibrary.CachingLibraryModule.EngineHostKey, RecordValue> cache, LanguageLibrary.CachingLibraryModule.EngineHostKey key)
				{
					this.cache = cache;
					this.key = key;
				}

				// Token: 0x060098A8 RID: 39080 RVA: 0x001F7F13 File Offset: 0x001F6113
				public void Dispose()
				{
					if (this.cache != null)
					{
						this.cache.Remove(this.key);
						this.cache = null;
					}
				}

				// Token: 0x040050D2 RID: 20690
				private readonly LanguageLibrary.CachingLibraryModule.EngineHostKey key;

				// Token: 0x040050D3 RID: 20691
				private LruCache<LanguageLibrary.CachingLibraryModule.EngineHostKey, RecordValue> cache;
			}
		}
	}
}
