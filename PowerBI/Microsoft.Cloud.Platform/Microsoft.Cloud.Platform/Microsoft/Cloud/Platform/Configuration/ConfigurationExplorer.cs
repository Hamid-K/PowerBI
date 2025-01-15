using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200042C RID: 1068
	public sealed class ConfigurationExplorer
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x0007BE63 File Offset: 0x0007A063
		public ConfigurationExplorer(IConfigurationClass root)
			: this(new IConfigurationClass[] { root })
		{
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0007BE78 File Offset: 0x0007A078
		public ConfigurationExplorer(IEnumerable<IConfigurationClass> roots)
		{
			this.m_elements = new Dictionary<string, ConfigurationPathElement>();
			Queue<ConfigurationPathElement> queue = new Queue<ConfigurationPathElement>(roots.Select((IConfigurationClass c) => new ConfigurationPathElement(null, c, "/" + c.GetType().Name, false, false)));
			while (queue.Any<ConfigurationPathElement>())
			{
				ConfigurationPathElement o = queue.Dequeue();
				if (ConfigurationTypes.IsConfigurationCollection(o.Item))
				{
					int num = 0;
					using (IEnumerator enumerator = ((ICollection)o.Item).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							string listItemName = ConfigurationExplorer.GetListItemName(obj, num++);
							queue.Enqueue(new ConfigurationPathElement(o, obj, o.Path + "/" + listItemName, o.LoadToFile, o.LoadToParent));
						}
						continue;
					}
				}
				if (ConfigurationTypes.IsConfigurationDictionary(o.Item))
				{
					IDictionary dictionary = (IDictionary)o.Item;
					using (IEnumerator enumerator = dictionary.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							queue.Enqueue(new ConfigurationPathElement(o, dictionary[obj2], o.Path + "/" + obj2, o.LoadToFile, o.LoadToParent));
						}
						continue;
					}
				}
				List<PropertyInfo> configurationProperties = ConfigurationPropertyAttribute.GetConfigurationProperties(o.Item);
				if (configurationProperties.Any<PropertyInfo>())
				{
					queue.EnqueueRange(from p in configurationProperties
						select new ConfigurationPathElement(o, p.GetValue(o.Item, null), o.Path + "/" + p.Name, o.LoadToFile || ConfigurationPropertyAttribute.ShouldLoadToFile(p), o.LoadToParent || ConfigurationPropertyAttribute.ShouldLoadToParent(p), ConfigurationPropertyAttribute.ShouldEncrypt(p), ConfigurationPropertyAttribute.ContainsValidClientId(p), ConfigurationPropertyAttribute.ShouldBeOnlyLowercase(p)) into x
						where x.Item != null
						select x);
				}
				else
				{
					this.m_elements.Add(o.Path, o);
				}
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0007C0C4 File Offset: 0x0007A2C4
		public object GetValue(ConfigurationPathElement element)
		{
			if (!ConfigurationTypes.IsConfigurationCollection(element.Parent.Item))
			{
				return element.GetProperty().GetValue(element.Parent.Item, null);
			}
			return ConfigurationExplorer.GetListItemValue(element.Parent.Item as IList, element.Name);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0007C118 File Offset: 0x0007A318
		public void SetValue(ConfigurationPathElement element, object value)
		{
			if (ConfigurationTypes.IsConfigurationCollection(element.Parent.Item))
			{
				ConfigurationExplorer.SetListItemValue(element.Parent.Item as IList, element.Name, value);
				return;
			}
			if (ConfigurationTypes.IsConfigurationDictionary(element.Parent.Item))
			{
				throw new NotImplementedException("ConfigurationExplorer.SetValue does not yet support ConfigurationDictionary items");
			}
			element.GetProperty().SetValue(element.Parent.Item, value, null);
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007C189 File Offset: 0x0007A389
		public IEnumerable<ConfigurationPathElement> Items
		{
			get
			{
				return this.m_elements.Values;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0007C198 File Offset: 0x0007A398
		private static bool HasAttribute(PropertyInfo p, Type attribute)
		{
			return p.GetCustomAttributes(false).Any((object a) => a.GetType().Equals(attribute));
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0007C1CC File Offset: 0x0007A3CC
		private static string GetListItemName(object item, int index)
		{
			PropertyInfo propertyInfo = ConfigurationPropertyAttribute.GetConfigurationProperties(item).FirstOrDefault((PropertyInfo property) => ConfigurationExplorer.HasAttribute(property, typeof(ItemPathComponentAttribute)));
			if (!(propertyInfo != null))
			{
				return index.ToString(CultureInfo.InvariantCulture);
			}
			return propertyInfo.GetValue(item, null).ToString();
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0007C228 File Offset: 0x0007A428
		private static int GetListItemIndex(IList list, string itemName)
		{
			int num = 0;
			using (IEnumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ConfigurationExplorer.GetListItemName(enumerator.Current, num).Equals(itemName))
					{
						return num;
					}
					num++;
				}
			}
			throw new ArgumentException("ConfigurationExplorer: item with name '{0}' does not exist in Items");
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0007C294 File Offset: 0x0007A494
		private static void SetListItemValue(IList list, string itemName, object item)
		{
			int listItemIndex = ConfigurationExplorer.GetListItemIndex(list, itemName);
			list[listItemIndex] = item;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0007C2B4 File Offset: 0x0007A4B4
		private static object GetListItemValue(IList list, string itemName)
		{
			int listItemIndex = ConfigurationExplorer.GetListItemIndex(list, itemName);
			return list[listItemIndex];
		}

		// Token: 0x04000B4A RID: 2890
		private readonly Dictionary<string, ConfigurationPathElement> m_elements;

		// Token: 0x04000B4B RID: 2891
		private const string c_separator = "/";
	}
}
