using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001720 RID: 5920
	public abstract class Assembly : IAssembly
	{
		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x06009668 RID: 38504
		public abstract FunctionValue Function { get; }

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x06009669 RID: 38505
		public abstract RecordValue Exports { get; }

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x0600966A RID: 38506 RVA: 0x001F25C9 File Offset: 0x001F07C9
		IFunctionValue IAssembly.Function
		{
			get
			{
				return this.Function;
			}
		}

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x0600966B RID: 38507 RVA: 0x001F25D1 File Offset: 0x001F07D1
		IRecordValue IAssembly.Exports
		{
			get
			{
				return this.Exports;
			}
		}
	}
}
