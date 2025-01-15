using System;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001DD RID: 477
	internal sealed class ChildLink<TOwner, TObject> : ObjectLinkBase<TOwner, TObject> where TOwner : MetadataObject where TObject : MetadataObject
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x000C4233 File Offset: 0x000C2433
		public ChildLink(TOwner owner, string propertyName)
			: base(owner, propertyName)
		{
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000C423D File Offset: 0x000C243D
		public override LinkType LinkType
		{
			get
			{
				return LinkType.ChildLink;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000C4240 File Offset: 0x000C2440
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x000C4248 File Offset: 0x000C2448
		public override TObject Object
		{
			get
			{
				return base.Object;
			}
			set
			{
				if (value == base.Object)
				{
					return;
				}
				if (value != null && value.Parent != null)
				{
					throw new TomException(string.Format("Object '{0}' already has a parent", ClientHostingManager.MarkAsRestrictedInformation(value.ToString(), InfoRestrictionType.CCON)));
				}
				TObject @object = base.Object;
				if (@object != null)
				{
					ObjectChangeTracker.RegisterObjectRemoving(@object, base.Owner);
				}
				if (value != null)
				{
					ObjectChangeTracker.RegisterObjectAdding(value, base.Owner);
				}
				base.Object = value;
				if (@object != null)
				{
					@object.Parent = null;
					ObjectChangeTracker.RegisterObjectRemoved(@object, base.Owner);
				}
				if (value != null)
				{
					value.Parent = base.Owner;
					ObjectChangeTracker.RegisterObjectAdded(value, base.Owner);
				}
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000C4347 File Offset: 0x000C2547
		public override void CopyFrom(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			this.CopyFrom((ChildLink<TOwner, TObject>)other, context);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000C4358 File Offset: 0x000C2558
		public void CopyFrom(ChildLink<TOwner, TObject> other, CopyContext context)
		{
			if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
			{
				base.CopyFrom(other, context);
				return;
			}
			if ((context.Flags & CopyFlags.ShallowCopy) == CopyFlags.ShallowCopy)
			{
				base.CopyFrom(other, context);
				return;
			}
			if ((context.Flags & CopyFlags.Incremental) == CopyFlags.Incremental && base.ObjectID == other.ObjectID && other.Object == null)
			{
				return;
			}
			if (this.Object == null && other.Object != null)
			{
				this.Object = other.Object.CloneInternal<TObject>(context);
				return;
			}
			if (this.Object != null && other.Object == null)
			{
				this.Object = default(TObject);
				return;
			}
			if (this.Object == null || other.Object == null)
			{
				if (this.Object == null)
				{
					TObject tobject = other.Object;
				}
				return;
			}
			if (base.ObjectID.IsNull)
			{
				if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds)
				{
					base.CopyObject(other.Object, context);
					return;
				}
				this.Object.CopyFrom(other.Object, context);
				return;
			}
			else
			{
				if (other.ObjectID.IsNull)
				{
					this.Object.CopyFrom(other.Object, context);
					return;
				}
				if ((context.Flags & CopyFlags.IgnoreIdsForChildLinks) == CopyFlags.IgnoreIdsForChildLinks)
				{
					this.Object.CopyFrom(other.Object, context);
					return;
				}
				if (base.ObjectID != other.ObjectID)
				{
					this.Object = other.Object.CloneInternal<TObject>(context);
					return;
				}
				this.Object.CopyFrom(other.Object, context);
				return;
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000C453C File Offset: 0x000C273C
		public override bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			ChildLink<TOwner, TObject> childLink = (ChildLink<TOwner, TObject>)other;
			if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
			{
				return false;
			}
			if ((context.Flags & CopyFlags.ShallowCopy) == CopyFlags.ShallowCopy)
			{
				return false;
			}
			if ((context.Flags & CopyFlags.Incremental) == CopyFlags.Incremental && base.ObjectID == other.ObjectID && other.Object == null)
			{
				return true;
			}
			if (this.Object == null && other.Object != null)
			{
				return false;
			}
			if (this.Object != null && other.Object == null)
			{
				return false;
			}
			if (this.Object != null && other.Object != null)
			{
				if (base.ObjectID.IsNull)
				{
					return ((context.Flags & CopyFlags.IncludeObjectIds) != CopyFlags.IncludeObjectIds || other.ObjectID.IsNull) && this.Object.GetPath(null).Equals(other.Object.GetPath(null));
				}
				if (other.ObjectID.IsNull)
				{
					return this.Object.GetPath(null).Equals(other.Object.GetPath(null));
				}
				if ((context.Flags & CopyFlags.IgnoreIdsForChildLinks) == CopyFlags.IgnoreIdsForChildLinks)
				{
					return this.Object.GetPath(null).Equals(other.Object.GetPath(null));
				}
				return base.ObjectID == other.ObjectID;
			}
			else
			{
				if (this.Object == null)
				{
					TObject tobject = other.Object;
					return true;
				}
				return true;
			}
		}
	}
}
