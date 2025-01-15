using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000DE RID: 222
	[XmlRoot("Assembly", Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[DesignerSerializer("Microsoft.DataWarehouse.Serialization.DesignXmlSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.DesignerComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[DesignerSerializer("Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[Guid("3E7DF179-D73A-4426-A560-759FA6BC0FC3")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
	public abstract class Assembly : MajorObject, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00030E48 File Offset: 0x0002F048
		Server IMajorObject.ParentServer
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				if (majorObject != null)
				{
					return majorObject.ParentServer;
				}
				return null;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00030E6C File Offset: 0x0002F06C
		Database IMajorObject.ParentDatabase
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				if (majorObject != null)
				{
					return majorObject.ParentDatabase;
				}
				return null;
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00030E90 File Offset: 0x0002F090
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			IMajorObject majorObject = base.Parent as IMajorObject;
			if (majorObject != null)
			{
				majorObject.WriteRef(writer);
			}
			writer.WriteElementString("AssemblyID", base.ID);
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00030EC4 File Offset: 0x0002F0C4
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00030ECC File Offset: 0x0002F0CC
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		string IMajorObject.Path
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				return ((majorObject == null) ? string.Empty : majorObject.Path) + "<AssemblyID>" + base.ID + "</AssemblyID>";
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00030F12 File Offset: 0x0002F112
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00030F1A File Offset: 0x0002F11A
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && (base.Parent == obj || ((IMajorObject)this).ParentDatabase == obj || ((IMajorObject)this).ParentServer == obj);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00030F3E File Offset: 0x0002F13E
		internal override IObjectReference GetObjectReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00030F45 File Offset: 0x0002F145
		internal override Type GetBaseType()
		{
			return typeof(Assembly);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00030F51 File Offset: 0x0002F151
		internal Assembly()
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00030F59 File Offset: 0x0002F159
		internal Assembly(string name)
			: base(name)
		{
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00030F62 File Offset: 0x0002F162
		internal Assembly(string name, string id)
			: base(name, id)
		{
		}
	}
}
