using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000CF RID: 207
	[XmlInclude(typeof(ComAssembly))]
	[XmlInclude(typeof(ClrAssembly))]
	[XmlRoot("Assembly", Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[DesignerSerializer("Microsoft.DataWarehouse.Serialization.DesignXmlSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.DesignerComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[DesignerSerializer("Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", "Microsoft.DataWarehouse.Serialization.OnlineComponentSerializer, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[Guid("8EF950A1-B9A6-4d80-9891-7DB638615A2D")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
	public abstract class Assembly : Assembly, IMajorObject, ICloneable, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0006C3E4 File Offset: 0x0006A5E4
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

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0006C408 File Offset: 0x0006A608
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

		// Token: 0x06000CFB RID: 3323 RVA: 0x0006C42C File Offset: 0x0006A62C
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			IMajorObject majorObject = base.Parent as IMajorObject;
			if (majorObject != null)
			{
				majorObject.WriteRef(writer);
			}
			writer.WriteElementString("AssemblyID", base.ID);
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0006C460 File Offset: 0x0006A660
		Type IMajorObject.BaseType
		{
			get
			{
				return typeof(Assembly);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0006C46C File Offset: 0x0006A66C
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0006C474 File Offset: 0x0006A674
		string IMajorObject.Path
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				return ((majorObject == null) ? string.Empty : majorObject.Path) + "<AssemblyID>" + base.ID + "</AssemblyID>";
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0006C4B4 File Offset: 0x0006A6B4
		ObjectReference IMajorObject.ObjectReference
		{
			get
			{
				IMajorObject majorObject = base.Parent as IMajorObject;
				if (majorObject is Server)
				{
					return new ObjectReference
					{
						AssemblyID = base.ID
					};
				}
				ObjectReference objectReference = majorObject.ObjectReference;
				if (objectReference != null)
				{
					objectReference.AssemblyID = base.ID;
					return objectReference;
				}
				return null;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0006C500 File Offset: 0x0006A700
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return obj != null && (base.Parent == obj || ((IMajorObject)this).ParentDatabase == obj || ((IMajorObject)this).ParentServer == obj);
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0006C524 File Offset: 0x0006A724
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0006C548 File Offset: 0x0006A748
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

		// Token: 0x06000D03 RID: 3331 RVA: 0x0006C56C File Offset: 0x0006A76C
		void IMajorObject.WriteRef(XmlWriter writer)
		{
			((IMajorObject)this).WriteRef(writer);
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0006C575 File Offset: 0x0006A775
		Type IMajorObject.BaseType
		{
			get
			{
				return typeof(Assembly);
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0006C581 File Offset: 0x0006A781
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0006C589 File Offset: 0x0006A789
		string IMajorObject.Path
		{
			get
			{
				return ((IMajorObject)this).Path;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0006C591 File Offset: 0x0006A791
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0006C599 File Offset: 0x0006A799
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return ((IMajorObject)this).DependsOn((IMajorObject)obj);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
		internal override IObjectReference GetObjectReference()
		{
			IMajorObject majorObject = base.Parent as IMajorObject;
			if (majorObject is Server)
			{
				return new ObjectReference
				{
					AssemblyID = base.ID
				};
			}
			ObjectReference objectReference = majorObject.ObjectReference;
			if (objectReference != null)
			{
				objectReference.AssemblyID = base.ID;
				return objectReference;
			}
			return null;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0006C5F4 File Offset: 0x0006A7F4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0006C5FC File Offset: 0x0006A7FC
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Assembly assembly = destination as Assembly;
			if (assembly == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			assembly.ImpersonationInfo = ((this.ImpersonationInfo == null) ? null : this.ImpersonationInfo.Clone());
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0006C661 File Offset: 0x0006A861
		protected Assembly()
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0006C669 File Offset: 0x0006A869
		protected Assembly(string name)
			: base(name)
		{
			base.ID = Utils.GetNewID(name, base.GetType());
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0006C684 File Offset: 0x0006A884
		protected Assembly(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0006C68E File Offset: 0x0006A88E
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0006C69B File Offset: 0x0006A89B
		[XmlElement(IsNullable = false)]
		[LocalizedDescription("PropertyDescription_Assembly_ImpersonationInfo")]
		public ImpersonationInfo ImpersonationInfo
		{
			get
			{
				return base.GetBody<Assembly.AssemblyBody>().ImpersonationInfo;
			}
			set
			{
				base.GetBody<Assembly.AssemblyBody>().ImpersonationInfo = value;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0006C6A9 File Offset: 0x0006A8A9
		public Assembly CopyTo(Assembly obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000D12 RID: 3346
		public abstract Assembly Clone();

		// Token: 0x020002E7 RID: 743
		internal abstract class AssemblyBody : MajorObject.MajorObjectBody
		{
			// Token: 0x060023AE RID: 9134 RVA: 0x000E291B File Offset: 0x000E0B1B
			private protected AssemblyBody(Assembly owner)
				: base(owner)
			{
			}

			// Token: 0x04000AB6 RID: 2742
			public ImpersonationInfo ImpersonationInfo;
		}
	}
}
