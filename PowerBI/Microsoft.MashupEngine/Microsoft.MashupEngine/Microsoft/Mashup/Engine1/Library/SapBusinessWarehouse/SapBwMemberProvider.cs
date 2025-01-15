using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004BF RID: 1215
	internal class SapBwMemberProvider
	{
		// Token: 0x060027DA RID: 10202 RVA: 0x000756FC File Offset: 0x000738FC
		public SapBwMemberProvider(SapBwVariableValueProvider provider, long? startRow, string hierarchyUniqueNameOverride = null)
		{
			this.provider = provider;
			this.startRow = startRow;
			if (this.startRow != null)
			{
				this.endRow = startRow.Value + 2500L - 1L;
			}
			this.hasMoreValues = true;
			this.hierarchyUniqueNameOverride = hierarchyUniqueNameOverride;
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x0007574F File Offset: 0x0007394F
		public long? StartRow
		{
			get
			{
				return this.startRow;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x00075757 File Offset: 0x00073957
		public long EndRow
		{
			get
			{
				return this.endRow;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x0007575F File Offset: 0x0007395F
		public bool HasMoreValues
		{
			get
			{
				this.EnsureBuffered();
				return this.hasMoreValues;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x0007576D File Offset: 0x0007396D
		public bool HasValues
		{
			get
			{
				this.EnsureBuffered();
				return this.cachedValues.Count > 0;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x00075783 File Offset: 0x00073983
		public int ValueCount
		{
			get
			{
				this.EnsureBuffered();
				return this.cachedValues.Count;
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x00075796 File Offset: 0x00073996
		public IEnumerable<IValueReference> GetValues(long offset = 0L)
		{
			this.EnsureBuffered();
			return this.cachedValues.SkipLong(offset);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000757AC File Offset: 0x000739AC
		private void EnsureBuffered()
		{
			if (this.cachedValues == null)
			{
				this.cachedValues = new List<IValueReference>();
				if (this.provider.Variable.Dimension != null)
				{
					using (IDataReader dataReader = this.provider.Service.ExtractMetadata("BAPI_MDPROVIDER_GET_MEMBERS", "MEMBERS", this.GetMembersRestrictions()))
					{
						int num = 0;
						while (dataReader.Read())
						{
							string uniqueNameForValidMember = this.GetUniqueNameForValidMember(dataReader);
							if (uniqueNameForValidMember != null)
							{
								Value value = TextValue.New(uniqueNameForValidMember);
								value = NavigationTableServices.AddCaption(value, dataReader[5] as string);
								this.cachedValues.Add(value);
							}
							num++;
						}
						this.hasMoreValues = num >= 2500;
					}
				}
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x00075870 File Offset: 0x00073A70
		private SapBwRestrictions GetMembersRestrictions()
		{
			SapBwRestrictions sapBwRestrictions = new SapBwRestrictions
			{
				{
					"CUBE_NAM",
					this.provider.MdxCube.Name
				},
				{
					"DIM_UNAM",
					this.provider.Variable.Dimension
				}
			};
			if (!string.IsNullOrEmpty(this.provider.Variable.Hierarchy))
			{
				sapBwRestrictions.Add("HRY_UNAM", this.provider.Variable.Hierarchy);
			}
			else if (!string.IsNullOrEmpty(this.hierarchyUniqueNameOverride))
			{
				sapBwRestrictions.Add("HRY_UNAM", this.hierarchyUniqueNameOverride);
			}
			if (this.startRow != null)
			{
				sapBwRestrictions.Add("START_ROW", this.startRow);
				sapBwRestrictions.Add("END_ROW", this.endRow);
			}
			return sapBwRestrictions;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00075948 File Offset: 0x00073B48
		private string GetUniqueNameForValidMember(IDataReader reader)
		{
			string @string = reader.GetString(2);
			if (reader.GetString(0) != "00" && (!@string.EndsWith("[#]", StringComparison.Ordinal) || this.provider.AllowNonAssigned))
			{
				return @string;
			}
			return null;
		}

		// Token: 0x040010DC RID: 4316
		private const int levelOrdinal = 0;

		// Token: 0x040010DD RID: 4317
		private const int memberNameOrdinal = 1;

		// Token: 0x040010DE RID: 4318
		private const int memberUniqueNameOrdinal = 2;

		// Token: 0x040010DF RID: 4319
		private const int captionOrdinal = 5;

		// Token: 0x040010E0 RID: 4320
		private readonly SapBwVariableValueProvider provider;

		// Token: 0x040010E1 RID: 4321
		private readonly string hierarchyUniqueNameOverride;

		// Token: 0x040010E2 RID: 4322
		private readonly long? startRow;

		// Token: 0x040010E3 RID: 4323
		private readonly long endRow;

		// Token: 0x040010E4 RID: 4324
		private List<IValueReference> cachedValues;

		// Token: 0x040010E5 RID: 4325
		private bool hasMoreValues;
	}
}
