using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000C8 RID: 200
	internal class PropertyCacheHandler
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x00015864 File Offset: 0x00013A64
		public PropertySerializationInfo GetProperty(string name, IEdmStructuredType owningType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (owningType != null)
			{
				stringBuilder.Append(owningType.FullTypeName());
				stringBuilder.Append("-");
			}
			stringBuilder.Append(name);
			if (this.currentResourceScopeLevel != this.resourceSetScopeLevel + 1)
			{
				stringBuilder.Append(this.currentResourceScopeLevel - this.resourceSetScopeLevel);
			}
			return this.currentPropertyCache.GetProperty(name, stringBuilder.ToString(), owningType);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000158D2 File Offset: 0x00013AD2
		public void SetCurrentResourceScopeLevel(int level)
		{
			this.currentResourceScopeLevel = level;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000158DC File Offset: 0x00013ADC
		public void EnterResourceSetScope(IEdmStructuredType resourceType, int scopeLevel)
		{
			PropertyCache propertyCache;
			if (resourceType != null)
			{
				if (!this.cacheDictionary.TryGetValue(resourceType, ref propertyCache))
				{
					propertyCache = new PropertyCache();
					this.cacheDictionary[resourceType] = propertyCache;
				}
			}
			else
			{
				propertyCache = new PropertyCache();
			}
			this.cacheStack.Push(this.currentPropertyCache);
			this.currentPropertyCache = propertyCache;
			this.scopeLevelStack.Push(this.resourceSetScopeLevel);
			this.resourceSetScopeLevel = scopeLevel;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00015947 File Offset: 0x00013B47
		public void LeaveResourceSetScope()
		{
			this.resourceSetScopeLevel = this.scopeLevelStack.Pop();
			this.currentPropertyCache = this.cacheStack.Pop();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001596B File Offset: 0x00013B6B
		public bool InResourceSetScope()
		{
			return this.resourceSetScopeLevel > 0;
		}

		// Token: 0x04000315 RID: 789
		private readonly Stack<PropertyCache> cacheStack = new Stack<PropertyCache>();

		// Token: 0x04000316 RID: 790
		private readonly Stack<int> scopeLevelStack = new Stack<int>();

		// Token: 0x04000317 RID: 791
		private readonly Dictionary<IEdmStructuredType, PropertyCache> cacheDictionary = new Dictionary<IEdmStructuredType, PropertyCache>();

		// Token: 0x04000318 RID: 792
		private PropertyCache currentPropertyCache;

		// Token: 0x04000319 RID: 793
		private int resourceSetScopeLevel;

		// Token: 0x0400031A RID: 794
		private int currentResourceScopeLevel;
	}
}
