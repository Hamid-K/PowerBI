using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000924 RID: 2340
	internal sealed class MqModule : Module
	{
		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x000E0BB2 File Offset: 0x000DEDB2
		public override string Name
		{
			get
			{
				return "MQ";
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000E0BB9 File Offset: 0x000DEDB9
		public override Keys ExportKeys
		{
			get
			{
				if (MqModule.exportKeys == null)
				{
					MqModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "MQ.Queue";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return MqModule.exportKeys;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x000E0BF1 File Offset: 0x000DEDF1
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { MqModule.resourceKindInfo };
			}
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x000E0C04 File Offset: 0x000DEE04
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new MqModule.MqQueueFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000E0C38 File Offset: 0x000DEE38
		private static IDictionary<MqFunctionOption, object> CreateOptionsMap(Value options, HashSet<string> validOptions)
		{
			IDictionary<MqFunctionOption, object> dictionary = new Dictionary<MqFunctionOption, object>();
			if (options.IsRecord)
			{
				RecordValue asRecord = options.AsRecord;
				for (int i = 0; i < asRecord.Keys.Length; i++)
				{
					int count = dictionary.Count;
					string text = asRecord.Keys[i];
					Value value = asRecord[i];
					if (!(text == "Timeout"))
					{
						if (!(text == "BinaryDisplayEncoding"))
						{
							if (text == "MessageDataDisplayEncoding")
							{
								if (MqModule.ValidEncoding(value))
								{
									dictionary[MqFunctionOption.MessageDataDisplayEncoding] = value;
								}
							}
						}
						else if (MqModule.ValidEncoding(value))
						{
							dictionary[MqFunctionOption.BinaryDisplayEncoding] = value;
						}
					}
					else if (value.IsDuration)
					{
						double totalMilliseconds = value.AsDuration.AsClrTimeSpan.TotalMilliseconds;
						if (totalMilliseconds >= 0.0 && totalMilliseconds <= 2147483647.0)
						{
							dictionary[MqFunctionOption.Timeout] = Convert.ToInt32(totalMilliseconds);
						}
					}
					if (dictionary.Count == count || !validOptions.Contains(text))
					{
						throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("MQ", Strings.MqUnsupportedQueryOption(text, value.ToSource())), Value.Null, null);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x000E0D78 File Offset: 0x000DEF78
		private static bool ValidEncoding(Value optionValue)
		{
			if (optionValue.IsType)
			{
				TypeValue asType = optionValue.AsType;
				return asType.Equals(TypeValue.Text) || asType.Equals(TypeValue.Binary);
			}
			return optionValue.Equals(Library.BinaryEncoding.Base64) || optionValue.Equals(Library.BinaryEncoding.Hex) || optionValue.Equals(TextEncoding.Utf16) || optionValue.Equals(TextEncoding.Unicode) || optionValue.Equals(TextEncoding.BigEndianUnicode) || optionValue.Equals(TextEncoding.Windows) || optionValue.Equals(TextEncoding.Ascii) || optionValue.Equals(TextEncoding.Utf8);
		}

		// Token: 0x04002316 RID: 8982
		public const string DataSourceNameString = "MQ";

		// Token: 0x04002317 RID: 8983
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Timeout", TypeValue.Duration),
			new OptionItem("BinaryDisplayEncoding", TypeValue.Number),
			new OptionItem("MessageDataDisplayEncoding", TypeValue.Number)
		});

		// Token: 0x04002318 RID: 8984
		private static readonly ResourceKindInfo resourceKindInfo = new MqResourceKindInfo();

		// Token: 0x04002319 RID: 8985
		private static Keys exportKeys;

		// Token: 0x02000925 RID: 2341
		private enum Exports
		{
			// Token: 0x0400231B RID: 8987
			Queue,
			// Token: 0x0400231C RID: 8988
			Count
		}

		// Token: 0x02000926 RID: 2342
		private sealed class MqQueueFunctionValue : NativeFunctionValue5<TableValue, TextValue, TextValue, TextValue, TextValue, Value>
		{
			// Token: 0x060042C2 RID: 17090 RVA: 0x000E0E78 File Offset: 0x000DF078
			public MqQueueFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 4, "server", TypeValue.Text, "queuemanager", TypeValue.Text, "channel", TypeValue.Text, "queue", TypeValue.Text, "options", MqModule.MqQueueFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001546 RID: 5446
			// (get) Token: 0x060042C3 RID: 17091 RVA: 0x000E0BB2 File Offset: 0x000DEDB2
			public override string PrimaryResourceKind
			{
				get
				{
					return "MQ";
				}
			}

			// Token: 0x060042C4 RID: 17092 RVA: 0x000E0ECC File Offset: 0x000DF0CC
			public override TableValue TypedInvoke(TextValue server, TextValue queuemanager, TextValue channel, TextValue queue, Value options)
			{
				if (server.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_ServerCannotBeEmpty, server, null);
				}
				if (queuemanager.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_QueuemanagerCannotBeEmpty, server, null);
				}
				if (channel.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_ChannelCannotBeEmpty, server, null);
				}
				if (queue.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_QueueCannotBeEmpty, server, null);
				}
				AssemblyLoader.EnsureHisConnectorsLoaded(this.host);
				MqConnectionParameters mqConnectionParameters = new MqConnectionParameters(server.AsString, queuemanager.AsString, channel.AsString, queue.AsString);
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, mqConnectionParameters.Resource, null);
				if (resourceCredentialCollection.Count > 0)
				{
					UsernamePasswordCredential usernamePasswordCredential = resourceCredentialCollection.OfType<UsernamePasswordCredential>().FirstOrDefault<UsernamePasswordCredential>();
					if (usernamePasswordCredential != null)
					{
						if (!string.IsNullOrEmpty(usernamePasswordCredential.Username))
						{
							mqConnectionParameters.Username = usernamePasswordCredential.Username;
						}
						if (!string.IsNullOrEmpty(usernamePasswordCredential.Password))
						{
							mqConnectionParameters.Password = usernamePasswordCredential.Password;
						}
					}
					ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = resourceCredentialCollection.OfType<ConnectionStringPropertiesAdornment>().FirstOrDefault<ConnectionStringPropertiesAdornment>();
					string text;
					if (connectionStringPropertiesAdornment != null && connectionStringPropertiesAdornment.Properties.TryGetValue("EffectiveUserName", out text))
					{
						mqConnectionParameters.ConnectAs = text;
					}
					EncryptedConnectionAdornment encryptedConnectionAdornment = resourceCredentialCollection.OfType<EncryptedConnectionAdornment>().FirstOrDefault<EncryptedConnectionAdornment>();
					if (encryptedConnectionAdornment != null)
					{
						mqConnectionParameters.UseSsl = encryptedConnectionAdornment.RequireEncryption;
					}
				}
				IDictionary<MqFunctionOption, object> dictionary = MqModule.CreateOptionsMap(options, MqModule.MqQueueFunctionValue.validOptions);
				return new MqTestConnectionTableValue(this.host, mqConnectionParameters, new QueryTableValue(MqQuery.New(this.host, mqConnectionParameters, dictionary)));
			}

			// Token: 0x060042C5 RID: 17093 RVA: 0x000E1034 File Offset: 0x000DF234
			private static bool ValidArguments(Value[] args)
			{
				return args.Length == 5 && args[0].IsText && args[1].IsText && args[2].IsText && args[3].IsText && (args[4].IsNull || args[4].IsRecord);
			}

			// Token: 0x060042C6 RID: 17094 RVA: 0x000E1088 File Offset: 0x000DF288
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				Value value2;
				Value value3;
				Value value4;
				if (MqModule.MqQueueFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("server", out value) && value.IsText && dictionary.TryGetConstant("queueManager", out value2) && value2.IsText && dictionary.TryGetConstant("channel", out value3) && value3.IsText && dictionary.TryGetConstant("queue", out value4) && value4.IsText)
				{
					location = new MqDataSourceLocation();
					location.Address["server"] = value.AsString;
					location.Address["queueManager"] = value2.AsString;
					location.Address["channel"] = value3.AsString;
					location.Address["queue"] = value4.AsString;
					IExpression @null;
					if (!dictionary.TryGetValue("options", out @null))
					{
						@null = ConstantExpressionSyntaxNode.Null;
					}
					foundOptions = ExpressionAnalysis.GetRecord(@null);
					if (foundOptions == null)
					{
						unknownOptions = null;
					}
					else
					{
						ExpressionAnalysis.RemovePlaceholders(foundOptions, out unknownOptions);
					}
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400231D RID: 8989
			private static readonly HashSet<string> validOptions = new HashSet<string> { "Timeout", "BinaryDisplayEncoding", "MessageDataDisplayEncoding" };

			// Token: 0x0400231E RID: 8990
			private static readonly TypeValue optionsType = MqModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x0400231F RID: 8991
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, __queueManager, __channel, __queue, _o_options)" });

			// Token: 0x04002320 RID: 8992
			private readonly IEngineHost host;
		}
	}
}
