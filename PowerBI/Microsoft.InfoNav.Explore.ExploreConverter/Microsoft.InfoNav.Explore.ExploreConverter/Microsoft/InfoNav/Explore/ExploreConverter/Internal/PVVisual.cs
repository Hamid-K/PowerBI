using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000027 RID: 39
	[DataContract]
	internal sealed class PVVisual
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003F45 File Offset: 0x00002145
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003F4D File Offset: 0x0000214D
		[DataMember]
		public string Type { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003F56 File Offset: 0x00002156
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003F5E File Offset: 0x0000215E
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003F67 File Offset: 0x00002167
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003F6F File Offset: 0x0000216F
		[DataMember]
		public int ZIndex { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003F78 File Offset: 0x00002178
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003F80 File Offset: 0x00002180
		[DataMember]
		public List<PVVisual> Visuals { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003F89 File Offset: 0x00002189
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003F91 File Offset: 0x00002191
		[DataMember]
		public LayoutContext LayoutContext { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003F9A File Offset: 0x0000219A
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00003FA2 File Offset: 0x000021A2
		[DataMember]
		public DataContext DataContext { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003FAB File Offset: 0x000021AB
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00003FB3 File Offset: 0x000021B3
		[DataMember]
		public PVFrame Frame { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003FBC File Offset: 0x000021BC
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00003FC4 File Offset: 0x000021C4
		[DataMember]
		public List<PVProperty> Properties { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003FCD File Offset: 0x000021CD
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00003FD5 File Offset: 0x000021D5
		public PVVisual ParentVisual { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003FDE File Offset: 0x000021DE
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003FE6 File Offset: 0x000021E6
		public bool ParentIsDataContainer { get; set; }

		// Token: 0x060000FD RID: 253 RVA: 0x00003FEF File Offset: 0x000021EF
		public void AddVisual(PVVisual childVisual, bool parentIsDataContainer = false)
		{
			this.Visuals.Add(childVisual);
			childVisual.ParentVisual = this;
			childVisual.ParentIsDataContainer = parentIsDataContainer;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000400C File Offset: 0x0000220C
		public CustomPVProperties GetValue(string name)
		{
			IEnumerable<PVProperty> enumerable = this.Properties.Where((PVProperty prop) => prop.Name == name);
			if (enumerable.Count<PVProperty>() > 0)
			{
				return enumerable.First<PVProperty>().Value;
			}
			return null;
		}
	}
}
