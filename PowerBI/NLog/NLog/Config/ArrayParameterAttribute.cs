using System;
using JetBrains.Annotations;

namespace NLog.Config
{
	// Token: 0x0200017C RID: 380
	[AttributeUsage(AttributeTargets.Property)]
	[MeansImplicitUse]
	public sealed class ArrayParameterAttribute : Attribute
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x0002D595 File Offset: 0x0002B795
		public ArrayParameterAttribute(Type itemType, string elementName)
		{
			this.ItemType = itemType;
			this.ElementName = elementName;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0002D5AB File Offset: 0x0002B7AB
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0002D5B3 File Offset: 0x0002B7B3
		public Type ItemType { get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0002D5BC File Offset: 0x0002B7BC
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		public string ElementName { get; private set; }
	}
}
