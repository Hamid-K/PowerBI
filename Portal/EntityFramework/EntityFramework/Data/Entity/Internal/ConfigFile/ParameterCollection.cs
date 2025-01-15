using System;
using System.Configuration;
using System.Linq;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000150 RID: 336
	internal class ParameterCollection : ConfigurationElementCollection
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x00038609 File Offset: 0x00036809
		protected override ConfigurationElement CreateNewElement()
		{
			ConfigurationElement configurationElement = new ParameterElement(this._nextKey);
			this._nextKey++;
			return configurationElement;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00038624 File Offset: 0x00036824
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ParameterElement)element).Key;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00038636 File Offset: 0x00036836
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00038639 File Offset: 0x00036839
		protected override string ElementName
		{
			get
			{
				return "parameter";
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00038640 File Offset: 0x00036840
		public virtual object[] GetTypedParameterValues()
		{
			return (from ParameterElement e in this
				select e.GetTypedParameterValue()).ToArray<object>();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00038674 File Offset: 0x00036874
		internal ParameterElement NewElement()
		{
			ConfigurationElement configurationElement = this.CreateNewElement();
			base.BaseAdd(configurationElement);
			return (ParameterElement)configurationElement;
		}

		// Token: 0x040009E9 RID: 2537
		private const string ParameterKey = "parameter";

		// Token: 0x040009EA RID: 2538
		private int _nextKey;
	}
}
