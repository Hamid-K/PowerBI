using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000088 RID: 136
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelField : ModelFieldFolderItem, IBindingContext
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x000138DE File Offset: 0x00011ADE
		protected ModelField()
		{
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000138E6 File Offset: 0x00011AE6
		internal override void Reset()
		{
			base.Reset();
			this.m_autoName = true;
			this.__variations = new ModelFieldCollection(this);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00013901 File Offset: 0x00011B01
		internal ModelField(ModelField baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
			this.m_autoName = baseItem.m_autoName;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00013917 File Offset: 0x00011B17
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0001392E File Offset: 0x00011B2E
		public override string Name
		{
			get
			{
				if (this.m_autoName)
				{
					return this.GetAutoName();
				}
				return base.Name;
			}
			set
			{
				base.Name = value;
				this.m_autoName = base.Name.Length == 0;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001394B File Offset: 0x00011B4B
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00013953 File Offset: 0x00011B53
		public bool UseAutoName
		{
			get
			{
				return this.m_autoName;
			}
			set
			{
				base.CheckWriteable();
				this.m_autoName = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00013962 File Offset: 0x00011B62
		public ModelFieldCollection Variations
		{
			get
			{
				if (this.__variations == null)
				{
					this.__variations = this.BaseItem.Variations.CloneFor(this);
				}
				return this.__variations;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00013989 File Offset: 0x00011B89
		public bool IsVariation
		{
			get
			{
				return base.ParentItem is ModelField;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00013999 File Offset: 0x00011B99
		DataSourceView IBindingContext.GetDataSourceView()
		{
			if (this.Model == null)
			{
				return null;
			}
			return this.Model.DataSourceView;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000139B0 File Offset: 0x00011BB0
		Binding IBindingContext.GetParentBinding()
		{
			if (base.Entity == null)
			{
				return null;
			}
			return base.Entity.Binding;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000139C7 File Offset: 0x00011BC7
		internal new ModelField BaseItem
		{
			get
			{
				return (ModelField)base.BaseItem;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000139D4 File Offset: 0x00011BD4
		public IEnumerable<ModelAttribute> GetAggregateVariations()
		{
			foreach (ModelField modelField in this.Variations)
			{
				if (modelField is ModelAttribute && ((ModelAttribute)modelField).IsAggregate)
				{
					yield return (ModelAttribute)modelField;
				}
			}
			List<ModelField>.Enumerator enumerator = default(List<ModelField>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000139E4 File Offset: 0x00011BE4
		public override IEnumerable<ModelItem> GetOwnedItems()
		{
			return this.Variations;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000139EC File Offset: 0x00011BEC
		public override IEnumerable<ModelItem> GetNamespaceItems()
		{
			if (base.Entity != null)
			{
				Bag<ModelItem> bag = new Bag<ModelItem>(base.GetNamespaceItems());
				foreach (ModelField modelField in base.Entity.GetAllFields())
				{
					bag.Add(modelField, true);
				}
				return bag;
			}
			return base.GetNamespaceItems();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00013A5C File Offset: 0x00011C5C
		public override bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			if (!(newParentItem is ModelField))
			{
				return base.IsValidParentItem(newParentItem, out message);
			}
			if (base.IsLazyClone ? (this.BaseItem.Variations.Count > 0) : (this.Variations.Count > 0))
			{
				message = new ValidationMessage(ModelingErrorCode.NestedVariations, Severity.Error, this, SRErrors.NestedVariations(SRObjectDescriptor.FromScope(this)));
				return false;
			}
			if (newParentItem.ParentItem is ModelField)
			{
				message = new ValidationMessage(ModelingErrorCode.NestedVariations, Severity.Error, newParentItem, SRErrors.NestedVariations(SRObjectDescriptor.FromScope(newParentItem)));
				return false;
			}
			message = null;
			return true;
		}

		// Token: 0x06000621 RID: 1569
		protected abstract string GetAutoName();

		// Token: 0x06000622 RID: 1570 RVA: 0x00013AE8 File Offset: 0x00011CE8
		internal override void LoadCore(ModelingXmlReader xr, bool fragment)
		{
			if (!fragment)
			{
				this.m_autoName = true;
			}
			base.LoadCore(xr, fragment);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00013AFC File Offset: 0x00011CFC
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (!(localName == "Name"))
				{
					if (localName == "Variations")
					{
						this.Variations.Load(xr);
						return true;
					}
				}
				else
				{
					if (base.LoadXmlElement(xr))
					{
						this.m_autoName = false;
						return true;
					}
					return false;
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00013B5D File Offset: 0x00011D5D
		internal override bool ShouldSerializeName
		{
			get
			{
				return !this.UseAutoName;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013B68 File Offset: 0x00011D68
		internal void WriteVariations(ModelingXmlWriter xw)
		{
			this.Variations.WriteTo(xw, "Variations");
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00013B7C File Offset: 0x00011D7C
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelField.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.AutoName)
				{
					if (memberName != MemberName.Variations)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					((IPersistable)this.Variations).Serialize(writer);
				}
				else
				{
					writer.Write(this.m_autoName);
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013C08 File Offset: 0x00011E08
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelField.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.AutoName)
					{
						if (memberName != MemberName.Variations)
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						((IPersistable)this.Variations).Deserialize(reader);
					}
					else
					{
						this.m_autoName = reader.ReadBoolean();
					}
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00013CB8 File Offset: 0x00011EB8
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelField.__declaration, ModelField.__declarationLock, () => new Declaration(ObjectType.ModelField, ObjectType.ModelFieldFolderItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.AutoName, Token.Boolean),
					new MemberInfo(MemberName.Variations, ObjectType.OwnedModelItemCollection)
				}));
			}
		}

		// Token: 0x04000323 RID: 803
		private const string VariationsElem = "Variations";

		// Token: 0x04000324 RID: 804
		private bool m_autoName;

		// Token: 0x04000325 RID: 805
		private ModelFieldCollection __variations;

		// Token: 0x04000326 RID: 806
		private static Declaration __declaration;

		// Token: 0x04000327 RID: 807
		private static readonly object __declarationLock = new object();
	}
}
