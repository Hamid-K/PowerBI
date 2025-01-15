using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x02000248 RID: 584
	public sealed class DataSourceModule : Module
	{
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x00031C2E File Offset: 0x0002FE2E
		public override string Name
		{
			get
			{
				return "DataSource";
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00031C35 File Offset: 0x0002FE35
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
							return "DataSource.TestConnection";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00031C70 File Offset: 0x0002FE70
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new DataSourceModule.DatabaseFunctionValue();
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040006C4 RID: 1732
		private const string DataSourceTestConnection = "DataSource.TestConnection";

		// Token: 0x040006C5 RID: 1733
		private Keys exportKeys;

		// Token: 0x02000249 RID: 585
		private enum Exports
		{
			// Token: 0x040006C7 RID: 1735
			TestConnection,
			// Token: 0x040006C8 RID: 1736
			Count
		}

		// Token: 0x0200024A RID: 586
		private sealed class DatabaseFunctionValue : NativeFunctionValue1<LogicalValue, Value>
		{
			// Token: 0x0600193B RID: 6459 RVA: 0x00031C9C File Offset: 0x0002FE9C
			public DatabaseFunctionValue()
				: base(TypeValue.Logical, 1, "dataSource", TypeValue.Any)
			{
			}

			// Token: 0x0600193C RID: 6460 RVA: 0x00031CB4 File Offset: 0x0002FEB4
			public override LogicalValue TypedInvoke(Value dataSource)
			{
				dataSource.TestConnection();
				return LogicalValue.True;
			}
		}
	}
}
