using System;
using System.Collections.Generic;
using System.Reflection;
using NLog.Common;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000195 RID: 405
	internal class MethodFactory<TClassAttributeType, TMethodAttributeType> : INamedItemFactory<MethodInfo, MethodInfo>, IFactory where TClassAttributeType : Attribute where TMethodAttributeType : NameBaseAttribute
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00032804 File Offset: 0x00030A04
		public IDictionary<string, MethodInfo> AllRegisteredItems
		{
			get
			{
				return this._nameToMethodInfo;
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003280C File Offset: 0x00030A0C
		public void ScanTypes(Type[] types, string prefix)
		{
			foreach (Type type in types)
			{
				try
				{
					this.RegisterType(type, prefix);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to add type '{0}'.", new object[] { type.FullName });
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00032870 File Offset: 0x00030A70
		public void RegisterType(Type type, string itemNamePrefix)
		{
			if (type.IsDefined(typeof(TClassAttributeType), false))
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					foreach (TMethodAttributeType tmethodAttributeType in (TMethodAttributeType[])methodInfo.GetCustomAttributes(typeof(TMethodAttributeType), false))
					{
						this.RegisterDefinition(itemNamePrefix + tmethodAttributeType.Name, methodInfo);
					}
				}
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000328F5 File Offset: 0x00030AF5
		public void Clear()
		{
			this._nameToMethodInfo.Clear();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00032902 File Offset: 0x00030B02
		public void RegisterDefinition(string itemName, MethodInfo itemDefinition)
		{
			this._nameToMethodInfo[itemName] = itemDefinition;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00032911 File Offset: 0x00030B11
		public bool TryCreateInstance(string itemName, out MethodInfo result)
		{
			return this._nameToMethodInfo.TryGetValue(itemName, out result);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00032920 File Offset: 0x00030B20
		public MethodInfo CreateInstance(string itemName)
		{
			MethodInfo methodInfo;
			if (this.TryCreateInstance(itemName, out methodInfo))
			{
				return methodInfo;
			}
			throw new NLogConfigurationException("Unknown function: '" + itemName + "'");
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003294F File Offset: 0x00030B4F
		public bool TryGetDefinition(string itemName, out MethodInfo result)
		{
			return this._nameToMethodInfo.TryGetValue(itemName, out result);
		}

		// Token: 0x040004FE RID: 1278
		private readonly Dictionary<string, MethodInfo> _nameToMethodInfo = new Dictionary<string, MethodInfo>();
	}
}
