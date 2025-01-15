using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Providers
{
	// Token: 0x020000EF RID: 239
	internal class EdmComplexTypeWithDelayLoadedProperties : EdmComplexType
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x00025799 File Offset: 0x00023999
		internal EdmComplexTypeWithDelayLoadedProperties(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen, Action<EdmComplexTypeWithDelayLoadedProperties> propertyLoadAction)
			: base(namespaceName, name, baseType, isAbstract, isOpen)
		{
			this.propertyLoadAction = propertyLoadAction;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000257BB File Offset: 0x000239BB
		public override IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				this.EnsurePropertyLoaded();
				return base.DeclaredProperties;
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000257CC File Offset: 0x000239CC
		private void EnsurePropertyLoaded()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.propertyLoadAction != null)
				{
					this.propertyLoadAction(this);
					this.propertyLoadAction = null;
				}
			}
		}

		// Token: 0x040005DC RID: 1500
		private readonly object lockObject = new object();

		// Token: 0x040005DD RID: 1501
		private Action<EdmComplexTypeWithDelayLoadedProperties> propertyLoadAction;
	}
}
