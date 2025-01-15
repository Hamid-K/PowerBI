using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016D7 RID: 5847
	internal sealed class VectorizedAddColumnEnumerable : IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x060094BB RID: 38075 RVA: 0x001EB167 File Offset: 0x001E9367
		public VectorizedAddColumnEnumerable(IEnumerable<IValueReference> rows, TableTypeValue innerTableType, Keys columns, FunctionValue vectorFunction, ColumnSelection vectorArguments, FunctionTypeValue scalarFunctionType)
		{
			this.rows = rows;
			this.innerTableType = innerTableType;
			this.columns = columns;
			this.vectorFunction = vectorFunction;
			this.vectorArguments = vectorArguments;
			this.scalarFunctionType = scalarFunctionType;
		}

		// Token: 0x060094BC RID: 38076 RVA: 0x001EB19C File Offset: 0x001E939C
		public IEnumerator<IValueReference> GetEnumerator()
		{
			return new VectorizedAddColumnEnumerable.Enumerator(this.rows, this.innerTableType, this.columns, this.vectorFunction, this.vectorArguments, this.scalarFunctionType);
		}

		// Token: 0x060094BD RID: 38077 RVA: 0x001EB1C7 File Offset: 0x001E93C7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004F0C RID: 20236
		private readonly IEnumerable<IValueReference> rows;

		// Token: 0x04004F0D RID: 20237
		private readonly TableTypeValue innerTableType;

		// Token: 0x04004F0E RID: 20238
		private readonly Keys columns;

		// Token: 0x04004F0F RID: 20239
		private readonly FunctionValue vectorFunction;

		// Token: 0x04004F10 RID: 20240
		private readonly ColumnSelection vectorArguments;

		// Token: 0x04004F11 RID: 20241
		private readonly FunctionTypeValue scalarFunctionType;

		// Token: 0x020016D8 RID: 5848
		private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060094BE RID: 38078 RVA: 0x001EB1D0 File Offset: 0x001E93D0
			public Enumerator(IEnumerable<IValueReference> rows, TableTypeValue innerTableType, Keys columns, FunctionValue vectorFunction, ColumnSelection vectorArguments, FunctionTypeValue scalarFunctionType)
			{
				this.columns = columns;
				VectorizedAddColumnEnumerable.Enumerator.Buffer buffer = new VectorizedAddColumnEnumerable.Enumerator.Buffer(rows.GetEnumerator());
				this.enumerator = new VectorizedAddColumnEnumerable.Enumerator.TeeEnumerator(buffer, -1);
				this.argumentsEnumerator = new VectorizedAddColumnEnumerable.Enumerator.TeeEnumerator(buffer, 1);
				TableValue tableValue = new ListTableValue(ListValue.New(new VectorizedAddColumnEnumerable.Enumerator.SingleUseEnumerable(this.argumentsEnumerator)), innerTableType);
				tableValue = tableValue.SelectColumns(vectorArguments);
				tableValue = VectorizedAddColumnEnumerable.Enumerator.ReplaceTableType(tableValue, scalarFunctionType);
				ListValue asList = vectorFunction.Invoke(tableValue).AsList;
				this.newColumnEnumerator = asList.GetEnumerator();
			}

			// Token: 0x060094BF RID: 38079 RVA: 0x001EB254 File Offset: 0x001E9454
			private static TableValue ReplaceTableType(TableValue table, FunctionTypeValue scalarFunctionType)
			{
				RecordTypeValue itemType = table.Type.AsTableType.ItemType;
				RecordBuilder recordBuilder = new RecordBuilder(itemType.Fields.Count);
				for (int i = 0; i < itemType.Fields.Count; i++)
				{
					string text = itemType.Fields.Keys[i];
					Value value = itemType.Fields[i];
					Value value2;
					if (value.AsRecord.Item0.AsType == TypeValue.Any && scalarFunctionType.Parameters.TryGetValue(text, out value2))
					{
						value = RecordTypeValue.NewField(value2.AsType, null);
					}
					else
					{
						value = itemType.Fields[i];
					}
					recordBuilder.Add(text, value, value.Type);
				}
				return table.ReplaceType(TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()))).AsTable;
			}

			// Token: 0x17002706 RID: 9990
			// (get) Token: 0x060094C0 RID: 38080 RVA: 0x001EB331 File Offset: 0x001E9531
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060094C1 RID: 38081 RVA: 0x001EB33C File Offset: 0x001E953C
			public void Dispose()
			{
				if (this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
				if (this.argumentsEnumerator != null)
				{
					this.argumentsEnumerator.Dispose();
					this.argumentsEnumerator = null;
				}
				if (this.newColumnEnumerator != null)
				{
					this.newColumnEnumerator.Dispose();
					this.newColumnEnumerator = null;
				}
			}

			// Token: 0x060094C2 RID: 38082 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x17002707 RID: 9991
			// (get) Token: 0x060094C3 RID: 38083 RVA: 0x001EB398 File Offset: 0x001E9598
			public IValueReference Current
			{
				get
				{
					if (this.current == null)
					{
						RecordValue asRecord = this.enumerator.Current.Value.AsRecord;
						IValueReference[] array = new IValueReference[this.columns.Length];
						for (int i = 0; i < asRecord.Keys.Length; i++)
						{
							array[i] = asRecord.GetReference(i);
						}
						if (this.newColumnEnumerator != null)
						{
							array[asRecord.Keys.Length] = this.newColumnEnumerator.Current;
						}
						else
						{
							array[asRecord.Keys.Length] = new ExceptionValueReference(ValueException.NewExpressionError<Message0>(Strings.FunctionScalarVectorRowCountMismatch, null, null));
						}
						this.current = RecordValue.New(this.columns, array);
					}
					return this.current;
				}
			}

			// Token: 0x060094C4 RID: 38084 RVA: 0x001EB450 File Offset: 0x001E9650
			public bool MoveNext()
			{
				this.current = null;
				bool flag = this.enumerator.MoveNext();
				if (this.newColumnEnumerator != null && !this.newColumnEnumerator.MoveNext())
				{
					this.newColumnEnumerator.Dispose();
					this.newColumnEnumerator = null;
					this.argumentsEnumerator.Dispose();
					this.argumentsEnumerator = null;
				}
				return flag;
			}

			// Token: 0x04004F12 RID: 20242
			private readonly Keys columns;

			// Token: 0x04004F13 RID: 20243
			private IEnumerator<IValueReference> enumerator;

			// Token: 0x04004F14 RID: 20244
			private IEnumerator<IValueReference> argumentsEnumerator;

			// Token: 0x04004F15 RID: 20245
			private IEnumerator<IValueReference> newColumnEnumerator;

			// Token: 0x04004F16 RID: 20246
			private IValueReference current;

			// Token: 0x020016D9 RID: 5849
			private sealed class SingleUseEnumerable : IEnumerable<IValueReference>, IEnumerable
			{
				// Token: 0x060094C5 RID: 38085 RVA: 0x001EB4A8 File Offset: 0x001E96A8
				public SingleUseEnumerable(IEnumerator<IValueReference> enumerator)
				{
					this.enumerator = enumerator;
				}

				// Token: 0x060094C6 RID: 38086 RVA: 0x001EB4B7 File Offset: 0x001E96B7
				public IEnumerator<IValueReference> GetEnumerator()
				{
					if (this.enumerator == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.FunctionScalarVectorMultipleEnumeration, null, null);
					}
					IEnumerator<IValueReference> enumerator = this.enumerator;
					this.enumerator = null;
					return enumerator;
				}

				// Token: 0x060094C7 RID: 38087 RVA: 0x001EB4DB File Offset: 0x001E96DB
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04004F17 RID: 20247
				private IEnumerator<IValueReference> enumerator;
			}

			// Token: 0x020016DA RID: 5850
			private sealed class TeeEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060094C8 RID: 38088 RVA: 0x001EB4E3 File Offset: 0x001E96E3
				public TeeEnumerator(VectorizedAddColumnEnumerable.Enumerator.Buffer buffer, int side)
				{
					this.buffer = buffer;
					this.side = side;
				}

				// Token: 0x17002708 RID: 9992
				// (get) Token: 0x060094C9 RID: 38089 RVA: 0x001EB4F9 File Offset: 0x001E96F9
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060094CA RID: 38090 RVA: 0x001EB501 File Offset: 0x001E9701
				public void Dispose()
				{
					if (this.buffer != null)
					{
						this.buffer.Dispose(this.side);
						this.buffer = null;
					}
				}

				// Token: 0x060094CB RID: 38091 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x17002709 RID: 9993
				// (get) Token: 0x060094CC RID: 38092 RVA: 0x001EB523 File Offset: 0x001E9723
				public IValueReference Current
				{
					get
					{
						return this.buffer.Current(this.side);
					}
				}

				// Token: 0x060094CD RID: 38093 RVA: 0x001EB536 File Offset: 0x001E9736
				public bool MoveNext()
				{
					return this.buffer.MoveNext(this.side);
				}

				// Token: 0x04004F18 RID: 20248
				private readonly int side;

				// Token: 0x04004F19 RID: 20249
				private VectorizedAddColumnEnumerable.Enumerator.Buffer buffer;
			}

			// Token: 0x020016DB RID: 5851
			private sealed class Buffer
			{
				// Token: 0x060094CE RID: 38094 RVA: 0x001EB549 File Offset: 0x001E9749
				public Buffer(IEnumerator<IValueReference> enumerator)
				{
					this.enumerator = enumerator;
					this.behind = 0;
					this.disposed = 0;
				}

				// Token: 0x060094CF RID: 38095 RVA: 0x001EB568 File Offset: 0x001E9768
				public bool MoveNext(int reader)
				{
					if (reader == this.behind)
					{
						this.buffer.Dequeue();
						if (this.buffer.Count > 0)
						{
							return true;
						}
						this.behind = 0;
						return this.enumerator != null;
					}
					else
					{
						if (this.enumerator == null)
						{
							return false;
						}
						this.behind = VectorizedAddColumnEnumerable.Enumerator.Buffer.Other(reader);
						if (this.buffer == null)
						{
							this.buffer = new Queue<IValueReference>();
							this.buffer.Enqueue(null);
						}
						else
						{
							this.buffer.Enqueue(this.enumerator.Current);
						}
						if (this.enumerator.MoveNext())
						{
							return true;
						}
						this.enumerator.Dispose();
						this.enumerator = null;
						return false;
					}
				}

				// Token: 0x060094D0 RID: 38096 RVA: 0x001EB61A File Offset: 0x001E981A
				public IValueReference Current(int reader)
				{
					if (reader == this.behind)
					{
						return this.buffer.Peek();
					}
					return this.enumerator.Current;
				}

				// Token: 0x060094D1 RID: 38097 RVA: 0x001EB63C File Offset: 0x001E983C
				public void Dispose(int reader)
				{
					if (this.enumerator == null && this.buffer == null)
					{
						return;
					}
					if (this.disposed == 0)
					{
						this.disposed = reader;
						return;
					}
					if (reader == VectorizedAddColumnEnumerable.Enumerator.Buffer.Other(this.disposed))
					{
						if (this.enumerator != null)
						{
							this.enumerator.Dispose();
							this.enumerator = null;
						}
						this.buffer = null;
						this.disposed = 0;
					}
				}

				// Token: 0x060094D2 RID: 38098 RVA: 0x001EB6A0 File Offset: 0x001E98A0
				private bool IsValidReader(int reader)
				{
					return reader == -1 || reader == 1;
				}

				// Token: 0x060094D3 RID: 38099 RVA: 0x001EB6AC File Offset: 0x001E98AC
				private bool IsValidState()
				{
					return (this.behind == 0) ^ (this.buffer != null && this.buffer.Count > 0);
				}

				// Token: 0x060094D4 RID: 38100 RVA: 0x001EB6D1 File Offset: 0x001E98D1
				private static int Other(int reader)
				{
					return -reader;
				}

				// Token: 0x04004F1A RID: 20250
				public const int Left = -1;

				// Token: 0x04004F1B RID: 20251
				public const int Right = 1;

				// Token: 0x04004F1C RID: 20252
				private const int Tied = 0;

				// Token: 0x04004F1D RID: 20253
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x04004F1E RID: 20254
				private Queue<IValueReference> buffer;

				// Token: 0x04004F1F RID: 20255
				private int behind;

				// Token: 0x04004F20 RID: 20256
				private int disposed;
			}
		}
	}
}
