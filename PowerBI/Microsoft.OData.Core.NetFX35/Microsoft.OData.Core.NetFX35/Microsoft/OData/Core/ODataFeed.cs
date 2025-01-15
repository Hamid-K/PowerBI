using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x02000173 RID: 371
	public sealed class ODataFeed : ODataFeedBase
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000315E4 File Offset: 0x0002F7E4
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000315EC File Offset: 0x0002F7EC
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000315F4 File Offset: 0x0002F7F4
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x000315FC File Offset: 0x0002F7FC
		internal ODataFeedAndEntrySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataFeedAndEntrySerializationInfo.Validate(value);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003160A File Offset: 0x0002F80A
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00031631 File Offset: 0x0002F831
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x040005EE RID: 1518
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x040005EF RID: 1519
		private List<ODataFunction> functions = new List<ODataFunction>();

		// Token: 0x040005F0 RID: 1520
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
