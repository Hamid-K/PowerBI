using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Delta
{
	// Token: 0x02000CBD RID: 3261
	public sealed class DeltaModule : Module
	{
		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x00133C8C File Offset: 0x00131E8C
		public override string Name
		{
			get
			{
				return "Delta";
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x00133C93 File Offset: 0x00131E93
		public override Keys ExportKeys
		{
			get
			{
				if (DeltaModule.exportKeys == null)
				{
					DeltaModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Delta.Since";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return DeltaModule.exportKeys;
			}
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x00133CCB File Offset: 0x00131ECB
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return DeltaModule.Delta.Since;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040031B9 RID: 12729
		public const string DeltaTag = "Delta.Tag";

		// Token: 0x040031BA RID: 12730
		public const string PathKey = "Path";

		// Token: 0x040031BB RID: 12731
		public const string ValueKey = "Value";

		// Token: 0x040031BC RID: 12732
		private static Keys exportKeys;

		// Token: 0x02000CBE RID: 3262
		private enum Exports
		{
			// Token: 0x040031BE RID: 12734
			Delta_Since,
			// Token: 0x040031BF RID: 12735
			Count
		}

		// Token: 0x02000CBF RID: 3263
		public static class Delta
		{
			// Token: 0x040031C0 RID: 12736
			public static readonly FunctionValue Since = new DeltaModule.Delta.SinceFunctionValue();

			// Token: 0x02000CC0 RID: 3264
			private sealed class SinceFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06005826 RID: 22566 RVA: 0x00133D03 File Offset: 0x00131F03
				public SinceFunctionValue()
					: base(TypeValue.Table, 1, "table", TypeValue.Table, "tag", TypeValue.Any)
				{
				}

				// Token: 0x06005827 RID: 22567 RVA: 0x00133D25 File Offset: 0x00131F25
				public override TableValue TypedInvoke(TableValue table, Value tag)
				{
					return table.DeltaSince(tag);
				}
			}
		}
	}
}
