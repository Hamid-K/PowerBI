using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200054D RID: 1357
	public sealed class ModificationFunctionMapping : MappingItem
	{
		// Token: 0x060042A5 RID: 17061 RVA: 0x000E55C4 File Offset: 0x000E37C4
		public ModificationFunctionMapping(EntitySetBase entitySet, EntityTypeBase entityType, EdmFunction function, IEnumerable<ModificationFunctionParameterBinding> parameterBindings, FunctionParameter rowsAffectedParameter, IEnumerable<ModificationFunctionResultBinding> resultBindings)
		{
			Check.NotNull<EntitySetBase>(entitySet, "entitySet");
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<IEnumerable<ModificationFunctionParameterBinding>>(parameterBindings, "parameterBindings");
			this._function = function;
			this._rowsAffectedParameter = rowsAffectedParameter;
			this._parameterBindings = new ReadOnlyCollection<ModificationFunctionParameterBinding>(parameterBindings.ToList<ModificationFunctionParameterBinding>());
			if (resultBindings != null)
			{
				List<ModificationFunctionResultBinding> list = resultBindings.ToList<ModificationFunctionResultBinding>();
				if (0 < list.Count)
				{
					this._resultBindings = new ReadOnlyCollection<ModificationFunctionResultBinding>(list);
				}
			}
			this._collocatedAssociationSetEnds = new ReadOnlyCollection<AssociationSetEnd>(ModificationFunctionMapping.GetReferencedAssociationSetEnds(entitySet as EntitySet, entityType as EntityType, parameterBindings).ToList<AssociationSetEnd>());
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000E5661 File Offset: 0x000E3861
		// (set) Token: 0x060042A7 RID: 17063 RVA: 0x000E5669 File Offset: 0x000E3869
		public FunctionParameter RowsAffectedParameter
		{
			get
			{
				return this._rowsAffectedParameter;
			}
			internal set
			{
				this._rowsAffectedParameter = value;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000E5672 File Offset: 0x000E3872
		internal string RowsAffectedParameterName
		{
			get
			{
				if (this.RowsAffectedParameter == null)
				{
					return null;
				}
				return this.RowsAffectedParameter.Name;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000E5689 File Offset: 0x000E3889
		public EdmFunction Function
		{
			get
			{
				return this._function;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060042AA RID: 17066 RVA: 0x000E5691 File Offset: 0x000E3891
		public ReadOnlyCollection<ModificationFunctionParameterBinding> ParameterBindings
		{
			get
			{
				return this._parameterBindings;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x000E5699 File Offset: 0x000E3899
		internal ReadOnlyCollection<AssociationSetEnd> CollocatedAssociationSetEnds
		{
			get
			{
				return this._collocatedAssociationSetEnds;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x000E56A1 File Offset: 0x000E38A1
		public ReadOnlyCollection<ModificationFunctionResultBinding> ResultBindings
		{
			get
			{
				return this._resultBindings;
			}
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000E56A9 File Offset: 0x000E38A9
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Func{{{0}}}: Prm={{{1}}}, Result={{{2}}}", new object[]
			{
				this.Function,
				StringUtil.ToCommaSeparatedStringSorted(this.ParameterBindings),
				StringUtil.ToCommaSeparatedStringSorted(this.ResultBindings)
			});
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000E56E5 File Offset: 0x000E38E5
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._parameterBindings);
			MappingItem.SetReadOnly(this._resultBindings);
			base.SetReadOnly();
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000E5704 File Offset: 0x000E3904
		private static IEnumerable<AssociationSetEnd> GetReferencedAssociationSetEnds(EntitySet entitySet, EntityType entityType, IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			HashSet<AssociationSetEnd> hashSet = new HashSet<AssociationSetEnd>();
			if (entitySet != null && entityType != null)
			{
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in parameterBindings)
				{
					AssociationSetEnd associationSetEnd = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd;
					if (associationSetEnd != null)
					{
						hashSet.Add(associationSetEnd);
					}
				}
				foreach (AssociationSet associationSet in entitySet.AssociationSets)
				{
					ReadOnlyMetadataCollection<ReferentialConstraint> referentialConstraints = associationSet.ElementType.ReferentialConstraints;
					if (referentialConstraints != null)
					{
						foreach (ReferentialConstraint referentialConstraint in referentialConstraints)
						{
							if (associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name].EntitySet == entitySet && referentialConstraint.ToRole.GetEntityType().IsAssignableFrom(entityType))
							{
								hashSet.Add(associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name]);
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x04001777 RID: 6007
		private FunctionParameter _rowsAffectedParameter;

		// Token: 0x04001778 RID: 6008
		private readonly EdmFunction _function;

		// Token: 0x04001779 RID: 6009
		private readonly ReadOnlyCollection<ModificationFunctionParameterBinding> _parameterBindings;

		// Token: 0x0400177A RID: 6010
		private readonly ReadOnlyCollection<AssociationSetEnd> _collocatedAssociationSetEnds;

		// Token: 0x0400177B RID: 6011
		private readonly ReadOnlyCollection<ModificationFunctionResultBinding> _resultBindings;
	}
}
