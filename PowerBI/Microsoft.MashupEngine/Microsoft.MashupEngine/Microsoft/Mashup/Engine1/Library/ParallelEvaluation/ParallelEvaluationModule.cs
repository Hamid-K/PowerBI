using System;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ParallelEvaluation
{
	// Token: 0x02000543 RID: 1347
	internal class ParallelEvaluationModule : Module
	{
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000834FB File Offset: 0x000816FB
		public override string Name
		{
			get
			{
				return "ParallelEvaluation";
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x00083502 File Offset: 0x00081702
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "List.ParallelInvoke";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00083540 File Offset: 0x00081740
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ParallelEvaluationModule.List.ParallelInvokeFunctionValue(host);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x040012CC RID: 4812
		private Keys exportKeys;

		// Token: 0x02000544 RID: 1348
		private enum Exports
		{
			// Token: 0x040012CE RID: 4814
			List_ParallelInvoke,
			// Token: 0x040012CF RID: 4815
			Count
		}

		// Token: 0x02000545 RID: 1349
		public class List
		{
			// Token: 0x02000546 RID: 1350
			public class ParallelInvokeFunctionValue : NativeFunctionValue2<ListValue, ListValue, Value>
			{
				// Token: 0x06002B56 RID: 11094 RVA: 0x00083571 File Offset: 0x00081771
				public ParallelInvokeFunctionValue(IEngineHost engineHost)
					: base(TypeValue.List, 1, "list", ParallelEvaluationModule.List.ParallelInvokeFunctionValue.listOfFunctionsType, "concurrency", NullableTypeValue.Int32)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06002B57 RID: 11095 RVA: 0x0008359C File Offset: 0x0008179C
				public override ListValue TypedInvoke(ListValue list, Value concurrency)
				{
					int num = (concurrency.IsNull ? 8 : concurrency.AsNumber.AsInteger32);
					if (num < 1)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ListParallelInvoke_ConcurrencyMustBePositive, concurrency, null);
					}
					return new ParallelEvaluationModule.List.ParallelInvokeFunctionValue.ParallelListValue(this.engineHost, list, num);
				}

				// Token: 0x040012D0 RID: 4816
				private const int defaultConcurrency = 8;

				// Token: 0x040012D1 RID: 4817
				private static readonly TypeValue listOfFunctionsType = ListTypeValue.New(TypeValue.Function);

				// Token: 0x040012D2 RID: 4818
				private readonly IEngineHost engineHost;

				// Token: 0x02000547 RID: 1351
				private class ParallelListValue : StreamedListValue
				{
					// Token: 0x06002B59 RID: 11097 RVA: 0x000835EF File Offset: 0x000817EF
					public ParallelListValue(IEngineHost engineHost, ListValue list, int concurrency)
					{
						this.syncRoot = new object();
						this.engineHost = engineHost;
						this.list = list;
						this.concurrency = concurrency;
					}

					// Token: 0x06002B5A RID: 11098 RVA: 0x00083618 File Offset: 0x00081818
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						EngineContext.Enable();
						ParallelEvaluationModule.List.ParallelInvokeFunctionValue.ParallelListValue.ConcurrentEnumerator concurrentEnumerator = null;
						IEnumerator<IValueReference> enumerator;
						try
						{
							concurrentEnumerator = new ParallelEvaluationModule.List.ParallelInvokeFunctionValue.ParallelListValue.ConcurrentEnumerator(this.list.GetEnumerator());
							enumerator = new ParallelValueEnumerator(this.engineHost, this.concurrency, delegate(int index)
							{
								IValueReference reference = concurrentEnumerator.GetReference(index);
								if (reference != null)
								{
									return reference.Value.AsFunction.Invoke();
								}
								return null;
							}).AfterDispose(new Action(concurrentEnumerator.Dispose)).AfterDispose(new Action(EngineContext.Disable));
						}
						catch
						{
							if (concurrentEnumerator != null)
							{
								concurrentEnumerator.Dispose();
							}
							EngineContext.Disable();
							throw;
						}
						return enumerator;
					}

					// Token: 0x040012D3 RID: 4819
					private readonly object syncRoot;

					// Token: 0x040012D4 RID: 4820
					private readonly IEngineHost engineHost;

					// Token: 0x040012D5 RID: 4821
					private readonly ListValue list;

					// Token: 0x040012D6 RID: 4822
					private readonly int concurrency;

					// Token: 0x02000548 RID: 1352
					private class ConcurrentEnumerator
					{
						// Token: 0x06002B5B RID: 11099 RVA: 0x000836C0 File Offset: 0x000818C0
						public ConcurrentEnumerator(IEnumerator<IValueReference> enumerator)
						{
							this.enumeratorLock = new object();
							this.cachedValues = new Dictionary<int, IValueReference>();
							this.enumerator = enumerator;
							this.cachedToIndex = -1;
						}

						// Token: 0x06002B5C RID: 11100 RVA: 0x000836EC File Offset: 0x000818EC
						private IValueReference GetCachedValue(int index)
						{
							Dictionary<int, IValueReference> dictionary = this.cachedValues;
							lock (dictionary)
							{
								if (index <= this.cachedToIndex)
								{
									IValueReference valueReference;
									if (this.cachedValues.TryGetValue(index, out valueReference))
									{
										this.cachedValues.Remove(index);
										return valueReference;
									}
									throw new InvalidOperationException("Item at index " + index.ToString() + " already retrieved.");
								}
							}
							return null;
						}

						// Token: 0x06002B5D RID: 11101 RVA: 0x00083770 File Offset: 0x00081970
						public IValueReference GetReference(int index)
						{
							IValueReference cachedValue2;
							using (EngineContext.Leave())
							{
								IValueReference cachedValue;
								for (;;)
								{
									cachedValue = this.GetCachedValue(index);
									if (cachedValue != null)
									{
										break;
									}
									object obj = this.enumeratorLock;
									lock (obj)
									{
										if (this.enumerator != null)
										{
											IValueReference valueReference = null;
											using (EngineContext.Enter())
											{
												if (this.enumerator.MoveNext())
												{
													valueReference = this.enumerator.Current;
												}
												else
												{
													this.enumerator.Dispose();
													this.enumerator = null;
												}
											}
											if (valueReference != null)
											{
												Dictionary<int, IValueReference> dictionary = this.cachedValues;
												lock (dictionary)
												{
													this.cachedToIndex++;
													this.cachedValues.Add(this.cachedToIndex, valueReference);
												}
											}
										}
										if (this.enumerator != null)
										{
											continue;
										}
									}
									goto IL_00DE;
								}
								return cachedValue;
								IL_00DE:
								cachedValue2 = this.GetCachedValue(index);
							}
							return cachedValue2;
						}

						// Token: 0x06002B5E RID: 11102 RVA: 0x000838A8 File Offset: 0x00081AA8
						public void Dispose()
						{
							object obj = this.enumeratorLock;
							lock (obj)
							{
								Dictionary<int, IValueReference> dictionary = this.cachedValues;
								lock (dictionary)
								{
									this.cachedToIndex = -2;
									this.cachedValues.Clear();
									if (this.enumerator != null)
									{
										this.enumerator.Dispose();
										this.enumerator = null;
									}
								}
							}
						}

						// Token: 0x040012D7 RID: 4823
						private const int disposed = -2;

						// Token: 0x040012D8 RID: 4824
						private readonly object enumeratorLock;

						// Token: 0x040012D9 RID: 4825
						private readonly Dictionary<int, IValueReference> cachedValues;

						// Token: 0x040012DA RID: 4826
						private IEnumerator<IValueReference> enumerator;

						// Token: 0x040012DB RID: 4827
						private int cachedToIndex;
					}
				}
			}
		}
	}
}
