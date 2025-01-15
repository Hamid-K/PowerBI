using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000384 RID: 900
	internal sealed class OpenApiModule : Module
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x000525E5 File Offset: 0x000507E5
		public override string Name
		{
			get
			{
				return "OpenApi";
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000525EC File Offset: 0x000507EC
		public override Keys ExportKeys
		{
			get
			{
				if (OpenApiModule.exportKeys == null)
				{
					OpenApiModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "OpenApi.Document";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return OpenApiModule.exportKeys;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00052624 File Offset: 0x00050824
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return base.DataSources;
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005262C File Offset: 0x0005082C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new OpenApiModule.OpenApiDocumentValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04000BEF RID: 3055
		private const string OpenApiDocument = "OpenApi.Document";

		// Token: 0x04000BF0 RID: 3056
		private const string DataSourceNameString = "OpenApi";

		// Token: 0x04000BF1 RID: 3057
		private static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("IncludeExtensions", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			new OptionItem("SecurityDefinition", NullableTypeValue.Text),
			new OptionItem("ManualStatusHandling", NullableTypeValue.List),
			new OptionItem("IncludeDeprecated", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("IncludeMoreColumns", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x04000BF2 RID: 3058
		private static Keys exportKeys;

		// Token: 0x02000385 RID: 901
		private enum Exports
		{
			// Token: 0x04000BF4 RID: 3060
			OpenApiDocument,
			// Token: 0x04000BF5 RID: 3061
			Count
		}

		// Token: 0x02000386 RID: 902
		private sealed class OpenApiDocumentValue : NativeFunctionValue2<TableValue, BinaryValue, Value>
		{
			// Token: 0x06001FB3 RID: 8115 RVA: 0x000526EF File Offset: 0x000508EF
			public OpenApiDocumentValue(IEngineHost host)
				: base(TypeValue.Any, 1, "definition", TypeValue.Binary, "options", OpenApiModule.OpenApiDocumentValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06001FB4 RID: 8116 RVA: 0x00052718 File Offset: 0x00050918
			public override TableValue TypedInvoke(BinaryValue binaryValue, Value options)
			{
				OpenApiUserSettings openApiUserSettings = OpenApiUserSettings.BuildUserSettings(options);
				return new OpenApiTableValue(new OpenApiDocument(JsonModule.Json.Document.Invoke(binaryValue).AsRecord, openApiUserSettings, this.host));
			}

			// Token: 0x17000DE3 RID: 3555
			// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000378A3 File Offset: 0x00035AA3
			public override string PrimaryResourceKind
			{
				get
				{
					return "Web";
				}
			}

			// Token: 0x04000BF6 RID: 3062
			private static readonly TypeValue optionsType = OpenApiModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000BF7 RID: 3063
			private readonly IEngineHost host;
		}
	}
}
