using System;
using System.Resources;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012DD RID: 4829
	public class DocumentationApplyingModule : DelegatingModule
	{
		// Token: 0x06007FDE RID: 32734 RVA: 0x001B48CB File Offset: 0x001B2ACB
		public DocumentationApplyingModule(Module module, ResourceManager resourceManager)
			: base(module)
		{
			this.resourceManager = resourceManager;
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x001B48DC File Offset: 0x001B2ADC
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			RecordValue recordValue = base.Link(environment, hostEnvironment);
			Value value;
			if (recordValue.TryGetValue("Shared", out value) && value.IsRecord)
			{
				recordValue = RecordValue.Combine(ListValue.New(new Value[]
				{
					recordValue,
					RecordValue.New(DelegatingModule.SharedKeys, new Value[] { this.ApplyDocumentation(value.AsRecord) })
				}));
			}
			return recordValue;
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x001B4944 File Offset: 0x001B2B44
		private RecordValue ApplyDocumentation(RecordValue exports)
		{
			return RecordValue.New(exports.Keys, (int i) => exports[i].AddHelp(exports.Keys[i], this.resourceManager));
		}

		// Token: 0x040045B9 RID: 17849
		private readonly ResourceManager resourceManager;
	}
}
