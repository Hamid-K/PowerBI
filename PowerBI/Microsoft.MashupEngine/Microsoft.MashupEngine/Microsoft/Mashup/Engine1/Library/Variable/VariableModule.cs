using System;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Variable
{
	// Token: 0x020002AD RID: 685
	internal class VariableModule : Module
	{
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00038D68 File Offset: 0x00036F68
		public override string Name
		{
			get
			{
				return "Variable";
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00038D6F File Offset: 0x00036F6F
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
							return "Variable.Value";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00038DAC File Offset: 0x00036FAC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new VariableModule.VariableValueFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x0400086F RID: 2159
		public const string VariableValue = "Variable.Value";

		// Token: 0x04000870 RID: 2160
		private Keys exportKeys;

		// Token: 0x020002AE RID: 686
		private enum Exports
		{
			// Token: 0x04000872 RID: 2162
			VariableValue,
			// Token: 0x04000873 RID: 2163
			Count
		}

		// Token: 0x020002AF RID: 687
		private class VariableValueFunctionValue : NativeFunctionValue1<Value, TextValue>
		{
			// Token: 0x06001B3C RID: 6972 RVA: 0x00038DDD File Offset: 0x00036FDD
			public VariableValueFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, 1, "identifier", TypeValue.Text)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x00038DFC File Offset: 0x00036FFC
			public override Value TypedInvoke(TextValue identifier)
			{
				object variable = this.GetVariable(identifier);
				if (variable is Func<Stream>)
				{
					return new GetStreamBinaryValue((Func<Stream>)variable);
				}
				if (variable is Func<IPageReader>)
				{
					Func<IPageReader> func = (Func<IPageReader>)variable;
					return new GetPageReaderTableValue(this.engineHost.QueryService<ILifetimeService>(), func);
				}
				return VariableModule.VariableValueFunctionValue.MarshalFromClr(variable);
			}

			// Token: 0x06001B3E RID: 6974 RVA: 0x00038E4C File Offset: 0x0003704C
			private object GetVariable(TextValue identifier)
			{
				object obj;
				if (this.engineHost.QueryService<IVariableService>().TryGetValue(identifier.String, out obj))
				{
					return obj;
				}
				throw ValueException.NewExpressionError<Message1>(Strings.VariableNotFound(identifier.String), null, null);
			}

			// Token: 0x06001B3F RID: 6975 RVA: 0x00038E88 File Offset: 0x00037088
			private static Value MarshalFromClr(object obj)
			{
				Value value;
				try
				{
					value = ValueMarshaller.MarshalFromClr(obj);
				}
				catch (InvalidOperationException)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueMarshaller_CannotMarshalToValue, null, null);
				}
				return value;
			}

			// Token: 0x04000874 RID: 2164
			private readonly IEngineHost engineHost;
		}
	}
}
