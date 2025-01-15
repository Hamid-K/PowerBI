using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x0200020C RID: 524
	internal class TxSavepoint
	{
		// Token: 0x06001D9D RID: 7581 RVA: 0x000C8FEB File Offset: 0x000C71EB
		internal TxSavepoint(TxManager manager)
		{
			this.TxManager = manager;
			this.allBodies = new HashSet<ITxObjectBody>();
			this.allMergePartitionsRequestedTables = new HashSet<Table>();
			this.anyRenameRequestedThroughAPI = false;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000C9018 File Offset: 0x000C7218
		internal TxSavepoint(TxManager manager, TxSavepoint savedSavepoint)
			: this(manager)
		{
			foreach (IRefreshableMetadataObjectBody refreshableMetadataObjectBody in from b in savedSavepoint.allBodies
				where b is IRefreshableMetadataObjectBody
				select (IRefreshableMetadataObjectBody)b)
			{
				if (!refreshableMetadataObjectBody.IsRemoved && (refreshableMetadataObjectBody.RefreshRequested || (refreshableMetadataObjectBody is IRefreshablePartitionBody && ((IRefreshablePartitionBody)refreshableMetadataObjectBody).MergePartitionsRequested) || (refreshableMetadataObjectBody is IIncrementalRefreshMetadataObjectBody && ((IIncrementalRefreshMetadataObjectBody)refreshableMetadataObjectBody).ApplyRefreshPolicyRequested)))
				{
					ITxObjectBody txObjectBody = refreshableMetadataObjectBody.Owner.CloneBody(this);
					if (txObjectBody is IRefreshablePartitionBody)
					{
						((IRefreshablePartitionBody)txObjectBody).AnalyzeRefreshPolicyImpactRequested = false;
					}
				}
			}
			foreach (Table table in savedSavepoint.allMergePartitionsRequestedTables)
			{
				this.allMergePartitionsRequestedTables.Add(table);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x000C9154 File Offset: 0x000C7354
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x000C915C File Offset: 0x000C735C
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000C9165 File Offset: 0x000C7365
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x000C916D File Offset: 0x000C736D
		public TxSavepoint Prev { get; internal set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000C9176 File Offset: 0x000C7376
		internal ICollection<ITxObjectBody> AllBodies
		{
			get
			{
				return this.allBodies;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x000C917E File Offset: 0x000C737E
		internal ICollection<Table> AllMergePartitionsRequestedTables
		{
			get
			{
				return this.allMergePartitionsRequestedTables;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000C9186 File Offset: 0x000C7386
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x000C918E File Offset: 0x000C738E
		public TxManager TxManager { get; private set; }

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000C9197 File Offset: 0x000C7397
		internal void RegisterBody(ITxObjectBody body)
		{
			if (body.Savepoint != null)
			{
				TxSavepoint savepoint = body.Savepoint;
			}
			this.allBodies.Add(body);
			body.Savepoint = this;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000C91BE File Offset: 0x000C73BE
		internal void UnregisterBody(ITxObjectBody body)
		{
			this.allBodies.Remove(body);
			body.Savepoint = null;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000C91D4 File Offset: 0x000C73D4
		[Conditional("DEBUG")]
		internal void ValidateAllBodies()
		{
			this.AllBodies.Any((ITxObjectBody b) => !(b is IMetadataObjectBody) && !(b is IMetadataObjectCollectionBody));
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000C9204 File Offset: 0x000C7404
		internal IList<MetadataObject> GetApplyRefreshPolicyDeltaFromPrevious()
		{
			List<MetadataObject> list = new List<MetadataObject>();
			foreach (IIncrementalRefreshMetadataObjectBody incrementalRefreshMetadataObjectBody in from b in this.AllBodies
				where b is IIncrementalRefreshMetadataObjectBody
				select (IIncrementalRefreshMetadataObjectBody)b)
			{
				if (incrementalRefreshMetadataObjectBody.ApplyRefreshPolicyRequested && !incrementalRefreshMetadataObjectBody.IsRemoved)
				{
					MetadataObject metadataObject = (MetadataObject)incrementalRefreshMetadataObjectBody.Owner;
					if (metadataObject.ObjectType == ObjectType.Model)
					{
						list.Insert(0, metadataObject);
					}
					else
					{
						list.Add(metadataObject);
					}
				}
			}
			return list;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000C92D0 File Offset: 0x000C74D0
		internal bool HasDeltaFromPrevious(bool includeRefreshAndMergeChanges)
		{
			foreach (TxSavepoint.ChangeInfo changeInfo in this.EnumerateChangesFromPrevious(includeRefreshAndMergeChanges))
			{
				if (changeInfo.Type != TxSavepoint.ChangeType.Alter)
				{
					return true;
				}
				if (!changeInfo.Body.IsEqualTo((IMetadataObjectBody)changeInfo.Body.CreatedFrom))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000C9348 File Offset: 0x000C7548
		internal ObjectChangelist GetDeltaFromPrevious(bool includeRefreshAndMergeChanges)
		{
			ObjectChangelist objectChangelist = new ObjectChangelist();
			CompareContext compareContext = new CompareContext(objectChangelist);
			foreach (TxSavepoint.ChangeInfo changeInfo in this.EnumerateChangesFromPrevious(includeRefreshAndMergeChanges))
			{
				switch (changeInfo.Type)
				{
				case TxSavepoint.ChangeType.Delete:
					objectChangelist.RegisterRemovedObject(changeInfo.Object);
					if (changeInfo.Object.Parent == null && !changeInfo.Object.LastParent.IsRemoved)
					{
						objectChangelist.RegisterRemovedSubtree(changeInfo.Object);
					}
					break;
				case TxSavepoint.ChangeType.Add:
					objectChangelist.RegisterAddedObject(changeInfo.Object);
					if (changeInfo.Object.Parent == null || !this.IsObjectAddedInSavepoint(changeInfo.Object.Parent))
					{
						objectChangelist.RegisterAddedSubtree(changeInfo.Object);
					}
					break;
				case TxSavepoint.ChangeType.Alter:
					changeInfo.Body.CompareWith((IMetadataObjectBody)changeInfo.Body.CreatedFrom, compareContext);
					break;
				case TxSavepoint.ChangeType.RefreshRequest:
					objectChangelist.RegisterObjectForRefresh(changeInfo.Object);
					break;
				case TxSavepoint.ChangeType.MergeRequest:
					objectChangelist.RegisterObjectForMergePartitions((Partition)changeInfo.Object);
					break;
				case TxSavepoint.ChangeType.AnalyzeImpactRequest:
					objectChangelist.RegisterObjectForAnalyzeRefreshPolicyImpact((Partition)changeInfo.Object);
					break;
				case TxSavepoint.ChangeType.RenameRequest:
					objectChangelist.RegisterObjectForRename((NamedMetadataObject)changeInfo.Object);
					break;
				}
			}
			return objectChangelist.MarkAsReadOnly();
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000C94C8 File Offset: 0x000C76C8
		internal void MergeWithPreviousSavepoint()
		{
			TxSavepoint prev = this.Prev;
			foreach (ITxObjectBody txObjectBody in prev.AllBodies.ToList<ITxObjectBody>())
			{
				ITxObjectBody txObjectBody2 = txObjectBody.FindNextBody();
				if (txObjectBody2 == null)
				{
					prev.UnregisterBody(txObjectBody);
					this.RegisterBody(txObjectBody);
				}
				else
				{
					txObjectBody2.CreatedFrom = txObjectBody.CreatedFrom;
					prev.UnregisterBody(txObjectBody);
				}
			}
			this.Prev = prev.Prev;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000C955C File Offset: 0x000C775C
		internal void DropPreviousSavepoints()
		{
			for (TxSavepoint txSavepoint = this.Prev; txSavepoint != null; txSavepoint = txSavepoint.Prev)
			{
				int count = txSavepoint.allBodies.Count;
			}
			this.Prev = null;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000C9590 File Offset: 0x000C7790
		private bool IsObjectAddedInSavepoint(ITxObject obj)
		{
			ITxObjectBody txObjectBody = obj.Body;
			while (txObjectBody != null && txObjectBody.Savepoint != this)
			{
				txObjectBody = txObjectBody.CreatedFrom;
			}
			return txObjectBody != null && (txObjectBody.CreatedFrom == null || txObjectBody.CreatedFrom.Savepoint == null);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000C95D8 File Offset: 0x000C77D8
		internal void ClearPendingOperationFlags()
		{
			foreach (IMetadataObjectBody metadataObjectBody in from b in this.AllBodies
				where b is IMetadataObjectBody
				select (IMetadataObjectBody)b)
			{
				metadataObjectBody.ClearOperationFlags();
			}
			this.allMergePartitionsRequestedTables.Clear();
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000C9678 File Offset: 0x000C7878
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x000C9680 File Offset: 0x000C7880
		internal bool AnyRenameRequestedThroughAPI
		{
			get
			{
				return this.anyRenameRequestedThroughAPI;
			}
			set
			{
				this.anyRenameRequestedThroughAPI = value;
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000C968B File Offset: 0x000C788B
		private IEnumerable<TxSavepoint.ChangeInfo> EnumerateChangesFromPrevious(bool includeRefreshAndMergeChanges)
		{
			foreach (IMetadataObjectBody metadataObjectBody in this.AllBodies.Where((ITxObjectBody b) => b is IMetadataObjectBody).Cast<IMetadataObjectBody>())
			{
				MetadataObject metadataObject = (MetadataObject)metadataObjectBody.Owner;
				if (metadataObjectBody.IsRemoved)
				{
					if (metadataObjectBody.CreatedFrom == null || metadataObjectBody.CreatedFrom.Savepoint != null)
					{
						yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.Delete, metadataObject, metadataObjectBody);
					}
				}
				else if (this.IsObjectAddedInSavepoint(metadataObject))
				{
					yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.Add, metadataObject, metadataObjectBody);
				}
				else
				{
					yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.Alter, metadataObject, metadataObjectBody);
				}
			}
			IEnumerator<IMetadataObjectBody> enumerator = null;
			foreach (IRefreshableMetadataObjectBody body in this.AllBodies.Where((ITxObjectBody b) => b is IRefreshableMetadataObjectBody).Cast<IRefreshableMetadataObjectBody>())
			{
				MetadataObject owner = (MetadataObject)body.Owner;
				if (includeRefreshAndMergeChanges && body.RefreshRequested && !body.IsRemoved)
				{
					yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.RefreshRequest, owner, body);
				}
				if (body is IRefreshablePartitionBody)
				{
					IRefreshablePartitionBody partition = (IRefreshablePartitionBody)body;
					if (partition.AnalyzeRefreshPolicyImpactRequested)
					{
						yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.AnalyzeImpactRequest, owner, partition);
					}
					if (includeRefreshAndMergeChanges && partition.MergePartitionsRequested && !body.IsRemoved)
					{
						yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.MergeRequest, owner, partition);
					}
					partition = null;
				}
				owner = null;
				body = null;
			}
			IEnumerator<IRefreshableMetadataObjectBody> enumerator2 = null;
			foreach (INamedMetadataObjectBody namedMetadataObjectBody in this.AllBodies.Where((ITxObjectBody b) => b is INamedMetadataObjectBody).Cast<INamedMetadataObjectBody>())
			{
				if (namedMetadataObjectBody.RenameRequestedThroughAPI && !namedMetadataObjectBody.IsRemoved)
				{
					yield return new TxSavepoint.ChangeInfo(TxSavepoint.ChangeType.RenameRequest, (MetadataObject)namedMetadataObjectBody.Owner, namedMetadataObjectBody);
				}
			}
			IEnumerator<INamedMetadataObjectBody> enumerator3 = null;
			yield break;
			yield break;
		}

		// Token: 0x040006D5 RID: 1749
		private string name;

		// Token: 0x040006D6 RID: 1750
		private HashSet<ITxObjectBody> allBodies;

		// Token: 0x040006D7 RID: 1751
		private HashSet<Table> allMergePartitionsRequestedTables;

		// Token: 0x040006D8 RID: 1752
		private bool anyRenameRequestedThroughAPI;

		// Token: 0x0200043A RID: 1082
		private enum ChangeType
		{
			// Token: 0x040013FF RID: 5119
			Unknown,
			// Token: 0x04001400 RID: 5120
			Delete,
			// Token: 0x04001401 RID: 5121
			Add,
			// Token: 0x04001402 RID: 5122
			Alter,
			// Token: 0x04001403 RID: 5123
			RefreshRequest,
			// Token: 0x04001404 RID: 5124
			MergeRequest,
			// Token: 0x04001405 RID: 5125
			AnalyzeImpactRequest,
			// Token: 0x04001406 RID: 5126
			RenameRequest
		}

		// Token: 0x0200043B RID: 1083
		private struct ChangeInfo
		{
			// Token: 0x060028CB RID: 10443 RVA: 0x000F018C File Offset: 0x000EE38C
			public ChangeInfo(TxSavepoint.ChangeType type, MetadataObject @object, IMetadataObjectBody body)
			{
				this.Type = type;
				this.Object = @object;
				this.Body = body;
			}

			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x060028CC RID: 10444 RVA: 0x000F01A3 File Offset: 0x000EE3A3
			internal bool IsValid
			{
				get
				{
					return this.Type != TxSavepoint.ChangeType.Unknown && this.Object != null && this.Body != null;
				}
			}

			// Token: 0x04001407 RID: 5127
			public readonly TxSavepoint.ChangeType Type;

			// Token: 0x04001408 RID: 5128
			public readonly MetadataObject Object;

			// Token: 0x04001409 RID: 5129
			public readonly IMetadataObjectBody Body;
		}
	}
}
