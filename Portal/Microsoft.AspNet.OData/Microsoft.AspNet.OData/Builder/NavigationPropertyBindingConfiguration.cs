using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000134 RID: 308
	public class NavigationPropertyBindingConfiguration
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x0002B126 File Offset: 0x00029326
		public NavigationPropertyBindingConfiguration(NavigationPropertyConfiguration navigationProperty, NavigationSourceConfiguration navigationSource)
			: this(navigationProperty, navigationSource, new MemberInfo[] { navigationProperty.PropertyInfo })
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002B140 File Offset: 0x00029340
		public NavigationPropertyBindingConfiguration(NavigationPropertyConfiguration navigationProperty, NavigationSourceConfiguration navigationSource, IList<MemberInfo> path)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			if (navigationSource == null)
			{
				throw Error.ArgumentNull("navigationSource");
			}
			if (path == null)
			{
				throw Error.ArgumentNull("path");
			}
			this.NavigationProperty = navigationProperty;
			this.TargetNavigationSource = navigationSource;
			this.Path = path;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002B192 File Offset: 0x00029392
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0002B19A File Offset: 0x0002939A
		public NavigationPropertyConfiguration NavigationProperty { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002B1A3 File Offset: 0x000293A3
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0002B1AB File Offset: 0x000293AB
		public NavigationSourceConfiguration TargetNavigationSource { get; private set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002B1B4 File Offset: 0x000293B4
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002B1BC File Offset: 0x000293BC
		public IList<MemberInfo> Path { get; private set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public string BindingPath
		{
			get
			{
				return this.Path.ConvertBindingPath();
			}
		}
	}
}
