using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x0200012B RID: 299
	internal sealed class DataTransformReferenceMap
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0002BFAB File Offset: 0x0002A1AB
		private DataTransformReferenceMap()
			: this(new Dictionary<DataTransformTableColumn, DataTransformColumnReferrers>())
		{
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		internal DataTransformReferenceMap(IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnReferrers> referrersByColumn)
		{
			this.m_referrersByColumn = referrersByColumn;
			DataTransformReferenceMap.PopulateReferringItems(referrersByColumn, out this.m_referringGroups, out this.m_referringCalculations);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		private static void PopulateReferringItems(IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnReferrers> referrersByColumn, out HashSet<DataMember> referringGroups, out HashSet<Calculation> referringCalculations)
		{
			referringGroups = new HashSet<DataMember>();
			referringCalculations = new HashSet<Calculation>();
			foreach (KeyValuePair<DataTransformTableColumn, DataTransformColumnReferrers> keyValuePair in referrersByColumn)
			{
				DataTransformColumnReferrers value = keyValuePair.Value;
				foreach (DataMember dataMember in value.Groups)
				{
					referringGroups.Add(dataMember);
				}
				foreach (Calculation calculation in value.Calculations)
				{
					referringCalculations.Add(calculation);
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		public IReadOnlyList<Expression> GetReferringExpressions(DataTransformTableColumn column)
		{
			DataTransformColumnReferrers dataTransformColumnReferrers;
			if (this.m_referrersByColumn.TryGetValue(column, out dataTransformColumnReferrers))
			{
				return dataTransformColumnReferrers.Expressions;
			}
			return null;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
		public IReadOnlyList<Calculation> GetReferringCalculations(DataTransformTableColumn column)
		{
			DataTransformColumnReferrers dataTransformColumnReferrers;
			if (this.m_referrersByColumn.TryGetValue(column, out dataTransformColumnReferrers))
			{
				return dataTransformColumnReferrers.Calculations;
			}
			return null;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002C10C File Offset: 0x0002A30C
		public IReadOnlyList<DataMember> GetReferringGroups(DataTransformTableColumn column)
		{
			DataTransformColumnReferrers dataTransformColumnReferrers;
			if (this.m_referrersByColumn.TryGetValue(column, out dataTransformColumnReferrers))
			{
				return dataTransformColumnReferrers.Groups;
			}
			return null;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002C131 File Offset: 0x0002A331
		public bool HasDataTransformColumnReference(DataMember member)
		{
			return this.m_referringGroups.Contains(member);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002C13F File Offset: 0x0002A33F
		public bool HasDataTransformColumnReference(Calculation calculation)
		{
			return this.m_referringCalculations.Contains(calculation);
		}

		// Token: 0x040005AD RID: 1453
		internal static readonly DataTransformReferenceMap Empty = new DataTransformReferenceMap();

		// Token: 0x040005AE RID: 1454
		private readonly IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnReferrers> m_referrersByColumn;

		// Token: 0x040005AF RID: 1455
		private readonly HashSet<DataMember> m_referringGroups;

		// Token: 0x040005B0 RID: 1456
		private readonly HashSet<Calculation> m_referringCalculations;
	}
}
