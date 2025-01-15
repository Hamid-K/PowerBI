using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000529 RID: 1321
	internal struct InputForComputingCellGroups : IEquatable<InputForComputingCellGroups>, IEqualityComparer<InputForComputingCellGroups>
	{
		// Token: 0x06004137 RID: 16695 RVA: 0x000DCC03 File Offset: 0x000DAE03
		internal InputForComputingCellGroups(EntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			this.ContainerMapping = containerMapping;
			this.Config = config;
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000DCC13 File Offset: 0x000DAE13
		public bool Equals(InputForComputingCellGroups other)
		{
			return this.ContainerMapping.Equals(other.ContainerMapping) && this.Config.Equals(other.Config);
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000DCC3B File Offset: 0x000DAE3B
		public bool Equals(InputForComputingCellGroups one, InputForComputingCellGroups two)
		{
			return one == two || (one != null && two != null && one.Equals(two));
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000DCC67 File Offset: 0x000DAE67
		public int GetHashCode(InputForComputingCellGroups value)
		{
			return value.GetHashCode();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x000DCC76 File Offset: 0x000DAE76
		public override int GetHashCode()
		{
			return this.ContainerMapping.GetHashCode();
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000DCC83 File Offset: 0x000DAE83
		public override bool Equals(object obj)
		{
			return obj is InputForComputingCellGroups && this.Equals((InputForComputingCellGroups)obj);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000DCC9B File Offset: 0x000DAE9B
		public static bool operator ==(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return input1 == input2 || input1.Equals(input2);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000DCCB5 File Offset: 0x000DAEB5
		public static bool operator !=(InputForComputingCellGroups input1, InputForComputingCellGroups input2)
		{
			return !(input1 == input2);
		}

		// Token: 0x0400169B RID: 5787
		internal readonly EntityContainerMapping ContainerMapping;

		// Token: 0x0400169C RID: 5788
		internal readonly ConfigViewGenerator Config;
	}
}
