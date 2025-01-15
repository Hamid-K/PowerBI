using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B43 RID: 2883
	internal class PropertyNode
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x001774FB File Offset: 0x001756FB
		// (set) Token: 0x06005B28 RID: 23336 RVA: 0x00177503 File Offset: 0x00175703
		internal string Name { get; set; }

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0017750C File Offset: 0x0017570C
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x00177514 File Offset: 0x00175714
		internal PropertyValueDefinition Value { get; set; }

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0017751D File Offset: 0x0017571D
		internal Dictionary<string, PropertyNode> Children { get; } = new Dictionary<string, PropertyNode>();

		// Token: 0x06005B2C RID: 23340 RVA: 0x00177525 File Offset: 0x00175725
		internal PropertyNode(string name, PropertyValueDefinition propertyValue)
		{
			this.Name = name;
			this.Value = propertyValue;
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x00177548 File Offset: 0x00175748
		internal void AddProperty(PropertyValueDefinition pvd)
		{
			string[] array = pvd.FullName.Split(new char[] { '.' });
			PropertyNode propertyNode = this;
			for (int i = 1; i < array.Length; i++)
			{
				PropertyNode propertyNode2;
				if (!propertyNode.Children.TryGetValue(array[i], out propertyNode2))
				{
					propertyNode2 = new PropertyNode(array[i], null);
					propertyNode.Children.Add(array[i], propertyNode2);
				}
				propertyNode = propertyNode2;
			}
			propertyNode.Value = pvd;
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x001775B0 File Offset: 0x001757B0
		internal void AddToXml(StringBuilder sb)
		{
			foreach (PropertyNode propertyNode in this.Children.Values)
			{
				propertyNode.AddNodeToXml(sb);
			}
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x00177608 File Offset: 0x00175808
		internal void AddNodeToXml(StringBuilder sb)
		{
			if (this.Children.Count == 0)
			{
				this.Value.AddToXml(sb);
				return;
			}
			sb.Append("<" + this.Name + ">");
			foreach (PropertyNode propertyNode in this.Children.Values)
			{
				propertyNode.AddNodeToXml(sb);
			}
			sb.Append("</" + this.Name + ">");
		}
	}
}
