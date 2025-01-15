using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x0200063A RID: 1594
	internal class Coordinator<T> : Coordinator
	{
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x0010ED1B File Offset: 0x0010CF1B
		internal virtual T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0010ED24 File Offset: 0x0010CF24
		internal Coordinator(CoordinatorFactory<T> coordinatorFactory, Coordinator parent, Coordinator next)
			: base(coordinatorFactory, parent, next)
		{
			this.TypedCoordinatorFactory = coordinatorFactory;
			Coordinator coordinator = null;
			foreach (CoordinatorFactory coordinatorFactory2 in coordinatorFactory.NestedCoordinators.Reverse<CoordinatorFactory>())
			{
				base.Child = coordinatorFactory2.CreateCoordinator(this, coordinator);
				coordinator = base.Child;
			}
			this.IsUsingElementCollection = !base.IsRoot && typeof(T) != typeof(RecordState);
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0010EDC0 File Offset: 0x0010CFC0
		internal override void ResetCollection(Shaper shaper)
		{
			if (this._handleClose != null)
			{
				this._handleClose(shaper, this._wrappedElements);
				this._handleClose = null;
			}
			base.IsEntered = false;
			if (this.IsUsingElementCollection)
			{
				this._elements = this.TypedCoordinatorFactory.InitializeCollection(shaper);
				this._wrappedElements = new List<IEntityWrapper>();
			}
			if (base.Child != null)
			{
				base.Child.ResetCollection(shaper);
			}
			if (this.Next != null)
			{
				this.Next.ResetCollection(shaper);
			}
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0010EE48 File Offset: 0x0010D048
		internal override void ReadNextElement(Shaper shaper)
		{
			IEntityWrapper entityWrapper = null;
			T t;
			try
			{
				if (this.TypedCoordinatorFactory.WrappedElement == null)
				{
					t = this.TypedCoordinatorFactory.Element(shaper);
				}
				else
				{
					entityWrapper = this.TypedCoordinatorFactory.WrappedElement(shaper);
					t = (T)((object)entityWrapper.Entity);
				}
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType() && !shaper.Reader.IsClosed)
				{
					this.ResetCollection(shaper);
					t = this.TypedCoordinatorFactory.ElementWithErrorHandling(shaper);
				}
				throw;
			}
			if (this.IsUsingElementCollection)
			{
				this._elements.Add(t);
				if (entityWrapper != null)
				{
					this._wrappedElements.Add(entityWrapper);
					return;
				}
			}
			else
			{
				this._current = t;
			}
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0010EF04 File Offset: 0x0010D104
		internal void RegisterCloseHandler(Action<Shaper, List<IEntityWrapper>> closeHandler)
		{
			this._handleClose = closeHandler;
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0010EF0D File Offset: 0x0010D10D
		internal void SetCurrentToDefault()
		{
			this._current = default(T);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0010EF1B File Offset: 0x0010D11B
		private IEnumerable<T> GetElements()
		{
			return this._elements;
		}

		// Token: 0x04001B31 RID: 6961
		internal readonly CoordinatorFactory<T> TypedCoordinatorFactory;

		// Token: 0x04001B32 RID: 6962
		private T _current;

		// Token: 0x04001B33 RID: 6963
		private ICollection<T> _elements;

		// Token: 0x04001B34 RID: 6964
		private List<IEntityWrapper> _wrappedElements;

		// Token: 0x04001B35 RID: 6965
		private Action<Shaper, List<IEntityWrapper>> _handleClose;

		// Token: 0x04001B36 RID: 6966
		private readonly bool IsUsingElementCollection;
	}
}
