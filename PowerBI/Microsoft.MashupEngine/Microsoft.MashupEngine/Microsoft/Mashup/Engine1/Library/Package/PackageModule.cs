using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Package
{
	// Token: 0x02000554 RID: 1364
	internal class PackageModule : Module
	{
		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00083D9D File Offset: 0x00081F9D
		public override string Name
		{
			get
			{
				return "Package";
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00083DA4 File Offset: 0x00081FA4
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
							return "Embedded.Value";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x00083DE0 File Offset: 0x00081FE0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new PackageModule.EmbeddedValueFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x040012F4 RID: 4852
		public const string EmbeddedValue = "Embedded.Value";

		// Token: 0x040012F5 RID: 4853
		private Keys exportKeys;

		// Token: 0x02000555 RID: 1365
		private enum Exports
		{
			// Token: 0x040012F7 RID: 4855
			EmbeddedValue,
			// Token: 0x040012F8 RID: 4856
			Count
		}

		// Token: 0x02000556 RID: 1366
		private class EmbeddedValueFunctionValue : NativeFunctionValue2<Value, Value, TextValue>
		{
			// Token: 0x06002B89 RID: 11145 RVA: 0x00083E11 File Offset: 0x00082011
			public EmbeddedValueFunctionValue(IEngineHost hostEnvironment)
				: base(TypeValue.Any, 2, "value", TypeValue.Any, "path", TypeValue.Text)
			{
				this.hostEnvironment = hostEnvironment;
			}

			// Token: 0x06002B8A RID: 11146 RVA: 0x00083E3C File Offset: 0x0008203C
			public override Value TypedInvoke(Value value, TextValue pathValue)
			{
				string @string = pathValue.String;
				IEmbeddedValueLoggingService embeddedValueLoggingService = this.hostEnvironment.QueryService<IEmbeddedValueLoggingService>();
				if (embeddedValueLoggingService != null)
				{
					embeddedValueLoggingService.LogEmbeddedValue(@string);
				}
				return value;
			}

			// Token: 0x040012F9 RID: 4857
			private readonly IEngineHost hostEnvironment;
		}
	}
}
