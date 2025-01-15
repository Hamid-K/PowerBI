using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000328 RID: 808
	[DomNoInterfaceObject]
	public interface ICssProperties : IEnumerable<ICssProperty>, IEnumerable
	{
		// Token: 0x17000613 RID: 1555
		string this[string propertyName] { get; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001721 RID: 5921
		[DomName("length")]
		int Length { get; }

		// Token: 0x06001722 RID: 5922
		[DomName("getPropertyValue")]
		string GetPropertyValue(string propertyName);

		// Token: 0x06001723 RID: 5923
		[DomName("getPropertyPriority")]
		string GetPropertyPriority(string propertyName);

		// Token: 0x06001724 RID: 5924
		[DomName("setProperty")]
		void SetProperty(string propertyName, string propertyValue, string priority = null);

		// Token: 0x06001725 RID: 5925
		[DomName("removeProperty")]
		string RemoveProperty(string propertyName);
	}
}
