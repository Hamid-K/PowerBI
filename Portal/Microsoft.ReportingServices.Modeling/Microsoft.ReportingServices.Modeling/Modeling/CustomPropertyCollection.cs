using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000043 RID: 67
	public sealed class CustomPropertyCollection : CheckedCollection<CustomProperty>, ICloneable, IXmlLoadable, IPersistable
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000A558 File Offset: 0x00008758
		public CustomPropertyCollection()
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A560 File Offset: 0x00008760
		public CustomPropertyCollection(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700005B RID: 91
		public CustomProperty this[QName name]
		{
			get
			{
				return base.Items.Find(CustomPropertyCollection.NameMatch(name));
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A57C File Offset: 0x0000877C
		public CustomPropertyCollection Clone()
		{
			CustomPropertyCollection customPropertyCollection = new CustomPropertyCollection(base.Count);
			foreach (CustomProperty customProperty in this)
			{
				customPropertyCollection.Add(customProperty.Clone());
			}
			return customPropertyCollection;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A5DC File Offset: 0x000087DC
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public CustomProperty Add(QName name)
		{
			CustomProperty customProperty = new CustomProperty(name);
			base.Add(customProperty);
			return customProperty;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A600 File Offset: 0x00008800
		public bool Contains(QName name)
		{
			return base.Items.Exists(CustomPropertyCollection.NameMatch(name));
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A613 File Offset: 0x00008813
		public List<CustomProperty> GetAll(QName name)
		{
			return base.Items.FindAll(CustomPropertyCollection.NameMatch(name));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A626 File Offset: 0x00008826
		public void RemoveAll(QName name)
		{
			base.CheckWriteable();
			base.Items.RemoveAll(CustomPropertyCollection.NameMatch(name));
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A640 File Offset: 0x00008840
		internal void MakeReadOnly()
		{
			foreach (CustomProperty customProperty in this)
			{
				customProperty.MakeReadOnly();
			}
			base.SetReadOnlyIndicator();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A694 File Offset: 0x00008894
		private static Predicate<CustomProperty> NameMatch(QName name)
		{
			return (CustomProperty item) => item.Name == name;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A6AD File Offset: 0x000088AD
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject("CustomProperties", this);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A6C1 File Offset: 0x000088C1
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A6C4 File Offset: 0x000088C4
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "CustomProperty")
			{
				CustomPropertyCollection.CustomPropertyLoader customPropertyLoader = new CustomPropertyCollection.CustomPropertyLoader();
				xr.LoadObject(customPropertyLoader);
				base.Add(customPropertyLoader.GetCustomProperty());
				return true;
			}
			return false;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A708 File Offset: 0x00008908
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteCollectionElement<CustomProperty>("CustomProperties", this, delegate(CustomProperty item)
			{
				CustomPropertyCollection.WriteCustomProperty(xw, item);
			});
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A740 File Offset: 0x00008940
		private static void WriteCustomProperty(ModelingXmlWriter xw, CustomProperty item)
		{
			xw.WriteStartElement("CustomProperty");
			xw.WriteAttribute("Name", item.Name);
			if (item.Value != null)
			{
				xw.WriteTypedElement("Value", item.Value);
			}
			xw.WriteEndElement();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A78D File Offset: 0x0000898D
		private static CustomPropertyCollection CreateEmptyInstance()
		{
			CustomPropertyCollection customPropertyCollection = new CustomPropertyCollection();
			customPropertyCollection.MakeReadOnly();
			return customPropertyCollection;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A79A File Offset: 0x0000899A
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A7A4 File Offset: 0x000089A4
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CustomPropertyCollection.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.WriteRIFList<CustomProperty>(this);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A813 File Offset: 0x00008A13
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A81C File Offset: 0x00008A1C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(CustomPropertyCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					reader.ReadListOfRIFObjects(this);
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A8BC File Offset: 0x00008ABC
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.CustomPropertyCollection;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref CustomPropertyCollection.__declaration, CustomPropertyCollection.__declarationLock, () => new Declaration(ObjectType.CustomPropertyCollection, ObjectType.CheckedCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.CustomProperty)
				}));
			}
		}

		// Token: 0x04000164 RID: 356
		internal const string CustomPropertiesElem = "CustomProperties";

		// Token: 0x04000165 RID: 357
		private const string CustomPropertyElem = "CustomProperty";

		// Token: 0x04000166 RID: 358
		private const string NameAttr = "Name";

		// Token: 0x04000167 RID: 359
		private const string ValueElem = "Value";

		// Token: 0x04000168 RID: 360
		public static readonly CustomPropertyCollection Empty = CustomPropertyCollection.CreateEmptyInstance();

		// Token: 0x04000169 RID: 361
		private static Declaration __declaration;

		// Token: 0x0400016A RID: 362
		private static readonly object __declarationLock = new object();

		// Token: 0x02000120 RID: 288
		private class CustomPropertyLoader : IXmlLoadable
		{
			// Token: 0x06000DAA RID: 3498 RVA: 0x0002D03A File Offset: 0x0002B23A
			internal CustomPropertyLoader()
			{
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0002D042 File Offset: 0x0002B242
			public bool LoadXmlAttribute(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Name")
				{
					this.m_qname = xr.ReadValueAsQName();
					return true;
				}
				return false;
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x0002D06D File Offset: 0x0002B26D
			public bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Value")
				{
					this.m_value = xr.ReadTypedValue();
					return true;
				}
				return false;
			}

			// Token: 0x06000DAD RID: 3501 RVA: 0x0002D098 File Offset: 0x0002B298
			public CustomProperty GetCustomProperty()
			{
				if (this.m_qname.Name.Length == 0)
				{
					throw new InternalModelingException("Missing value for m_qname");
				}
				return new CustomProperty(this.m_qname, this.m_value);
			}

			// Token: 0x040005B2 RID: 1458
			private QName m_qname;

			// Token: 0x040005B3 RID: 1459
			private object m_value;
		}
	}
}
