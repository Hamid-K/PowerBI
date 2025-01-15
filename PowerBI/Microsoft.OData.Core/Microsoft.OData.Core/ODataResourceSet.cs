using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000085 RID: 133
	public sealed class ODataResourceSet : ODataResourceSetBase
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000C05C File Offset: 0x0000A25C
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000C064 File Offset: 0x0000A264
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000C06C File Offset: 0x0000A26C
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000C094 File Offset: 0x0000A294
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x04000210 RID: 528
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x04000211 RID: 529
		private List<ODataFunction> functions = new List<ODataFunction>();
	}
}
