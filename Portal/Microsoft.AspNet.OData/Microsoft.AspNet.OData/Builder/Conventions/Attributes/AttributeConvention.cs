using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000177 RID: 375
	internal abstract class AttributeConvention : IConvention
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x00031D09 File Offset: 0x0002FF09
		protected AttributeConvention(Func<Attribute, bool> attributeFilter, bool allowMultiple)
		{
			if (attributeFilter == null)
			{
				throw Error.ArgumentNull("attributeFilter");
			}
			this.AllowMultiple = allowMultiple;
			this.AttributeFilter = attributeFilter;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00031D2D File Offset: 0x0002FF2D
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00031D35 File Offset: 0x0002FF35
		public Func<Attribute, bool> AttributeFilter { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00031D3E File Offset: 0x0002FF3E
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00031D46 File Offset: 0x0002FF46
		public bool AllowMultiple { get; private set; }

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00031D50 File Offset: 0x0002FF50
		public Attribute[] GetAttributes(MemberInfo member)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			Attribute[] array = member.GetCustomAttributes(true).OfType<Attribute>().Where(this.AttributeFilter)
				.ToArray<Attribute>();
			if (!this.AllowMultiple && array.Length > 1)
			{
				throw Error.Argument("member", SRResources.MultipleAttributesFound, new object[]
				{
					member.Name,
					TypeHelper.GetReflectedType(member).Name,
					array.First<Attribute>().GetType().Name
				});
			}
			return array;
		}
	}
}
