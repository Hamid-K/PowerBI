using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x0200020B RID: 523
	internal sealed class TxManager
	{
		// Token: 0x06001D94 RID: 7572 RVA: 0x000C8CF1 File Offset: 0x000C6EF1
		public TxManager(Model owner, bool createBeginTx, bool modelIsSynced)
		{
			this.owner = owner;
			this.CreateInitialSavepointsForModel(createBeginTx, modelIsSynced);
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x000C8D08 File Offset: 0x000C6F08
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x000C8D10 File Offset: 0x000C6F10
		public TxSavepoint CurrentSavepoint { get; private set; }

		// Token: 0x06001D97 RID: 7575 RVA: 0x000C8D1C File Offset: 0x000C6F1C
		public TxSavepoint AddSavepoint(string name)
		{
			TxSavepoint txSavepoint = new TxSavepoint(this)
			{
				Name = name
			};
			this.AddSavepointImpl(txSavepoint);
			return txSavepoint;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000C8D40 File Offset: 0x000C6F40
		public void RevertToSavepoint(TxSavepoint targetSavepoint)
		{
			bool flag = false;
			for (TxSavepoint txSavepoint = this.CurrentSavepoint; txSavepoint != null; txSavepoint = txSavepoint.Prev)
			{
				if (txSavepoint == targetSavepoint)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new TomInternalException("Attempt to revert to a savepoint which doesn't belong to savepoint chain");
			}
			List<TxSavepoint> list = new List<TxSavepoint>();
			for (TxSavepoint txSavepoint2 = targetSavepoint; txSavepoint2 != null; txSavepoint2 = txSavepoint2.Prev)
			{
				list.Add(txSavepoint2);
			}
			for (TxSavepoint txSavepoint3 = this.CurrentSavepoint; txSavepoint3 != targetSavepoint; txSavepoint3 = txSavepoint3.Prev)
			{
				foreach (ITxObjectBody txObjectBody in txSavepoint3.AllBodies.ToList<ITxObjectBody>())
				{
					ITxObjectBody txObjectBody2 = txObjectBody;
					while (txObjectBody2 != null && txObjectBody2.Savepoint != null && !list.Contains(txObjectBody2.Savepoint))
					{
						txObjectBody2 = txObjectBody2.CreatedFrom;
					}
					if (txObjectBody2 != null && txObjectBody != txObjectBody2)
					{
						for (ITxObjectBody txObjectBody3 = txObjectBody; txObjectBody3 != txObjectBody2; txObjectBody3 = txObjectBody3.CreatedFrom)
						{
							txObjectBody3.Savepoint.UnregisterBody(txObjectBody3);
						}
						txObjectBody.Owner.Body = txObjectBody2;
						txObjectBody.Owner.NotifyBodyReverted();
					}
				}
			}
			this.CurrentSavepoint = targetSavepoint;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000C8E78 File Offset: 0x000C7078
		public void ForceTransactionForModifiedModel()
		{
			((ITxService)this.owner.Server).BeginTransaction(this.owner.Database);
			TxSavepoint prev = this.CurrentSavepoint.Prev;
			prev.Name = "BeginTransaction";
			TxSavepoint txSavepoint = new TxSavepoint(this)
			{
				Name = "Synced",
				Prev = prev
			};
			this.CurrentSavepoint.Prev = txSavepoint;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000C8EE4 File Offset: 0x000C70E4
		public void HandlePartialSave(bool isNewSavepointInheritedFromCurrent)
		{
			TxSavepoint currentSavepoint = this.CurrentSavepoint;
			currentSavepoint.Name = "Saved";
			if (isNewSavepointInheritedFromCurrent)
			{
				this.AddSavepointImpl(new TxSavepoint(this, currentSavepoint)
				{
					Name = "Modified"
				});
			}
			else
			{
				this.AddSavepointImpl(new TxSavepoint(this)
				{
					Name = "Modified"
				});
			}
			currentSavepoint.ClearPendingOperationFlags();
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000C8F40 File Offset: 0x000C7140
		private void CreateInitialSavepointsForModel(bool createBeginTx, bool modelIsSynced)
		{
			if (createBeginTx)
			{
				this.AddSavepoint("BeginTransaction");
			}
			this.AddSavepoint("Synced");
			if (!modelIsSynced)
			{
				Utils.Verify(this.owner.body.CreatedFrom == null);
				this.owner.body.CreatedFrom = Model.ObjectBody.GetBodyForNewModel(this.owner);
				this.CurrentSavepoint.RegisterBody(this.owner.body.CreatedFrom);
				this.AddSavepoint("Modified");
			}
			ObjectChangeTracker.RegisterAddedSubtreeWithSavepoint(this.CurrentSavepoint, this.owner);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000C8FD6 File Offset: 0x000C71D6
		private void AddSavepointImpl(TxSavepoint savepoint)
		{
			savepoint.Prev = this.CurrentSavepoint;
			this.CurrentSavepoint = savepoint;
		}

		// Token: 0x040006D3 RID: 1747
		private Model owner;
	}
}
