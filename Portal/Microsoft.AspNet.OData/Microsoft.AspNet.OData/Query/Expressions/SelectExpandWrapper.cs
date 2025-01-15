using System;
using Newtonsoft.Json;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E2 RID: 226
	[JsonConverter(typeof(SelectExpandWrapperConverter))]
	internal class SelectExpandWrapper<TElement> : SelectExpandWrapper
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0001B8CA File Offset: 0x00019ACA
		public TElement Instance
		{
			get
			{
				return (TElement)((object)base.UntypedInstance);
			}
			set
			{
				base.UntypedInstance = value;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		protected override Type GetElementType()
		{
			if (base.UntypedInstance != null)
			{
				return base.UntypedInstance.GetType();
			}
			return typeof(TElement);
		}
	}
}
