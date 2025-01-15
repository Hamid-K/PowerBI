using System;

namespace AngleSharp.Attributes
{
	// Token: 0x02000414 RID: 1044
	[AttributeUsage(AttributeTargets.Property, Inherited = false)]
	public sealed class DomPutForwardsAttribute : Attribute
	{
		// Token: 0x06002115 RID: 8469 RVA: 0x000588B2 File Offset: 0x00056AB2
		public DomPutForwardsAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000588C1 File Offset: 0x00056AC1
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x000588C9 File Offset: 0x00056AC9
		public string PropertyName { get; private set; }
	}
}
