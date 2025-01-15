using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Json
{
	// Token: 0x02000A36 RID: 2614
	internal sealed class JsonModule : Module
	{
		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x000F3569 File Offset: 0x000F1769
		public override string Name
		{
			get
			{
				return "Json";
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x060048C7 RID: 18631 RVA: 0x000F3570 File Offset: 0x000F1770
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Json.Document";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Json.FromValue";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000F35AB File Offset: 0x000F17AB
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			if (this.exports == null)
			{
				this.exports = RecordValue.New(this.ExportKeys, delegate(int index)
				{
					if (index == 0)
					{
						return JsonModule.Json.Document;
					}
					if (index != 1)
					{
						throw new InvalidOperationException();
					}
					return JsonModule.Json.FromValue;
				});
			}
			return this.exports;
		}

		// Token: 0x040026F1 RID: 9969
		private Keys exportKeys;

		// Token: 0x040026F2 RID: 9970
		private RecordValue exports;

		// Token: 0x02000A37 RID: 2615
		private enum Exports
		{
			// Token: 0x040026F4 RID: 9972
			JsonDocument,
			// Token: 0x040026F5 RID: 9973
			JsonFromValue,
			// Token: 0x040026F6 RID: 9974
			Count
		}

		// Token: 0x02000A38 RID: 2616
		public class Json
		{
			// Token: 0x040026F7 RID: 9975
			public static readonly FunctionValue Document = new JsonModule.Json.DocumentFunctionValue();

			// Token: 0x040026F8 RID: 9976
			public static readonly FunctionValue FromValue = new JsonModule.Json.FromValueFunctionValue();

			// Token: 0x02000A39 RID: 2617
			private class DocumentFunctionValue : NativeFunctionValue2<Value, Value, Value>
			{
				// Token: 0x060048CC RID: 18636 RVA: 0x000F3601 File Offset: 0x000F1801
				public DocumentFunctionValue()
					: base(TypeValue.Any, 1, "jsonText", TypeValue.Any, "encoding", TextEncoding.Type.Nullable)
				{
				}

				// Token: 0x060048CD RID: 18637 RVA: 0x000F3628 File Offset: 0x000F1828
				public override Value TypedInvoke(Value jsonText, Value encoding)
				{
					ContentHelper.VerifyIsContentType(jsonText);
					if (jsonText.IsText)
					{
						return JsonParser.Parse(new StringReader(jsonText.AsString), jsonText);
					}
					Value value;
					using (StreamReader streamReader = jsonText.AsBinary.OpenText(encoding))
					{
						value = JsonParser.Parse(streamReader, jsonText);
					}
					return value;
				}
			}

			// Token: 0x02000A3A RID: 2618
			private class FromValueFunctionValue : NativeFunctionValue2<BinaryValue, Value, Value>
			{
				// Token: 0x060048CE RID: 18638 RVA: 0x000F3688 File Offset: 0x000F1888
				public FromValueFunctionValue()
					: base(TypeValue.Binary, 1, "value", TypeValue.Any, "encoding", TextEncoding.Type.Nullable)
				{
				}

				// Token: 0x060048CF RID: 18639 RVA: 0x000F36B0 File Offset: 0x000F18B0
				public override BinaryValue TypedInvoke(Value value, Value encoding)
				{
					Encoding encoding2 = (encoding.IsNull ? null : TextEncoding.GetEncoding(encoding, LogicalValue.False, TextEncoding.CodePage.Utf8));
					return new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue(value, encoding2);
				}

				// Token: 0x02000A3B RID: 2619
				private sealed class JsonBinaryValue : StreamedBinaryValue
				{
					// Token: 0x060048D0 RID: 18640 RVA: 0x000F36E0 File Offset: 0x000F18E0
					public JsonBinaryValue(Value value, Encoding encoding)
					{
						this.value = value;
						this.encoding = encoding;
					}

					// Token: 0x060048D1 RID: 18641 RVA: 0x000F36F6 File Offset: 0x000F18F6
					public override Stream Open()
					{
						return new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream(this.value, this.encoding);
					}

					// Token: 0x040026F9 RID: 9977
					private readonly Value value;

					// Token: 0x040026FA RID: 9978
					private readonly Encoding encoding;

					// Token: 0x02000A3C RID: 2620
					private sealed class JsonBinaryStream : Stream
					{
						// Token: 0x060048D2 RID: 18642 RVA: 0x000F370C File Offset: 0x000F190C
						public JsonBinaryStream(Value value, Encoding encoding)
						{
							this.stack = new Stack<JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo>();
							this.buffer = new MemoryStream();
							this.writer = ((encoding == null) ? new StreamWriter(this.buffer) : new StreamWriter(this.buffer, encoding));
							this.singleByte = new byte[1];
							switch (value.Kind)
							{
							case ValueKind.List:
								this.Push(value.AsList);
								return;
							case ValueKind.Record:
								this.Push(value.AsRecord);
								return;
							case ValueKind.Table:
								this.Push(TableModule.Table.ToRecords.Invoke(value).AsList);
								return;
							default:
								this.Push(new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo
								{
									keys = null,
									index = -1,
									enumerator = new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.SingleValueEnumerator(value),
									head = null,
									tail = null
								});
								return;
							}
						}

						// Token: 0x060048D3 RID: 18643 RVA: 0x000F37E8 File Offset: 0x000F19E8
						private void Push(RecordValue record)
						{
							this.Push(new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo
							{
								keys = record.Keys,
								index = -1,
								enumerator = new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.RecordEnumerator(record),
								head = "{",
								tail = "}"
							});
						}

						// Token: 0x060048D4 RID: 18644 RVA: 0x000F3835 File Offset: 0x000F1A35
						private void Push(ListValue list)
						{
							this.Push(new JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo
							{
								keys = null,
								index = -1,
								enumerator = list.GetEnumerator(),
								head = "[",
								tail = "]"
							});
						}

						// Token: 0x060048D5 RID: 18645 RVA: 0x000F3872 File Offset: 0x000F1A72
						private void Push(JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo valueInfo)
						{
							if (this.current != null)
							{
								this.stack.Push(this.current);
							}
							this.current = valueInfo;
						}

						// Token: 0x060048D6 RID: 18646 RVA: 0x000F3894 File Offset: 0x000F1A94
						private void Pop()
						{
							if (this.stack.Count > 0)
							{
								this.current = this.stack.Pop();
								return;
							}
							this.current = null;
						}

						// Token: 0x17001709 RID: 5897
						// (get) Token: 0x060048D7 RID: 18647 RVA: 0x00002139 File Offset: 0x00000339
						public override bool CanRead
						{
							get
							{
								return true;
							}
						}

						// Token: 0x1700170A RID: 5898
						// (get) Token: 0x060048D8 RID: 18648 RVA: 0x00002105 File Offset: 0x00000305
						public override bool CanWrite
						{
							get
							{
								return false;
							}
						}

						// Token: 0x1700170B RID: 5899
						// (get) Token: 0x060048D9 RID: 18649 RVA: 0x00002105 File Offset: 0x00000305
						public override bool CanSeek
						{
							get
							{
								return false;
							}
						}

						// Token: 0x1700170C RID: 5900
						// (get) Token: 0x060048DA RID: 18650 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Length
						{
							get
							{
								throw new InvalidOperationException();
							}
						}

						// Token: 0x1700170D RID: 5901
						// (get) Token: 0x060048DB RID: 18651 RVA: 0x0000EE09 File Offset: 0x0000D009
						// (set) Token: 0x060048DC RID: 18652 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Position
						{
							get
							{
								throw new InvalidOperationException();
							}
							set
							{
								throw new InvalidOperationException();
							}
						}

						// Token: 0x060048DD RID: 18653 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override long Seek(long offset, SeekOrigin origin)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060048DE RID: 18654 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void Flush()
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060048DF RID: 18655 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void SetLength(long length)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060048E0 RID: 18656 RVA: 0x000F38C0 File Offset: 0x000F1AC0
						public override int Read(byte[] array, int offset, int count)
						{
							int num;
							for (;;)
							{
								num = this.buffer.Read(array, offset, count);
								if (num > 0)
								{
									break;
								}
								if (!this.ReadNextField() && this.buffer.Length == 0L)
								{
									return 0;
								}
							}
							return num;
						}

						// Token: 0x060048E1 RID: 18657 RVA: 0x000F38F8 File Offset: 0x000F1AF8
						public override int ReadByte()
						{
							if (this.Read(this.singleByte, 0, 1) > 0)
							{
								return (int)this.singleByte[0];
							}
							return -1;
						}

						// Token: 0x060048E2 RID: 18658 RVA: 0x0000EE09 File Offset: 0x0000D009
						public override void Write(byte[] array, int offset, int count)
						{
							throw new InvalidOperationException();
						}

						// Token: 0x060048E3 RID: 18659 RVA: 0x0000336E File Offset: 0x0000156E
						protected override void Dispose(bool disposing)
						{
						}

						// Token: 0x060048E4 RID: 18660 RVA: 0x000F3918 File Offset: 0x000F1B18
						private bool ReadNextField()
						{
							bool flag;
							try
							{
								this.buffer.SetLength(0L);
								while (this.current != null)
								{
									if (this.current.index < 0 && this.current.head != null)
									{
										this.writer.Write(this.current.head);
									}
									if (this.current.enumerator.MoveNext())
									{
										this.current.index++;
										if (this.current.index > 0)
										{
											this.writer.Write(",");
										}
										if (this.current.keys != null)
										{
											this.writer.Write(JsonFormatter.FormatString(this.current.keys[this.current.index]));
											this.writer.Write(':');
										}
										Value value = this.current.enumerator.Current.Value;
										switch (value.Kind)
										{
										case ValueKind.List:
											this.Push(value.AsList);
											return true;
										case ValueKind.Record:
											this.Push(value.AsRecord);
											return true;
										case ValueKind.Table:
											this.Push(TableModule.Table.ToRecords.Invoke(value).AsList);
											return true;
										default:
											this.writer.Write(JsonFormatter.FormatValue(value));
											return true;
										}
									}
									else
									{
										if (this.current.tail != null)
										{
											this.writer.Write(this.current.tail);
										}
										this.current.enumerator.Dispose();
										this.Pop();
									}
								}
								flag = false;
							}
							finally
							{
								this.writer.Flush();
								this.buffer.Position = 0L;
							}
							return flag;
						}

						// Token: 0x040026FB RID: 9979
						private readonly Stack<JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo> stack;

						// Token: 0x040026FC RID: 9980
						private readonly TextWriter writer;

						// Token: 0x040026FD RID: 9981
						private readonly MemoryStream buffer;

						// Token: 0x040026FE RID: 9982
						private readonly byte[] singleByte;

						// Token: 0x040026FF RID: 9983
						private JsonModule.Json.FromValueFunctionValue.JsonBinaryValue.JsonBinaryStream.ValueInfo current;

						// Token: 0x02000A3D RID: 2621
						private class ValueInfo
						{
							// Token: 0x04002700 RID: 9984
							public Keys keys;

							// Token: 0x04002701 RID: 9985
							public int index;

							// Token: 0x04002702 RID: 9986
							public IEnumerator<IValueReference> enumerator;

							// Token: 0x04002703 RID: 9987
							public string head;

							// Token: 0x04002704 RID: 9988
							public string tail;
						}

						// Token: 0x02000A3E RID: 2622
						private sealed class SingleValueEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
						{
							// Token: 0x060048E6 RID: 18662 RVA: 0x000F3AF8 File Offset: 0x000F1CF8
							public SingleValueEnumerator(IValueReference value)
							{
								this.value = value;
							}

							// Token: 0x1700170E RID: 5902
							// (get) Token: 0x060048E7 RID: 18663 RVA: 0x000F3B07 File Offset: 0x000F1D07
							public IValueReference Current
							{
								get
								{
									return this.value;
								}
							}

							// Token: 0x060048E8 RID: 18664 RVA: 0x0000336E File Offset: 0x0000156E
							public void Dispose()
							{
							}

							// Token: 0x1700170F RID: 5903
							// (get) Token: 0x060048E9 RID: 18665 RVA: 0x000F3B07 File Offset: 0x000F1D07
							object IEnumerator.Current
							{
								get
								{
									return this.value;
								}
							}

							// Token: 0x060048EA RID: 18666 RVA: 0x000F3B0F File Offset: 0x000F1D0F
							public bool MoveNext()
							{
								bool flag = !this.read;
								this.read = true;
								return flag;
							}

							// Token: 0x060048EB RID: 18667 RVA: 0x000F3B21 File Offset: 0x000F1D21
							public void Reset()
							{
								this.read = false;
							}

							// Token: 0x04002705 RID: 9989
							private readonly IValueReference value;

							// Token: 0x04002706 RID: 9990
							private bool read;
						}

						// Token: 0x02000A3F RID: 2623
						private sealed class RecordEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
						{
							// Token: 0x060048EC RID: 18668 RVA: 0x000F3B2A File Offset: 0x000F1D2A
							public RecordEnumerator(RecordValue record)
							{
								this.record = record;
								this.index = -1;
							}

							// Token: 0x17001710 RID: 5904
							// (get) Token: 0x060048ED RID: 18669 RVA: 0x000F3B40 File Offset: 0x000F1D40
							object IEnumerator.Current
							{
								get
								{
									return this.Current;
								}
							}

							// Token: 0x17001711 RID: 5905
							// (get) Token: 0x060048EE RID: 18670 RVA: 0x000F3B48 File Offset: 0x000F1D48
							public IValueReference Current
							{
								get
								{
									return this.record[this.index];
								}
							}

							// Token: 0x060048EF RID: 18671 RVA: 0x0000EE09 File Offset: 0x0000D009
							public void Reset()
							{
								throw new InvalidOperationException();
							}

							// Token: 0x060048F0 RID: 18672 RVA: 0x000F3B5B File Offset: 0x000F1D5B
							public bool MoveNext()
							{
								this.index++;
								return this.index < this.record.Keys.Length;
							}

							// Token: 0x060048F1 RID: 18673 RVA: 0x0000336E File Offset: 0x0000156E
							public void Dispose()
							{
							}

							// Token: 0x04002707 RID: 9991
							private RecordValue record;

							// Token: 0x04002708 RID: 9992
							private int index;
						}
					}
				}
			}
		}
	}
}
