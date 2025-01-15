using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200001B RID: 27
	internal class PropertyCacheHandler
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000038AC File Offset: 0x00001AAC
		public PropertySerializationInfo GetProperty(IEdmModel model, string name, IEdmStructuredType owningType)
		{
			int num = this.currentResourceScopeLevel - this.resourceSetScopeLevel;
			string text = ((num == 1) ? string.Empty : num.ToString(CultureInfo.InvariantCulture));
			string text2 = ((owningType != null) ? (owningType.FullTypeName() + "-" + text + name) : (text + name));
			return this.currentPropertyCache.GetProperty(model, name, text2, owningType);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000390D File Offset: 0x00001B0D
		public void SetCurrentResourceScopeLevel(int level)
		{
			this.currentResourceScopeLevel = level;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003918 File Offset: 0x00001B18
		public void EnterResourceSetScope(IEdmStructuredType resourceType, int scopeLevel)
		{
			PropertyCache propertyCache;
			if (resourceType != null)
			{
				if (!this.cacheDictionary.TryGetValue(resourceType, out propertyCache))
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

		// Token: 0x0600012F RID: 303 RVA: 0x00003983 File Offset: 0x00001B83
		public void LeaveResourceSetScope()
		{
			this.resourceSetScopeLevel = this.scopeLevelStack.Pop();
			this.currentPropertyCache = this.cacheStack.Pop();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000039A7 File Offset: 0x00001BA7
		public bool InResourceSetScope()
		{
			return this.resourceSetScopeLevel > 0;
		}

		// Token: 0x04000046 RID: 70
		private const string PropertyTypeDelimiter = "-";

		// Token: 0x04000047 RID: 71
		private readonly Stack<PropertyCache> cacheStack = new Stack<PropertyCache>();

		// Token: 0x04000048 RID: 72
		private readonly Stack<int> scopeLevelStack = new Stack<int>();

		// Token: 0x04000049 RID: 73
		private readonly Dictionary<IEdmStructuredType, PropertyCache> cacheDictionary = new Dictionary<IEdmStructuredType, PropertyCache>();

		// Token: 0x0400004A RID: 74
		private PropertyCache currentPropertyCache;

		// Token: 0x0400004B RID: 75
		private int resourceSetScopeLevel;

		// Token: 0x0400004C RID: 76
		private int currentResourceScopeLevel;
	}
}
