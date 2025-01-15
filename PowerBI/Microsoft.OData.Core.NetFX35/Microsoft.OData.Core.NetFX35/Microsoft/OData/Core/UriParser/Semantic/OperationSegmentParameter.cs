using System;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000253 RID: 595
	public sealed class OperationSegmentParameter : ODataAnnotatable
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0004ADAF File Offset: 0x00048FAF
		public OperationSegmentParameter(string name, object value)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0004ADD0 File Offset: 0x00048FD0
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		public string Name { get; private set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0004ADE1 File Offset: 0x00048FE1
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x0004ADE9 File Offset: 0x00048FE9
		public object Value { get; private set; }
	}
}
