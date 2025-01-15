using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000EA RID: 234
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Guid("59FDCCCD-71F2-4C6F-84A0-2D8EA1FB7072")]
	public abstract class Role : MajorObject, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00031904 File Offset: 0x0002FB04
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return base.Parent as Database;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00031911 File Offset: 0x0002FB11
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			this.ParentOrException.WriteRef(writer);
			writer.WriteElementString("RoleID", base.ID);
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00031930 File Offset: 0x0002FB30
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00031938 File Offset: 0x0002FB38
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00031940 File Offset: 0x0002FB40
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

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003196C File Offset: 0x0002FB6C
		string IMajorObject.Path
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				return ((majorObject == null) ? string.Empty : majorObject.Path) + "<RoleID>" + base.ID + "</RoleID>";
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000319AA File Offset: 0x0002FBAA
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000319B2 File Offset: 0x0002FBB2
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && base.Parent == obj;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000319C2 File Offset: 0x0002FBC2
		internal Role()
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000319CA File Offset: 0x0002FBCA
		internal Role(string name)
			: base(name)
		{
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000319D3 File Offset: 0x0002FBD3
		internal Role(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000319E0 File Offset: 0x0002FBE0
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

		// Token: 0x06000E7B RID: 3707 RVA: 0x00031A0E File Offset: 0x0002FC0E
		internal override IObjectReference GetObjectReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00031A15 File Offset: 0x0002FC15
		internal override Type GetBaseType()
		{
			return typeof(Role);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00031A24 File Offset: 0x0002FC24
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Role role = destination as Role;
			if (role == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			this.Members.CopyTo(role.Members);
			this.ExternalMembers.CopyTo(role.ExternalMembers);
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00031A8F File Offset: 0x0002FC8F
		[XmlArray]
		[XmlArrayItem("Member", typeof(RoleMember))]
		[Browsable(false)]
		public RoleMemberCollection Members
		{
			get
			{
				return base.GetBody<Role.RoleBody>().Members;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00031A9C File Offset: 0x0002FC9C
		[XmlArray(Namespace = "http://schemas.microsoft.com/analysisservices/2013/engine/500/500")]
		[XmlArrayItem(ElementName = "ExternalMember", Type = typeof(ExternalRoleMember), Namespace = "http://schemas.microsoft.com/analysisservices/2013/engine/500/500")]
		[Browsable(false)]
		public ExternalRoleMemberCollection ExternalMembers
		{
			get
			{
				return base.GetBody<Role.RoleBody>().ExtMembers;
			}
		}

		// Token: 0x020001A9 RID: 425
		internal sealed class RoleBody : MajorObject.MajorObjectBody
		{
			// Token: 0x0600133B RID: 4923 RVA: 0x0004365C File Offset: 0x0004185C
			public RoleBody(Role owner)
				: base(owner)
			{
				this.Members = new RoleMemberCollection();
				this.ExtMembers = new ExternalRoleMemberCollection();
			}

			// Token: 0x040010D4 RID: 4308
			public RoleMemberCollection Members;

			// Token: 0x040010D5 RID: 4309
			public ExternalRoleMemberCollection ExtMembers;
		}
	}
}
