using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Providers
{
	// Token: 0x020000F0 RID: 240
	internal class EdmEntityTypeWithDelayLoadedProperties : EdmEntityType
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x00025824 File Offset: 0x00023A24
		internal EdmEntityTypeWithDelayLoadedProperties(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream, Action<EdmEntityTypeWithDelayLoadedProperties> propertyLoadAction)
			: base(namespaceName, name, baseType, isAbstract, isOpen, hasStream)
		{
			this.propertyLoadAction = propertyLoadAction;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00025848 File Offset: 0x00023A48
		public override IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				this.EnsurePropertyLoaded();
				return base.DeclaredKey;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00025856 File Offset: 0x00023A56
		public override IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				this.EnsurePropertyLoaded();
				return base.DeclaredProperties;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00025864 File Offset: 0x00023A64
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

		// Token: 0x040005DE RID: 1502
		private readonly object lockObject = new object();

		// Token: 0x040005DF RID: 1503
		private Action<EdmEntityTypeWithDelayLoadedProperties> propertyLoadAction;
	}
}
