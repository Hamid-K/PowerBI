using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010E RID: 270
	internal sealed class DataShapeResultBuilder : BaseBuilder<DataShape, DataShape>
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal DataShapeResultBuilder()
			: base(null)
		{
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public override DataShape Build()
		{
			return new DataShape
			{
				PrimaryHierarchy = this._primaryHierarchy,
				SecondaryHierarchy = this._secondaryHierarchy
			};
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000F008 File Offset: 0x0000D208
		internal DataShapeResultBuilder.DataMembersBuilder WithPrimaryHierarchy()
		{
			DataShapeResultBuilder.DataMembersBuilder dataMembersBuilder = new DataShapeResultBuilder.DataMembersBuilder(this);
			this._primaryHierarchy = dataMembersBuilder.Build();
			return dataMembersBuilder;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000F02C File Offset: 0x0000D22C
		internal DataShapeResultBuilder.DataMembersBuilder WithSecondaryHierarchy()
		{
			DataShapeResultBuilder.DataMembersBuilder dataMembersBuilder = new DataShapeResultBuilder.DataMembersBuilder(this);
			this._secondaryHierarchy = dataMembersBuilder.Build();
			return dataMembersBuilder;
		}

		// Token: 0x0400031F RID: 799
		private List<DataMember> _primaryHierarchy;

		// Token: 0x04000320 RID: 800
		private List<DataMember> _secondaryHierarchy;

		// Token: 0x02000312 RID: 786
		internal sealed class DataMembersBuilder : BaseBuilder<List<DataMember>, DataShapeResultBuilder>
		{
			// Token: 0x0600197C RID: 6524 RVA: 0x0002DD99 File Offset: 0x0002BF99
			internal DataMembersBuilder(DataShapeResultBuilder parent)
				: base(parent)
			{
				this._members = new List<DataMember>();
			}

			// Token: 0x0600197D RID: 6525 RVA: 0x0002DDAD File Offset: 0x0002BFAD
			public override List<DataMember> Build()
			{
				return this._members;
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
			internal DataShapeResultBuilder.DataMemberBuilder WithDataMember()
			{
				DataShapeResultBuilder.DataMemberBuilder dataMemberBuilder = new DataShapeResultBuilder.DataMemberBuilder(this);
				this._members.Add(dataMemberBuilder.Build());
				return dataMemberBuilder;
			}

			// Token: 0x04000971 RID: 2417
			private readonly List<DataMember> _members;
		}

		// Token: 0x02000313 RID: 787
		internal sealed class DataMemberBuilder : BaseBuilder<DataMember, DataShapeResultBuilder.DataMembersBuilder>
		{
			// Token: 0x0600197F RID: 6527 RVA: 0x0002DDDE File Offset: 0x0002BFDE
			internal DataMemberBuilder(DataShapeResultBuilder.DataMembersBuilder parent)
				: base(parent)
			{
				this._instances = new List<DataMemberInstance>();
				this._dataMember = new DataMember
				{
					Instances = this._instances
				};
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x0002DE09 File Offset: 0x0002C009
			public override DataMember Build()
			{
				return this._dataMember;
			}

			// Token: 0x06001981 RID: 6529 RVA: 0x0002DE14 File Offset: 0x0002C014
			internal DataShapeResultBuilder.InstanceBuilder WithInstance()
			{
				DataShapeResultBuilder.InstanceBuilder instanceBuilder = new DataShapeResultBuilder.InstanceBuilder(this);
				this._instances.Add(instanceBuilder.Build());
				return instanceBuilder;
			}

			// Token: 0x04000972 RID: 2418
			private readonly DataMember _dataMember;

			// Token: 0x04000973 RID: 2419
			private readonly List<DataMemberInstance> _instances;
		}

		// Token: 0x02000314 RID: 788
		internal sealed class InstanceBuilder : BaseBuilder<DataMemberInstance, DataShapeResultBuilder.DataMemberBuilder>
		{
			// Token: 0x06001982 RID: 6530 RVA: 0x0002DE3A File Offset: 0x0002C03A
			internal InstanceBuilder(DataShapeResultBuilder.DataMemberBuilder parent)
				: base(parent)
			{
				this._calculations = new List<Calculation>();
				this._instance = new DataMemberInstance
				{
					Calculations = this._calculations
				};
			}

			// Token: 0x06001983 RID: 6531 RVA: 0x0002DE65 File Offset: 0x0002C065
			public override DataMemberInstance Build()
			{
				return this._instance;
			}

			// Token: 0x06001984 RID: 6532 RVA: 0x0002DE6D File Offset: 0x0002C06D
			internal DataShapeResultBuilder.InstanceBuilder WithCalculation(Calculation calculation)
			{
				this._calculations.Add(calculation);
				return this;
			}

			// Token: 0x06001985 RID: 6533 RVA: 0x0002DE7C File Offset: 0x0002C07C
			internal DataShapeResultBuilder.InstanceBuilder WithCalculation(string id, PrimitiveValue value)
			{
				return this.WithCalculation(new Calculation
				{
					Id = id,
					JsonValue = PrimitiveValueEncoding.ToTypeEncodedString(value)
				});
			}

			// Token: 0x04000974 RID: 2420
			private readonly DataMemberInstance _instance;

			// Token: 0x04000975 RID: 2421
			private readonly List<Calculation> _calculations;
		}
	}
}
