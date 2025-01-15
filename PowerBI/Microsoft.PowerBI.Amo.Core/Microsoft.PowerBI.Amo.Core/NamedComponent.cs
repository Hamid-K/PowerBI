using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A5 RID: 165
	[Designer("Microsoft.DataWarehouse.Design.NameComponentDesigner, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91")]
	[Guid("AA77A7C5-38A3-41ad-BAE8-3101FE6A116D")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
	public abstract class NamedComponent : ModelComponent, INamedComponent, IModelComponent, IComponent, IDisposable, IFormattable
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x00027306 File Offset: 0x00025506
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			if (format == null)
			{
				format = "{0:G}";
			}
			return string.Format(formatProvider, format, this.Name);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0002731F File Offset: 0x0002551F
		protected internal override string FriendlyName
		{
			get
			{
				if (this.componentName != null)
				{
					return this.componentName;
				}
				return this.id;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00027336 File Offset: 0x00025536
		protected internal override string KeyForCollection
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002733E File Offset: 0x0002553E
		protected NamedComponent()
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00027346 File Offset: 0x00025546
		protected NamedComponent(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00027355 File Offset: 0x00025555
		protected NamedComponent(string name, string id)
		{
			this.Name = name;
			this.ID = id;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002736B File Offset: 0x0002556B
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00027388 File Offset: 0x00025588
		[XmlElement(IsNullable = false)]
		[ReadOnly(true)]
		[LocalizedDescription("PropertyDescription_NamedComponent_ID")]
		[LocalizedCategory("PropertyCategory_Basic")]
		public string ID
		{
			get
			{
				if (this.id == null)
				{
					this.ID = this.Name;
				}
				return this.id;
			}
			set
			{
				string text = Utils.Trim(value);
				string text2 = this.id;
				if (text != text2)
				{
					if (base.OwningCollection != null)
					{
						throw new InvalidOperationException(SR.PropertyCannotBeChangedForObjectInCollection("ID", typeof(NamedComponent).Name));
					}
					string text3;
					if (!this.IsSyntacticallyValidID(text, base.GetType(), out text3))
					{
						throw new InvalidOperationException(text3);
					}
					this.id = text;
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000273F2 File Offset: 0x000255F2
		[XmlIgnore]
		internal bool HasID
		{
			get
			{
				return this.id != null;
			}
		}

		// Token: 0x0600080F RID: 2063
		internal abstract bool IsSyntacticallyValidID(string newValue, Type type, out string error);

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000273FD File Offset: 0x000255FD
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00027408 File Offset: 0x00025608
		[XmlElement("Name", IsNullable = false)]
		[LocalizedCategory("PropertyCategory_Basic")]
		[LocalizedDescription("PropertyDescription_NamedComponent_Name")]
		[MergableProperty(false)]
		public string Name
		{
			get
			{
				return this.componentName;
			}
			set
			{
				string text = Utils.Trim(value);
				string name = this.Name;
				if (text != name)
				{
					NamedComponentCollection namedComponentCollection = base.OwningCollection as NamedComponentCollection;
					if (namedComponentCollection != null && namedComponentCollection.BaseGetByName(text, false) == this)
					{
						this.componentName = text;
						return;
					}
					if (base.GetType() == typeof(Database) || base.GetType().IsSubclassOf(typeof(Database)))
					{
						string text2;
						if (!Utils.IsValidNameCharsForDatabase(base.Parent as Server, text, out text2))
						{
							throw new InvalidOperationException(text2);
						}
					}
					else
					{
						ModelType modelType = this.GetModelType();
						int compatibilityLevel = this.GetCompatibilityLevel();
						string text2;
						if (!this.IsValidName(text, base.GetType(), modelType, compatibilityLevel, base.OwningCollection as NamedComponentCollection, out text2))
						{
							throw new InvalidOperationException(text2);
						}
					}
					if (this is MajorObject && this is IMajorObject && ((IMajorObject)this).ParentServer != null && ((MajorObject)this).InternalState == MajorObjectState.Ready && ((IMajorObject)this).ParentServer.Connected)
					{
						((MajorObject)this).CheckBody();
					}
					this.componentName = text;
				}
			}
		}

		// Token: 0x06000812 RID: 2066
		internal abstract bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error);

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00027521 File Offset: 0x00025721
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00027529 File Offset: 0x00025729
		[XmlElement(IsNullable = false)]
		[LocalizedCategory("PropertyCategory_Basic")]
		[LocalizedDescription("PropertyDescription_NamedComponent_Descripton")]
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00027532 File Offset: 0x00025732
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0002753A File Offset: 0x0002573A
		[XmlIgnore]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00027543 File Offset: 0x00025743
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0002755A File Offset: 0x0002575A
		[XmlIgnore]
		[Browsable(false)]
		public string SiteID
		{
			get
			{
				if (this.Site == null)
				{
					return null;
				}
				return this.Site.Name;
			}
			set
			{
				if (this.Site == null)
				{
					throw new InvalidOperationException();
				}
				this.Site.Name = value;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00027576 File Offset: 0x00025776
		protected void CopyTo(NamedComponent obj)
		{
			base.CopyTo(obj);
			if (obj.OwningCollection == null)
			{
				obj.ID = this.ID;
			}
			obj.Name = this.Name;
			obj.Description = this.Description;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000275AB File Offset: 0x000257AB
		protected new void Reset()
		{
			base.Reset();
			this.description = null;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000275BA File Offset: 0x000257BA
		internal override bool IsCompatible(ModelType modelType, int compatibilityLevel, out string error)
		{
			return this.IsValidName(this.componentName, base.GetType(), modelType, compatibilityLevel, null, out error);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000275D2 File Offset: 0x000257D2
		public override string ToString()
		{
			if (this.componentName != null)
			{
				return this.componentName;
			}
			return base.ToString();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000275EC File Offset: 0x000257EC
		public override bool Validate(ValidationErrorCollection errors, bool includeDetailedErrors, ServerEdition serverEdition)
		{
			if (!base.Validate(errors, includeDetailedErrors, serverEdition))
			{
				return false;
			}
			int count = errors.Count;
			if (this.Name == null)
			{
				errors.Add(this, ValidationSR.NameIsMissing);
			}
			if (this.ID == null)
			{
				errors.Add(this, ValidationSR.IDIsMissing);
			}
			return count == errors.Count;
		}

		// Token: 0x040004A6 RID: 1190
		public const int MaxNameLength = 100;

		// Token: 0x040004A7 RID: 1191
		public const int MaxIDLength = 100;

		// Token: 0x040004A8 RID: 1192
		private string id;

		// Token: 0x040004A9 RID: 1193
		private string componentName;

		// Token: 0x040004AA RID: 1194
		private string description;
	}
}
