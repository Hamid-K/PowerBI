using System;
using System.Xml;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000112 RID: 274
	public class Role : Role, ICloneable, IMajorObject
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0007E45B File Offset: 0x0007C65B
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return base.Parent as Database;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0007E468 File Offset: 0x0007C668
		private IMajorObject ParentOrException
		{
			get
			{
				IMajorObject majorObject = (IMajorObject)base.Parent;
				if (majorObject == null)
				{
					throw Utils.CreateParentMissingException(this, typeof(IMajorObject));
				}
				return majorObject;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0007E496 File Offset: 0x0007C696
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			this.ParentOrException.WriteRef(writer);
			writer.WriteElementString("RoleID", base.ID);
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0007E4BD File Offset: 0x0007C6BD
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0007E4C8 File Offset: 0x0007C6C8
		Server IMajorObject.ParentServer
		{
			get
			{
				IModelComponent parent = base.Parent;
				Database database = parent as Database;
				if (database != null)
				{
					return database.Parent;
				}
				return parent as Server;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0007E4F4 File Offset: 0x0007C6F4
		string IMajorObject.Path
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				return ((majorObject == null) ? string.Empty : majorObject.Path) + "<RoleID>" + base.ID + "</RoleID>";
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0007E532 File Offset: 0x0007C732
		ObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return (ObjectReference)this.GetObjectReference();
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0007E53F File Offset: 0x0007C73F
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && base.Parent == obj;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0007E550 File Offset: 0x0007C750
		internal override IObjectReference GetObjectReference()
		{
			IMajorObject majorObject = base.Parent as IMajorObject;
			if (majorObject is Server)
			{
				return new ObjectReference
				{
					RoleID = base.ID
				};
			}
			if (majorObject is Database)
			{
				ObjectReference objectReference = majorObject.ObjectReference;
				if (objectReference != null)
				{
					objectReference.RoleID = base.ID;
					return objectReference;
				}
			}
			return null;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0007E5A4 File Offset: 0x0007C7A4
		internal override Type GetBaseType()
		{
			return typeof(Role);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0007E5B0 File Offset: 0x0007C7B0
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new Role.RoleBody(this);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0007E5B8 File Offset: 0x0007C7B8
		public Role()
		{
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0007E5C0 File Offset: 0x0007C7C0
		public Role(string name)
			: base(name)
		{
			base.ID = Utils.GetNewID(name, base.GetType());
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0007E5DB File Offset: 0x0007C7DB
		public Role(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0007E5E5 File Offset: 0x0007C7E5
		object ICloneable.Clone()
		{
			return this.CopyTo(new Role());
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0007E5F2 File Offset: 0x0007C7F2
		public Role Clone()
		{
			return this.CopyTo(new Role());
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0007E600 File Offset: 0x0007C800
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (!(destination is Role))
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0007E63D File Offset: 0x0007C83D
		public Role CopyTo(Role obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0007E648 File Offset: 0x0007C848
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new Role();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0007E664 File Offset: 0x0007C864
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0007E66E File Offset: 0x0007C86E
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}
	}
}
