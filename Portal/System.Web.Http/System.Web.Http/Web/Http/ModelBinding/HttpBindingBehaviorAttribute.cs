using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200005B RID: 91
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class HttpBindingBehaviorAttribute : Attribute
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00007993 File Offset: 0x00005B93
		public HttpBindingBehaviorAttribute(HttpBindingBehavior behavior)
		{
			this.Behavior = behavior;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000079A2 File Offset: 0x00005BA2
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000079AA File Offset: 0x00005BAA
		public HttpBindingBehavior Behavior { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000079B3 File Offset: 0x00005BB3
		public override object TypeId
		{
			get
			{
				return HttpBindingBehaviorAttribute._typeId;
			}
		}

		// Token: 0x0400008B RID: 139
		private static readonly object _typeId = new object();
	}
}
