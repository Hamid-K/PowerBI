using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000641 RID: 1601
	internal class Shaper<T> : Shaper
	{
		// Token: 0x06004D0A RID: 19722 RVA: 0x001101E4 File Offset: 0x0010E3E4
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, bool readerOwned, bool streaming)
			: base(reader, context, workspace, mergeOption, stateCount, streaming)
		{
			this.RootCoordinator = (Coordinator<T>)rootCoordinatorFactory.CreateCoordinator(null, null);
			this._isObjectQuery = !(typeof(T) == typeof(RecordState));
			this._isActive = true;
			this.RootCoordinator.Initialize(this);
			this._readerOwned = readerOwned;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06004D0B RID: 19723 RVA: 0x00110254 File Offset: 0x0010E454
		// (remove) Token: 0x06004D0C RID: 19724 RVA: 0x0011028C File Offset: 0x0010E48C
		internal event EventHandler OnDone;

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x001102C1 File Offset: 0x0010E4C1
		// (set) Token: 0x06004D0E RID: 19726 RVA: 0x001102C9 File Offset: 0x0010E4C9
		internal bool DataWaiting { get; set; }

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x001102D2 File Offset: 0x0010E4D2
		internal IDbEnumerator<T> RootEnumerator
		{
			get
			{
				if (this._rootEnumerator == null)
				{
					this.InitializeRecordStates(this.RootCoordinator.CoordinatorFactory);
					this._rootEnumerator = this.GetEnumerator();
				}
				return this._rootEnumerator;
			}
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x00110300 File Offset: 0x0010E500
		private void InitializeRecordStates(CoordinatorFactory coordinatorFactory)
		{
			foreach (RecordStateFactory recordStateFactory in coordinatorFactory.RecordStateFactories)
			{
				this.State[recordStateFactory.StateSlotNumber] = recordStateFactory.Create(coordinatorFactory);
			}
			foreach (CoordinatorFactory coordinatorFactory2 in coordinatorFactory.NestedCoordinators)
			{
				this.InitializeRecordStates(coordinatorFactory2);
			}
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x00110398 File Offset: 0x0010E598
		public virtual IDbEnumerator<T> GetEnumerator()
		{
			if (this.RootCoordinator.CoordinatorFactory.IsSimple)
			{
				return new Shaper<T>.SimpleEnumerator(this);
			}
			Shaper<T>.RowNestedResultEnumerator rowNestedResultEnumerator = new Shaper<T>.RowNestedResultEnumerator(this);
			if (this._isObjectQuery)
			{
				return new Shaper<T>.ObjectQueryNestedEnumerator(rowNestedResultEnumerator);
			}
			return (IDbEnumerator<T>)new Shaper<T>.RecordStateEnumerator(rowNestedResultEnumerator);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x001103E0 File Offset: 0x0010E5E0
		private void Finally()
		{
			if (this._isActive)
			{
				this._isActive = false;
				if (this._readerOwned)
				{
					if (this._isObjectQuery)
					{
						this.Reader.Dispose();
					}
					if (this.Context != null && this.Streaming)
					{
						this.Context.ReleaseConnection();
					}
				}
				if (this.OnDone != null)
				{
					this.OnDone(this, new EventArgs());
				}
			}
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0011044C File Offset: 0x0010E64C
		private bool StoreRead()
		{
			bool flag;
			try
			{
				flag = this.Reader.Read();
			}
			catch (Exception ex)
			{
				this.HandleReaderException(ex);
				throw;
			}
			return flag;
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x00110484 File Offset: 0x0010E684
		private async Task<bool> StoreReadAsync(CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				flag = await this.Reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			catch (Exception ex)
			{
				this.HandleReaderException(ex);
				throw;
			}
			return flag;
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x001104D1 File Offset: 0x0010E6D1
		private void HandleReaderException(Exception e)
		{
			if (!e.IsCatchableEntityExceptionType())
			{
				return;
			}
			if (this.Reader.IsClosed)
			{
				throw new EntityCommandExecutionException(Strings.ADP_DataReaderClosed("Read"), e);
			}
			throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, e);
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x00110505 File Offset: 0x0010E705
		private void StartMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = true;
				base.InitializeForOnMaterialize();
			}
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x00110521 File Offset: 0x0010E721
		private void StopMaterializingElement()
		{
			if (this.Context != null)
			{
				this.Context.InMaterialization = false;
				base.RaiseMaterializedEvents();
			}
		}

		// Token: 0x04001B5E RID: 7006
		private readonly bool _isObjectQuery;

		// Token: 0x04001B5F RID: 7007
		private bool _isActive;

		// Token: 0x04001B60 RID: 7008
		private IDbEnumerator<T> _rootEnumerator;

		// Token: 0x04001B61 RID: 7009
		private readonly bool _readerOwned;

		// Token: 0x04001B64 RID: 7012
		internal readonly Coordinator<T> RootCoordinator;

		// Token: 0x02000C63 RID: 3171
		private class SimpleEnumerator : IDbEnumerator<T>, IEnumerator<T>, IDisposable, IEnumerator, IDbAsyncEnumerator<T>, IDbAsyncEnumerator
		{
			// Token: 0x06006AC3 RID: 27331 RVA: 0x0016C80E File Offset: 0x0016AA0E
			internal SimpleEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
			}

			// Token: 0x17001177 RID: 4471
			// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x0016C81D File Offset: 0x0016AA1D
			public T Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x17001178 RID: 4472
			// (get) Token: 0x06006AC5 RID: 27333 RVA: 0x0016C82F File Offset: 0x0016AA2F
			object IEnumerator.Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x17001179 RID: 4473
			// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x0016C846 File Offset: 0x0016AA46
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._shaper.RootCoordinator.Current;
				}
			}

			// Token: 0x06006AC7 RID: 27335 RVA: 0x0016C85D File Offset: 0x0016AA5D
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.RootCoordinator.SetCurrentToDefault();
				this._shaper.Finally();
			}

			// Token: 0x06006AC8 RID: 27336 RVA: 0x0016C880 File Offset: 0x0016AA80
			public bool MoveNext()
			{
				if (!this._shaper._isActive)
				{
					return false;
				}
				if (this._shaper.StoreRead())
				{
					try
					{
						this._shaper.StartMaterializingElement();
						this._shaper.RootCoordinator.ReadNextElement(this._shaper);
					}
					finally
					{
						this._shaper.StopMaterializingElement();
					}
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06006AC9 RID: 27337 RVA: 0x0016C8F4 File Offset: 0x0016AAF4
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				bool flag;
				if (!this._shaper._isActive)
				{
					flag = false;
				}
				else
				{
					cancellationToken.ThrowIfCancellationRequested();
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this._shaper.StoreReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (cultureAwaiter.GetResult())
					{
						try
						{
							this._shaper.StartMaterializingElement();
							this._shaper.RootCoordinator.ReadNextElement(this._shaper);
						}
						finally
						{
							this._shaper.StopMaterializingElement();
						}
						flag = true;
					}
					else
					{
						this.Dispose();
						flag = false;
					}
				}
				return flag;
			}

			// Token: 0x06006ACA RID: 27338 RVA: 0x0016C941 File Offset: 0x0016AB41
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040030F2 RID: 12530
			private readonly Shaper<T> _shaper;
		}

		// Token: 0x02000C64 RID: 3172
		private class RowNestedResultEnumerator : IDbEnumerator<Coordinator[]>, IEnumerator<Coordinator[]>, IDisposable, IEnumerator, IDbAsyncEnumerator<Coordinator[]>, IDbAsyncEnumerator
		{
			// Token: 0x06006ACB RID: 27339 RVA: 0x0016C948 File Offset: 0x0016AB48
			internal RowNestedResultEnumerator(Shaper<T> shaper)
			{
				this._shaper = shaper;
				this._current = new Coordinator[this._shaper.RootCoordinator.MaxDistanceToLeaf() + 1];
			}

			// Token: 0x1700117A RID: 4474
			// (get) Token: 0x06006ACC RID: 27340 RVA: 0x0016C974 File Offset: 0x0016AB74
			public Coordinator[] Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700117B RID: 4475
			// (get) Token: 0x06006ACD RID: 27341 RVA: 0x0016C97C File Offset: 0x0016AB7C
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700117C RID: 4476
			// (get) Token: 0x06006ACE RID: 27342 RVA: 0x0016C984 File Offset: 0x0016AB84
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06006ACF RID: 27343 RVA: 0x0016C98C File Offset: 0x0016AB8C
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._shaper.Finally();
			}

			// Token: 0x06006AD0 RID: 27344 RVA: 0x0016C9A0 File Offset: 0x0016ABA0
			public bool MoveNext()
			{
				try
				{
					this._shaper.StartMaterializingElement();
					if (!this._shaper.StoreRead())
					{
						this.RootCoordinator.ResetCollection(this._shaper);
						return false;
					}
					this.MaterializeRow();
				}
				finally
				{
					this._shaper.StopMaterializingElement();
				}
				return true;
			}

			// Token: 0x06006AD1 RID: 27345 RVA: 0x0016CA04 File Offset: 0x0016AC04
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				try
				{
					this._shaper.StartMaterializingElement();
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this._shaper.StoreReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						this.RootCoordinator.ResetCollection(this._shaper);
						return false;
					}
					this.MaterializeRow();
				}
				finally
				{
					this._shaper.StopMaterializingElement();
				}
				return true;
			}

			// Token: 0x06006AD2 RID: 27346 RVA: 0x0016CA54 File Offset: 0x0016AC54
			private void MaterializeRow()
			{
				Coordinator coordinator = this._shaper.RootCoordinator;
				int i = 0;
				bool flag = false;
				while (i < this._current.Length)
				{
					while (coordinator != null && !coordinator.CoordinatorFactory.HasData(this._shaper))
					{
						coordinator = coordinator.Next;
					}
					if (coordinator == null)
					{
						IL_00A8:
						while (i < this._current.Length)
						{
							this._current[i] = null;
							i++;
						}
						return;
					}
					if (coordinator.HasNextElement(this._shaper))
					{
						if (!flag && coordinator.Child != null)
						{
							coordinator.Child.ResetCollection(this._shaper);
						}
						flag = true;
						coordinator.ReadNextElement(this._shaper);
						this._current[i] = coordinator;
					}
					else
					{
						this._current[i] = null;
					}
					coordinator = coordinator.Child;
					i++;
				}
				goto IL_00A8;
			}

			// Token: 0x06006AD3 RID: 27347 RVA: 0x0016CB14 File Offset: 0x0016AD14
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700117D RID: 4477
			// (get) Token: 0x06006AD4 RID: 27348 RVA: 0x0016CB1B File Offset: 0x0016AD1B
			internal Coordinator<T> RootCoordinator
			{
				get
				{
					return this._shaper.RootCoordinator;
				}
			}

			// Token: 0x040030F3 RID: 12531
			private readonly Shaper<T> _shaper;

			// Token: 0x040030F4 RID: 12532
			private readonly Coordinator[] _current;
		}

		// Token: 0x02000C65 RID: 3173
		private class ObjectQueryNestedEnumerator : IDbEnumerator<T>, IEnumerator<T>, IDisposable, IEnumerator, IDbAsyncEnumerator<T>, IDbAsyncEnumerator
		{
			// Token: 0x06006AD5 RID: 27349 RVA: 0x0016CB28 File Offset: 0x0016AD28
			internal ObjectQueryNestedEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._previousElement = default(T);
				this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Start;
			}

			// Token: 0x1700117E RID: 4478
			// (get) Token: 0x06006AD6 RID: 27350 RVA: 0x0016CB4A File Offset: 0x0016AD4A
			public T Current
			{
				get
				{
					return this._previousElement;
				}
			}

			// Token: 0x1700117F RID: 4479
			// (get) Token: 0x06006AD7 RID: 27351 RVA: 0x0016CB52 File Offset: 0x0016AD52
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001180 RID: 4480
			// (get) Token: 0x06006AD8 RID: 27352 RVA: 0x0016CB5F File Offset: 0x0016AD5F
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006AD9 RID: 27353 RVA: 0x0016CB6C File Offset: 0x0016AD6C
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x06006ADA RID: 27354 RVA: 0x0016CB80 File Offset: 0x0016AD80
			public bool MoveNext()
			{
				switch (this._state)
				{
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Start:
					if (this.TryReadToNextElement())
					{
						this.ReadElement();
					}
					else
					{
						this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					}
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Reading:
					this.ReadElement();
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending:
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					break;
				}
				bool flag;
				if (this._state == Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows)
				{
					this._previousElement = default(T);
					flag = false;
				}
				else
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06006ADB RID: 27355 RVA: 0x0016CBEC File Offset: 0x0016ADEC
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				cancellationToken.ThrowIfCancellationRequested();
				switch (this._state)
				{
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Start:
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this.TryReadToNextElementAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (cultureAwaiter.GetResult())
					{
						await this.ReadElementAsync(cancellationToken).WithCurrentCulture();
					}
					else
					{
						this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					}
					break;
				}
				case Shaper<T>.ObjectQueryNestedEnumerator.State.Reading:
					await this.ReadElementAsync(cancellationToken).WithCurrentCulture();
					break;
				case Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending:
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows;
					break;
				}
				bool flag;
				if (this._state == Shaper<T>.ObjectQueryNestedEnumerator.State.NoRows)
				{
					this._previousElement = default(T);
					flag = false;
				}
				else
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06006ADC RID: 27356 RVA: 0x0016CC39 File Offset: 0x0016AE39
			private void ReadElement()
			{
				this._previousElement = this._rowEnumerator.RootCoordinator.Current;
				if (this.TryReadToNextElement())
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Reading;
					return;
				}
				this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending;
			}

			// Token: 0x06006ADD RID: 27357 RVA: 0x0016CC68 File Offset: 0x0016AE68
			private async Task ReadElementAsync(CancellationToken cancellationToken)
			{
				this._previousElement = this._rowEnumerator.RootCoordinator.Current;
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this.TryReadToNextElementAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.Reading;
				}
				else
				{
					this._state = Shaper<T>.ObjectQueryNestedEnumerator.State.NoRowsLastElementPending;
				}
			}

			// Token: 0x06006ADE RID: 27358 RVA: 0x0016CCB5 File Offset: 0x0016AEB5
			private bool TryReadToNextElement()
			{
				while (this._rowEnumerator.MoveNext())
				{
					if (this._rowEnumerator.Current[0] != null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006ADF RID: 27359 RVA: 0x0016CCD8 File Offset: 0x0016AED8
			private async Task<bool> TryReadToNextElementAsync(CancellationToken cancellationToken)
			{
				do
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this._rowEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						goto Block_3;
					}
				}
				while (this._rowEnumerator.Current[0] == null);
				return true;
				Block_3:
				return false;
			}

			// Token: 0x06006AE0 RID: 27360 RVA: 0x0016CD25 File Offset: 0x0016AF25
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x040030F5 RID: 12533
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x040030F6 RID: 12534
			private T _previousElement;

			// Token: 0x040030F7 RID: 12535
			private Shaper<T>.ObjectQueryNestedEnumerator.State _state;

			// Token: 0x02000D94 RID: 3476
			private enum State
			{
				// Token: 0x040033A7 RID: 13223
				Start,
				// Token: 0x040033A8 RID: 13224
				Reading,
				// Token: 0x040033A9 RID: 13225
				NoRowsLastElementPending,
				// Token: 0x040033AA RID: 13226
				NoRows
			}
		}

		// Token: 0x02000C66 RID: 3174
		private class RecordStateEnumerator : IDbEnumerator<RecordState>, IEnumerator<RecordState>, IDisposable, IEnumerator, IDbAsyncEnumerator<RecordState>, IDbAsyncEnumerator
		{
			// Token: 0x06006AE1 RID: 27361 RVA: 0x0016CD32 File Offset: 0x0016AF32
			internal RecordStateEnumerator(Shaper<T>.RowNestedResultEnumerator rowEnumerator)
			{
				this._rowEnumerator = rowEnumerator;
				this._current = null;
				this._depth = -1;
				this._readerConsumed = false;
			}

			// Token: 0x17001181 RID: 4481
			// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x0016CD56 File Offset: 0x0016AF56
			public RecordState Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17001182 RID: 4482
			// (get) Token: 0x06006AE3 RID: 27363 RVA: 0x0016CD5E File Offset: 0x0016AF5E
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17001183 RID: 4483
			// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x0016CD66 File Offset: 0x0016AF66
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06006AE5 RID: 27365 RVA: 0x0016CD6E File Offset: 0x0016AF6E
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this._rowEnumerator.Dispose();
			}

			// Token: 0x06006AE6 RID: 27366 RVA: 0x0016CD84 File Offset: 0x0016AF84
			public bool MoveNext()
			{
				if (!this._readerConsumed)
				{
					Coordinator coordinator;
					for (;;)
					{
						if (-1 == this._depth || this._rowEnumerator.Current.Length == this._depth)
						{
							if (!this._rowEnumerator.MoveNext())
							{
								break;
							}
							this._depth = 0;
						}
						coordinator = this._rowEnumerator.Current[this._depth];
						if (coordinator != null)
						{
							goto Block_3;
						}
						this._depth++;
					}
					this._current = null;
					this._readerConsumed = true;
					goto IL_0097;
					Block_3:
					this._current = ((Coordinator<RecordState>)coordinator).Current;
					this._depth++;
				}
				IL_0097:
				return !this._readerConsumed;
			}

			// Token: 0x06006AE7 RID: 27367 RVA: 0x0016CE34 File Offset: 0x0016B034
			public async Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				if (!this._readerConsumed)
				{
					cancellationToken.ThrowIfCancellationRequested();
					Coordinator coordinator;
					for (;;)
					{
						if (-1 == this._depth || this._rowEnumerator.Current.Length == this._depth)
						{
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = this._rowEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
							if (!cultureAwaiter.IsCompleted)
							{
								await cultureAwaiter;
								global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
								cultureAwaiter = cultureAwaiter2;
								cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
							}
							if (!cultureAwaiter.GetResult())
							{
								break;
							}
							this._depth = 0;
						}
						coordinator = this._rowEnumerator.Current[this._depth];
						if (coordinator != null)
						{
							goto Block_5;
						}
						this._depth++;
					}
					this._current = null;
					this._readerConsumed = true;
					goto IL_0120;
					Block_5:
					this._current = ((Coordinator<RecordState>)coordinator).Current;
					this._depth++;
				}
				IL_0120:
				return !this._readerConsumed;
			}

			// Token: 0x06006AE8 RID: 27368 RVA: 0x0016CE81 File Offset: 0x0016B081
			public void Reset()
			{
				this._rowEnumerator.Reset();
			}

			// Token: 0x040030F8 RID: 12536
			private readonly Shaper<T>.RowNestedResultEnumerator _rowEnumerator;

			// Token: 0x040030F9 RID: 12537
			private RecordState _current;

			// Token: 0x040030FA RID: 12538
			private int _depth;

			// Token: 0x040030FB RID: 12539
			private bool _readerConsumed;
		}
	}
}
