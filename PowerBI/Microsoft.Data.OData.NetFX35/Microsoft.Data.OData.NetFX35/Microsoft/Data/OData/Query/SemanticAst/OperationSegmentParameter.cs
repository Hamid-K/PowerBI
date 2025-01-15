using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000073 RID: 115
	public sealed class OperationSegmentParameter : ODataAnnotatable
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000A781 File Offset: 0x00008981
		public OperationSegmentParameter(string name, object value)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A7A2 File Offset: 0x000089A2
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000A7AA File Offset: 0x000089AA
		public string Name { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A7B3 File Offset: 0x000089B3
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000A7BB File Offset: 0x000089BB
		public object Value { get; private set; }
	}
}
