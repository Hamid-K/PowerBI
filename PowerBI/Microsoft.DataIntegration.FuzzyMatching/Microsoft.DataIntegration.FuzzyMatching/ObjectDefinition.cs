using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class ObjectDefinition : IXmlSerializable, IBinarySerialize, IName
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004D35 File Offset: 0x00002F35
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004D3D File Offset: 0x00002F3D
		public string Name { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004D46 File Offset: 0x00002F46
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004D4E File Offset: 0x00002F4E
		public string AssemblyName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004D57 File Offset: 0x00002F57
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004D5F File Offset: 0x00002F5F
		public string TypeName { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004D68 File Offset: 0x00002F68
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004D70 File Offset: 0x00002F70
		public bool IsReference { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004D79 File Offset: 0x00002F79
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004D81 File Offset: 0x00002F81
		public bool AcquireReferences { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004D8A File Offset: 0x00002F8A
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004D92 File Offset: 0x00002F92
		public List<Property> Properties { get; set; }

		// Token: 0x060000F1 RID: 241 RVA: 0x00004D9B File Offset: 0x00002F9B
		public ObjectDefinition()
		{
			this.AssemblyName = typeof(FuzzyLookup).Assembly.ToString();
			this.Properties = new List<Property>();
			this.AcquireReferences = true;
			this.IsReference = false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public ObjectDefinition(XmlReader xml)
			: this()
		{
			this.ReadXml(xml);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public ObjectDefinition(ObjectDefinition obj)
		{
			this.Name = obj.Name;
			this.AssemblyName = obj.AssemblyName;
			this.TypeName = obj.TypeName;
			this.Properties = new List<Property>();
			foreach (Property property in obj.Properties)
			{
				this.Properties.Add(new Property(property));
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004E7C File Offset: 0x0000307C
		public virtual string GetXmlElementName()
		{
			return "Object";
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004E84 File Offset: 0x00003084
		public virtual object CreateInstance()
		{
			object obj;
			if (this.IsReference)
			{
				obj = SqlClr.ObjectManager.GetObject(this.Name);
			}
			else
			{
				obj = ObjectDefinition.CreateInstance(this.AssemblyName, this.TypeName, this.Properties);
			}
			if (this.AcquireReferences && obj is IObjectReferenceContainer)
			{
				(obj as IObjectReferenceContainer).AcquireReferences();
			}
			return obj;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static object CreateInstance(string assemblyName, string typeName, List<Property> properties)
		{
			if (!typeName.Contains("."))
			{
				typeName = typeof(FuzzyLookup).Namespace.ToString() + "." + typeName;
			}
			object obj = Activator.CreateInstance(assemblyName, typeName).Unwrap();
			Property.SetProperties(obj, properties);
			return obj;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004F30 File Offset: 0x00003130
		public virtual void Write(BinaryWriter w)
		{
			w.Write(this.Name);
			w.Write(this.AssemblyName);
			w.Write(this.TypeName);
			w.Write(this.Properties.Count);
			foreach (Property property in this.Properties)
			{
				property.Write(w);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004FB8 File Offset: 0x000031B8
		public virtual void Read(BinaryReader r)
		{
			this.Name = r.ReadString();
			this.AssemblyName = r.ReadString();
			this.TypeName = r.ReadString();
			int num = r.ReadInt32();
			this.Properties = new List<Property>(num);
			for (int i = 0; i < num; i++)
			{
				Property property = new Property();
				property.Read(r);
				this.Properties.Add(property);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005021 File Offset: 0x00003221
		public virtual XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005024 File Offset: 0x00003224
		public virtual void ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing(this.GetXmlElementName()))
			{
				if (reader.MoveToAttribute("name"))
				{
					this.Name = reader.Value;
				}
				if (reader.MoveToAttribute("assemblyName"))
				{
					this.AssemblyName = reader.Value;
				}
				if (reader.MoveToAttribute("typeName"))
				{
					this.TypeName = reader.Value;
				}
				if (reader.MoveToAttribute("isReference"))
				{
					this.IsReference = bool.Parse(reader.Value);
				}
				if (this.IsReference && (string.IsNullOrEmpty(this.Name) || !string.IsNullOrEmpty(this.AssemblyName) || !string.IsNullOrEmpty(this.TypeName)))
				{
					throw new XmlException("If isReference attribute is true, the name attribute must be defined and the assemblyName and typeName attributes must be empty.");
				}
				if (reader.ReadToFollowing("Properties"))
				{
					CollectionSerialization.ReadXml<Property>(reader.ReadSubtree(), "Properties", this.Properties);
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005110 File Offset: 0x00003310
		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(this.GetXmlElementName());
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.AssemblyName))
			{
				writer.WriteAttributeString("assemblyName", this.AssemblyName);
			}
			if (!string.IsNullOrEmpty(this.TypeName))
			{
				writer.WriteAttributeString("typeName", this.TypeName);
			}
			writer.WriteAttributeString("isReference", this.IsReference.ToString());
			CollectionSerialization.WriteXml<Property>(writer, "Properties", this.Properties);
			writer.WriteEndElement();
		}
	}
}
