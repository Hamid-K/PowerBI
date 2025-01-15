using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000183 RID: 387
	internal class Factory<TBaseType, TAttributeType> : INamedItemFactory<TBaseType, Type>, IFactory where TBaseType : class where TAttributeType : NameBaseAttribute
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
		internal Factory(ConfigurationItemFactory parentFactory)
		{
			this._parentFactory = parentFactory;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0002E104 File Offset: 0x0002C304
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

		// Token: 0x060011B2 RID: 4530 RVA: 0x0002E168 File Offset: 0x0002C368
		public void RegisterType(Type type, string itemNamePrefix)
		{
			IEnumerable<TAttributeType> customAttributes = type.GetCustomAttributes(false);
			if (customAttributes != null)
			{
				foreach (TAttributeType tattributeType in customAttributes)
				{
					this.RegisterDefinition(itemNamePrefix + tattributeType.Name, type);
				}
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		public void RegisterNamedType(string itemName, string typeName)
		{
			this._items[itemName] = () => Type.GetType(typeName, false);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0002E1FE File Offset: 0x0002C3FE
		public void Clear()
		{
			this._items.Clear();
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0002E20C File Offset: 0x0002C40C
		public void RegisterDefinition(string itemName, Type itemDefinition)
		{
			this._items[itemName] = () => itemDefinition;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0002E240 File Offset: 0x0002C440
		public bool TryGetDefinition(string itemName, out Type result)
		{
			Factory<TBaseType, TAttributeType>.GetTypeDelegate getTypeDelegate;
			if (!this._items.TryGetValue(itemName, out getTypeDelegate))
			{
				result = null;
				return false;
			}
			bool flag;
			try
			{
				result = getTypeDelegate();
				flag = result != null;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0002E298 File Offset: 0x0002C498
		public virtual bool TryCreateInstance(string itemName, out TBaseType result)
		{
			Type type;
			if (!this.TryGetDefinition(itemName, out type))
			{
				result = default(TBaseType);
				return false;
			}
			result = (TBaseType)((object)this._parentFactory.CreateInstance(type));
			return true;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0002E2D8 File Offset: 0x0002C4D8
		public virtual TBaseType CreateInstance(string itemName)
		{
			TBaseType tbaseType;
			if (this.TryCreateInstance(itemName, out tbaseType))
			{
				return tbaseType;
			}
			string text = typeof(TBaseType).Name + " cannot be found: '" + itemName + "'";
			if (itemName != null && (itemName.StartsWith("aspnet", StringComparison.OrdinalIgnoreCase) || itemName.StartsWith("iis", StringComparison.OrdinalIgnoreCase)))
			{
				text += ". Is NLog.Web not included?";
			}
			throw new ArgumentException(text);
		}

		// Token: 0x040004D6 RID: 1238
		private readonly Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate> _items = new Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040004D7 RID: 1239
		private readonly ConfigurationItemFactory _parentFactory;

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x060016F9 RID: 5881
		private delegate Type GetTypeDelegate();
	}
}
