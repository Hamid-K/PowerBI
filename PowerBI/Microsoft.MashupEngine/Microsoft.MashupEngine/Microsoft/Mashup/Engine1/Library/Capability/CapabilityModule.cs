using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Capability
{
	// Token: 0x02000E59 RID: 3673
	internal sealed class CapabilityModule : Module
	{
		// Token: 0x060062D1 RID: 25297 RVA: 0x001535F7 File Offset: 0x001517F7
		public static RecordValue NewCapability(string name, Value value)
		{
			return RecordValue.New(CapabilityModule.capabilityKeys, new Value[]
			{
				TextValue.New(name),
				value
			});
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060062D2 RID: 25298 RVA: 0x00153616 File Offset: 0x00151816
		public override string Name
		{
			get
			{
				return "CapabilityModule";
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060062D3 RID: 25299 RVA: 0x0015361D File Offset: 0x0015181D
		public override Keys ExportKeys
		{
			get
			{
				if (CapabilityModule.exportKeys == null)
				{
					CapabilityModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "DirectQueryCapabilities.From";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return CapabilityModule.exportKeys;
			}
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x00153655 File Offset: 0x00151855
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return CapabilityModule.DirectQueryCapabilities.From;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040035BC RID: 13756
		private static readonly Keys capabilityKeys = Keys.New("Name", "Value");

		// Token: 0x040035BD RID: 13757
		private static Keys exportKeys;

		// Token: 0x02000E5A RID: 3674
		private enum Exports
		{
			// Token: 0x040035BF RID: 13759
			DirectQueryCapabilities_From,
			// Token: 0x040035C0 RID: 13760
			Count
		}

		// Token: 0x02000E5B RID: 3675
		public static class DirectQueryCapabilities
		{
			// Token: 0x040035C1 RID: 13761
			public static readonly FunctionValue From = FoldableFunctionValue.New(new CapabilityModule.DirectQueryCapabilities.FromFunctionValue());

			// Token: 0x02000E5C RID: 3676
			private sealed class FromFunctionValue : NativeFunctionValue1<TableValue, Value>
			{
				// Token: 0x060062D8 RID: 25304 RVA: 0x001536A8 File Offset: 0x001518A8
				public FromFunctionValue()
					: base(TypeValue.Table, "value", TypeValue.Any)
				{
				}

				// Token: 0x17001CDC RID: 7388
				// (get) Token: 0x060062D9 RID: 25305 RVA: 0x001536BF File Offset: 0x001518BF
				protected override TypeValue ReturnType
				{
					get
					{
						return CapabilityModule.DirectQueryCapabilities.FromFunctionValue.returnType;
					}
				}

				// Token: 0x060062DA RID: 25306 RVA: 0x001536C8 File Offset: 0x001518C8
				public override TableValue TypedInvoke(Value value)
				{
					if (value.IsTable)
					{
						RenameReorderColumnsQuery renameReorderColumnsQuery = value.AsTable.Query as RenameReorderColumnsQuery;
						if (renameReorderColumnsQuery != null)
						{
							return CapabilityModule.DirectQueryCapabilities.From.Invoke(new QueryTableValue(renameReorderColumnsQuery.InnerQuery)).AsTable;
						}
					}
					return ListValue.New(EmptyArray<Value>.Instance).ToTable(CapabilityModule.DirectQueryCapabilities.FromFunctionValue.returnType);
				}

				// Token: 0x040035C2 RID: 13762
				private static readonly Keys resultKeys = Keys.New("Name", "Value");

				// Token: 0x040035C3 RID: 13763
				private static readonly TableTypeValue returnType = TableTypeValue.New(CapabilityModule.DirectQueryCapabilities.FromFunctionValue.resultKeys, null);
			}
		}
	}
}
