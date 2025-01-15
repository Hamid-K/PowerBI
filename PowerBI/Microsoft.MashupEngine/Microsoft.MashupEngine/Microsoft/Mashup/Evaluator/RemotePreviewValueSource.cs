using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D36 RID: 7478
	internal static class RemotePreviewValueSource
	{
		// Token: 0x0600BA3E RID: 47678 RVA: 0x0025B9EC File Offset: 0x00259BEC
		public static void RunStub(IEngineHost engineHost, IMessageChannel channel, Func<IPreviewValueSource> getPreviewValueSource)
		{
			EvaluationHost.ReportExceptions("RemotePreviewValueSource/RunStub", engineHost, channel, delegate
			{
				using (IPreviewValueSource previewValueSource = getPreviewValueSource())
				{
					bool flag = false;
					string text;
					try
					{
						channel.Post(new RemotePreviewValueSource.TableSourceMessage
						{
							TableSource = previewValueSource.TableSource
						});
						flag = true;
						string smallValue = previewValueSource.SmallValue;
						if (!previewValueSource.IsComplete)
						{
							channel.Post(new RemotePreviewValueSource.SerializedValueMessage
							{
								Small = true,
								SerializedValue = smallValue
							});
						}
						text = previewValueSource.Value;
					}
					catch (ValueException2 valueException)
					{
						if (!flag)
						{
							channel.Post(new RemotePreviewValueSource.TableSourceMessage());
						}
						IGetStackFrameExtendedInfo getStackFrameExtendedInfo = engineHost.QueryService<IGetStackFrameExtendedInfo>();
						text = ValueSerializer.SerializePreviewException(engineHost.QueryService<IEngine>(), valueException, getStackFrameExtendedInfo);
					}
					channel.Post(new RemotePreviewValueSource.SerializedValueMessage
					{
						Small = false,
						SerializedValue = text
					});
				}
			});
		}

		// Token: 0x0600BA3F RID: 47679 RVA: 0x0025BA36 File Offset: 0x00259C36
		public static IPreviewValueSource CreateProxy(IEngineHost engineHost, IMessageChannel channel, ExceptionHandler exceptionHandler)
		{
			return new RemotePreviewValueSource.PreviewValueSource(channel, engineHost, exceptionHandler);
		}

		// Token: 0x02001D37 RID: 7479
		private sealed class PreviewValueSource : IPreviewValueSource, IDisposable
		{
			// Token: 0x0600BA40 RID: 47680 RVA: 0x0025BA40 File Offset: 0x00259C40
			public PreviewValueSource(IMessageChannel channel, IEngineHost engineHost, ExceptionHandler exceptionHandler)
			{
				this.channel = channel;
				this.engineHost = engineHost;
				this.exceptionHandler = exceptionHandler;
			}

			// Token: 0x17002E09 RID: 11785
			// (get) Token: 0x0600BA41 RID: 47681 RVA: 0x0025BA5D File Offset: 0x00259C5D
			public bool IsComplete
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x17002E0A RID: 11786
			// (get) Token: 0x0600BA42 RID: 47682 RVA: 0x0025BA68 File Offset: 0x00259C68
			public ITableSource TableSource
			{
				get
				{
					this.WaitFor(new Func<bool>(this.HaveTableSource), false);
					return this.tableSource;
				}
			}

			// Token: 0x17002E0B RID: 11787
			// (get) Token: 0x0600BA43 RID: 47683 RVA: 0x0025BA83 File Offset: 0x00259C83
			public string SmallValue
			{
				get
				{
					this.WaitFor(new Func<bool>(this.HaveSmallValue), false);
					return this.smallValue;
				}
			}

			// Token: 0x17002E0C RID: 11788
			// (get) Token: 0x0600BA44 RID: 47684 RVA: 0x0025BA9E File Offset: 0x00259C9E
			public string Value
			{
				get
				{
					this.WaitFor(new Func<bool>(this.HaveValue), false);
					return this.value;
				}
			}

			// Token: 0x0600BA45 RID: 47685 RVA: 0x0025BAB9 File Offset: 0x00259CB9
			public void Dispose()
			{
				this.WaitFor(new Func<bool>(this.HaveValue), true);
			}

			// Token: 0x0600BA46 RID: 47686 RVA: 0x0025BAD0 File Offset: 0x00259CD0
			private void WaitFor(Func<bool> condition, bool disposing = false)
			{
				try
				{
					if (this.exception != null)
					{
						throw this.exception.ToCallbackException();
					}
					while (!condition())
					{
						try
						{
							if (!this.haveTableSource)
							{
								this.tableSource = this.channel.WaitFor<RemotePreviewValueSource.TableSourceMessage>().TableSource;
								this.haveTableSource = true;
							}
							else
							{
								RemotePreviewValueSource.SerializedValueMessage serializedValueMessage = this.channel.WaitFor<RemotePreviewValueSource.SerializedValueMessage>();
								if (serializedValueMessage.Small || this.smallValue == null)
								{
									this.smallValue = serializedValueMessage.SerializedValue;
								}
								if (!serializedValueMessage.Small)
								{
									this.value = serializedValueMessage.SerializedValue;
								}
							}
						}
						catch (ValueException2 valueException)
						{
							SerializedValueException ex = SerializedValueException.New(valueException, this.engineHost);
							this.haveTableSource = true;
							this.smallValue = this.smallValue ?? ex.SerializedException;
							this.value = this.value ?? ex.SerializedException;
						}
					}
				}
				catch (Exception ex2)
				{
					this.exception = this.exception ?? ex2;
					if (!this.exceptionHandler(ex2, disposing) || !disposing)
					{
						throw;
					}
				}
			}

			// Token: 0x0600BA47 RID: 47687 RVA: 0x0025BBEC File Offset: 0x00259DEC
			private bool HaveTableSource()
			{
				return this.haveTableSource;
			}

			// Token: 0x0600BA48 RID: 47688 RVA: 0x0025BBF4 File Offset: 0x00259DF4
			private bool HaveSmallValue()
			{
				return this.smallValue != null;
			}

			// Token: 0x0600BA49 RID: 47689 RVA: 0x0025BA5D File Offset: 0x00259C5D
			private bool HaveValue()
			{
				return this.value != null;
			}

			// Token: 0x04005ECC RID: 24268
			private readonly IMessageChannel channel;

			// Token: 0x04005ECD RID: 24269
			private readonly IEngineHost engineHost;

			// Token: 0x04005ECE RID: 24270
			private readonly ExceptionHandler exceptionHandler;

			// Token: 0x04005ECF RID: 24271
			private bool haveTableSource;

			// Token: 0x04005ED0 RID: 24272
			private ITableSource tableSource;

			// Token: 0x04005ED1 RID: 24273
			private string smallValue;

			// Token: 0x04005ED2 RID: 24274
			private string value;

			// Token: 0x04005ED3 RID: 24275
			private Exception exception;
		}

		// Token: 0x02001D38 RID: 7480
		private sealed class TableSourceMessage : BufferedMessage
		{
			// Token: 0x17002E0D RID: 11789
			// (get) Token: 0x0600BA4A RID: 47690 RVA: 0x0025BBFF File Offset: 0x00259DFF
			// (set) Token: 0x0600BA4B RID: 47691 RVA: 0x0025BC07 File Offset: 0x00259E07
			public ITableSource TableSource { get; set; }

			// Token: 0x0600BA4C RID: 47692 RVA: 0x0025BC10 File Offset: 0x00259E10
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteITableSource(this.TableSource);
			}

			// Token: 0x0600BA4D RID: 47693 RVA: 0x0025BC1E File Offset: 0x00259E1E
			public override void Deserialize(BinaryReader reader)
			{
				this.TableSource = reader.ReadITableSource();
			}
		}

		// Token: 0x02001D39 RID: 7481
		private sealed class SerializedValueMessage : UnbufferedMessage
		{
			// Token: 0x17002E0E RID: 11790
			// (get) Token: 0x0600BA4F RID: 47695 RVA: 0x0025BC2C File Offset: 0x00259E2C
			// (set) Token: 0x0600BA50 RID: 47696 RVA: 0x0025BC34 File Offset: 0x00259E34
			public bool Small { get; set; }

			// Token: 0x17002E0F RID: 11791
			// (get) Token: 0x0600BA51 RID: 47697 RVA: 0x0025BC3D File Offset: 0x00259E3D
			// (set) Token: 0x0600BA52 RID: 47698 RVA: 0x0025BC45 File Offset: 0x00259E45
			public string SerializedValue { get; set; }

			// Token: 0x0600BA53 RID: 47699 RVA: 0x0025BC4E File Offset: 0x00259E4E
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Small);
				writer.WriteByteArray(BinarySerializer.Serialize(delegate(BinaryWriter w)
				{
					w.WriteString(this.SerializedValue);
				}));
			}

			// Token: 0x0600BA54 RID: 47700 RVA: 0x0025BC73 File Offset: 0x00259E73
			public override void Deserialize(BinaryReader reader)
			{
				this.Small = reader.ReadBool();
				this.SerializedValue = BinarySerializer.Deserialize<string>(reader.ReadByteArray(), (BinaryReader r) => r.ReadString());
			}
		}
	}
}
