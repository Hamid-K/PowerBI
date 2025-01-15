using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FF RID: 511
	internal abstract class ObjectLinkBase<TOwner, TObject> where TOwner : MetadataObject where TObject : MetadataObject
	{
		// Token: 0x06001D35 RID: 7477 RVA: 0x000C79BD File Offset: 0x000C5BBD
		public ObjectLinkBase(TOwner owner, string propertyName)
		{
			this.Owner = owner;
			this.PropertyName = propertyName;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001D36 RID: 7478
		public abstract LinkType LinkType { get; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x000C79D3 File Offset: 0x000C5BD3
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x000C79DB File Offset: 0x000C5BDB
		public TOwner Owner { get; private set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000C79E4 File Offset: 0x000C5BE4
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x000C79EC File Offset: 0x000C5BEC
		public string PropertyName { get; private set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000C79F5 File Offset: 0x000C5BF5
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x000C79FD File Offset: 0x000C5BFD
		public ObjectId ObjectID
		{
			get
			{
				return this.id;
			}
			set
			{
				if (this.id != value)
				{
					this.id = value;
					if (this.obj != null)
					{
						this.obj = default(TObject);
					}
				}
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x000C7A2D File Offset: 0x000C5C2D
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x000C7A38 File Offset: 0x000C5C38
		public virtual TObject Object
		{
			get
			{
				return this.obj;
			}
			set
			{
				if (this.obj != value)
				{
					this.obj = value;
					if (value != null)
					{
						this.id = value.Id;
						return;
					}
					this.id = ObjectId.Null;
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000C7A84 File Offset: 0x000C5C84
		public virtual bool IsResolved
		{
			get
			{
				return this.id.IsNull || this.obj != null;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000C7AA3 File Offset: 0x000C5CA3
		public bool IsNull
		{
			get
			{
				return this.id.IsNull && this.obj == null;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000C7AC4 File Offset: 0x000C5CC4
		public bool ResolveById(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			if (this.id.IsNull)
			{
				this.obj = default(TObject);
				return false;
			}
			MetadataObject metadataObject;
			if (!objectMap.TryGetValue(this.id, out metadataObject))
			{
				if (throwIfCantResolve)
				{
					throw TomInternalException.Create("Could not resolve link to object with Id '{0}'", new object[] { this.id });
				}
				return false;
			}
			else
			{
				if (!(metadataObject is TObject))
				{
					throw TomInternalException.Create("Got an object of type {0}, expected {1}", new object[]
					{
						metadataObject.GetType().Name,
						typeof(TObject).Name
					});
				}
				this.obj = (TObject)((object)metadataObject);
				return true;
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000C7B66 File Offset: 0x000C5D66
		public virtual void CopyFrom(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			if ((context.Flags & CopyFlags.ShallowCopy) == CopyFlags.ShallowCopy)
			{
				this.id = other.id;
				return;
			}
			this.obj = other.obj;
			this.id = other.id;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000C7B9C File Offset: 0x000C5D9C
		public void CompareWith(ObjectLinkBase<TOwner, TObject> other, string linkPropertyName, PropertyFlags extraPropFlags, CompareContext context)
		{
			if (!this.IsEqualTo(other))
			{
				context.RegisterPropertyChange(this.Owner, linkPropertyName, typeof(TObject), PropertyFlags.DdlAndUser | extraPropFlags, other.obj, other.GetPathOrNull(), this.obj, this.GetPathOrNull());
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000C7BF4 File Offset: 0x000C5DF4
		public void CompareWith(ObjectLinkBase<TOwner, TObject> other, string linkDdlPropertyName, string linkUserPropertyName, PropertyFlags extraPropFlags, CompareContext context)
		{
			if (!this.IsEqualTo(other))
			{
				if (!string.IsNullOrEmpty(linkDdlPropertyName))
				{
					context.RegisterPropertyChange(this.Owner, linkDdlPropertyName, typeof(TObject), PropertyFlags.Ddl | extraPropFlags, other.obj, other.GetPathOrNull(), this.obj, this.GetPathOrNull());
				}
				if (!string.IsNullOrEmpty(linkUserPropertyName))
				{
					context.RegisterPropertyChange(this.Owner, linkUserPropertyName, typeof(TObject), PropertyFlags.User | extraPropFlags, other.obj, other.GetPathOrNull(), this.obj, this.GetPathOrNull());
				}
			}
		}

		// Token: 0x06001D45 RID: 7493
		public abstract bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other, CopyContext context);

		// Token: 0x06001D46 RID: 7494 RVA: 0x000C7CA3 File Offset: 0x000C5EA3
		public virtual bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other)
		{
			return this.obj == other.obj;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000C7CBD File Offset: 0x000C5EBD
		protected void CopyObject(MetadataObject other, CopyContext context)
		{
			if (this.id != other.Id)
			{
				this.id = other.Id;
			}
			this.obj.CopyFrom(other, context);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000C7CF0 File Offset: 0x000C5EF0
		internal virtual ObjectPath GetPathOrNull()
		{
			return null;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000C7CF3 File Offset: 0x000C5EF3
		private protected void UpdateObjectId(ObjectId id)
		{
			this.id = id;
		}

		// Token: 0x040006A3 RID: 1699
		private TObject obj;

		// Token: 0x040006A4 RID: 1700
		private ObjectId id;
	}
}
