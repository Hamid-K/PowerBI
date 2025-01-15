using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000007 RID: 7
	internal class JaXmlSerializer
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003C90 File Offset: 0x00001E90
		static JaXmlSerializer()
		{
			JaXmlSerializer.WriterSettings.Encoding = Encoding.UTF8;
			JaXmlSerializer.WriterSettings.Indent = true;
			JaXmlSerializer.WriterSettings.IndentChars = "  ";
			JaXmlSerializer.WriterSettings.OmitXmlDeclaration = true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public JaXmlSerializer(Type rootType, XmlAttributeOverrides overrides_, DesignXmlSerializerOptions options)
		{
			this.rootType = rootType;
			this.options = options;
			this.attributesOverrides = overrides_;
			if (this.attributesOverrides == null)
			{
				this.attributesOverrides = new XmlAttributeOverrides();
			}
			XmlAttributes xmlAttributes = new XmlAttributes();
			xmlAttributes.XmlIgnore = true;
			XmlAttributes xmlAttributes2 = this.attributesOverrides[typeof(Component), "Site"];
			if (xmlAttributes2 == null)
			{
				this.attributesOverrides.Add(typeof(Component), "Site", xmlAttributes);
			}
			else
			{
				xmlAttributes2.XmlIgnore = true;
			}
			xmlAttributes2 = this.attributesOverrides[typeof(Component), "Container"];
			if (xmlAttributes2 == null)
			{
				this.attributesOverrides.Add(typeof(Component), "Container", xmlAttributes);
			}
			else
			{
				xmlAttributes2.XmlIgnore = true;
			}
			try
			{
				Type type = Type.GetType("Microsoft.VisualStudio.Configuration.ManagedPropertiesService, Microsoft.VisualStudio, PublicKeyToken=b03f5f7f11d50a3a", false);
				if (type != null)
				{
					this.attributesOverrides.Add(type, "DynamicProperties", xmlAttributes);
				}
				Type type2 = Type.GetType("Microsoft.VisualStudio.Designer.Host.DesignerHost+HostExtenderProvider, Microsoft.VisualStudio, PublicKeyToken=b03f5f7f11d50a3a", false);
				if (type2 != null)
				{
					this.attributesOverrides.Add(type2, "Name", xmlAttributes);
				}
			}
			catch
			{
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003E00 File Offset: 0x00002000
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00003E08 File Offset: 0x00002008
		public IDesignerSerializationManager SerializationManager
		{
			get
			{
				return this.serializationManager;
			}
			set
			{
				this.serializationManager = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003E11 File Offset: 0x00002011
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00003E19 File Offset: 0x00002019
		public object UserSerializationOptions
		{
			get
			{
				return this.userSerializationOptions;
			}
			set
			{
				this.userSerializationOptions = value;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003E22 File Offset: 0x00002022
		public void Serialize(XmlWriter writer, object root)
		{
			new DesignXmlWriter(this.attributesOverrides, this.options, this.userSerializationOptions).SerializeComponent(writer, this.rootType, root);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003E48 File Offset: 0x00002048
		[EditorBrowsable(EditorBrowsableState.Never)]
		public JaXmlSerializer()
			: this(null, null, DesignXmlSerializerOptions.Default)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003E53 File Offset: 0x00002053
		[EditorBrowsable(EditorBrowsableState.Never)]
		public JaXmlSerializer(DesignXmlSerializerOptions options)
			: this(null, null, options)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003E5E File Offset: 0x0000205E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public JaXmlSerializer(XmlAttributeOverrides xmlAttributeOverrides, DesignXmlSerializerOptions serializerOptions)
			: this(null, xmlAttributeOverrides, serializerOptions)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003E69 File Offset: 0x00002069
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IComponent DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type root)
		{
			return this.DeserializeComponentImpl(manager, reader, root, this.options);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003E7C File Offset: 0x0000207C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IComponent DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type root, bool binaryXmlEnabled)
		{
			DesignXmlSerializerOptions designXmlSerializerOptions = this.options;
			if (binaryXmlEnabled)
			{
				designXmlSerializerOptions |= DesignXmlSerializerOptions.BinaryXml;
			}
			return this.DeserializeComponentImpl(manager, reader, root, designXmlSerializerOptions);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003EA6 File Offset: 0x000020A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SerializeComponent(XmlWriter writer, Type rootType, IComponent root)
		{
			new DesignXmlWriter(this.attributesOverrides, this.options, this.userSerializationOptions).SerializeComponent(writer, rootType, root);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003EC7 File Offset: 0x000020C7
		private IComponent DeserializeComponentImpl(IDesignerSerializationManager manager, XmlReader reader, Type root, DesignXmlSerializerOptions serializationOptions)
		{
			return (IComponent)new DesignXmlReader(this.attributesOverrides, serializationOptions, this.DontForceSiteName).DeserializeComponent(manager, reader, root);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003EE9 File Offset: 0x000020E9
		private bool DontForceSiteName
		{
			get
			{
				return (this.options & DesignXmlSerializerOptions.DontForceSiteName) == DesignXmlSerializerOptions.DontForceSiteName;
			}
		}

		// Token: 0x0400004A RID: 74
		private static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings();

		// Token: 0x0400004B RID: 75
		private XmlAttributeOverrides attributesOverrides;

		// Token: 0x0400004C RID: 76
		private DesignXmlSerializerOptions options;

		// Token: 0x0400004D RID: 77
		private Type rootType;

		// Token: 0x0400004E RID: 78
		private IDesignerSerializationManager serializationManager;

		// Token: 0x0400004F RID: 79
		private object userSerializationOptions;
	}
}
