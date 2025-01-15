using System;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FA RID: 506
	internal abstract class MetadataObjectBody<TOwner> : IMetadataObjectBody, ITxObjectBody where TOwner : MetadataObject
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x000C6413 File Offset: 0x000C4613
		public MetadataObjectBody(TOwner owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000C642D File Offset: 0x000C462D
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x000C6435 File Offset: 0x000C4635
		public TOwner Owner { get; private set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x000C643E File Offset: 0x000C463E
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x000C6446 File Offset: 0x000C4646
		public MetadataObjectBody<TOwner> CreatedFrom { get; internal set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x000C644F File Offset: 0x000C464F
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x000C6457 File Offset: 0x000C4657
		public TxSavepoint Savepoint { get; internal set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x000C6460 File Offset: 0x000C4660
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x000C6468 File Offset: 0x000C4668
		public ObjectId Id { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000C6471 File Offset: 0x000C4671
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x000C6479 File Offset: 0x000C4679
		public bool IsRemoved { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x000C6482 File Offset: 0x000C4682
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x000C648A File Offset: 0x000C468A
		public MetadataObject LastParent { get; set; }

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000C6494 File Offset: 0x000C4694
		public virtual void CopyFrom(MetadataObjectBody<TOwner> other, CopyContext context)
		{
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds)
			{
				this.Id = other.Id;
			}
			if ((context.Flags & CopyFlags.IncludeOperationalFlags) == CopyFlags.IncludeOperationalFlags)
			{
				this.IsRemoved = other.IsRemoved;
				this.LastParent = other.LastParent;
			}
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				MetadataObjectBody<TOwner>.CompatibilityDemand.CopyCompatibilityDemandSet(other.compatibilityRequirements, this.compatibilityRequirements);
				return;
			}
			MetadataObjectBody<TOwner>.CompatibilityDemand.ResetCompatibilityDemandSet(this.compatibilityRequirements);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000C6513 File Offset: 0x000C4713
		public virtual bool IsEqualTo(IMetadataObjectBody other)
		{
			return PropertyHelper.AreValuesIdentical(this.Id, other.Id);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000C6528 File Offset: 0x000C4728
		public virtual void CompareWith(IMetadataObjectBody other, CompareContext context)
		{
			if (!PropertyHelper.AreValuesIdentical(this.Id, other.Id))
			{
				context.RegisterPropertyChange(this.Owner, "Id", typeof(ObjectId), PropertyFlags.DdlAndUser, other.Id, this.Id);
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000C657F File Offset: 0x000C477F
		public virtual void ClearOperationFlags()
		{
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000C6584 File Offset: 0x000C4784
		bool IMetadataObjectBody.HasCompatibilityInfo(CompatibilityMode mode)
		{
			if (mode != CompatibilityMode.Unknown)
			{
				return this.compatibilityRequirements[CompatibilityRestrictionSet.GetModeRestrictionIndex(mode)].RequiredLevel != -3;
			}
			for (int i = 0; i < 3; i++)
			{
				if (this.compatibilityRequirements[i].RequiredLevel != -3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000C65D8 File Offset: 0x000C47D8
		bool IMetadataObjectBody.GetCompatibilityRequirement(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			MetadataObjectBody<TOwner>.CompatibilityDemand compatibilityDemand = this.compatibilityRequirements[CompatibilityRestrictionSet.GetModeRestrictionIndex(mode)];
			requiredLevel = compatibilityDemand.RequiredLevel;
			requestingPath = compatibilityDemand.RequestingPath;
			return requiredLevel == -2 || requiredLevel > 1200;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000C661A File Offset: 0x000C481A
		void IMetadataObjectBody.UpdateCompatibilityRequirement(CompatibilityMode mode, int requiredLevel, string requestingPath)
		{
			this.compatibilityRequirements[CompatibilityRestrictionSet.GetModeRestrictionIndex(mode)].Update(requiredLevel, requestingPath);
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x000C6634 File Offset: 0x000C4834
		ITxObject ITxObjectBody.Owner
		{
			get
			{
				return this.Owner;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x000C6641 File Offset: 0x000C4841
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x000C6649 File Offset: 0x000C4849
		ITxObjectBody ITxObjectBody.CreatedFrom
		{
			get
			{
				return this.CreatedFrom;
			}
			set
			{
				this.CreatedFrom = (MetadataObjectBody<TOwner>)value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000C6657 File Offset: 0x000C4857
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x000C665F File Offset: 0x000C485F
		TxSavepoint ITxObjectBody.Savepoint
		{
			get
			{
				return this.Savepoint;
			}
			set
			{
				this.Savepoint = value;
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000C6668 File Offset: 0x000C4868
		void ITxObjectBody.CopyFrom(ITxObjectBody other, CopyContext context)
		{
			this.CopyFrom((MetadataObjectBody<TOwner>)other, context);
		}

		// Token: 0x0400068C RID: 1676
		private MetadataObjectBody<TOwner>.CompatibilityDemand[] compatibilityRequirements = MetadataObjectBody<TOwner>.CompatibilityDemand.CreateCompatibilityDemandSet();

		// Token: 0x02000433 RID: 1075
		private struct CompatibilityDemand
		{
			// Token: 0x170007E1 RID: 2017
			// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000EFCF3 File Offset: 0x000EDEF3
			public int RequiredLevel
			{
				get
				{
					return this.requiredLevel;
				}
			}

			// Token: 0x170007E2 RID: 2018
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000EFCFB File Offset: 0x000EDEFB
			public string RequestingPath
			{
				get
				{
					return this.requestingPath;
				}
			}

			// Token: 0x060028A6 RID: 10406 RVA: 0x000EFD03 File Offset: 0x000EDF03
			public static MetadataObjectBody<TOwner>.CompatibilityDemand[] CreateCompatibilityDemandSet()
			{
				MetadataObjectBody<TOwner>.CompatibilityDemand[] array = new MetadataObjectBody<TOwner>.CompatibilityDemand[3];
				MetadataObjectBody<TOwner>.CompatibilityDemand.ResetCompatibilityDemandSet(array);
				return array;
			}

			// Token: 0x060028A7 RID: 10407 RVA: 0x000EFD14 File Offset: 0x000EDF14
			public static void CopyCompatibilityDemandSet(MetadataObjectBody<TOwner>.CompatibilityDemand[] source, MetadataObjectBody<TOwner>.CompatibilityDemand[] target)
			{
				for (int i = 0; i < 3; i++)
				{
					target[i].Update(source[i].requiredLevel, source[i].requestingPath);
				}
			}

			// Token: 0x060028A8 RID: 10408 RVA: 0x000EFD54 File Offset: 0x000EDF54
			public static void ResetCompatibilityDemandSet(MetadataObjectBody<TOwner>.CompatibilityDemand[] set)
			{
				for (int i = 0; i < 3; i++)
				{
					set[i].Update(-3, string.Empty);
				}
			}

			// Token: 0x060028A9 RID: 10409 RVA: 0x000EFD80 File Offset: 0x000EDF80
			public void Update(int requiredLevel, string requestingPath)
			{
				this.requiredLevel = requiredLevel;
				this.requestingPath = requestingPath;
			}

			// Token: 0x040013E7 RID: 5095
			private int requiredLevel;

			// Token: 0x040013E8 RID: 5096
			private string requestingPath;
		}
	}
}
