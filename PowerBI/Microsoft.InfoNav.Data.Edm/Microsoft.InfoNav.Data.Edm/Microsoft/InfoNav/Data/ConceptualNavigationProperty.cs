using System;

namespace Microsoft.InfoNav.Data
{
	// Token: 0x02000006 RID: 6
	internal class ConceptualNavigationProperty : IConceptualNavigationProperty
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		internal ConceptualNavigationProperty(string name, string edmName, bool isActive, CrossFilterDirection crossFilterDirection, IConceptualColumn sourceColumn, IConceptualColumn targetColumn, IConceptualEntity targetEntity, ConceptualMultiplicity sourceMultiplicity, ConceptualMultiplicity targetMultiplicity, ConceptualNavigationBehavior behavior)
		{
			this._name = name;
			this._edmName = edmName;
			this._isActive = isActive;
			this._crossFilterDirection = crossFilterDirection;
			this._sourceColumn = sourceColumn;
			this._targetColumn = targetColumn;
			this._targetEntity = targetEntity;
			this._sourceMultiplicity = sourceMultiplicity;
			this._targetMultiplicity = targetMultiplicity;
			this._behavior = behavior;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public CrossFilterDirection CrossFilterDirection
		{
			get
			{
				return this._crossFilterDirection;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		public IConceptualColumn SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F8 File Offset: 0x000002F8
		public IConceptualColumn TargetColumn
		{
			get
			{
				return this._targetColumn;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002100 File Offset: 0x00000300
		public IConceptualEntity TargetEntity
		{
			get
			{
				return this._targetEntity;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002108 File Offset: 0x00000308
		public ConceptualMultiplicity SourceMultiplicity
		{
			get
			{
				return this._sourceMultiplicity;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002110 File Offset: 0x00000310
		public ConceptualMultiplicity TargetMultiplicity
		{
			get
			{
				return this._targetMultiplicity;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002118 File Offset: 0x00000318
		public ConceptualNavigationBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x04000029 RID: 41
		private readonly string _name;

		// Token: 0x0400002A RID: 42
		private readonly string _edmName;

		// Token: 0x0400002B RID: 43
		private readonly bool _isActive;

		// Token: 0x0400002C RID: 44
		private readonly CrossFilterDirection _crossFilterDirection;

		// Token: 0x0400002D RID: 45
		private readonly IConceptualColumn _sourceColumn;

		// Token: 0x0400002E RID: 46
		private readonly IConceptualColumn _targetColumn;

		// Token: 0x0400002F RID: 47
		private readonly IConceptualEntity _targetEntity;

		// Token: 0x04000030 RID: 48
		private readonly ConceptualMultiplicity _sourceMultiplicity;

		// Token: 0x04000031 RID: 49
		private readonly ConceptualMultiplicity _targetMultiplicity;

		// Token: 0x04000032 RID: 50
		private readonly ConceptualNavigationBehavior _behavior;
	}
}
