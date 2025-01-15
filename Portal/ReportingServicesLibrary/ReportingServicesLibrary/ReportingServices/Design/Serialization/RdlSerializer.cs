using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000394 RID: 916
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class RdlSerializer
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x0007BB13 File Offset: 0x00079D13
		public RdlSerializer()
			: this(RdlSerializerOptions.Default)
		{
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0007BB1C File Offset: 0x00079D1C
		public RdlSerializer(RdlSerializerOptions options)
			: this(new XmlAttributeOverrides(), options)
		{
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0007BB2A File Offset: 0x00079D2A
		public RdlSerializer(XmlAttributeOverrides overrides, RdlSerializerOptions options)
		{
			this.options = options;
			this.dontForceSiteName = (options & RdlSerializerOptions.DontForceSiteName) == RdlSerializerOptions.DontForceSiteName;
			this.attributesOverrides = overrides;
			new XmlAttributes().XmlIgnore = true;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0007BB58 File Offset: 0x00079D58
		public object DeserializeComponent(TextReader textReader, Type root)
		{
			XmlReader xmlReader = this.CreateXmlReader(textReader, "Microsoft.ReportingServices.Design.Serialization.ReportDefinition.xsd");
			return this.DeserializeComponent(null, xmlReader, root);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0007BB7B File Offset: 0x00079D7B
		public object DeserializeComponent(IDesignerSerializationManager manager, XmlReader reader, Type root)
		{
			return new RdlReader(this.attributesOverrides, this.dontForceSiteName).DeserializeComponent(manager, reader, root);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0007BB96 File Offset: 0x00079D96
		public object DeserializeIntoComponent(IDesignerSerializationManager manager, XmlReader reader, Type rootType, object root)
		{
			return new RdlReader(this.attributesOverrides, this.dontForceSiteName).DeserializeComponent(manager, reader, rootType, root);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0007BBB3 File Offset: 0x00079DB3
		public void SerializeComponent(IDesignerSerializationManager manager, XmlWriter writer, Type rootType, object root)
		{
			this.SerializeComponent(writer, rootType, root);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0007BBBF File Offset: 0x00079DBF
		public void SerializeComponent(XmlWriter writer, Type rootType, object root)
		{
			new RdlWriter(this.attributesOverrides, this.options).SerializeComponent(writer, rootType, root);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0007BBDA File Offset: 0x00079DDA
		private XmlWriter CreateXmlWriter(TextWriter stream)
		{
			return new XmlTextWriter(stream)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			};
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0007BBF0 File Offset: 0x00079DF0
		private XmlWriter CreateXmlWriter(Stream stream)
		{
			return new XmlTextWriter(stream, Encoding.UTF8)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			};
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0007BC0C File Offset: 0x00079E0C
		private XmlReader CreateXmlReader(TextReader textReader, string xsdResourceId)
		{
			return this.CreateXmlReader(new XmlTextReader(textReader)
			{
				ProhibitDtd = true,
				WhitespaceHandling = WhitespaceHandling.None
			}, xsdResourceId);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0007BC36 File Offset: 0x00079E36
		private XmlReader CreateXmlReader(XmlReader xmlReader, string xsdResourceId)
		{
			if ((this.options & RdlSerializerOptions.DontValidateXml) != RdlSerializerOptions.Default)
			{
				return xmlReader;
			}
			return new RdlSerializer.RmlValidatingReader(xmlReader, xsdResourceId);
		}

		// Token: 0x04000CCD RID: 3277
		protected XmlAttributeOverrides attributesOverrides;

		// Token: 0x04000CCE RID: 3278
		protected RdlSerializerOptions options;

		// Token: 0x04000CCF RID: 3279
		protected bool dontForceSiteName;

		// Token: 0x04000CD0 RID: 3280
		internal const string DesignerPrefix = "rd";

		// Token: 0x04000CD1 RID: 3281
		private const string XsdResourceID = "Microsoft.ReportingServices.Design.Serialization.ReportDefinition.xsd";

		// Token: 0x02000509 RID: 1289
		private class RmlValidatingReader : Microsoft.ReportingServices.ReportProcessing.RDLValidatingReader
		{
			// Token: 0x0600250F RID: 9487 RVA: 0x000877C8 File Offset: 0x000859C8
			public RmlValidatingReader(XmlReader xmlReader, string xsdResourceId)
				: base(xmlReader, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")
			{
				base.Schemas.Add(XmlSchema.Read(Assembly.GetExecutingAssembly().GetManifestResourceStream(xsdResourceId), null));
				base.ValidationEventHandler += this.ValidationCallBack;
				base.ValidationType = ValidationType.Schema;
			}

			// Token: 0x06002510 RID: 9488 RVA: 0x00087818 File Offset: 0x00085A18
			public override bool Read()
			{
				string text = "";
				base.Read();
				if (!base.Validate(out text))
				{
					throw new XmlSchemaException(text);
				}
				return !this.EOF;
			}

			// Token: 0x06002511 RID: 9489 RVA: 0x0008784C File Offset: 0x00085A4C
			private void ValidationCallBack(object sender, ValidationEventArgs args)
			{
				if ("http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition" == base.NamespaceURI)
				{
					throw args.Exception;
				}
			}
		}

		// Token: 0x0200050A RID: 1290
		private sealed class XmlNullResolver : XmlUrlResolver
		{
			// Token: 0x06002512 RID: 9490 RVA: 0x00087867 File Offset: 0x00085A67
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new XmlException("Can't resolve URI reference ", null);
			}
		}
	}
}
